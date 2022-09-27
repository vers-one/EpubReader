using VersOne.Epub.Internal;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;
using VersOne.Epub.Test.Unit.TestData;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class BookRefReaderTests
    {
        [Fact(DisplayName = "Opening a minimal EPUB book from a file synchronously should succeed")]
        public void OpenMinimalBookFromFileTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateMinimalTestEpubFile();
            TestFileSystem testFileSystem = new(EPUB_FILE_PATH, testEpubFile);
            EpubBookRef expectedEpubBookRef = TestEpubBookRefs.CreateMinimalTestEpubBookRef(testEpubFile, EPUB_FILE_PATH);
            EpubBookRef actualEpubBookRef = BookRefReader.OpenBook(EPUB_FILE_PATH, null, testFileSystem);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a file asynchronously should succeed")]
        public async void OpenBookFromFileAsyncTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            TestFileSystem testFileSystem = new(EPUB_FILE_PATH, testEpubFile);
            EpubBookRef expectedEpubBookRef = TestEpubBookRefs.CreateFullTestEpubBookRef(testEpubFile, EPUB_FILE_PATH);
            EpubBookRef actualEpubBookRef = await BookRefReader.OpenBookAsync(EPUB_FILE_PATH, null, testFileSystem);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a file synchronously should succeed")]
        public void OpenBookFromFileTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            TestFileSystem testFileSystem = new(EPUB_FILE_PATH, testEpubFile);
            EpubBookRef expectedEpubBookRef = TestEpubBookRefs.CreateFullTestEpubBookRef(testEpubFile, EPUB_FILE_PATH);
            EpubBookRef actualEpubBookRef = BookRefReader.OpenBook(EPUB_FILE_PATH, null, testFileSystem);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a stream synchronously should succeed")]
        public void OpenBookFromStreamTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            TestFileSystem testFileSystem = new(epubFileStream, testEpubFile);
            EpubBookRef expectedEpubBookRef = TestEpubBookRefs.CreateFullTestEpubBookRef(testEpubFile, null);
            EpubBookRef actualEpubBookRef = BookRefReader.OpenBook(epubFileStream, null, testFileSystem);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a stream asynchronously should succeed")]
        public async void OpenBookFromStreamAsyncTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            TestFileSystem testFileSystem = new(epubFileStream, testEpubFile);
            EpubBookRef expectedEpubBookRef = TestEpubBookRefs.CreateFullTestEpubBookRef(testEpubFile, null);
            EpubBookRef actualEpubBookRef = await BookRefReader.OpenBookAsync(epubFileStream, null, testFileSystem);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "OpenBook should throw FileNotFoundException if the specified file does not exist")]
        public void OpenBookFromFileWithMissingFileTest()
        {
            Assert.Throws<FileNotFoundException>(() => BookRefReader.OpenBook(EPUB_FILE_PATH, null, new TestFileSystem()));
        }

        [Fact(DisplayName = "OpenBookAsync should throw FileNotFoundException if the specified file does not exist")]
        public async void OpenBookFromFileAsyncWithMissingFileTest()
        {
            await Assert.ThrowsAsync<FileNotFoundException>(() => BookRefReader.OpenBookAsync(EPUB_FILE_PATH, null, new TestFileSystem()));
        }

        [Fact(DisplayName = "OpenBook should rethrow EPUB parsing exceptions")]
        public void OpenBookFromFileWithIncorrectEpubFileTest()
        {
            TestZipFile incorrectEpubFile = new();
            TestFileSystem testFileSystem = new(EPUB_FILE_PATH, incorrectEpubFile);
            Assert.Throws<EpubContainerException>(() => BookRefReader.OpenBook(EPUB_FILE_PATH, null, testFileSystem));
        }

        [Fact(DisplayName = "OpenBookAsync should rethrow EPUB parsing exceptions")]
        public async void OpenBookFromFileAsyncWithIncorrectEpubTest()
        {
            TestZipFile incorrectEpubFile = new();
            TestFileSystem testFileSystem = new(EPUB_FILE_PATH, incorrectEpubFile);
            await Assert.ThrowsAsync<EpubContainerException>(() => BookRefReader.OpenBookAsync(EPUB_FILE_PATH, null, testFileSystem));
        }
    }
}
