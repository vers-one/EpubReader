using System.Text.Json;

namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class PropertyDeserializer
    {
        private readonly Func<JsonElement, JsonSerializationContext?, object?> deserializer;

        public PropertyDeserializer(string typePropertyName, string jsonPropertyName, Func<JsonElement, JsonSerializationContext?, object?> deserializer, bool obtainCustomDeserializer)
        {
            TypePropertyName = typePropertyName;
            JsonPropertyName = jsonPropertyName;
            this.deserializer = deserializer;
            ObtainCustomDeserializer = obtainCustomDeserializer;
        }

        public string TypePropertyName { get; }
        public string JsonPropertyName { get; }
        public bool ObtainCustomDeserializer { get; }

        public object? Deserialize(JsonElement serializedPropertyValue, JsonSerializationContext? jsonSerializationContext)
        {
            return deserializer(serializedPropertyValue, jsonSerializationContext);
        }
    }
}
