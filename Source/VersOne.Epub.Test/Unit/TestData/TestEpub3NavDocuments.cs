using VersOne.Epub.Schema;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpub3NavDocuments
    {
        public static Epub3NavDocument CreateFullTestEpub3NavDocument()
        {
            return new Epub3NavDocument
            (
                filePath: NAV_FILE_PATH,
                navs: new List<Epub3Nav>()
                {
                    new Epub3Nav
                    (
                        type: Epub3StructuralSemanticsProperty.TOC,
                        ol: new Epub3NavOl
                        (
                            lis: new List<Epub3NavLi>()
                            {
                                new Epub3NavLi
                                (
                                    anchor: new Epub3NavAnchor
                                    (
                                        href: CHAPTER1_FILE_NAME,
                                        text: "Chapter 1"
                                    )
                                ),
                                new Epub3NavLi
                                (
                                    anchor: new Epub3NavAnchor
                                    (
                                        href: CHAPTER2_FILE_NAME,
                                        text: "Chapter 2"
                                    )
                                )
                            }
                        )
                    )
                }
            );
        }
    }
}
