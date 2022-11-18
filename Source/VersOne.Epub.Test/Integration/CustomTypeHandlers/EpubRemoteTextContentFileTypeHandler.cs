namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal class EpubRemoteTextContentFileTypeHandler : CustomTypeHandler<EpubRemoteTextContentFile>
    {
        public EpubRemoteTextContentFileTypeHandler(CustomPropertyDependencies customPropertyDependencies)
            : base(customPropertyDependencies)
        {
            AddCustomPropertyHandler(nameof(EpubRemoteTextContentFile.Content), "$content", SerializeContent, DeserializeContent);
        }

        private string? SerializeContent(EpubRemoteTextContentFile serializingObject)
        {
            return serializingObject.Url;
        }

        private object? DeserializeContent(string? serializedValue)
        {
            return serializedValue != null ? CustomPropertyDependencies.DownloadFileAsText(serializedValue) : null;
        }
    }
}
