namespace VersOne.Epub.Test.Unit.Exceptions
{
    public class EpubSmilExceptionTests
    {
        private const string TEST_SMIL_FILE_PATH = "Content/file.smil";
        private const string TEST_MESSAGE = "test message";

        [Fact(DisplayName = "Creating EpubSmilException with SMIL file path should succeed")]
        public void CreateWithContentFilePathTest()
        {
            EpubSmilException epubSmilException = new(TEST_SMIL_FILE_PATH);
            Assert.Equal(TEST_SMIL_FILE_PATH, epubSmilException.SmilFilePath);
        }

        [Fact(DisplayName = "Creating EpubSmilException with message and SMIL file path should succeed")]
        public void CreateWithMessageAndContentFilePathTest()
        {
            EpubSmilException epubSmilException = new(TEST_MESSAGE, TEST_SMIL_FILE_PATH);
            Assert.Equal(TEST_MESSAGE, epubSmilException.Message);
            Assert.Equal(TEST_SMIL_FILE_PATH, epubSmilException.SmilFilePath);
        }

        [Fact(DisplayName = "Creating EpubSmilException with message, inner exception, and SMIL file path should succeed")]
        public void CreateWithMessageAndInnerExceptionAndContentFilePathTest()
        {
            Exception innerException = new();
            EpubSmilException epubSmilException = new(TEST_MESSAGE, innerException, TEST_SMIL_FILE_PATH);
            Assert.Equal(TEST_MESSAGE, epubSmilException.Message);
            Assert.Equal(innerException, epubSmilException.InnerException);
            Assert.Equal(TEST_SMIL_FILE_PATH, epubSmilException.SmilFilePath);
        }
    }
}
