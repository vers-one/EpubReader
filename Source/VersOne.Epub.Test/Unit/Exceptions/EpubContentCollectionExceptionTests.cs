namespace VersOne.Epub.Test.Unit.Exceptions
{
    public class EpubContentCollectionExceptionTests
    {
        private const string TEST_MESSAGE = "test message";

        [Fact(DisplayName = "Creating EpubContentCollectionException without arguments should succeed")]
        public void CreateWithNoArgumentsTest()
        {
            _ = new EpubContentCollectionException();
        }

        [Fact(DisplayName = "Creating EpubContentCollectionException with message should succeed")]
        public void CreateWithMessageTest()
        {
            EpubContentCollectionException epubContentCollectionException = new(TEST_MESSAGE);
            Assert.Equal(TEST_MESSAGE, epubContentCollectionException.Message);
        }

        [Fact(DisplayName = "Creating EpubContentCollectionException with message and inner exception should succeed")]
        public void CreateWithMessageAndInnerExceptionTest()
        {
            Exception innerException = new();
            EpubContentCollectionException epubContentCollectionException = new(TEST_MESSAGE, innerException);
            Assert.Equal(TEST_MESSAGE, epubContentCollectionException.Message);
            Assert.Equal(innerException, epubContentCollectionException.InnerException);
        }

    }
}
