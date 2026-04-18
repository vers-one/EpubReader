using System.Text.Json;

namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class PropertyDeserializer(string typePropertyName, string jsonPropertyName, Func<JsonElement, JsonSerializationContext, object?> deserializer,
        bool obtainCustomDeserializer)
    {
        private readonly Func<JsonElement, JsonSerializationContext, object?> deserializer = deserializer;

        public string TypePropertyName { get; } = typePropertyName;
        public string JsonPropertyName { get; } = jsonPropertyName;
        public bool ObtainCustomDeserializer { get; } = obtainCustomDeserializer;

        public object? Deserialize(JsonElement serializedPropertyValue, JsonSerializationContext jsonSerializationContext)
        {
            return deserializer(serializedPropertyValue, jsonSerializationContext);
        }
    }
}
