using VersOne.Epub.Internal;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class RootFilePathReaderTests
    {
        private const string EPUB_CONTAINER_FILE_PATH = "META-INF/container.xml";
        private const string ROOT_FILE_PATH_FOR_CORRECT_CONTAINER_FILE = "OEBPS/content.opf";

        private const string CORRECT_CONTAINER_FILE = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
                <rootfile media-type="application/oebps-package+xml" full-path="{ROOT_FILE_PATH_FOR_CORRECT_CONTAINER_FILE}" />
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

        [Fact(DisplayName = "Getting root file path from a ZIP archive with a correct container file should succeed")]
        public async void TestGetRootFilePathAsyncWithCorrectContainerFile()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH, CORRECT_CONTAINER_FILE);
            RootFilePathReader rootFilePathReader = new();
            string actualRootFilePath = await rootFilePathReader.GetRootFilePathAsync(testZipFile);
            Assert.Equal(ROOT_FILE_PATH_FOR_CORRECT_CONTAINER_FILE, actualRootFilePath);
        }

        [Fact(DisplayName = "Getting root file path from a ZIP archive without a container file should throw EpubContainerException")]
        public async void TestGetRootFilePathAsyncWithNoContainerFile()
        {
            TestZipFile testZipFile = new();
            RootFilePathReader rootFilePathReader = new();
            await Assert.ThrowsAsync<EpubContainerException>(() => rootFilePathReader.GetRootFilePathAsync(testZipFile));
        }

        [Theory(DisplayName = "Getting root file path from a ZIP archive with an incorrect container file should throw EpubContainerException")]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_FULL_PATH_ATTRIBUTE)]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_ROOTFILE_ELEMENT)]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_ROOTFILES_ELEMENT)]
        [InlineData(INCORRECT_CONTAINER_FILE_NO_CONTAINER_ELEMENT)]
        public async void TestGetRootFilePathAsyncWithIncorrectContainerFile(string containerFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH, containerFileContent);
            RootFilePathReader rootFilePathReader = new();
            await Assert.ThrowsAsync<EpubContainerException>(() => rootFilePathReader.GetRootFilePathAsync(testZipFile));
        }
    }
}
