using System.Text.Json;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class PropertyDeserializer
    {
        private readonly Func<JsonElement, object?> deserializer;

        public PropertyDeserializer(string propertyName, Func<JsonElement, object?> deserializer)
        {
            PropertyName = propertyName;
            this.deserializer = deserializer;
        }

        public string PropertyName { get; }

        public object? Deserialize(JsonElement serializedPropertyValue)
        {
            return deserializer(serializedPropertyValue);
        }
    }
}
