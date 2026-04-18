using System.Text.Json;
using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomSerialization.Context;

namespace VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers
{
    internal abstract class CustomPropertySerializer(string typePropertyName, string jsonPropertyName)
    {
        public string TypePropertyName { get; } = typePropertyName ?? throw new ArgumentNullException(nameof(typePropertyName));
        public string JsonPropertyName { get; } = jsonPropertyName ?? throw new ArgumentNullException(nameof(jsonPropertyName));

        public abstract JsonNode? SerializePropertyValue(object serializingObject, TestEpubFile testEpubFile);
        public abstract object? DeserializePropertyValue(JsonElement serializedValue, TestEpubFile testEpubFile);
    }

    internal class CustomPropertySerializer<T>(string typePropertyName, string jsonPropertyName,
        Func<T, TestEpubFile, JsonNode?> propertySerializer, Func<string?, TestEpubFile, object?> propertyDeserializer) :
        CustomPropertySerializer(typePropertyName, jsonPropertyName) where T : class
    {
        private readonly Func<T, TestEpubFile, JsonNode?> propertySerializer = propertySerializer;
        private readonly Func<string?, TestEpubFile, object?> propertyDeserializer = propertyDeserializer;

        public override JsonNode? SerializePropertyValue(object serializingObject, TestEpubFile testEpubFile)
        {
            T? typedSerializingObject = serializingObject as T;
            Assert.NotNull(typedSerializingObject);
            return propertySerializer(typedSerializingObject, testEpubFile);
        }

        public override object? DeserializePropertyValue(JsonElement serializedValue, TestEpubFile testEpubFile)
        {
            Assert.Equal(JsonValueKind.String, serializedValue.ValueKind);
            string? stringValue = serializedValue.GetString();
            return propertyDeserializer(stringValue, testEpubFile);
        }
    }
}
