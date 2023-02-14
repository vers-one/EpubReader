using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal class EpubBookTypeHandler : CustomTypeHandler<EpubBook>
    {
        public EpubBookTypeHandler(TestCasesSerializationContext testCasesSerializationContext)
            : base(testCasesSerializationContext)
        {
            AddCustomPropertyHandler(nameof(EpubBook.FilePath), "$file", SerializeFilePath, DeserializeFilePath);
            AddCustomPropertyHandler(nameof(EpubBook.CoverImage), "$cover", SerializeCoverImage, DeserializeCoverImage);
            AddOptionalProperty(nameof(EpubBook.AuthorList), PropertyDefaultValue.EMPTY_ARRAY);
            AddOptionalProperty(nameof(EpubBook.Description), PropertyDefaultValue.NULL);
            AddOptionalProperty(nameof(EpubBook.CoverImage), PropertyDefaultValue.NULL);
            AddOptionalProperty(nameof(EpubBook.ReadingOrder), PropertyDefaultValue.EMPTY_ARRAY);
            AddOptionalProperty(nameof(EpubBook.Navigation), PropertyDefaultValue.NULL);
        }

        public override bool PreserveReferences => false;

        public static JsonNode? SerializeFilePath(EpubBook serializingObject)
        {
            string? epubBookFileName = Path.GetFileName(serializingObject.FilePath);
            Assert.NotNull(epubBookFileName);
            return JsonValue.Create(epubBookFileName);
        }

        public object? DeserializeFilePath(string? serializedValue)
        {
            Assert.NotNull(serializedValue);
            return Path.Combine(TestCasesSerializationContext.TestCaseDirectoryPath, serializedValue);
        }

        public static JsonNode? SerializeCoverImage(EpubBook serializingObject)
        {
            string? filePath = serializingObject.Content.Cover?.FilePath;
            return filePath != null ? JsonValue.Create(TestCasesSerializationContext.GetFilePathInEpub(filePath)) : null;
        }

        public object? DeserializeCoverImage(string? serializedValue)
        {
            return serializedValue != null ? TestCasesSerializationContext.ReadFileAsBytes(serializedValue) : null;
        }
    }
}
