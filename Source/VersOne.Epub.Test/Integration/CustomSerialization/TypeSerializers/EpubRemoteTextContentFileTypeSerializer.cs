using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomSerialization.Context;

namespace VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers
{
    internal class EpubRemoteTextContentFileTypeSerializer : CustomTypeSerializer<EpubRemoteTextContentFile>
    {
        public EpubRemoteTextContentFileTypeSerializer()
        {
            AddCustomPropertySerializer(nameof(EpubRemoteTextContentFile.Content), "$content", SerializeContent, DeserializeContent);
        }

        private static JsonNode? SerializeContent(EpubRemoteTextContentFile serializingObject, TestEpubFile _)
        {
            return serializingObject.Content != null ? JsonValue.Create(serializingObject.Url) : null;
        }

        private static object? DeserializeContent(string? serializedValue, TestEpubFile _)
        {
            return serializedValue != null ? TestEpubFile.DownloadFileAsText(serializedValue) : null;
        }
    }
}
