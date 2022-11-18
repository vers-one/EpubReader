using System.Reflection;
using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Content
{
    public class EpubRemoteContentLoaderTests
    {
        private const string TEXT_FILE_HREF = "https://example.com/books/123/test.html";
        private const string TEXT_FILE_CONTENT = "<html><head><title>Test HTML</title></head><body><h1>Test content</h1></body></html>";
        private const EpubContentType TEXT_FILE_CONTENT_TYPE = EpubContentType.XHTML_1_1;
        private const string TEXT_FILE_CONTENT_MIME_TYPE = "application/xhtml+xml";
        private const string BYTE_FILE_HREF = "https://example.com/books/123/image.jpg";
        private const EpubContentType BYTE_FILE_CONTENT_TYPE = EpubContentType.IMAGE_JPEG;
        private const string BYTE_FILE_CONTENT_MIME_TYPE = "image/jpeg";

        private static readonly byte[] BYTE_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46 };

        private static EpubContentFileRefMetadata TextFileRefMetadata => new(TEXT_FILE_HREF, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE);

        private static EpubContentFileRefMetadata ByteFileRefMetadata => new(BYTE_FILE_HREF, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE);

        [Fact(DisplayName = "Constructing a remote content loader with non-null constructor parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            _ = new EpubRemoteContentLoader(new TestEnvironmentDependencies(), new ContentDownloaderOptions());
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if environmentDependencies parameter is null")]
        public void ConstructorWithNullEnvironmentDependenciesTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubRemoteContentLoader(null!, new ContentDownloaderOptions()));
        }

        [Fact(DisplayName = "Constructing a remote content loader with null contentReaderOptions parameter should succeed")]
        public void ConstructorWithNullContentReaderOptionsTest()
        {
            _ = new EpubRemoteContentLoader(new TestEnvironmentDependencies(), null);
        }

        [Fact(DisplayName = "Loading text file content synchronously should succeed")]
        public void LoadContentAsTextTest()
        {
            TestContentDownloader testContentDownloader = new(TEXT_FILE_HREF, TEXT_FILE_CONTENT);
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(testContentDownloader);
            string textContent = epubRemoteContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Loading text file content asynchronously should succeed")]
        public async void LoadContentAsTextAsyncTest()
        {
            TestContentDownloader testContentDownloader = new(TEXT_FILE_HREF, TEXT_FILE_CONTENT);
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(testContentDownloader);
            string textContent = await epubRemoteContentLoader.LoadContentAsTextAsync(TextFileRefMetadata);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Loading byte file content synchronously should succeed")]
        public void LoadContentAsBytesTest()
        {
            TestContentDownloader testContentDownloader = new(BYTE_FILE_HREF, BYTE_FILE_CONTENT);
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(testContentDownloader);
            byte[] byteContent = epubRemoteContentLoader.LoadContentAsBytes(ByteFileRefMetadata);
            Assert.Equal(BYTE_FILE_CONTENT, byteContent);
        }

        [Fact(DisplayName = "Loading byte file content asynchronously should succeed")]
        public async void LoadContentAsBytesAsyncTest()
        {
            TestContentDownloader testContentDownloader = new(BYTE_FILE_HREF, BYTE_FILE_CONTENT);
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(testContentDownloader);
            byte[] byteContent = await epubRemoteContentLoader.LoadContentAsBytesAsync(ByteFileRefMetadata);
            Assert.Equal(BYTE_FILE_CONTENT, byteContent);
        }

        [Fact(DisplayName = "Getting file content stream synchronously should succeed")]
        public void GetContentStreamTest()
        {
            using MemoryStream testContentStream = new();
            TestContentDownloader testContentDownloader = new(TEXT_FILE_HREF, testContentStream);
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(testContentDownloader);
            Stream textContentStream = epubRemoteContentLoader.GetContentStream(TextFileRefMetadata);
            Assert.Equal(testContentStream, textContentStream);
        }

        [Fact(DisplayName = "Getting file content stream asynchronously should succeed")]
        public async void GetContentStreamAsyncTest()
        {
            using MemoryStream testContentStream = new();
            TestContentDownloader testContentDownloader = new(TEXT_FILE_HREF, testContentStream);
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(testContentDownloader);
            Stream textContentStream = await epubRemoteContentLoader.GetContentStreamAsync(TextFileRefMetadata);
            Assert.Equal(testContentStream, textContentStream);
        }

        [Fact(DisplayName = "Loading content of multiple files should succeed")]
        public void LoadContentWithMultipleFilesTest()
        {
            TestContentDownloader testContentDownloader = new();
            testContentDownloader.AddTextRemoteFile(TEXT_FILE_HREF, TEXT_FILE_CONTENT);
            testContentDownloader.AddByteRemoteFile(BYTE_FILE_HREF, BYTE_FILE_CONTENT);
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(testContentDownloader);
            string textContent = epubRemoteContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
            byte[] byteContent = epubRemoteContentLoader.LoadContentAsBytes(ByteFileRefMetadata);
            Assert.Equal(BYTE_FILE_CONTENT, byteContent);
        }

        [Fact(DisplayName = "LoadContentAsText should throw EpubContentDownloaderException if ContentDownloaderOptions.DownloadContent is false")]
        public void LoadContentAsTextWithDownloadContentFalseTest()
        {
            ContentDownloaderOptions contentDownloaderOptions = new()
            {
                DownloadContent = false
            };
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(contentDownloaderOptions: contentDownloaderOptions);
            Assert.Throws<EpubContentDownloaderException>(() => epubRemoteContentLoader.LoadContentAsText(TextFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsTextAsync should throw EpubContentDownloaderException if ContentDownloaderOptions.DownloadContent is false")]
        public async void LoadContentAsTextAsyncWithDownloadContentFalseTest()
        {
            ContentDownloaderOptions contentDownloaderOptions = new()
            {
                DownloadContent = false
            };
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(contentDownloaderOptions: contentDownloaderOptions);
            await Assert.ThrowsAsync<EpubContentDownloaderException>(() => epubRemoteContentLoader.LoadContentAsTextAsync(TextFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsBytes should throw EpubContentDownloaderException if ContentDownloaderOptions.DownloadContent is false")]
        public void LoadContentAsBytesWithDownloadContentFalseTest()
        {
            ContentDownloaderOptions contentDownloaderOptions = new()
            {
                DownloadContent = false
            };
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(contentDownloaderOptions: contentDownloaderOptions);
            Assert.Throws<EpubContentDownloaderException>(() => epubRemoteContentLoader.LoadContentAsBytes(ByteFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsBytesAsync should throw EpubContentDownloaderException if ContentDownloaderOptions.DownloadContent is false")]
        public async void LoadContentAsBytesAsyncWithDownloadContentFalseTest()
        {
            ContentDownloaderOptions contentDownloaderOptions = new()
            {
                DownloadContent = false
            };
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(contentDownloaderOptions: contentDownloaderOptions);
            await Assert.ThrowsAsync<EpubContentDownloaderException>(() => epubRemoteContentLoader.LoadContentAsBytesAsync(ByteFileRefMetadata));
        }

        [Fact(DisplayName = "GetContentStream should throw EpubContentDownloaderException if ContentDownloaderOptions.DownloadContent is false")]
        public void GetContentStreamWithDownloadContentFalseTest()
        {
            ContentDownloaderOptions contentDownloaderOptions = new()
            {
                DownloadContent = false
            };
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(contentDownloaderOptions: contentDownloaderOptions);
            Assert.Throws<EpubContentDownloaderException>(() => epubRemoteContentLoader.GetContentStream(TextFileRefMetadata));
        }

        [Fact(DisplayName = "GetContentStreamAsync should throw EpubContentDownloaderException if ContentDownloaderOptions.DownloadContent is false")]
        public async void GetContentStreamAsyncWithDownloadContentFalseTest()
        {
            ContentDownloaderOptions contentDownloaderOptions = new()
            {
                DownloadContent = false
            };
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(contentDownloaderOptions: contentDownloaderOptions);
            await Assert.ThrowsAsync<EpubContentDownloaderException>(() => epubRemoteContentLoader.GetContentStreamAsync(TextFileRefMetadata));
        }

        [Fact(DisplayName = "Remote content loader should pass ContentDownloaderOptions.DownloaderUserAgent to the content downloader")]
        public void LoadContentWithSpecifiedUserAgentTest()
        {
            TestContentDownloader testContentDownloader = new(TEXT_FILE_HREF, TEXT_FILE_CONTENT);
            string testUserAgent = "Test UserAgent";
            ContentDownloaderOptions contentDownloaderOptions = new()
            {
                DownloadContent = true,
                DownloaderUserAgent = testUserAgent
            };
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(testContentDownloader, contentDownloaderOptions);
            epubRemoteContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(testUserAgent, testContentDownloader.LastUserAgent);
        }

        [Fact(DisplayName = "Remote content loader should use EpubReader/<version> user agent if ContentDownloaderOptions.DownloaderUserAgent is null")]
        public void LoadContentWithNonSpecifiedUserAgentTest()
        {
            TestContentDownloader testContentDownloader = new(TEXT_FILE_HREF, TEXT_FILE_CONTENT);
            AssemblyInformationalVersionAttribute? assemblyInformationalVersionAttribute =
                typeof(EpubRemoteContentLoaderTests).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            Assert.NotNull(assemblyInformationalVersionAttribute);
            string expectedUserAgent = "EpubReader/" + assemblyInformationalVersionAttribute.InformationalVersion;
            ContentDownloaderOptions contentDownloaderOptions = new()
            {
                DownloadContent = true,
                DownloaderUserAgent = null
            };
            EpubRemoteContentLoader epubRemoteContentLoader = CreateEpubRemoteContentLoader(testContentDownloader, contentDownloaderOptions);
            epubRemoteContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(expectedUserAgent, testContentDownloader.LastUserAgent);
        }

        [Fact(DisplayName = "Remote content loader should use custom content downloader if it is supplied through ContentDownloaderOptions.CustomContentDownloader")]
        public void LoadContentWithCustomContentDownloaderTest()
        {
            TestEnvironmentDependencies testEnvironmentDependencies = new(contentDownloader: null);
            TestContentDownloader testContentDownloader = new(TEXT_FILE_HREF, TEXT_FILE_CONTENT);
            ContentDownloaderOptions contentDownloaderOptions = new()
            {
                DownloadContent = true,
                CustomContentDownloader = testContentDownloader
            };
            EpubRemoteContentLoader epubRemoteContentLoader = new(testEnvironmentDependencies, contentDownloaderOptions);
            string textContent = epubRemoteContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        private static EpubRemoteContentLoader CreateEpubRemoteContentLoader(TestContentDownloader? testContentDownloader = null, ContentDownloaderOptions? contentDownloaderOptions = null)
        {
            TestEnvironmentDependencies testEnvironmentDependencies = new(contentDownloader: testContentDownloader ?? new TestContentDownloader());
            contentDownloaderOptions ??= new()
            {
                DownloadContent = true
            };
            return new(testEnvironmentDependencies, contentDownloaderOptions);
        }
    }
}
