using System.IO.Compression;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class TestCaseExtensionDataHandler : IDisposable
    {
        private const string FILE_PATH_IN_EPUB_PREFIX = "epub://";

        private readonly ZipArchive testEpubArchive;
        private readonly string testCaseDirectoryPath;

        public TestCaseExtensionDataHandler(string testEpubFilePath)
        {
            testEpubArchive = ZipFile.OpenRead(testEpubFilePath);
            testCaseDirectoryPath = Path.GetDirectoryName(testEpubFilePath);
        }

        public void Dispose()
        {
            testEpubArchive.Dispose();
        }

        public IEnumerable<KeyValuePair<object, object>> GetEpubBookExtensionData(object serializingObject)
        {
            EpubBook epubBook = (EpubBook)serializingObject;
            yield return new KeyValuePair<object, object>("$file", Path.GetFileName(epubBook.FilePath));
            if (epubBook.CoverImage != null)
            {
                yield return new KeyValuePair<object, object>("$cover", FILE_PATH_IN_EPUB_PREFIX + epubBook.Content.Cover.FilePath);
            }
        }

        public void SetEpubBookExtensionData(object deserializingObject, string key, object value)
        {
            if (value == null)
            {
                return;
            }
            EpubBook epubBook = (EpubBook)deserializingObject;
            switch (key)
            {
                case "$cover":
                    epubBook.CoverImage = ReadFileAsBytes(value.ToString()[FILE_PATH_IN_EPUB_PREFIX.Length..]);
                    break;
                case "$file":
                    epubBook.FilePath = Path.Combine(testCaseDirectoryPath, value.ToString());
                    break;
            }
        }

        public IEnumerable<KeyValuePair<object, object>> GetEpubLocalContentFileExtensionData(object serializingObject)
        {
            EpubLocalContentFile epubLocalContentFile = (EpubLocalContentFile)serializingObject;
            if (!String.IsNullOrEmpty(epubLocalContentFile.FilePath))
            {
                yield return new KeyValuePair<object, object>("$content", FILE_PATH_IN_EPUB_PREFIX + epubLocalContentFile.FilePath);
            }
        }

        public void SetEpubLocalByteContentFileExtensionData(object deserializingObject, string key, object value)
        {
            if (key == "$content" && value != null && value.ToString().StartsWith(FILE_PATH_IN_EPUB_PREFIX))
            {
                (deserializingObject as EpubLocalByteContentFile).Content = ReadFileAsBytes(value.ToString()[FILE_PATH_IN_EPUB_PREFIX.Length..]);
            }
        }

        public void SetEpubLocalTextContentFileExtensionData(object deserializingObject, string key, object value)
        {
            if (key == "$content" && value != null && value.ToString().StartsWith(FILE_PATH_IN_EPUB_PREFIX))
            {
                (deserializingObject as EpubLocalTextContentFile).Content = ReadFileAsText(value.ToString()[FILE_PATH_IN_EPUB_PREFIX.Length..]);
            }
        }

        private byte[] ReadFileAsBytes(string filePathInEpub)
        {
            ZipArchiveEntry fileInEpub = testEpubArchive.GetEntry(filePathInEpub);
            byte[] result = new byte[(int)fileInEpub.Length];
            using Stream fileStream = fileInEpub.Open();
            using MemoryStream memoryStream = new(result);
            fileStream.CopyTo(memoryStream);
            return result;
        }

        private string ReadFileAsText(string filePathInEpub)
        {
            ZipArchiveEntry fileInEpub = testEpubArchive.GetEntry(filePathInEpub);
            using Stream fileStream = fileInEpub.Open();
            using StreamReader streamReader = new(fileStream);
            return streamReader.ReadToEnd();
        }
    }
}
