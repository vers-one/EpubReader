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
            Assert.Throws<ArgumentNullException>(() => new ContentReader(null!, new EpubReaderOptions()));
        }

        [Fact(DisplayName = "Constructing a content reader with null EpubReaderOptions should succeed")]
        public void ConstructorWithNullContentReaderOptionsTest()
        {
            _ = new ContentReader(environmentDependencies, null);
        }

        [Fact(DisplayName = "Parsing content map from a minimal EPUB schema should succeed")]
        public void ParseContentMapWithMinimalEpubSchemaTest()
        {
            EpubSchema epubSchema = CreateEpubSchema();
            EpubContentRef expectedContentMap = new();
            ContentReader contentReader = new(environmentDependencies);
            EpubContentRef actualContentMap = contentReader.ParseContentMap(epubSchema, new TestZipFile());
            EpubContentRefComparer.CompareEpubContentRefs(expectedContentMap, actualContentMap);
        }

        [Fact(DisplayName = "Parsing content map from a full EPUB schema should succeed")]
        public void ParseContentMapWithFullEpubSchemaTest()
        {
            EpubSchema epubSchema = CreateEpubSchema
            (
                manifest: new EpubManifest
                (
                    items: new List<EpubManifestItem>()
                    {
                        new EpubManifestItem
                        (
                            id: "item-1",
                            href: "text.html",
                            mediaType: "application/xhtml+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-2",
                            href: "doc.dtb",
                            mediaType: "application/x-dtbook+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-3",
                            href: "toc.ncx",
                            mediaType: "application/x-dtbncx+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-4",
                            href: "oeb.html",
                            mediaType: "text/x-oeb1-document"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-5",
                            href: "file.xml",
                            mediaType: "application/xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-6",
                            href: "styles.css",
                            mediaType: "text/css"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-7",
                            href: "oeb.css",
                            mediaType: "text/x-oeb1-css"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-8",
                            href: "image1.gif",
                            mediaType: "image/gif"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-9",
                            href: "image2.jpg",
                            mediaType: "image/jpeg"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-10",
                            href: "image3.png",
                            mediaType: "image/png"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-11",
                            href: "image4.svg",
                            mediaType: "image/svg+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-12",
                            href: "font1.ttf",
                            mediaType: "font/truetype"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-13",
                            href: "font2.ttf",
                            mediaType: "application/x-font-truetype"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-14",
                            href: "font3.otf",
                            mediaType: "font/opentype"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-15",
                            href: "font4.otf",
                            mediaType: "application/vnd.ms-opentype"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-16",
                            href: "narration.smil",
                            mediaType: "application/smil+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-17",
                            href: "video.mp4",
                            mediaType: "video/mp4"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-18",
                            href: "cover.jpg",
                            mediaType: "image/jpeg",
                            properties: new List<EpubManifestProperty>
                            {
                                EpubManifestProperty.COVER_IMAGE
                            }
                        ),
                        new EpubManifestItem
                        (
                            id: "item-19",
                            href: "toc.html",
                            mediaType: "application/xhtml+xml",
                            properties: new List<EpubManifestProperty>
                            {
                                EpubManifestProperty.NAV
                            }
                        ),
                        new EpubManifestItem
                        (
                            id: "item-20",
                            href: "https://example.com/books/123/test.html",
                            mediaType: "application/xhtml+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-21",
                            href: "https://example.com/books/123/test.css",
                            mediaType: "text/css"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-22",
                            href: "https://example.com/books/123/image.jpg",
                            mediaType: "image/jpeg"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-23",
                            href: "https://example.com/books/123/font.ttf",
                            mediaType: "font/truetype"
                        )
                    }
                )
            );
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
            EpubLocalTextContentFileRef expectedFileRef16 = CreateLocalTextFileRef("narration.smil", EpubContentType.SMIL, "application/smil+xml");
            EpubLocalByteContentFileRef expectedFileRef17 = CreateLocalByteFileRef("video.mp4", EpubContentType.OTHER, "video/mp4");
            EpubLocalByteContentFileRef expectedFileRef18 = CreateLocalByteFileRef("cover.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubLocalTextContentFileRef expectedFileRef19 = CreateLocalTextFileRef("toc.html", EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubRemoteTextContentFileRef expectedFileRef20 = CreateRemoteTextFileRef("https://example.com/books/123/test.html", EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubRemoteTextContentFileRef expectedFileRef21 = CreateRemoteTextFileRef("https://example.com/books/123/test.css", EpubContentType.CSS, "text/css");
            EpubRemoteByteContentFileRef expectedFileRef22 = CreateRemoteByteFileRef("https://example.com/books/123/image.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubRemoteByteContentFileRef expectedFileRef23 = CreateRemoteByteFileRef("https://example.com/books/123/font.ttf", EpubContentType.FONT_TRUETYPE, "font/truetype");
            EpubContentRef expectedContentMap = new
            (
                cover: expectedFileRef18,
                navigationHtmlFile: expectedFileRef19,
                html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalTextContentFileRef>()
                    {
                        {
                            "text.html",
                            expectedFileRef1
                        },
                        {
                            "toc.html",
                            expectedFileRef19
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.html",
                            expectedFileRef20
                        }
                    }
                ),
                css: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalTextContentFileRef>()
                    {
                        {
                            "styles.css",
                            expectedFileRef6
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.css",
                            expectedFileRef21
                        }
                    }
                ),
                images: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalByteContentFileRef>()
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
                            expectedFileRef18
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/image.jpg",
                            expectedFileRef22
                        }
                    }
                ),
                fonts: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalByteContentFileRef>()
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
                    remote: new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/font.ttf",
                            expectedFileRef23
                        }
                    }
                ),
                allFiles: new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalContentFileRef>()
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
                            "narration.smil",
                            expectedFileRef16
                        },
                        {
                            "video.mp4",
                            expectedFileRef17
                        },
                        {
                            "cover.jpg",
                            expectedFileRef18
                        },
                        {
                            "toc.html",
                            expectedFileRef19
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.html",
                            expectedFileRef20
                        },
                        {
                            "https://example.com/books/123/test.css",
                            expectedFileRef21
                        },
                        {
                            "https://example.com/books/123/image.jpg",
                            expectedFileRef22
                        },
                        {
                            "https://example.com/books/123/font.ttf",
                            expectedFileRef23
                        }
                    }
                )
            );
            ContentReader contentReader = new(environmentDependencies);
            EpubContentRef actualContentMap = contentReader.ParseContentMap(epubSchema, new TestZipFile());
            EpubContentRefComparer.CompareEpubContentRefs(expectedContentMap, actualContentMap);
        }

        [Fact(DisplayName = "Parsing content map from a EPUB schema with only remote text content files should succeed")]
        public void ParseContentMapWithRemoteTextContentFilesTest()
        {
            EpubSchema epubSchema = CreateEpubSchema
            (
                manifest: new EpubManifest
                (
                    items: new List<EpubManifestItem>()
                    {
                        new EpubManifestItem
                        (
                            id: "item-1",
                            href: "https://example.com/books/123/test.html",
                            mediaType: "application/xhtml+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-2",
                            href: "https://example.com/books/123/test.css",
                            mediaType: "text/css"
                        )
                    }
                )
            );
            EpubRemoteTextContentFileRef expectedFileRef1 = CreateRemoteTextFileRef("https://example.com/books/123/test.html", EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubRemoteTextContentFileRef expectedFileRef2 = CreateRemoteTextFileRef("https://example.com/books/123/test.css", EpubContentType.CSS, "text/css");
            EpubContentRef expectedContentMap = new
            (
                html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>
                (
                    remote: new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.html",
                            expectedFileRef1
                        }
                    }
                ),
                css: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>
                (
                    remote: new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.css",
                            expectedFileRef2
                        }
                    }
                ),
                allFiles: new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>
                (
                    remote: new Dictionary<string, EpubRemoteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.html",
                            expectedFileRef1
                        },
                        {
                            "https://example.com/books/123/test.css",
                            expectedFileRef2
                        }
                    }
                )
            );
            ContentReader contentReader = new(environmentDependencies);
            EpubContentRef actualContentMap = contentReader.ParseContentMap(epubSchema, new TestZipFile());
            EpubContentRefComparer.CompareEpubContentRefs(expectedContentMap, actualContentMap);
        }

        [Fact(DisplayName = "Parsing content map from a EPUB schema with only remote byte content files should succeed")]
        public void ParseContentMapWithRemoteByteContentFilesTest()
        {
            EpubSchema epubSchema = CreateEpubSchema
            (
                manifest: new EpubManifest
                (
                    items: new List<EpubManifestItem>()
                    {
                        new EpubManifestItem
                        (
                            id: "item-1",
                            href: "https://example.com/books/123/image.jpg",
                            mediaType: "image/jpeg"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-2",
                            href: "https://example.com/books/123/font.ttf",
                            mediaType: "font/truetype"
                        )
                    }
                )
            );
            EpubRemoteByteContentFileRef expectedFileRef1 = CreateRemoteByteFileRef("https://example.com/books/123/image.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubRemoteByteContentFileRef expectedFileRef2 = CreateRemoteByteFileRef("https://example.com/books/123/font.ttf", EpubContentType.FONT_TRUETYPE, "font/truetype");
            EpubContentRef expectedContentMap = new
            (
                images: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>
                (
                    remote: new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/image.jpg",
                            expectedFileRef1
                        }
                    }
                ),
                fonts: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>
                (
                    remote: new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/font.ttf",
                            expectedFileRef2
                        }
                    }
                ),
                allFiles: new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>
                (
                    remote: new Dictionary<string, EpubRemoteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/image.jpg",
                            expectedFileRef1
                        },
                        {
                            "https://example.com/books/123/font.ttf",
                            expectedFileRef2
                        }
                    }
                )
            );
            ContentReader contentReader = new(environmentDependencies);
            EpubContentRef actualContentMap = contentReader.ParseContentMap(epubSchema, new TestZipFile());
            EpubContentRefComparer.CompareEpubContentRefs(expectedContentMap, actualContentMap);
        }

        [Fact(DisplayName = "ParseContentMap should throw EpubPackageException if EPUB 3 navigation document is a remote resource")]
        public void ParseContentMapWithRemoteNavigationDocumentTest()
        {
            EpubSchema epubSchema = CreateEpubSchema
            (
                manifest: new EpubManifest
                (
                    items: new List<EpubManifestItem>()
                    {
                        new EpubManifestItem
                        (
                            id: "item-toc",
                            href: "http://example.com/toc.html",
                            mediaType: "application/xhtml+xml",
                            properties: new List<EpubManifestProperty>
                            {
                                EpubManifestProperty.NAV
                            }
                        )
                    }
                )
            );
            ContentReader contentReader = new(environmentDependencies);
            Assert.Throws<EpubPackageException>(() => contentReader.ParseContentMap(epubSchema, new TestZipFile()));
        }

        private static EpubSchema CreateEpubSchema(EpubManifest? manifest = null)
        {
            return new
            (
                package: new EpubPackage
                (
                    epubVersion: EpubVersion.EPUB_3,
                    metadata: new EpubMetadata(),
                    manifest: manifest ?? new EpubManifest(),
                    spine: new EpubSpine(),
                    guide: null
                ),
                epub2Ncx: null,
                epub3NavDocument: null,
                mediaOverlays: null,
                contentDirectoryPath: String.Empty
            );
        }

        private static EpubLocalTextContentFileRef CreateLocalTextFileRef(string fileName, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(fileName, contentType, contentMimeType), fileName, new TestEpubContentLoader());
        }

        private static EpubLocalByteContentFileRef CreateLocalByteFileRef(string fileName, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(fileName, contentType, contentMimeType), fileName, new TestEpubContentLoader());
        }

        private static EpubRemoteTextContentFileRef CreateRemoteTextFileRef(string href, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(href, contentType, contentMimeType), new TestEpubContentLoader());
        }

        private static EpubRemoteByteContentFileRef CreateRemoteByteFileRef(string href, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(href, contentType, contentMimeType), new TestEpubContentLoader());
        }
    }
}
