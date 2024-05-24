using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class Epub2NcxReaderTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string NCX_FILE_NAME = "toc.ncx";
        private const string NCX_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{NCX_FILE_NAME}";
        private const string TOC_ID = "ncx";

        private const string MINIMAL_NCX_FILE = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap />
            </ncx>
            """;

        private const string FULL_NCX_FILE = """
            <?xml version='1.0' encoding='UTF-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/" version="2005-1">
              <head>
                <meta name="dtb:uid" content="9781234567890" />
                <meta name="dtb:depth" content="1" />
                <meta name="dtb:generator" content="EpubWriter" />
                <meta name="dtb:totalPageCount" content="0" />
                <meta name="dtb:maxPageNumber" content="0" />
                <meta name="location" content="https://example.com/books/123/ncx" scheme="URI" />
              </head>
              <docTitle>
                <text>Test title</text>
              </docTitle>
              <docAuthor>
                <text>John Doe</text>
              </docAuthor>
              <docAuthor>
                <text>Jane Doe</text>
              </docAuthor>
              <navMap>
                <navPoint id="navpoint-1" class="chapter" playOrder="1">
                  <navLabel>
                    <text>Chapter 1</text>
                  </navLabel>
                  <navLabel>
                    <text>Capitolo 1</text>
                  </navLabel>
                  <content id="content-1" src="chapter1.html" />
                  <navPoint id="navpoint-1-1" class="section">
                    <navLabel>
                      <text>Chapter 1.1</text>
                    </navLabel>
                    <content id="content-1-1" src="chapter1.html#section-1" />
                  </navPoint>
                  <navPoint id="navpoint-1-2" class="section">
                    <navLabel>
                      <text>Chapter 1.2</text>
                    </navLabel>
                    <content id="content-1-2" src="chapter1.html#section-2" />
                  </navPoint>
                </navPoint>
                <navPoint id="navpoint-2">
                  <navLabel>
                    <text>Chapter 2</text>
                  </navLabel>
                  <content src="chapter2.html" />
                </navPoint>
              </navMap>
              <pageList>
                <pageTarget id="page-target-1" value="1" type="front" class="front-matter" playorder="1">
                  <navLabel>
                    <text>1</text>
                  </navLabel>
                  <navLabel>
                    <text>I</text>
                  </navLabel>
                  <content src="front.html" />
                </pageTarget>
                <pageTarget type="normal">
                  <navLabel>
                    <text>2</text>
                  </navLabel>
                  <content id="content-2" src="chapter1.html#page-2" />
                </pageTarget>        
              </pageList>
              <navList id="navlist-1" class="navlist-illustrations">
                <navLabel>
                  <text>List of Illustrations</text>
                </navLabel>
                <navLabel>
                  <text>Illustrazioni</text>
                </navLabel>
                <navTarget id="navtarget-1" value="Illustration 1" class="illustration" playorder="1">
                  <navLabel>
                    <text>Illustration 1</text>
                  </navLabel>
                  <navLabel>
                    <text>Illustrazione 1</text>
                  </navLabel>
                  <content src="chapter1.html#illustration-1" />
                </navTarget>
              </navList>
              <navList id="navlist-2" class="navlist-tables">
                <navLabel>
                  <text>List of Tables</text>
                </navLabel>
                <navTarget id="navtarget-2">
                  <navLabel>
                    <text>Tables</text>
                  </navLabel>
                </navTarget>
                <navTarget id="navtarget-3">
                  <navLabel>
                    <text>Table 1</text>
                  </navLabel>
                  <content src="chapter1.html#table-1" />
                </navTarget>
              </navList>
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_NCX_ELEMENT = """
            <?xml version='1.0' encoding='utf-8'?>
            <test />
            """;

        private const string NCX_FILE_WITHOUT_HEAD_ELEMENT = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <test />
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_DOCTITLE_ELEMENT = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <test />
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_NAVMAP_ELEMENT = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <test />
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_META_NAME_ATTRIBUTE = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head>
                <meta content="9781234567890" />
              </head>
              <docTitle />
              <navMap />
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_META_CONTENT_ATTRIBUTE = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head>
                <meta name="dtb:uid" />
              </head>
              <docTitle />
              <navMap />
            </ncx>
            """;

        private const string MINIMAL_NCX_FILE_WITH_UNKNOWN_ELEMENT_IN_DOCTITLE = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle>
                <test />
              </docTitle>
              <navMap />
            </ncx>
            """;

        private const string MINIMAL_NCX_FILE_WITH_UNKNOWN_ELEMENT_IN_DOCAUTHOR = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <docAuthor>
                <test />
              </docAuthor>
              <navMap />
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_NAVPOINT_ID_ATTRIBUTE = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap>
                <navPoint>
                  <navLabel>
                    <text>Chapter 1</text>
                  </navLabel>
                  <content src="chapter1.html" />
                </navPoint>
              </navMap>
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_NAVPOINT_NAVLABEL_ELEMENTS = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap>
                <navPoint id="navpoint-1">
                  <content src="chapter1.html" />
                </navPoint>
              </navMap>
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_NAVPOINT_CONTENT_ELEMENT = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap>
                <navPoint id="navpoint-1">
                  <navLabel>
                    <text>Chapter 1</text>
                  </navLabel>
                </navPoint>
              </navMap>
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_NAVLABEL_TEXT_ELEMENT = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap>
                <navPoint id="navpoint-1">
                  <navLabel />
                  <content src="chapter1.html" />
                </navPoint>
              </navMap>
            </ncx>
            """;

        private const string NCX_FILE_WITH_ESCAPED_CONTENT_SRC_ATTRIBUTE = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap>
                <navPoint id="navpoint-1">
                  <navLabel>
                    <text>Chapter 1</text>
                  </navLabel>
                  <content src="chapter%31.html" />
                </navPoint>
              </navMap>
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_CONTENT_SRC_ATTRIBUTE = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap>
                <navPoint id="navpoint-1">
                  <navLabel>
                    <text>Chapter 1</text>
                  </navLabel>
                  <content />
                </navPoint>
              </navMap>
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_PAGETARGET_TYPE_ATTRIBUTE = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap />
              <pageList>
                <pageTarget>
                  <navLabel>
                    <text>1</text>
                  </navLabel>
                  <content src="chapter1.html#page-1" />
                </pageTarget>
              </pageList>
            </ncx>
            """;

        private const string MINIMAL_NCX_FILE_WITH_UNKNOWN_PAGETARGET_TYPE = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap />
              <pageList>
                <pageTarget type="test">
                  <navLabel>
                    <text>1</text>
                  </navLabel>
                  <content src="chapter1.html#page-1" />
                </pageTarget>
              </pageList>
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_PAGETARGET_NAVLABEL_ELEMENTS = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap />
              <pageList>
                <pageTarget type="normal">
                  <content src="chapter1.html#page-1" />
                </pageTarget>
              </pageList>
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_NAVLIST_NAVLABEL_ELEMENTS = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap />
              <navList id="navlist-1">
                <navTarget id="navtarget-1">
                  <navLabel>
                    <text>Tables</text>
                  </navLabel>
                </navTarget>
              </navList>
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_NAVTARGET_ID_ATTRIBUTE = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap />
              <navList id="navlist-1">
                <navLabel>
                  <text>List of Tables</text>
                </navLabel>
                <navTarget>
                  <navLabel>
                    <text>Tables</text>
                  </navLabel>
                </navTarget>
              </navList>
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_NAVTARGET_NAVLABEL_ELEMENTS = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <docTitle />
              <navMap />
              <navList id="navlist-1">
                <navLabel>
                  <text>List of Tables</text>
                </navLabel>
                <navTarget id="navtarget-1" />
              </navList>
            </ncx>
            """;

        private static EpubPackage MinimalEpubPackageWithNcx =>
            new
            (
                uniqueIdentifier: null,
                epubVersion: EpubVersion.EPUB_2,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest
                (
                    items: new List<EpubManifestItem>()
                    {
                        new
                        (
                            id: TOC_ID,
                            href: NCX_FILE_NAME,
                            mediaType: "application/x-dtbncx+xml"
                        )
                    }
                ),
                spine: new EpubSpine
                (
                    toc: TOC_ID
                ),
                guide: null
            );

        private static Epub2Ncx MinimalEpub2Ncx =>
            new
            (
                filePath: NCX_FILE_PATH,
                head: new Epub2NcxHead(),
                docTitle: null,
                docAuthors: null,
                navMap: new Epub2NcxNavigationMap(),
                pageList: null,
                navLists: null
            );

        private static Epub2Ncx FullEpub2Ncx =>
            new
            (
                filePath: NCX_FILE_PATH,
                head: new Epub2NcxHead
                (
                    items: new List<Epub2NcxHeadMeta>
                    {
                        new
                        (
                            name: "dtb:uid",
                            content: "9781234567890"
                        ),
                        new
                        (
                            name: "dtb:depth",
                            content: "1"
                        ),
                        new
                        (
                            name: "dtb:generator",
                            content: "EpubWriter"
                        ),
                        new
                        (
                            name: "dtb:totalPageCount",
                            content: "0"
                        ),
                        new
                        (
                            name: "dtb:maxPageNumber",
                            content: "0"
                        ),
                        new
                        (
                            name: "location",
                            content: "https://example.com/books/123/ncx",
                            scheme: "URI"
                        )
                    }
                ),
                docTitle: "Test title",
                docAuthors: new List<string>()
                {
                    "John Doe",
                    "Jane Doe"
                },
                navMap: new Epub2NcxNavigationMap
                (
                    items: new List<Epub2NcxNavigationPoint>()
                    {
                        new
                        (
                            id: "navpoint-1",
                            @class: "chapter",
                            playOrder: "1",
                            navigationLabels: new List<Epub2NcxNavigationLabel>()
                            {
                                new
                                (
                                    text: "Chapter 1"
                                ),
                                new
                                (
                                    text: "Capitolo 1"
                                )
                            },
                            content: new Epub2NcxContent
                            (
                                id: "content-1",
                                source: "chapter1.html"
                            ),
                            childNavigationPoints: new List<Epub2NcxNavigationPoint>()
                            {
                                new
                                (
                                    id: "navpoint-1-1",
                                    @class: "section",
                                    playOrder: null,
                                    navigationLabels: new List<Epub2NcxNavigationLabel>()
                                    {
                                        new
                                        (
                                            text: "Chapter 1.1"
                                        )
                                    },
                                    content: new Epub2NcxContent
                                    (
                                        id: "content-1-1",
                                        source: "chapter1.html#section-1"
                                    ),
                                    childNavigationPoints: null
                                ),
                                new
                                (
                                    id: "navpoint-1-2",
                                    @class: "section",
                                    playOrder: null,
                                    navigationLabels: new List<Epub2NcxNavigationLabel>()
                                    {
                                        new
                                        (
                                            text: "Chapter 1.2"
                                        )
                                    },
                                    content: new Epub2NcxContent
                                    (
                                        id: "content-1-2",
                                        source: "chapter1.html#section-2"
                                    ),
                                    childNavigationPoints: null
                                )
                            }
                        ),
                        new
                        (
                            id: "navpoint-2",
                            navigationLabels: new List<Epub2NcxNavigationLabel>()
                            {
                                new
                                (
                                    text: "Chapter 2"
                                )
                            },
                            content: new Epub2NcxContent
                            (
                                source: "chapter2.html"
                            )
                        )
                    }
                ),
                pageList: new Epub2NcxPageList
                (
                    items: new List<Epub2NcxPageTarget>()
                    {
                        new
                        (
                            id: "page-target-1",
                            value: "1",
                            type: Epub2NcxPageTargetType.FRONT,
                            @class: "front-matter",
                            playOrder: "1",
                            navigationLabels: new List<Epub2NcxNavigationLabel>()
                            {
                                new
                                (
                                    text: "1"
                                ),
                                new
                                (
                                    text: "I"
                                )
                            },
                            content: new Epub2NcxContent
                            (
                                source: "front.html"
                            )
                        ),
                        new
                        (
                            type: Epub2NcxPageTargetType.NORMAL,
                            navigationLabels: new List<Epub2NcxNavigationLabel>()
                            {
                                new
                                (
                                    text: "2"
                                )
                            },
                            content: new Epub2NcxContent
                            (
                                id: "content-2",
                                source: "chapter1.html#page-2"
                            )
                        )
                    }
                ),
                navLists: new List<Epub2NcxNavigationList>()
                {
                    new
                    (
                        id: "navlist-1",
                        @class: "navlist-illustrations",
                        navigationLabels: new List<Epub2NcxNavigationLabel>()
                        {
                            new
                            (
                                text: "List of Illustrations"
                            ),
                            new
                            (
                                text: "Illustrazioni"
                            )
                        },
                        navigationTargets: new List<Epub2NcxNavigationTarget>()
                        {
                            new
                            (
                                id: "navtarget-1",
                                value: "Illustration 1",
                                @class: "illustration",
                                playOrder: "1",
                                navigationLabels: new List<Epub2NcxNavigationLabel>()
                                {
                                    new
                                    (
                                        text: "Illustration 1"
                                    ),
                                    new
                                    (
                                        text: "Illustrazione 1"
                                    )
                                },
                                content: new Epub2NcxContent
                                (
                                    source: "chapter1.html#illustration-1"
                                )
                            )
                        }
                    ),
                    new
                    (
                        id: "navlist-2",
                        @class: "navlist-tables",
                        navigationLabels: new List<Epub2NcxNavigationLabel>()
                        {
                            new
                            (
                                text: "List of Tables"
                            )
                        },
                        navigationTargets: new List<Epub2NcxNavigationTarget>()
                        {
                            new
                            (
                                id: "navtarget-2",
                                navigationLabels: new List<Epub2NcxNavigationLabel>()
                                {
                                    new
                                    (
                                        text: "Tables"
                                    )
                                },
                                content: null
                            ),
                            new
                            (
                                id: "navtarget-3",
                                navigationLabels: new List<Epub2NcxNavigationLabel>()
                                {
                                    new
                                    (
                                        text: "Table 1"
                                    )
                                },
                                content: new Epub2NcxContent
                                (
                                    source: "chapter1.html#table-1"
                                )
                            )
                        }
                    )
                }
            );

        private static Epub2Ncx MinimalEpub2NcxWithUnknownPageTargetType =>
            new
            (
                filePath: NCX_FILE_PATH,
                head: new Epub2NcxHead(),
                docTitle: null,
                docAuthors: null,
                navMap: new Epub2NcxNavigationMap(),
                pageList: new Epub2NcxPageList
                (
                    items: new List<Epub2NcxPageTarget>()
                    {
                        new
                        (
                            type: Epub2NcxPageTargetType.UNKNOWN,
                            navigationLabels: new List<Epub2NcxNavigationLabel>()
                            {
                                new
                                (
                                    text: "1"
                                )
                            },
                            content: new Epub2NcxContent
                            (
                                source: "chapter1.html#page-1"
                            )
                        )
                    }
                ),
                navLists: null
            );

        [Fact(DisplayName = "Constructing a Epub2NcxReader instance with a non-null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNonNullEpubReaderOptionsTest()
        {
            _ = new Epub2NcxReader(new EpubReaderOptions());
        }

        [Fact(DisplayName = "Constructing a Epub2NcxReader instance with a null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNullEpubReaderOptionsTest()
        {
            _ = new Epub2NcxReader(null);
        }

        [Fact(DisplayName = "Reading a minimal NCX file should succeed")]
        public async Task ReadEpub2NcxAsyncWithMinimalNcxFileTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NCX_FILE, MinimalEpub2Ncx);
        }

        [Fact(DisplayName = "Reading a full NCX file should succeed")]
        public async Task ReadEpub2NcxAsyncWithFullNcxFileTest()
        {
            await TestSuccessfulReadOperation(FULL_NCX_FILE, FullEpub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should return null if EpubPackage is missing spine TOC")]
        public async Task ReadEpub2NcxAsyncWithoutTocTest()
        {
            TestZipFile testZipFile = new();
            EpubPackage epubPackage = new
            (
                uniqueIdentifier: null,
                epubVersion: EpubVersion.EPUB_2,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest(),
                spine: new EpubSpine(),
                guide: null
            );
            Epub2NcxReader epub2NcxReader = new();
            Epub2Ncx? epub2Ncx = await epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, epubPackage);
            Assert.Null(epub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if EpubPackage is missing the manifest item referenced by the spine TOC")]
        public async Task ReadEpub2NcxAsyncWithoutTocManifestItemTest()
        {
            TestZipFile testZipFile = new();
            EpubPackage epubPackage = new
            (
                uniqueIdentifier: null,
                epubVersion: EpubVersion.EPUB_2,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest(),
                spine: new EpubSpine
                (
                    toc: TOC_ID
                ),
                guide: null
            );
            Epub2NcxReader epub2NcxReader = new();
            await Assert.ThrowsAsync<Epub2NcxException>(() => epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, epubPackage));
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if EPUB file is missing the NCX file specified in the EpubPackage")]
        public async Task ReadEpub2NcxAsyncWithoutNcxFileTest()
        {
            TestZipFile testZipFile = new();
            Epub2NcxReader epub2NcxReader = new();
            await Assert.ThrowsAsync<Epub2NcxException>(() => epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx));
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file is larger than 2 GB")]
        public async Task ReadEpub2NcxAsyncWithLargeNcxFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NCX_FILE_PATH, new Test4GbZipFileEntry());
            Epub2NcxReader epub2NcxReader = new();
            await Assert.ThrowsAsync<Epub2NcxException>(() => epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx));
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'ncx' XML element")]
        public async Task ReadEpub2NcxAsyncWithoutNcxElementTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NCX_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'head' XML element")]
        public async Task ReadEpub2NcxAsyncWithoutHeadElementTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_HEAD_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'docTitle' XML element")]
        public async Task ReadEpub2NcxAsyncWithoutDocTitleElementTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_DOCTITLE_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'navMap' XML element")]
        public async Task ReadEpub2NcxAsyncWithoutNavMapElementTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVMAP_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'meta' XML element has no 'name' attribute")]
        public async Task ReadEpub2NcxAsyncWithoutMetaNameTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_META_NAME_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'meta' XML element has no 'content' attribute")]
        public async Task ReadEpub2NcxAsyncWithoutMetaContentTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_META_CONTENT_ATTRIBUTE);
        }

        [Fact(DisplayName = "Reading an NCX file with unknown XML element in the 'docTitle' element should succeed")]
        public async Task ReadEpub2NcxAsyncWithUnknownElementInDocTitleTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NCX_FILE_WITH_UNKNOWN_ELEMENT_IN_DOCTITLE, MinimalEpub2Ncx);
        }

        [Fact(DisplayName = "Reading an NCX file with unknown XML element in the 'docAuthor' element should succeed")]
        public async Task ReadEpub2NcxAsyncWithUnknownElementInDocAuthorTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NCX_FILE_WITH_UNKNOWN_ELEMENT_IN_DOCAUTHOR, MinimalEpub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navpoint' XML element has no 'id' attribute")]
        public async Task ReadEpub2NcxAsyncWithoutNavPointIdTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVPOINT_ID_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navpoint' XML element has no 'navlabel' elements")]
        public async Task ReadEpub2NcxAsyncWithoutNavPointNavLabelsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVPOINT_NAVLABEL_ELEMENTS);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navpoint' XML element has no 'content' element")]
        public async Task ReadEpub2NcxAsyncWithoutNavPointContentTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVPOINT_CONTENT_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navpoint' XML element has no 'content' element and Epub2NcxReaderOptions is null")]
        public async Task ReadEpub2NcxAsyncWithoutNavPointContentAndWithNullEpub2NcxReaderOptionsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = null!
            };
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVPOINT_CONTENT_ELEMENT, epubReaderOptions);
        }

        [Fact(DisplayName = "Reading an NCX file without 'content' element in a 'navpoint' XML element with IgnoreMissingContentForNavigationPoints = true should succeed")]
        public async Task ReadEpub2NcxAsyncWithoutNavPointContentWithIgnoreMissingContentForNavigationPointsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
                {
                    IgnoreMissingContentForNavigationPoints = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_NAVPOINT_CONTENT_ELEMENT, MinimalEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navlabel' XML element has no 'text' element")]
        public async Task ReadEpub2NcxAsyncWithoutNavLabelTextTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVLABEL_TEXT_ELEMENT);
        }

        [Fact(DisplayName = "Reading an NCX file with a URI-escaped 'src' attribute in a 'content' XML element should succeed")]
        public async Task ReadEpub2NcxAsyncWithEscapedContentSrcTest()
        {
            Epub2Ncx expectedEpub2Ncx = new
            (
                filePath: NCX_FILE_PATH,
                head: new Epub2NcxHead(),
                docTitle: null,
                docAuthors: null,
                navMap: new Epub2NcxNavigationMap
                (
                    items: new List<Epub2NcxNavigationPoint>()
                    {
                        new
                        (
                            id: "navpoint-1",
                            navigationLabels: new List<Epub2NcxNavigationLabel>()
                            {
                                new
                                (
                                    text: "Chapter 1"
                                )
                            },
                            content: new Epub2NcxContent
                            (
                                source: "chapter1.html"
                            )
                        )
                    }
                ),
                pageList: null,
                navLists: null
            );
            await TestSuccessfulReadOperation(NCX_FILE_WITH_ESCAPED_CONTENT_SRC_ATTRIBUTE, expectedEpub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'content' XML element has no 'src' attribute")]
        public async Task ReadEpub2NcxAsyncWithoutContentSrcTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_CONTENT_SRC_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'pageTarget' XML element has no 'type' attribute")]
        public async Task ReadEpub2NcxAsyncWithoutPageTargetTypeTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_PAGETARGET_TYPE_ATTRIBUTE);
        }

        [Fact(DisplayName = "Reading an NCX file with unknown value of the 'type' attribute of a 'pageTarget' XML element should succeed")]
        public async Task ReadEpub2NcxAsyncWithUnknownPageTargetTypeTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NCX_FILE_WITH_UNKNOWN_PAGETARGET_TYPE, MinimalEpub2NcxWithUnknownPageTargetType);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'pageTarget' XML element has no 'navlabel' elements")]
        public async Task ReadEpub2NcxAsyncWithoutPageTargetNavLabelsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_PAGETARGET_NAVLABEL_ELEMENTS);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navList' XML element has no 'navlabel' elements")]
        public async Task ReadEpub2NcxAsyncWithoutNavListNavLabelsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVLIST_NAVLABEL_ELEMENTS);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navTarget' XML element has no 'id' attribute")]
        public async Task ReadEpub2NcxAsyncWithoutNavTargetIdTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVTARGET_ID_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navTarget' XML element has no 'navlabel' elements")]
        public async Task ReadEpub2NcxAsyncWithoutNavTargetNavLabelsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVTARGET_NAVLABEL_ELEMENTS);
        }

        private static async Task TestSuccessfulReadOperation(string ncxFileContent, Epub2Ncx expectedEpub2Ncx, EpubReaderOptions? epubReaderOptions = null)
        {
            TestZipFile testZipFile = CreateTestZipFileWithNcxFile(ncxFileContent);
            Epub2NcxReader epub2NcxReader = new(epubReaderOptions ?? new EpubReaderOptions());
            Epub2Ncx? actualEpub2Ncx = await epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx);
            Epub2NcxComparer.CompareEpub2Ncxes(expectedEpub2Ncx, actualEpub2Ncx);
        }

        private static async Task TestFailingReadOperation(string ncxFileContent, EpubReaderOptions? epubReaderOptions = null)
        {
            TestZipFile testZipFile = CreateTestZipFileWithNcxFile(ncxFileContent);
            Epub2NcxReader epub2NcxReader = new(epubReaderOptions ?? new EpubReaderOptions());
            await Assert.ThrowsAsync<Epub2NcxException>(() => epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx));
        }

        private static TestZipFile CreateTestZipFileWithNcxFile(string ncxFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NCX_FILE_PATH, new TestZipFileEntry(ncxFileContent));
            return testZipFile;
        }
    }
}
