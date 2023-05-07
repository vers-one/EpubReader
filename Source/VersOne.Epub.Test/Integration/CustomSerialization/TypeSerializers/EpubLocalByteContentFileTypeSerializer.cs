using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomSerialization.Context;

namespace VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers
{
    internal class EpubLocalByteContentFileTypeSerializer : CustomTypeSerializer<EpubLocalByteContentFile>
    {
        public EpubLocalByteContentFileTypeSerializer()
        {
            AddCustomPropertySerializer(nameof(EpubLocalByteContentFile.Content), "$content", SerializeContent, DeserializeContent);
        }

        private static JsonNode? SerializeContent(EpubLocalByteContentFile serializingObject, TestEpubFile _)
        {
            string? filePath = serializingObject.FilePath;
            return filePath != null ? JsonValue.Create(TestEpubFile.GetFilePathInEpub(filePath)) : null;
        }

        private object? DeserializeContent(string? serializedValue, TestEpubFile testEpubFile)
        {
            return serializedValue != null ? testEpubFile.ReadFileAsBytes(serializedValue) : null;
        }
    }
}
