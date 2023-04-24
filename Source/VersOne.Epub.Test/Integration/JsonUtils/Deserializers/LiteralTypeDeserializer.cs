using System.Text.Json;

namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class LiteralTypeDeserializer : TypeDeserializer
    {
        public override object? Deserialize(JsonElement jsonElement, JsonSerializationContext? _)
        {
            return jsonElement.ValueKind switch
            {
                JsonValueKind.Null => null,
                JsonValueKind.String => jsonElement.GetString(),
                JsonValueKind.Number => jsonElement.GetInt32(),
                JsonValueKind.True or JsonValueKind.False => jsonElement.GetBoolean(),
                _ => throw new ArgumentException($"Unexpected JSON value kind: {jsonElement.ValueKind} while trying to deserialize JSON value."),
            };
        }
    }
}
