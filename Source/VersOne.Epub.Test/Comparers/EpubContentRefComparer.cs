namespace VersOne.Epub.Test.Comparers
{
    internal static class EpubContentRefComparer
    {
        public static void CompareEpubContentRefs(EpubContentRef expected, EpubContentRef actual)
        {
            CompareEpubContentFileRefs(expected.Cover, actual.Cover);
            CompareEpubContentFileRefs(expected.NavigationHtmlFile, actual.NavigationHtmlFile);
            CollectionComparer.CompareDictionaries(expected.Html, actual.Html, CompareEpubContentFileRefs);
            CollectionComparer.CompareDictionaries(expected.Css, actual.Css, CompareEpubContentFileRefs);
            CollectionComparer.CompareDictionaries(expected.Images, actual.Images, CompareEpubContentFileRefs);
            CollectionComparer.CompareDictionaries(expected.Fonts, actual.Fonts, CompareEpubContentFileRefs);
            CollectionComparer.CompareDictionaries(expected.AllFiles, actual.AllFiles, CompareEpubContentFileRefs);
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
                Assert.Equal(expected.Href, actual.Href);
                Assert.Equal(expected.ContentLocation, actual.ContentLocation);
                Assert.Equal(expected.ContentType, actual.ContentType);
                Assert.Equal(expected.ContentMimeType, actual.ContentMimeType);
            }
        }
    }
}
