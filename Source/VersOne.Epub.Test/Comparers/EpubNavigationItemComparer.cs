namespace VersOne.Epub.Test.Comparers
{
    internal static class EpubNavigationItemComparer
    {
        public static void CompareNavigationItemLists(List<EpubNavigationItem> expected, List<EpubNavigationItem> actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                CollectionComparer.CompareCollections(expected, actual, CompareNavigationItems);
            }
        }

        public static void CompareNavigationItemLinks(EpubNavigationItemLink expected, EpubNavigationItemLink actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.ContentFileName, actual.ContentFileName);
                Assert.Equal(expected.ContentFilePathInEpubArchive, actual.ContentFilePathInEpubArchive);
                Assert.Equal(expected.Anchor, actual.Anchor);
            }
        }

        private static void CompareNavigationItems(EpubNavigationItem expected, EpubNavigationItem actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Title, actual.Title);
            CompareNavigationItemLinks(expected.Link, actual.Link);
            EpubContentComparer.CompareEpubLocalTextContentFiles(expected.HtmlContentFile, actual.HtmlContentFile);
            CompareNavigationItemLists(expected.NestedItems, actual.NestedItems);
        }
    }
}
