using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubBooks
    {
        public static EpubBook CreateMinimalTestEpubBook(string epubFilePath)
        {
            return new
            (
                filePath: epubFilePath,
                title: String.Empty,
                author: String.Empty,
                authorList: new List<string>(),
                description: null,
                coverImage: null,
                readingOrder: new List<EpubLocalTextContentFile>(),
                navigation: new List<EpubNavigationItem>(),
                schema: TestEpubSchemas.CreateMinimalTestEpubSchema(),
                content: TestEpubContent.CreateMinimalTestEpubContentWithNavigation()
            );
        }

        public static EpubBook CreateMinimalTestEpub2BookWithoutNavigation(string epubFilePath)
        {
            return new
            (
                filePath: epubFilePath,
                title: String.Empty,
                author: String.Empty,
                authorList: new List<string>(),
                description: null,
                coverImage: null,
                readingOrder: new List<EpubLocalTextContentFile>(),
                navigation: null,
                schema: TestEpubSchemas.CreateMinimalTestEpub2SchemaWithoutNavigation(),
                content: new EpubContent()
            );
        }

        public static EpubBook CreateFullTestEpubBook(string? epubFilePath, bool populateRemoteFilesContents)
        {
            return new
            (
                filePath: epubFilePath,
                title: BOOK_TITLE,
                author: BOOK_AUTHOR,
                authorList: new List<string> { BOOK_AUTHOR },
                description: BOOK_DESCRIPTION,
                coverImage: TestEpubFiles.COVER_FILE_CONTENT,
                readingOrder: TestEpubReadingOrder.CreateFullTestEpubReadingOrder(),
                navigation: TestEpubNavigation.CreateFullTestEpubNavigation(),
                schema: TestEpubSchemas.CreateFullTestEpubSchema(),
                content: TestEpubContent.CreateFullTestEpubContent(populateRemoteFilesContents)
            );
        }
    }
}
