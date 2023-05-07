using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubMetadataLinkTests
    {
        private const string ID = "link";
        private const string HREF = "https://example.com/onix/123";
        private const string MEDIA_TYPE = "application/xml";
        private const string REFINES = "#title";

        private static List<EpubMetadataLinkProperty> Properties =>
            new()
            {
                EpubMetadataLinkProperty.ONIX
            };

        private static List<EpubMetadataLinkRelationship> Relationships =>
            new()
            {
                EpubMetadataLinkRelationship.RECORD,
                EpubMetadataLinkRelationship.ONIX_RECORD
            };

        [Fact(DisplayName = "Constructing a EpubMetadataLink instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataLink epubMetadataLink = new(ID, HREF, MEDIA_TYPE, Properties, REFINES, Relationships);
            Assert.Equal(ID, epubMetadataLink.Id);
            Assert.Equal(HREF, epubMetadataLink.Href);
            Assert.Equal(MEDIA_TYPE, epubMetadataLink.MediaType);
            Assert.Equal(Properties, epubMetadataLink.Properties);
            Assert.Equal(REFINES, epubMetadataLink.Refines);
            Assert.Equal(Relationships, epubMetadataLink.Relationships);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if href parameter is null")]
        public void ConstructorWithNullHrefTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataLink(ID, null!, MEDIA_TYPE, Properties, REFINES, Relationships));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataLink instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataLink epubMetadataLink = new(null, HREF, MEDIA_TYPE, Properties, REFINES, Relationships);
            Assert.Null(epubMetadataLink.Id);
            Assert.Equal(HREF, epubMetadataLink.Href);
            Assert.Equal(MEDIA_TYPE, epubMetadataLink.MediaType);
            Assert.Equal(Properties, epubMetadataLink.Properties);
            Assert.Equal(REFINES, epubMetadataLink.Refines);
            Assert.Equal(Relationships, epubMetadataLink.Relationships);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataLink instance with null mediaType parameter should succeed")]
        public void ConstructorWithNullMediaTypeTest()
        {
            EpubMetadataLink epubMetadataLink = new(ID, HREF, null, Properties, REFINES, Relationships);
            Assert.Equal(ID, epubMetadataLink.Id);
            Assert.Equal(HREF, epubMetadataLink.Href);
            Assert.Null(epubMetadataLink.MediaType);
            Assert.Equal(Properties, epubMetadataLink.Properties);
            Assert.Equal(REFINES, epubMetadataLink.Refines);
            Assert.Equal(Relationships, epubMetadataLink.Relationships);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataLink instance with null properties parameter should succeed")]
        public void ConstructorWithNullPropertiesTest()
        {
            EpubMetadataLink epubMetadataLink = new(ID, HREF, MEDIA_TYPE, null, REFINES, Relationships);
            Assert.Equal(ID, epubMetadataLink.Id);
            Assert.Equal(HREF, epubMetadataLink.Href);
            Assert.Equal(MEDIA_TYPE, epubMetadataLink.MediaType);
            Assert.Null(epubMetadataLink.Properties);
            Assert.Equal(REFINES, epubMetadataLink.Refines);
            Assert.Equal(Relationships, epubMetadataLink.Relationships);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataLink instance with null refines parameter should succeed")]
        public void ConstructorWithNullRefinesTest()
        {
            EpubMetadataLink epubMetadataLink = new(ID, HREF, MEDIA_TYPE, Properties, null, Relationships);
            Assert.Equal(ID, epubMetadataLink.Id);
            Assert.Equal(HREF, epubMetadataLink.Href);
            Assert.Equal(MEDIA_TYPE, epubMetadataLink.MediaType);
            Assert.Equal(Properties, epubMetadataLink.Properties);
            Assert.Null(epubMetadataLink.Refines);
            Assert.Equal(Relationships, epubMetadataLink.Relationships);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataLink instance with null relationships parameter should succeed")]
        public void ConstructorWithNullRelationshipsTest()
        {
            EpubMetadataLink epubMetadataLink = new(ID, HREF, MEDIA_TYPE, Properties, REFINES, null);
            Assert.Equal(ID, epubMetadataLink.Id);
            Assert.Equal(HREF, epubMetadataLink.Href);
            Assert.Equal(MEDIA_TYPE, epubMetadataLink.MediaType);
            Assert.Equal(Properties, epubMetadataLink.Properties);
            Assert.Equal(REFINES, epubMetadataLink.Refines);
            Assert.Equal(new List<EpubMetadataLinkRelationship>(), epubMetadataLink.Relationships);
        }
    }
}
