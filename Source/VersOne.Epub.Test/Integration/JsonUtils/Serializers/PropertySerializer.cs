using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.JsonUtils.Serializers
{
    internal class PropertySerializer(string typePropertyName, string jsonPropertyName, Func<object, JsonSerializationContext, JsonNode?> serializer,
        bool skipPropertyIfValueIsNull, bool obtainCustomSerializer)
    {
        private readonly Func<object, JsonSerializationContext, JsonNode?> serializer = serializer;

        public string TypePropertyName { get; } = typePropertyName;
        public string JsonPropertyName { get; } = jsonPropertyName;
        public bool SkipPropertyIfValueIsNull { get; } = skipPropertyIfValueIsNull;
        public bool ObtainCustomSerializer { get; } = obtainCustomSerializer;

        public JsonNode? Serialize(object propertyValue, JsonSerializationContext jsonSerializationContext)
        {
            return serializer(propertyValue, jsonSerializationContext);
        }
    }
}
