namespace VersOne.Epub.Test.Comparers
{
    internal static class EpubContentComparer
    {
        public static void CompareEpubContents(EpubContent expected, EpubContent actual)
        {
            CompareEpubLocalByteContentFiles(expected.Cover, actual.Cover);
            CompareEpubLocalTextContentFiles(expected.NavigationHtmlFile, actual.NavigationHtmlFile);
            CompareContentCollections(expected.Html, actual.Html, CompareEpubLocalTextContentFiles, CompareEpubRemoteTextContentFiles);
            CompareContentCollections(expected.Css, actual.Css, CompareEpubLocalTextContentFiles, CompareEpubRemoteTextContentFiles);
            CompareContentCollections(expected.Images, actual.Images, CompareEpubLocalByteContentFiles, CompareEpubRemoteByteContentFiles);
            CompareContentCollections(expected.Fonts, actual.Fonts, CompareEpubLocalByteContentFiles, CompareEpubRemoteByteContentFiles);
            CompareContentCollections(expected.AllFiles, actual.AllFiles, CompareLocalEpubContentFilesWithContent, CompareRemoteEpubContentFilesWithContent);
        }

        public static void CompareEpubLocalTextContentFileLists(List<EpubLocalTextContentFile> expected, List<EpubLocalTextContentFile> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareEpubLocalTextContentFiles);
        }

        public static void CompareEpubLocalTextContentFiles(EpubLocalTextContentFile? expected, EpubLocalTextContentFile? actual)
        {
            CompareEpubLocalContentFiles(expected, actual);
            if (expected != null)
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Content, actual.Content);
            }
        }

        public static void CompareEpubLocalByteContentFiles(EpubLocalByteContentFile? expected, EpubLocalByteContentFile? actual)
        {
            CompareEpubLocalContentFiles(expected, actual);
            if (expected != null)
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Content, actual.Content);
            }
        }

        public static void CompareEpubRemoteTextContentFiles(EpubRemoteTextContentFile? expected, EpubRemoteTextContentFile? actual)
        {
            CompareEpubRemoteContentFiles(expected, actual);
            if (expected != null)
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Content, actual.Content);
            }
        }

        public static void CompareEpubRemoteByteContentFiles(EpubRemoteByteContentFile? expected, EpubRemoteByteContentFile? actual)
        {
            CompareEpubRemoteContentFiles(expected, actual);
            if (expected != null)
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Content, actual.Content);
            }
        }

        private static void CompareEpubContentFiles(EpubContentFile? expected, EpubContentFile? actual)
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

        private static void CompareContentCollections<TLocalContentFile, TRemoteContentFile>(
            EpubContentCollection<TLocalContentFile, TRemoteContentFile> expected,
            EpubContentCollection<TLocalContentFile, TRemoteContentFile> actual,
            Action<TLocalContentFile, TLocalContentFile> localContentFileComprarer,
            Action<TRemoteContentFile, TRemoteContentFile> remoteContentFileComparer)
            where TLocalContentFile : EpubLocalContentFile
            where TRemoteContentFile : EpubRemoteContentFile
        {
            CollectionComparer.CompareDictionaries(expected.Local, actual.Local, localContentFileComprarer);
            CollectionComparer.CompareDictionaries(expected.Remote, actual.Remote, remoteContentFileComparer);
        }

        private static void CompareLocalEpubContentFilesWithContent(EpubLocalContentFile? expected, EpubLocalContentFile? actual)
        {
            if (expected is EpubLocalTextContentFile)
            {
                CompareEpubLocalTextContentFiles(expected as EpubLocalTextContentFile, actual as EpubLocalTextContentFile);
            }
            else if (expected is EpubLocalByteContentFile)
            {
                CompareEpubLocalByteContentFiles(expected as EpubLocalByteContentFile, actual as EpubLocalByteContentFile);
            }
        }

        private static void CompareRemoteEpubContentFilesWithContent(EpubRemoteContentFile? expected, EpubRemoteContentFile? actual)
        {
            if (expected is EpubRemoteTextContentFile)
            {
                CompareEpubRemoteTextContentFiles(expected as EpubRemoteTextContentFile, actual as EpubRemoteTextContentFile);
            }
            else if (expected is EpubRemoteByteContentFile)
            {
                CompareEpubRemoteByteContentFiles(expected as EpubRemoteByteContentFile, actual as EpubRemoteByteContentFile);
            }
        }

        private static void CompareEpubLocalContentFiles(EpubLocalContentFile? expected, EpubLocalContentFile? actual)
        {
            CompareEpubContentFiles(expected, actual);
            if (expected != null)
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.FilePath, actual.FilePath);
            }
        }

        private static void CompareEpubRemoteContentFiles(EpubRemoteContentFile? expected, EpubRemoteContentFile? actual)
        {
            CompareEpubContentFiles(expected, actual);
            if (expected != null)
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Url, actual.Url);
            }
        }
    }
}
