using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomSerialization.Context;

namespace VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers
{
    internal class EpubLocalTextContentFileTypeSerializer : CustomTypeSerializer<EpubLocalTextContentFile>
    {
        public EpubLocalTextContentFileTypeSerializer()
        {
            AddCustomPropertySerializer(nameof(EpubLocalTextContentFile.Content), "$content", SerializeContent, DeserializeContent);
        }

        private static JsonNode? SerializeContent(EpubLocalTextContentFile serializingObject, TestEpubFile _)
        {
            string? filePath = serializingObject.FilePath;
            return filePath != null ? JsonValue.Create(TestEpubFile.GetFilePathInEpub(filePath)) : null;
        }

        private object? DeserializeContent(string? serializedValue, TestEpubFile testEpubFile)
        {
            return serializedValue != null ? testEpubFile.ReadFileAsText(serializedValue) : null;
        }
    }
}
