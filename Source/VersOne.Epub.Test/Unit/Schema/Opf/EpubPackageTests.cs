using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.TestData;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubPackageTests
    {
        private const EpubVersion EPUB_VERSION = EpubVersion.EPUB_3_1;

        private static EpubMetadata Metadata =>
            new
            (
                titles: new List<string>()
                {
                    BOOK_TITLE
                },
                creators: new List<EpubMetadataCreator>()
                {
                    new EpubMetadataCreator
                    (
                        creator: BOOK_AUTHOR
                    )
                },
                description: BOOK_DESCRIPTION
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

        [Fact(DisplayName = "Constructing a EpubPackage instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubPackage epubPackage = new(EPUB_VERSION, Metadata, Manifest, Spine, Guide);
            Assert.Equal(EPUB_VERSION, epubPackage.EpubVersion);
            EpubPackageComparer.CompareEpubMetadatas(Metadata, epubPackage.Metadata);
            EpubPackageComparer.CompareEpubManifests(Manifest, epubPackage.Manifest);
            EpubPackageComparer.CompareEpubSpines(Spine, epubPackage.Spine);
            EpubPackageComparer.CompareEpubGuides(Guide, epubPackage.Guide);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if metadata parameter is null")]
        public void ConstructorWithNullMetadataTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubPackage(EPUB_VERSION, null!, Manifest, Spine, Guide));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if manifest parameter is null")]
        public void ConstructorWithNullManifestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubPackage(EPUB_VERSION, Metadata, null!, Spine, Guide));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if spine parameter is null")]
        public void ConstructorWithNullSpineTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubPackage(EPUB_VERSION, Metadata, Manifest, null!, Guide));
        }

        [Fact(DisplayName = "Constructing a EpubPackage instance with null guide parameter should succeed")]
        public void ConstructorWithNullGuideTest()
        {
            EpubPackage epubPackage = new(EPUB_VERSION, Metadata, Manifest, Spine, null);
            Assert.Equal(EPUB_VERSION, epubPackage.EpubVersion);
            EpubPackageComparer.CompareEpubMetadatas(Metadata, epubPackage.Metadata);
            EpubPackageComparer.CompareEpubManifests(Manifest, epubPackage.Manifest);
            EpubPackageComparer.CompareEpubSpines(Spine, epubPackage.Spine);
            EpubPackageComparer.CompareEpubGuides(null, epubPackage.Guide);
        }

        [Fact(DisplayName = "GetVersionString should return a string representation of the EpubVersion property")]
        public void GetVersionStringTest()
        {
            EpubPackage epubPackage = new
            (
                epubVersion: EPUB_VERSION,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest(),
                spine: new EpubSpine(),
                guide: null
            );
            Assert.Equal("3.1", epubPackage.GetVersionString());
        }
    }
}
