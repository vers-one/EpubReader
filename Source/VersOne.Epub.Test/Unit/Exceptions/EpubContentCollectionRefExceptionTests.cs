namespace VersOne.Epub.Test.Unit.Exceptions
{
    public class EpubContentCollectionRefExceptionTests
    {
        private const string TEST_MESSAGE = "test message";

        [Fact(DisplayName = "Creating EpubContentCollectionRefException without arguments should succeed")]
        public void CreateWithNoArgumentsTest()
        {
            _ = new EpubContentCollectionRefException();
        }

        [Fact(DisplayName = "Creating EpubContentCollectionRefException with message should succeed")]
        public void CreateWithMessageTest()
        {
            EpubContentCollectionRefException epubContentCollectionRefException = new(TEST_MESSAGE);
            Assert.Equal(TEST_MESSAGE, epubContentCollectionRefException.Message);
        }

        [Fact(DisplayName = "Creating EpubContentCollectionRefException with message and inner exception should succeed")]
        public void CreateWithMessageAndInnerExceptionTest()
        {
            Exception innerException = new();
            EpubContentCollectionRefException epubContentCollectionRefException = new(TEST_MESSAGE, innerException);
            Assert.Equal(TEST_MESSAGE, epubContentCollectionRefException.Message);
            Assert.Equal(innerException, epubContentCollectionRefException.InnerException);
        }

    }
}
