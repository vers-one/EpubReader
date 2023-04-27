using VersOne.Epub.Schema;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubPackages
    {
        public static EpubPackage CreateMinimalTestEpubPackage()
        {
            return new
            (
                epubVersion: EpubVersion.EPUB_3,
                metadata: new EpubMetadata(),
                manifest: TestEpubManifests.CreateMinimalTestEpubManifest(),
                spine: new EpubSpine(),
                guide: null
            );
        }

        public static EpubPackage CreateMinimalTestEpub2PackageWithoutNavigation()
        {
            return new
            (
                epubVersion: EpubVersion.EPUB_2,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest(),
                spine: new EpubSpine(),
                guide: null
            );
        }

        public static EpubPackage CreateFullTestEpubPackage()
        {
            return new
            (
                epubVersion: EpubVersion.EPUB_3,
                metadata: new EpubMetadata
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
                    subjects: new List<string>(),
                    description: BOOK_DESCRIPTION,
                    publishers: new List<string>(),
                    contributors: new List<EpubMetadataContributor>(),
                    dates: new List<EpubMetadataDate>(),
                    types: new List<string>(),
                    formats: new List<string>(),
                    identifiers: new List<EpubMetadataIdentifier>(),
                    sources: new List<string>(),
                    languages: new List<string>(),
                    relations: new List<string>(),
                    coverages: new List<string>(),
                    rights: new List<string>(),
                    links: new List<EpubMetadataLink>(),
                    metaItems: new List<EpubMetadataMeta>()
                ),
                manifest: TestEpubManifests.CreateFullTestEpubManifest(),
                spine: new EpubSpine
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
                ),
                guide: null
            );
        }
    }
}
