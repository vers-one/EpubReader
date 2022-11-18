using VersOne.Epub.Schema;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpub2Ncxes
    {
        public static Epub2Ncx CreateFullTestEpub2Ncx()
        {
            return new Epub2Ncx
            (
                filePath: NCX_FILE_PATH,
                head: new Epub2NcxHead
                (
                    items: new List<Epub2NcxHeadMeta>()
                    {
                        new Epub2NcxHeadMeta
                        (
                            name: "dtb:uid",
                            content: BOOK_UID
                        )
                    }
                ),
                docTitle: BOOK_TITLE,
                docAuthors: new List<string>()
                {
                    BOOK_AUTHOR
                },
                navMap: new Epub2NcxNavigationMap
                (
                    items: new List<Epub2NcxNavigationPoint>()
                    {
                        new Epub2NcxNavigationPoint
                        (
                            id: "navpoint-1",
                            navigationLabels: new List<Epub2NcxNavigationLabel>()
                            {
                                new Epub2NcxNavigationLabel
                                (
                                    text: "Chapter 1"
                                )
                            },
                            content: new Epub2NcxContent
                            (
                                source: CHAPTER1_FILE_NAME
                            )
                        ),
                        new Epub2NcxNavigationPoint
                        (
                            id: "navpoint-2",
                            navigationLabels: new List<Epub2NcxNavigationLabel>()
                            {
                                new Epub2NcxNavigationLabel
                                (
                                    text: "Chapter 2"
                                )
                            },
                            content: new Epub2NcxContent
                            (
                                source: CHAPTER2_FILE_NAME
                            )
                        )
                    }
                )
            );
        }
    }
}
