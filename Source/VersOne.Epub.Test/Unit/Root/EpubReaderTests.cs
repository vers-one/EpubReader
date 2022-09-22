using VersOne.Epub.Environment;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Root
{
    public class EpubReaderTests
    {
        private const string EPUB_FILE_PATH = "test.epub";
        private const string CONTAINER_FILE_PATH = "META-INF/container.xml";
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string OPF_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/content.opf";
        private const EpubContentType HTML_CONTENT_TYPE = EpubContentType.XHTML_1_1;
        private const string HTML_CONTENT_MIME_TYPE = "application/xhtml+xml";
        private const string CHAPTER1_FILE_NAME = "chapter1.html";
        private const string CHAPTER1_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{CHAPTER1_FILE_NAME}";
        private const string CHAPTER2_FILE_NAME = "chapter2.html";
        private const string CHAPTER2_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{CHAPTER2_FILE_NAME}";
        private const EpubContentType CSS_CONTENT_TYPE = EpubContentType.CSS;
        private const string CSS_CONTENT_MIME_TYPE = "text/css";
        private const string STYLES1_FILE_NAME = "styles1.css";
        private const string STYLES1_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{STYLES1_FILE_NAME}";
        private const string STYLES2_FILE_NAME = "styles2.css";
        private const string STYLES2_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{STYLES2_FILE_NAME}";
        private const EpubContentType IMAGE_CONTENT_TYPE = EpubContentType.IMAGE_JPEG;
        private const string IMAGE_CONTENT_MIME_TYPE = "image/jpeg";
        private const string IMAGE1_FILE_NAME = "image1.jpg";
        private const string IMAGE1_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{IMAGE1_FILE_NAME}";
        private const string IMAGE2_FILE_NAME = "image2.jpg";
        private const string IMAGE2_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{IMAGE2_FILE_NAME}";
        private const EpubContentType FONT_CONTENT_TYPE = EpubContentType.FONT_TRUETYPE;
        private const string FONT_CONTENT_MIME_TYPE = "font/truetype";
        private const string FONT1_FILE_NAME = "font1.ttf";
        private const string FONT1_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{FONT1_FILE_NAME}";
        private const string FONT2_FILE_NAME = "font2.ttf";
        private const string FONT2_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{FONT2_FILE_NAME}";
        private const string NAV_FILE_NAME = "toc.html";
        private const string NAV_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{NAV_FILE_NAME}";
        private const string COVER_FILE_NAME = "cover.jpg";
        private const string COVER_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{COVER_FILE_NAME}";
        private const EpubContentType NCX_CONTENT_TYPE = EpubContentType.DTBOOK_NCX;
        private const string NCX_CONTENT_MIME_TYPE = "application/x-dtbncx+xml";
        private const string NCX_FILE_NAME = "toc.ncx";
        private const string NCX_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{NCX_FILE_NAME}";
        private const EpubContentType OTHER_CONTENT_TYPE = EpubContentType.OTHER;
        private const string AUDIO_MPEG_CONTENT_MIME_TYPE = "audio/mpeg";
        private const string AUDIO_FILE_NAME = "audio.mp3";
        private const string AUDIO_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{AUDIO_FILE_NAME}";
        private const string REMOTE_TEXT_CONTENT_ITEM_HREF = "https://example.com/books/123/test.html";
        private const string REMOTE_BYTE_CONTENT_ITEM_HREF = "https://example.com/books/123/image.jpg";
        private const string BOOK_TITLE = "Test title";
        private const string BOOK_AUTHOR = "John Doe";
        private const string BOOK_DESCRIPTION = "Test description";
        private const string BOOK_UID = "9781234567890";

        private const string CONTAINER_FILE_CONTENT = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
                <rootfile media-type="application/oebps-package+xml" full-path="{OPF_FILE_PATH}"/>
              </rootfiles>
            </container>
            """;

        private const string MINIMAL_OPF_FILE_CONTENT = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest>
                <item id="item-toc" href="{NAV_FILE_NAME}" media-type="{HTML_CONTENT_MIME_TYPE}" properties="nav" />
              </manifest>
              <spine />
            </package>
            """;

        private const string MINIMAL_NAV_FILE_CONTENT = $"""
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body />
            </html>
            """;

        private const string FULL_OPF_FILE_CONTENT = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" xmlns:opf="http://www.idpf.org/2007/opf" xmlns:dc="http://purl.org/dc/elements/1.1/" version="3.0">
              <metadata>
                <dc:title>{BOOK_TITLE}</dc:title>
                <dc:creator>{BOOK_AUTHOR}</dc:creator>
                <dc:description>{BOOK_DESCRIPTION}</dc:description>
              </metadata>
              <manifest>
                <item id="item-1" href="{CHAPTER1_FILE_NAME}" media-type="{HTML_CONTENT_MIME_TYPE}" />
                <item id="item-2" href="{CHAPTER2_FILE_NAME}" media-type="{HTML_CONTENT_MIME_TYPE}" />
                <item id="item-3" href="{STYLES1_FILE_NAME}" media-type="{CSS_CONTENT_MIME_TYPE}" />
                <item id="item-4" href="{STYLES2_FILE_NAME}" media-type="{CSS_CONTENT_MIME_TYPE}" />
                <item id="item-5" href="{IMAGE1_FILE_NAME}" media-type="{IMAGE_CONTENT_MIME_TYPE}" />
                <item id="item-6" href="{IMAGE2_FILE_NAME}" media-type="{IMAGE_CONTENT_MIME_TYPE}" />
                <item id="item-7" href="{FONT1_FILE_NAME}" media-type="{FONT_CONTENT_MIME_TYPE}" />
                <item id="item-8" href="{FONT2_FILE_NAME}" media-type="{FONT_CONTENT_MIME_TYPE}" />
                <item id="item-9" href="{AUDIO_FILE_NAME}" media-type="{AUDIO_MPEG_CONTENT_MIME_TYPE}" />
                <item id="item-10" href="{REMOTE_TEXT_CONTENT_ITEM_HREF}" media-type="{HTML_CONTENT_MIME_TYPE}" />
                <item id="item-11" href="{REMOTE_BYTE_CONTENT_ITEM_HREF}" media-type="{IMAGE_CONTENT_MIME_TYPE}" />
                <item id="item-toc" href="{NAV_FILE_NAME}" media-type="{HTML_CONTENT_MIME_TYPE}" properties="nav" />
                <item id="item-cover" href="{COVER_FILE_NAME}" media-type="{IMAGE_CONTENT_MIME_TYPE}" properties="cover-image" />
                <item id="ncx" href="{NCX_FILE_NAME}" media-type="{NCX_CONTENT_MIME_TYPE}" />
              </manifest>
              <spine toc="ncx">
                <itemref id="itemref-1" idref="item-1" />
                <itemref id="itemref-2" idref="item-2" />
              </spine>
            </package>
            """;

        private const string NCX_FILE_CONTENT = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/" version="2005-1">
              <head>
                <meta name="dtb:uid" content="{BOOK_UID}" />
              </head>
              <docTitle>
                <text>{BOOK_TITLE}</text>
              </docTitle>
              <docAuthor>
                <text>{BOOK_AUTHOR}</text>
              </docAuthor>
              <navMap>
                <navPoint id="navpoint-1">
                  <navLabel>
                    <text>Chapter 1</text>
                  </navLabel>
                  <content src="{CHAPTER1_FILE_NAME}" />
                </navPoint>
                <navPoint id="navpoint-2">
                  <navLabel>
                    <text>Chapter 2</text>
                  </navLabel>
                  <content src="{CHAPTER2_FILE_NAME}" />
                </navPoint>
              </navMap>
            </ncx>
            """;

        private const string FULL_NAV_FILE_CONTENT = $"""
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
                  <ol>
                    <li>
                        <a href="{CHAPTER1_FILE_NAME}">Chapter 1</a>
                    </li>
                    <li>
                        <a href="{CHAPTER2_FILE_NAME}">Chapter 2</a>
                    </li>
                  </ol>
                </nav>
              </body>
            </html>
            """;

        private const string CHAPTER1_FILE_CONTENT = "<html><head><title>Chapter 1</title></head><body><h1>Chapter 1</h1></body></html>";

        private const string CHAPTER2_FILE_CONTENT = "<html><head><title>Chapter 2</title></head><body><h1>Chapter 2</h1></body></html>";

        private const string STYLES1_FILE_CONTENT = ".text{color:#010101}";

        private const string STYLES2_FILE_CONTENT = ".text{color:#020202}";

        private static readonly byte[] IMAGE1_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46, 0x01 };

        private static readonly byte[] IMAGE2_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46, 0x02 };

        private static readonly byte[] COVER_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46, 0xff };

        private static readonly byte[] FONT1_FILE_CONTENT = new byte[] { 0x00, 0x01, 0x00, 0x01 };

        private static readonly byte[] FONT2_FILE_CONTENT = new byte[] { 0x00, 0x01, 0x00, 0x02 };

        private static readonly byte[] AUDIO_FILE_CONTENT = new byte[] { 0x49, 0x44, 0x33, 0x03 };

        [Fact(DisplayName = "Opening a minimal EPUB book from a file synchronously should succeed")]
        public void OpenMinimalBookFromFileTest()
        {
            TestZipFile testEpubFile = CreateMinimalTestEpubFile();
            SetupTestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBookRef expectedEpubBookRef = CreateMinimalExpectedEpubBookRef(testEpubFile, EPUB_FILE_PATH);
            EpubBookRef actualEpubBookRef = EpubReader.OpenBook(EPUB_FILE_PATH);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Reading a minimal EPUB book from a file synchronously should succeed")]
        public void ReadMinimalBookFromFileTest()
        {
            TestZipFile testEpubFile = CreateMinimalTestEpubFile();
            SetupTestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = CreateMinimalExpectedEpubBook(EPUB_FILE_PATH);
            EpubBook actualEpubBook = EpubReader.ReadBook(EPUB_FILE_PATH);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a file synchronously should succeed")]
        public void OpenBookFromFileTest()
        {
            TestZipFile testEpubFile = CreateFullTestEpubFile();
            SetupTestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBookRef expectedEpubBookRef = CreateFullExpectedEpubBookRef(testEpubFile, EPUB_FILE_PATH);
            EpubBookRef actualEpubBookRef = EpubReader.OpenBook(EPUB_FILE_PATH);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a file asynchronously should succeed")]
        public async void OpenBookFromFileAsyncTest()
        {
            TestZipFile testEpubFile = CreateFullTestEpubFile();
            SetupTestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBookRef expectedEpubBookRef = CreateFullExpectedEpubBookRef(testEpubFile, EPUB_FILE_PATH);
            EpubBookRef actualEpubBookRef = await EpubReader.OpenBookAsync(EPUB_FILE_PATH);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a stream synchronously should succeed")]
        public void OpenBookFromStreamTest()
        {
            TestZipFile testEpubFile = CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            SetupTestFileSystem(epubFileStream, testEpubFile);
            EpubBookRef expectedEpubBookRef = CreateFullExpectedEpubBookRef(testEpubFile, null);
            EpubBookRef actualEpubBookRef = EpubReader.OpenBook(epubFileStream);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Opening a full EPUB book from a stream asynchronously should succeed")]
        public async void OpenBookFromStreamAsyncTest()
        {
            TestZipFile testEpubFile = CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            SetupTestFileSystem(epubFileStream, testEpubFile);
            EpubBookRef expectedEpubBookRef = CreateFullExpectedEpubBookRef(testEpubFile, null);
            EpubBookRef actualEpubBookRef = await EpubReader.OpenBookAsync(epubFileStream);
            EpubBookRefComparer.CompareEpubBookRefs(expectedEpubBookRef, actualEpubBookRef);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a file synchronously should succeed")]
        public void ReadBookFromFileTest()
        {
            TestZipFile testEpubFile = CreateFullTestEpubFile();
            SetupTestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = CreateFullExpectedEpubBook(EPUB_FILE_PATH);
            EpubBook actualEpubBook = EpubReader.ReadBook(EPUB_FILE_PATH);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a file asynchronously should succeed")]
        public async void ReadBookFromFileAsyncTest()
        {
            TestZipFile testEpubFile = CreateFullTestEpubFile();
            SetupTestFileSystem(EPUB_FILE_PATH, testEpubFile);
            EpubBook expectedEpubBook = CreateFullExpectedEpubBook(EPUB_FILE_PATH);
            EpubBook actualEpubBook = await EpubReader.ReadBookAsync(EPUB_FILE_PATH);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a stream synchronously should succeed")]
        public void ReadBookFromStreamTest()
        {
            TestZipFile testEpubFile = CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            SetupTestFileSystem(epubFileStream, testEpubFile);
            EpubBook expectedEpubBook = CreateFullExpectedEpubBook(null);
            EpubBook actualEpubBook = EpubReader.ReadBook(epubFileStream);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "Reading a full EPUB book from a stream asynchronously should succeed")]
        public async void ReadBookFromStreamAsyncTest()
        {
            TestZipFile testEpubFile = CreateFullTestEpubFile();
            MemoryStream epubFileStream = new();
            SetupTestFileSystem(epubFileStream, testEpubFile);
            EpubBook expectedEpubBook = CreateFullExpectedEpubBook(null);
            EpubBook actualEpubBook = await EpubReader.ReadBookAsync(epubFileStream);
            EpubBookComparer.CompareEpubBooks(expectedEpubBook, actualEpubBook);
        }

        [Fact(DisplayName = "OpenBook should throw FileNotFoundException if the specified file does not exist")]
        public void OpenBookFromFileWithMissingFileTest()
        {
            SetupEmptyTestFileSystem();
            Assert.Throws<FileNotFoundException>(() => EpubReader.OpenBook(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "OpenBookAsync should throw FileNotFoundException if the specified file does not exist")]
        public async void OpenBookFromFileAsyncWithMissingFileTest()
        {
            SetupEmptyTestFileSystem();
            await Assert.ThrowsAsync<FileNotFoundException>(() => EpubReader.OpenBookAsync(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "ReadBook should throw FileNotFoundException if the specified file does not exist")]
        public void ReadBookFromFileWithMissingFileTest()
        {
            SetupEmptyTestFileSystem();
            Assert.Throws<FileNotFoundException>(() => EpubReader.ReadBook(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "ReadBookAsync should throw FileNotFoundException if the specified file does not exist")]
        public async void ReadBookFromFileAsyncWithMissingFileTest()
        {
            SetupEmptyTestFileSystem();
            await Assert.ThrowsAsync<FileNotFoundException>(() => EpubReader.ReadBookAsync(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "OpenBook should rethrow EPUB parsing exceptions")]
        public void OpenBookFromFileWithIncorrectEpubFileTest()
        {
            TestZipFile incorrectEpubFile = new();
            SetupTestFileSystem(EPUB_FILE_PATH, incorrectEpubFile);
            Assert.Throws<EpubContainerException>(() => EpubReader.OpenBook(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "OpenBookAsync should rethrow EPUB parsing exceptions")]
        public async void OpenBookFromFileAsyncWithIncorrectEpubTest()
        {
            TestZipFile incorrectEpubFile = new();
            SetupTestFileSystem(EPUB_FILE_PATH, incorrectEpubFile);
            await Assert.ThrowsAsync<EpubContainerException>(() => EpubReader.OpenBookAsync(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "ReadBook should rethrow EPUB parsing exceptions")]
        public void ReadBookFromFileWithIncorrectEpubTest()
        {
            TestZipFile incorrectEpubFile = new();
            SetupTestFileSystem(EPUB_FILE_PATH, incorrectEpubFile);
            Assert.Throws<EpubContainerException>(() => EpubReader.ReadBook(EPUB_FILE_PATH));
        }

        [Fact(DisplayName = "ReadBookAsync should rethrow EPUB parsing exceptions")]
        public async void ReadBookFromFileAsyncWithIncorrectEpubTest()
        {
            TestZipFile incorrectEpubFile = new();
            SetupTestFileSystem(EPUB_FILE_PATH, incorrectEpubFile);
            await Assert.ThrowsAsync<EpubContainerException>(() => EpubReader.ReadBookAsync(EPUB_FILE_PATH));
        }

        private TestZipFile CreateMinimalTestEpubFile()
        {
            TestZipFile result = new();
            result.AddEntry(CONTAINER_FILE_PATH, CONTAINER_FILE_CONTENT);
            result.AddEntry(OPF_FILE_PATH, MINIMAL_OPF_FILE_CONTENT);
            result.AddEntry(NAV_FILE_PATH, MINIMAL_NAV_FILE_CONTENT);
            return result;
        }

        private TestZipFile CreateFullTestEpubFile()
        {
            TestZipFile result = new();
            result.AddEntry(CONTAINER_FILE_PATH, CONTAINER_FILE_CONTENT);
            result.AddEntry(OPF_FILE_PATH, FULL_OPF_FILE_CONTENT);
            result.AddEntry(CHAPTER1_FILE_PATH, CHAPTER1_FILE_CONTENT);
            result.AddEntry(CHAPTER2_FILE_PATH, CHAPTER2_FILE_CONTENT);
            result.AddEntry(STYLES1_FILE_PATH, STYLES1_FILE_CONTENT);
            result.AddEntry(STYLES2_FILE_PATH, STYLES2_FILE_CONTENT);
            result.AddEntry(IMAGE1_FILE_PATH, IMAGE1_FILE_CONTENT);
            result.AddEntry(IMAGE2_FILE_PATH, IMAGE2_FILE_CONTENT);
            result.AddEntry(FONT1_FILE_PATH, FONT1_FILE_CONTENT);
            result.AddEntry(FONT2_FILE_PATH, FONT2_FILE_CONTENT);
            result.AddEntry(AUDIO_FILE_PATH, AUDIO_FILE_CONTENT);
            result.AddEntry(NAV_FILE_PATH, FULL_NAV_FILE_CONTENT);
            result.AddEntry(COVER_FILE_PATH, COVER_FILE_CONTENT);
            result.AddEntry(NCX_FILE_PATH, NCX_FILE_CONTENT);
            return result;
        }

        private void SetupEmptyTestFileSystem()
        {
            TestFileSystem testFileSystem = new();
            EnvironmentDependencies.FileSystem = testFileSystem;
        }

        private void SetupTestFileSystem(string epubFilePath, TestZipFile testEpubFile)
        {
            TestFileSystem testFileSystem = new();
            testFileSystem.AddTestZipFile(epubFilePath, testEpubFile);
            EnvironmentDependencies.FileSystem = testFileSystem;
        }

        private void SetupTestFileSystem(Stream epubFileStream, TestZipFile testEpubFile)
        {
            TestFileSystem testFileSystem = new();
            testFileSystem.AddTestZipFile(epubFileStream, testEpubFile);
            EnvironmentDependencies.FileSystem = testFileSystem;
        }

        private EpubBookRef CreateMinimalExpectedEpubBookRef(TestZipFile epubFile, string epubFilePath)
        {
            EpubBookRef result = new(epubFile)
            {
                FilePath = epubFilePath,
                Title = String.Empty,
                Author = String.Empty,
                AuthorList = new List<string>(),
                Description = null,
                Schema = CreateMinimalExpectedEpubSchema(),
                Content = new EpubContentRef()
                {
                    Html = new Dictionary<string, EpubTextContentFileRef>(),
                    Css = new Dictionary<string, EpubTextContentFileRef>(),
                    Images = new Dictionary<string, EpubByteContentFileRef>(),
                    Fonts = new Dictionary<string, EpubByteContentFileRef>(),
                    AllFiles = new Dictionary<string, EpubContentFileRef>(),
                    Cover = null
                }
            };
            EpubTextContentFileRef navFileRef = new(result, NAV_FILE_NAME, EpubContentLocation.LOCAL, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            result.Content.Html[NAV_FILE_NAME] = navFileRef;
            result.Content.AllFiles[NAV_FILE_NAME] = navFileRef;
            result.Content.NavigationHtmlFile = navFileRef;
            return result;
        }

        private EpubBook CreateMinimalExpectedEpubBook(string epubFilePath)
        {
            EpubTextContentFile navFile = new()
            {
                FileName = NAV_FILE_NAME,
                FilePathInEpubArchive = NAV_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                Content = MINIMAL_NAV_FILE_CONTENT
            };
            return new()
            {
                FilePath = epubFilePath,
                Title = String.Empty,
                Author = String.Empty,
                AuthorList = new List<string>(),
                Description = null,
                CoverImage = null,
                ReadingOrder = new List<EpubTextContentFile>(),
                Navigation = new List<EpubNavigationItem>(),
                Schema = CreateMinimalExpectedEpubSchema(),
                Content = new EpubContent()
                {
                    Html = new Dictionary<string, EpubTextContentFile>()
                    {
                        {
                            NAV_FILE_NAME,
                            navFile
                        }
                    },
                    Css = new Dictionary<string, EpubTextContentFile>(),
                    Images = new Dictionary<string, EpubByteContentFile>(),
                    Fonts = new Dictionary<string, EpubByteContentFile>(),
                    AllFiles = new Dictionary<string, EpubContentFile>()
                    {
                        {
                            NAV_FILE_NAME,
                            navFile
                        }
                    },
                    NavigationHtmlFile = navFile,
                    Cover = null
                }
            };
        }

        private EpubSchema CreateMinimalExpectedEpubSchema()
        {
            return new()
            {
                Package = new EpubPackage()
                {
                    EpubVersion = EpubVersion.EPUB_3,
                    Metadata = new EpubMetadata()
                    {
                        Titles = new List<string>(),
                        Creators = new List<EpubMetadataCreator>(),
                        Subjects = new List<string>(),
                        Description = null,
                        Publishers = new List<string>(),
                        Contributors = new List<EpubMetadataContributor>(),
                        Dates = new List<EpubMetadataDate>(),
                        Types = new List<string>(),
                        Formats = new List<string>(),
                        Identifiers = new List<EpubMetadataIdentifier>(),
                        Sources = new List<string>(),
                        Languages = new List<string>(),
                        Relations = new List<string>(),
                        Coverages = new List<string>(),
                        Rights = new List<string>(),
                        Links = new List<EpubMetadataLink>(),
                        MetaItems = new List<EpubMetadataMeta>()
                    },
                    Manifest = new EpubManifest()
                    {
                        Items = new List<EpubManifestItem>()
                        {
                            new EpubManifestItem()
                            {
                                Id = "item-toc",
                                Href = NAV_FILE_NAME,
                                MediaType = HTML_CONTENT_MIME_TYPE,
                                Properties = new List<EpubManifestProperty>()
                                {
                                    EpubManifestProperty.NAV
                                }
                            }
                        }
                    },
                    Spine = new EpubSpine()
                    {
                        Items = new List<EpubSpineItemRef>()
                    },
                    Guide = null
                },
                Epub2Ncx = null,
                Epub3NavDocument = new Epub3NavDocument()
                {
                    Navs = new List<Epub3Nav>()
                },
                ContentDirectoryPath = CONTENT_DIRECTORY_PATH
            };
        }

        private EpubBookRef CreateFullExpectedEpubBookRef(TestZipFile epubFile, string epubFilePath)
        {
            EpubBookRef result = new(epubFile)
            {
                FilePath = epubFilePath,
                Title = BOOK_TITLE,
                Author = BOOK_AUTHOR,
                AuthorList = new List<string> { BOOK_AUTHOR },
                Description = BOOK_DESCRIPTION,
                Schema = CreateFullExpectedEpubSchema()
            };
            EpubTextContentFileRef chapter1FileRef = new(result, CHAPTER1_FILE_NAME, EpubContentLocation.LOCAL, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubTextContentFileRef chapter2FileRef = new(result, CHAPTER2_FILE_NAME, EpubContentLocation.LOCAL, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubTextContentFileRef styles1FileRef = new(result, STYLES1_FILE_NAME, EpubContentLocation.LOCAL, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
            EpubTextContentFileRef styles2FileRef = new(result, STYLES2_FILE_NAME, EpubContentLocation.LOCAL, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
            EpubByteContentFileRef image1FileRef = new(result, IMAGE1_FILE_NAME, EpubContentLocation.LOCAL, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubByteContentFileRef image2FileRef = new(result, IMAGE2_FILE_NAME, EpubContentLocation.LOCAL, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubByteContentFileRef font1FileRef = new(result, FONT1_FILE_NAME, EpubContentLocation.LOCAL, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
            EpubByteContentFileRef font2FileRef = new(result, FONT2_FILE_NAME, EpubContentLocation.LOCAL, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
            EpubByteContentFileRef audioFileRef = new(result, AUDIO_FILE_NAME, EpubContentLocation.LOCAL, OTHER_CONTENT_TYPE, AUDIO_MPEG_CONTENT_MIME_TYPE);
            EpubTextContentFileRef remoteTextContentItemRef = new(result, REMOTE_TEXT_CONTENT_ITEM_HREF, EpubContentLocation.REMOTE, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubByteContentFileRef remoteByteContentItemRef = new(result, REMOTE_BYTE_CONTENT_ITEM_HREF, EpubContentLocation.REMOTE, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubTextContentFileRef navFileRef = new(result, NAV_FILE_NAME, EpubContentLocation.LOCAL, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubByteContentFileRef coverFileRef = new(result, COVER_FILE_NAME, EpubContentLocation.LOCAL, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubTextContentFileRef ncxFileRef = new(result, NCX_FILE_NAME, EpubContentLocation.LOCAL, NCX_CONTENT_TYPE, NCX_CONTENT_MIME_TYPE);
            result.Content = new EpubContentRef()
            {
                Html = new Dictionary<string, EpubTextContentFileRef>()
                {
                    {
                        CHAPTER1_FILE_NAME,
                        chapter1FileRef
                    },
                    {
                        CHAPTER2_FILE_NAME,
                        chapter2FileRef
                    },
                    {
                        REMOTE_TEXT_CONTENT_ITEM_HREF,
                        remoteTextContentItemRef
                    },
                    {
                        NAV_FILE_NAME,
                        navFileRef
                    }
                },
                Css = new Dictionary<string, EpubTextContentFileRef>()
                {
                    {
                        STYLES1_FILE_NAME,
                        styles1FileRef
                    },
                    {
                        STYLES2_FILE_NAME,
                        styles2FileRef
                    }
                },
                Images = new Dictionary<string, EpubByteContentFileRef>()
                {
                    {
                        IMAGE1_FILE_NAME,
                        image1FileRef
                    },
                    {
                        IMAGE2_FILE_NAME,
                        image2FileRef
                    },
                    {
                        REMOTE_BYTE_CONTENT_ITEM_HREF,
                        remoteByteContentItemRef
                    },
                    {
                        COVER_FILE_NAME,
                        coverFileRef
                    }
                },
                Fonts = new Dictionary<string, EpubByteContentFileRef>()
                {
                    {
                        FONT1_FILE_NAME,
                        font1FileRef
                    },
                    {
                        FONT2_FILE_NAME,
                        font2FileRef
                    }
                },
                AllFiles = new Dictionary<string, EpubContentFileRef>()
                {
                    {
                        CHAPTER1_FILE_NAME,
                        chapter1FileRef
                    },
                    {
                        CHAPTER2_FILE_NAME,
                        chapter2FileRef
                    },
                    {
                        STYLES1_FILE_NAME,
                        styles1FileRef
                    },
                    {
                        STYLES2_FILE_NAME,
                        styles2FileRef
                    },
                    {
                        IMAGE1_FILE_NAME,
                        image1FileRef
                    },
                    {
                        IMAGE2_FILE_NAME,
                        image2FileRef
                    },
                    {
                        FONT1_FILE_NAME,
                        font1FileRef
                    },
                    {
                        FONT2_FILE_NAME,
                        font2FileRef
                    },
                    {
                        AUDIO_FILE_NAME,
                        audioFileRef
                    },
                    {
                        REMOTE_TEXT_CONTENT_ITEM_HREF,
                        remoteTextContentItemRef
                    },
                    {
                        REMOTE_BYTE_CONTENT_ITEM_HREF,
                        remoteByteContentItemRef
                    },
                    {
                        NAV_FILE_NAME,
                        navFileRef
                    },
                    {
                        COVER_FILE_NAME,
                        coverFileRef
                    },
                    {
                        NCX_FILE_NAME,
                        ncxFileRef
                    }
                },
                NavigationHtmlFile = navFileRef,
                Cover = coverFileRef
            };
            return result;
        }

        private EpubBook CreateFullExpectedEpubBook(string epubFilePath)
        {
            EpubTextContentFile chapter1File = new()
            {
                FileName = CHAPTER1_FILE_NAME,
                FilePathInEpubArchive = CHAPTER1_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                Content = CHAPTER1_FILE_CONTENT
            };
            EpubTextContentFile chapter2File = new()
            {
                FileName = CHAPTER2_FILE_NAME,
                FilePathInEpubArchive = CHAPTER2_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                Content = CHAPTER2_FILE_CONTENT
            };
            EpubTextContentFile styles1File = new()
            {
                FileName = STYLES1_FILE_NAME,
                FilePathInEpubArchive = STYLES1_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = CSS_CONTENT_TYPE,
                ContentMimeType = CSS_CONTENT_MIME_TYPE,
                Content = STYLES1_FILE_CONTENT
            };
            EpubTextContentFile styles2File = new()
            {
                FileName = STYLES2_FILE_NAME,
                FilePathInEpubArchive = STYLES2_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = CSS_CONTENT_TYPE,
                ContentMimeType = CSS_CONTENT_MIME_TYPE,
                Content = STYLES2_FILE_CONTENT
            };
            EpubByteContentFile image1File = new()
            {
                FileName = IMAGE1_FILE_NAME,
                FilePathInEpubArchive = IMAGE1_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                Content = IMAGE1_FILE_CONTENT
            };
            EpubByteContentFile image2File = new()
            {
                FileName = IMAGE2_FILE_NAME,
                FilePathInEpubArchive = IMAGE2_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                Content = IMAGE2_FILE_CONTENT
            };
            EpubByteContentFile font1File = new()
            {
                FileName = FONT1_FILE_NAME,
                FilePathInEpubArchive = FONT1_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = FONT_CONTENT_TYPE,
                ContentMimeType = FONT_CONTENT_MIME_TYPE,
                Content = FONT1_FILE_CONTENT
            };
            EpubByteContentFile font2File = new()
            {
                FileName = FONT2_FILE_NAME,
                FilePathInEpubArchive = FONT2_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = FONT_CONTENT_TYPE,
                ContentMimeType = FONT_CONTENT_MIME_TYPE,
                Content = FONT2_FILE_CONTENT
            };
            EpubByteContentFile audioFile = new()
            {
                FileName = AUDIO_FILE_NAME,
                FilePathInEpubArchive = AUDIO_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = OTHER_CONTENT_TYPE,
                ContentMimeType = AUDIO_MPEG_CONTENT_MIME_TYPE,
                Content = AUDIO_FILE_CONTENT
            };
            EpubTextContentFile remoteTextContentItem = new()
            {
                FileName = null,
                FilePathInEpubArchive = null,
                Href = REMOTE_TEXT_CONTENT_ITEM_HREF,
                ContentLocation = EpubContentLocation.REMOTE,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                Content = null
            };
            EpubByteContentFile remoteByteContentItem = new()
            {
                FileName = null,
                FilePathInEpubArchive = null,
                Href = REMOTE_BYTE_CONTENT_ITEM_HREF,
                ContentLocation = EpubContentLocation.REMOTE,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                Content = null
            };
            EpubTextContentFile navFile = new()
            {
                FileName = NAV_FILE_NAME,
                FilePathInEpubArchive = NAV_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                Content = FULL_NAV_FILE_CONTENT
            };
            EpubByteContentFile coverFile = new()
            {
                FileName = COVER_FILE_NAME,
                FilePathInEpubArchive = COVER_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                Content = COVER_FILE_CONTENT
            };
            EpubTextContentFile ncxFile = new()
            {
                FileName = NCX_FILE_NAME,
                FilePathInEpubArchive = NCX_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = NCX_CONTENT_TYPE,
                ContentMimeType = NCX_CONTENT_MIME_TYPE,
                Content = NCX_FILE_CONTENT
            };
            return new()
            {
                FilePath = epubFilePath,
                Title = BOOK_TITLE,
                Author = BOOK_AUTHOR,
                AuthorList = new List<string> { BOOK_AUTHOR },
                Description = BOOK_DESCRIPTION,
                CoverImage = COVER_FILE_CONTENT,
                ReadingOrder = new List<EpubTextContentFile>()
                {
                    chapter1File,
                    chapter2File
                },
                Navigation = new List<EpubNavigationItem>()
                {
                    new EpubNavigationItem()
                    {
                        Type = EpubNavigationItemType.LINK,
                        Title = "Chapter 1",
                        Link = new EpubNavigationItemLink(CHAPTER1_FILE_NAME, CONTENT_DIRECTORY_PATH),
                        HtmlContentFile = chapter1File,
                        NestedItems = new List<EpubNavigationItem>()
                    },
                    new EpubNavigationItem()
                    {
                        Type = EpubNavigationItemType.LINK,
                        Title = "Chapter 2",
                        Link = new EpubNavigationItemLink(CHAPTER2_FILE_NAME, CONTENT_DIRECTORY_PATH),
                        HtmlContentFile = chapter2File,
                        NestedItems = new List<EpubNavigationItem>()
                    }
                },
                Schema = CreateFullExpectedEpubSchema(),
                Content = new EpubContent()
                {
                    Html = new Dictionary<string, EpubTextContentFile>()
                    {
                        {
                            CHAPTER1_FILE_NAME,
                            chapter1File
                        },
                        {
                            CHAPTER2_FILE_NAME,
                            chapter2File
                        },
                        {
                            REMOTE_TEXT_CONTENT_ITEM_HREF,
                            remoteTextContentItem
                        },
                        {
                            NAV_FILE_NAME,
                            navFile
                        }
                    },
                    Css = new Dictionary<string, EpubTextContentFile>()
                    {
                        {
                            STYLES1_FILE_NAME,
                            styles1File
                        },
                        {
                            STYLES2_FILE_NAME,
                            styles2File
                        }
                    },
                    Images = new Dictionary<string, EpubByteContentFile>()
                    {
                        {
                            IMAGE1_FILE_NAME,
                            image1File
                        },
                        {
                            IMAGE2_FILE_NAME,
                            image2File
                        },
                        {
                            REMOTE_BYTE_CONTENT_ITEM_HREF,
                            remoteByteContentItem
                        },
                        {
                            COVER_FILE_NAME,
                            coverFile
                        }
                    },
                    Fonts = new Dictionary<string, EpubByteContentFile>()
                    {
                        {
                            FONT1_FILE_NAME,
                            font1File
                        },
                        {
                            FONT2_FILE_NAME,
                            font2File
                        }
                    },
                    AllFiles = new Dictionary<string, EpubContentFile>()
                    {
                        {
                            CHAPTER1_FILE_NAME,
                            chapter1File
                        },
                        {
                            CHAPTER2_FILE_NAME,
                            chapter2File
                        },
                        {
                            STYLES1_FILE_NAME,
                            styles1File
                        },
                        {
                            STYLES2_FILE_NAME,
                            styles2File
                        },
                        {
                            IMAGE1_FILE_NAME,
                            image1File
                        },
                        {
                            IMAGE2_FILE_NAME,
                            image2File
                        },
                        {
                            FONT1_FILE_NAME,
                            font1File
                        },
                        {
                            FONT2_FILE_NAME,
                            font2File
                        },
                        {
                            AUDIO_FILE_NAME,
                            audioFile
                        },
                        {
                            REMOTE_TEXT_CONTENT_ITEM_HREF,
                            remoteTextContentItem
                        },
                        {
                            REMOTE_BYTE_CONTENT_ITEM_HREF,
                            remoteByteContentItem
                        },
                        {
                            NAV_FILE_NAME,
                            navFile
                        },
                        {
                            COVER_FILE_NAME,
                            coverFile
                        },
                        {
                            NCX_FILE_NAME,
                            ncxFile
                        }
                    },
                    NavigationHtmlFile = navFile,
                    Cover = coverFile
                }
            };
        }

        private EpubSchema CreateFullExpectedEpubSchema()
        {
            return new()
            {
                Package = new EpubPackage()
                {
                    EpubVersion = EpubVersion.EPUB_3,
                    Metadata = new EpubMetadata()
                    {
                        Titles = new List<string>()
                        {
                            BOOK_TITLE
                        },
                        Creators = new List<EpubMetadataCreator>()
                        {
                            new EpubMetadataCreator()
                            {
                                Creator = BOOK_AUTHOR
                            }
                        },
                        Subjects = new List<string>(),
                        Description = BOOK_DESCRIPTION,
                        Publishers = new List<string>(),
                        Contributors = new List<EpubMetadataContributor>(),
                        Dates = new List<EpubMetadataDate>(),
                        Types = new List<string>(),
                        Formats = new List<string>(),
                        Identifiers = new List<EpubMetadataIdentifier>(),
                        Sources = new List<string>(),
                        Languages = new List<string>(),
                        Relations = new List<string>(),
                        Coverages = new List<string>(),
                        Rights = new List<string>(),
                        Links = new List<EpubMetadataLink>(),
                        MetaItems = new List<EpubMetadataMeta>()
                    },
                    Manifest = new EpubManifest()
                    {
                        Items = new List<EpubManifestItem>()
                        {
                            new EpubManifestItem()
                            {
                                Id = "item-1",
                                Href = CHAPTER1_FILE_NAME,
                                MediaType = HTML_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-2",
                                Href = CHAPTER2_FILE_NAME,
                                MediaType = HTML_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-3",
                                Href = STYLES1_FILE_NAME,
                                MediaType = CSS_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-4",
                                Href = STYLES2_FILE_NAME,
                                MediaType = CSS_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-5",
                                Href = IMAGE1_FILE_NAME,
                                MediaType = IMAGE_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-6",
                                Href = IMAGE2_FILE_NAME,
                                MediaType = IMAGE_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-7",
                                Href = FONT1_FILE_NAME,
                                MediaType = FONT_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-8",
                                Href = FONT2_FILE_NAME,
                                MediaType = FONT_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-9",
                                Href = AUDIO_FILE_NAME,
                                MediaType = AUDIO_MPEG_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-10",
                                Href = REMOTE_TEXT_CONTENT_ITEM_HREF,
                                MediaType = HTML_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-11",
                                Href = REMOTE_BYTE_CONTENT_ITEM_HREF,
                                MediaType = IMAGE_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-toc",
                                Href = NAV_FILE_NAME,
                                MediaType = HTML_CONTENT_MIME_TYPE,
                                Properties = new List<EpubManifestProperty>()
                                {
                                    EpubManifestProperty.NAV
                                }
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-cover",
                                Href = COVER_FILE_NAME,
                                MediaType = IMAGE_CONTENT_MIME_TYPE,
                                Properties = new List<EpubManifestProperty>()
                                {
                                    EpubManifestProperty.COVER_IMAGE
                                }
                            },
                            new EpubManifestItem()
                            {
                                Id = "ncx",
                                Href = NCX_FILE_NAME,
                                MediaType = NCX_CONTENT_MIME_TYPE
                            }
                        }
                    },
                    Spine = new EpubSpine()
                    {
                        Toc = "ncx",
                        Items = new List<EpubSpineItemRef>()
                        {
                            new EpubSpineItemRef()
                            {
                                Id = "itemref-1",
                                IdRef = "item-1",
                                IsLinear = true
                            },
                            new EpubSpineItemRef()
                            {
                                Id = "itemref-2",
                                IdRef = "item-2",
                                IsLinear = true
                            }
                        }
                    },
                    Guide = null
                },
                Epub2Ncx = new Epub2Ncx()
                {
                    Head = new Epub2NcxHead()
                    {
                        Items = new List<Epub2NcxHeadMeta>()
                        {
                            new Epub2NcxHeadMeta()
                            {
                                Name = "dtb:uid",
                                Content = BOOK_UID
                            }
                        }
                    },
                    DocTitle = BOOK_TITLE,
                    DocAuthors = new List<string>()
                    {
                        BOOK_AUTHOR
                    },
                    NavMap = new Epub2NcxNavigationMap()
                    {
                        Items = new List<Epub2NcxNavigationPoint>()
                        {
                            new Epub2NcxNavigationPoint()
                            {
                                Id = "navpoint-1",
                                NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Chapter 1"
                                    }
                                },
                                Content = new Epub2NcxContent()
                                {
                                    Source = CHAPTER1_FILE_NAME
                                },
                                ChildNavigationPoints = new List<Epub2NcxNavigationPoint>()
                            },
                            new Epub2NcxNavigationPoint()
                            {
                                Id = "navpoint-2",
                                NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Chapter 2"
                                    }
                                },
                                Content = new Epub2NcxContent()
                                {
                                    Source = CHAPTER2_FILE_NAME
                                },
                                ChildNavigationPoints = new List<Epub2NcxNavigationPoint>()
                            }
                        }
                    },
                    NavLists = new List<Epub2NcxNavigationList>()
                },
                Epub3NavDocument = new Epub3NavDocument()
                {
                    Navs = new List<Epub3Nav>()
                    {
                        new Epub3Nav()
                        {
                            Type = Epub3NavStructuralSemanticsProperty.TOC,
                            Ol = new Epub3NavOl()
                            {
                                Lis = new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi()
                                    {
                                        Anchor = new Epub3NavAnchor()
                                        {
                                            Href = CHAPTER1_FILE_NAME,
                                            Text = "Chapter 1"
                                        }
                                    },
                                    new Epub3NavLi()
                                    {
                                        Anchor = new Epub3NavAnchor()
                                        {
                                            Href = CHAPTER2_FILE_NAME,
                                            Text = "Chapter 2"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                ContentDirectoryPath = CONTENT_DIRECTORY_PATH
            };
        }
    }
}
