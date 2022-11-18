namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal class EpubLocalTextContentFileTypeHandler : CustomTypeHandler<EpubLocalTextContentFile>
    {
        public EpubLocalTextContentFileTypeHandler(CustomPropertyDependencies customPropertyDependencies)
            : base(customPropertyDependencies)
        {
            AddCustomPropertyHandler(nameof(EpubLocalTextContentFile.Content), "$content", SerializeContent, DeserializeContent);
        }

        private string? SerializeContent(EpubLocalTextContentFile serializingObject)
        {
            string? filePath = serializingObject.FilePath;
            return filePath != null ? CustomPropertyDependencies.GetFilePathInEpub(filePath) : null;
        }

        private object? DeserializeContent(string? serializedValue)
        {
            return serializedValue != null ? CustomPropertyDependencies.ReadFileAsText(serializedValue) : null;
        }
    }
}
