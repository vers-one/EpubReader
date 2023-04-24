using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.JsonUtils.Serializers
{
    internal class EnumSerializer : TypeSerializer
    {
        public override JsonNode? Serialize(object? value, JsonSerializationContext? _)
        {
            return value != null ? JsonValue.Create(Enum.GetName(value.GetType(), value)) : null;
        }
    }
}
