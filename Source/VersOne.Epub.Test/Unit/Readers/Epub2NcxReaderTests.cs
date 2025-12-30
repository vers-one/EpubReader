using System.Xml;
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
              <docTitle />
              <navMap />
              <test />
            </ncx>
            """;

        private const string NCX_FILE_WITHOUT_DOCTITLE_ELEMENT = """
            <?xml version='1.0' encoding='utf-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/">
              <head />
              <navMap />
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
                  <navLabel>
                    <text>Chapter 1</text>
                  </navLabel>
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
                    items:
                    [
                        new
                        (
                            id: TOC_ID,
                            href: NCX_FILE_NAME,
                            mediaType: "application/x-dtbncx+xml"
                        )
                    ]
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
                    items:
                    [
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
                    ]
                ),
                docTitle: "Test title",
                docAuthors:
                [
                    "John Doe",
                    "Jane Doe"
                ],
                navMap: new Epub2NcxNavigationMap
                (
                    items:
                    [
                        new
                        (
                            id: "navpoint-1",
                            @class: "chapter",
                            playOrder: "1",
                            navigationLabels:
                            [
                                new
                                (
                                    text: "Chapter 1"
                                ),
                                new
                                (
                                    text: "Capitolo 1"
                                )
                            ],
                            content: new Epub2NcxContent
                            (
                                id: "content-1",
                                source: "chapter1.html"
                            ),
                            childNavigationPoints:
                            [
                                new
                                (
                                    id: "navpoint-1-1",
                                    @class: "section",
                                    playOrder: null,
                                    navigationLabels:
                                    [
                                        new
                                        (
                                            text: "Chapter 1.1"
                                        )
                                    ],
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
                                    navigationLabels:
                                    [
                                        new
                                        (
                                            text: "Chapter 1.2"
                                        )
                                    ],
                                    content: new Epub2NcxContent
                                    (
                                        id: "content-1-2",
                                        source: "chapter1.html#section-2"
                                    ),
                                    childNavigationPoints: null
                                )
                            ]
                        ),
                        new
                        (
                            id: "navpoint-2",
                            navigationLabels:
                            [
                                new
                                (
                                    text: "Chapter 2"
                                )
                            ],
                            content: new Epub2NcxContent
                            (
                                source: "chapter2.html"
                            )
                        )
                    ]
                ),
                pageList: new Epub2NcxPageList
                (
                    items:
                    [
                        new
                        (
                            id: "page-target-1",
                            value: "1",
                            type: Epub2NcxPageTargetType.FRONT,
                            @class: "front-matter",
                            playOrder: "1",
                            navigationLabels:
                            [
                                new
                                (
                                    text: "1"
                                ),
                                new
                                (
                                    text: "I"
                                )
                            ],
                            content: new Epub2NcxContent
                            (
                                source: "front.html"
                            )
                        ),
                        new
                        (
                            type: Epub2NcxPageTargetType.NORMAL,
                            navigationLabels:
                            [
                                new
                                (
                                    text: "2"
                                )
                            ],
                            content: new Epub2NcxContent
                            (
                                id: "content-2",
                                source: "chapter1.html#page-2"
                            )
                        )
                    ]
                ),
                navLists:
                [
                    new
                    (
                        id: "navlist-1",
                        @class: "navlist-illustrations",
                        navigationLabels:
                        [
                            new
                            (
                                text: "List of Illustrations"
                            ),
                            new
                            (
                                text: "Illustrazioni"
                            )
                        ],
                        navigationTargets:
                        [
                            new
                            (
                                id: "navtarget-1",
                                value: "Illustration 1",
                                @class: "illustration",
                                playOrder: "1",
                                navigationLabels:
                                [
                                    new
                                    (
                                        text: "Illustration 1"
                                    ),
                                    new
                                    (
                                        text: "Illustrazione 1"
                                    )
                                ],
                                content: new Epub2NcxContent
                                (
                                    source: "chapter1.html#illustration-1"
                                )
                            )
                        ]
                    ),
                    new
                    (
                        id: "navlist-2",
                        @class: "navlist-tables",
                        navigationLabels:
                        [
                            new
                            (
                                text: "List of Tables"
                            )
                        ],
                        navigationTargets:
                        [
                            new
                            (
                                id: "navtarget-2",
                                navigationLabels:
                                [
                                    new
                                    (
                                        text: "Tables"
                                    )
                                ],
                                content: null
                            ),
                            new
                            (
                                id: "navtarget-3",
                                navigationLabels:
                                [
                                    new
                                    (
                                        text: "Table 1"
                                    )
                                ],
                                content: new Epub2NcxContent
                                (
                                    source: "chapter1.html#table-1"
                                )
                            )
                        ]
                    )
                ]
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
                    items:
                    [
                        new
                        (
                            type: Epub2NcxPageTargetType.UNKNOWN,
                            navigationLabels:
                            [
                                new
                                (
                                    text: "1"
                                )
                            ],
                            content: new Epub2NcxContent
                            (
                                source: "chapter1.html#page-1"
                            )
                        )
                    ]
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

        [Fact(DisplayName = "Constructing a Epub2NcxReader instance with a null Epub2NcxReaderOptions property inside the epubReaderOptions parameter should succeed")]
        public void ConstructorWithNullEpub2NcxReaderOptionsTest()
        {
            _ = new Epub2NcxReader(new EpubReaderOptions
            {
                Epub2NcxReaderOptions = null!
            });
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

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if EpubPackage is missing the manifest item referenced by the spine TOC and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutTocManifestItemAndDefaultOptionsTest()
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

        [Fact(DisplayName = "ReadEpub2NcxAsync should return null if EpubPackage is missing the manifest item referenced by the spine TOC and IgnoreMissingTocManifestItemError = true")]
        public async Task ReadEpub2NcxAsyncWithoutTocManifestItemAndIgnoreMissingTocManifestItemErrorTest()
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
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new()
                {
                    IgnoreMissingTocManifestItemError = true
                }
            };
            Epub2NcxReader epub2NcxReader = new(epubReaderOptions);
            Epub2Ncx? actualEpub2Ncx = await epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, epubPackage);
            Assert.Null(actualEpub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if EPUB file is missing the NCX file specified in the EpubPackage and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutNcxFileAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = new();
            Epub2NcxReader epub2NcxReader = new();
            await Assert.ThrowsAsync<Epub2NcxException>(() => epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx));
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should return null if EPUB file is missing the NCX file specified in the EpubPackage and IgnoreMissingTocFileError = true")]
        public async Task ReadEpub2NcxAsyncWithoutNcxFileAndIgnoreMissingTocFileErrorTest()
        {
            TestZipFile testZipFile = new();
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new()
                {
                    IgnoreMissingTocFileError = true
                }
            };
            Epub2NcxReader epub2NcxReader = new(epubReaderOptions);
            Epub2Ncx? actualEpub2Ncx = await epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx);
            Assert.Null(actualEpub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file is larger than 2 GB and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithLargeNcxFileAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NCX_FILE_PATH, new Test4GbZipFileEntry());
            Epub2NcxReader epub2NcxReader = new();
            await Assert.ThrowsAsync<Epub2NcxException>(() => epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx));
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should return null if the NCX file is larger than 2 GB and IgnoreTocFileIsTooLargeError = true")]
        public async Task ReadEpub2NcxAsyncWithLargeNcxFileAndIgnoreTocFileIsTooLargeErrorTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NCX_FILE_PATH, new Test4GbZipFileEntry());
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new()
                {
                    IgnoreTocFileIsTooLargeError = true
                }
            };
            Epub2NcxReader epub2NcxReader = new(epubReaderOptions);
            Epub2Ncx? actualEpub2Ncx = await epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx);
            Assert.Null(actualEpub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException with an inner XmlException if the NCX file is not a valid XML file and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithInvalidXhtmlFileAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = CreateTestZipFileWithNcxFile("not a valid XML file");
            Epub2NcxReader epub2NcxReader = new(new EpubReaderOptions());
            Epub2NcxException outerException =
                await Assert.ThrowsAsync<Epub2NcxException>(() =>
                    epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx));
            Assert.NotNull(outerException.InnerException);
            Assert.Equal(typeof(XmlException), outerException.InnerException.GetType());
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should return null if the NCX file is not a valid XML file and IgnoreTocFileIsNotValidXmlError = true")]
        public async Task ReadEpub2NcxAsyncWithInvalidXhtmlFileAndIgnoreTocFileIsNotValidXmlErrorTest()
        {
            TestZipFile testZipFile = CreateTestZipFileWithNcxFile("not a valid XML file");
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new()
                {
                    IgnoreTocFileIsNotValidXmlError = true
                }
            };
            Epub2NcxReader epub2NcxReader = new(epubReaderOptions);
            Epub2Ncx? actualEpub2Ncx = await epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx);
            Assert.Null(actualEpub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'ncx' XML element and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutNcxElementAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NCX_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should return null if the NCX file has no 'ncx' XML element and IgnoreMissingNcxElementError = true")]
        public async Task ReadEpub2NcxAsyncWithoutNcxElementAndIgnoreMissingNcxElementErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new()
                {
                    IgnoreMissingNcxElementError = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_NCX_ELEMENT, null, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'head' XML element and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutHeadElementAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_HEAD_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if the NCX file has no 'head' XML element and IgnoreMissingHeadElementError = true")]
        public async Task ReadEpub2NcxAsyncWithoutHeadElementAndIgnoreMissingHeadElementErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new()
                {
                    IgnoreMissingHeadElementError = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_HEAD_ELEMENT, MinimalEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'docTitle' XML element and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutDocTitleElementAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_DOCTITLE_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if the NCX file has no 'docTitle' XML element and IgnoreMissingDocTitleElementError = true")]
        public async Task ReadEpub2NcxAsyncWithoutDocTitleElementAndIgnoreMissingDocTitleElementErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new()
                {
                    IgnoreMissingDocTitleElementError = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_DOCTITLE_ELEMENT, MinimalEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'navMap' XML element and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutNavMapElementAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVMAP_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if the NCX file has no 'navMap' XML element and IgnoreMissingNavMapElementError = true")]
        public async Task ReadEpub2NcxAsyncWithoutNavMapElementAndIgnoreMissingNavMapElementErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new()
                {
                    IgnoreMissingNavMapElementError = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_NAVMAP_ELEMENT, MinimalEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'meta' XML element has no 'name' attribute and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutMetaNameAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_META_NAME_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if a 'meta' XML element has no 'name' attribute and SkipInvalidMetaElements = true")]
        public async Task ReadEpub2NcxAsyncWithoutMetaNameAndSkipInvalidMetaElementsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new()
                {
                    SkipInvalidMetaElements = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_META_NAME_ATTRIBUTE, MinimalEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'meta' XML element has no 'content' attribute and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutMetaContentAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_META_CONTENT_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if a 'meta' XML element has no 'content' attribute and SkipInvalidMetaElements = true")]
        public async Task ReadEpub2NcxAsyncWithoutMetaContentAndSkipInvalidMetaElementsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new()
                {
                    SkipInvalidMetaElements = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_META_CONTENT_ATTRIBUTE, MinimalEpub2Ncx, epubReaderOptions);
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

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navpoint' XML element has no 'id' attribute and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutNavPointIdAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVPOINT_ID_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed Epub2NcxException if a 'navpoint' XML element has no 'id' attribute and SkipNavigationPointsWithMissingIds = true")]
        public async Task ReadEpub2NcxAsyncWithoutNavPointIdAndSkipNavigationPointsWithMissingIdsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
                {
                    SkipNavigationPointsWithMissingIds = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_NAVPOINT_ID_ATTRIBUTE, MinimalEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navpoint' XML element has no 'navlabel' elements and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutNavPointNavLabelsAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVPOINT_NAVLABEL_ELEMENTS);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed Epub2NcxException if a 'navpoint' XML element has no 'navlabel' elements and AllowNavigationPointsWithoutLabels = true")]
        public async Task ReadEpub2NcxAsyncWithoutNavPointNavLabelsAndAllowNavigationPointsWithoutLabelsTest()
        {
            Epub2Ncx expectedEpub2Ncx = MinimalEpub2Ncx;
            expectedEpub2Ncx.NavMap.Items.Add(
                new
                (
                    id: "navpoint-1",
                    navigationLabels: null,
                    content:
                    new
                    (
                        source: "chapter1.html"
                    )
                )
            );
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
                {
                    AllowNavigationPointsWithoutLabels = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_NAVPOINT_NAVLABEL_ELEMENTS, expectedEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navpoint' XML element has no 'content' element and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutNavPointContentAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVPOINT_CONTENT_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed Epub2NcxException if a 'navpoint' XML element has no 'content' element and IgnoreMissingContentForNavigationPoints = true")]
        public async Task ReadEpub2NcxAsyncWithoutNavPointContentAndIgnoreMissingContentForNavigationPointsTest()
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

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navlabel' XML element has no 'text' element and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutNavLabelTextAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVLABEL_TEXT_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if a 'navlabel' XML element has no 'text' element and SkipInvalidNavigationLabels = true")]
        public async Task ReadEpub2NcxAsyncWithoutNavLabelTextAndSkipInvalidNavigationLabelsTest()
        {
            Epub2Ncx expectedEpub2Ncx = MinimalEpub2Ncx;
            expectedEpub2Ncx.NavMap.Items.Add(
                new
                (
                    id: "navpoint-1",
                    navigationLabels:
                    [
                          new
                          (
                              text: "Chapter 1"
                          )
                    ],
                    content:
                    new
                    (
                        source: "chapter1.html"
                    )
                )
            );
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
                {
                    SkipInvalidNavigationLabels = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_NAVLABEL_TEXT_ELEMENT, expectedEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "Reading an NCX file with a URI-escaped 'src' attribute in a 'content' XML element should succeed")]
        public async Task ReadEpub2NcxAsyncWithEscapedContentSrcTest()
        {
            Epub2Ncx expectedEpub2Ncx = MinimalEpub2Ncx;
            expectedEpub2Ncx.NavMap.Items.Add(
                new
                (
                    id: "navpoint-1",
                    navigationLabels:
                    [
                          new
                          (
                              text: "Chapter 1"
                          )
                    ],
                    content:
                    new
                    (
                        source: "chapter1.html"
                    )
                )
            );
            await TestSuccessfulReadOperation(NCX_FILE_WITH_ESCAPED_CONTENT_SRC_ATTRIBUTE, expectedEpub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'content' XML element has no 'src' attribute and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutContentSrcAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_CONTENT_SRC_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if a 'content' XML element has no 'src' attribute and SkipInvalidNavigationContent = true")]
        public async Task ReadEpub2NcxAsyncWithoutContentSrcAndSkipInvalidNavigationContentTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
                {
                    SkipInvalidNavigationContent = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_CONTENT_SRC_ATTRIBUTE, MinimalEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'pageTarget' XML element has no 'type' attribute and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutPageTargetTypeAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_PAGETARGET_TYPE_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if a 'pageTarget' XML element has no 'type' attribute and ReplaceMissingPageTargetTypesWithUnknown = true")]
        public async Task ReadEpub2NcxAsyncWithoutPageTargetTypeAndReplaceMissingPageTargetTypesWithUnknownTest()
        {
            Epub2Ncx expectedEpub2Ncx =
            new
            (
                filePath: NCX_FILE_PATH,
                head: new Epub2NcxHead(),
                docTitle: null,
                docAuthors: null,
                navMap: new Epub2NcxNavigationMap(),
                pageList:
                new
                (
                    items:
                    [
                        new
                        (
                            type: Epub2NcxPageTargetType.UNKNOWN,
                            navigationLabels:
                            [
                                new
                                (
                                    text: "1"
                                )
                            ],
                            content:
                            new
                            (
                                source: "chapter1.html#page-1"
                            )
                        )
                    ]
                ),
                navLists: null
            );
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
                {
                    ReplaceMissingPageTargetTypesWithUnknown = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_PAGETARGET_TYPE_ATTRIBUTE, expectedEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "Reading an NCX file with unknown value of the 'type' attribute of a 'pageTarget' XML element should succeed")]
        public async Task ReadEpub2NcxAsyncWithUnknownPageTargetTypeTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NCX_FILE_WITH_UNKNOWN_PAGETARGET_TYPE, MinimalEpub2NcxWithUnknownPageTargetType);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'pageTarget' XML element has no 'navlabel' elements and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutPageTargetNavLabelsAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_PAGETARGET_NAVLABEL_ELEMENTS);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if a 'pageTarget' XML element has no 'navlabel' elements and AllowNavigationPageTargetsWithoutLabels = true")]
        public async Task ReadEpub2NcxAsyncWithoutPageTargetNavLabelsAndAllowNavigationPageTargetsWithoutLabelsTest()
        {
            Epub2Ncx expectedEpub2Ncx =
            new
            (
                filePath: NCX_FILE_PATH,
                head: new Epub2NcxHead(),
                docTitle: null,
                docAuthors: null,
                navMap: new Epub2NcxNavigationMap(),
                pageList:
                new
                (
                    items:
                    [
                        new
                        (
                            type: Epub2NcxPageTargetType.NORMAL,
                            navigationLabels: null,
                            content:
                            new
                            (
                                source: "chapter1.html#page-1"
                            )
                        )
                    ]
                ),
                navLists: null
            );
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
                {
                    AllowNavigationPageTargetsWithoutLabels = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_PAGETARGET_NAVLABEL_ELEMENTS, expectedEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navList' XML element has no 'navlabel' elements and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutNavListNavLabelsAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVLIST_NAVLABEL_ELEMENTS);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if a 'navList' XML element has no 'navlabel' elements and AllowNavigationListsWithoutLabels = true")]
        public async Task ReadEpub2NcxAsyncWithoutNavListNavLabelsAndAllowNavigationListsWithoutLabelsTest()
        {
            Epub2Ncx expectedEpub2Ncx = MinimalEpub2Ncx;
            expectedEpub2Ncx.NavLists.Add(
                new
                (
                    id: "navlist-1",
                    @class: null,
                    navigationLabels: null,
                    navigationTargets:
                    [
                          new
                          (
                              id: "navtarget-1",
                              navigationLabels:
                              [
                                  new
                                  (
                                      text: "Tables"
                                  )
                              ],
                              content: null
                          )
                    ]
                )
            );
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
                {
                    AllowNavigationListsWithoutLabels = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_NAVLIST_NAVLABEL_ELEMENTS, expectedEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navTarget' XML element has no 'id' attribute and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutNavTargetIdAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVTARGET_ID_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if a 'navTarget' XML element has no 'id' attribute and SkipInvalidNavigationTargets = true")]
        public async Task ReadEpub2NcxAsyncWithoutNavTargetIdAndSkipInvalidNavigationTargetsTest()
        {
            Epub2Ncx expectedEpub2Ncx = MinimalEpub2Ncx;
            expectedEpub2Ncx.NavLists.Add(
                new
                (
                    id: "navlist-1",
                    @class: null,
                    navigationLabels:
                    [
                        new
                        (
                            text: "List of Tables"
                        )
                    ],
                    navigationTargets: null
                )
            );
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
                {
                    SkipInvalidNavigationTargets = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_NAVTARGET_ID_ATTRIBUTE, expectedEpub2Ncx, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navTarget' XML element has no 'navlabel' elements and no Epub2NcxReaderOptions are provided")]
        public async Task ReadEpub2NcxAsyncWithoutNavTargetNavLabelsAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVTARGET_NAVLABEL_ELEMENTS);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should succeed if a 'navTarget' XML element has no 'navlabel' elements and AllowNavigationTargetsWithoutLabels = true")]
        public async Task ReadEpub2NcxAsyncWithoutNavTargetNavLabelsAndAllowNavigationTargetsWithoutLabelsTest()
        {
            Epub2Ncx expectedEpub2Ncx = MinimalEpub2Ncx;
            expectedEpub2Ncx.NavLists.Add(
                new
                (
                    id: "navlist-1",
                    @class: null,
                    navigationLabels:
                    [
                        new
                        (
                            text: "List of Tables"
                        )
                    ],
                    navigationTargets:
                    [
                        new
                        (
                            id: "navtarget-1",
                            navigationLabels: null,
                            content: null
                        )
                    ]
                )
            );
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
                {
                    AllowNavigationTargetsWithoutLabels = true
                }
            };
            await TestSuccessfulReadOperation(NCX_FILE_WITHOUT_NAVTARGET_NAVLABEL_ELEMENTS, expectedEpub2Ncx, epubReaderOptions);
        }

        private static async Task TestSuccessfulReadOperation(string ncxFileContent, Epub2Ncx? expectedEpub2Ncx, EpubReaderOptions? epubReaderOptions = null)
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
