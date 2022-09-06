using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubPackageTests
    {
        [Fact(DisplayName = "GetVersionString should return a string representation of the EpubVersion property")]
        public void GetVersionStringTest()
        {
            EpubPackage epubPackage = new()
            {
                EpubVersion = EpubVersion.EPUB_3_1
            };
            Assert.Equal("3.1", epubPackage.GetVersionString());
        }
    }
}
