namespace VersOne.Epub.Test.Comparers
{
    internal static class EpubContentRefComparer
    {
        public static void CompareEpubContentRefs(EpubContentRef expected, EpubContentRef actual)
        {
            CompareEpubLocalContentFileRefs(expected.Cover, actual.Cover);
            CompareEpubLocalContentFileRefs(expected.NavigationHtmlFile, actual.NavigationHtmlFile);
            CompareContentCollectionRefs(expected.Html, actual.Html);
            CompareContentCollectionRefs(expected.Css, actual.Css);
            CompareContentCollectionRefs(expected.Images, actual.Images);
            CompareContentCollectionRefs(expected.Fonts, actual.Fonts);
            CompareContentCollectionRefs(expected.AllFiles, actual.AllFiles);
        }

        public static void CompareEpubLocalContentFileRefs(EpubLocalContentFileRef? expected, EpubLocalContentFileRef? actual)
        {
            CompareEpubContentFileRefs(expected, actual);
            if (expected != null)
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.FilePath, actual.FilePath);
            }
        }

        public static void CompareEpubRemoteContentFileRefs(EpubRemoteContentFileRef? expected, EpubRemoteContentFileRef? actual)
        {
            CompareEpubContentFileRefs(expected, actual);
            if (expected != null)
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Url, actual.Url);
            }
        }

        private static void CompareContentCollectionRefs<TLocalContentFileRef, TRemoteContentFileRef>(
            EpubContentCollectionRef<TLocalContentFileRef, TRemoteContentFileRef> expected,
            EpubContentCollectionRef<TLocalContentFileRef, TRemoteContentFileRef> actual)
            where TLocalContentFileRef : EpubLocalContentFileRef
            where TRemoteContentFileRef : EpubRemoteContentFileRef
        {
            CollectionComparer.CompareDictionaries(expected.Local, actual.Local, CompareEpubLocalContentFileRefs);
            CollectionComparer.CompareDictionaries(expected.Remote, actual.Remote, CompareEpubRemoteContentFileRefs);
        }

        private static void CompareEpubContentFileRefs(EpubContentFileRef? expected, EpubContentFileRef? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.GetType(), actual.GetType());
                Assert.Equal(expected.Key, actual.Key);
                Assert.Equal(expected.ContentLocation, actual.ContentLocation);
                Assert.Equal(expected.ContentType, actual.ContentType);
                Assert.Equal(expected.ContentMimeType, actual.ContentMimeType);
            }
        }
    }
}
