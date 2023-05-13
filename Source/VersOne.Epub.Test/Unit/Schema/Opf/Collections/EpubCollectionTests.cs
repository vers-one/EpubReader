using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Collections
{
    public class EpubCollectionTests
    {
        private const string ROLE = "http://example.org/roles/group";
        private const string ID = "";
        private const EpubTextDirection TEXT_DIRECTION = EpubTextDirection.LEFT_TO_RIGHT;
        private const string LANGUAGE = "en";

        private static EpubMetadata Metadata =>
            new
            (
                titles: new List<EpubMetadataTitle>()
                {
                    new EpubMetadataTitle
                    (
                        title: "Test title for outer collection",
                        id: "collection-outer-title",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    )
                }
            );

        private static List<EpubCollection> NestedCollections =>
            new()
            {
                new EpubCollection
                (
                    role: "http://example.org/roles/unit",
                    metadata: new EpubMetadata
                    (
                        titles: new List<EpubMetadataTitle>()
                        {
                            new EpubMetadataTitle
                            (
                                title: "Test title for inner collection",
                                id: "collection-inner-title",
                                textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                                language: "is"
                            )
                        }
                    ),
                    nestedCollections: new List<EpubCollection>(),
                    links: new List<EpubMetadataLink>(),
                    id: "collection-inner",
                    textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                    language: "is"
                )
            };

        private static List<EpubMetadataLink> Links =>
            new()
            {
                new EpubMetadataLink
                (
                    href: "https://example.com/onix/123",
                    id: "collection-outer-link",
                    mediaType: "application/xml",
                    properties: new List<EpubMetadataLinkProperty>()
                    {
                        EpubMetadataLinkProperty.ONIX
                    },
                    relationships: new List<EpubMetadataLinkRelationship>()
                    {
                        EpubMetadataLinkRelationship.RECORD,
                        EpubMetadataLinkRelationship.ONIX_RECORD
                    },
                    hrefLanguage: null
                )
            };

        [Fact(DisplayName = "Constructing a EpubCollection instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubCollection epubCollection = new(ROLE, Metadata, NestedCollections, Links, ID, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(ROLE, epubCollection.Role);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubCollection.Metadata);
            EpubPackageComparer.CompareEpubCollectionLists(NestedCollections, epubCollection.NestedCollections);
            EpubMetadataComparer.CompareEpubMetadataLinkLists(Links, epubCollection.Links);
            Assert.Equal(ID, epubCollection.Id);
            Assert.Equal(TEXT_DIRECTION, epubCollection.TextDirection);
            Assert.Equal(LANGUAGE, epubCollection.Language);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contributor parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubCollection(null!, Metadata, NestedCollections, Links, ID, TEXT_DIRECTION, LANGUAGE));
        }

        [Fact(DisplayName = "Constructing a EpubCollection instance with null metadata parameter should succeed")]
        public void ConstructorWithNullMetadataTest()
        {
            EpubCollection epubCollection = new(ROLE, null, NestedCollections, Links, ID, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(ROLE, epubCollection.Role);
            Assert.Null(epubCollection.Metadata);
            EpubPackageComparer.CompareEpubCollectionLists(NestedCollections, epubCollection.NestedCollections);
            EpubMetadataComparer.CompareEpubMetadataLinkLists(Links, epubCollection.Links);
            Assert.Equal(ID, epubCollection.Id);
            Assert.Equal(TEXT_DIRECTION, epubCollection.TextDirection);
            Assert.Equal(LANGUAGE, epubCollection.Language);
        }

        [Fact(DisplayName = "Constructing a EpubCollection instance with null nestedCollections parameter should succeed")]
        public void ConstructorWithNullNestedCollectionsTest()
        {
            EpubCollection epubCollection = new(ROLE, Metadata, null, Links, ID, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(ROLE, epubCollection.Role);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubCollection.Metadata);
            EpubPackageComparer.CompareEpubCollectionLists(new List<EpubCollection>(), epubCollection.NestedCollections);
            EpubMetadataComparer.CompareEpubMetadataLinkLists(Links, epubCollection.Links);
            Assert.Equal(ID, epubCollection.Id);
            Assert.Equal(TEXT_DIRECTION, epubCollection.TextDirection);
            Assert.Equal(LANGUAGE, epubCollection.Language);
        }

        [Fact(DisplayName = "Constructing a EpubCollection instance with null links parameter should succeed")]
        public void ConstructorWithNullLinksTest()
        {
            EpubCollection epubCollection = new(ROLE, Metadata, NestedCollections, null, ID, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(ROLE, epubCollection.Role);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubCollection.Metadata);
            EpubPackageComparer.CompareEpubCollectionLists(NestedCollections, epubCollection.NestedCollections);
            EpubMetadataComparer.CompareEpubMetadataLinkLists(new List<EpubMetadataLink>(), epubCollection.Links);
            Assert.Equal(ID, epubCollection.Id);
            Assert.Equal(TEXT_DIRECTION, epubCollection.TextDirection);
            Assert.Equal(LANGUAGE, epubCollection.Language);
        }

        [Fact(DisplayName = "Constructing a EpubCollection instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubCollection epubCollection = new(ROLE, Metadata, NestedCollections, Links, null, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(ROLE, epubCollection.Role);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubCollection.Metadata);
            EpubPackageComparer.CompareEpubCollectionLists(NestedCollections, epubCollection.NestedCollections);
            EpubMetadataComparer.CompareEpubMetadataLinkLists(Links, epubCollection.Links);
            Assert.Null(epubCollection.Id);
            Assert.Equal(TEXT_DIRECTION, epubCollection.TextDirection);
            Assert.Equal(LANGUAGE, epubCollection.Language);
        }

        [Fact(DisplayName = "Constructing a EpubCollection instance with null textDirection parameter should succeed")]
        public void ConstructorWithNullTextDirectionTest()
        {
            EpubCollection epubCollection = new(ROLE, Metadata, NestedCollections, Links, ID, null, LANGUAGE);
            Assert.Equal(ROLE, epubCollection.Role);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubCollection.Metadata);
            EpubPackageComparer.CompareEpubCollectionLists(NestedCollections, epubCollection.NestedCollections);
            EpubMetadataComparer.CompareEpubMetadataLinkLists(Links, epubCollection.Links);
            Assert.Equal(ID, epubCollection.Id);
            Assert.Null(epubCollection.TextDirection);
            Assert.Equal(LANGUAGE, epubCollection.Language);
        }

        [Fact(DisplayName = "Constructing a EpubCollection instance with null language parameter should succeed")]
        public void ConstructorWithNullLanguageTest()
        {
            EpubCollection epubCollection = new(ROLE, Metadata, NestedCollections, Links, ID, TEXT_DIRECTION, null);
            Assert.Equal(ROLE, epubCollection.Role);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubCollection.Metadata);
            EpubPackageComparer.CompareEpubCollectionLists(NestedCollections, epubCollection.NestedCollections);
            EpubMetadataComparer.CompareEpubMetadataLinkLists(Links, epubCollection.Links);
            Assert.Equal(ID, epubCollection.Id);
            Assert.Equal(TEXT_DIRECTION, epubCollection.TextDirection);
            Assert.Null(epubCollection.Language);
        }
    }
}
