using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal class EpubRemoteByteContentFileTypeHandler : CustomTypeHandler<EpubRemoteByteContentFile>
    {
        public EpubRemoteByteContentFileTypeHandler(TestCasesSerializationContext testCasesSerializationContext)
            : base(testCasesSerializationContext)
        {
            AddCustomPropertyHandler(nameof(EpubRemoteByteContentFile.Content), "$content", SerializeContent, DeserializeContent);
            AddIgnoredProperty(nameof(EpubLocalByteContentFile.ContentFileType));
            AddIgnoredProperty(nameof(EpubLocalByteContentFile.ContentLocation));
        }

        public override bool PreserveReferences => true;

        private static JsonNode? SerializeContent(EpubRemoteByteContentFile serializingObject)
        {
            return serializingObject.Content != null ? JsonValue.Create(serializingObject.Url) : null;
        }

        private static object? DeserializeContent(string? serializedValue)
        {
            return serializedValue != null ? TestCasesSerializationContext.DownloadFileAsBytes(serializedValue) : null;
        }
    }
}
