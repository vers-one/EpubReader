using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubMetadataIdentifierTests
    {
        private const string ID = "identifier";
        private const string SCHEME = "ISBN";
        private const string IDENTIFIER = "9781234567890";

        [Fact(DisplayName = "Constructing a EpubMetadataIdentifier instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataIdentifier epubMetadataIdentifier = new(ID, SCHEME, IDENTIFIER);
            Assert.Equal(ID, epubMetadataIdentifier.Id);
            Assert.Equal(SCHEME, epubMetadataIdentifier.Scheme);
            Assert.Equal(IDENTIFIER, epubMetadataIdentifier.Identifier);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if identifier parameter is null")]
        public void ConstructorWithNullIdentifierTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataIdentifier(ID, SCHEME, null!));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataIdentifier instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataIdentifier epubMetadataIdentifier = new(null, SCHEME, IDENTIFIER);
            Assert.Null(epubMetadataIdentifier.Id);
            Assert.Equal(SCHEME, epubMetadataIdentifier.Scheme);
            Assert.Equal(IDENTIFIER, epubMetadataIdentifier.Identifier);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataIdentifier instance with null scheme parameter should succeed")]
        public void ConstructorWithNullSchemeTest()
        {
            EpubMetadataIdentifier epubMetadataIdentifier = new(ID, null, IDENTIFIER);
            Assert.Equal(ID, epubMetadataIdentifier.Id);
            Assert.Null(epubMetadataIdentifier.Scheme);
            Assert.Equal(IDENTIFIER, epubMetadataIdentifier.Identifier);
        }
    }
}
