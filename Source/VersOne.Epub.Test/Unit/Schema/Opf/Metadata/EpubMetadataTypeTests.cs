using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataTypeTests
    {
        private const string TYPE = "dictionary";
        private const string ID = "type";

        [Fact(DisplayName = "Constructing a EpubMetadataType instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataType epubMetadataType = new(TYPE, ID);
            Assert.Equal(TYPE, epubMetadataType.Type);
            Assert.Equal(ID, epubMetadataType.Id);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if type parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataType(null!, ID));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataType instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataType epubMetadataType = new(TYPE, null);
            Assert.Equal(TYPE, epubMetadataType.Type);
            Assert.Null(epubMetadataType.Id);
        }
    }
}
