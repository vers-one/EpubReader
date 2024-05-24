using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;
using VersOne.Epub.Test.Unit.TestData;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class BookRefReaderTests
    {
        private readonly TestEnvironmentDependencies environmentDependencies;

        public BookRefReaderTests()
        {
            environmentDependencies = new TestEnvironmentDependencies();
        }

        [Fact(DisplayName = "Constructing a BookRefReader instance with non-null constructor parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            _ = new BookRefReader(environmentDependencies, new EpubReaderOptions());
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if environmentDependencies parameter is null")]
        public void ConstructorWithNullEnvironmentDependenciesTest()
        {
            Assert.Throws<ArgumentNullException>(() => new BookRefReader(null!, new EpubReaderOptions()));
        }

        [Fact(DisplayName = "Constructing a BookRefReader instance with a null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNullEpubReaderOptionsTest()
        {
            _ = new BookRefReader(environmentDependencies, null);
        }

        [Fact(DisplayName = "Opening a minimal EPUB book from a file synchronously should succeed")]
        public void OpenMinimalBookFromFileTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateMinimalTestEpubFile();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBookRef expectedEpubBookRef = TestEpubBookRefs.CreateMinimalTestEpubBookRef(testEpubFile, EPUB_FILE_PATH);
            BookRefReader bookRefReader = new(environmentDependencies, null);
            EpubBookRef actualEpubBookRef = bookRefReader.OpenBook(EPUB_FILE_PATH);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a file asynchronously should succeed")]
        public async Task OpenBookFromFileAsyncTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBookRef expectedEpubBookRef = TestEpubBookRefs.CreateFullTestEpubBookRef(testEpubFile, EPUB_FILE_PATH);
            BookRefReader bookRefReader = new(environmentDependencies, null);
            EpubBookRef actualEpubBookRef = await bookRefReader.OpenBookAsync(EPUB_FILE_PATH);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a file synchronously should succeed")]
        public void OpenBookFromFileTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBookRef expectedEpubBookRef = TestEpubBookRefs.CreateFullTestEpubBookRef(testEpubFile, EPUB_FILE_PATH);
            BookRefReader bookRefReader = new(environmentDependencies, null);
            EpubBookRef actualEpubBookRef = bookRefReader.OpenBook(EPUB_FILE_PATH);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a stream synchronously should succeed")]
        public void OpenBookFromStreamTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            environmentDependencies.FileSystem = new TestFileSystem(epubFileStream, testEpubFile);
            EpubBookRef expectedEpubBookRef = TestEpubBookRefs.CreateFullTestEpubBookRef(testEpubFile, null);
            BookRefReader bookRefReader = new(environmentDependencies, null);
            EpubBookRef actualEpubBookRef = bookRefReader.OpenBook(epubFileStream);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a stream asynchronously should succeed")]
        public async Task OpenBookFromStreamAsyncTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            environmentDependencies.FileSystem = new TestFileSystem(epubFileStream, testEpubFile);
            EpubBookRef expectedEpubBookRef = TestEpubBookRefs.CreateFullTestEpubBookRef(testEpubFile, null);
            BookRefReader bookRefReader = new(environmentDependencies, null);
            EpubBookRef actualEpubBookRef = await bookRefReader.OpenBookAsync(epubFileStream);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "OpenBook should throw FileNotFoundException if the specified file does not exist")]
        public void OpenBookFromFileWithMissingFileTest()
        {
            BookRefReader bookRefReader = new(environmentDependencies, null);
            Assert.Throws<FileNotFoundException>(() => bookRefReader.OpenBook(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "OpenBookAsync should throw FileNotFoundException if the specified file does not exist")]
        public async Task OpenBookFromFileAsyncWithMissingFileTest()
        {
            BookRefReader bookRefReader = new(environmentDependencies, null);
            await Assert.ThrowsAsync<FileNotFoundException>(() => bookRefReader.OpenBookAsync(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "OpenBook should rethrow EPUB parsing exceptions")]
        public void OpenBookFromFileWithIncorrectEpubFileTest()
        {
            TestZipFile incorrectEpubFile = new();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, incorrectEpubFile);
            BookRefReader bookRefReader = new(environmentDependencies, null);
            Assert.Throws<EpubContainerException>(() => bookRefReader.OpenBook(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "OpenBookAsync should rethrow EPUB parsing exceptions")]
        public async Task OpenBookFromFileAsyncWithIncorrectEpubTest()
        {
            TestZipFile incorrectEpubFile = new();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, incorrectEpubFile);
            BookRefReader bookRefReader = new(environmentDependencies, null);
            await Assert.ThrowsAsync<EpubContainerException>(() => bookRefReader.OpenBookAsync(EPUB_FILE_PATH));
        }
    }
}
