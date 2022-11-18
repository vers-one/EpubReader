namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal class EpubBookTypeHandler : CustomTypeHandler<EpubBook>
    {
        public EpubBookTypeHandler(CustomPropertyDependencies customPropertyDependencies)
            : base(customPropertyDependencies)
        {
            AddCustomPropertyHandler(nameof(EpubBook.FilePath), "$file", SerializeFilePath, DeserializeFilePath);
            AddCustomPropertyHandler(nameof(EpubBook.CoverImage), "$cover", SerializeCoverImage, DeserializeCoverImage);
        }

        public string? SerializeFilePath(EpubBook serializingObject)
        {
            string? epubBookFileName = Path.GetFileName(serializingObject.FilePath);
            Assert.NotNull(epubBookFileName);
            return epubBookFileName;
        }

        public object? DeserializeFilePath(string? serializedValue)
        {
            Assert.NotNull(serializedValue);
            return Path.Combine(CustomPropertyDependencies.TestCaseDirectoryPath, serializedValue);
        }

        public string? SerializeCoverImage(EpubBook serializingObject)
        {
            string? filePath = serializingObject.Content.Cover?.FilePath;
            return filePath != null ? CustomPropertyDependencies.GetFilePathInEpub(filePath) : null;
        }

        public object? DeserializeCoverImage(string? serializedValue)
        {
            return serializedValue != null ? CustomPropertyDependencies.ReadFileAsBytes(serializedValue) : null;
        }
    }
}
