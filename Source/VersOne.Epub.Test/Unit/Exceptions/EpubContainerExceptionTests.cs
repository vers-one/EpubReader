namespace VersOne.Epub.Test.Unit.Exceptions
{
    public class EpubContainerExceptionTests
    {
        private const string TEST_MESSAGE = "test message";

        [Fact(DisplayName = "Creating EpubContainerException without arguments should succeed")]
        public void CreateWithNoArgumentsTest()
        {
            EpubContainerException epubContainerException = new();
            Assert.Equal(EpubSchemaFileType.META_INF_CONTAINER, epubContainerException.SchemaFileType);
        }

        [Fact(DisplayName = "Creating EpubContainerException with message should succeed")]
        public void CreateWithMessageTest()
        {
            EpubContainerException epubContainerException = new(TEST_MESSAGE);
            Assert.Equal(EpubSchemaFileType.META_INF_CONTAINER, epubContainerException.SchemaFileType);
            Assert.Equal(TEST_MESSAGE, epubContainerException.Message);
        }

        [Fact(DisplayName = "Creating EpubContainerException with message and inner exception should succeed")]
        public void CreateWithMessageAndInnerExceptionTest()
        {
            Exception innerException = new();
            EpubContainerException epubContainerException = new(TEST_MESSAGE, innerException);
            Assert.Equal(EpubSchemaFileType.META_INF_CONTAINER, epubContainerException.SchemaFileType);
            Assert.Equal(TEST_MESSAGE, epubContainerException.Message);
            Assert.Equal(innerException, epubContainerException.InnerException);
        }
    }
}
