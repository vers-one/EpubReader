using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomTypeHandlers;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class TypeSerializer
    {
        private const string TYPE_PROPERTY_NAME = "$type";

        private readonly Type type;
        private readonly Dictionary<Type, TypeSerializer> typeSerializers;
        private readonly Dictionary<Type, ICustomTypeHandler> customTypeHandlers;
        private readonly ICustomTypeHandler? customTypeHandler;
        private readonly Lazy<List<PropertySerializer>> propertySerializers;
        private readonly Lazy<List<PropertyDeserializer>> propertyDeserializers;

        public TypeSerializer(Type type, Dictionary<Type, TypeSerializer> typeSerializers, Dictionary<Type, ICustomTypeHandler> customTypeHandlers)
        {
            this.type = type;
            this.typeSerializers = typeSerializers;
            this.customTypeHandlers = customTypeHandlers;
            customTypeHandlers.TryGetValue(type, out customTypeHandler);
            propertySerializers = new Lazy<List<PropertySerializer>>(() => CreatePropertySerializers());
            propertyDeserializers = new Lazy<List<PropertyDeserializer>>(() => CreatePropertyDeserializers());
        }

        public JsonNode SerializeObject(object @object, Type? declaredType = null)
        {
            JsonObject result = new();
            if (declaredType != null && type != declaredType)
            {
                result.Add(TYPE_PROPERTY_NAME, type.Name);
            }
            foreach (PropertySerializer propertySerializer in propertySerializers.Value)
            {
                JsonNode? serializedProperty = propertySerializer.Serialize(@object);
                if (serializedProperty != null || !propertySerializer.SkipPropertyIfValueIsNull)
                {
                    result.Add(propertySerializer.PropertyName, serializedProperty);
                }
            }
            return result;
        }

        public object? DeserializeObject(JsonElement jsonElement)
        {
            if (jsonElement.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            Assert.Equal(JsonValueKind.Object, jsonElement.ValueKind);
            if (type.IsAbstract)
            {
                if (!jsonElement.TryGetProperty(TYPE_PROPERTY_NAME, out JsonElement typeProperty) || typeProperty.ValueKind == JsonValueKind.Null)
                {
                    throw new ArgumentException($"{TYPE_PROPERTY_NAME} property must be present in JSON for abstract types.");
                }
                string? typeName = typeProperty.GetString();
                Assert.NotNull(typeName);
                Type? concreteType = typeof(EpubReader).Assembly.DefinedTypes.FirstOrDefault(definedType => definedType.Name == typeName);
                Assert.NotNull(concreteType);
                if (!typeSerializers.TryGetValue(concreteType, out TypeSerializer? concreteTypeSerializer))
                {
                    concreteTypeSerializer = new TypeSerializer(concreteType, typeSerializers, customTypeHandlers);
                    typeSerializers.Add(concreteType, concreteTypeSerializer);
                }
                return concreteTypeSerializer.DeserializeObject(jsonElement);
            }
            Dictionary<string, bool> handledProperties = jsonElement.EnumerateObject().ToDictionary(property => property.Name, _ => false);
            object?[] constructorParametersValues = new object?[propertyDeserializers.Value.Count];
            for (int i = 0; i < constructorParametersValues.Length; i++)
            {
                PropertyDeserializer propertyDeserializer = propertyDeserializers.Value[i];
                string propertyName = propertyDeserializer.PropertyName;
                handledProperties[propertyName] = true;
                if (!jsonElement.TryGetProperty(propertyName, out JsonElement serializedProperty) || serializedProperty.ValueKind == JsonValueKind.Null)
                {
                    continue;
                }
                constructorParametersValues[i] = propertyDeserializer.Deserialize(serializedProperty);
            }
            object? result = Activator.CreateInstance(type, constructorParametersValues);
            foreach (string notHandledPropertyName in handledProperties.Where(property => !property.Value).Select(property => property.Key))
            {
                if (notHandledPropertyName == TYPE_PROPERTY_NAME)
                {
                    continue;
                }
                if (!jsonElement.TryGetProperty(notHandledPropertyName, out JsonElement serializedProperty) || serializedProperty.ValueKind == JsonValueKind.Null)
                {
                    continue;
                }
                PropertyInfo? propertyInfo = type.GetProperty(notHandledPropertyName);
                Assert.NotNull(propertyInfo);
                Func<JsonElement, object?> notHandledPropertyDeserializer = CreateObjectValueDeserializer(propertyInfo.PropertyType);
                propertyInfo.SetValue(result, notHandledPropertyDeserializer(serializedProperty));
            }
            return result;
        }

        private List<PropertySerializer> CreatePropertySerializers()
        {
            List<PropertySerializer> result = new();
            foreach (PropertyInfo propertyInfo in type.GetRuntimeProperties())
            {
                string propertyName = propertyInfo.Name;
                if (customTypeHandler != null && (customTypeHandler.CustomPropertyHandlers.ContainsKey(propertyName) || customTypeHandler.IgnoredProperties.Contains(propertyName)))
                {
                    continue;
                }
                else
                {
                    Type propertyType = propertyInfo.PropertyType;
                    ParameterExpression inputParameterExpression = Expression.Parameter(typeof(object));
                    Expression inputToTypeExpression = Expression.Convert(inputParameterExpression, type);
                    MemberExpression getPropertyValueExpression = Expression.Property(inputToTypeExpression, propertyInfo);
                    Expression propertyValueToObjectExpression = Expression.Convert(getPropertyValueExpression, typeof(object));
                    Func<object, object?> propertyAccessor = Expression.Lambda<Func<object, object?>>(propertyValueToObjectExpression, inputParameterExpression).Compile();
                    Func<object?, JsonNode?> valueSerializer = CreateObjectValueSerializer(propertyType);
                    JsonNode? propertySerializer(object @object) => valueSerializer(propertyAccessor(@object));
                    if (customTypeHandler != null && customTypeHandler.OptionalProperties.TryGetValue(propertyName, out PropertyDefaultValue propertyDefaultValue))
                    {
                        JsonNode? optionalPropertySerializer(object @object)
                        {
                            JsonNode? serializedProperty = propertySerializer(@object);
                            switch (propertyDefaultValue)
                            {
                                case PropertyDefaultValue.NULL:
                                    if (serializedProperty == null)
                                    {
                                        return null;
                                    }
                                    break;
                                case PropertyDefaultValue.FALSE:
                                    if (serializedProperty is JsonValue serializedValue && serializedValue.TryGetValue(out bool booleanValue) && booleanValue == false)
                                    {
                                        return null;
                                    }
                                    break;
                                case PropertyDefaultValue.EMPTY_ARRAY:
                                    if (serializedProperty is JsonArray serializedArray && serializedArray.Count == 0)
                                    {
                                        return null;
                                    }
                                    break;
                                case PropertyDefaultValue.EMPTY_OBJECT:
                                    if (serializedProperty is JsonObject serializedObject && serializedObject.Count == 0)
                                    {
                                        return null;
                                    }
                                    break;
                            }
                            return serializedProperty;
                        }
                        result.Add(new PropertySerializer(propertyName, optionalPropertySerializer, skipPropertyIfValueIsNull: true));
                    }
                    else
                    {
                        result.Add(new PropertySerializer(propertyName, propertySerializer, skipPropertyIfValueIsNull: false));
                    }
                }
            }
            if (customTypeHandler != null)
            {
                foreach (ICustomPropertyHandler customPropertyHandler in customTypeHandler.CustomPropertyHandlers.Values)
                {
                    result.Add(new PropertySerializer(customPropertyHandler.JsonPropertyName, customPropertyHandler.SerializePropertyValue, skipPropertyIfValueIsNull: false));
                }
            }
            return result;
        }

        private Func<object?, JsonNode?> CreateObjectValueSerializer(Type declaredObjectType)
        {
            Type serializingType = declaredObjectType;
            Type? underlyingTypeOfNullableType = Nullable.GetUnderlyingType(serializingType);
            if (underlyingTypeOfNullableType != null)
            {
                serializingType = underlyingTypeOfNullableType;
            }
            if (serializingType.IsValueType || serializingType == typeof(string))
            {
                if (serializingType.IsEnum)
                {
                    return value => SerializeEnum(serializingType, value);
                }
                else
                {
                    return value => SerializeValueType(value);
                }
            }
            else if (typeof(IEnumerable).IsAssignableFrom(serializingType))
            {
                if (typeof(IDictionary).IsAssignableFrom(serializingType))
                {
                    return value => SerializeDictionary(value);
                }
                else
                {
                    return value => SerializeList(value);
                }
            }
            else
            {
                return value => SerializeNestedObject(serializingType, value);
            }
        }

        private static JsonNode? SerializeValueType(object? value)
        {
            return JsonValue.Create(value);
        }

        private static JsonNode? SerializeEnum(Type enumType, object? value)
        {
            return value != null ? JsonValue.Create(Enum.GetName(enumType, value)) : null;
        }

        private JsonNode? SerializeList(object? value)
        {
            if (value is not IList list)
            {
                return null;
            }
            JsonArray array = new();
            Type listType = list.GetType();
            Type listItemType = listType.IsGenericType ? listType.GetGenericArguments().First() : throw new ArgumentException($"{listType.Name} is not a generic List<T> type.");
            Func<object?, JsonNode?> listItemSerializer = CreateObjectValueSerializer(listItemType);
            foreach (object? listItem in list)
            {
                array.Add(listItemSerializer(listItem));
            }
            return array;
        }

        private JsonNode? SerializeDictionary(object? value)
        {
            if (value is not IDictionary dictionary)
            {
                return null;
            }
            JsonObject dictionaryObject = new();
            Type dictionaryType = dictionary.GetType();
            Type dictionaryValueType = dictionaryType.IsGenericType ?
                dictionaryType.GetGenericArguments()[1] : throw new ArgumentException($"{dictionaryType.Name} is not a generic Dictionary<K,V> type.");
            Func<object?, JsonNode?> dictionaryValueSerializer = CreateObjectValueSerializer(dictionaryValueType);
            foreach (DictionaryEntry dictionaryItem in dictionary)
            {
                if (dictionaryItem.Key is not string key)
                {
                    throw new ArgumentException($"Expected string dictionary key type but got {dictionaryItem.Key.GetType().Name}.");
                }
                dictionaryObject.Add(key, dictionaryValueSerializer(dictionaryItem.Value));
            }
            return dictionaryObject;
        }

        private JsonNode? SerializeNestedObject(Type declaredObjectType, object? @object)
        {
            if (@object == null)
            {
                return null;
            }
            Type propertyValueType = @object.GetType();
            if (!typeSerializers.TryGetValue(propertyValueType, out TypeSerializer? typeSerializer))
            {
                typeSerializer = new TypeSerializer(propertyValueType, typeSerializers, customTypeHandlers);
                typeSerializers.Add(propertyValueType, typeSerializer);
            }
            return typeSerializer.SerializeObject(@object, declaredObjectType);
        }

        private List<PropertyDeserializer> CreatePropertyDeserializers()
        {
            List<PropertyDeserializer> result = new();
            ConstructorInfo constructorInfo = GetConstructorWithMostParameters();
            foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
            {
                Assert.NotNull(parameterInfo.Name);
                string parameterName = Char.ToUpper(parameterInfo.Name[0]) + parameterInfo.Name[1..];
                if (customTypeHandler != null && customTypeHandler.CustomPropertyHandlers.TryGetValue(parameterName, out ICustomPropertyHandler? customPropertyHandler))
                {
                    result.Add(new PropertyDeserializer(customPropertyHandler.JsonPropertyName, customPropertyHandler.DeserializePropertyValue));
                }
                else
                {
                    result.Add(new PropertyDeserializer(parameterName, CreateObjectValueDeserializer(parameterInfo.ParameterType)));
                }
            }
            return result;
        }

        private Func<JsonElement, object?> CreateObjectValueDeserializer(Type declaredObjectType)
        {
            Type? underlyingTypeOfNullableType = Nullable.GetUnderlyingType(declaredObjectType);
            if (underlyingTypeOfNullableType != null)
            {
                declaredObjectType = underlyingTypeOfNullableType;
            }
            if (declaredObjectType.IsValueType || declaredObjectType == typeof(string))
            {
                if (declaredObjectType.IsEnum)
                {
                    return jsonElement => DeserializeEnum(declaredObjectType, jsonElement);
                }
                else
                {
                    return DeserializeValueType;
                }
            }
            else if (typeof(IEnumerable).IsAssignableFrom(declaredObjectType))
            {
                if (typeof(IDictionary).IsAssignableFrom(declaredObjectType))
                {
                    return jsonElement => DeserializeDictionary(declaredObjectType, jsonElement);
                }
                else
                {
                    return jsonElement => DeserializeList(declaredObjectType, jsonElement);
                }
            }
            else
            {
                return jsonElement => DeserializeNestedObject(declaredObjectType, jsonElement);
            }
        }

        private static object? DeserializeValueType(JsonElement jsonElement)
        {
            return jsonElement.ValueKind switch
            {
                JsonValueKind.Null => null,
                JsonValueKind.String => jsonElement.GetString(),
                JsonValueKind.Number => jsonElement.GetInt32(),
                JsonValueKind.True or JsonValueKind.False => jsonElement.GetBoolean(),
                _ => throw new ArgumentException($"Unexpected JSON value kind: {jsonElement.ValueKind} while trying to deserialize JSON value."),
            };
        }

        private static object? DeserializeEnum(Type enumType, JsonElement jsonElement)
        {
            Assert.Equal(JsonValueKind.String, jsonElement.ValueKind);
            string? stringValue = jsonElement.GetString();
            Assert.NotNull(stringValue);
            if (!Enum.TryParse(enumType, stringValue, out object? result))
            {
                throw new ArgumentException($"{stringValue} is not defined in {enumType.Name}.");
            }
            return result;
        }

        private object? DeserializeList(Type listType, JsonElement jsonElement)
        {
            Assert.Equal(JsonValueKind.Array, jsonElement.ValueKind);
            Type listItemType = listType.IsGenericType ? listType.GetGenericArguments().First() : throw new ArgumentException($"{listType.Name} is not a generic List<T> type.");
            Func<JsonElement, object?> listItemDeserializer = CreateObjectValueDeserializer(listItemType);
            IList? result = Activator.CreateInstance(listType, new object[] { jsonElement.GetArrayLength() }) as IList;
            Assert.NotNull(result);
            foreach (JsonElement serializedListItem in jsonElement.EnumerateArray())
            {
                result.Add(listItemDeserializer(serializedListItem));
            }
            return result;
        }

        private object? DeserializeDictionary(Type dictionaryType, JsonElement jsonElement)
        {
            Assert.Equal(JsonValueKind.Object, jsonElement.ValueKind);
            if (!dictionaryType.IsGenericType || dictionaryType.GenericTypeArguments.Length != 2)
            {
                throw new ArgumentException($"{dictionaryType.Name} is not a generic Dictionary<K,V> type.");
            }
            Type dictionaryKeyType = dictionaryType.GenericTypeArguments[0];
            if (dictionaryKeyType != typeof(string))
            {
                throw new ArgumentException($"Expected string dictionary key type but got {dictionaryKeyType.Name}.");
            }
            Type dictionaryValueType = dictionaryType.GenericTypeArguments[1];
            Func<JsonElement, object?> dictionaryValueDeserializer = CreateObjectValueDeserializer(dictionaryValueType);
            IDictionary? result = Activator.CreateInstance(dictionaryType) as IDictionary;
            Assert.NotNull(result);
            foreach (JsonProperty serializedDictionaryItem in jsonElement.EnumerateObject())
            {
                result.Add(serializedDictionaryItem.Name, dictionaryValueDeserializer(serializedDictionaryItem.Value));
            }
            return result;
        }

        private object? DeserializeNestedObject(Type declaredObjectType, JsonElement jsonElement)
        {
            if (!typeSerializers.TryGetValue(declaredObjectType, out TypeSerializer? typeSerializer))
            {
                typeSerializer = new TypeSerializer(declaredObjectType, typeSerializers, customTypeHandlers);
                typeSerializers.Add(declaredObjectType, typeSerializer);
            }
            return typeSerializer.DeserializeObject(jsonElement);
        }

        private ConstructorInfo GetConstructorWithMostParameters()
        {
            ConstructorInfo? result = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public).
                OrderByDescending(constructor => constructor.GetParameters().Length).
                FirstOrDefault();
            Assert.NotNull(result);
            return result;
        }
    }
}
