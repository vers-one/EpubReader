using System.Text.Json;
using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.JsonUtils.Configuration;
using VersOne.Epub.Test.Integration.JsonUtils.Deserializers;
using VersOne.Epub.Test.Integration.JsonUtils.Serializers;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class JsonSerializer
    {
        private readonly Lazy<TypeSerializerCollection> typeSerializers;
        private readonly Lazy<TypeDeserializerCollection> typeDeserializers;

        public JsonSerializer(JsonSerializerConfiguration? configuration)
        {
            typeSerializers = new Lazy<TypeSerializerCollection>(() => new TypeSerializerCollection(configuration));
            typeDeserializers = new Lazy<TypeDeserializerCollection>(() => new TypeDeserializerCollection(configuration));
        }

        public JsonNode? Serialize<T>(T? value, JsonSerializationContext? jsonSerializationContext = null)
        {
            TypeSerializer typeSerializer = typeSerializers.Value.GetSerializer(typeof(T));
            return typeSerializer.Serialize(value, jsonSerializationContext);
        }

        public T? Deserialize<T>(JsonElement jsonElement, JsonSerializationContext? jsonSerializationContext = null) where T : class
        {
            TypeDeserializer typeDeserializer = typeDeserializers.Value.GetDeserializer(typeof(T));
            return typeDeserializer.Deserialize(jsonElement, jsonSerializationContext) as T;
        }
    }
}
