namespace VersOne.Epub.Test.Unit.Exceptions
{
    public class EpubContentDownloaderExceptionTests
    {
        private const string TEST_REMOTE_CONTENT_FILE_URL = "https://example.com/books/123/test.html";
        private const string TEST_MESSAGE = "test message";

        [Fact(DisplayName = "Creating EpubContentDownloaderException with remote content file URL should succeed")]
        public void CreateWithRemoteContentFileUrlTest()
        {
            EpubContentDownloaderException epubContentDownloaderException = new(TEST_REMOTE_CONTENT_FILE_URL);
            Assert.Equal(TEST_REMOTE_CONTENT_FILE_URL, epubContentDownloaderException.RemoteContentFileUrl);
        }

        [Fact(DisplayName = "Creating EpubContentDownloaderException with message and remote content file URL should succeed")]
        public void CreateWithMessageAndContentFileUrlTest()
        {
            EpubContentDownloaderException epubContentDownloaderException = new(TEST_MESSAGE, TEST_REMOTE_CONTENT_FILE_URL);
            Assert.Equal(TEST_MESSAGE, epubContentDownloaderException.Message);
            Assert.Equal(TEST_REMOTE_CONTENT_FILE_URL, epubContentDownloaderException.RemoteContentFileUrl);
        }

        [Fact(DisplayName = "Creating EpubContentDownloaderException with message, inner exception, and remote content file URL should succeed")]
        public void CreateWithMessageAndInnerExceptionAndContentFileUrlTest()
        {
            Exception innerException = new();
            EpubContentDownloaderException epubContentDownloaderException = new(TEST_MESSAGE, innerException, TEST_REMOTE_CONTENT_FILE_URL);
            Assert.Equal(TEST_MESSAGE, epubContentDownloaderException.Message);
            Assert.Equal(innerException, epubContentDownloaderException.InnerException);
            Assert.Equal(TEST_REMOTE_CONTENT_FILE_URL, epubContentDownloaderException.RemoteContentFileUrl);
        }

    }
}
