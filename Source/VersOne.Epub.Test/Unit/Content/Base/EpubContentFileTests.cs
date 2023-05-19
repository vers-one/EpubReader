namespace VersOne.Epub.Test.Unit.Content.Base
{
    public class EpubContentFileTests
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

        [Fact(DisplayName = "Constructing a EpubLocalTextContentFile instance with non-null parameters should succeed")]
        public void LocalTextContentFileConstructorTest()
        {
            EpubLocalTextContentFile epubLocalTextContentFile = new(LOCAL_TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, LOCAL_TEXT_FILE_PATH, TEXT_FILE_CONTENT);
            Assert.Equal(LOCAL_TEXT_FILE_NAME, epubLocalTextContentFile.Key);
            Assert.Equal(TEXT_FILE_CONTENT_TYPE, epubLocalTextContentFile.ContentType);
            Assert.Equal(TEXT_FILE_CONTENT_MIME_TYPE, epubLocalTextContentFile.ContentMimeType);
            Assert.Equal(LOCAL_TEXT_FILE_PATH, epubLocalTextContentFile.FilePath);
            Assert.Equal(TEXT_FILE_CONTENT, epubLocalTextContentFile.Content);
            Assert.Equal(EpubContentLocation.LOCAL, epubLocalTextContentFile.ContentLocation);
            Assert.Equal(EpubContentFileType.TEXT, epubLocalTextContentFile.ContentFileType);
        }

        [Fact(DisplayName = "Constructing a EpubLocalByteContentFile instance with non-null parameters should succeed")]
        public void LocalByteContentFileConstructorTest()
        {
            EpubLocalByteContentFile epubLocalByteContentFile = new(LOCAL_BYTE_FILE_NAME, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE, LOCAL_BYTE_FILE_PATH, BYTE_FILE_CONTENT);
            Assert.Equal(LOCAL_BYTE_FILE_NAME, epubLocalByteContentFile.Key);
            Assert.Equal(BYTE_FILE_CONTENT_TYPE, epubLocalByteContentFile.ContentType);
            Assert.Equal(BYTE_FILE_CONTENT_MIME_TYPE, epubLocalByteContentFile.ContentMimeType);
            Assert.Equal(LOCAL_BYTE_FILE_PATH, epubLocalByteContentFile.FilePath);
            Assert.Equal(BYTE_FILE_CONTENT, epubLocalByteContentFile.Content);
            Assert.Equal(EpubContentLocation.LOCAL, epubLocalByteContentFile.ContentLocation);
            Assert.Equal(EpubContentFileType.BYTE_ARRAY, epubLocalByteContentFile.ContentFileType);
        }

        [Fact(DisplayName = "Constructing a EpubRemoteTextContentFile instance with non-null parameters should succeed")]
        public void RemoteTextContentFileConstructorTest()
        {
            EpubRemoteTextContentFile epubRemoteTextContentFile = new(REMOTE_TEXT_FILE_HREF, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, TEXT_FILE_CONTENT);
            Assert.Equal(REMOTE_TEXT_FILE_HREF, epubRemoteTextContentFile.Key);
            Assert.Equal(TEXT_FILE_CONTENT_TYPE, epubRemoteTextContentFile.ContentType);
            Assert.Equal(TEXT_FILE_CONTENT_MIME_TYPE, epubRemoteTextContentFile.ContentMimeType);
            Assert.Equal(REMOTE_TEXT_FILE_HREF, epubRemoteTextContentFile.Url);
            Assert.Equal(TEXT_FILE_CONTENT, epubRemoteTextContentFile.Content);
            Assert.Equal(EpubContentLocation.REMOTE, epubRemoteTextContentFile.ContentLocation);
            Assert.Equal(EpubContentFileType.TEXT, epubRemoteTextContentFile.ContentFileType);
        }

        [Fact(DisplayName = "Constructing a EpubRemoteByteContentFile instance with non-null parameters should succeed")]
        public void RemoteByteContentFileConstructorTest()
        {
            EpubRemoteByteContentFile epubRemoteByteContentFile = new(REMOTE_BYTE_FILE_HREF, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE, BYTE_FILE_CONTENT);
            Assert.Equal(REMOTE_BYTE_FILE_HREF, epubRemoteByteContentFile.Key);
            Assert.Equal(BYTE_FILE_CONTENT_TYPE, epubRemoteByteContentFile.ContentType);
            Assert.Equal(BYTE_FILE_CONTENT_MIME_TYPE, epubRemoteByteContentFile.ContentMimeType);
            Assert.Equal(REMOTE_BYTE_FILE_HREF, epubRemoteByteContentFile.Url);
            Assert.Equal(BYTE_FILE_CONTENT, epubRemoteByteContentFile.Content);
            Assert.Equal(EpubContentLocation.REMOTE, epubRemoteByteContentFile.ContentLocation);
            Assert.Equal(EpubContentFileType.BYTE_ARRAY, epubRemoteByteContentFile.ContentFileType);
        }

        [Fact(DisplayName = "EpubLocalTextContentFile constructor should throw ArgumentNullException if key parameter is null")]
        public void LocalTextContentFileConstructorWithNullKeyTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubLocalTextContentFile(null!, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, LOCAL_TEXT_FILE_PATH, TEXT_FILE_CONTENT));
        }

        [Fact(DisplayName = "EpubLocalByteContentFile constructor should throw ArgumentNullException if key parameter is null")]
        public void LocalByteContentFileConstructorWithNullKeyTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubLocalByteContentFile(null!, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE, LOCAL_BYTE_FILE_PATH, BYTE_FILE_CONTENT));
        }

        [Fact(DisplayName = "EpubRemoteTextContentFile constructor should throw ArgumentNullException if key parameter is null")]
        public void RemoteTextContentFileConstructorWithNullKeyTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubRemoteTextContentFile(null!, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, TEXT_FILE_CONTENT));
        }

        [Fact(DisplayName = "EpubRemoteByteContentFile constructor should throw ArgumentNullException if key parameter is null")]
        public void RemoteByteContentFileConstructorWithNullKeyTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubRemoteByteContentFile(null!, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE, BYTE_FILE_CONTENT));
        }

        [Fact(DisplayName = "EpubLocalTextContentFile constructor should throw ArgumentNullException if contentMimeType parameter is null")]
        public void LocalTextContentFileConstructorWithNullContentMimeTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubLocalTextContentFile(LOCAL_TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, null!, LOCAL_TEXT_FILE_PATH, TEXT_FILE_CONTENT));
        }

        [Fact(DisplayName = "EpubLocalByteContentFile constructor should throw ArgumentNullException if contentMimeType parameter is null")]
        public void LocalByteContentFileConstructorWithNullContentMimeTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubLocalByteContentFile(LOCAL_BYTE_FILE_NAME, BYTE_FILE_CONTENT_TYPE, null!, LOCAL_BYTE_FILE_PATH, BYTE_FILE_CONTENT));
        }

        [Fact(DisplayName = "EpubRemoteTextContentFile constructor should throw ArgumentNullException if contentMimeType parameter is null")]
        public void RemoteTextContentFileConstructorWithNullContentMimeTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubRemoteTextContentFile(REMOTE_TEXT_FILE_HREF, TEXT_FILE_CONTENT_TYPE, null!, TEXT_FILE_CONTENT));
        }

        [Fact(DisplayName = "EpubRemoteByteContentFile constructor should throw ArgumentNullException if contentMimeType parameter is null")]
        public void RemoteByteContentFileConstructorWithNullContentMimeTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubRemoteByteContentFile(REMOTE_BYTE_FILE_HREF, BYTE_FILE_CONTENT_TYPE, null!, BYTE_FILE_CONTENT));
        }

        [Fact(DisplayName = "EpubLocalTextContentFile constructor should throw ArgumentNullException if filePath parameter is null")]
        public void LocalTextContentFileConstructorWithNullFilePathTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubLocalTextContentFile(LOCAL_TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, null!, TEXT_FILE_CONTENT));
        }

        [Fact(DisplayName = "EpubLocalByteContentFile constructor should throw ArgumentNullException if filePath parameter is null")]
        public void LocalByteContentFileConstructorWithNullFilePathTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubLocalByteContentFile(LOCAL_BYTE_FILE_NAME, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE, null!, BYTE_FILE_CONTENT));
        }

        [Fact(DisplayName = "EpubLocalTextContentFile constructor should throw ArgumentNullException if content parameter is null")]
        public void LocalTextContentFileConstructorWithNullContentTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubLocalTextContentFile(LOCAL_TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, LOCAL_TEXT_FILE_PATH, null!));
        }

        [Fact(DisplayName = "EpubLocalByteContentFile constructor should throw ArgumentNullException if content parameter is null")]
        public void LocalByteContentFileConstructorWithNullContentTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubLocalByteContentFile(LOCAL_BYTE_FILE_NAME, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE, LOCAL_BYTE_FILE_PATH, null!));
        }

        [Fact(DisplayName = "Constructing a EpubRemoteTextContentFile instance with null content parameter should succeed")]
        public void RemoteTextContentFileConstructorWithNullContentTest()
        {
            EpubRemoteTextContentFile epubRemoteTextContentFile = new(REMOTE_TEXT_FILE_HREF, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, null);
            Assert.Equal(REMOTE_TEXT_FILE_HREF, epubRemoteTextContentFile.Key);
            Assert.Equal(TEXT_FILE_CONTENT_TYPE, epubRemoteTextContentFile.ContentType);
            Assert.Equal(TEXT_FILE_CONTENT_MIME_TYPE, epubRemoteTextContentFile.ContentMimeType);
            Assert.Equal(REMOTE_TEXT_FILE_HREF, epubRemoteTextContentFile.Url);
            Assert.Null(epubRemoteTextContentFile.Content);
            Assert.Equal(EpubContentLocation.REMOTE, epubRemoteTextContentFile.ContentLocation);
            Assert.Equal(EpubContentFileType.TEXT, epubRemoteTextContentFile.ContentFileType);
        }

        [Fact(DisplayName = "Constructing a EpubRemoteByteContentFile instance with null content parameter should succeed")]
        public void RemoteByteContentFileConstructorWithNullContentTest()
        {
            EpubRemoteByteContentFile epubRemoteByteContentFile = new(REMOTE_BYTE_FILE_HREF, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE, null);
            Assert.Equal(REMOTE_BYTE_FILE_HREF, epubRemoteByteContentFile.Key);
            Assert.Equal(BYTE_FILE_CONTENT_TYPE, epubRemoteByteContentFile.ContentType);
            Assert.Equal(BYTE_FILE_CONTENT_MIME_TYPE, epubRemoteByteContentFile.ContentMimeType);
            Assert.Equal(REMOTE_BYTE_FILE_HREF, epubRemoteByteContentFile.Url);
            Assert.Null(epubRemoteByteContentFile.Content);
            Assert.Equal(EpubContentLocation.REMOTE, epubRemoteByteContentFile.ContentLocation);
            Assert.Equal(EpubContentFileType.BYTE_ARRAY, epubRemoteByteContentFile.ContentFileType);
        }
    }
}
