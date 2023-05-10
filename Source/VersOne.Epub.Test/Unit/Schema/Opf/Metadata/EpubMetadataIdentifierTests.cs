using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataIdentifierTests
    {
        private const string IDENTIFIER = "9781234567890";
        private const string ID = "identifier";
        private const string SCHEME = "ISBN";

        [Fact(DisplayName = "Constructing a EpubMetadataIdentifier instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataIdentifier epubMetadataIdentifier = new(IDENTIFIER, ID, SCHEME);
            Assert.Equal(IDENTIFIER, epubMetadataIdentifier.Identifier);
            Assert.Equal(ID, epubMetadataIdentifier.Id);
            Assert.Equal(SCHEME, epubMetadataIdentifier.Scheme);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if identifier parameter is null")]
        public void ConstructorWithNullIdentifierTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataIdentifier(null!, ID, SCHEME));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataIdentifier instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataIdentifier epubMetadataIdentifier = new(IDENTIFIER, null, SCHEME);
            Assert.Equal(IDENTIFIER, epubMetadataIdentifier.Identifier);
            Assert.Null(epubMetadataIdentifier.Id);
            Assert.Equal(SCHEME, epubMetadataIdentifier.Scheme);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataIdentifier instance with null scheme parameter should succeed")]
        public void ConstructorWithNullSchemeTest()
        {
            EpubMetadataIdentifier epubMetadataIdentifier = new(IDENTIFIER, ID, null);
            Assert.Equal(IDENTIFIER, epubMetadataIdentifier.Identifier);
            Assert.Equal(ID, epubMetadataIdentifier.Id);
            Assert.Null(epubMetadataIdentifier.Scheme);
        }
    }
}
