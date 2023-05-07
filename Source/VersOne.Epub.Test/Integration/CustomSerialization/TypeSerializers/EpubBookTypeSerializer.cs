using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomSerialization.Context;

namespace VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers
{
    internal class EpubBookTypeSerializer : CustomTypeSerializer<EpubBook>
    {
        public EpubBookTypeSerializer()
        {
            AddCustomPropertySerializer(nameof(EpubBook.FilePath), "$file", SerializeFilePath, DeserializeFilePath);
            AddCustomPropertySerializer(nameof(EpubBook.CoverImage), "$cover", SerializeCoverImage, DeserializeCoverImage);
        }

        public static JsonNode? SerializeFilePath(EpubBook serializingObject, TestEpubFile _)
        {
            string? epubBookFileName = Path.GetFileName(serializingObject.FilePath);
            Assert.NotNull(epubBookFileName);
            return JsonValue.Create(epubBookFileName);
        }

        public object? DeserializeFilePath(string? serializedValue, TestEpubFile testEpubFile)
        {
            Assert.NotNull(serializedValue);
            return Path.Combine(testEpubFile.TestCaseDirectoryPath, serializedValue);
        }

        public static JsonNode? SerializeCoverImage(EpubBook serializingObject, TestEpubFile _)
        {
            string? filePath = serializingObject.Content.Cover?.FilePath;
            return filePath != null ? JsonValue.Create(TestEpubFile.GetFilePathInEpub(filePath)) : null;
        }

        public object? DeserializeCoverImage(string? serializedValue, TestEpubFile testEpubFile)
        {
            return serializedValue != null ? testEpubFile.ReadFileAsBytes(serializedValue) : null;
        }
    }
}
