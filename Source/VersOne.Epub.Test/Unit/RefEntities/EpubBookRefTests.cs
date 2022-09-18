using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.RefEntities
{
    public class EpubBookRefTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string COVER_FILE_NAME = "image.jpg";
        private const string COVER_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{COVER_FILE_NAME}";
        private const EpubContentType COVER_FILE_CONTENT_TYPE = EpubContentType.IMAGE_JPEG;
        private const string COVER_FILE_CONTENT_MIME_TYPE = "image/jpeg";
        private const string HTML_FILE_NAME = "test.html";
        private const string TEST_ANCHOR_TEXT = "Test anchor";

        private static readonly byte[] COVER_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46 };

        [Fact(DisplayName = "Reading the existing cover synchronously should succeed")]
        public void ReadCoverTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithCover(COVER_FILE_CONTENT);
            byte[] coverContent = epubBookRef.ReadCover();
            Assert.Equal(COVER_FILE_CONTENT, coverContent);
        }

        [Fact(DisplayName = "Reading the existing cover asynchronously should succeed")]
        public async void ReadCoverAsyncTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithCover(COVER_FILE_CONTENT);
            byte[] coverContent = await epubBookRef.ReadCoverAsync();
            Assert.Equal(COVER_FILE_CONTENT, coverContent);
        }

        [Fact(DisplayName = "ReadCover should return null if the cover is missing")]
        public void ReadCoverWithoutCoverTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            byte[] coverContent = epubBookRef.ReadCover();
            Assert.Null(coverContent);
        }

        [Fact(DisplayName = "ReadCoverAsync should return null if the cover is missing")]
        public async void ReadCoverAsyncWithoutCoverTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            byte[] coverContent = await epubBookRef.ReadCoverAsync();
            Assert.Null(coverContent);
        }

        [Fact(DisplayName = "Getting reading order synchronously should succeed")]
        public void GetReadingOrderTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithReadingOrder(HTML_FILE_NAME);
            List<EpubTextContentFileRef> expectedReadingOrder = new()
            {
                epubBookRef.Content.Html[HTML_FILE_NAME]
            };
            List<EpubTextContentFileRef> actualReadingOrder = epubBookRef.GetReadingOrder();
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        [Fact(DisplayName = "Getting reading order asynchronously should succeed")]
        public async void GetReadingOrderAsyncTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithReadingOrder(HTML_FILE_NAME);
            List<EpubTextContentFileRef> expectedReadingOrder = new()
            {
                epubBookRef.Content.Html[HTML_FILE_NAME]
            };
            List<EpubTextContentFileRef> actualReadingOrder = await epubBookRef.GetReadingOrderAsync();
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        [Fact(DisplayName = "Getting navigation items synchronously should succeed")]
        public void GetNavigationTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithNavigation(HTML_FILE_NAME, TEST_ANCHOR_TEXT);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                CreateTestNavigationLink(TEST_ANCHOR_TEXT, HTML_FILE_NAME, epubBookRef.Content.Html[HTML_FILE_NAME])
            };
            List<EpubNavigationItemRef> actualNavigationItems = epubBookRef.GetNavigation();
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items asynchronously should succeed")]
        public async void GetNavigationAsyncTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRefWithNavigation(HTML_FILE_NAME, TEST_ANCHOR_TEXT);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                CreateTestNavigationLink(TEST_ANCHOR_TEXT, HTML_FILE_NAME, epubBookRef.Content.Html[HTML_FILE_NAME])
            };
            List<EpubNavigationItemRef> actualNavigationItems = await epubBookRef.GetNavigationAsync();
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Disposing EpubBookRef with EPUB file should succeed")]
        public void DisposeWithEpubFileTest()
        {
            TestZipFile testZipFile = new();
            using EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
        }

        [Fact(DisplayName = "Disposing EpubBookRef without EPUB file should succeed")]
        public void DisposeWithoutEpubFileTest()
        {
            using EpubBookRef epubBookRef = CreateEpubBookRef(null);
        }

        private EpubBookRef CreateEpubBookRefWithCover(byte[] coverFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(COVER_FILE_PATH, coverFileContent);
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile);
            epubBookRef.Content.Cover = new EpubByteContentFileRef(epubBookRef, COVER_FILE_NAME, EpubContentLocation.LOCAL, COVER_FILE_CONTENT_TYPE, COVER_FILE_CONTENT_MIME_TYPE);
            return epubBookRef;
        }

        private EpubBookRef CreateEpubBookRefWithReadingOrder(string htmlFileName)
        {
            EpubBookRef epubBookRef = CreateEpubBookRef(new TestZipFile());
            epubBookRef.Schema.Package.Spine = new EpubSpine()
            {
                Items = new List<EpubSpineItemRef>()
                {
                    new EpubSpineItemRef()
                    {
                        IdRef = "item-1"
                    }
                }
            };
            epubBookRef.Schema.Package.Manifest = new EpubManifest()
            {
                Items = new List<EpubManifestItem>()
                {
                    new EpubManifestItem()
                    {
                        Id = "item-1",
                        Href = htmlFileName,
                        MediaType = "application/xhtml+xml"
                    }
                }
            };
            EpubTextContentFileRef htmlFileRef = CreateTestHtmlFileRef(epubBookRef, htmlFileName);
            epubBookRef.Content.Html = new Dictionary<string, EpubTextContentFileRef>()
            {
                {
                    htmlFileName,
                    htmlFileRef
                }
            };
            return epubBookRef;
        }

        private EpubBookRef CreateEpubBookRefWithNavigation(string htmlFileName, string anchorText)
        {
            EpubBookRef epubBookRef = CreateEpubBookRef(new TestZipFile());
            epubBookRef.Schema.Epub3NavDocument = new Epub3NavDocument()
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
                                        Text = anchorText,
                                        Href = htmlFileName
                                    }
                                }
                            }
                        }
                    }
                }
            };
            EpubTextContentFileRef htmlFileRef = CreateTestHtmlFileRef(epubBookRef, htmlFileName);
            epubBookRef.Content = new EpubContentRef()
            {
                NavigationHtmlFile = CreateTestHtmlFileRef(epubBookRef, "toc.html"),
                Html = new Dictionary<string, EpubTextContentFileRef>()
                {
                    {
                        htmlFileName,
                        htmlFileRef
                    }
                }
            };
            return epubBookRef;
        }

        private EpubBookRef CreateEpubBookRef(TestZipFile testZipFile)
        {
            return new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    ContentDirectoryPath = CONTENT_DIRECTORY_PATH,
                    Package = new EpubPackage()
                    {
                        EpubVersion = EpubVersion.EPUB_3,
                        Metadata = new EpubMetadata()
                        {
                            Titles = new List<string>(),
                            Creators = new List<EpubMetadataCreator>(),
                            Subjects = new List<string>(),
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
                        Manifest = new EpubManifest(),
                        Spine = new EpubSpine()
                    }
                },
                Content = new EpubContentRef()
            };
        }

        private EpubTextContentFileRef CreateTestHtmlFileRef(EpubBookRef epubBookRef, string fileName)
        {
            return new(epubBookRef, fileName, EpubContentLocation.LOCAL, EpubContentType.XHTML_1_1, "application/xhtml+xml");
        }

        private EpubNavigationItemRef CreateTestNavigationLink(string title, string htmlFileUrl, EpubTextContentFileRef htmlFileRef)
        {
            EpubNavigationItemRef result = EpubNavigationItemRef.CreateAsLink();
            result.Title = title;
            result.Link = new EpubNavigationItemLink(htmlFileUrl, CONTENT_DIRECTORY_PATH);
            result.HtmlContentFileRef = htmlFileRef;
            result.NestedItems = new List<EpubNavigationItemRef>();
            return result;
        }
    }
}
