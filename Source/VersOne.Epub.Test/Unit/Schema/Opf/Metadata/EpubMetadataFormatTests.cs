using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataFormatTests
    {
        private const string FORMAT = "test-format";
        private const string ID = "format";

        [Fact(DisplayName = "Constructing a EpubMetadataFormat instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataFormat epubMetadataFormat = new(FORMAT, ID);
            Assert.Equal(FORMAT, epubMetadataFormat.Format);
            Assert.Equal(ID, epubMetadataFormat.Id);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if format parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataFormat(null!, ID));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataFormat instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataFormat epubMetadataFormat = new(FORMAT, null);
            Assert.Equal(FORMAT, epubMetadataFormat.Format);
            Assert.Null(epubMetadataFormat.Id);
        }
    }
}
