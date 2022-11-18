namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal class EpubLocalByteContentFileTypeHandler : CustomTypeHandler<EpubLocalByteContentFile>
    {
        public EpubLocalByteContentFileTypeHandler(CustomPropertyDependencies customPropertyDependencies)
            : base(customPropertyDependencies)
        {
            AddCustomPropertyHandler(nameof(EpubLocalByteContentFile.Content), "$content", SerializeContent, DeserializeContent);
        }

        public string? SerializeContent(EpubLocalByteContentFile serializingObject)
        {
            string? filePath = serializingObject.FilePath;
            return filePath != null ? CustomPropertyDependencies.GetFilePathInEpub(filePath) : null;
        }

        public object DeserializeContent(string serializedValue)
        {
            return CustomPropertyDependencies.ReadFileAsBytes(serializedValue);
        }
    }
}
