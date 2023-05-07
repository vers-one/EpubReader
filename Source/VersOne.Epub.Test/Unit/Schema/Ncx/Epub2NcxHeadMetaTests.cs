using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxHeadMetaTests
    {
        private const string NAME = "location";
        private const string CONTENT = "https://example.com/books/123/ncx";
        private const string SCHEME = "URI";

        [Fact(DisplayName = "Constructing a Epub2NcxHeadMeta instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub2NcxHeadMeta epub2NcxHeadMeta = new(NAME, CONTENT, SCHEME);
            Assert.Equal(NAME, epub2NcxHeadMeta.Name);
            Assert.Equal(CONTENT, epub2NcxHeadMeta.Content);
            Assert.Equal(SCHEME, epub2NcxHeadMeta.Scheme);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if name parameter is null")]
        public void ConstructorWithNullNameTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub2NcxHeadMeta(null!, CONTENT, SCHEME));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if content parameter is null")]
        public void ConstructorWithNullContentTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub2NcxHeadMeta(NAME, null!, SCHEME));
        }

        [Fact(DisplayName = "Constructing a Epub2NcxHeadMeta instance with null scheme parameter should succeed")]
        public void ConstructorWithNullFilePathTest()
        {
            Epub2NcxHeadMeta epub2NcxHeadMeta = new(NAME, CONTENT, null);
            Assert.Equal(NAME, epub2NcxHeadMeta.Name);
            Assert.Equal(CONTENT, epub2NcxHeadMeta.Content);
            Assert.Null(epub2NcxHeadMeta.Scheme);
        }
    }
}
