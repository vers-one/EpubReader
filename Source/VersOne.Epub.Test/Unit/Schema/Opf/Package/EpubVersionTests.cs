using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Package
{
    public class EpubVersionTests
    {
        [Theory(DisplayName = "Converting EpubVersion enumeration to string should succeed")]
        [InlineData(EpubVersion.EPUB_2, "2")]
        [InlineData(EpubVersion.EPUB_3, "3")]
        [InlineData(EpubVersion.EPUB_3_1, "3.1")]
        [InlineData((EpubVersion)0, "0")]
        public void GetVersionStringTest(EpubVersion epubVersion, string expectedVersionString)
        {
            string actualVersionString = epubVersion.GetVersionString();
            Assert.Equal(expectedVersionString, actualVersionString);
        }
    }
}
