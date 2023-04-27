using System.Text.Json;
using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal abstract class JsonSerializationContext
    {
        public abstract JsonNode? SerializePropertyValue(Type type, string propertyName, object serializingObject);
        public abstract object? DeserializePropertyValue(Type type, string propertyName, JsonElement serializedValue);
    }
}
