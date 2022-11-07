using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

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
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile, EpubVersion.EPUB_3);
            byte[] coverContent = epubBookRef.ReadCover();
            Assert.Null(coverContent);
        }

        [Fact(DisplayName = "ReadCoverAsync should return null if the cover is missing")]
        public async void ReadCoverAsyncWithoutCoverTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile, EpubVersion.EPUB_3);
            byte[] coverContent = await epubBookRef.ReadCoverAsync();
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
            List<EpubNavigationItemRef> actualNavigationItems = epubBookRef.GetNavigation();
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
            List<EpubNavigationItemRef> actualNavigationItems = await epubBookRef.GetNavigationAsync();
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "GetNavigation should return null for EPUB 2 books with no navigation")]
        public void GetNavigationForEpub2WithNoNavigationTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRef(new TestZipFile(), EpubVersion.EPUB_2);
            List<EpubNavigationItemRef> navigationItems = epubBookRef.GetNavigation();
            Assert.Null(navigationItems);
        }

        [Fact(DisplayName = "GetNavigation should return null for EPUB 3 books with no navigation")]
        public void GetNavigationForEpub3WithNoNavigationTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRef(new TestZipFile(), EpubVersion.EPUB_3);
            List<EpubNavigationItemRef> navigationItems = epubBookRef.GetNavigation();
            Assert.Null(navigationItems);
        }

        [Fact(DisplayName = "GetNavigationAsync should return null for EPUB 2 books with no navigation")]
        public async void GetNavigationAsyncForEpub2WithNoNavigationTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRef(new TestZipFile(), EpubVersion.EPUB_2);
            List<EpubNavigationItemRef> navigationItems = await epubBookRef.GetNavigationAsync();
            Assert.Null(navigationItems);
        }

        [Fact(DisplayName = "GetNavigationAsync should return null for EPUB 3 books with no navigation")]
        public async void GetNavigationAsyncForEpub3WithNoNavigationTest()
        {
            EpubBookRef epubBookRef = CreateEpubBookRef(new TestZipFile(), EpubVersion.EPUB_3);
            List<EpubNavigationItemRef> navigationItems = await epubBookRef.GetNavigationAsync();
            Assert.Null(navigationItems);
        }

        [Fact(DisplayName = "Disposing EpubBookRef with EPUB file should succeed")]
        public void DisposeWithEpubFileTest()
        {
            TestZipFile testZipFile = new();
            using EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile, EpubVersion.EPUB_3);
        }

        [Fact(DisplayName = "Disposing EpubBookRef without EPUB file should succeed")]
        public void DisposeWithoutEpubFileTest()
        {
            using EpubBookRef epubBookRef = CreateEpubBookRef(null, EpubVersion.EPUB_3);
        }

        private EpubBookRef CreateEpubBookRefWithCover(byte[] coverFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(COVER_FILE_PATH, coverFileContent);
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile, EpubVersion.EPUB_3);
            EpubContentFileRefMetadata coverFileRefMetadata = new(COVER_FILE_NAME, COVER_FILE_CONTENT_TYPE, COVER_FILE_CONTENT_MIME_TYPE);
            epubBookRef.Content.Cover = new EpubLocalByteContentFileRef(coverFileRefMetadata, COVER_FILE_PATH, new TestEpubContentLoader(coverFileContent));
            return epubBookRef;
        }

        private EpubBookRef CreateEpubBookRefWithReadingOrder(string htmlFileName, string htmlFilePath)
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile, EpubVersion.EPUB_3);
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
            EpubLocalTextContentFileRef htmlFileRef = CreateTestHtmlFileRef(htmlFileName, htmlFilePath);
            epubBookRef.Content.Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
            {
                Local = new Dictionary<string, EpubLocalTextContentFileRef>()
                {
                    {
                        htmlFileName,
                        htmlFileRef
                    }
                },
                Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
            };
            return epubBookRef;
        }

        private EpubBookRef CreateEpubBookRefWithNavigation(string htmlFileName, string htmlFilePath, string anchorText)
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEpubBookRef(testZipFile, EpubVersion.EPUB_3);
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
            EpubLocalTextContentFileRef htmlFileRef = CreateTestHtmlFileRef(htmlFileName, htmlFilePath);
            epubBookRef.Content = new EpubContentRef()
            {
                NavigationHtmlFile = CreateTestHtmlFileRef("toc.html", $"{CONTENT_DIRECTORY_PATH}/toc.html"),
                Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalTextContentFileRef>()
                    {
                        {
                            htmlFileName,
                            htmlFileRef
                        }
                    },
                    Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                }
            };
            return epubBookRef;
        }

        private EpubBookRef CreateEpubBookRef(TestZipFile testZipFile, EpubVersion epubVersion)
        {
            return new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    ContentDirectoryPath = CONTENT_DIRECTORY_PATH,
                    Package = new EpubPackage()
                    {
                        EpubVersion = epubVersion,
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

        private EpubLocalTextContentFileRef CreateTestHtmlFileRef(string fileName, string filePath)
        {
            EpubContentFileRefMetadata htmlFileRefMetadata = new(fileName, EpubContentType.XHTML_1_1, "application/xhtml+xml");
            return new(htmlFileRefMetadata, filePath, new TestEpubContentLoader());
        }

        private EpubNavigationItemRef CreateTestNavigationLink(string title, string htmlFileUrl, EpubLocalTextContentFileRef htmlFileRef)
        {
            return new()
            {
                Type = EpubNavigationItemType.LINK,
                Title = title,
                Link = new EpubNavigationItemLink(htmlFileUrl, CONTENT_DIRECTORY_PATH),
                HtmlContentFileRef = htmlFileRef,
                NestedItems = new List<EpubNavigationItemRef>()
            };
        }
    }
}
