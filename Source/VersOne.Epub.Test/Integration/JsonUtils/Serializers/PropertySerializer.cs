using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.JsonUtils.Serializers
{
    internal class PropertySerializer
    {
        private readonly Func<object, JsonSerializationContext?, JsonNode?> serializer;

        public PropertySerializer(string typePropertyName, string jsonPropertyName, Func<object, JsonSerializationContext?, JsonNode?> serializer,
            bool skipPropertyIfValueIsNull, bool obtainCustomSerializer)
        {
            TypePropertyName = typePropertyName;
            JsonPropertyName = jsonPropertyName;
            this.serializer = serializer;
            SkipPropertyIfValueIsNull = skipPropertyIfValueIsNull;
            ObtainCustomSerializer = obtainCustomSerializer;
        }

        public string TypePropertyName { get; }
        public string JsonPropertyName { get; }
        public bool SkipPropertyIfValueIsNull { get; }
        public bool ObtainCustomSerializer { get; }

        public JsonNode? Serialize(object propertyValue, JsonSerializationContext? jsonSerializationContext)
        {
            return serializer(propertyValue, jsonSerializationContext);
        }
    }
}
