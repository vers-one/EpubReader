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
                epub2Ncx: new Epub2Ncx
                (
                    filePath: NCX_FILE_PATH,
                    head: new Epub2NcxHead(),
                    docTitle: null,
                    docAuthors: null,
                    navMap: new Epub2NcxNavigationMap(),
                    pageList: null,
                    navLists: null
                ),
                epub3NavDocument: null,
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubContentRef epubContentRef = new();
            List<EpubNavigationItemRef> expectedNavigationItems = new();
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 2 schemas with full NCX file should succeed")]
        public void GetNavigationItemsForEpub2WithFullNcxTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_2),
                epub2Ncx: new Epub2Ncx
                (
                    filePath: NCX_FILE_PATH,
                    head: new Epub2NcxHead(),
                    docTitle: null,
                    docAuthors: null,
                    navMap: new Epub2NcxNavigationMap
                    (
                        items: new List<Epub2NcxNavigationPoint>()
                        {
                            new Epub2NcxNavigationPoint
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels: new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel
                                    (
                                        text: "Test label 1"
                                    ),
                                    new Epub2NcxNavigationLabel
                                    (
                                        text: "Test label 2"
                                    )
                                },
                                content: new Epub2NcxContent
                                (
                                    source: "chapter1.html"
                                ),
                                childNavigationPoints: new List<Epub2NcxNavigationPoint>()
                                {
                                    new Epub2NcxNavigationPoint
                                    (
                                        id: String.Empty,
                                        navigationLabels: new List<Epub2NcxNavigationLabel>()
                                        {
                                            new Epub2NcxNavigationLabel
                                            (
                                                text: "Test label 3"
                                            )
                                        },
                                        content: new Epub2NcxContent
                                        (
                                            source: "chapter1.html#section-1"
                                        )
                                    )
                                }
                            )
                        }
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
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                expectedNavigationItem1
            };
            List<EpubNavigationItemRef>? actualNavigationItems = NavigationReader.GetNavigationItems(epubSchema, epubContentRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "GetNavigationItems should throw a Epub2NcxException if an NCX navigation point has no navigation labels")]
        public void GetNavigationItemsForEpub2WithoutNavigationLabelsTest()
        {
            EpubSchema epubSchema = new
            (
                package: CreateEmptyPackage(EpubVersion.EPUB_2),
                epub2Ncx: new Epub2Ncx
                (
                    filePath: NCX_FILE_PATH,
                    head: new Epub2NcxHead(),
                    docTitle: null,
                    docAuthors: null,
                    navMap: new Epub2NcxNavigationMap
                    (
                        items: new List<Epub2NcxNavigationPoint>()
                        {
                            new Epub2NcxNavigationPoint
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels: new List<Epub2NcxNavigationLabel>(),
                                content: new Epub2NcxContent
                                (
                                    source: "chapter1.html"
                                ),
                                childNavigationPoints: null
                            )
                        }
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
                epub2Ncx: new Epub2Ncx
                (
                    filePath: NCX_FILE_PATH,
                    head: new Epub2NcxHead(),
                    docTitle: null,
                    docAuthors: null,
                    navMap: new Epub2NcxNavigationMap
                    (
                        items: new List<Epub2NcxNavigationPoint>()
                        {
                            new Epub2NcxNavigationPoint
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels: new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel
                                    (
                                        text: "Test label"
                                    )
                                },
                                content: new Epub2NcxContent
                                (
                                    source: remoteFileHref
                                ),
                                childNavigationPoints: null
                            )
                        }
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
                epub2Ncx: new Epub2Ncx
                (
                    filePath: NCX_FILE_PATH,
                    head: new Epub2NcxHead(),
                    docTitle: null,
                    docAuthors: null,
                    navMap: new Epub2NcxNavigationMap
                    (
                        items: new List<Epub2NcxNavigationPoint>()
                        {
                            new Epub2NcxNavigationPoint
                            (
                                id: String.Empty,
                                @class: null,
                                playOrder: null,
                                navigationLabels: new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel
                                    (
                                        text: "Test label"
                                    )
                                },
                                content: new Epub2NcxContent
                                (
                                    source: "chapter1.html"
                                ),
                                childNavigationPoints: null
                            )
                        }
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
                epub3NavDocument: new Epub3NavDocument
                (
                    filePath: NAV_FILE_PATH
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubContentRef epubContentRef = new();
            List<EpubNavigationItemRef> expectedNavigationItems = new();
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
                epub3NavDocument: new Epub3NavDocument
                (
                    filePath: NAV_FILE_PATH,
                    navs: new List<Epub3Nav>()
                    {
                        new Epub3Nav
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            isHidden: false,
                            head: "Test header",
                            ol: new Epub3NavOl
                            (
                                lis: new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            text: "Test text 1",
                                            title: "Test title 1",
                                            alt: "Test alt 1",
                                            href: "chapter1.html"
                                        ),
                                        childOl: new Epub3NavOl
                                        (
                                            lis: new List<Epub3NavLi>()
                                            {
                                                new Epub3NavLi
                                                (
                                                    anchor: new Epub3NavAnchor
                                                    (
                                                        text: "Test text 2",
                                                        title: "Test title 2",
                                                        alt: "Test alt 2",
                                                        href: "chapter1.html#section-1"
                                                    )
                                                )
                                            }
                                        )
                                    ),
                                    new Epub3NavLi
                                    (
                                        span: new Epub3NavSpan
                                        (
                                            text: "Test text 3",
                                            title: "Test title 3",
                                            alt: "Test alt 3"
                                        ),
                                        childOl: new Epub3NavOl
                                        (
                                            lis: new List<Epub3NavLi>()
                                            {
                                                new Epub3NavLi
                                                (
                                                    anchor: new Epub3NavAnchor
                                                    (
                                                        text: "Test text 4",
                                                        title: "Test title 4",
                                                        alt: "Test alt 4",
                                                        href: "chapter2.html"
                                                    )
                                                )
                                            }
                                        )
                                    )
                                }
                            )
                        )
                    }
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
            expectedNavigationItem1.NestedItems.AddRange(new[] { expectedNavigationItem2, expectedNavigationItem4 });
            expectedNavigationItem2.NestedItems.Add(expectedNavigationItem3);
            expectedNavigationItem4.NestedItems.Add(expectedNavigationItem5);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                expectedNavigationItem1
            };
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
                epub3NavDocument: new Epub3NavDocument
                (
                    filePath: NAV_FILE_PATH,
                    navs: new List<Epub3Nav>()
                    {
                        new Epub3Nav
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            isHidden: false,
                            head: null,
                            ol: new Epub3NavOl
                            (
                                lis: new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            text: "Test text",
                                            href: "chapter1.html"
                                        )
                                    )
                                }
                            )
                        )
                    }
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubLocalTextContentFileRef testTextContentFileRef = CreateTestHtmlFile("chapter1.html");
            EpubContentRef epubContentRef = CreateContentRef(testNavigationHtmlFileRef, testTextContentFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                CreateNavigationLink("Test text", "chapter1.html", testTextContentFileRef)
            };
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
                epub3NavDocument: new Epub3NavDocument
                (
                    filePath: NAV_FILE_PATH,
                    navs: new List<Epub3Nav>()
                    {
                        new Epub3Nav
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            ol: new Epub3NavOl
                            (
                                lis: new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi()
                                }
                            )
                        )
                    }
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubContentRef epubContentRef = CreateContentRef(testNavigationHtmlFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = new();
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
                epub3NavDocument: new Epub3NavDocument
                (
                    filePath: NAV_FILE_PATH,
                    navs: new List<Epub3Nav>()
                    {
                        new Epub3Nav
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            ol: new Epub3NavOl
                            (
                                lis: new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            text: "Null href test",
                                            href: null
                                        )
                                    )
                                }
                            )
                        )
                    }
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubContentRef epubContentRef = CreateContentRef(testNavigationHtmlFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                CreateNavigationHeader("Null href test")
            };
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
                epub3NavDocument: new Epub3NavDocument
                (
                    filePath: NAV_FILE_PATH,
                    navs: new List<Epub3Nav>()
                    {
                        new Epub3Nav
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            ol: new Epub3NavOl
                            (
                                lis: new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            text: String.Empty,
                                            title: "Test title 1",
                                            href: "chapter1.html"
                                        )
                                    ),
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: "Test alt 2",
                                            href: "chapter1.html"
                                        )
                                    ),
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            text: String.Empty,
                                            title: String.Empty,
                                            alt: "Test alt 3",
                                            href: "chapter1.html"
                                        )
                                    ),
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: null,
                                            href: "chapter1.html"
                                        )
                                    ),
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: String.Empty,
                                            href: "chapter1.html"
                                        )
                                    ),
                                    new Epub3NavLi
                                    (
                                        span: new Epub3NavSpan
                                        (
                                            text: String.Empty,
                                            title: "Test title 6"
                                        )
                                    ),
                                    new Epub3NavLi
                                    (
                                        span: new Epub3NavSpan
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: "Test alt 7"
                                        )
                                    ),
                                    new Epub3NavLi
                                    (
                                        span: new Epub3NavSpan
                                        (
                                            text: String.Empty,
                                            title: String.Empty,
                                            alt: "Test alt 8"
                                        )
                                    ),
                                    new Epub3NavLi
                                    (
                                        span: new Epub3NavSpan
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: null
                                        )
                                    ),
                                    new Epub3NavLi
                                    (
                                        span: new Epub3NavSpan
                                        (
                                            text: String.Empty,
                                            title: null,
                                            alt: String.Empty
                                        )
                                    )
                                }
                            )
                        )
                    }
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubLocalTextContentFileRef testTextContentFileRef = CreateTestHtmlFile("chapter1.html");
            EpubContentRef epubContentRef = CreateContentRef(testNavigationHtmlFileRef, testTextContentFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
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
            };
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
                epub3NavDocument: new Epub3NavDocument
                (
                    filePath: NAV_FILE_PATH,
                    navs: new List<Epub3Nav>()
                    {
                        new Epub3Nav
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            isHidden: false,
                            head: null,
                            ol: new Epub3NavOl
                            (
                                lis: new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            text: "Test text",
                                            href: "chapter1.html"
                                        )
                                    )
                                }
                            )
                        )
                    }
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
                epub3NavDocument: new Epub3NavDocument
                (
                    filePath: NAV_FILE_PATH,
                    navs: new List<Epub3Nav>()
                    {
                        new Epub3Nav
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            ol: new Epub3NavOl
                            (
                                lis: new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            text: "Test text",
                                            href: remoteFileHref
                                        )
                                    )
                                }
                            )
                        )
                    }
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            List<EpubLocalTextContentFileRef> htmlLocal = new()
            {
                testNavigationHtmlFileRef
            };
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
            return new(new EpubContentFileRefMetadata(htmlFileName, EpubContentType.XHTML_1_1, "application/xhtml+xml"),
                $"{CONTENT_DIRECTORY_PATH}/{htmlFileName}", new TestEpubContentLoader());
        }

        private static EpubPackage CreateEmptyPackage(EpubVersion epubVersion)
        {
            return new
            (
                uniqueIdentifier: null,
                epubVersion: epubVersion,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest(),
                spine: new EpubSpine(),
                guide: null
            );
        }

        private static EpubNavigationItemRef CreateNavigationLink(string title, string htmlFileUrl, EpubLocalTextContentFileRef htmlFileRef)
        {
            return new
            (
                type: EpubNavigationItemType.LINK,
                title: title,
                link: new EpubNavigationItemLink(htmlFileUrl, CONTENT_DIRECTORY_PATH),
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
            List<EpubLocalTextContentFileRef> local = new();
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
