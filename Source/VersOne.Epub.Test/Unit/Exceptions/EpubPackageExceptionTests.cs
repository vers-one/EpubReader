namespace VersOne.Epub.Test.Unit.Exceptions
{
    public class EpubPackageExceptionTests
    {
        private const string TEST_MESSAGE = "test message";

        [Fact(DisplayName = "Creating EpubPackageException without arguments should succeed")]
        public void CreateWithNoArgumentsTest()
        {
            EpubPackageException epubPackageException = new();
            Assert.Equal(EpubSchemaFileType.OPF_PACKAGE, epubPackageException.SchemaFileType);
        }

        [Fact(DisplayName = "Creating EpubPackageException with message should succeed")]
        public void CreateWithMessageTest()
        {
            EpubPackageException epubPackageException = new(TEST_MESSAGE);
            Assert.Equal(EpubSchemaFileType.OPF_PACKAGE, epubPackageException.SchemaFileType);
            Assert.Equal(TEST_MESSAGE, epubPackageException.Message);
        }

        [Fact(DisplayName = "Creating EpubPackageException with message and inner exception should succeed")]
        public void CreateWithMessageAndInnerExceptionTest()
        {
            Exception innerException = new();
            EpubPackageException epubPackageException = new(TEST_MESSAGE, innerException);
            Assert.Equal(EpubSchemaFileType.OPF_PACKAGE, epubPackageException.SchemaFileType);
            Assert.Equal(TEST_MESSAGE, epubPackageException.Message);
            Assert.Equal(innerException, epubPackageException.InnerException);
        }
    }
}
