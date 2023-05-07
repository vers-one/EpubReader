using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubGuideReferenceTests
    {
        private const string TYPE = "toc";
        private const string TITLE = "Title";
        private const string HREF = "toc.html";

        [Fact(DisplayName = "Constructing a EpubGuideReference instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubGuideReference epubGuideReference = new(TYPE, TITLE, HREF);
            Assert.Equal(TYPE, epubGuideReference.Type);
            Assert.Equal(TITLE, epubGuideReference.Title);
            Assert.Equal(HREF, epubGuideReference.Href);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if type parameter is null")]
        public void ConstructorWithNullTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubGuideReference(null!, TITLE, HREF));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if href parameter is null")]
        public void ConstructorWithNullHrefTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubGuideReference(TYPE, TITLE, null!));
        }

        [Fact(DisplayName = "Constructing a EpubGuideReference instance with null title parameter should succeed")]
        public void ConstructorWithNullTitleTest()
        {
            EpubGuideReference epubGuideReference = new(TYPE, null, HREF);
            Assert.Equal(TYPE, epubGuideReference.Type);
            Assert.Null(epubGuideReference.Title);
            Assert.Equal(HREF, epubGuideReference.Href);
        }

        [Fact(DisplayName = "ToString method should produce a string with the values of Type and Href properties")]
        public void ToStringTest()
        {
            EpubGuideReference epubGuideReference = new
            (
                type: "toc",
                title: null,
                href: "toc.html"
            );
            Assert.Equal("Type: toc, Href: toc.html", epubGuideReference.ToString());
        }
    }
}
