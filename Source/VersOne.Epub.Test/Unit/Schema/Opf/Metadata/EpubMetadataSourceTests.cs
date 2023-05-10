using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataSourceTests
    {
        private const string SOURCE = "https://example.com/books/123/content.html";
        private const string ID = "source";

        [Fact(DisplayName = "Constructing a EpubMetadataSource instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataSource epubMetadataSource = new(SOURCE, ID);
            Assert.Equal(SOURCE, epubMetadataSource.Source);
            Assert.Equal(ID, epubMetadataSource.Id);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if source parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataSource(null!, ID));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataSource instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataSource epubMetadataSource = new(SOURCE, null);
            Assert.Equal(SOURCE, epubMetadataSource.Source);
            Assert.Null(epubMetadataSource.Id);
        }
    }
}
