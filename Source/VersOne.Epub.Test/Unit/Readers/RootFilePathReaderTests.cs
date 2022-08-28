using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class RootFilePathReaderTests
    {
        private const string EPUB_CONTAINER_FILE_PATH = "META-INF/container.xml";

        [Fact]
        public async void TestGetRootFilePathAsync()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH,
@"<?xml version='1.0' encoding='utf-8'?>
<container xmlns=""urn:oasis:names:tc:opendocument:xmlns:container"" version=""1.0"">
  <rootfiles>
    <rootfile media-type=""application/oebps-package+xml"" full-path=""OEBPS/content.opf""/>
  </rootfiles>
</container>");
            string actualRootFilePath = await RootFilePathReader.GetRootFilePathAsync(testZipFile, new EpubReaderOptions());
            Assert.Equal("OEBPS/content.opf", actualRootFilePath);
        }
    }
}
