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
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef();
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
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef();
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
            EpubTextContentFileRef expectedFileRef1 = new(epubBookRef, "text.html", EpubContentLocation.LOCAL, EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubTextContentFileRef expectedFileRef2 = new(epubBookRef, "doc.dtb", EpubContentLocation.LOCAL, EpubContentType.DTBOOK, "application/x-dtbook+xml");
            EpubTextContentFileRef expectedFileRef3 = new(epubBookRef, "toc.ncx", EpubContentLocation.LOCAL, EpubContentType.DTBOOK_NCX, "application/x-dtbncx+xml");
            EpubTextContentFileRef expectedFileRef4 = new(epubBookRef, "oeb.html", EpubContentLocation.LOCAL, EpubContentType.OEB1_DOCUMENT, "text/x-oeb1-document");
            EpubTextContentFileRef expectedFileRef5 = new(epubBookRef, "file.xml", EpubContentLocation.LOCAL, EpubContentType.XML, "application/xml");
            EpubTextContentFileRef expectedFileRef6 = new(epubBookRef, "styles.css", EpubContentLocation.LOCAL, EpubContentType.CSS, "text/css");
            EpubTextContentFileRef expectedFileRef7 = new(epubBookRef, "oeb.css", EpubContentLocation.LOCAL, EpubContentType.OEB1_CSS, "text/x-oeb1-css");
            EpubByteContentFileRef expectedFileRef8 = new(epubBookRef, "image1.gif", EpubContentLocation.LOCAL, EpubContentType.IMAGE_GIF, "image/gif");
            EpubByteContentFileRef expectedFileRef9 = new(epubBookRef, "image2.jpg", EpubContentLocation.LOCAL, EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubByteContentFileRef expectedFileRef10 = new(epubBookRef, "image3.png", EpubContentLocation.LOCAL, EpubContentType.IMAGE_PNG, "image/png");
            EpubByteContentFileRef expectedFileRef11 = new(epubBookRef, "image4.svg", EpubContentLocation.LOCAL, EpubContentType.IMAGE_SVG, "image/svg+xml");
            EpubByteContentFileRef expectedFileRef12 = new(epubBookRef, "font1.ttf", EpubContentLocation.LOCAL, EpubContentType.FONT_TRUETYPE, "font/truetype");
            EpubByteContentFileRef expectedFileRef13 = new(epubBookRef, "font2.ttf", EpubContentLocation.LOCAL, EpubContentType.FONT_TRUETYPE, "application/x-font-truetype");
            EpubByteContentFileRef expectedFileRef14 = new(epubBookRef, "font3.otf", EpubContentLocation.LOCAL, EpubContentType.FONT_OPENTYPE, "font/opentype");
            EpubByteContentFileRef expectedFileRef15 = new(epubBookRef, "font4.otf", EpubContentLocation.LOCAL, EpubContentType.FONT_OPENTYPE, "application/vnd.ms-opentype");
            EpubByteContentFileRef expectedFileRef16 = new(epubBookRef, "video.mp4", EpubContentLocation.LOCAL, EpubContentType.OTHER, "video/mp4");
            EpubByteContentFileRef expectedFileRef17 = new(epubBookRef, "cover.jpg", EpubContentLocation.LOCAL, EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubTextContentFileRef expectedFileRef18 = new(epubBookRef, "toc.html", EpubContentLocation.LOCAL, EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubTextContentFileRef expectedFileRef19 =
                new(epubBookRef, "https://example.com/books/123/test.html", EpubContentLocation.REMOTE, EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubByteContentFileRef expectedFileRef20 =
                new(epubBookRef, "https://example.com/books/123/image.jpg", EpubContentLocation.REMOTE, EpubContentType.IMAGE_JPEG, "image/jpeg");
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

        private EpubBookRef CreateEmptyEpubBookRef()
        {
            return new(new TestZipFile())
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
    }
}
