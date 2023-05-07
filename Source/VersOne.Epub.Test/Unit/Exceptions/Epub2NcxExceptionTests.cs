namespace VersOne.Epub.Test.Unit.Exceptions
{
    public class Epub2NcxExceptionTests
    {
        private const string TEST_MESSAGE = "test message";

        [Fact(DisplayName = "Creating Epub2NcxException without arguments should succeed")]
        public void CreateWithNoArgumentsTest()
        {
            Epub2NcxException epub2NcxException = new();
            Assert.Equal(EpubSchemaFileType.EPUB2_NCX, epub2NcxException.SchemaFileType);
        }

        [Fact(DisplayName = "Creating Epub2NcxException with message should succeed")]
        public void CreateWithMessageTest()
        {
            Epub2NcxException epub2NcxException = new(TEST_MESSAGE);
            Assert.Equal(EpubSchemaFileType.EPUB2_NCX, epub2NcxException.SchemaFileType);
            Assert.Equal(TEST_MESSAGE, epub2NcxException.Message);
        }

        [Fact(DisplayName = "Creating Epub2NcxException with message and inner exception should succeed")]
        public void CreateWithMessageAndInnerExceptionTest()
        {
            Exception innerException = new();
            Epub2NcxException epub2NcxException = new(TEST_MESSAGE, innerException);
            Assert.Equal(EpubSchemaFileType.EPUB2_NCX, epub2NcxException.SchemaFileType);
            Assert.Equal(TEST_MESSAGE, epub2NcxException.Message);
            Assert.Equal(innerException, epub2NcxException.InnerException);
        }
    }
}
