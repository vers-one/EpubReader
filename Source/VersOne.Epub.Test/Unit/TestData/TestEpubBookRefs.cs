using VersOne.Epub.Test.Unit.Mocks;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubBookRefs
    {
        public static EpubBookRef CreateMinimalTestEpubBookRef(TestZipFile epubFile, string epubFilePath)
        {
            return new
            (
                epubFile: epubFile,
                filePath: epubFilePath,
                title: String.Empty,
                author: String.Empty,
                authorList: new List<string>(),
                description: null,
                schema: TestEpubSchemas.CreateMinimalTestEpubSchema(),
                content: TestEpubContentRef.CreateMinimalTestEpubContentRefWithNavigation()
            );
        }

        public static EpubBookRef CreateFullTestEpubBookRef(TestZipFile epubFile, string? epubFilePath)
        {
            return new
            (
                epubFile: epubFile,
                filePath: epubFilePath,
                title: BOOK_TITLE,
                author: BOOK_AUTHOR,
                authorList: new List<string> { BOOK_AUTHOR },
                description: BOOK_DESCRIPTION,
                schema: TestEpubSchemas.CreateFullTestEpubSchema(),
                content: TestEpubContentRef.CreateFullTestEpubContentRef()
            );
        }
    }
}
