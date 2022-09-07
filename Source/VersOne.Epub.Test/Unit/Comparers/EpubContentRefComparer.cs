using VersOne.Epub.Test.Unit.TestUtils;

namespace VersOne.Epub.Test.Unit.Comparers
{
    internal static class EpubContentRefComparer
    {
        public static void CompareEpubContentRefs(EpubContentRef expected, EpubContentRef actual)
        {
            CompareEpubContentFileRefs(expected.Cover, actual.Cover);
            CompareEpubContentFileRefs(expected.NavigationHtmlFile, actual.NavigationHtmlFile);
            AssertUtils.DictionariesEqual(expected.Html, actual.Html, CompareEpubContentFileRefs);
            AssertUtils.DictionariesEqual(expected.Css, actual.Css, CompareEpubContentFileRefs);
            AssertUtils.DictionariesEqual(expected.Images, actual.Images, CompareEpubContentFileRefs);
            AssertUtils.DictionariesEqual(expected.Fonts, actual.Fonts, CompareEpubContentFileRefs);
            AssertUtils.DictionariesEqual(expected.AllFiles, actual.AllFiles, CompareEpubContentFileRefs);
        }

        private static void CompareEpubContentFileRefs(EpubContentFileRef expected, EpubContentFileRef actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.GetType(), actual.GetType());
                Assert.Equal(expected.FileName, actual.FileName);
                Assert.Equal(expected.FilePathInEpubArchive, actual.FilePathInEpubArchive);
                Assert.Equal(expected.ContentType, actual.ContentType);
                Assert.Equal(expected.ContentMimeType, actual.ContentMimeType);
            }
        }
    }
}
