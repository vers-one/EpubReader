using System.Reflection;
using System.Text.Json;
using VersOne.Epub.Test.Integration.JsonUtils.Configuration;

namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class ObjectDeserializer : TypeDeserializer
    {
        private readonly Type objectType;
        private readonly TypeDeserializerCollection typeDeserializerCollection;
        private readonly Lazy<ObjectPropertyDeserializerCollection> propertyDeserializers;

        public ObjectDeserializer(Type objectType, JsonSerializerConfiguration? jsonSerializerConfiguration, TypeDeserializerCollection typeDeserializerCollection)
        {
            this.objectType = objectType;
            this.typeDeserializerCollection = typeDeserializerCollection;
            propertyDeserializers = new Lazy<ObjectPropertyDeserializerCollection>(() =>
                CreatePropertyDeserializers(objectType, jsonSerializerConfiguration?.GetCustomType(objectType), typeDeserializerCollection));
        }

        public override object? Deserialize(JsonElement jsonElement, JsonSerializationContext? jsonSerializationContext)
        {
            if (jsonElement.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            Assert.Equal(JsonValueKind.Object, jsonElement.ValueKind);
            if (objectType.IsAbstract)
            {
                if (!jsonElement.TryGetProperty(Constants.TYPE_PROPERTY_NAME, out JsonElement typeProperty) || typeProperty.ValueKind == JsonValueKind.Null)
                {
                    throw new ArgumentException($"{Constants.TYPE_PROPERTY_NAME} property must be present in JSON for abstract type {objectType.Name}.");
                }
                string? typeName = typeProperty.GetString();
                Assert.NotNull(typeName);
                Type? concreteType = typeof(EpubReader).Assembly.DefinedTypes.FirstOrDefault(definedType => definedType.Name == typeName);
                Assert.NotNull(concreteType);
                TypeDeserializer concreteTypeDeserializer = typeDeserializerCollection.GetDeserializer(concreteType);
                return concreteTypeDeserializer.Deserialize(jsonElement, jsonSerializationContext);
            }
            object?[] constructorParametersValues = new object?[propertyDeserializers.Value.ConstructorParameterDeserializers.Count];
            for (int i = 0; i < constructorParametersValues.Length; i++)
            {
                PropertyDeserializer constructorParameterDeserializer = propertyDeserializers.Value.ConstructorParameterDeserializers[i];
                constructorParametersValues[i] = DeserializeProperty(jsonElement, constructorParameterDeserializer, jsonSerializationContext);
            }
            object? result = Activator.CreateInstance(objectType, constructorParametersValues);
            foreach (PropertyDeserializer standalonePropertyDeserializer in propertyDeserializers.Value.StandalonePropertyDeserializers)
            {
                object? propertyValue = DeserializeProperty(jsonElement, standalonePropertyDeserializer, jsonSerializationContext);
                if (propertyValue != null)
                {
                    PropertyInfo? propertyInfo = objectType.GetProperty(standalonePropertyDeserializer.TypePropertyName);
                    Assert.NotNull(propertyInfo);
                    propertyInfo.SetValue(result, propertyValue);
                }
            }
            return result;
        }

        private object? DeserializeProperty(JsonElement objectJsonElement, PropertyDeserializer propertyDeserializer, JsonSerializationContext? jsonSerializationContext)
        {
            if (!objectJsonElement.TryGetProperty(propertyDeserializer.JsonPropertyName, out JsonElement serializedPropertyValue) ||
                serializedPropertyValue.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            if (propertyDeserializer.ObtainCustomDeserializer)
            {
                if (jsonSerializationContext == null)
                {
                    throw new ArgumentException(
                        $"JSON serialization context is required to deserialize property {propertyDeserializer.TypePropertyName} in the type {objectType.Name}.");
                }
                return jsonSerializationContext.DeserializePropertyValue(objectType, propertyDeserializer.TypePropertyName, serializedPropertyValue);
            }
            else
            {
                return propertyDeserializer.Deserialize(serializedPropertyValue, jsonSerializationContext);
            }
        }

        private static ObjectPropertyDeserializerCollection CreatePropertyDeserializers(Type type, CustomType? customType, TypeDeserializerCollection typeDeserializerCollection)
        {
            ObjectPropertyDeserializerCollection result = new();
            if (!type.IsAbstract)
            {
                ConstructorInfo constructorInfo = GetConstructorWithMostParameters(type);
                HashSet<string> propertiesSetInConstructor = new();
                foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
                {
                    Assert.NotNull(parameterInfo.Name);
                    string propertyName = Char.ToUpper(parameterInfo.Name[0]) + parameterInfo.Name[1..];
                    propertiesSetInConstructor.Add(propertyName);
                    PropertyDeserializer? propertyDeserializer = CreatePropertyDeserializer(type, customType, typeDeserializerCollection, propertyName, parameterInfo.ParameterType);
                    Assert.NotNull(propertyDeserializer);
                    result.ConstructorParameterDeserializers.Add(propertyDeserializer);
                }
                foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public).
                    Where(property => !propertiesSetInConstructor.Contains(property.Name)))
                {
                    PropertyDeserializer? propertyDeserializer = CreatePropertyDeserializer(type, customType, typeDeserializerCollection, propertyInfo.Name, propertyInfo.PropertyType);
                    if (propertyDeserializer != null)
                    {
                        result.StandalonePropertyDeserializers.Add(propertyDeserializer);
                    }
                }
            }
            return result;
        }

        private static PropertyDeserializer? CreatePropertyDeserializer(Type type, CustomType? customType, TypeDeserializerCollection typeDeserializerCollection,
            string propertyName, Type propertyType)
        {
            CustomProperty? customProperty = customType?.GetCustomProperty(propertyName);
            if (customProperty != null)
            {
                if (customProperty.UsesCustomSerialization)
                {
                    return new PropertyDeserializer(propertyName, customProperty.JsonPropertyName,
                        (JsonElement _, JsonSerializationContext? _) =>
                            throw new InvalidOperationException($"Custom deserializer should be obtained for property {propertyName} in the type {type.Name}."),
                        obtainCustomDeserializer: true);
                }
                else if (customProperty.IsIgnored)
                {
                    return null;
                }
            }
            TypeDeserializer parameterTypeDeserializer = typeDeserializerCollection.GetDeserializer(propertyType);
            return new PropertyDeserializer(propertyName, propertyName, parameterTypeDeserializer.Deserialize, obtainCustomDeserializer: false);
        }

        private static ConstructorInfo GetConstructorWithMostParameters(Type type)
        {
            ConstructorInfo? result = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public).
                OrderByDescending(constructor => constructor.GetParameters().Length).
                FirstOrDefault();
            Assert.NotNull(result);
            return result;
        }
    }
}
