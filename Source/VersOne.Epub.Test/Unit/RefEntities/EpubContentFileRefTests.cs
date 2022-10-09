using System.Text;
using VersOne.Epub.Options;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.RefEntities
{
    public class EpubContentFileRefTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string LOCAL_TEXT_FILE_NAME = "test.html";
        private const string TEXT_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{LOCAL_TEXT_FILE_NAME}";
        private const string REMOTE_TEXT_CONTENT_HREF = "https://example.com/books/123/test.html";
        private const string TEXT_FILE_CONTENT = "<html><head><title>Test HTML</title></head><body><h1>Test content</h1></body></html>";
        private const EpubContentType TEXT_FILE_CONTENT_TYPE = EpubContentType.XHTML_1_1;
        private const string TEXT_FILE_CONTENT_MIME_TYPE = "application/xhtml+xml";
        private const string LOCAL_BYTE_FILE_NAME = "image.jpg";
        private const string BYTE_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{LOCAL_BYTE_FILE_NAME}";
        private const string REMOTE_BYTE_CONTENT_HREF = "https://example.com/books/123/image.jpg";
        private const EpubContentType BYTE_FILE_CONTENT_TYPE = EpubContentType.IMAGE_JPEG;
        private const string BYTE_FILE_CONTENT_MIME_TYPE = "image/jpeg";
        
        private static readonly byte[] BYTE_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46 };

        [Fact(DisplayName = "EpubTextContentFileRef with ContentLocation = LOCAL should have non-null FileName and FilePathInEpubArchive properties and null Href property")]
        public void LocalTextContentItemPropertiesTest()
        {
            EpubTextContentFileRef epubTextContentFileRef =
                new(LOCAL_TEXT_FILE_NAME, EpubContentLocation.LOCAL, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH);
            Assert.Equal(LOCAL_TEXT_FILE_NAME, epubTextContentFileRef.FileName);
            Assert.Equal(TEXT_FILE_PATH, epubTextContentFileRef.FilePathInEpubArchive);
            Assert.Null(epubTextContentFileRef.Href);
        }

        [Fact(DisplayName = "EpubByteContentFileRef with ContentLocation = LOCAL should have non-null FileName and FilePathInEpubArchive properties and null Href property")]
        public void LocalByteContentItemPropertiesTest()
        {
            EpubByteContentFileRef epubByteContentFileRef =
                new(LOCAL_BYTE_FILE_NAME, EpubContentLocation.LOCAL, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH);
            Assert.Equal(LOCAL_BYTE_FILE_NAME, epubByteContentFileRef.FileName);
            Assert.Equal(BYTE_FILE_PATH, epubByteContentFileRef.FilePathInEpubArchive);
            Assert.Null(epubByteContentFileRef.Href);
        }

        [Fact(DisplayName = "EpubTextContentFileRef with ContentLocation = REMOTE should have null FileName and FilePathInEpubArchive properties and non-null Href property")]
        public void RemoteTextContentItemPropertiesTest()
        {
            EpubTextContentFileRef epubTextContentFileRef =
                new(REMOTE_TEXT_CONTENT_HREF, EpubContentLocation.REMOTE, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH);
            Assert.Equal(REMOTE_TEXT_CONTENT_HREF, epubTextContentFileRef.Href);
            Assert.Null(epubTextContentFileRef.FileName);
            Assert.Null(epubTextContentFileRef.FilePathInEpubArchive);
        }

        [Fact(DisplayName = "EpubByteContentFileRef with ContentLocation = REMOTE should have null FileName and FilePathInEpubArchive properties and non-null Href property")]
        public void RemoteByteContentItemPropertiesTest()
        {
            EpubByteContentFileRef epubByteContentFileRef =
                new(REMOTE_BYTE_CONTENT_HREF, EpubContentLocation.REMOTE, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH);
            Assert.Equal(REMOTE_BYTE_CONTENT_HREF, epubByteContentFileRef.Href);
            Assert.Null(epubByteContentFileRef.FileName);
            Assert.Null(epubByteContentFileRef.FilePathInEpubArchive);
        }

        [Fact(DisplayName = "Reading content of an existing text file synchronously should succeed")]
        public void ReadTextContentTest()
        {
            TestZipFile testZipFile = CreateTestZipFileWithTextFile();
            EpubTextContentFileRef epubTextContentFileRef = CreateLocalTextContentFileRef();
            string textContent = epubTextContentFileRef.ReadContent(testZipFile);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Reading content of an existing text file asynchronously should succeed")]
        public async void ReadTextContentAsyncTest()
        {
            TestZipFile testZipFile = CreateTestZipFileWithTextFile();
            EpubTextContentFileRef epubTextContentFileRef = CreateLocalTextContentFileRef();
            string textContent = await epubTextContentFileRef.ReadContentAsync(testZipFile);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Reading content of an existing byte file synchronously should succeed")]
        public void ReadByteContentTest()
        {
            TestZipFile testZipFile = CreateTestZipFileWithByteFile();
            EpubByteContentFileRef epubByteContentFileRef = CreateLocalByteContentFileRef();
            byte[] byteContent = epubByteContentFileRef.ReadContent(testZipFile);
            Assert.Equal(BYTE_FILE_CONTENT, byteContent);
        }

        [Fact(DisplayName = "Reading content of an existing byte file asynchronously should succeed")]
        public async void ReadByteContentAsyncTest()
        {
            TestZipFile testZipFile = CreateTestZipFileWithByteFile();
            EpubByteContentFileRef epubByteContentFileRef = CreateLocalByteContentFileRef();
            byte[] byteContent = await epubByteContentFileRef.ReadContentAsync(testZipFile);
            Assert.Equal(BYTE_FILE_CONTENT, byteContent);
        }

        [Fact(DisplayName = "GetContentStream should throw EpubPackageException if the file name is empty")]
        public void GetContentStreamWithEmptyFileNameTest()
        {
            TestZipFile testZipFile = new();
            EpubTextContentFileRef epubTextContentFileRef =
                new(String.Empty, EpubContentLocation.LOCAL, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH);
            Assert.Throws<EpubPackageException>(() => epubTextContentFileRef.GetContentStream(testZipFile));
        }

        [Fact(DisplayName = "GetContentStream should throw EpubContentException if the file is missing in the EPUB archive")]
        public void GetContentStreamWithMissingFileTest()
        {
            TestZipFile testZipFile = new();
            EpubTextContentFileRef epubTextContentFileRef =
                new(LOCAL_TEXT_FILE_NAME, EpubContentLocation.LOCAL, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH);
            Assert.Throws<EpubContentException>(() => epubTextContentFileRef.GetContentStream(testZipFile));
        }

        [Fact(DisplayName = "GetContentStream should throw EpubContentException if the file is larger than 2 GB")]
        public void GetContentStreamWithLargeFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, new Test4GbZipFileEntry());
            EpubTextContentFileRef epubTextContentFileRef =
                new(LOCAL_TEXT_FILE_NAME, EpubContentLocation.LOCAL, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH);
            Assert.Throws<EpubContentException>(() => epubTextContentFileRef.GetContentStream(testZipFile));
        }

        [Fact(DisplayName = "EpubContentFileRef should return an empty content if the file is missing and SuppressException is true")]
        public void ReadContentWithMissingFileAndSuppressExceptionTrueTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            contentReaderOptions.ContentFileMissing += (sender, e) => e.SuppressException = true;
            TestZipFile testZipFile = new();
            EpubTextContentFileRef epubTextContentFileRef =
                new(LOCAL_TEXT_FILE_NAME, EpubContentLocation.LOCAL, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH, contentReaderOptions);
            string textContent = epubTextContentFileRef.ReadContent(testZipFile);
            Assert.Equal(String.Empty, textContent);
        }

        [Fact(DisplayName = "EpubContentFileRef should return a replacement content if the file is missing and ReplacementContentStream is set")]
        public void ReadContentWithMissingFileAndReplacementContentStreamSetTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            contentReaderOptions.ContentFileMissing += (sender, e) => e.ReplacementContentStream = new MemoryStream(Encoding.UTF8.GetBytes(TEXT_FILE_CONTENT));
            TestZipFile testZipFile = new();
            EpubTextContentFileRef epubTextContentFileRef =
                new(LOCAL_TEXT_FILE_NAME, EpubContentLocation.LOCAL, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH, contentReaderOptions);
            string textContent = epubTextContentFileRef.ReadContent(testZipFile);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Using replacement content for a missing file should succeed")]
        public void ReadContentWithReplacementContentMultipleTimesTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            contentReaderOptions.ContentFileMissing += (sender, e) => e.ReplacementContentStream = new MemoryStream(Encoding.UTF8.GetBytes(TEXT_FILE_CONTENT));
            TestZipFile testZipFile = new();
            EpubTextContentFileRef epubTextContentFileRef =
                new(LOCAL_TEXT_FILE_NAME, EpubContentLocation.LOCAL, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH, contentReaderOptions);
            string textContent = epubTextContentFileRef.ReadContent(testZipFile);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
            textContent = epubTextContentFileRef.ReadContent(testZipFile);
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "GetContentStream should throw EpubContentException if the file is missing and ContentFileMissing has no event handlers")]
        public void GetContentStreamWithMissingFileAndNoContentFileMissingHandlersTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            TestZipFile testZipFile = new();
            EpubTextContentFileRef epubTextContentFileRef =
                new(LOCAL_TEXT_FILE_NAME, EpubContentLocation.LOCAL, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH, contentReaderOptions);
            Assert.Throws<EpubContentException>(() => epubTextContentFileRef.GetContentStream(testZipFile));
        }

        [Fact(DisplayName = "ContentFileMissingEventArgs should contain details of the missing file")]
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
            EpubTextContentFileRef epubTextContentFileRef =
                new(LOCAL_TEXT_FILE_NAME, EpubContentLocation.LOCAL, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH, contentReaderOptions);
            epubTextContentFileRef.ReadContent(testZipFile);
            Assert.Equal(LOCAL_TEXT_FILE_NAME, contentFileMissingEventArgs.FileName);
            Assert.Equal(TEXT_FILE_PATH, contentFileMissingEventArgs.FilePathInEpubArchive);
            Assert.Equal(TEXT_FILE_CONTENT_TYPE, contentFileMissingEventArgs.ContentType);
            Assert.Equal(TEXT_FILE_CONTENT_MIME_TYPE, contentFileMissingEventArgs.ContentMimeType);
        }

        [Fact(DisplayName = "ReadContent should throw ArgumentNullException if the supplied EPUB file is null")]
        public void ReadContentForLocalTextContentItemWithNullEpubFileTest()
        {
            EpubTextContentFileRef epubTextContentFileRef = CreateLocalTextContentFileRef();
            Assert.Throws<ArgumentNullException>(() => epubTextContentFileRef.ReadContent(null));
        }

        [Fact(DisplayName = "ReadContentAsync should throw ArgumentNullException if the supplied EPUB file is null")]
        public async void ReadContentAsyncForLocalTextContentItemWithNullEpubFileTest()
        {
            EpubTextContentFileRef epubTextContentFileRef = CreateLocalTextContentFileRef();
            await Assert.ThrowsAsync<ArgumentNullException>(() => epubTextContentFileRef.ReadContentAsync(null));
        }

        [Fact(DisplayName = "ReadContent should throw ArgumentNullException if the supplied EPUB file is null")]
        public void ReadContentForLocalByteContentItemWithNullEpubFileTest()
        {
            EpubByteContentFileRef epubByteContentFileRef = CreateLocalByteContentFileRef();
            Assert.Throws<ArgumentNullException>(() => epubByteContentFileRef.ReadContent(null));
        }

        [Fact(DisplayName = "ReadContentAsync should throw ArgumentNullException if the supplied EPUB file is null")]
        public async void ReadContentAsyncForLocalByteContentItemWithNullEpubFileTest()
        {
            EpubByteContentFileRef epubByteContentFileRef = CreateLocalByteContentFileRef();
            await Assert.ThrowsAsync<ArgumentNullException>(() => epubByteContentFileRef.ReadContentAsync(null));
        }

        [Fact(DisplayName = "GetContentStream should throw ArgumentNullException if the supplied EPUB file is null")]
        public void GetContentStreamForLocalContentItemWithNullEpubFileTest()
        {
            EpubTextContentFileRef epubTextContentFileRef = CreateLocalTextContentFileRef();
            Assert.Throws<ArgumentNullException>(() => epubTextContentFileRef.GetContentStream(null));
        }

        [Fact(DisplayName = "ReadContent should throw InvalidOperationException for remote text content items")]
        public void ReadContentForRemoteTextContentItemTest()
        {
            TestZipFile testZipFile = new();
            EpubTextContentFileRef epubTextContentFileRef = CreateRemoteTextContentFileRef();
            Assert.Throws<InvalidOperationException>(() => epubTextContentFileRef.ReadContent(testZipFile));
        }

        [Fact(DisplayName = "ReadContentAsync should throw InvalidOperationException for remote text content items")]
        public async void ReadContentAsyncForRemoteTextContentItemTest()
        {
            TestZipFile testZipFile = new();
            EpubTextContentFileRef epubTextContentFileRef = CreateRemoteTextContentFileRef();
            await Assert.ThrowsAsync<InvalidOperationException>(() => epubTextContentFileRef.ReadContentAsync(testZipFile));
        }

        [Fact(DisplayName = "ReadContent should throw InvalidOperationException for remote byte content items")]
        public void ReadContentForRemoteByteContentItemTest()
        {
            TestZipFile testZipFile = new();
            EpubByteContentFileRef epubByteContentFileRef = CreateRemoteByteContentFileRef();
            Assert.Throws<InvalidOperationException>(() => epubByteContentFileRef.ReadContent(testZipFile));
        }

        [Fact(DisplayName = "ReadContentAsync should throw InvalidOperationException for remote byte content items")]
        public async void ReadContentAsyncForRemoteByteContentItemTest()
        {
            TestZipFile testZipFile = new();
            EpubByteContentFileRef epubByteContentFileRef = CreateRemoteByteContentFileRef();
            await Assert.ThrowsAsync<InvalidOperationException>(() => epubByteContentFileRef.ReadContentAsync(testZipFile));
        }

        [Fact(DisplayName = "GetContentStream should throw InvalidOperationException for remote content items")]
        public void GetContentStreamForRemoteContentItemTest()
        {
            TestZipFile testZipFile = new();
            EpubTextContentFileRef epubTextContentFileRef = CreateRemoteTextContentFileRef();
            Assert.Throws<InvalidOperationException>(() => epubTextContentFileRef.GetContentStream(testZipFile));
        }

        private TestZipFile CreateTestZipFileWithTextFile()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, TEXT_FILE_CONTENT);
            return testZipFile;
        }

        private TestZipFile CreateTestZipFileWithByteFile()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(BYTE_FILE_PATH, BYTE_FILE_CONTENT);
            return testZipFile;
        }

        private EpubTextContentFileRef CreateLocalTextContentFileRef()
        {
            return new(LOCAL_TEXT_FILE_NAME, EpubContentLocation.LOCAL, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH);
        }

        private EpubByteContentFileRef CreateLocalByteContentFileRef()
        {
            return new(LOCAL_BYTE_FILE_NAME, EpubContentLocation.LOCAL, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH);
        }

        private EpubTextContentFileRef CreateRemoteTextContentFileRef()
        {
            return new(REMOTE_TEXT_CONTENT_HREF, EpubContentLocation.REMOTE, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH);
        }

        private EpubByteContentFileRef CreateRemoteByteContentFileRef()
        {
            return new(REMOTE_BYTE_CONTENT_HREF, EpubContentLocation.REMOTE, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE, CONTENT_DIRECTORY_PATH);
        }

        private EpubBookRef CreateEpubBookRef(TestZipFile testZipFile)
        {
            return new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    ContentDirectoryPath = CONTENT_DIRECTORY_PATH
                }
            };
        }
    }
}
