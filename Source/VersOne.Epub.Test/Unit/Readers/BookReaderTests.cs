using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;
using VersOne.Epub.Test.Unit.TestData;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class BookReaderTests
    {
        private readonly TestEnvironmentDependencies environmentDependencies;

        public BookReaderTests()
        {
            environmentDependencies = new TestEnvironmentDependencies();
        }

        [Fact(DisplayName = "Constructing a BookReader instance with non-null constructor parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            _ = new BookReader(environmentDependencies, new EpubReaderOptions());
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if environmentDependencies parameter is null")]
        public void ConstructorWithNullEnvironmentDependenciesTest()
        {
            Assert.Throws<ArgumentNullException>(() => new BookReader(null!, new EpubReaderOptions()));
        }

        [Fact(DisplayName = "Constructing a BookReader instance with a null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNullEpubReaderOptionsTest()
        {
            _ = new BookReader(environmentDependencies, null);
        }

        [Fact(DisplayName = "Reading a minimal EPUB book from a file should succeed")]
        public void ReadMinimalBookFromFileTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateMinimalTestEpubFile();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateMinimalTestEpubBook(EPUB_FILE_PATH);
            BookReader bookReader = new(environmentDependencies, null);
            EpubBook actualEpubBook = bookReader.ReadBook(EPUB_FILE_PATH);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a minimal EPUB 2 book without navigation from a file with IgnoreMissingToc = true should succeed")]
        public void ReadMinimalEpub2BookWithoutNavigationFromFileTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateMinimalTestEpub2FileWithoutNavigation();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateMinimalTestEpub2BookWithoutNavigation(EPUB_FILE_PATH);
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = new PackageReaderOptions()
                {
                    IgnoreMissingToc = true
                }
            };
            BookReader bookReader = new(environmentDependencies, epubReaderOptions);
            EpubBook actualEpubBook = bookReader.ReadBook(EPUB_FILE_PATH);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a file synchronously without downloading remote files should succeed")]
        public void ReadBookFromFileWithoutDownloadingRemoteFilesTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(EPUB_FILE_PATH, populateRemoteFilesContents: false);
            BookReader bookReader = new(environmentDependencies, null);
            EpubBook actualEpubBook = bookReader.ReadBook(EPUB_FILE_PATH);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a file asynchronously without downloading remote files should succeed")]
        public async void ReadBookFromFileAsyncWithoutDownloadingRemoteFilesTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(EPUB_FILE_PATH, populateRemoteFilesContents: false);
            BookReader bookReader = new(environmentDependencies, null);
            EpubBook actualEpubBook = await bookReader.ReadBookAsync(EPUB_FILE_PATH);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a stream synchronously without downloading remote files should succeed")]
        public void ReadBookFromStreamWithoutDownloadingRemoteFilesTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            environmentDependencies.FileSystem = new TestFileSystem(epubFileStream, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(null, populateRemoteFilesContents: false);
            BookReader bookReader = new(environmentDependencies, null);
            EpubBook actualEpubBook = bookReader.ReadBook(epubFileStream);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a stream asynchronously without downloading remote files should succeed")]
        public async void ReadBookFromStreamAsyncWithoutDownloadingRemoteFilesTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            environmentDependencies.FileSystem = new TestFileSystem(epubFileStream, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(null, populateRemoteFilesContents: false);
            BookReader bookReader = new(environmentDependencies, null);
            EpubBook actualEpubBook = await bookReader.ReadBookAsync(epubFileStream);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a file synchronously with downloading remote files should succeed")]
        public void ReadBookFromFileWithDownloadingRemoteFilesTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(EPUB_FILE_PATH, populateRemoteFilesContents: true);
            EpubReaderOptions epubReaderOptions = CreateEpubReaderOptionsToDownloadRemoteFiles();
            BookReader bookReader = new(environmentDependencies, epubReaderOptions);
            EpubBook actualEpubBook = bookReader.ReadBook(EPUB_FILE_PATH);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a file asynchronously with downloading remote files should succeed")]
        public async void ReadBookFromFileAsyncWithDownloadingRemoteFilesTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(EPUB_FILE_PATH, populateRemoteFilesContents: true);
            EpubReaderOptions epubReaderOptions = CreateEpubReaderOptionsToDownloadRemoteFiles();
            BookReader bookReader = new(environmentDependencies, epubReaderOptions);
            EpubBook actualEpubBook = await bookReader.ReadBookAsync(EPUB_FILE_PATH);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a stream synchronously with downloading remote files should succeed")]
        public void ReadBookFromStreamWithDownloadingRemoteFilesTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            environmentDependencies.FileSystem = new TestFileSystem(epubFileStream, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(null, populateRemoteFilesContents: true);
            EpubReaderOptions epubReaderOptions = CreateEpubReaderOptionsToDownloadRemoteFiles();
            BookReader bookReader = new(environmentDependencies, epubReaderOptions);
            EpubBook actualEpubBook = bookReader.ReadBook(epubFileStream);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a stream asynchronously with downloading remote files should succeed")]
        public async void ReadBookFromStreamAsyncWithDownloadingRemoteFilesTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            environmentDependencies.FileSystem = new TestFileSystem(epubFileStream, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(null, populateRemoteFilesContents: true);
            EpubReaderOptions epubReaderOptions = CreateEpubReaderOptionsToDownloadRemoteFiles();
            BookReader bookReader = new(environmentDependencies, epubReaderOptions);
            EpubBook actualEpubBook = await bookReader.ReadBookAsync(epubFileStream);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book with null ContentDownloaderOptions should succeed")]
        public void ReadBookFromFileWithNullContentDownloaderOptionsTest()
        {
            TestZipFile testEpubFile = TestEpubFiles.CreateFullTestEpubFile();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = TestEpubBooks.CreateFullTestEpubBook(EPUB_FILE_PATH, populateRemoteFilesContents: false);
            EpubReaderOptions epubReaderOptions = new()
            {
                ContentDownloaderOptions = null!
            };
            BookReader bookReader = new(environmentDependencies, epubReaderOptions);
            EpubBook actualEpubBook = bookReader.ReadBook(EPUB_FILE_PATH);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "ReadBook should throw FileNotFoundException if the specified file does not exist")]
        public void ReadBookFromFileWithMissingFileTest()
        {
            BookReader bookReader = new(environmentDependencies, null);
            Assert.Throws<FileNotFoundException>(() => bookReader.ReadBook(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "ReadBookAsync should throw FileNotFoundException if the specified file does not exist")]
        public async void ReadBookFromFileAsyncWithMissingFileTest()
        {
            BookReader bookReader = new(environmentDependencies, null);
            await Assert.ThrowsAsync<FileNotFoundException>(() => bookReader.ReadBookAsync(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "ReadBook should rethrow EPUB parsing exceptions")]
        public void ReadBookFromFileWithIncorrectEpubTest()
        {
            TestZipFile incorrectEpubFile = new();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, incorrectEpubFile);
            BookReader bookReader = new(environmentDependencies, null);
            Assert.Throws<EpubContainerException>(() => bookReader.ReadBook(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "ReadBookAsync should rethrow EPUB parsing exceptions")]
        public async void ReadBookFromFileAsyncWithIncorrectEpubTest()
        {
            TestZipFile incorrectEpubFile = new();
            environmentDependencies.FileSystem = new TestFileSystem(EPUB_FILE_PATH, incorrectEpubFile);
            BookReader bookReader = new(environmentDependencies, null);
            await Assert.ThrowsAsync<EpubContainerException>(() => bookReader.ReadBookAsync(EPUB_FILE_PATH));
        }

        private static EpubReaderOptions CreateEpubReaderOptionsToDownloadRemoteFiles()
        {
            TestContentDownloader testContentDownloader = new();
            testContentDownloader.AddTextRemoteFile(REMOTE_HTML_CONTENT_FILE_HREF, TestEpubFiles.REMOTE_HTML_FILE_CONTENT);
            testContentDownloader.AddTextRemoteFile(REMOTE_CSS_CONTENT_FILE_HREF, TestEpubFiles.REMOTE_CSS_FILE_CONTENT);
            testContentDownloader.AddByteRemoteFile(REMOTE_IMAGE_CONTENT_FILE_HREF, TestEpubFiles.REMOTE_IMAGE_FILE_CONTENT);
            testContentDownloader.AddByteRemoteFile(REMOTE_FONT_CONTENT_FILE_HREF, TestEpubFiles.REMOTE_FONT_FILE_CONTENT);
            testContentDownloader.AddTextRemoteFile(REMOTE_XML_CONTENT_FILE_HREF, TestEpubFiles.REMOTE_XML_FILE_CONTENT);
            testContentDownloader.AddByteRemoteFile(REMOTE_AUDIO_CONTENT_FILE_HREF, TestEpubFiles.REMOTE_AUDIO_FILE_CONTENT);
            return new()
            {
                ContentDownloaderOptions = new ContentDownloaderOptions()
                {
                    DownloadContent = true,
                    CustomContentDownloader = testContentDownloader
                }
            };
        }
    }
}
