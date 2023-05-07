using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.JsonUtils.Configuration;

namespace VersOne.Epub.Test.Integration.JsonUtils.Serializers
{
    internal class ObjectSerializer : TypeSerializer
    {
        private readonly Type objectType;
        private readonly CustomType? customType;
        private readonly TypeSerializerCollection typeSerializerCollection;
        private readonly Lazy<List<PropertySerializer>> propertySerializers;

        public ObjectSerializer(Type objectType, JsonSerializerConfiguration? jsonSerializerConfiguration, TypeSerializerCollection typeSerializerCollection)
        {
            this.objectType = objectType;
            customType = jsonSerializerConfiguration?.GetCustomType(objectType);
            this.typeSerializerCollection = typeSerializerCollection;
            propertySerializers = new Lazy<List<PropertySerializer>>(() => CreatePropertySerializers(objectType, customType, typeSerializerCollection));
        }

        public override JsonNode? Serialize(object? value, JsonSerializationContext jsonSerializationContext)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                JsonObject result = new();
                Type actualType = value.GetType();
                if (actualType != objectType)
                {
                    ObjectSerializer? actualTypeSerializer = typeSerializerCollection.GetSerializer(actualType) as ObjectSerializer;
                    Assert.NotNull(actualTypeSerializer);
                    actualTypeSerializer.SerializeIntoJsonObject(value, true, jsonSerializationContext, result);
                }
                else
                {
                    SerializeIntoJsonObject(value, false, jsonSerializationContext, result);
                }
                return result;
            }
        }

        private void SerializeIntoJsonObject(object value, bool writeTypeName, JsonSerializationContext jsonSerializationContext, JsonObject result)
        {
            if (customType?.PreserveReferences == true)
            {
                (int referenceNumber, bool isDuplicateReference) = jsonSerializationContext.GetReferenceNumber(value);
                if (isDuplicateReference)
                {
                    result.Add(Constants.DUPLICATE_REFERENCE_NUMBER_PROPERTY_NAME, referenceNumber);
                    return;
                }
                else
                {
                    result.Add(Constants.NEW_REFERENCE_NUMBER_PROPERTY_NAME, referenceNumber);
                }
            }
            if (writeTypeName)
            {
                result.Add(Constants.TYPE_PROPERTY_NAME, objectType.Name);
            }
            foreach (PropertySerializer propertySerializer in propertySerializers.Value)
            {
                JsonNode? serializedProperty;
                if (propertySerializer.ObtainCustomSerializer)
                {
                    if (jsonSerializationContext == null)
                    {
                        throw new ArgumentException($"JSON serialization context is required to serialize property {propertySerializer.TypePropertyName} in the type {objectType.Name}.");
                    }
                    serializedProperty = jsonSerializationContext.SerializePropertyValue(objectType, propertySerializer.TypePropertyName, value);
                }
                else
                {
                    serializedProperty = propertySerializer.Serialize(value, jsonSerializationContext);
                }
                if (serializedProperty != null || !propertySerializer.SkipPropertyIfValueIsNull)
                {
                    result.Add(propertySerializer.JsonPropertyName, serializedProperty);
                }
            }
        }

        private static List<PropertySerializer> CreatePropertySerializers(Type type, CustomType? customType, TypeSerializerCollection typeSerializerCollection)
        {
            List<PropertySerializer> result = new();
            foreach (PropertyInfo propertyInfo in type.GetRuntimeProperties())
            {
                string propertyName = propertyInfo.Name;
                CustomProperty? customProperty = customType?.GetCustomProperty(propertyName);
                if (customProperty != null)
                {
                    if (customProperty.UsesCustomSerialization)
                    {
                        result.Add(new PropertySerializer(propertyName, customProperty.JsonPropertyName,
                            (object _, JsonSerializationContext _) =>
                                throw new InvalidOperationException($"Custom serializer should be obtained for property {propertyName} in the type {type.Name}."),
                            skipPropertyIfValueIsNull: false, obtainCustomSerializer: true));
                        continue;
                    }
                    else if (customProperty.IsIgnored)
                    {
                        continue;
                    }
                }
                Type propertyType = propertyInfo.PropertyType;
                ParameterExpression inputParameterExpression = Expression.Parameter(typeof(object));
                Expression inputCastToTypeExpression = Expression.Convert(inputParameterExpression, type);
                MemberExpression getPropertyValueExpression = Expression.Property(inputCastToTypeExpression, propertyInfo);
                Expression propertyValueCastToObjectExpression = Expression.Convert(getPropertyValueExpression, typeof(object));
                Func<object, object?> propertyAccessor = Expression.Lambda<Func<object, object?>>(propertyValueCastToObjectExpression, inputParameterExpression).Compile();
                TypeSerializer propertyTypeSerializer = typeSerializerCollection.GetSerializer(propertyType);
                Func<object?, JsonSerializationContext, JsonNode?> propertyValueSerializer = propertyTypeSerializer.Serialize;
                JsonNode? propertySerializer(object @object, JsonSerializationContext jsonSerializationContext) =>
                    propertyValueSerializer(propertyAccessor(@object), jsonSerializationContext);
                PropertyDefaultValue? propertyDefaultValue = customProperty?.OptionalPropertyValue;
                if (propertyDefaultValue != null)
                {
                    JsonNode? optionalPropertySerializer(object @object, JsonSerializationContext jsonSerializationContext)
                    {
                        JsonNode? serializedProperty = propertySerializer(@object, jsonSerializationContext);
                        if (propertyDefaultValue?.HasFlag(PropertyDefaultValue.NULL) == true)
                        {
                            if (serializedProperty == null)
                            {
                                return null;
                            }
                        }
                        if (propertyDefaultValue?.HasFlag(PropertyDefaultValue.FALSE) == true)
                        {
                            if (serializedProperty is JsonValue serializedValue && serializedValue.TryGetValue(out bool booleanValue) && booleanValue == false)
                            {
                                return null;
                            }
                        }
                        if (propertyDefaultValue?.HasFlag(PropertyDefaultValue.EMPTY_ARRAY) == true)
                        {
                            if (serializedProperty is JsonArray serializedArray && serializedArray.Count == 0)
                            {
                                return null;
                            }
                        }
                        if (propertyDefaultValue?.HasFlag(PropertyDefaultValue.EMPTY_OBJECT) == true || propertyDefaultValue?.HasFlag(PropertyDefaultValue.EMPTY_DICTIONARY) == true)
                        {
                            if (serializedProperty is JsonObject serializedObject && serializedObject.Count == 0)
                            {
                                return null;
                            }
                        }
                        return serializedProperty;
                    }
                    result.Add(new PropertySerializer(propertyName, propertyName, optionalPropertySerializer, skipPropertyIfValueIsNull: true, obtainCustomSerializer: false));
                }
                else
                {
                    result.Add(new PropertySerializer(propertyName, propertyName, propertySerializer, skipPropertyIfValueIsNull: false, obtainCustomSerializer: false));
                }
            }
            return result;
        }
    }
}
