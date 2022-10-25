using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class ContentReaderTests
    {
        [Fact(DisplayName = "Parsing content map from a minimal EPUB book ref should succeed")]
        public void ParseContentMapWithMinimalEpubBookRefTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(new TestZipFile());
            EpubContentRef expectedContentMap = new()
            {
                Html = new Dictionary<string, EpubTextContentFileRef>(),
                Css = new Dictionary<string, EpubTextContentFileRef>(),
                Images = new Dictionary<string, EpubByteContentFileRef>(),
                Fonts = new Dictionary<string, EpubByteContentFileRef>(),
                AllFiles = new Dictionary<string, EpubContentFileRef>(),
                NavigationHtmlFile = null,
                Cover = null
            };
            EpubContentRef actualContentMap = ContentReader.ParseContentMap(epubBookRef, new ContentReaderOptions());
            EpubContentRefComparer.CompareEpubContentRefs(expectedContentMap, actualContentMap);
        }

        [Fact(DisplayName = "Parsing content map from a full EPUB book ref should succeed")]
        public void ParseContentMapWithFullEpubBookRefTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(testZipFile);
            epubBookRef.Schema.Package.Manifest = new EpubManifest()
            {
                Items = new List<EpubManifestItem>()
                {
                    new EpubManifestItem()
                    {
                        Id = "item-1",
                        Href = "text.html",
                        MediaType = "application/xhtml+xml"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-2",
                        Href = "doc.dtb",
                        MediaType = "application/x-dtbook+xml"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-3",
                        Href = "toc.ncx",
                        MediaType = "application/x-dtbncx+xml"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-4",
                        Href = "oeb.html",
                        MediaType = "text/x-oeb1-document"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-5",
                        Href = "file.xml",
                        MediaType = "application/xml"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-6",
                        Href = "styles.css",
                        MediaType = "text/css"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-7",
                        Href = "oeb.css",
                        MediaType = "text/x-oeb1-css"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-8",
                        Href = "image1.gif",
                        MediaType = "image/gif"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-9",
                        Href = "image2.jpg",
                        MediaType = "image/jpeg"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-10",
                        Href = "image3.png",
                        MediaType = "image/png"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-11",
                        Href = "image4.svg",
                        MediaType = "image/svg+xml"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-12",
                        Href = "font1.ttf",
                        MediaType = "font/truetype"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-13",
                        Href = "font2.ttf",
                        MediaType = "application/x-font-truetype"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-14",
                        Href = "font3.otf",
                        MediaType = "font/opentype"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-15",
                        Href = "font4.otf",
                        MediaType = "application/vnd.ms-opentype"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-16",
                        Href = "video.mp4",
                        MediaType = "video/mp4"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-17",
                        Href = "cover.jpg",
                        MediaType = "image/jpeg",
                        Properties = new List<EpubManifestProperty>()
                        {
                            EpubManifestProperty.COVER_IMAGE
                        }
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-18",
                        Href = "toc.html",
                        MediaType = "application/xhtml+xml",
                        Properties = new List<EpubManifestProperty>()
                        {
                            EpubManifestProperty.NAV
                        }
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-19",
                        Href = "https://example.com/books/123/test.html",
                        MediaType = "application/xhtml+xml"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-20",
                        Href = "https://example.com/books/123/image.jpg",
                        MediaType = "image/jpeg"
                    }
                }
            };
            EpubTextContentFileRef expectedFileRef1 = CreateLocalTextFileRef(testZipFile, "text.html", EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubTextContentFileRef expectedFileRef2 = CreateLocalTextFileRef(testZipFile, "doc.dtb", EpubContentType.DTBOOK, "application/x-dtbook+xml");
            EpubTextContentFileRef expectedFileRef3 = CreateLocalTextFileRef(testZipFile, "toc.ncx", EpubContentType.DTBOOK_NCX, "application/x-dtbncx+xml");
            EpubTextContentFileRef expectedFileRef4 = CreateLocalTextFileRef(testZipFile, "oeb.html", EpubContentType.OEB1_DOCUMENT, "text/x-oeb1-document");
            EpubTextContentFileRef expectedFileRef5 = CreateLocalTextFileRef(testZipFile, "file.xml", EpubContentType.XML, "application/xml");
            EpubTextContentFileRef expectedFileRef6 = CreateLocalTextFileRef(testZipFile, "styles.css", EpubContentType.CSS, "text/css");
            EpubTextContentFileRef expectedFileRef7 = CreateLocalTextFileRef(testZipFile, "oeb.css", EpubContentType.OEB1_CSS, "text/x-oeb1-css");
            EpubByteContentFileRef expectedFileRef8 = CreateLocalByteFileRef(testZipFile, "image1.gif", EpubContentType.IMAGE_GIF, "image/gif");
            EpubByteContentFileRef expectedFileRef9 = CreateLocalByteFileRef(testZipFile, "image2.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubByteContentFileRef expectedFileRef10 = CreateLocalByteFileRef(testZipFile, "image3.png", EpubContentType.IMAGE_PNG, "image/png");
            EpubByteContentFileRef expectedFileRef11 = CreateLocalByteFileRef(testZipFile, "image4.svg", EpubContentType.IMAGE_SVG, "image/svg+xml");
            EpubByteContentFileRef expectedFileRef12 = CreateLocalByteFileRef(testZipFile, "font1.ttf", EpubContentType.FONT_TRUETYPE, "font/truetype");
            EpubByteContentFileRef expectedFileRef13 = CreateLocalByteFileRef(testZipFile, "font2.ttf", EpubContentType.FONT_TRUETYPE, "application/x-font-truetype");
            EpubByteContentFileRef expectedFileRef14 = CreateLocalByteFileRef(testZipFile, "font3.otf", EpubContentType.FONT_OPENTYPE, "font/opentype");
            EpubByteContentFileRef expectedFileRef15 = CreateLocalByteFileRef(testZipFile, "font4.otf", EpubContentType.FONT_OPENTYPE, "application/vnd.ms-opentype");
            EpubByteContentFileRef expectedFileRef16 = CreateLocalByteFileRef(testZipFile, "video.mp4", EpubContentType.OTHER, "video/mp4");
            EpubByteContentFileRef expectedFileRef17 = CreateLocalByteFileRef(testZipFile, "cover.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubTextContentFileRef expectedFileRef18 = CreateLocalTextFileRef(testZipFile, "toc.html", EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubTextContentFileRef expectedFileRef19 =
                new("https://example.com/books/123/test.html", EpubContentLocation.REMOTE, EpubContentType.XHTML_1_1, "application/xhtml+xml", testZipFile, String.Empty);
            EpubByteContentFileRef expectedFileRef20 =
                new("https://example.com/books/123/image.jpg", EpubContentLocation.REMOTE, EpubContentType.IMAGE_JPEG, "image/jpeg", testZipFile, String.Empty);
            EpubContentRef expectedContentMap = new()
            {
                Html = new Dictionary<string, EpubTextContentFileRef>()
                {
                    {
                        "text.html",
                        expectedFileRef1
                    },
                    {
                        "toc.html",
                        expectedFileRef18
                    },
                    {
                        "https://example.com/books/123/test.html",
                        expectedFileRef19
                    }
                },
                Css = new Dictionary<string, EpubTextContentFileRef>()
                {
                    {
                        "styles.css",
                        expectedFileRef6
                    }
                },
                Images = new Dictionary<string, EpubByteContentFileRef>()
                {
                    {
                        "image1.gif",
                        expectedFileRef8
                    },
                    {
                        "image2.jpg",
                        expectedFileRef9
                    },
                    {
                        "image3.png",
                        expectedFileRef10
                    },
                    {
                        "image4.svg",
                        expectedFileRef11
                    },
                    {
                        "cover.jpg",
                        expectedFileRef17
                    },
                    {
                        "https://example.com/books/123/image.jpg",
                        expectedFileRef20
                    }
                },
                Fonts = new Dictionary<string, EpubByteContentFileRef>()
                {
                    {
                        "font1.ttf",
                        expectedFileRef12
                    },
                    {
                        "font2.ttf",
                        expectedFileRef13
                    },
                    {
                        "font3.otf",
                        expectedFileRef14
                    },
                    {
                        "font4.otf",
                        expectedFileRef15
                    }
                },
                AllFiles = new Dictionary<string, EpubContentFileRef>()
                {
                    {
                        "text.html",
                        expectedFileRef1
                    },
                    {
                        "doc.dtb",
                        expectedFileRef2
                    },
                    {
                        "toc.ncx",
                        expectedFileRef3
                    },
                    {
                        "oeb.html",
                        expectedFileRef4
                    },
                    {
                        "file.xml",
                        expectedFileRef5
                    },
                    {
                        "styles.css",
                        expectedFileRef6
                    },
                    {
                        "oeb.css",
                        expectedFileRef7
                    },
                    {
                        "image1.gif",
                        expectedFileRef8
                    },
                    {
                        "image2.jpg",
                        expectedFileRef9
                    },
                    {
                        "image3.png",
                        expectedFileRef10
                    },
                    {
                        "image4.svg",
                        expectedFileRef11
                    },
                    {
                        "font1.ttf",
                        expectedFileRef12
                    },
                    {
                        "font2.ttf",
                        expectedFileRef13 
                    },
                    {
                        "font3.otf",
                        expectedFileRef14
                    },
                    {
                        "font4.otf",
                        expectedFileRef15
                    },
                    {
                        "video.mp4",
                        expectedFileRef16
                    },
                    {
                        "cover.jpg",
                        expectedFileRef17
                    },
                    {
                        "toc.html",
                        expectedFileRef18
                    },
                    {
                        "https://example.com/books/123/test.html",
                        expectedFileRef19
                    },
                    {
                        "https://example.com/books/123/image.jpg",
                        expectedFileRef20
                    }
                },
                NavigationHtmlFile = expectedFileRef18,
                Cover = expectedFileRef17
            };
            EpubContentRef actualContentMap = ContentReader.ParseContentMap(epubBookRef, new ContentReaderOptions());
            EpubContentRefComparer.CompareEpubContentRefs(expectedContentMap, actualContentMap);
        }

        private EpubBookRef CreateEmptyEpubBookRef(TestZipFile testZipFile)
        {
            return new(testZipFile)
            {
                Schema = new EpubSchema()
                {
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
                        Manifest = new EpubManifest()
                        {
                            Items = new List<EpubManifestItem>()
                        },
                        Spine = new EpubSpine()
                    }
                }
            };
        }

        private EpubTextContentFileRef CreateLocalTextFileRef(TestZipFile testZipFile, string href, EpubContentType contentType, string contentMimeType)
        {
            return new(href, EpubContentLocation.LOCAL, contentType, contentMimeType, testZipFile, String.Empty);
        }

        private EpubByteContentFileRef CreateLocalByteFileRef(TestZipFile testZipFile, string href, EpubContentType contentType, string contentMimeType)
        {
            return new(href, EpubContentLocation.LOCAL, contentType, contentMimeType, testZipFile, String.Empty);
        }
    }
}
