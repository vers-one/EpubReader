namespace VersOne.Epub.Test.Unit.Exceptions
{
    public class Epub3NavExceptionTests
    {
        private const string TEST_MESSAGE = "test message";

        [Fact(DisplayName = "Creating Epub3NavException without arguments should succeed")]
        public void CreateWithNoArgumentsTest()
        {
            Epub3NavException epub3NavException = new();
            Assert.Equal(EpubSchemaFileType.EPUB3_NAV_DOCUMENT, epub3NavException.SchemaFileType);
        }

        [Fact(DisplayName = "Creating Epub3NavException with message should succeed")]
        public void CreateWithMessageTest()
        {
            Epub3NavException epub3NavException = new(TEST_MESSAGE);
            Assert.Equal(EpubSchemaFileType.EPUB3_NAV_DOCUMENT, epub3NavException.SchemaFileType);
            Assert.Equal(TEST_MESSAGE, epub3NavException.Message);
        }

        [Fact(DisplayName = "Creating Epub3NavException with message and inner exception should succeed")]
        public void CreateWithMessageAndInnerExceptionTest()
        {
            Exception innerException = new();
            Epub3NavException epub3NavException = new(TEST_MESSAGE, innerException);
            Assert.Equal(EpubSchemaFileType.EPUB3_NAV_DOCUMENT, epub3NavException.SchemaFileType);
            Assert.Equal(TEST_MESSAGE, epub3NavException.Message);
            Assert.Equal(innerException, epub3NavException.InnerException);
        }
    }
}
