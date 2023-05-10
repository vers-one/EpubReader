using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.TestData;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Package
{
    public class EpubPackageTests
    {
        private const EpubVersion EPUB_VERSION = EpubVersion.EPUB_3_1;
        private const string PACKAGE_ID = "package-id";
        private const EpubTextDirection PACKAGE_TEXT_DIRECTION = EpubTextDirection.LEFT_TO_RIGHT;
        private const string PACKAGE_PREFIX = "foaf: http://xmlns.com/foaf/spec/";
        private const string PACKAGE_LANGUAGE = "en";

        private static EpubMetadata Metadata =>
            new
            (
                titles: new List<EpubMetadataTitle>()
                {
                    new EpubMetadataTitle
                    (
                        title: BOOK_TITLE
                    )
                },
                creators: new List<EpubMetadataCreator>()
                {
                    new EpubMetadataCreator
                    (
                        creator: BOOK_AUTHOR
                    )
                },
                descriptions: new List<EpubMetadataDescription>()
                {
                    new EpubMetadataDescription
                    (
                        description: BOOK_DESCRIPTION
                    )
                },
                identifiers: new List<EpubMetadataIdentifier>()
                {
                    new EpubMetadataIdentifier
                    (
                        identifier: BOOK_UID,
                        id: BOOK_IDENTIFIER_ID
                    )
                }
            );

        private static EpubManifest Manifest => TestEpubManifests.CreateFullTestEpubManifest();

        private static EpubSpine Spine =>
            new
            (
                toc: "ncx",
                items: new List<EpubSpineItemRef>()
                {
                    new EpubSpineItemRef
                    (
                        id: "itemref-1",
                        idRef: "item-1",
                        isLinear: true
                    ),
                    new EpubSpineItemRef
                    (
                        id: "itemref-2",
                        idRef: "item-2",
                        isLinear: true
                    )
                }
            );

        private static EpubGuide Guide =>
            new
            (
                items: new List<EpubGuideReference>()
                {
                    new EpubGuideReference
                    (
                        type: "toc",
                        title: "Contents",
                        href: "toc.html"
                    )
                }
            );

        private static List<EpubCollection> Collections =>
            new()
            {
                new EpubCollection
                (
                    role: COLLECTION_ROLE
                )
            };

        [Fact(DisplayName = "Constructing a EpubPackage instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubPackage epubPackage =
                new(BOOK_IDENTIFIER_ID, EPUB_VERSION, Metadata, Manifest, Spine, Guide, Collections, PACKAGE_ID, PACKAGE_TEXT_DIRECTION, PACKAGE_PREFIX, PACKAGE_LANGUAGE);
            Assert.Equal(BOOK_IDENTIFIER_ID, epubPackage.UniqueIdentifier);
            Assert.Equal(EPUB_VERSION, epubPackage.EpubVersion);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubPackage.Metadata);
            EpubPackageComparer.CompareEpubManifests(Manifest, epubPackage.Manifest);
            EpubPackageComparer.CompareEpubSpines(Spine, epubPackage.Spine);
            EpubPackageComparer.CompareEpubGuides(Guide, epubPackage.Guide);
            EpubPackageComparer.CompareEpubCollectionLists(Collections, epubPackage.Collections);
            Assert.Equal(PACKAGE_ID, epubPackage.Id);
            Assert.Equal(PACKAGE_TEXT_DIRECTION, epubPackage.TextDirection);
            Assert.Equal(PACKAGE_PREFIX, epubPackage.Prefix);
            Assert.Equal(PACKAGE_LANGUAGE, epubPackage.Language);
        }

        [Fact(DisplayName = "Constructing a EpubPackage instance with null uniqueIdentifier parameter should succeed")]
        public void ConstructorWithNullUniqueIdentifierTest()
        {
            EpubPackage epubPackage =
                new(null, EPUB_VERSION, Metadata, Manifest, Spine, Guide, Collections, PACKAGE_ID, PACKAGE_TEXT_DIRECTION, PACKAGE_PREFIX, PACKAGE_LANGUAGE);
            Assert.Null(epubPackage.UniqueIdentifier);
            Assert.Equal(EPUB_VERSION, epubPackage.EpubVersion);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubPackage.Metadata);
            EpubPackageComparer.CompareEpubManifests(Manifest, epubPackage.Manifest);
            EpubPackageComparer.CompareEpubSpines(Spine, epubPackage.Spine);
            EpubPackageComparer.CompareEpubGuides(Guide, epubPackage.Guide);
            EpubPackageComparer.CompareEpubCollectionLists(Collections, epubPackage.Collections);
            Assert.Equal(PACKAGE_ID, epubPackage.Id);
            Assert.Equal(PACKAGE_TEXT_DIRECTION, epubPackage.TextDirection);
            Assert.Equal(PACKAGE_PREFIX, epubPackage.Prefix);
            Assert.Equal(PACKAGE_LANGUAGE, epubPackage.Language);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if metadata parameter is null")]
        public void ConstructorWithNullMetadataTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubPackage(BOOK_IDENTIFIER_ID, EPUB_VERSION, null!, Manifest, Spine, Guide, Collections, PACKAGE_ID, PACKAGE_TEXT_DIRECTION, PACKAGE_PREFIX, PACKAGE_LANGUAGE));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if manifest parameter is null")]
        public void ConstructorWithNullManifestTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubPackage(BOOK_IDENTIFIER_ID, EPUB_VERSION, Metadata, null!, Spine, Guide, Collections, PACKAGE_ID, PACKAGE_TEXT_DIRECTION, PACKAGE_PREFIX, PACKAGE_LANGUAGE));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if spine parameter is null")]
        public void ConstructorWithNullSpineTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubPackage(BOOK_IDENTIFIER_ID, EPUB_VERSION, Metadata, Manifest, null!, Guide, Collections, PACKAGE_ID, PACKAGE_TEXT_DIRECTION, PACKAGE_PREFIX, PACKAGE_LANGUAGE));
        }

        [Fact(DisplayName = "Constructing a EpubPackage instance with null guide parameter should succeed")]
        public void ConstructorWithNullGuideTest()
        {
            EpubPackage epubPackage =
                new(BOOK_IDENTIFIER_ID, EPUB_VERSION, Metadata, Manifest, Spine, null, Collections, PACKAGE_ID, PACKAGE_TEXT_DIRECTION, PACKAGE_PREFIX, PACKAGE_LANGUAGE);
            Assert.Equal(BOOK_IDENTIFIER_ID, epubPackage.UniqueIdentifier);
            Assert.Equal(EPUB_VERSION, epubPackage.EpubVersion);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubPackage.Metadata);
            EpubPackageComparer.CompareEpubManifests(Manifest, epubPackage.Manifest);
            EpubPackageComparer.CompareEpubSpines(Spine, epubPackage.Spine);
            EpubPackageComparer.CompareEpubGuides(null, epubPackage.Guide);
            EpubPackageComparer.CompareEpubCollectionLists(Collections, epubPackage.Collections);
            Assert.Equal(PACKAGE_ID, epubPackage.Id);
            Assert.Equal(PACKAGE_TEXT_DIRECTION, epubPackage.TextDirection);
            Assert.Equal(PACKAGE_PREFIX, epubPackage.Prefix);
            Assert.Equal(PACKAGE_LANGUAGE, epubPackage.Language);
        }

        [Fact(DisplayName = "Constructing a EpubPackage instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubPackage epubPackage =
                new(BOOK_IDENTIFIER_ID, EPUB_VERSION, Metadata, Manifest, Spine, Guide, Collections, null, PACKAGE_TEXT_DIRECTION, PACKAGE_PREFIX, PACKAGE_LANGUAGE);
            Assert.Equal(BOOK_IDENTIFIER_ID, epubPackage.UniqueIdentifier);
            Assert.Equal(EPUB_VERSION, epubPackage.EpubVersion);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubPackage.Metadata);
            EpubPackageComparer.CompareEpubManifests(Manifest, epubPackage.Manifest);
            EpubPackageComparer.CompareEpubSpines(Spine, epubPackage.Spine);
            EpubPackageComparer.CompareEpubGuides(Guide, epubPackage.Guide);
            EpubPackageComparer.CompareEpubCollectionLists(Collections, epubPackage.Collections);
            Assert.Null(epubPackage.Id);
            Assert.Equal(PACKAGE_TEXT_DIRECTION, epubPackage.TextDirection);
            Assert.Equal(PACKAGE_PREFIX, epubPackage.Prefix);
            Assert.Equal(PACKAGE_LANGUAGE, epubPackage.Language);
        }

        [Fact(DisplayName = "Constructing a EpubPackage instance with null textDirection parameter should succeed")]
        public void ConstructorWithNullTextDirectionTest()
        {
            EpubPackage epubPackage =
                new(BOOK_IDENTIFIER_ID, EPUB_VERSION, Metadata, Manifest, Spine, Guide, Collections, PACKAGE_ID, null, PACKAGE_PREFIX, PACKAGE_LANGUAGE);
            Assert.Equal(BOOK_IDENTIFIER_ID, epubPackage.UniqueIdentifier);
            Assert.Equal(EPUB_VERSION, epubPackage.EpubVersion);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubPackage.Metadata);
            EpubPackageComparer.CompareEpubManifests(Manifest, epubPackage.Manifest);
            EpubPackageComparer.CompareEpubSpines(Spine, epubPackage.Spine);
            EpubPackageComparer.CompareEpubGuides(Guide, epubPackage.Guide);
            EpubPackageComparer.CompareEpubCollectionLists(Collections, epubPackage.Collections);
            Assert.Equal(PACKAGE_ID, epubPackage.Id);
            Assert.Null(epubPackage.TextDirection);
            Assert.Equal(PACKAGE_PREFIX, epubPackage.Prefix);
            Assert.Equal(PACKAGE_LANGUAGE, epubPackage.Language);
        }

        [Fact(DisplayName = "Constructing a EpubPackage instance with null prefix parameter should succeed")]
        public void ConstructorWithNullPrefixTest()
        {
            EpubPackage epubPackage =
                new(BOOK_IDENTIFIER_ID, EPUB_VERSION, Metadata, Manifest, Spine, Guide, Collections, PACKAGE_ID, PACKAGE_TEXT_DIRECTION, null, PACKAGE_LANGUAGE);
            Assert.Equal(BOOK_IDENTIFIER_ID, epubPackage.UniqueIdentifier);
            Assert.Equal(EPUB_VERSION, epubPackage.EpubVersion);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubPackage.Metadata);
            EpubPackageComparer.CompareEpubManifests(Manifest, epubPackage.Manifest);
            EpubPackageComparer.CompareEpubSpines(Spine, epubPackage.Spine);
            EpubPackageComparer.CompareEpubGuides(Guide, epubPackage.Guide);
            EpubPackageComparer.CompareEpubCollectionLists(Collections, epubPackage.Collections);
            Assert.Equal(PACKAGE_ID, epubPackage.Id);
            Assert.Equal(PACKAGE_TEXT_DIRECTION, epubPackage.TextDirection);
            Assert.Null(epubPackage.Prefix);
            Assert.Equal(PACKAGE_LANGUAGE, epubPackage.Language);
        }

        [Fact(DisplayName = "Constructing a EpubPackage instance with null language parameter should succeed")]
        public void ConstructorWithNullLanguageTest()
        {
            EpubPackage epubPackage =
                new(BOOK_IDENTIFIER_ID, EPUB_VERSION, Metadata, Manifest, Spine, Guide, Collections, PACKAGE_ID, PACKAGE_TEXT_DIRECTION, PACKAGE_PREFIX, null);
            Assert.Equal(BOOK_IDENTIFIER_ID, epubPackage.UniqueIdentifier);
            Assert.Equal(EPUB_VERSION, epubPackage.EpubVersion);
            EpubMetadataComparer.CompareEpubMetadatas(Metadata, epubPackage.Metadata);
            EpubPackageComparer.CompareEpubManifests(Manifest, epubPackage.Manifest);
            EpubPackageComparer.CompareEpubSpines(Spine, epubPackage.Spine);
            EpubPackageComparer.CompareEpubGuides(Guide, epubPackage.Guide);
            EpubPackageComparer.CompareEpubCollectionLists(Collections, epubPackage.Collections);
            Assert.Equal(PACKAGE_ID, epubPackage.Id);
            Assert.Equal(PACKAGE_TEXT_DIRECTION, epubPackage.TextDirection);
            Assert.Equal(PACKAGE_PREFIX, epubPackage.Prefix);
            Assert.Null(epubPackage.Language);
        }

        [Fact(DisplayName = "GetVersionString should return a string representation of the EpubVersion property")]
        public void GetVersionStringTest()
        {
            EpubPackage epubPackage = new
            (
                uniqueIdentifier: BOOK_IDENTIFIER_ID,
                epubVersion: EPUB_VERSION,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest(),
                spine: new EpubSpine(),
                guide: null,
                collections: null,
                id: null,
                textDirection: null,
                prefix: null,
                language: null
            );
            Assert.Equal("3.1", epubPackage.GetVersionString());
        }
    }
}
