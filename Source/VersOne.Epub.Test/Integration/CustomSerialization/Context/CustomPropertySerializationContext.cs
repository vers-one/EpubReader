using System.Text.Json.Nodes;
using System.Text.Json;
using VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers;

namespace VersOne.Epub.Test.Integration.CustomSerialization.Context
{
    internal class CustomPropertySerializationContext(CustomPropertySerializer customPropertySerializer, TestEpubFile testEpubFile)
    {
        private readonly CustomPropertySerializer customPropertySerializer = customPropertySerializer;
        private readonly TestEpubFile testEpubFile = testEpubFile;

        public JsonNode? SerializePropertyValue(object serializingObject)
        {
            return customPropertySerializer.SerializePropertyValue(serializingObject, testEpubFile);
        }

        public object? DeserializePropertyValue(JsonElement serializedValue)
        {
            return customPropertySerializer.DeserializePropertyValue(serializedValue, testEpubFile);
        }
    }
}
