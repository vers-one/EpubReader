using System.Text;
using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Content
{
    public class EpubLocalContentLoaderTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string TEXT_FILE_NAME = "test.html";
        private const string TEXT_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{TEXT_FILE_NAME}";
        private const string TEXT_FILE_CONTENT = "<html><head><title>Test HTML</title></head><body><h1>Test content</h1></body></html>";
        private const EpubContentType TEXT_FILE_CONTENT_TYPE = EpubContentType.XHTML_1_1;
        private const string TEXT_FILE_CONTENT_MIME_TYPE = "application/xhtml+xml";
        private const string BYTE_FILE_NAME = "image.jpg";
        private const string BYTE_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{BYTE_FILE_NAME}";
        private const EpubContentType BYTE_FILE_CONTENT_TYPE = EpubContentType.IMAGE_JPEG;
        private const string BYTE_FILE_CONTENT_MIME_TYPE = "image/jpeg";

        private static readonly byte[] BYTE_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46 };

        private EpubContentFileRefMetadata TextFileRefMetadata => new(TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE);

        private EpubContentFileRefMetadata ByteFileRefMetadata => new(BYTE_FILE_NAME, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE);

        [Fact(DisplayName = "Constructing a local content loader with non-null constructor parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            _ = new EpubLocalContentLoader(new TestEnvironmentDependencies(), new ContentReaderOptions(), new TestZipFile(), CONTENT_DIRECTORY_PATH);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if environmentDependencies parameter is null")]
        public void ConstructorWithNullEnvironmentDependenciesTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubLocalContentLoader(null, new ContentReaderOptions(), new TestZipFile(), CONTENT_DIRECTORY_PATH));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if epubFile parameter is null")]
        public void ConstructorWithNullEpubFileTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubLocalContentLoader(new TestEnvironmentDependencies(), new ContentReaderOptions(), null, CONTENT_DIRECTORY_PATH));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contentDirectoryPath parameter is null")]
        public void ConstructorWithNullContentDirectoryPathTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubLocalContentLoader(new TestEnvironmentDependencies(), new ContentReaderOptions(), new TestZipFile(), null));
        }

        [Fact(DisplayName = "Constructing a local content loader with null contentReaderOptions parameter should succeed")]
        public void ConstructorWithNullContentReaderOptionsTest()
        {
            _ = new EpubLocalContentLoader(new TestEnvironmentDependencies(), null, new TestZipFile(), CONTENT_DIRECTORY_PATH);
        }

        [Fact(DisplayName = "Loading content of an existing text file synchronously should succeed")]
        public void LoadContentAsTextTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, TEXT_FILE_CONTENT);
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            string textContent = epubLocalContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Loading content of an existing text file asynchronously should succeed")]
        public async void LoadContentAsTextAsyncTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, TEXT_FILE_CONTENT);
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            string textContent = await epubLocalContentLoader.LoadContentAsTextAsync(TextFileRefMetadata);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Loading content of an existing byte file synchronously should succeed")]
        public void LoadContentAsBytesTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(BYTE_FILE_PATH, BYTE_FILE_CONTENT);
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            byte[] byteContent = epubLocalContentLoader.LoadContentAsBytes(ByteFileRefMetadata);
            Assert.Equal(BYTE_FILE_CONTENT, byteContent);
        }

        [Fact(DisplayName = "Loading content of an existing byte file asynchronously should succeed")]
        public async void LoadContentAsBytesAsyncTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(BYTE_FILE_PATH, BYTE_FILE_CONTENT);
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            byte[] byteContent = await epubLocalContentLoader.LoadContentAsBytesAsync(ByteFileRefMetadata);
            Assert.Equal(BYTE_FILE_CONTENT, byteContent);
        }

        [Fact(DisplayName = "Getting content stream of an existing file synchronously should succeed")]
        public void GetContentStreamTest()
        {
            TestZipFile testZipFile = new();
            using MemoryStream testContentStream = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, testContentStream);
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            Stream textContentStream = epubLocalContentLoader.GetContentStream(TextFileRefMetadata);
            Assert.Equal(testContentStream, textContentStream);
        }

        [Fact(DisplayName = "Getting content stream of an existing file asynchronously should succeed")]
        public async void GetContentStreamAsyncTest()
        {
            TestZipFile testZipFile = new();
            using MemoryStream testContentStream = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, testContentStream);
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            Stream textContentStream = await epubLocalContentLoader.GetContentStreamAsync(TextFileRefMetadata);
            Assert.Equal(testContentStream, textContentStream);
        }

        [Fact(DisplayName = "Loading content of multiple existing files should succeed")]
        public void LoadContentWithMultipleFilesTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, TEXT_FILE_CONTENT);
            testZipFile.AddEntry(BYTE_FILE_PATH, BYTE_FILE_CONTENT);
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            string textContent = epubLocalContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
            byte[] byteContent = epubLocalContentLoader.LoadContentAsBytes(ByteFileRefMetadata);
            Assert.Equal(BYTE_FILE_CONTENT, byteContent);
        }

        [Fact(DisplayName = "LoadContentAsText should throw EpubContentException if the file is missing in the EPUB archive")]
        public void LoadContentAsTextWithMissingFileTest()
        {
            TestZipFile testZipFile = new();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            Assert.Throws<EpubContentException>(() => epubLocalContentLoader.LoadContentAsText(TextFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsTextAsync should throw EpubContentException if the file is missing in the EPUB archive")]
        public async void LoadContentAsTextAsyncWithMissingFileTest()
        {
            TestZipFile testZipFile = new();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            await Assert.ThrowsAsync<EpubContentException>(() => epubLocalContentLoader.LoadContentAsTextAsync(TextFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsBytes should throw EpubContentException if the file is missing in the EPUB archive")]
        public void LoadContentAsBytesWithMissingFileTest()
        {
            TestZipFile testZipFile = new();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            Assert.Throws<EpubContentException>(() => epubLocalContentLoader.LoadContentAsBytes(ByteFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsBytesAsync should throw EpubContentException if the file is missing in the EPUB archive")]
        public async void LoadContentAsBytesAsyncWithMissingFileTest()
        {
            TestZipFile testZipFile = new();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            await Assert.ThrowsAsync<EpubContentException>(() => epubLocalContentLoader.LoadContentAsBytesAsync(ByteFileRefMetadata));
        }

        [Fact(DisplayName = "GetContentStream should throw EpubContentException if the file is missing in the EPUB archive")]
        public void GetContentStreamWithMissingFileTest()
        {
            TestZipFile testZipFile = new();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            Assert.Throws<EpubContentException>(() => epubLocalContentLoader.GetContentStream(TextFileRefMetadata));
        }

        [Fact(DisplayName = "GetContentStreamAsync should throw EpubContentException if the file is missing in the EPUB archive")]
        public async void GetContentStreamAsyncWithMissingFileTest()
        {
            TestZipFile testZipFile = new();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            await Assert.ThrowsAsync<EpubContentException>(() => epubLocalContentLoader.GetContentStreamAsync(TextFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsText should throw EpubContentException if the file is larger than 2 GB")]
        public void LoadContentAsTextWithLargeFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, new Test4GbZipFileEntry());
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            Assert.Throws<EpubContentException>(() => epubLocalContentLoader.LoadContentAsText(TextFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsTextAsync should throw EpubContentException if the file is larger than 2 GB")]
        public async void LoadContentAsTextAsyncWithLargeFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, new Test4GbZipFileEntry());
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            await Assert.ThrowsAsync<EpubContentException>(() => epubLocalContentLoader.LoadContentAsTextAsync(TextFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsBytes should throw EpubContentException if the file is larger than 2 GB")]
        public void LoadContentAsBytesWithLargeFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(BYTE_FILE_PATH, new Test4GbZipFileEntry());
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            Assert.Throws<EpubContentException>(() => epubLocalContentLoader.LoadContentAsBytes(ByteFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsBytesAsync should throw EpubContentException if the file is larger than 2 GB")]
        public async void LoadContentAsBytesAsyncWithLargeFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(BYTE_FILE_PATH, new Test4GbZipFileEntry());
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            await Assert.ThrowsAsync<EpubContentException>(() => epubLocalContentLoader.LoadContentAsBytesAsync(ByteFileRefMetadata));
        }

        [Fact(DisplayName = "GetContentStream should throw EpubContentException if the file is larger than 2 GB")]
        public void GetContentStreamWithLargeFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, new Test4GbZipFileEntry());
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            Assert.Throws<EpubContentException>(() => epubLocalContentLoader.GetContentStream(TextFileRefMetadata));
        }

        [Fact(DisplayName = "GetContentStreamAsync should throw EpubContentException if the file is larger than 2 GB")]
        public async void GetContentStreamAsyncWithLargeFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, new Test4GbZipFileEntry());
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            await Assert.ThrowsAsync<EpubContentException>(() => epubLocalContentLoader.GetContentStreamAsync(TextFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsText should throw ObjectDisposedException if the EPUB file is disposed")]
        public void LoadContentAsTextWithDisposedEpubFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.Dispose();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            Assert.Throws<ObjectDisposedException>(() => epubLocalContentLoader.LoadContentAsText(TextFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsTextAsync should throw ObjectDisposedException if the EPUB file is disposed")]
        public async void LoadContentAsTextAsyncWithDisposedEpubFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.Dispose();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            await Assert.ThrowsAsync<ObjectDisposedException>(() => epubLocalContentLoader.LoadContentAsTextAsync(TextFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsBytes should throw ObjectDisposedException if the EPUB file is disposed")]
        public void LoadContentAsBytesWithDisposedEpubFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.Dispose();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            Assert.Throws<ObjectDisposedException>(() => epubLocalContentLoader.LoadContentAsBytes(ByteFileRefMetadata));
        }

        [Fact(DisplayName = "LoadContentAsBytesAsync should throw ObjectDisposedException if the EPUB file is disposed")]
        public async void LoadContentAsBytesAsyncWithDisposedEpubFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.Dispose();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            await Assert.ThrowsAsync<ObjectDisposedException>(() => epubLocalContentLoader.LoadContentAsBytesAsync(ByteFileRefMetadata));
        }

        [Fact(DisplayName = "GetContentStream should throw ObjectDisposedException if the EPUB file is disposed")]
        public void GetContentStreamWithDisposedEpubFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.Dispose();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            Assert.Throws<ObjectDisposedException>(() => epubLocalContentLoader.GetContentStream(TextFileRefMetadata));
        }

        [Fact(DisplayName = "GetContentStreamAsync should throw ObjectDisposedException if the EPUB file is disposed")]
        public async void GetContentStreamAsyncWithDisposedEpubFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.Dispose();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile);
            await Assert.ThrowsAsync<ObjectDisposedException>(() => epubLocalContentLoader.GetContentStreamAsync(TextFileRefMetadata));
        }

        [Fact(DisplayName = "Local content loader should return an empty content if the file is missing and SuppressException is true")]
        public void LoadContentWithMissingFileAndSuppressExceptionTrueTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            contentReaderOptions.ContentFileMissing += (sender, e) => e.SuppressException = true;
            TestZipFile testZipFile = new();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile, contentReaderOptions);
            string textContent = epubLocalContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(String.Empty, textContent);
        }

        [Fact(DisplayName = "Local content loader should return a replacement content if the file is missing and ReplacementContentStream is set")]
        public void LoadContentWithMissingFileAndReplacementContentStreamSetTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            contentReaderOptions.ContentFileMissing += (sender, e) => e.ReplacementContentStream = new MemoryStream(Encoding.UTF8.GetBytes(TEXT_FILE_CONTENT));
            TestZipFile testZipFile = new();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile, contentReaderOptions);
            string textContent = epubLocalContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Using replacement content multiple times while loading the same content file should succeed")]
        public void LoadContentWithReplacementContentMultipleTimesTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            contentReaderOptions.ContentFileMissing += (sender, e) => e.ReplacementContentStream = new MemoryStream(Encoding.UTF8.GetBytes(TEXT_FILE_CONTENT));
            TestZipFile testZipFile = new();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile, contentReaderOptions);
            string textContent = epubLocalContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
            textContent = epubLocalContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Local content loader should throw EpubContentException if the file is missing and ContentFileMissing has no event handlers")]
        public void LoadContentWithMissingFileAndNoContentFileMissingHandlersTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            TestZipFile testZipFile = new();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile, contentReaderOptions);
            Assert.Throws<EpubContentException>(() => epubLocalContentLoader.GetContentStream(TextFileRefMetadata));
        }

        [Fact(DisplayName = "ContentFileMissingEventArgs should contain details of the missing content file")]
        public void ContentFileMissingEventArgsTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            ContentFileMissingEventArgs contentFileMissingEventArgs = null;
            contentReaderOptions.ContentFileMissing += (sender, e) =>
            {
                e.SuppressException = true;
                contentFileMissingEventArgs = e;
            };
            TestZipFile testZipFile = new();
            EpubLocalContentLoader epubLocalContentLoader = CreateEpubLocalContentLoader(testZipFile, contentReaderOptions);
            epubLocalContentLoader.LoadContentAsText(TextFileRefMetadata);
            Assert.Equal(TEXT_FILE_NAME, contentFileMissingEventArgs.FileKey);
            Assert.Equal(TEXT_FILE_PATH, contentFileMissingEventArgs.FilePath);
            Assert.Equal(TEXT_FILE_CONTENT_TYPE, contentFileMissingEventArgs.ContentType);
            Assert.Equal(TEXT_FILE_CONTENT_MIME_TYPE, contentFileMissingEventArgs.ContentMimeType);
        }

        private EpubLocalContentLoader CreateEpubLocalContentLoader(TestZipFile testZipFile, ContentReaderOptions contentReaderOptions = null)
        {
            return new(new TestEnvironmentDependencies(), contentReaderOptions, testZipFile, CONTENT_DIRECTORY_PATH);
        }
    }
}
