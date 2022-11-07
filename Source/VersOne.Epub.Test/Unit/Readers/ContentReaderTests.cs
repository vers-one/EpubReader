using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class ContentReaderTests
    {
        private readonly TestEnvironmentDependencies environmentDependencies;
        
        public ContentReaderTests()
        {
            environmentDependencies = new TestEnvironmentDependencies();
        }

        [Fact(DisplayName = "Constructing a content reader with non-null constructor parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            _ = new ContentReader(environmentDependencies, new EpubReaderOptions());
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if environmentDependencies parameter is null")]
        public void ConstructorWithNullEnvironmentDependenciesTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ContentReader(null, new EpubReaderOptions()));
        }

        [Fact(DisplayName = "Constructing a content reader with null EpubReaderOptions should succeed")]
        public void ConstructorWithNullContentReaderOptionsTest()
        {
            _ = new ContentReader(environmentDependencies, null);
        }

        [Fact(DisplayName = "Parsing content map from a minimal EPUB book ref should succeed")]
        public void ParseContentMapWithMinimalEpubBookRefTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(new TestZipFile());
            EpubContentRef expectedContentMap = new()
            {
                Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalTextContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                },
                Css = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalTextContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                },
                Images = new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalByteContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
                },
                Fonts = new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalByteContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
                },
                AllFiles = new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteContentFileRef>()
                },
                NavigationHtmlFile = null,
                Cover = null
            };
            ContentReader contentReader = new(environmentDependencies, new EpubReaderOptions());
            EpubContentRef actualContentMap = contentReader.ParseContentMap(epubBookRef);
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
                        Href = "https://example.com/books/123/test.css",
                        MediaType = "text/css"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-21",
                        Href = "https://example.com/books/123/image.jpg",
                        MediaType = "image/jpeg"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-22",
                        Href = "https://example.com/books/123/font.ttf",
                        MediaType = "font/truetype"
                    }
                }
            };
            EpubLocalTextContentFileRef expectedFileRef1 = CreateLocalTextFileRef("text.html", EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubLocalTextContentFileRef expectedFileRef2 = CreateLocalTextFileRef("doc.dtb", EpubContentType.DTBOOK, "application/x-dtbook+xml");
            EpubLocalTextContentFileRef expectedFileRef3 = CreateLocalTextFileRef("toc.ncx", EpubContentType.DTBOOK_NCX, "application/x-dtbncx+xml");
            EpubLocalTextContentFileRef expectedFileRef4 = CreateLocalTextFileRef("oeb.html", EpubContentType.OEB1_DOCUMENT, "text/x-oeb1-document");
            EpubLocalTextContentFileRef expectedFileRef5 = CreateLocalTextFileRef("file.xml", EpubContentType.XML, "application/xml");
            EpubLocalTextContentFileRef expectedFileRef6 = CreateLocalTextFileRef("styles.css", EpubContentType.CSS, "text/css");
            EpubLocalTextContentFileRef expectedFileRef7 = CreateLocalTextFileRef("oeb.css", EpubContentType.OEB1_CSS, "text/x-oeb1-css");
            EpubLocalByteContentFileRef expectedFileRef8 = CreateLocalByteFileRef("image1.gif", EpubContentType.IMAGE_GIF, "image/gif");
            EpubLocalByteContentFileRef expectedFileRef9 = CreateLocalByteFileRef("image2.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubLocalByteContentFileRef expectedFileRef10 = CreateLocalByteFileRef("image3.png", EpubContentType.IMAGE_PNG, "image/png");
            EpubLocalByteContentFileRef expectedFileRef11 = CreateLocalByteFileRef("image4.svg", EpubContentType.IMAGE_SVG, "image/svg+xml");
            EpubLocalByteContentFileRef expectedFileRef12 = CreateLocalByteFileRef("font1.ttf", EpubContentType.FONT_TRUETYPE, "font/truetype");
            EpubLocalByteContentFileRef expectedFileRef13 = CreateLocalByteFileRef("font2.ttf", EpubContentType.FONT_TRUETYPE, "application/x-font-truetype");
            EpubLocalByteContentFileRef expectedFileRef14 = CreateLocalByteFileRef("font3.otf", EpubContentType.FONT_OPENTYPE, "font/opentype");
            EpubLocalByteContentFileRef expectedFileRef15 = CreateLocalByteFileRef("font4.otf", EpubContentType.FONT_OPENTYPE, "application/vnd.ms-opentype");
            EpubLocalByteContentFileRef expectedFileRef16 = CreateLocalByteFileRef("video.mp4", EpubContentType.OTHER, "video/mp4");
            EpubLocalByteContentFileRef expectedFileRef17 = CreateLocalByteFileRef("cover.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubLocalTextContentFileRef expectedFileRef18 = CreateLocalTextFileRef("toc.html", EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubRemoteTextContentFileRef expectedFileRef19 = CreateRemoteTextFileRef("https://example.com/books/123/test.html", EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubRemoteTextContentFileRef expectedFileRef20 = CreateRemoteTextFileRef("https://example.com/books/123/test.css", EpubContentType.CSS, "text/css");
            EpubRemoteByteContentFileRef expectedFileRef21 = CreateRemoteByteFileRef("https://example.com/books/123/image.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubRemoteByteContentFileRef expectedFileRef22 = CreateRemoteByteFileRef("https://example.com/books/123/font.ttf", EpubContentType.FONT_TRUETYPE, "font/truetype");
            EpubContentRef expectedContentMap = new()
            {
                Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalTextContentFileRef>()
                    {
                        {
                            "text.html",
                            expectedFileRef1
                        },
                        {
                            "toc.html",
                            expectedFileRef18
                        }
                    },
                    Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.html",
                            expectedFileRef19
                        }
                    }
                },
                Css = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalTextContentFileRef>()
                    {
                        {
                            "styles.css",
                            expectedFileRef6
                        }
                    },
                    Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.css",
                            expectedFileRef20
                        }
                    }
                },
                Images = new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalByteContentFileRef>()
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
                        }
                    },
                    Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/image.jpg",
                            expectedFileRef21
                        }
                    }
                },
                Fonts = new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalByteContentFileRef>()
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
                    Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/font.ttf",
                            expectedFileRef22
                        }
                    }
                },
                AllFiles = new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalContentFileRef>()
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
                        }
                    },
                    Remote = new Dictionary<string, EpubRemoteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.html",
                            expectedFileRef19
                        },
                        {
                            "https://example.com/books/123/test.css",
                            expectedFileRef20
                        },
                        {
                            "https://example.com/books/123/image.jpg",
                            expectedFileRef21
                        },
                        {
                            "https://example.com/books/123/font.ttf",
                            expectedFileRef22
                        }
                    }
                },
                NavigationHtmlFile = expectedFileRef18,
                Cover = expectedFileRef17
            };
            ContentReader contentReader = new(environmentDependencies, new EpubReaderOptions());
            EpubContentRef actualContentMap = contentReader.ParseContentMap(epubBookRef);
            EpubContentRefComparer.CompareEpubContentRefs(expectedContentMap, actualContentMap);
        }

        [Fact(DisplayName = "ParseContentMap should throw EpubPackageException if EPUB 3 navigation document is a remote resource")]
        public void ParseContentMapWithRemoteNavigationDocumentTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(testZipFile);
            epubBookRef.Schema.Package.Manifest = new EpubManifest()
            {
                Items = new List<EpubManifestItem>()
                {
                    new EpubManifestItem()
                    {
                        Id = "item-toc",
                        Href = "https://example.com/books/123/toc.html",
                        MediaType = "application/xhtml+xml",
                        Properties = new List<EpubManifestProperty>()
                        {
                            EpubManifestProperty.NAV
                        }
                    }
                }
            };
            ContentReader contentReader = new(environmentDependencies, new EpubReaderOptions());
            Assert.Throws<EpubPackageException>(() => contentReader.ParseContentMap(epubBookRef));
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
                    },
                    ContentDirectoryPath = String.Empty
                }
            };
        }

        private EpubLocalTextContentFileRef CreateLocalTextFileRef(string fileName, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(fileName, contentType, contentMimeType), fileName, new TestEpubContentLoader());
        }

        private EpubLocalByteContentFileRef CreateLocalByteFileRef(string fileName, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(fileName, contentType, contentMimeType), fileName, new TestEpubContentLoader());
        }

        private EpubRemoteTextContentFileRef CreateRemoteTextFileRef(string href, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(href, contentType, contentMimeType), new TestEpubContentLoader());
        }

        private EpubRemoteByteContentFileRef CreateRemoteByteFileRef(string href, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(href, contentType, contentMimeType), new TestEpubContentLoader());
        }
    }
}
