using System.Text.Json;
using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers;
using VersOne.Epub.Test.Integration.JsonUtils;

namespace VersOne.Epub.Test.Integration.CustomSerialization.Context
{
    internal class TestCaseSerializationContext : JsonSerializationContext
    {
        private readonly Dictionary<Type, Lazy<CustomTypeSerializationContext>> customTypes;

        public TestCaseSerializationContext(TestEpubFile testEpubFile)
        {
            customTypes = CustomTypeSerializers.TypeSerializers.ToDictionary(typeSerializer => typeSerializer.Key,
                typeSerializer => new Lazy<CustomTypeSerializationContext>(() => new(typeSerializer.Value, testEpubFile)));
        }

        public override JsonNode? SerializePropertyValue(Type type, string propertyName, object serializingObject)
        {
            if (!customTypes.TryGetValue(type, out Lazy<CustomTypeSerializationContext>? customTypeSerializationContext))
            {
                throw new ArgumentException($"There is no custom type serializer for type {type.Name}.");
            }
            return customTypeSerializationContext.Value.SerializePropertyValue(propertyName, serializingObject);
        }

        public override object? DeserializePropertyValue(Type type, string propertyName, JsonElement serializedValue)
        {
            if (!customTypes.TryGetValue(type, out Lazy<CustomTypeSerializationContext>? customTypeSerializationContext))
            {
                throw new ArgumentException($"There is no custom type deserializer for type {type.Name}.");
            }
            return customTypeSerializationContext.Value.DeserializePropertyValue(propertyName, serializedValue);
        }
    }
}
