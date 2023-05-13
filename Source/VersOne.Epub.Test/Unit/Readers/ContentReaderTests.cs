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
                            id: "item-html",
                            href: "text.html",
                            mediaType: "application/xhtml+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-dtb",
                            href: "doc.dtb",
                            mediaType: "application/x-dtbook+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-ncx",
                            href: "toc.ncx",
                            mediaType: "application/x-dtbncx+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-oeb",
                            href: "oeb.html",
                            mediaType: "text/x-oeb1-document"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-xml",
                            href: "file.xml",
                            mediaType: "application/xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-css",
                            href: "styles.css",
                            mediaType: "text/css"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-oebcss",
                            href: "oeb.css",
                            mediaType: "text/x-oeb1-css"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-js-1",
                            href: "script1.js",
                            mediaType: "application/javascript"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-js-2",
                            href: "script2.js",
                            mediaType: "application/ecmascript"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-js-3",
                            href: "script3.js",
                            mediaType: "text/javascript"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-gif",
                            href: "image.gif",
                            mediaType: "image/gif"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-jpg",
                            href: "image.jpg",
                            mediaType: "image/jpeg"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-png",
                            href: "image.png",
                            mediaType: "image/png"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-svg",
                            href: "image.svg",
                            mediaType: "image/svg+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-webp",
                            href: "image.webp",
                            mediaType: "image/webp"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-ttf-1",
                            href: "font1.ttf",
                            mediaType: "font/truetype"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-ttf-2",
                            href: "font2.ttf",
                            mediaType: "font/ttf"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-ttf-3",
                            href: "font3.ttf",
                            mediaType: "application/x-font-truetype"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-otf-1",
                            href: "font1.otf",
                            mediaType: "font/opentype"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-otf-2",
                            href: "font2.otf",
                            mediaType: "font/otf"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-otf-3",
                            href: "font3.otf",
                            mediaType: "application/vnd.ms-opentype"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-sfnt-1",
                            href: "font.aat",
                            mediaType: "font/sfnt"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-sfnt-2",
                            href: "font.sil",
                            mediaType: "application/font-sfnt"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-woff-1",
                            href: "font1.woff",
                            mediaType: "font/woff"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-woff-2",
                            href: "font2.woff",
                            mediaType: "application/font-woff"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-woff2",
                            href: "font.woff2",
                            mediaType: "font/woff2"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-smil",
                            href: "narration.smil",
                            mediaType: "application/smil+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-mp3",
                            href: "audio.mp3",
                            mediaType: "audio/mpeg"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-mp4",
                            href: "audio.mp4",
                            mediaType: "audio/mp4"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-ogg-1",
                            href: "audio1.opus",
                            mediaType: "audio/ogg"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-ogg-2",
                            href: "audio2.opus",
                            mediaType: "audio/ogg; codecs=opus"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-video",
                            href: "video.mp4",
                            mediaType: "video/mp4"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-cover",
                            href: "cover.jpg",
                            mediaType: "image/jpeg",
                            properties: new List<EpubManifestProperty>
                            {
                                EpubManifestProperty.COVER_IMAGE
                            }
                        ),
                        new EpubManifestItem
                        (
                            id: "item-toc",
                            href: "toc.html",
                            mediaType: "application/xhtml+xml",
                            properties: new List<EpubManifestProperty>
                            {
                                EpubManifestProperty.NAV
                            }
                        ),
                        new EpubManifestItem
                        (
                            id: "item-remote-html",
                            href: "https://example.com/books/123/test.html",
                            mediaType: "application/xhtml+xml"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-remote-css",
                            href: "https://example.com/books/123/test.css",
                            mediaType: "text/css"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-remote-jpg",
                            href: "https://example.com/books/123/image.jpg",
                            mediaType: "image/jpeg"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-remote-ttf",
                            href: "https://example.com/books/123/font.ttf",
                            mediaType: "font/truetype"
                        ),
                        new EpubManifestItem
                        (
                            id: "item-remote-mp3",
                            href: "https://example.com/books/123/audio.mp3",
                            mediaType: "audio/mpeg"
                        )
                    }
                )
            );
            EpubLocalTextContentFileRef expectedHtmlFileRef = CreateLocalTextFileRef("text.html", EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubLocalTextContentFileRef expectedDtbFileRef = CreateLocalTextFileRef("doc.dtb", EpubContentType.DTBOOK, "application/x-dtbook+xml");
            EpubLocalTextContentFileRef expectedNcxFileRef = CreateLocalTextFileRef("toc.ncx", EpubContentType.DTBOOK_NCX, "application/x-dtbncx+xml");
            EpubLocalTextContentFileRef expectedOebFileRef = CreateLocalTextFileRef("oeb.html", EpubContentType.OEB1_DOCUMENT, "text/x-oeb1-document");
            EpubLocalTextContentFileRef expectedXmlFileRef = CreateLocalTextFileRef("file.xml", EpubContentType.XML, "application/xml");
            EpubLocalTextContentFileRef expectedCssFileRef = CreateLocalTextFileRef("styles.css", EpubContentType.CSS, "text/css");
            EpubLocalTextContentFileRef expectedOebCssFileRef = CreateLocalTextFileRef("oeb.css", EpubContentType.OEB1_CSS, "text/x-oeb1-css");
            EpubLocalTextContentFileRef expectedJs1FileRef = CreateLocalTextFileRef("script1.js", EpubContentType.SCRIPT, "application/javascript");
            EpubLocalTextContentFileRef expectedJs2FileRef = CreateLocalTextFileRef("script2.js", EpubContentType.SCRIPT, "application/ecmascript");
            EpubLocalTextContentFileRef expectedJs3FileRef = CreateLocalTextFileRef("script3.js", EpubContentType.SCRIPT, "text/javascript");
            EpubLocalByteContentFileRef expectedGifFileRef = CreateLocalByteFileRef("image.gif", EpubContentType.IMAGE_GIF, "image/gif");
            EpubLocalByteContentFileRef expectedJpgFileRef = CreateLocalByteFileRef("image.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubLocalByteContentFileRef expectedPngFileRef = CreateLocalByteFileRef("image.png", EpubContentType.IMAGE_PNG, "image/png");
            EpubLocalByteContentFileRef expectedSvgFileRef = CreateLocalByteFileRef("image.svg", EpubContentType.IMAGE_SVG, "image/svg+xml");
            EpubLocalByteContentFileRef expectedWebpFileRef = CreateLocalByteFileRef("image.webp", EpubContentType.IMAGE_WEBP, "image/webp");
            EpubLocalByteContentFileRef expectedTtf1FileRef = CreateLocalByteFileRef("font1.ttf", EpubContentType.FONT_TRUETYPE, "font/truetype");
            EpubLocalByteContentFileRef expectedTtf2FileRef = CreateLocalByteFileRef("font2.ttf", EpubContentType.FONT_TRUETYPE, "font/ttf");
            EpubLocalByteContentFileRef expectedTtf3FileRef = CreateLocalByteFileRef("font3.ttf", EpubContentType.FONT_TRUETYPE, "application/x-font-truetype");
            EpubLocalByteContentFileRef expectedOtf1FileRef = CreateLocalByteFileRef("font1.otf", EpubContentType.FONT_OPENTYPE, "font/opentype");
            EpubLocalByteContentFileRef expectedOtf2FileRef = CreateLocalByteFileRef("font2.otf", EpubContentType.FONT_OPENTYPE, "font/otf");
            EpubLocalByteContentFileRef expectedOtf3FileRef = CreateLocalByteFileRef("font3.otf", EpubContentType.FONT_OPENTYPE, "application/vnd.ms-opentype");
            EpubLocalByteContentFileRef expectedSfnt1FileRef = CreateLocalByteFileRef("font.aat", EpubContentType.FONT_SFNT, "font/sfnt");
            EpubLocalByteContentFileRef expectedSfnt2FileRef = CreateLocalByteFileRef("font.sil", EpubContentType.FONT_SFNT, "application/font-sfnt");
            EpubLocalByteContentFileRef expectedWoff11FileRef = CreateLocalByteFileRef("font1.woff", EpubContentType.FONT_WOFF, "font/woff");
            EpubLocalByteContentFileRef expectedWoff12FileRef = CreateLocalByteFileRef("font2.woff", EpubContentType.FONT_WOFF, "application/font-woff");
            EpubLocalByteContentFileRef expectedWoff2FileRef = CreateLocalByteFileRef("font.woff2", EpubContentType.FONT_WOFF2, "font/woff2");
            EpubLocalTextContentFileRef expectedSmilFileRef = CreateLocalTextFileRef("narration.smil", EpubContentType.SMIL, "application/smil+xml");
            EpubLocalByteContentFileRef expectedMp3FileRef = CreateLocalByteFileRef("audio.mp3", EpubContentType.AUDIO_MP3, "audio/mpeg");
            EpubLocalByteContentFileRef expectedMp4FileRef = CreateLocalByteFileRef("audio.mp4", EpubContentType.AUDIO_MP4, "audio/mp4");
            EpubLocalByteContentFileRef expectedOgg1FileRef = CreateLocalByteFileRef("audio1.opus", EpubContentType.AUDIO_OGG, "audio/ogg");
            EpubLocalByteContentFileRef expectedOgg2FileRef = CreateLocalByteFileRef("audio2.opus", EpubContentType.AUDIO_OGG, "audio/ogg; codecs=opus");
            EpubLocalByteContentFileRef expectedVideoFileRef = CreateLocalByteFileRef("video.mp4", EpubContentType.OTHER, "video/mp4");
            EpubLocalByteContentFileRef expectedCoverFileRef = CreateLocalByteFileRef("cover.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubLocalTextContentFileRef expectedTocFileRef = CreateLocalTextFileRef("toc.html", EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubRemoteTextContentFileRef expectedRemoteHtmlFileRef = CreateRemoteTextFileRef("https://example.com/books/123/test.html", EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubRemoteTextContentFileRef expectedRemoteCssFileRef = CreateRemoteTextFileRef("https://example.com/books/123/test.css", EpubContentType.CSS, "text/css");
            EpubRemoteByteContentFileRef expectedRemoteJpgFileRef = CreateRemoteByteFileRef("https://example.com/books/123/image.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
            EpubRemoteByteContentFileRef expectedRemoteTtfFileRef = CreateRemoteByteFileRef("https://example.com/books/123/font.ttf", EpubContentType.FONT_TRUETYPE, "font/truetype");
            EpubRemoteByteContentFileRef expectedRemoteMp3FileRef = CreateRemoteByteFileRef("https://example.com/books/123/audio.mp3", EpubContentType.AUDIO_MP3, "audio/mpeg");
            EpubContentRef expectedContentMap = new
            (
                cover: expectedCoverFileRef,
                navigationHtmlFile: expectedTocFileRef,
                html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalTextContentFileRef>()
                    {
                        {
                            "text.html",
                            expectedHtmlFileRef
                        },
                        {
                            "toc.html",
                            expectedTocFileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.html",
                            expectedRemoteHtmlFileRef
                        }
                    }
                ),
                css: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalTextContentFileRef>()
                    {
                        {
                            "styles.css",
                            expectedCssFileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.css",
                            expectedRemoteCssFileRef
                        }
                    }
                ),
                images: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalByteContentFileRef>()
                    {
                        {
                            "image.gif",
                            expectedGifFileRef
                        },
                        {
                            "image.jpg",
                            expectedJpgFileRef
                        },
                        {
                            "image.png",
                            expectedPngFileRef
                        },
                        {
                            "image.svg",
                            expectedSvgFileRef
                        },
                        {
                            "image.webp",
                            expectedWebpFileRef
                        },
                        {
                            "cover.jpg",
                            expectedCoverFileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/image.jpg",
                            expectedRemoteJpgFileRef
                        }
                    }
                ),
                fonts: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalByteContentFileRef>()
                    {
                        {
                            "font1.ttf",
                            expectedTtf1FileRef
                        },
                        {
                            "font2.ttf",
                            expectedTtf2FileRef
                        },
                        {
                            "font3.ttf",
                            expectedTtf3FileRef
                        },
                        {
                            "font1.otf",
                            expectedOtf1FileRef
                        },
                        {
                            "font2.otf",
                            expectedOtf2FileRef
                        },
                        {
                            "font3.otf",
                            expectedOtf3FileRef
                        },
                        {
                            "font.aat",
                            expectedSfnt1FileRef
                        },
                        {
                            "font.sil",
                            expectedSfnt2FileRef
                        },
                        {
                            "font1.woff",
                            expectedWoff11FileRef
                        },
                        {
                            "font2.woff",
                            expectedWoff12FileRef
                        },
                        {
                            "font.woff2",
                            expectedWoff2FileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/font.ttf",
                            expectedRemoteTtfFileRef
                        }
                    }
                ),
                audio: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalByteContentFileRef>()
                    {
                        {
                            "audio.mp3",
                            expectedMp3FileRef
                        },
                        {
                            "audio.mp4",
                            expectedMp4FileRef
                        },
                        {
                            "audio1.opus",
                            expectedOgg1FileRef
                        },
                        {
                            "audio2.opus",
                            expectedOgg2FileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/audio.mp3",
                            expectedRemoteMp3FileRef
                        }
                    }
                ),
                allFiles: new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalContentFileRef>()
                    {
                        {
                            "text.html",
                            expectedHtmlFileRef
                        },
                        {
                            "doc.dtb",
                            expectedDtbFileRef
                        },
                        {
                            "toc.ncx",
                            expectedNcxFileRef
                        },
                        {
                            "oeb.html",
                            expectedOebFileRef
                        },
                        {
                            "file.xml",
                            expectedXmlFileRef
                        },
                        {
                            "styles.css",
                            expectedCssFileRef
                        },
                        {
                            "oeb.css",
                            expectedOebCssFileRef
                        },
                        {
                            "script1.js",
                            expectedJs1FileRef
                        },
                        {
                            "script2.js",
                            expectedJs2FileRef
                        },
                        {
                            "script3.js",
                            expectedJs3FileRef
                        },
                        {
                            "image.gif",
                            expectedGifFileRef
                        },
                        {
                            "image.jpg",
                            expectedJpgFileRef
                        },
                        {
                            "image.png",
                            expectedPngFileRef
                        },
                        {
                            "image.svg",
                            expectedSvgFileRef
                        },
                        {
                            "image.webp",
                            expectedWebpFileRef
                        },
                        {
                            "font1.ttf",
                            expectedTtf1FileRef
                        },
                        {
                            "font2.ttf",
                            expectedTtf2FileRef
                        },
                        {
                            "font3.ttf",
                            expectedTtf3FileRef
                        },
                        {
                            "font1.otf",
                            expectedOtf1FileRef
                        },
                        {
                            "font2.otf",
                            expectedOtf2FileRef
                        },
                        {
                            "font3.otf",
                            expectedOtf3FileRef
                        },
                        {
                            "font.aat",
                            expectedSfnt1FileRef
                        },
                        {
                            "font.sil",
                            expectedSfnt2FileRef
                        },
                        {
                            "font1.woff",
                            expectedWoff11FileRef
                        },
                        {
                            "font2.woff",
                            expectedWoff12FileRef
                        },
                        {
                            "font.woff2",
                            expectedWoff2FileRef
                        },
                        {
                            "narration.smil",
                            expectedSmilFileRef
                        },
                        {
                            "audio.mp3",
                            expectedMp3FileRef
                        },
                        {
                            "audio.mp4",
                            expectedMp4FileRef
                        },
                        {
                            "audio1.opus",
                            expectedOgg1FileRef
                        },
                        {
                            "audio2.opus",
                            expectedOgg2FileRef
                        },
                        {
                            "video.mp4",
                            expectedVideoFileRef
                        },
                        {
                            "cover.jpg",
                            expectedCoverFileRef
                        },
                        {
                            "toc.html",
                            expectedTocFileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteContentFileRef>()
                    {
                        {
                            "https://example.com/books/123/test.html",
                            expectedRemoteHtmlFileRef
                        },
                        {
                            "https://example.com/books/123/test.css",
                            expectedRemoteCssFileRef
                        },
                        {
                            "https://example.com/books/123/image.jpg",
                            expectedRemoteJpgFileRef
                        },
                        {
                            "https://example.com/books/123/font.ttf",
                            expectedRemoteTtfFileRef
                        },
                        {
                            "https://example.com/books/123/audio.mp3",
                            expectedRemoteMp3FileRef
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
                    uniqueIdentifier: null,
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
