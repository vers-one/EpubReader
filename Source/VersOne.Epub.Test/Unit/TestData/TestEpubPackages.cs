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
                uniqueIdentifier: null,
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
                uniqueIdentifier: null,
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
                uniqueIdentifier: BOOK_IDENTIFIER_ID,
                epubVersion: EpubVersion.EPUB_3,
                metadata: new EpubMetadata
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
