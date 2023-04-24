using System.IO.Compression;

namespace VersOne.Epub.Test.Integration.CustomSerialization.Context
{
    internal class TestEpubFile : IDisposable
    {
        private const string FILE_PATH_IN_EPUB_PREFIX = "epub://";
        private const string HTTP_USER_AGENT = "EpubReader Integration Test Runner (https://github.com/vers-one/EpubReader)";

        private static readonly HttpClient httpClient = new();
        private readonly ZipArchive testEpubArchive;

        public TestEpubFile(string testEpubFilePath)
        {
            testEpubArchive = ZipFile.OpenRead(testEpubFilePath);
            TestCaseDirectoryPath = Path.GetDirectoryName(testEpubFilePath) ?? throw new ArgumentException($"Cannot get directory name for {testEpubFilePath}");
        }

        public string TestCaseDirectoryPath { get; }

        public void Dispose()
        {
            testEpubArchive.Dispose();
        }

        public static string GetFilePathInEpub(string filePath)
        {
            return Path.Combine(FILE_PATH_IN_EPUB_PREFIX, filePath);
        }

        public byte[] ReadFileAsBytes(string filePathInEpub)
        {
            string filePath = filePathInEpub[FILE_PATH_IN_EPUB_PREFIX.Length..];
            ZipArchiveEntry? fileInEpub = testEpubArchive.GetEntry(filePath);
            Assert.NotNull(fileInEpub);
            byte[] result = new byte[(int)fileInEpub.Length];
            using Stream fileStream = fileInEpub.Open();
            using MemoryStream memoryStream = new(result);
            fileStream.CopyTo(memoryStream);
            return result;
        }

        public string ReadFileAsText(string filePathInEpub)
        {
            string filePath = filePathInEpub[FILE_PATH_IN_EPUB_PREFIX.Length..];
            ZipArchiveEntry? fileInEpub = testEpubArchive.GetEntry(filePath);
            Assert.NotNull(fileInEpub);
            using Stream fileStream = fileInEpub.Open();
            using StreamReader streamReader = new(fileStream);
            return streamReader.ReadToEnd();
        }

        public static byte[] DownloadFileAsBytes(string url)
        {
            return RequestHttpContent(url).ReadAsByteArrayAsync().Result;
        }

        public static string DownloadFileAsText(string url)
        {
            return RequestHttpContent(url).ReadAsStringAsync().Result;
        }

        private static HttpContent RequestHttpContent(string url)
        {
            using HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, url);
            httpRequestMessage.Headers.Add("User-Agent", HTTP_USER_AGENT);
            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead);
            httpResponseMessage.EnsureSuccessStatusCode();
            return httpResponseMessage.Content;
        }
    }
}
