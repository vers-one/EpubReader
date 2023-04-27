using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomSerialization.Context;

namespace VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers
{
    internal class EpubRemoteByteContentFileTypeSerializer : CustomTypeSerializer<EpubRemoteByteContentFile>
    {
        public EpubRemoteByteContentFileTypeSerializer()
        {
            AddCustomPropertySerializer(nameof(EpubRemoteByteContentFile.Content), "$content", SerializeContent, DeserializeContent);
        }

        private static JsonNode? SerializeContent(EpubRemoteByteContentFile serializingObject, TestEpubFile _)
        {
            return serializingObject.Content != null ? JsonValue.Create(serializingObject.Url) : null;
        }

        private static object? DeserializeContent(string? serializedValue, TestEpubFile _)
        {
            return serializedValue != null ? TestEpubFile.DownloadFileAsBytes(serializedValue) : null;
        }
    }
}
