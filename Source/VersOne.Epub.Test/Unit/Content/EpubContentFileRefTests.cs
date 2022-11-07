using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Content
{
    public class EpubContentFileRefTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string LOCAL_TEXT_FILE_NAME = "test.html";
        private const string LOCAL_TEXT_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{LOCAL_TEXT_FILE_NAME}";
        private const string REMOTE_TEXT_FILE_HREF = "https://example.com/books/123/test.html";
        private const string TEXT_FILE_CONTENT = "<html><head><title>Test HTML</title></head><body><h1>Test content</h1></body></html>";
        private const EpubContentType TEXT_FILE_CONTENT_TYPE = EpubContentType.XHTML_1_1;
        private const string TEXT_FILE_CONTENT_MIME_TYPE = "application/xhtml+xml";
        private const string LOCAL_BYTE_FILE_NAME = "image.jpg";
        private const string LOCAL_BYTE_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{LOCAL_BYTE_FILE_NAME}";
        private const string REMOTE_BYTE_FILE_HREF = "https://example.com/books/123/image.jpg";
        private const EpubContentType BYTE_FILE_CONTENT_TYPE = EpubContentType.IMAGE_JPEG;
        private const string BYTE_FILE_CONTENT_MIME_TYPE = "image/jpeg";

        private static readonly byte[] BYTE_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46 };

        private EpubContentFileRefMetadata LocalTextFileRefMetadata => new(LOCAL_TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE);

        private EpubContentFileRefMetadata LocalByteFileRefMetadata => new(LOCAL_BYTE_FILE_NAME, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE);

        private EpubContentFileRefMetadata RemoteTextFileRefMetadata => new(REMOTE_TEXT_FILE_HREF, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE);

        private EpubContentFileRefMetadata RemoteByteFileRefMetadata => new(REMOTE_BYTE_FILE_HREF, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE);

        [Fact(DisplayName = "EpubLocalTextContentFileRef constructor should set correct property values")]
        public void LocalTextContentFileRefConstructorTest()
        {
            EpubLocalTextContentFileRef epubLocalTextContentFileRef = new(LocalTextFileRefMetadata, LOCAL_TEXT_FILE_PATH, new TestEpubContentLoader());
            Assert.Equal(LOCAL_TEXT_FILE_NAME, epubLocalTextContentFileRef.Key);
            Assert.Equal(TEXT_FILE_CONTENT_TYPE, epubLocalTextContentFileRef.ContentType);
            Assert.Equal(TEXT_FILE_CONTENT_MIME_TYPE, epubLocalTextContentFileRef.ContentMimeType);
            Assert.Equal(EpubContentLocation.LOCAL, epubLocalTextContentFileRef.ContentLocation);
            Assert.Equal(EpubContentFileType.TEXT, epubLocalTextContentFileRef.ContentFileType);
            Assert.Equal(LOCAL_TEXT_FILE_PATH, epubLocalTextContentFileRef.FilePath);
        }

        [Fact(DisplayName = "EpubLocalByteContentFileRef constructor should set correct property values")]
        public void LocalByteContentFileRefConstructorTest()
        {
            EpubLocalByteContentFileRef epubLocalByteContentFileRef = new(LocalByteFileRefMetadata, LOCAL_BYTE_FILE_PATH, new TestEpubContentLoader());
            Assert.Equal(LOCAL_BYTE_FILE_NAME, epubLocalByteContentFileRef.Key);
            Assert.Equal(BYTE_FILE_CONTENT_TYPE, epubLocalByteContentFileRef.ContentType);
            Assert.Equal(BYTE_FILE_CONTENT_MIME_TYPE, epubLocalByteContentFileRef.ContentMimeType);
            Assert.Equal(EpubContentLocation.LOCAL, epubLocalByteContentFileRef.ContentLocation);
            Assert.Equal(EpubContentFileType.BYTE_ARRAY, epubLocalByteContentFileRef.ContentFileType);
            Assert.Equal(LOCAL_BYTE_FILE_PATH, epubLocalByteContentFileRef.FilePath);
        }

        [Fact(DisplayName = "EpubRemoteTextContentFileRef constructor should set correct property values")]
        public void RemoteTextContentFileRefConstructorTest()
        {
            EpubRemoteTextContentFileRef epubRemoteTextContentFileRef = new(RemoteTextFileRefMetadata, new TestEpubContentLoader());
            Assert.Equal(REMOTE_TEXT_FILE_HREF, epubRemoteTextContentFileRef.Key);
            Assert.Equal(TEXT_FILE_CONTENT_TYPE, epubRemoteTextContentFileRef.ContentType);
            Assert.Equal(TEXT_FILE_CONTENT_MIME_TYPE, epubRemoteTextContentFileRef.ContentMimeType);
            Assert.Equal(EpubContentLocation.REMOTE, epubRemoteTextContentFileRef.ContentLocation);
            Assert.Equal(EpubContentFileType.TEXT, epubRemoteTextContentFileRef.ContentFileType);
            Assert.Equal(REMOTE_TEXT_FILE_HREF, epubRemoteTextContentFileRef.Url);
        }

        [Fact(DisplayName = "EpubRemoteByteContentFileRef constructor should set correct property values")]
        public void RemoteByteContentFileRefConstructorTest()
        {
            EpubRemoteByteContentFileRef epubRemoteByteContentFileRef = new(RemoteByteFileRefMetadata, new TestEpubContentLoader());
            Assert.Equal(REMOTE_BYTE_FILE_HREF, epubRemoteByteContentFileRef.Key);
            Assert.Equal(BYTE_FILE_CONTENT_TYPE, epubRemoteByteContentFileRef.ContentType);
            Assert.Equal(BYTE_FILE_CONTENT_MIME_TYPE, epubRemoteByteContentFileRef.ContentMimeType);
            Assert.Equal(EpubContentLocation.REMOTE, epubRemoteByteContentFileRef.ContentLocation);
            Assert.Equal(EpubContentFileType.BYTE_ARRAY, epubRemoteByteContentFileRef.ContentFileType);
            Assert.Equal(REMOTE_BYTE_FILE_HREF, epubRemoteByteContentFileRef.Url);
        }

        [Fact(DisplayName = "EpubLocalTextContentFileRef constructor should throw ArgumentNullException if the supplied metadata parameter is null")]
        public void CreateEpubLocalTextContentFileRefWithNullMetadataTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubLocalTextContentFileRef(null, LOCAL_TEXT_FILE_PATH, new TestEpubContentLoader()));
        }

        [Fact(DisplayName = "EpubLocalByteContentFileRef constructor should throw ArgumentNullException if the supplied metadata parameter is null")]
        public void CreateEpubLocalByteContentFileRefWithNullMetadataTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubLocalByteContentFileRef(null, LOCAL_BYTE_FILE_PATH, new TestEpubContentLoader()));
        }

        [Fact(DisplayName = "EpubRemoteTextContentFileRef constructor should throw ArgumentNullException if the supplied metadata parameter is null")]
        public void CreateEpubRemoteTextContentFileRefWithNullMetadataTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubRemoteTextContentFileRef(null, new TestEpubContentLoader()));
        }

        [Fact(DisplayName = "EpubRemoteByteContentFileRef constructor should throw ArgumentNullException if the supplied metadata parameter is null")]
        public void CreateEpubRemoteByteContentFileRefWithNullMetadataTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubRemoteByteContentFileRef(null, new TestEpubContentLoader()));
        }

        [Fact(DisplayName = "EpubLocalTextContentFileRef constructor should throw ArgumentNullException if the supplied epubContentLoader parameter is null")]
        public void CreateEpubLocalTextContentFileRefWithNullContentLoaderTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubLocalTextContentFileRef(LocalTextFileRefMetadata, LOCAL_TEXT_FILE_PATH, null));
        }

        [Fact(DisplayName = "EpubLocalByteContentFileRef constructor should throw ArgumentNullException if the supplied epubContentLoader parameter is null")]
        public void CreateEpubLocalByteContentFileRefWithNullContentLoaderTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubLocalByteContentFileRef(LocalByteFileRefMetadata, LOCAL_BYTE_FILE_PATH, null));
        }

        [Fact(DisplayName = "EpubRemoteTextContentFileRef constructor should throw ArgumentNullException if the supplied epubContentLoader parameter is null")]
        public void CreateEpubRemoteTextContentFileRefWithNullContentLoaderTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubRemoteTextContentFileRef(RemoteTextFileRefMetadata, null));
        }

        [Fact(DisplayName = "EpubRemoteByteContentFileRef constructor should throw ArgumentNullException if the supplied epubContentLoader parameter is null")]
        public void CreateEpubRemoteByteContentFileRefWithNullContentLoaderTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubRemoteByteContentFileRef(RemoteByteFileRefMetadata, null));
        }

        [Fact(DisplayName = "EpubLocalTextContentFileRef constructor should throw ArgumentNullException if the supplied file path is null")]
        public void CreateEpubLocalTextContentFileRefWithNullFilePathTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubLocalTextContentFileRef(LocalTextFileRefMetadata, null, new TestEpubContentLoader()));
        }

        [Fact(DisplayName = "EpubLocalByteContentFileRef constructor should throw ArgumentNullException if the supplied file path is null")]
        public void CreateEpubLocalByteContentFileRefWithNullFilePathTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubLocalByteContentFileRef(LocalByteFileRefMetadata, null, new TestEpubContentLoader()));
        }

        [Fact(DisplayName = "Reading local text content file synchronously should succeed")]
        public void LocalTextContentFileRefReadContentTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(TEXT_FILE_CONTENT);
            EpubLocalTextContentFileRef epubLocalTextContentFileRef = new(LocalTextFileRefMetadata, LOCAL_TEXT_FILE_PATH, testEpubContentLoader);
            string content = epubLocalTextContentFileRef.ReadContent();
            Assert.Equal(TEXT_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Reading local text content file asynchronously should succeed")]
        public async void LocalTextContentFileRefReadContentAsyncTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(TEXT_FILE_CONTENT);
            EpubLocalTextContentFileRef epubLocalTextContentFileRef = new(LocalTextFileRefMetadata, LOCAL_TEXT_FILE_PATH, testEpubContentLoader);
            string content = await epubLocalTextContentFileRef.ReadContentAsync();
            Assert.Equal(TEXT_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Reading local byte content file synchronously should succeed")]
        public void LocalByteContentFileRefReadContentTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(BYTE_FILE_CONTENT);
            EpubLocalByteContentFileRef epubLocalByteContentFileRef = new(LocalByteFileRefMetadata, LOCAL_BYTE_FILE_PATH, testEpubContentLoader);
            byte[] content = epubLocalByteContentFileRef.ReadContent();
            Assert.Equal(BYTE_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Reading local byte content file asynchronously should succeed")]
        public async void LocalByteContentFileRefReadContentAsyncTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(BYTE_FILE_CONTENT);
            EpubLocalByteContentFileRef epubLocalByteContentFileRef = new(LocalByteFileRefMetadata, LOCAL_BYTE_FILE_PATH, testEpubContentLoader);
            byte[] content = await epubLocalByteContentFileRef.ReadContentAsync();
            Assert.Equal(BYTE_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Reading local content file as text synchronously should succeed")]
        public void LocalContentFileRefReadContentAsTextTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(TEXT_FILE_CONTENT);
            EpubLocalTextContentFileRef epubLocalTextContentFileRef = new(LocalTextFileRefMetadata, LOCAL_TEXT_FILE_PATH, testEpubContentLoader);
            string content = epubLocalTextContentFileRef.ReadContentAsText();
            Assert.Equal(TEXT_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Reading local content file as text asynchronously should succeed")]
        public async void LocalContentFileRefReadContentAsTextAsyncTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(TEXT_FILE_CONTENT);
            EpubLocalTextContentFileRef epubLocalTextContentFileRef = new(LocalTextFileRefMetadata, LOCAL_TEXT_FILE_PATH, testEpubContentLoader);
            string content = await epubLocalTextContentFileRef.ReadContentAsTextAsync();
            Assert.Equal(TEXT_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Reading local content file as bytes synchronously should succeed")]
        public void LocalContentFileRefReadContentAsBytesTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(BYTE_FILE_CONTENT);
            EpubLocalByteContentFileRef epubLocalByteContentFileRef = new(LocalByteFileRefMetadata, LOCAL_BYTE_FILE_PATH, testEpubContentLoader);
            byte[] content = epubLocalByteContentFileRef.ReadContentAsBytes();
            Assert.Equal(BYTE_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Reading local content file as bytes asynchronously should succeed")]
        public async void LocalContentFileRefReadContentAsBytesAsyncTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(BYTE_FILE_CONTENT);
            EpubLocalByteContentFileRef epubLocalByteContentFileRef = new(LocalByteFileRefMetadata, LOCAL_BYTE_FILE_PATH, testEpubContentLoader);
            byte[] content = await epubLocalByteContentFileRef.ReadContentAsBytesAsync();
            Assert.Equal(BYTE_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Getting local content file stream synchronously should succeed")]
        public void LocalContentFileRefGetContentStreamTest()
        {
            using MemoryStream testStream = new();
            TestEpubContentLoader testEpubContentLoader = new(testStream);
            EpubLocalTextContentFileRef epubLocalTextContentFileRef = new(LocalTextFileRefMetadata, LOCAL_TEXT_FILE_PATH, testEpubContentLoader);
            Stream contentStream = epubLocalTextContentFileRef.GetContentStream();
            Assert.Equal(testStream, contentStream);
        }

        [Fact(DisplayName = "Getting local content file stream asynchronously should succeed")]
        public async void LocalContentFileRefGetContentStreamAsyncTest()
        {
            using MemoryStream testStream = new();
            TestEpubContentLoader testEpubContentLoader = new(testStream);
            EpubLocalTextContentFileRef epubLocalTextContentFileRef = new(LocalTextFileRefMetadata, LOCAL_TEXT_FILE_PATH, testEpubContentLoader);
            Stream contentStream = await epubLocalTextContentFileRef.GetContentStreamAsync();
            Assert.Equal(testStream, contentStream);
        }

        [Fact(DisplayName = "Downloading remote text content file synchronously should succeed")]
        public void RemoteTextContentFileRefDownloadContentTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(TEXT_FILE_CONTENT);
            EpubRemoteTextContentFileRef epubRemoteTextContentFileRef = new(RemoteTextFileRefMetadata, testEpubContentLoader);
            string content = epubRemoteTextContentFileRef.DownloadContent();
            Assert.Equal(TEXT_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Downloading remote text content file asynchronously should succeed")]
        public async void RemoteTextContentFileRefDownloadContentAsyncTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(TEXT_FILE_CONTENT);
            EpubRemoteTextContentFileRef epubRemoteTextContentFileRef = new(RemoteTextFileRefMetadata, testEpubContentLoader);
            string content = await epubRemoteTextContentFileRef.DownloadContentAsync();
            Assert.Equal(TEXT_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Downloading remote byte content file synchronously should succeed")]
        public void RemoteByteContentFileRefDownloadContentTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(BYTE_FILE_CONTENT);
            EpubRemoteByteContentFileRef epubRemoteByteContentFileRef = new(RemoteByteFileRefMetadata, testEpubContentLoader);
            byte[] content = epubRemoteByteContentFileRef.DownloadContent();
            Assert.Equal(BYTE_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Downloading remote byte content file asynchronously should succeed")]
        public async void RemoteByteContentFileRefDownloadContentAsyncTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(BYTE_FILE_CONTENT);
            EpubRemoteByteContentFileRef epubRemoteByteContentFileRef = new(RemoteByteFileRefMetadata, testEpubContentLoader);
            byte[] content = await epubRemoteByteContentFileRef.DownloadContentAsync();
            Assert.Equal(BYTE_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Downloading remote content file as text synchronously should succeed")]
        public void RemoteContentFileRefDownloadContentAsTextTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(TEXT_FILE_CONTENT);
            EpubRemoteTextContentFileRef epubRemoteTextContentFileRef = new(RemoteTextFileRefMetadata, testEpubContentLoader);
            string content = epubRemoteTextContentFileRef.DownloadContentAsText();
            Assert.Equal(TEXT_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Downloading remote content file as text asynchronously should succeed")]
        public async void RemoteContentFileRefDownloadContentAsTextAsyncTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(TEXT_FILE_CONTENT);
            EpubRemoteTextContentFileRef epubRemoteTextContentFileRef = new(RemoteTextFileRefMetadata, testEpubContentLoader);
            string content = await epubRemoteTextContentFileRef.DownloadContentAsTextAsync();
            Assert.Equal(TEXT_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Downloading remote content file as bytes synchronously should succeed")]
        public void RemoteContentFileRefDownloadContentAsBytesTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(BYTE_FILE_CONTENT);
            EpubRemoteByteContentFileRef epubRemoteByteContentFileRef = new(RemoteByteFileRefMetadata, testEpubContentLoader);
            byte[] content = epubRemoteByteContentFileRef.DownloadContentAsBytes();
            Assert.Equal(BYTE_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Downloading remote content file as bytes asynchronously should succeed")]
        public async void RemoteContentFileRefDownloadContentAsBytesAsyncTest()
        {
            TestEpubContentLoader testEpubContentLoader = new(BYTE_FILE_CONTENT);
            EpubRemoteByteContentFileRef epubRemoteByteContentFileRef = new(RemoteByteFileRefMetadata, testEpubContentLoader);
            byte[] content = await epubRemoteByteContentFileRef.DownloadContentAsBytesAsync();
            Assert.Equal(BYTE_FILE_CONTENT, content);
        }

        [Fact(DisplayName = "Getting remote content file stream synchronously should succeed")]
        public void RemoteContentFileRefGetDownloadingContentStreamTest()
        {
            MemoryStream testStream = new();
            TestEpubContentLoader testEpubContentLoader = new(testStream);
            EpubRemoteTextContentFileRef epubRemoteTextContentFileRef = new(RemoteTextFileRefMetadata, testEpubContentLoader);
            Stream contentStream = epubRemoteTextContentFileRef.GetDownloadingContentStream();
            Assert.Equal(testStream, contentStream);
        }

        [Fact(DisplayName = "Getting remote content file stream asynchronously should succeed")]
        public async void RemoteContentFileRefGetDownloadingContentStreamAsyncTest()
        {
            MemoryStream testStream = new();
            TestEpubContentLoader testEpubContentLoader = new(testStream);
            EpubRemoteTextContentFileRef epubRemoteTextContentFileRef = new(RemoteTextFileRefMetadata, testEpubContentLoader);
            Stream contentStream = await epubRemoteTextContentFileRef.GetDownloadingContentStreamAsync();
            Assert.Equal(testStream, contentStream);
        }
    }
}
