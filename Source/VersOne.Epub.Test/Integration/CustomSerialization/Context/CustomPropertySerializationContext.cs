using System.Text.Json.Nodes;
using System.Text.Json;
using VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers;

namespace VersOne.Epub.Test.Integration.CustomSerialization.Context
{
    internal class CustomPropertySerializationContext
    {
        private readonly CustomPropertySerializer customPropertySerializer;
        private readonly TestEpubFile testEpubFile;

        public CustomPropertySerializationContext(CustomPropertySerializer customPropertySerializer, TestEpubFile testEpubFile)
        {
            this.customPropertySerializer = customPropertySerializer;
            this.testEpubFile = testEpubFile;
        }

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
