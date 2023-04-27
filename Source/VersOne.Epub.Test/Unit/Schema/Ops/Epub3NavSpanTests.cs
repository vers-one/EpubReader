using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Ops
{
    public class Epub3NavSpanTests
    {
        private const string TEXT = "Text";
        private const string TITLE = "Title";
        private const string ALT = "Alt";

        [Fact(DisplayName = "Constructing a Epub3NavSpan instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub3NavSpan epub3NavSpan = new(TEXT, TITLE, ALT);
            Assert.Equal(TEXT, epub3NavSpan.Text);
            Assert.Equal(TITLE, epub3NavSpan.Title);
            Assert.Equal(ALT, epub3NavSpan.Alt);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if text parameter is null")]
        public void ConstructorWithNullTextTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub3NavSpan(null!, TITLE, ALT));
        }

        [Fact(DisplayName = "Constructing a Epub3NavSpan instance with null title parameter should succeed")]
        public void ConstructorWithNullTitleTest()
        {
            Epub3NavSpan epub3NavSpan = new(TEXT, null, ALT);
            Assert.Equal(TEXT, epub3NavSpan.Text);
            Assert.Null(epub3NavSpan.Title);
            Assert.Equal(ALT, epub3NavSpan.Alt);
        }

        [Fact(DisplayName = "Constructing a Epub3NavSpan instance with null alt parameter should succeed")]
        public void ConstructorWithNullAltTest()
        {
            Epub3NavSpan epub3NavSpan = new(TEXT, TITLE, null);
            Assert.Equal(TEXT, epub3NavSpan.Text);
            Assert.Equal(TITLE, epub3NavSpan.Title);
            Assert.Null(epub3NavSpan.Alt);
        }
    }
}
