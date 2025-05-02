using VersOne.Epub.Internal;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class NavigationReaderTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string NCX_FILE_NAME = "toc.ncx";
        private const string NCX_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{NCX_FILE_NAME}";
        private const string NAV_FILE_NAME = "toc.html";
        private const string NAV_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{NAV_FILE_NAME}";

        [Fact(DisplayName = "GetNavigationItems should return null for EPUB 2 schemas without NCX file")]
        public void GetNavigationItemsForEpub2WithoutNcxTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_2),
                epub2Ncx: null,
                epub3NavDocument: null,
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubContentRef epubContentRef = new();
            List<EpubNavigationItemRef>? navigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            Assert.Null(navigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 2 schemas with minimal NCX file should succeed")]
        public void GetNavigationItemsForEpub2WithMinimalNcxTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_2),
                epub2Ncx: new
                (
                    filePath: NCX_FILE_PATH,
                    head: new(),
                    docTitle: null,
                    docAuthors: null,
                    navMap: new(),
                    pageList: null,
                    navLists: null
                ),
                epub3NavDocument: null,
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubContentRef epubContentRef = new();
            List<EpubNavigationItemRef> expectedNavigationItems = [];
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 2 schemas with full NCX file should succeed")]
        public void GetNavigationItemsForEpub2WithFullNcxTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_2),
                epub2Ncx: new
                (
                    filePath: NCX_FILE_PATH,
                    head: new(),
                    docTitle: null,
                    docAuthors: null,
                    navMap: new
                    (
                        items:
                        [
                            new
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels:
                                [
                                    new
                                    (
                                        text: "Test label 1"
                                    ),
                                    new
                                    (
                                        text: "Test label 2"
                                    )
                                ],
                                content: new
                                (
                                    source: "chapter1.html"
                                ),
                                childNavigationPoints:
                                [
                                    new
                                    (
                                        id: String.Empty,
                                        navigationLabels:
                                        [
                                            new
                                            (
                                                text: "Test label 3"
                                            )
                                        ],
                                        content: new
                                        (
                                            source: "chapter1.html#section-1"
                                        )
                                    )
                                ]
                            )
                        ]
                    ),
                    pageList: null,
                    navLists: null
                ),
                epub3NavDocument: null,
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testTextContentFileRef = CreateTestHtmlFile("chapter1.html");
            EpubContentRef epubContentRef = CreateContentRef(null, testTextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem1 = CreateNavigationLink("Test label 1", "chapter1.html", testTextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem2 = CreateNavigationLink("Test label 3", "chapter1.html#section-1", testTextContentFileRef);
            expectedNavigationItem1.NestedItems.Add(expectedNavigationItem2);
            List<EpubNavigationItemRef> expectedNavigationItems =
            [
                expectedNavigationItem1
            ];
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 2 schemas with relative file paths within the NCX file should succeed")]
        public void GetNavigationItemsForEpub2WithRelativePathsTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_2),
                epub2Ncx: new
                (
                    filePath: $"toc/{NCX_FILE_NAME}",
                    head: new(),
                    docTitle: null,
                    docAuthors: null,
                    navMap: new
                    (
                        items:
                        [
                            new
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels:
                                [
                                    new
                                    (
                                        text: "Chapter 1"
                                    )
                                ],
                                content: new
                                (
                                    source: "chapter1.html"
                                ),
                                childNavigationPoints: null
                            ),
                            new
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels:
                                [
                                    new
                                    (
                                        text: "Chapter 2"
                                    )
                                ],
                                content: new
                                (
                                    source: "Subdirectory/chapter2.html"
                                ),
                                childNavigationPoints: null
                            ),
                            new
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels:
                                [
                                    new
                                    (
                                        text: "Chapter 3"
                                    )
                                ],
                                content: new
                                (
                                    source: "../chapter3.html"
                                ),
                                childNavigationPoints: null
                            ),
                            new
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels:
                                [
                                    new
                                    (
                                        text: "Chapter 4"
                                    )
                                ],
                                content: new
                                (
                                    source: "../OtherDirectory/chapter4.html"
                                ),
                                childNavigationPoints: null
                            ),
                            new
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels:
                                [
                                    new
                                    (
                                        text: "Chapter 5"
                                    )
                                ],
                                content: new
                                (
                                    source: "../OtherDirectory/Subdirectory/chapter5.html"
                                ),
                                childNavigationPoints: null
                            )
                        ]
                    ),
                    pageList: null,
                    navLists: null
                ),
                epub3NavDocument: null,
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef test1TextContentFileRef = CreateTestHtmlFile("toc", "chapter1.html");
            EpubLocalTextContentFileRef test2TextContentFileRef = CreateTestHtmlFile("toc/Subdirectory", "chapter2.html");
            EpubLocalTextContentFileRef test3TextContentFileRef = CreateTestHtmlFile(null, "chapter3.html");
            EpubLocalTextContentFileRef test4TextContentFileRef = CreateTestHtmlFile("OtherDirectory", "chapter4.html");
            EpubLocalTextContentFileRef test5TextContentFileRef = CreateTestHtmlFile("OtherDirectory/Subdirectory", "chapter5.html");
            EpubContentRef epubContentRef = CreateContentRef(null, test1TextContentFileRef, test2TextContentFileRef, test3TextContentFileRef,
                test4TextContentFileRef, test5TextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem1 = CreateNavigationLink("Chapter 1", "toc", "chapter1.html", test1TextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem2 = CreateNavigationLink("Chapter 2", "toc", "Subdirectory/chapter2.html", test2TextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem3 = CreateNavigationLink("Chapter 3", "toc", "../chapter3.html", test3TextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem4 = CreateNavigationLink("Chapter 4", "toc", "../OtherDirectory/chapter4.html", test4TextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem5 = CreateNavigationLink("Chapter 5", "toc", "../OtherDirectory/Subdirectory/chapter5.html",
                test5TextContentFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems =
            [
                expectedNavigationItem1,
                expectedNavigationItem2,
                expectedNavigationItem3,
                expectedNavigationItem4,
                expectedNavigationItem5
            ];
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "GetNavigationItems should throw a Epub2NcxException if an NCX navigation point has no navigation labels")]
        public void GetNavigationItemsForEpub2WithoutNavigationLabelsTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_2),
                epub2Ncx: new
                (
                    filePath: NCX_FILE_PATH,
                    head: new(),
                    docTitle: null,
                    docAuthors: null,
                    navMap: new
                    (
                        items:
                        [
                            new
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels: [],
                                content: new
                                (
                                    source: "chapter1.html"
                                ),
                                childNavigationPoints: null
                            )
                        ]
                    ),
                    pageList: null,
                    navLists: null
                ),
                epub3NavDocument: null,
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubContentRef epubContentRef = new();
            Assert.Throws<Epub2NcxException>(() => NavigationReader.GetNavigationItems(epubSchema, epubContentRef));
        }

        [Fact(DisplayName = "GetNavigationItems should throw a Epub2NcxException if an NCX navigation point points to a remote resource")]
        public void GetNavigationItemsForEpub2WithRemoteNavigationPointSourceTest()
        {
            string remoteFileHref = "https://example.com/books/123/chapter1.html";
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_2),
                epub2Ncx: new
                (
                    filePath: NCX_FILE_PATH,
                    head: new(),
                    docTitle: null,
                    docAuthors: null,
                    navMap: new
                    (
                        items:
                        [
                            new
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels:
                                [
                                    new
                                    (
                                        text: "Test label"
                                    )
                                ],
                                content: new
                                (
                                    source: remoteFileHref
                                ),
                                childNavigationPoints: null
                            )
                        ]
                    ),
                    pageList: null,
                    navLists: null
                ),
                epub3NavDocument: null,
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubContentRef epubContentRef = new();
            Assert.Throws<Epub2NcxException>(() => NavigationReader.GetNavigationItems(epubSchema, epubContentRef));
        }

        [Fact(DisplayName = "GetNavigationItems should throw a Epub2NcxException if the file referenced by an NCX navigation point is missing in the EpubContentRef")]
        public void GetNavigationItemsForEpub2WithMissingContentFileTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_2),
                epub2Ncx: new
                (
                    filePath: NCX_FILE_PATH,
                    head: new(),
                    docTitle: null,
                    docAuthors: null,
                    navMap: new
                    (
                        items:
                        [
                            new
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels:
                                [
                                    new
                                    (
                                        text: "Test label"
                                    )
                                ],
                                content: new
                                (
                                    source: "chapter1.html"
                                ),
                                childNavigationPoints: null
                            )
                        ]
                    ),
                    pageList: null,
                    navLists: null
                ),
                epub3NavDocument: null,
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubContentRef epubContentRef = new();
            Assert.Throws<Epub2NcxException>(() => NavigationReader.GetNavigationItems(epubSchema, epubContentRef));
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 schemas with minimal NAV file should succeed")]
        public void GetNavigationItemsForEpub3WithMinimalNavTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_3),
                epub2Ncx: null,
                epub3NavDocument: new
                (
                    filePath: NAV_FILE_PATH
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubContentRef epubContentRef = new();
            List<EpubNavigationItemRef> expectedNavigationItems = [];
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 schemas with full NAV file should succeed")]
        public void GetNavigationItemsForEpub3WithFullNavTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_3),
                epub2Ncx: null,
                epub3NavDocument: new
                (
                    filePath: NAV_FILE_PATH,
                    navs:
                    [
                        new
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            isHidden: false,
                            head: "Test header",
                            ol: new
                            (
                                lis:
                                [
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: "Test text 1",
                                            title: "Test title 1",
                                            alt: "Test alt 1",
                                            href: "chapter1.html"
                                        ),
                                        childOl: new
                                        (
                                            lis:
                                            [
                                                new
                                                (
                                                    anchor: new
                                                    (
                                                        text: "Test text 2",
                                                        title: "Test title 2",
                                                        alt: "Test alt 2",
                                                        href: "chapter1.html#section-1"
                                                    )
                                                )
                                            ]
                                        )
                                    ),
                                    new
                                    (
                                        span: new
                                        (
                                            text: "Test text 3",
                                            title: "Test title 3",
                                            alt: "Test alt 3"
                                        ),
                                        childOl: new
                                        (
                                            lis:
                                            [
                                                new
                                                (
                                                    anchor: new
                                                    (
                                                        text: "Test text 4",
                                                        title: "Test title 4",
                                                        alt: "Test alt 4",
                                                        href: "chapter2.html"
                                                    )
                                                )
                                            ]
                                        )
                                    )
                                ]
                            )
                        )
                    ]
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubLocalTextContentFileRef testTextContentFileRef1 = CreateTestHtmlFile("chapter1.html");
            EpubLocalTextContentFileRef testTextContentFileRef2 = CreateTestHtmlFile("chapter2.html");
            EpubContentRef epubContentRef = CreateContentRef(testNavigationHtmlFileRef, testTextContentFileRef1, testTextContentFileRef2);
            EpubNavigationItemRef expectedNavigationItem1 = CreateNavigationHeader("Test header");
            EpubNavigationItemRef expectedNavigationItem2 = CreateNavigationLink("Test text 1", "chapter1.html", testTextContentFileRef1);
            EpubNavigationItemRef expectedNavigationItem3 = CreateNavigationLink("Test text 2", "chapter1.html#section-1", testTextContentFileRef1);
            EpubNavigationItemRef expectedNavigationItem4 = CreateNavigationHeader("Test text 3");
            EpubNavigationItemRef expectedNavigationItem5 = CreateNavigationLink("Test text 4", "chapter2.html", testTextContentFileRef2);
            expectedNavigationItem1.NestedItems.AddRange([expectedNavigationItem2, expectedNavigationItem4]);
            expectedNavigationItem2.NestedItems.Add(expectedNavigationItem3);
            expectedNavigationItem4.NestedItems.Add(expectedNavigationItem5);
            List<EpubNavigationItemRef> expectedNavigationItems =
            [
                expectedNavigationItem1
            ];
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 schemas with relative file paths within the NAV file should succeed")]
        public void GetNavigationItemsForEpub3WithRelativePathsTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_3),
                epub2Ncx: null,
                epub3NavDocument: new
                (
                    filePath: $"nav/{NAV_FILE_NAME}",
                    navs:
                    [
                        new
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            isHidden: false,
                            head: "Test header",
                            ol: new
                            (
                                lis:
                                [
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: "Chapter 1",
                                            href: "chapter1.html"
                                        )
                                    ),
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: "Chapter 2",
                                            href: "Subdirectory/chapter2.html"
                                        )
                                    ),
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: "Chapter 3",
                                            href: "../chapter3.html"
                                        )
                                    ),
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: "Chapter 4",
                                            href: "../OtherDirectory/chapter4.html"
                                        )
                                    ),
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: "Chapter 5",
                                            href: "../OtherDirectory/Subdirectory/chapter5.html"
                                        )
                                    ),
                                ]
                            )
                        )
                    ]
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubLocalTextContentFileRef test1TextContentFileRef = CreateTestHtmlFile("nav", "chapter1.html");
            EpubLocalTextContentFileRef test2TextContentFileRef = CreateTestHtmlFile("nav/Subdirectory", "chapter2.html");
            EpubLocalTextContentFileRef test3TextContentFileRef = CreateTestHtmlFile(null, "chapter3.html");
            EpubLocalTextContentFileRef test4TextContentFileRef = CreateTestHtmlFile("OtherDirectory", "chapter4.html");
            EpubLocalTextContentFileRef test5TextContentFileRef = CreateTestHtmlFile("OtherDirectory/Subdirectory", "chapter5.html");
            EpubContentRef epubContentRef = CreateContentRef(testNavigationHtmlFileRef, test1TextContentFileRef, test2TextContentFileRef, test3TextContentFileRef,
                test4TextContentFileRef, test5TextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem1 = CreateNavigationHeader("Test header");
            EpubNavigationItemRef expectedNavigationItem2 = CreateNavigationLink("Chapter 1", "nav", "chapter1.html", test1TextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem3 = CreateNavigationLink("Chapter 2", "nav", "Subdirectory/chapter2.html", test2TextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem4 = CreateNavigationLink("Chapter 3", "nav", "../chapter3.html", test3TextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem5 = CreateNavigationLink("Chapter 4", "nav", "../OtherDirectory/chapter4.html", test4TextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem6 = CreateNavigationLink("Chapter 5", "nav", "../OtherDirectory/Subdirectory/chapter5.html",
                test5TextContentFileRef);
            expectedNavigationItem1.NestedItems.AddRange([expectedNavigationItem2, expectedNavigationItem3, expectedNavigationItem4, expectedNavigationItem5,
                expectedNavigationItem6]);
            List<EpubNavigationItemRef> expectedNavigationItems =
            [
                expectedNavigationItem1
            ];
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 NAV file without a header should succeed")]
        public void GetNavigationItemsForEpub3NavWithoutHeaderTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_3),
                epub2Ncx: null,
                epub3NavDocument: new
                (
                    filePath: NAV_FILE_PATH,
                    navs:
                    [
                        new
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            isHidden: false,
                            head: null,
                            ol: new
                            (
                                lis:
                                [
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: "Test text",
                                            href: "chapter1.html"
                                        )
                                    )
                                ]
                            )
                        )
                    ]
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubLocalTextContentFileRef testTextContentFileRef = CreateTestHtmlFile("chapter1.html");
            EpubContentRef epubContentRef = CreateContentRef(testNavigationHtmlFileRef, testTextContentFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems =
            [
                CreateNavigationLink("Test text", "chapter1.html", testTextContentFileRef)
            ];
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 NAV file with empty Lis should succeed")]
        public void GetNavigationItemsForEpub3NavWithEmptyLisTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_3),
                epub2Ncx: null,
                epub3NavDocument: new
                (
                    filePath: NAV_FILE_PATH,
                    navs:
                    [
                        new
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            ol: new
                            (
                                lis:
                                [
                                    new()
                                ]
                            )
                        )
                    ]
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubContentRef epubContentRef = CreateContentRef(testNavigationHtmlFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = [];
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 NAV file with null anchor href should succeed")]
        public void GetNavigationItemsForEpub3NavWithNullAnchorHrefTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_3),
                epub2Ncx: null,
                epub3NavDocument: new
                (
                    filePath: NAV_FILE_PATH,
                    navs:
                    [
                        new
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            ol: new
                            (
                                lis:
                                [
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: "Null href test",
                                            href: null
                                        )
                                    )
                                ]
                            )
                        )
                    ]
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubContentRef epubContentRef = CreateContentRef(testNavigationHtmlFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems =
            [
                CreateNavigationHeader("Null href test")
            ];
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 NAV file with null or empty titles should succeed")]
        public void GetNavigationItemsForEpub3NavWithNullOrEmptyTitlesTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_3),
                epub2Ncx: null,
                epub3NavDocument: new
                (
                    filePath: NAV_FILE_PATH,
                    navs:
                    [
                        new
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            ol: new
                            (
                                lis:
                                [
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: String.Empty,
                                            title: "Test title 1",
                                            href: "chapter1.html"
                                        )
                                    ),
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: "Test alt 2",
                                            href: "chapter1.html"
                                        )
                                    ),
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: String.Empty,
                                            title: String.Empty,
                                            alt: "Test alt 3",
                                            href: "chapter1.html"
                                        )
                                    ),
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: null,
                                            href: "chapter1.html"
                                        )
                                    ),
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: String.Empty,
                                            href: "chapter1.html"
                                        )
                                    ),
                                    new
                                    (
                                        span: new
                                        (
                                            text: String.Empty,
                                            title: "Test title 6"
                                        )
                                    ),
                                    new
                                    (
                                        span: new
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: "Test alt 7"
                                        )
                                    ),
                                    new
                                    (
                                        span: new
                                        (
                                            text: String.Empty,
                                            title: String.Empty,
                                            alt: "Test alt 8"
                                        )
                                    ),
                                    new
                                    (
                                        span: new
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: null
                                        )
                                    ),
                                    new
                                    (
                                        span: new
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: String.Empty
                                        )
                                    )
                                ]
                            )
                        )
                    ]
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubLocalTextContentFileRef testTextContentFileRef = CreateTestHtmlFile("chapter1.html");
            EpubContentRef epubContentRef = CreateContentRef(testNavigationHtmlFileRef, testTextContentFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems =
            [
                CreateNavigationLink("Test title 1", "chapter1.html", testTextContentFileRef),
                CreateNavigationLink("Test alt 2", "chapter1.html", testTextContentFileRef),
                CreateNavigationLink("Test alt 3", "chapter1.html", testTextContentFileRef),
                CreateNavigationLink(String.Empty, "chapter1.html", testTextContentFileRef),
                CreateNavigationLink(String.Empty, "chapter1.html", testTextContentFileRef),
                CreateNavigationHeader("Test title 6"),
                CreateNavigationHeader("Test alt 7"),
                CreateNavigationHeader("Test alt 8"),
                CreateNavigationHeader(String.Empty),
                CreateNavigationHeader(String.Empty),
            ];
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "GetNavigationItems should throw a Epub3NavException if the file referenced by a EPUB 3 navigation Li item is missing in the EpubContentRef")]
        public void GetNavigationItemsForEpub3WithMissingContentFile()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_3),
                epub2Ncx: null,
                epub3NavDocument: new
                (
                    filePath: NAV_FILE_PATH,
                    navs:
                    [
                        new
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            isHidden: false,
                            head: null,
                            ol: new
                            (
                                lis:
                                [
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: "Test text",
                                            href: "chapter1.html"
                                        )
                                    )
                                ]
                            )
                        )
                    ]
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubContentRef epubContentRef = new();
            Assert.Throws<Epub3NavException>(() => NavigationReader.GetNavigationItems(epubSchema, epubContentRef));
        }

        [Fact(DisplayName = "GetNavigationItems should throw Epub3NavException if a EPUB 3 navigation anchor points to a remote resource")]
        public void GetNavigationItemsWithRemoteContentFileTest()
        {
            string remoteFileHref = "https://example.com/books/123/chapter1.html";
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_3),
                epub2Ncx: null,
                epub3NavDocument: new
                (
                    filePath: NAV_FILE_PATH,
                    navs:
                    [
                        new
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            ol: new
                            (
                                lis:
                                [
                                    new
                                    (
                                        anchor: new
                                        (
                                            text: "Test text",
                                            href: remoteFileHref
                                        )
                                    )
                                ]
                            )
                        )
                    ]
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            List<EpubLocalTextContentFileRef> htmlLocal =
            [
                testNavigationHtmlFileRef
            ];
            EpubContentRef epubContentRef = new
            (
                navigationHtmlFile: testNavigationHtmlFileRef,
                html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(htmlLocal.AsReadOnly())
            );
            Assert.Throws<Epub3NavException>(() => NavigationReader.GetNavigationItems(epubSchema, epubContentRef));
        }

        private static EpubLocalTextContentFileRef CreateTestNavigationFile()
        {
            return CreateTestHtmlFile("toc.html");
        }

        private static EpubLocalTextContentFileRef CreateTestHtmlFile(string htmlFileName)
        {
            return CreateTestHtmlFile(CONTENT_DIRECTORY_PATH, htmlFileName);
        }

        private static EpubLocalTextContentFileRef CreateTestHtmlFile(string? directory, string htmlFileName)
        {
            return new(new EpubContentFileRefMetadata(htmlFileName, EpubContentType.XHTML_1_1, "application/xhtml+xml"),
                directory != null ? $"{directory}/{htmlFileName}" : htmlFileName, new TestEpubContentLoader());
        }

        private static EpubPackage CreateEmptyPackage(EpubVersion epubVersion)
        {
            return new
            (
                uniqueIdentifier: null,
                epubVersion: epubVersion,
                metadata: new(),
                manifest: new(),
                spine: new(),
                guide: null
            );
        }

        private static EpubNavigationItemRef CreateNavigationLink(string title, string htmlFileUrl, EpubLocalTextContentFileRef htmlFileRef)
        {
            return CreateNavigationLink(title, CONTENT_DIRECTORY_PATH, htmlFileUrl, htmlFileRef);
        }

        private static EpubNavigationItemRef CreateNavigationLink(string title, string directory, string htmlFileUrl, EpubLocalTextContentFileRef htmlFileRef)
        {
            return new
            (
                type: EpubNavigationItemType.LINK,
                title: title,
                link: new(htmlFileUrl, directory),
                htmlContentFileRef: htmlFileRef,
                nestedItems: null
            );
        }

        private static EpubNavigationItemRef CreateNavigationHeader(string title)
        {
            return new
            (
                type: EpubNavigationItemType.HEADER,
                title: title,
                link: null,
                htmlContentFileRef: null,
                nestedItems: null
            );
        }

        private static EpubContentRef CreateContentRef(EpubLocalTextContentFileRef? navigationHtmlFile, params EpubLocalTextContentFileRef[] htmlFiles)
        {
            List<EpubLocalTextContentFileRef> local = [];
            if (navigationHtmlFile != null)
            {
                local.Add(navigationHtmlFile);
            }
            foreach (EpubLocalTextContentFileRef htmlFile in htmlFiles)
            {
                local.Add(htmlFile);
            }
            EpubContentRef result = new
            (
                navigationHtmlFile: navigationHtmlFile,
                html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(local.AsReadOnly())
            );
            return result;
        }
    }
}
