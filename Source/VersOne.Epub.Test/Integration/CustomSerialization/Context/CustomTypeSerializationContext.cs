using System.Text.Json;
using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers;

namespace VersOne.Epub.Test.Integration.CustomSerialization.Context
{
    internal class CustomTypeSerializationContext
    {
        private readonly CustomTypeSerializer customTypeSerializer;
        private readonly Dictionary<string, CustomPropertySerializationContext> customProperties;

        public CustomTypeSerializationContext(CustomTypeSerializer customTypeSerializer, TestEpubFile testEpubFile)
        {
            this.customTypeSerializer = customTypeSerializer;
            customProperties = customTypeSerializer.CustomPropertySerializers.ToDictionary(customPropertySerializer => customPropertySerializer.Key,
                customPropertySerializer => new CustomPropertySerializationContext(customPropertySerializer.Value, testEpubFile));
        }

        public JsonNode? SerializePropertyValue(string propertyName, object serializingObject)
        {
            if (!customProperties.TryGetValue(propertyName, out CustomPropertySerializationContext? customPropertySerializationContext))
            {
                throw new ArgumentException($"There is no custom property serializer for property {propertyName} in the type {customTypeSerializer.Type.Name}.");
            }
            return customPropertySerializationContext.SerializePropertyValue(serializingObject);
        }

        public object? DeserializePropertyValue(string propertyName, JsonElement serializedValue)
        {
            if (!customProperties.TryGetValue(propertyName, out CustomPropertySerializationContext? customPropertySerializationContext))
            {
                throw new ArgumentException($"There is no custom property deserializer for property {propertyName} in the type {customTypeSerializer.Type.Name}.");
            }
            return customPropertySerializationContext.DeserializePropertyValue(serializedValue);
        }
    }
}
