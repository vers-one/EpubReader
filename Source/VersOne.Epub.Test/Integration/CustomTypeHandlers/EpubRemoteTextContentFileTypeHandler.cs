using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal class EpubRemoteTextContentFileTypeHandler : CustomTypeHandler<EpubRemoteTextContentFile>
    {
        public EpubRemoteTextContentFileTypeHandler(TestCasesSerializationContext testCasesSerializationContext)
            : base(testCasesSerializationContext)
        {
            AddCustomPropertyHandler(nameof(EpubRemoteTextContentFile.Content), "$content", SerializeContent, DeserializeContent);
            AddIgnoredProperty(nameof(EpubLocalByteContentFile.ContentFileType));
            AddIgnoredProperty(nameof(EpubLocalByteContentFile.ContentLocation));
        }

        public override bool PreserveReferences => true;

        private static JsonNode? SerializeContent(EpubRemoteTextContentFile serializingObject)
        {
            return serializingObject.Content != null ? JsonValue.Create(serializingObject.Url) : null;
        }

        private static object? DeserializeContent(string? serializedValue)
        {
            return serializedValue != null ? TestCasesSerializationContext.DownloadFileAsText(serializedValue) : null;
        }
    }
}
