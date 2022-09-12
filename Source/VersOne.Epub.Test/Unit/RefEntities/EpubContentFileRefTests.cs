using System.Text;
using VersOne.Epub.Options;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.RefEntities
{
    public class EpubContentFileRefTests
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

        [Fact(DisplayName = "Reading content of an existing text file synchronously should succeed")]
        public void ReadTextContentTest()
        {
            EpubTextContentFileRef epubTextContentFileRef = CreateTextContentFileRef();
            string textContent = epubTextContentFileRef.ReadContent();
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Reading content of an existing text file asynchronously should succeed")]
        public async void ReadTextContentAsyncTest()
        {
            EpubTextContentFileRef epubTextContentFileRef = CreateTextContentFileRef();
            string textContent = await epubTextContentFileRef.ReadContentAsync();
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Reading content of an existing byte file synchronously should succeed")]
        public void ReadByteContentTest()
        {
            EpubByteContentFileRef epubByteContentFileRef = CreateByteContentFileRef();
            byte[] byteContent = epubByteContentFileRef.ReadContent();
            Assert.Equal(BYTE_FILE_CONTENT, byteContent);
        }

        [Fact(DisplayName = "Reading content of an existing byte file asynchronously should succeed")]
        public async void ReadByteContentAsyncTest()
        {
            EpubByteContentFileRef epubByteContentFileRef = CreateByteContentFileRef();
            byte[] byteContent = await epubByteContentFileRef.ReadContentAsync();
            Assert.Equal(BYTE_FILE_CONTENT, byteContent);
        }

        [Fact(DisplayName = "GetContentStream should throw EpubPackageException if the file name is empty")]
        public void GetContentStreamWithEmptyFileNameTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            EpubTextContentFileRef epubTextContentFileRef = new(epubBookRef, String.Empty, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE);
            Assert.Throws<EpubPackageException>(() => epubTextContentFileRef.GetContentStream());
        }

        [Fact(DisplayName = "GetContentStream should throw EpubContentException if the file is missing in the EPUB archive")]
        public void GetContentStreamWithMissingFileTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            EpubTextContentFileRef epubTextContentFileRef = new(epubBookRef, TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE);
            Assert.Throws<EpubContentException>(() => epubTextContentFileRef.GetContentStream());
        }

        [Fact(DisplayName = "GetContentStream should throw EpubContentException if the file is larger than 2 GB")]
        public void GetContentStreamWithLargeFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, new Test4GbZipFileEntry());
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            EpubTextContentFileRef epubTextContentFileRef = new(epubBookRef, TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE);
            Assert.Throws<EpubContentException>(() => epubTextContentFileRef.GetContentStream());
        }

        [Fact(DisplayName = "EpubContentFileRef should return an empty content if the file is missing and SuppressException is true")]
        public void ReadContentWithMissingFileAndSuppressExceptionTrueTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            contentReaderOptions.ContentFileMissing += (sender, e) => e.SuppressException = true;
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            EpubTextContentFileRef epubTextContentFileRef = new(epubBookRef, TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, contentReaderOptions);
            string textContent = epubTextContentFileRef.ReadContent();
            Assert.Equal(String.Empty, textContent);
        }

        [Fact(DisplayName = "EpubContentFileRef should return a replacement content if the file is missing and ReplacementContentStream is set")]
        public void ReadContentWithMissingFileAndReplacementContentStreamSetTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            contentReaderOptions.ContentFileMissing += (sender, e) => e.ReplacementContentStream = new MemoryStream(Encoding.UTF8.GetBytes(TEXT_FILE_CONTENT));
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            EpubTextContentFileRef epubTextContentFileRef = new(epubBookRef, TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, contentReaderOptions);
            string textContent = epubTextContentFileRef.ReadContent();
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "Using replacement content for a missing file should succeed")]
        public void ReadContentWithReplacementContentMultipleTimesTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            contentReaderOptions.ContentFileMissing += (sender, e) => e.ReplacementContentStream = new MemoryStream(Encoding.UTF8.GetBytes(TEXT_FILE_CONTENT));
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            EpubTextContentFileRef epubTextContentFileRef = new(epubBookRef, TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, contentReaderOptions);
            string textContent = epubTextContentFileRef.ReadContent();
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
            textContent = epubTextContentFileRef.ReadContent();
            Assert.Equal(TEXT_FILE_CONTENT, textContent);
        }

        [Fact(DisplayName = "GetContentStream should throw EpubContentException if the file is missing and ContentFileMissing has no event handlers")]
        public void GetContentStreamWithMissingFileAndNoContentFileMissingHandlersTest()
        {
            ContentReaderOptions contentReaderOptions = new();
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            EpubTextContentFileRef epubTextContentFileRef = new(epubBookRef, TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, contentReaderOptions);
            Assert.Throws<EpubContentException>(() => epubTextContentFileRef.GetContentStream());
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
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            EpubTextContentFileRef epubTextContentFileRef = new(epubBookRef, TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE, contentReaderOptions);
            epubTextContentFileRef.ReadContent();
            Assert.Equal(TEXT_FILE_NAME, contentFileMissingEventArgs.FileName);
            Assert.Equal(TEXT_FILE_PATH, contentFileMissingEventArgs.FilePathInEpubArchive);
            Assert.Equal(TEXT_FILE_CONTENT_TYPE, contentFileMissingEventArgs.ContentType);
            Assert.Equal(TEXT_FILE_CONTENT_MIME_TYPE, contentFileMissingEventArgs.ContentMimeType);
        }

        private EpubTextContentFileRef CreateTextContentFileRef()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(TEXT_FILE_PATH, TEXT_FILE_CONTENT);
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            return new(epubBookRef, TEXT_FILE_NAME, TEXT_FILE_CONTENT_TYPE, TEXT_FILE_CONTENT_MIME_TYPE);
        }

        private EpubByteContentFileRef CreateByteContentFileRef()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(BYTE_FILE_PATH, BYTE_FILE_CONTENT);
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            return new(epubBookRef, BYTE_FILE_NAME, BYTE_FILE_CONTENT_TYPE, BYTE_FILE_CONTENT_MIME_TYPE);
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
