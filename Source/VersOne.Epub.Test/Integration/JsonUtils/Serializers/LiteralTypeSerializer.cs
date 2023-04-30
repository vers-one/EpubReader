using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.JsonUtils.Serializers
{
    internal class LiteralTypeSerializer : TypeSerializer
    {
        public override JsonNode? Serialize(object? value, JsonSerializationContext _)
        {
            return JsonValue.Create(value);
        }
    }
}
