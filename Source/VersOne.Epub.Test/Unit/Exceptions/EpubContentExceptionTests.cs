namespace VersOne.Epub.Test.Unit.Exceptions
{
    public class EpubContentExceptionTests
    {
        private const string TEST_CONTENT_FILE_PATH = "Content/file.html";
        private const string TEST_MESSAGE = "test message";

        [Fact(DisplayName = "Creating EpubContentException with content file path should succeed")]
        public void CreateWithContentFilePathTest()
        {
            EpubContentException epubContentException = new(TEST_CONTENT_FILE_PATH);
            Assert.Equal(TEST_CONTENT_FILE_PATH, epubContentException.ContentFilePath);
        }

        [Fact(DisplayName = "Creating EpubContentException with message and content file path should succeed")]
        public void CreateWithMessageAndContentFilePathTest()
        {
            EpubContentException epubContentException = new(TEST_MESSAGE, TEST_CONTENT_FILE_PATH);
            Assert.Equal(TEST_MESSAGE, epubContentException.Message);
            Assert.Equal(TEST_CONTENT_FILE_PATH, epubContentException.ContentFilePath);
        }

        [Fact(DisplayName = "Creating EpubContentException with message, inner exception, and content file path should succeed")]
        public void CreateWithMessageAndInnerExceptionAndContentFilePathTest()
        {
            Exception innerException = new();
            EpubContentException epubContentException = new(TEST_MESSAGE, innerException, TEST_CONTENT_FILE_PATH);
            Assert.Equal(TEST_MESSAGE, epubContentException.Message);
            Assert.Equal(innerException, epubContentException.InnerException);
            Assert.Equal(TEST_CONTENT_FILE_PATH, epubContentException.ContentFilePath);
        }
    }
}
