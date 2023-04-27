using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubNavigation
    {
        public static List<EpubNavigationItem> CreateFullTestEpubNavigation()
        {
            return new()
            {
                new EpubNavigationItem
                (
                    type: EpubNavigationItemType.LINK,
                    title: "Chapter 1",
                    link: new EpubNavigationItemLink(CHAPTER1_FILE_NAME, CONTENT_DIRECTORY_PATH),
                    htmlContentFile: TestEpubContent.Chapter1File,
                    nestedItems: new List<EpubNavigationItem>()
                ),
                new EpubNavigationItem
                (
                    type: EpubNavigationItemType.LINK,
                    title: "Chapter 2",
                    link: new EpubNavigationItemLink(CHAPTER2_FILE_NAME, CONTENT_DIRECTORY_PATH),
                    htmlContentFile: TestEpubContent.Chapter2File,
                    nestedItems: new List<EpubNavigationItem>()
                )
            };
        }
    }
}
