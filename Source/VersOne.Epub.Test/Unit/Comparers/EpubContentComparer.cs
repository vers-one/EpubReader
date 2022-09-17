using VersOne.Epub.Test.Unit.TestUtils;

namespace VersOne.Epub.Test.Unit.Comparers
{
    internal static class EpubContentComparer
    {
        public static void CompareEpubContents(EpubContent expected, EpubContent actual)
        {
            CompareEpubContentFiles(expected.Cover, actual.Cover);
            CompareEpubContentFiles(expected.NavigationHtmlFile, actual.NavigationHtmlFile);
            AssertUtils.DictionariesEqual(expected.Html, actual.Html, CompareEpubTextContentFiles);
            AssertUtils.DictionariesEqual(expected.Css, actual.Css, CompareEpubTextContentFiles);
            AssertUtils.DictionariesEqual(expected.Images, actual.Images, CompareEpubByteContentFiles);
            AssertUtils.DictionariesEqual(expected.Fonts, actual.Fonts, CompareEpubByteContentFiles);
            AssertUtils.DictionariesEqual(expected.AllFiles, actual.AllFiles, CompareEpubContentFilesWithContent);
        }

        public static void CompareEpubTextContentFiles(EpubTextContentFile expected, EpubTextContentFile actual)
        {
            CompareEpubContentFiles(expected, actual);
            Assert.Equal(expected.Content, actual.Content);
        }

        public static void CompareEpubByteContentFiles(EpubByteContentFile expected, EpubByteContentFile actual)
        {
            CompareEpubContentFiles(expected, actual);
            Assert.Equal(expected.Content, actual.Content);
        }

        private static void CompareEpubContentFilesWithContent(EpubContentFile expected, EpubContentFile actual)
        {
            if (expected is EpubTextContentFile)
            {
                CompareEpubTextContentFiles(expected as EpubTextContentFile, actual as EpubTextContentFile);
            }
            else if (expected is EpubByteContentFile)
            {
                CompareEpubByteContentFiles(expected as EpubByteContentFile, actual as EpubByteContentFile);
            }
        }

        private static void CompareEpubContentFiles(EpubContentFile expected, EpubContentFile actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.FileName, actual.FileName);
                Assert.Equal(expected.FilePathInEpubArchive, actual.FilePathInEpubArchive);
                Assert.Equal(expected.ContentType, actual.ContentType);
                Assert.Equal(expected.ContentMimeType, actual.ContentMimeType);
            }
        }
    }
}
