using VersOne.Epub.Internal;
using VersOne.Epub.Options;
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
                <rootfile media-type="application/oebps-package+xml" full-path="{ROOT_FILE_PATH_FOR_CORRECT_CONTAINER_FILE}"/>
              </rootfiles>
            </container>
            """;

        private const string INCORRECT_CONTAINER_FILE = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
              </rootfiles>
            </container>
            """;

        [Fact(DisplayName = "Getting root file path from a ZIP archive with a correct container file should succeed")]
        public async void TestGetRootFilePathAsyncWithCorrectContainerFile()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH, CORRECT_CONTAINER_FILE);
            string actualRootFilePath = await RootFilePathReader.GetRootFilePathAsync(testZipFile, new EpubReaderOptions());
            Assert.Equal(ROOT_FILE_PATH_FOR_CORRECT_CONTAINER_FILE, actualRootFilePath);
        }

        [Fact(DisplayName = "Getting root file path from a ZIP archive without a container file should throw EpubContainerException")]
        public async void TestGetRootFilePathAsyncWithNoContainerFile()
        {
            TestZipFile testZipFile = new();
            await Assert.ThrowsAsync<EpubContainerException>(() => RootFilePathReader.GetRootFilePathAsync(testZipFile, new EpubReaderOptions()));
        }

        [Fact(DisplayName = "Getting root file path from a ZIP archive with an incorrect container file should throw EpubContainerException")]
        public async void TestGetRootFilePathAsyncWithIncorrectContainerFile()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH, INCORRECT_CONTAINER_FILE);
            await Assert.ThrowsAsync<EpubContainerException>(() => RootFilePathReader.GetRootFilePathAsync(testZipFile, new EpubReaderOptions()));
        }
    }
}
