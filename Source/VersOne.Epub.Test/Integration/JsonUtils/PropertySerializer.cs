using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class PropertySerializer
    {
        private readonly Func<object, JsonNode?> serializer;

        public PropertySerializer(string propertyName, Func<object, JsonNode?> serializer, bool skipPropertyIfValueIsNull)
        {
            PropertyName = propertyName;
            this.serializer = serializer;
            SkipPropertyIfValueIsNull = skipPropertyIfValueIsNull;
        }

        public string PropertyName { get; }
        public bool SkipPropertyIfValueIsNull { get; }

        public JsonNode? Serialize(object propertyValue)
        {
            return serializer(propertyValue);
        }
    }
}
