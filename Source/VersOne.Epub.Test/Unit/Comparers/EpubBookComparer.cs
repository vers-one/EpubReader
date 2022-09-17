using VersOne.Epub.Test.Unit.TestUtils;

namespace VersOne.Epub.Test.Unit.Comparers
{
    internal static class EpubBookComparer
    {
        public static void CompareEpubBooks(EpubBook expected, EpubBook actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.FilePath, actual.FilePath);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Author, actual.Author);
            Assert.Equal(expected.AuthorList, actual.AuthorList);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.CoverImage, actual.CoverImage);
            AssertUtils.CollectionsEqual(expected.ReadingOrder, actual.ReadingOrder, EpubContentComparer.CompareEpubTextContentFiles);
            EpubNavigationItemComparer.CompareNavigationItemLists(expected.Navigation, actual.Navigation);
            EpubContentComparer.CompareEpubContents(expected.Content, actual.Content);
            EpubSchemaComparer.CompareEpubSchemas(expected.Schema, actual.Schema);
        }
    }
}
