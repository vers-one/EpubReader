using System.Xml;
using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class ContainerFileReaderTests
    {
        private const string EPUB_CONTAINER_FILE_PATH = "META-INF/container.xml";
        private const string OPF_PACKAGE_FILE_PATH_FOR_CORRECT_CONTAINER_FILE = "OEBPS/content.opf";

        private const string CORRECT_CONTAINER_FILE = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
                <rootfile media-type="application/oebps-package+xml" full-path="{OPF_PACKAGE_FILE_PATH_FOR_CORRECT_CONTAINER_FILE}" />
              </rootfiles>
            </container>
            """;

        private const string INCORRECT_CONTAINER_FILE_NO_FULL_PATH_ATTRIBUTE = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
                <rootfile media-type="application/oebps-package+xml" />
              </rootfiles>
            </container>
            """;

        private const string INCORRECT_CONTAINER_FILE_NO_ROOTFILE_ELEMENT = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
              </rootfiles>
            </container>
            """;

        private const string INCORRECT_CONTAINER_FILE_NO_ROOTFILES_ELEMENT = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
            </container>
            """;

        private const string INCORRECT_CONTAINER_FILE_NO_CONTAINER_ELEMENT = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <test />
            """;

        [Fact(DisplayName = "Constructing a ContainerFileReader instance with a non-null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNonNullEpubReaderOptionsTest()
        {
            _ = new ContainerFileReader(new EpubReaderOptions());
        }

        [Fact(DisplayName = "Constructing a ContainerFileReader instance with a null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNullEpubReaderOptionsTest()
        {
            _ = new ContainerFileReader(null);
        }

        [Fact(DisplayName = "Constructing a ContainerFileReader instance with epubReaderOptions.ContainerFileReaderOptions = null should succeed")]
        public void ConstructorWithNullPackageReaderOptionsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                ContainerFileReaderOptions = null!
            };
            _ = new ContainerFileReader(epubReaderOptions);
        }

        [Fact(DisplayName = "Getting OPF package file path from a ZIP archive with a correct container file should succeed")]
        public async Task GetPackageFilePathAsyncWithCorrectContainerFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH, CORRECT_CONTAINER_FILE);
            ContainerFileReader containerFileReader = new();
            string? actualPackageFilePath = await containerFileReader.GetPackageFilePathAsync(testZipFile);
            Assert.Equal(OPF_PACKAGE_FILE_PATH_FOR_CORRECT_CONTAINER_FILE, actualPackageFilePath);
        }

        [Fact(DisplayName = "GetPackageFilePathAsync should throw EpubContainerException if the ZIP archive doesn't have the container file and no ContainerFileReaderOptions are provided")]
        public async Task GetPackageFilePathAsyncWithNoContainerFileAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = new();
            ContainerFileReader containerFileReader = new();
            await Assert.ThrowsAsync<EpubContainerException>(() => containerFileReader.GetPackageFilePathAsync(testZipFile));
        }

        [Fact(DisplayName = "GetPackageFilePathAsync should return null if the ZIP archive doesn't have the container file and IgnoreMissingContainerFile = true")]
        public async Task GetPackageFilePathAsyncWithNoContainerFileAndIgnoreMissingContainerFileTest()
        {
            TestZipFile testZipFile = new();
            EpubReaderOptions epubReaderOptions = new()
            {
                ContainerFileReaderOptions = new()
                {
                    IgnoreMissingContainerFile = true
                }
            };
            ContainerFileReader containerFileReader = new(epubReaderOptions);
            string? actualPackageFilePath = await containerFileReader.GetPackageFilePathAsync(testZipFile);
            Assert.Null(actualPackageFilePath);
        }

        [Fact(DisplayName = "GetPackageFilePathAsync should throw EpubContainerException with an inner XmlException if the container document is not a valid XML file and no ContainerFileReaderOptions are provided")]
        public async Task GetPackageFilePathAsyncWithInvalidXmlFileAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH, "not a valid XML file");
            ContainerFileReader containerFileReader = new();
            EpubContainerException outerException =
                await Assert.ThrowsAsync<EpubContainerException>(() => containerFileReader.GetPackageFilePathAsync(testZipFile));
            Assert.NotNull(outerException.InnerException);
            Assert.Equal(typeof(XmlException), outerException.InnerException.GetType());
        }

        [Fact(DisplayName = "GetPackageFilePathAsync should return null if the container document is not a valid XML file and IgnoreContainerFileIsNotValidXmlError = true")]
        public async Task GetPackageFilePathAsyncWithInvalidXmlFileAndIgnoreContainerFileIsNotValidXmlErrorTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH, "not a valid XML file");
            EpubReaderOptions epubReaderOptions = new()
            {
                ContainerFileReaderOptions = new()
                {
                    IgnoreContainerFileIsNotValidXmlError = true
                }
            };
            ContainerFileReader containerFileReader = new(epubReaderOptions);
            string? actualPackageFilePath = await containerFileReader.GetPackageFilePathAsync(testZipFile);
            Assert.Null(actualPackageFilePath);
        }

        [Theory(DisplayName = "GetPackageFilePathAsync should throw EpubContainerException if the container document doesn't have the OPF package file path and no ContainerFileReaderOptions are provided")]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_FULL_PATH_ATTRIBUTE)]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_ROOTFILE_ELEMENT)]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_ROOTFILES_ELEMENT)]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_CONTAINER_ELEMENT)]
        public async Task GetPackageFilePathAsyncWithIncorrectContainerAndDefaultOptionsFileTest(string containerFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH, containerFileContent);
            ContainerFileReader containerFileReader = new();
            await Assert.ThrowsAsync<EpubContainerException>(() => containerFileReader.GetPackageFilePathAsync(testZipFile));
        }

        [Theory(DisplayName = "GetPackageFilePathAsync should return null if the container document doesn't have the OPF package file path and IgnoreMissingPackageFilePathError = true")]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_FULL_PATH_ATTRIBUTE)]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_ROOTFILE_ELEMENT)]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_ROOTFILES_ELEMENT)]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_CONTAINER_ELEMENT)]
        public async Task GetPackageFilePathAsyncWithIncorrectContainerAndIgnoreMissingPackageFilePathErrorTest(string containerFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH, containerFileContent);
            EpubReaderOptions epubReaderOptions = new()
            {
                ContainerFileReaderOptions = new()
                {
                    IgnoreMissingPackageFilePathError = true
                }
            };
            ContainerFileReader containerFileReader = new(epubReaderOptions);
            string? actualPackageFilePath = await containerFileReader.GetPackageFilePathAsync(testZipFile);
            Assert.Null(actualPackageFilePath);
        }
    }
}
