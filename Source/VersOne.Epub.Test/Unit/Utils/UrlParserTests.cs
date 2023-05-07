using VersOne.Epub.Internal;

namespace VersOne.Epub.Test.Unit.Utils
{
    public class UrlParserTests
    {
        [Theory(DisplayName = "Parsing URLs with or without anchor should succeed")]
        [InlineData("file.html#anchor", "file.html", "anchor")]
        [InlineData("file.html", "file.html", null)]
        public void ConstructorWithNonNullUrlTest(string url, string expectedPath, string expectedAnchor)
        {
            UrlParser urlParser = new(url);
            Assert.Equal(expectedPath, urlParser.Path);
            Assert.Equal(expectedAnchor, urlParser.Anchor);
        }

        [Fact(DisplayName = "Constructor should throw an ArgumentNullException if URL parameter is null")]
        public void ConstructorWithNullUrlTest()
        {
            Assert.Throws<ArgumentNullException>(() => new UrlParser(null!));
        }
    }
}
