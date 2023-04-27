using System.Text.Json;

namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal abstract class TypeDeserializer
    {
        public abstract object? Deserialize(JsonElement jsonElement, JsonSerializationContext? jsonSerializationContext);
    }
}
