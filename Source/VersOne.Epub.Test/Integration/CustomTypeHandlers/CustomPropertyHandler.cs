using System.Text.Json;
using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal interface ICustomPropertyHandler
    {
        public string TypePropertyName { get; }
        public string JsonPropertyName { get; }
        public string ConstructorParameterName { get; }
        public JsonNode? SerializePropertyValue(object serializingObject);
        public object? DeserializePropertyValue(JsonElement serializedValue);
    }

    internal class CustomPropertyHandler<T> : ICustomPropertyHandler where T : class
    {
        private readonly Func<T, JsonNode?> propertySerializer;
        private readonly Func<string?, object?> propertyDeserializer;

        public CustomPropertyHandler(string typePropertyName, string jsonPropertyName, Func<T, JsonNode?> propertySerializer, Func<string?, object?> propertyDeserializer)
        {
            this.propertySerializer = propertySerializer;
            this.propertyDeserializer = propertyDeserializer;
            TypePropertyName = typePropertyName ?? throw new ArgumentNullException(nameof(typePropertyName));
            JsonPropertyName = jsonPropertyName ?? throw new ArgumentNullException(nameof(jsonPropertyName));
            ConstructorParameterName = Char.ToLower(typePropertyName[0]) + typePropertyName[1..];
        }

        public string TypePropertyName { get; }
        public string JsonPropertyName { get; }
        public string ConstructorParameterName { get; }

        public JsonNode? SerializePropertyValue(object serializingObject)
        {
            T? typedSerializingObject = serializingObject as T;
            Assert.NotNull(typedSerializingObject);
            return propertySerializer(typedSerializingObject);
        }

        public object? DeserializePropertyValue(JsonElement serializedValue)
        {
            Assert.Equal(JsonValueKind.String, serializedValue.ValueKind);
            string? stringValue = serializedValue.GetString();
            return propertyDeserializer(stringValue);
        }
    }
}
