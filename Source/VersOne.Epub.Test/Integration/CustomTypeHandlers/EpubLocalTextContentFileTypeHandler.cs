using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal class EpubLocalTextContentFileTypeHandler : CustomTypeHandler<EpubLocalTextContentFile>
    {
        public EpubLocalTextContentFileTypeHandler(TestCasesSerializationContext testCasesSerializationContext)
            : base(testCasesSerializationContext)
        {
            AddCustomPropertyHandler(nameof(EpubLocalTextContentFile.Content), "$content", SerializeContent, DeserializeContent);
            AddIgnoredProperty(nameof(EpubLocalByteContentFile.ContentFileType));
            AddIgnoredProperty(nameof(EpubLocalByteContentFile.ContentLocation));
        }

        public override bool PreserveReferences => true;

        private static JsonNode? SerializeContent(EpubLocalTextContentFile serializingObject)
        {
            string? filePath = serializingObject.FilePath;
            return filePath != null ? JsonValue.Create(TestCasesSerializationContext.GetFilePathInEpub(filePath)) : null;
        }

        private object? DeserializeContent(string? serializedValue)
        {
            return serializedValue != null ? TestCasesSerializationContext.ReadFileAsText(serializedValue) : null;
        }
    }
}
