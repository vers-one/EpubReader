using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Ops
{
    public class Epub3NavAnchorTests
    {
        private const string HREF = "chapter.html";
        private const string TEXT = "Chapter";
        private const string TITLE = "Test anchor title";
        private const string ALT = "Test anchor alt";
        private Epub3StructuralSemanticsProperty TYPE = Epub3StructuralSemanticsProperty.BODYMATTER;

        [Fact(DisplayName = "Constructing a Epub3NavAnchor instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub3NavAnchor epub3NavAnchor = new(HREF, TEXT, TITLE, ALT, TYPE);
            Assert.Equal(HREF, epub3NavAnchor.Href);
            Assert.Equal(TEXT, epub3NavAnchor.Text);
            Assert.Equal(TITLE, epub3NavAnchor.Title);
            Assert.Equal(ALT, epub3NavAnchor.Alt);
            Assert.Equal(TYPE, epub3NavAnchor.Type);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if text parameter is null")]
        public void ConstructorWithNullTextTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub3NavAnchor(HREF, null!, TITLE, ALT, TYPE));
        }

        [Fact(DisplayName = "Constructing a Epub3NavAnchor instance with null href parameter should succeed")]
        public void ConstructorWithNullHrefTest()
        {
            Epub3NavAnchor epub3NavAnchor = new(null, TEXT, TITLE, ALT, TYPE);
            Assert.Null(epub3NavAnchor.Href);
            Assert.Equal(TEXT, epub3NavAnchor.Text);
            Assert.Equal(TITLE, epub3NavAnchor.Title);
            Assert.Equal(ALT, epub3NavAnchor.Alt);
            Assert.Equal(TYPE, epub3NavAnchor.Type);
        }

        [Fact(DisplayName = "Constructing a Epub3NavAnchor instance with null title parameter should succeed")]
        public void ConstructorWithNullTitleTest()
        {
            Epub3NavAnchor epub3NavAnchor = new(HREF, TEXT, null, ALT, TYPE);
            Assert.Equal(HREF, epub3NavAnchor.Href);
            Assert.Equal(TEXT, epub3NavAnchor.Text);
            Assert.Null(epub3NavAnchor.Title);
            Assert.Equal(ALT, epub3NavAnchor.Alt);
            Assert.Equal(TYPE, epub3NavAnchor.Type);
        }

        [Fact(DisplayName = "Constructing a Epub3NavAnchor instance with null alt parameter should succeed")]
        public void ConstructorWithNullAltTest()
        {
            Epub3NavAnchor epub3NavAnchor = new(HREF, TEXT, TITLE, null, TYPE);
            Assert.Equal(HREF, epub3NavAnchor.Href);
            Assert.Equal(TEXT, epub3NavAnchor.Text);
            Assert.Equal(TITLE, epub3NavAnchor.Title);
            Assert.Null(epub3NavAnchor.Alt);
            Assert.Equal(TYPE, epub3NavAnchor.Type);
        }
    }
}
