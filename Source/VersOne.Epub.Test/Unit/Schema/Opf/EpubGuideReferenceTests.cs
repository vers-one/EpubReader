using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubGuideReferenceTests
    {
        [Fact(DisplayName = "ToString method should produce a string with the values of Type and Href properties")]
        public void ToStringTest()
        {
            EpubGuideReference epubGuideReference = new()
            {
                Type = "toc",
                Href = "toc.html"
            };
            Assert.Equal("Type: toc, Href: toc.html", epubGuideReference.ToString());
        }
    }
}
