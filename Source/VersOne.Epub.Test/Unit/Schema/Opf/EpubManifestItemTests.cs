using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubManifestItemTests
    {
        public class EpubGuideReferenceTests
        {
            [Fact(DisplayName = "ToString method should produce a string with the values of Id, Href, and MediaType properties")]
            public void ToStringTest()
            {
                EpubManifestItem epubManifestItem = new()
                {
                    Id = "item-1",
                    Href = "chapter1.html",
                    MediaType = "application/xhtml+xml"
                };
                Assert.Equal("Id: item-1, Href = chapter1.html, MediaType = application/xhtml+xml", epubManifestItem.ToString());
            }
        }
    }
}
