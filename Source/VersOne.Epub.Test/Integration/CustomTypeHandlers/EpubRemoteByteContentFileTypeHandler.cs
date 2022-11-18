namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal class EpubRemoteByteContentFileTypeHandler : CustomTypeHandler<EpubRemoteByteContentFile>
    {
        public EpubRemoteByteContentFileTypeHandler(CustomPropertyDependencies customPropertyDependencies)
            : base(customPropertyDependencies)
        {
            AddCustomPropertyHandler(nameof(EpubRemoteByteContentFile.Content), "$content", SerializeContent, DeserializeContent);
        }

        private string? SerializeContent(EpubRemoteByteContentFile serializingObject)
        {
            return serializingObject.Url;
        }

        private object? DeserializeContent(string? serializedValue)
        {
            return serializedValue != null ? CustomPropertyDependencies.DownloadFileAsBytes(serializedValue) : null;
        }
    }
}
