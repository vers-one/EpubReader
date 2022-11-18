using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;
using VersOne.Epub.Test.Unit.TestData;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.Entities
{
    public class EpubBookRefTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string COVER_FILE_NAME = "image.jpg";
        private const string COVER_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{COVER_FILE_NAME}";
        private const EpubContentType COVER_FILE_CONTENT_TYPE = EpubContentType.IMAGE_JPEG;
        private const string COVER_FILE_CONTENT_MIME_TYPE = "image/jpeg";
        private const string HTML_FILE_NAME = "test.html";
        private const string HTML_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{HTML_FILE_NAME}";
        private const string TEST_ANCHOR_TEXT = "Test anchor";

        private static readonly byte[] COVER_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46 };

        private static List<string> AuthorList =>
            new()
            {
                BOOK_AUTHOR
            };

        private static EpubSchema Schema => TestEpubSchemas.CreateFullTestEpubSchema();

        private static EpubContentRef Content => TestEpubContentRef.CreateFullTestEpubContentRef();

        [Fact(DisplayName = "Constructing a EpubBookRef instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile, EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, Schema, Content);
            Assert.Equal(testZipFile, epubBookRef.EpubFile);
            Assert.Equal(EPUB_FILE_PATH, epubBookRef.FilePath);
            Assert.Equal(BOOK_TITLE, epubBookRef.Title);
            Assert.Equal(BOOK_AUTHOR, epubBookRef.Author);
            Assert.Equal(AuthorList, epubBookRef.AuthorList);
            Assert.Equal(BOOK_DESCRIPTION, epubBookRef.Description);
            EpubSchemaComparer.CompareEpubSchemas(Schema, epubBookRef.Schema);
            EpubContentRefComparer.CompareEpubContentRefs(Content, epubBookRef.Content);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if epubFile parameter is null")]
        public void ConstructorWithNullEpubFileTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubBookRef(null!, EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, Schema, Content));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if title parameter is null")]
        public void ConstructorWithNullTitleTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubBookRef(new TestZipFile(), EPUB_FILE_PATH, null!, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, Schema, Content));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if author parameter is null")]
        public void ConstructorWithNullAuthorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubBookRef(new TestZipFile(), EPUB_FILE_PATH, BOOK_TITLE, null!, AuthorList, BOOK_DESCRIPTION, Schema, Content));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if schema parameter is null")]
        public void ConstructorWithNullSchemaTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubBookRef(new TestZipFile(), EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, null!, Content));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if content parameter is null")]
        public void ConstructorWithNullContentTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubBookRef(new TestZipFile(), EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, Schema, null!));
        }

        [Fact(DisplayName = "Constructing a EpubBookRef instance with null filePath parameter should succeed")]
        public void ConstructorWithNullFilePathTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile, null, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, Schema, Content);
            Assert.Equal(testZipFile, epubBookRef.EpubFile);
            Assert.Null(epubBookRef.FilePath);
            Assert.Equal(BOOK_TITLE, epubBookRef.Title);
            Assert.Equal(BOOK_AUTHOR, epubBookRef.Author);
            Assert.Equal(AuthorList, epubBookRef.AuthorList);
            Assert.Equal(BOOK_DESCRIPTION, epubBookRef.Description);
            EpubSchemaComparer.CompareEpubSchemas(Schema, epubBookRef.Schema);
            EpubContentRefComparer.CompareEpubContentRefs(Content, epubBookRef.Content);
        }

        [Fact(DisplayName = "Constructing a EpubBookRef instance with null authorList parameter should succeed")]
        public void ConstructorWithNullAuthorListTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile, EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, null, BOOK_DESCRIPTION, Schema, Content);
            Assert.Equal(testZipFile, epubBookRef.EpubFile);
            Assert.Equal(EPUB_FILE_PATH, epubBookRef.FilePath);
            Assert.Equal(BOOK_TITLE, epubBookRef.Title);
            Assert.Equal(BOOK_AUTHOR, epubBookRef.Author);
            Assert.Equal(new List<string>(), epubBookRef.AuthorList);
            Assert.Equal(BOOK_DESCRIPTION, epubBookRef.Description);
            EpubSchemaComparer.CompareEpubSchemas(Schema, epubBookRef.Schema);
            EpubContentRefComparer.CompareEpubContentRefs(Content, epubBookRef.Content);
        }

        [Fact(DisplayName = "Constructing a EpubBookRef instance with null description parameter should succeed")]
        public void ConstructorWithNullDescriptionTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile, EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, null, Schema, Content);
            Assert.Equal(testZipFile, epubBookRef.EpubFile);
            Assert.Equal(EPUB_FILE_PATH, epubBookRef.FilePath);
            Assert.Equal(BOOK_TITLE, epubBookRef.Title);
            Assert.Equal(BOOK_AUTHOR, epubBookRef.Author);
            Assert.Equal(AuthorList, epubBookRef.AuthorList);
            Assert.Null(epubBookRef.Description);
            EpubSchemaComparer.CompareEpubSchemas(Schema, epubBookRef.Schema);
            EpubContentRefComparer.CompareEpubContentRefs(Content, epubBookRef.Content);
        }

        [Fact(DisplayName = "Reading the existing cover synchronously should succeed")]
        public void ReadCoverTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithCover(COVER_FILE_CONTENT);
            byte[]? coverContent = epubBookRef.ReadCover();
            Assert.Equal(COVER_FILE_CONTENT, coverContent);
        }

        [Fact(DisplayName = "Reading the existing cover asynchronously should succeed")]
        public async void ReadCoverAsyncTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithCover(COVER_FILE_CONTENT);
            byte[]? coverContent = await epubBookRef.ReadCoverAsync();
            Assert.Equal(COVER_FILE_CONTENT, coverContent);
        }

        [Fact(DisplayName = "ReadCover should return null if the cover is missing")]
        public void ReadCoverWithoutCoverTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile, EpubVersion.EPUB_3);
            byte[]? coverContent = epubBookRef.ReadCover();
            Assert.Null(coverContent);
        }

        [Fact(DisplayName = "ReadCoverAsync should return null if the cover is missing")]
        public async void ReadCoverAsyncWithoutCoverTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile, EpubVersion.EPUB_3);
            byte[]? coverContent = await epubBookRef.ReadCoverAsync();
            Assert.Null(coverContent);
        }

        [Fact(DisplayName = "Getting reading order synchronously should succeed")]
        public void GetReadingOrderTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithReadingOrder(HTML_FILE_NAME, HTML_FILE_PATH);
            List<EpubLocalTextContentFileRef> expectedReadingOrder = new()
            {
                epubBookRef.Content.Html.Local[HTML_FILE_NAME]
            };
            List<EpubLocalTextContentFileRef> actualReadingOrder = epubBookRef.GetReadingOrder();
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        [Fact(DisplayName = "Getting reading order asynchronously should succeed")]
        public async void GetReadingOrderAsyncTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithReadingOrder(HTML_FILE_NAME, HTML_FILE_PATH);
            List<EpubLocalTextContentFileRef> expectedReadingOrder = new()
            {
                epubBookRef.Content.Html.Local[HTML_FILE_NAME]
            };
            List<EpubLocalTextContentFileRef> actualReadingOrder = await epubBookRef.GetReadingOrderAsync();
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        [Fact(DisplayName = "Getting navigation items synchronously should succeed")]
        public void GetNavigationTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithNavigation(HTML_FILE_NAME, HTML_FILE_PATH, TEST_ANCHOR_TEXT);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                CreateTestNavigationLink(TEST_ANCHOR_TEXT, HTML_FILE_NAME, epubBookRef.Content.Html.Local[HTML_FILE_NAME])
            };
            List<EpubNavigationItemRef>? actualNavigationItems = epubBookRef.GetNavigation();
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items asynchronously should succeed")]
        public async void GetNavigationAsyncTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithNavigation(HTML_FILE_NAME, HTML_FILE_PATH, TEST_ANCHOR_TEXT);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                CreateTestNavigationLink(TEST_ANCHOR_TEXT, HTML_FILE_NAME, epubBookRef.Content.Html.Local[HTML_FILE_NAME])
            };
            List<EpubNavigationItemRef>? actualNavigationItems = await epubBookRef.GetNavigationAsync();
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "GetNavigation should return null for EPUB 2 books with no navigation")]
        public void GetNavigationForEpub2WithNoNavigationTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRef(new TestZipFile(), EpubVersion.EPUB_2);
            List<EpubNavigationItemRef>? navigationItems = epubBookRef.GetNavigation();
            Assert.Null(navigationItems);
        }

        [Fact(DisplayName = "GetNavigation should return null for EPUB 3 books with no navigation")]
        public void GetNavigationForEpub3WithNoNavigationTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRef(new TestZipFile(), EpubVersion.EPUB_3);
            List<EpubNavigationItemRef>? navigationItems = epubBookRef.GetNavigation();
            Assert.Null(navigationItems);
        }

        [Fact(DisplayName = "GetNavigationAsync should return null for EPUB 2 books with no navigation")]
        public async void GetNavigationAsyncForEpub2WithNoNavigationTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRef(new TestZipFile(), EpubVersion.EPUB_2);
            List<EpubNavigationItemRef>? navigationItems = await epubBookRef.GetNavigationAsync();
            Assert.Null(navigationItems);
        }

        [Fact(DisplayName = "GetNavigationAsync should return null for EPUB 3 books with no navigation")]
        public async void GetNavigationAsyncForEpub3WithNoNavigationTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRef(new TestZipFile(), EpubVersion.EPUB_3);
            List<EpubNavigationItemRef>? navigationItems = await epubBookRef.GetNavigationAsync();
            Assert.Null(navigationItems);
        }

        [Fact(DisplayName = "Disposing EpubBookRef with EPUB file should succeed")]
        public void DisposeWithEpubFileTest()
        {
            TestZipFile testZipFile = new();
            using EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile, EpubVersion.EPUB_3);
        }

        private static EpubBookRef CreateEpubBookRefWithCover(byte[] coverFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(COVER_FILE_PATH, coverFileContent);
            return CreateEpubBookRef
            (
                epubFile: testZipFile,
                epubVersion: EpubVersion.EPUB_3,
                content: new EpubContentRef
                (
                    cover: new EpubLocalByteContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata(COVER_FILE_NAME, COVER_FILE_CONTENT_TYPE, COVER_FILE_CONTENT_MIME_TYPE),
                        filePath: COVER_FILE_PATH,
                        epubContentLoader: new TestEpubContentLoader(coverFileContent)
                    )
                )
            );
        }

        private static EpubBookRef CreateEpubBookRefWithReadingOrder(string htmlFileName, string htmlFilePath)
        {
            EpubLocalTextContentFileRef htmlFileRef = CreateTestHtmlFileRef(htmlFileName, htmlFilePath);
            return CreateEpubBookRef
            (
                epubFile: new TestZipFile(),
                epubVersion: EpubVersion.EPUB_3,
                manifest: new EpubManifest
                (
                    items: new List<EpubManifestItem>()
                    {
                        new EpubManifestItem
                        (
                            id: "item-1",
                            href: htmlFileName,
                            mediaType: "application/xhtml+xml"
                        )
                    }
                ),
                spine: new EpubSpine
                (
                    items: new List<EpubSpineItemRef>()
                    {
                        new EpubSpineItemRef
                        (
                            idRef: "item-1"
                        )
                    }
                ),
                content: new EpubContentRef
                (
                    html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>
                    (
                        local: new Dictionary<string, EpubLocalTextContentFileRef>()
                        {
                            {
                                htmlFileName,
                                htmlFileRef
                            }
                        }
                    )
                )
            );
        }

        private static EpubBookRef CreateEpubBookRefWithNavigation(string htmlFileName, string htmlFilePath, string anchorText)
        {
            EpubLocalTextContentFileRef htmlFileRef = CreateTestHtmlFileRef(htmlFileName, htmlFilePath);
            EpubLocalTextContentFileRef navigationFileRef = CreateTestHtmlFileRef("toc.html", $"{CONTENT_DIRECTORY_PATH}/toc.html");
            return CreateEpubBookRef
            (
                epubFile: new TestZipFile(),
                epubVersion: EpubVersion.EPUB_3,
                epub3NavDocument: new Epub3NavDocument
                (
                    filePath: navigationFileRef.FilePath,
                    navs: new List<Epub3Nav>()
                    {
                        new Epub3Nav
                        (
                            type: Epub3NavStructuralSemanticsProperty.TOC,
                            ol: new Epub3NavOl
                            (
                                lis: new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            text: anchorText,
                                            href: htmlFileName
                                        )
                                    )
                                }
                            )
                        )
                    }
                ),
                content: new EpubContentRef
                (
                    navigationHtmlFile: navigationFileRef,
                    html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>
                    (
                        local: new Dictionary<string, EpubLocalTextContentFileRef>()
                        {
                            {
                                htmlFileName,
                                htmlFileRef
                            }
                        }
                    )
                )
            );
        }

        private static EpubBookRef CreateEpubBookRef(TestZipFile epubFile, EpubVersion epubVersion, EpubManifest? manifest = null, EpubSpine? spine = null,
            Epub3NavDocument? epub3NavDocument = null, EpubContentRef? content = null)
        {
            return new
            (
                epubFile: epubFile,
                filePath: null,
                title: String.Empty,
                author: String.Empty,
                authorList: null,
                description: null,
                schema: new EpubSchema
                (
                    contentDirectoryPath: CONTENT_DIRECTORY_PATH,
                    package: new EpubPackage
                    (
                        epubVersion: epubVersion,
                        metadata: new EpubMetadata(),
                        manifest: manifest ?? new EpubManifest(),
                        spine: spine ?? new EpubSpine(),
                        guide: null
                    ),
                    epub2Ncx: null,
                    epub3NavDocument: epub3NavDocument
                ),
                content: content ?? new EpubContentRef()
            );
        }

        private static EpubLocalTextContentFileRef CreateTestHtmlFileRef(string fileName, string filePath)
        {
            EpubContentFileRefMetadata htmlFileRefMetadata = new(fileName, EpubContentType.XHTML_1_1, "application/xhtml+xml");
            return new(htmlFileRefMetadata, filePath, new TestEpubContentLoader());
        }

        private static EpubNavigationItemRef CreateTestNavigationLink(string title, string htmlFileUrl, EpubLocalTextContentFileRef htmlFileRef)
        {
            return new
            (
                type: EpubNavigationItemType.LINK,
                title: title,
                link: new EpubNavigationItemLink(htmlFileUrl, CONTENT_DIRECTORY_PATH),
                htmlContentFileRef: htmlFileRef,
                nestedItems: null
            );
        }
    }
}
