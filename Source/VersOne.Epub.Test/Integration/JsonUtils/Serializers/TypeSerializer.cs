using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.JsonUtils.Serializers
{
    internal abstract class TypeSerializer
    {
        public abstract JsonNode? Serialize(object? value, JsonSerializationContext? jsonSerializationContext);
    }
}
