using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal class EpubLocalByteContentFileTypeHandler : CustomTypeHandler<EpubLocalByteContentFile>
    {
        public EpubLocalByteContentFileTypeHandler(TestCasesSerializationContext testCasesSerializationContext)
            : base(testCasesSerializationContext)
        {
            AddCustomPropertyHandler(nameof(EpubLocalByteContentFile.Content), "$content", SerializeContent, DeserializeContent);
            AddIgnoredProperty(nameof(EpubLocalByteContentFile.ContentFileType));
            AddIgnoredProperty(nameof(EpubLocalByteContentFile.ContentLocation));
        }

        public override bool PreserveReferences => true;

        private static JsonNode? SerializeContent(EpubLocalByteContentFile serializingObject)
        {
            string? filePath = serializingObject.FilePath;
            return filePath != null ? JsonValue.Create(TestCasesSerializationContext.GetFilePathInEpub(filePath)) : null;
        }

        private object? DeserializeContent(string? serializedValue)
        {
            return serializedValue != null ? TestCasesSerializationContext.ReadFileAsBytes(serializedValue) : null;
        }
    }
}
