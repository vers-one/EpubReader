using VersOne.Epub.Internal;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;
using VersOne.Epub.Test.Unit.TestData;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class BookReaderTests
    {

        [Fact(DisplayName = "Reading a minimal EPUB book from a file synchronously should succeed")]
        public void ReadMinimalBookFromFileTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateMinimalTestEpubFile();
            TestFileSystem testFileSystem = new(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateMinimalTestEpubBook(EPUB_FILE_PATH);
            EpubBook actualEpubBook = BookReader.ReadBook(EPUB_FILE_PATH, null, testFileSystem);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a file synchronously should succeed")]
        public void ReadBookFromFileTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            TestFileSystem testFileSystem = new(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(EPUB_FILE_PATH);
            EpubBook actualEpubBook = BookReader.ReadBook(EPUB_FILE_PATH, null, testFileSystem);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a file asynchronously should succeed")]
        public async void ReadBookFromFileAsyncTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            TestFileSystem testFileSystem = new(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(EPUB_FILE_PATH);
            EpubBook actualEpubBook = await BookReader.ReadBookAsync(EPUB_FILE_PATH, null, testFileSystem);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a stream synchronously should succeed")]
        public void ReadBookFromStreamTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            TestFileSystem testFileSystem = new(epubFileStream, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(null);
            EpubBook actualEpubBook = BookReader.ReadBook(epubFileStream, null, testFileSystem);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a stream asynchronously should succeed")]
        public async void ReadBookFromStreamAsyncTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            TestFileSystem testFileSystem = new(epubFileStream, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(null);
            EpubBook actualEpubBook = await BookReader.ReadBookAsync(epubFileStream, null, testFileSystem);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "ReadBook should throw FileNotFoundException if the specified file does not exist")]
        public void ReadBookFromFileWithMissingFileTest()
        {
            Assert.Throws<FileNotFoundException>(() => BookReader.ReadBook(EPUB_FILE_PATH, null, new TestFileSystem()));
        }

        [Fact(DisplayName = "ReadBookAsync should throw FileNotFoundException if the specified file does not exist")]
        public async void ReadBookFromFileAsyncWithMissingFileTest()
        {
            await Assert.ThrowsAsync<FileNotFoundException>(() => BookReader.ReadBookAsync(EPUB_FILE_PATH, null, new TestFileSystem()));
        }

        [Fact(DisplayName = "ReadBook should rethrow EPUB parsing exceptions")]
        public void ReadBookFromFileWithIncorrectEpubTest()
        {
            TestZipFile incorrectEpubFile = new();
            TestFileSystem testFileSystem = new(EPUB_FILE_PATH, incorrectEpubFile);
            Assert.Throws<EpubContainerException>(() => BookReader.ReadBook(EPUB_FILE_PATH, null, testFileSystem));
        }

        [Fact(DisplayName = "ReadBookAsync should rethrow EPUB parsing exceptions")]
        public async void ReadBookFromFileAsyncWithIncorrectEpubTest()
        {
            TestZipFile incorrectEpubFile = new();
            TestFileSystem testFileSystem = new(EPUB_FILE_PATH, incorrectEpubFile);
            await Assert.ThrowsAsync<EpubContainerException>(() => BookReader.ReadBookAsync(EPUB_FILE_PATH, null, testFileSystem));
        }
    }
}
