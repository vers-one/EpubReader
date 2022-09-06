using VersOne.Epub.Internal;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Mocks;
using VersOne.Epub.Test.Unit.TestUtils;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class NavigationReaderTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";

        [Fact(DisplayName = "GetNavigationItems should return null for EPUB 2 books without NCX file")]
        public void GetNavigationItemsForEpub2WithoutNcxTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    Package = new EpubPackage()
                    {
                        EpubVersion = EpubVersion.EPUB_2
                    },
                    Epub2Ncx = null
                }
            };
            List<EpubNavigationItemRef> navigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            Assert.Null(navigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 2 books with minimal NCX file should succeed")]
        public void GetNavigationItemsForEpub2WithMinimalNcxTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    Package = new EpubPackage()
                    {
                        EpubVersion = EpubVersion.EPUB_2
                    },
                    Epub2Ncx = new Epub2Ncx()
                    {
                        NavMap = new Epub2NcxNavigationMap()
                    }
                }
            };
            List<EpubNavigationItemRef> expectedNavigationItems = new();
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 2 books with full NCX file should succeed")]
        public void GetNavigationItemsForEpub2WithFullNcxTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    ContentDirectoryPath = CONTENT_DIRECTORY_PATH,
                    Package = new EpubPackage()
                    {
                        EpubVersion = EpubVersion.EPUB_2
                    },
                    Epub2Ncx = new Epub2Ncx()
                    {
                        NavMap = new Epub2NcxNavigationMap()
                        {
                            new Epub2NcxNavigationPoint()
                            {
                                NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Test label 1"
                                    },
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Test label 2"
                                    }
                                },
                                Content = new Epub2NcxContent()
                                {
                                    Source = "chapter1.html"
                                },
                                ChildNavigationPoints = new List<Epub2NcxNavigationPoint>()
                                {
                                    new Epub2NcxNavigationPoint()
                                    {
                                        NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                        {
                                            new Epub2NcxNavigationLabel()
                                            {
                                                Text = "Test label 3"
                                            }
                                        },
                                        Content = new Epub2NcxContent()
                                        {
                                            Source = "chapter1.html#section-1"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            EpubTextContentFileRef testTextContentFileRef = CreateTestHtmlFile(epubBookRef, "chapter1.html");
            epubBookRef.Content = CreateContentRef(null, testTextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem1 = CreateNavigationLink("Test label 1", "chapter1.html", testTextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem2 = CreateNavigationLink("Test label 3", "chapter1.html#section-1", testTextContentFileRef);
            expectedNavigationItem1.NestedItems.Add(expectedNavigationItem2);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                expectedNavigationItem1
            };
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 books with minimal NAV file should succeed")]
        public void GetNavigationItemsForEpub3WithMinimalNavTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    Package = new EpubPackage()
                    {
                        EpubVersion = EpubVersion.EPUB_3
                    },
                    Epub3NavDocument = new Epub3NavDocument()
                    {
                        Navs = new List<Epub3Nav>()
                    }
                }
            };
            List<EpubNavigationItemRef> expectedNavigationItems = new();
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 books with full NAV file should succeed")]
        public void GetNavigationItemsForEpub3WithFullNavTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    ContentDirectoryPath = CONTENT_DIRECTORY_PATH,
                    Package = new EpubPackage()
                    {
                        EpubVersion = EpubVersion.EPUB_3
                    },
                    Epub3NavDocument = new Epub3NavDocument()
                    {
                        Navs = new List<Epub3Nav>()
                        {
                            new Epub3Nav()
                            {
                                Type = Epub3NavStructuralSemanticsProperty.TOC,
                                Head = "Test header",
                                Ol = new Epub3NavOl()
                                {
                                    Lis = new List<Epub3NavLi>()
                                    {
                                        new Epub3NavLi()
                                        {
                                            Anchor = new Epub3NavAnchor()
                                            {
                                                Text = "Test text 1",
                                                Title = "Test title 1",
                                                Alt = "Test alt 1",
                                                Href = "chapter1.html"
                                            },
                                            ChildOl = new Epub3NavOl()
                                            {
                                                Lis = new List<Epub3NavLi>()
                                                {
                                                    new Epub3NavLi()
                                                    {
                                                        Anchor = new Epub3NavAnchor()
                                                        {
                                                            Text = "Test text 2",
                                                            Title = "Test title 2",
                                                            Alt = "Test alt 2",
                                                            Href = "chapter1.html#section-1"
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Span = new Epub3NavSpan()
                                            {
                                                Text = "Test text 3",
                                                Title = "Test title 3",
                                                Alt = "Test alt 3"
                                            },
                                            ChildOl = new Epub3NavOl()
                                            {
                                                Lis = new List<Epub3NavLi>()
                                                {
                                                    new Epub3NavLi()
                                                    {
                                                        Anchor = new Epub3NavAnchor()
                                                        {
                                                            Text = "Test text 4",
                                                            Title = "Test title 4",
                                                            Alt = "Test alt 4",
                                                            Href = "chapter2.html"
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            EpubTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile(epubBookRef);
            EpubTextContentFileRef testTextContentFileRef1 = CreateTestHtmlFile(epubBookRef, "chapter1.html");
            EpubTextContentFileRef testTextContentFileRef2 = CreateTestHtmlFile(epubBookRef, "chapter2.html");
            epubBookRef.Content = CreateContentRef(testNavigationHtmlFileRef, testTextContentFileRef1, testTextContentFileRef2);
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
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 NAV file without a header should succeed")]
        public void GetNavigationItemsForEpub3NavWithoutHeaderTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    ContentDirectoryPath = CONTENT_DIRECTORY_PATH,
                    Package = new EpubPackage()
                    {
                        EpubVersion = EpubVersion.EPUB_3
                    },
                    Epub3NavDocument = new Epub3NavDocument()
                    {
                        Navs = new List<Epub3Nav>()
                        {
                            new Epub3Nav()
                            {
                                Type = Epub3NavStructuralSemanticsProperty.TOC,
                                Head = null,
                                Ol = new Epub3NavOl()
                                {
                                    Lis = new List<Epub3NavLi>()
                                    {
                                        new Epub3NavLi()
                                        {
                                            Anchor = new Epub3NavAnchor()
                                            {
                                                Text = "Test text",
                                                Href = "chapter1.html"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            EpubTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile(epubBookRef);
            EpubTextContentFileRef testTextContentFileRef = CreateTestHtmlFile(epubBookRef, "chapter1.html");
            epubBookRef.Content = CreateContentRef(testNavigationHtmlFileRef, testTextContentFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                CreateNavigationLink("Test text", "chapter1.html", testTextContentFileRef)
            };
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 NAV file with null or empty Lis should succeed")]
        public void GetNavigationItemsForEpub3NavWithNullOrEmptyLisTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    ContentDirectoryPath = CONTENT_DIRECTORY_PATH,
                    Package = new EpubPackage()
                    {
                        EpubVersion = EpubVersion.EPUB_3
                    },
                    Epub3NavDocument = new Epub3NavDocument()
                    {
                        Navs = new List<Epub3Nav>()
                        {
                            new Epub3Nav()
                            {
                                Type = Epub3NavStructuralSemanticsProperty.TOC,
                                Head = null,
                                Ol = new Epub3NavOl()
                                {
                                    Lis = new List<Epub3NavLi>()
                                    {
                                        new Epub3NavLi()
                                        {
                                        },
                                        null
                                    }
                                }
                            }
                        }
                    }
                }
            };
            EpubTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile(epubBookRef);
            epubBookRef.Content = CreateContentRef(testNavigationHtmlFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = new();
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 NAV file with null or non-existent anchor hrefs should succeed")]
        public void GetNavigationItemsForEpub3NavWithNullOrNonExistentAnchorHrefsTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    ContentDirectoryPath = CONTENT_DIRECTORY_PATH,
                    Package = new EpubPackage()
                    {
                        EpubVersion = EpubVersion.EPUB_3
                    },
                    Epub3NavDocument = new Epub3NavDocument()
                    {
                        Navs = new List<Epub3Nav>()
                        {
                            new Epub3Nav()
                            {
                                Type = Epub3NavStructuralSemanticsProperty.TOC,
                                Head = null,
                                Ol = new Epub3NavOl()
                                {
                                    Lis = new List<Epub3NavLi>()
                                    {
                                        new Epub3NavLi()
                                        {
                                            Anchor = new Epub3NavAnchor()
                                            {
                                                Text = "Null href test",
                                                Href = null
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Anchor = new Epub3NavAnchor()
                                            {
                                                Text = "Non-existent href test",
                                                Href = "non-existent.html"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            EpubTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile(epubBookRef);
            epubBookRef.Content = CreateContentRef(testNavigationHtmlFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                CreateNavigationLink("Null href test", null, null),
                CreateNavigationLink("Non-existent href test", "non-existent.html", null)
            };
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "Getting navigation items for EPUB 3 NAV file with null or empty titles should succeed")]
        public void GetNavigationItemsForEpub3NavWithNullOrEmptyTitlesTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    ContentDirectoryPath = CONTENT_DIRECTORY_PATH,
                    Package = new EpubPackage()
                    {
                        EpubVersion = EpubVersion.EPUB_3
                    },
                    Epub3NavDocument = new Epub3NavDocument()
                    {
                        Navs = new List<Epub3Nav>()
                        {
                            new Epub3Nav()
                            {
                                Type = Epub3NavStructuralSemanticsProperty.TOC,
                                Head = null,
                                Ol = new Epub3NavOl()
                                {
                                    Lis = new List<Epub3NavLi>()
                                    {
                                        new Epub3NavLi()
                                        {
                                            Anchor = new Epub3NavAnchor()
                                            {
                                                Text = null,
                                                Title = "Test title 1",
                                                Href = "chapter1.html"
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Anchor = new Epub3NavAnchor()
                                            {
                                                Text = String.Empty,
                                                Title = "Test title 2",
                                                Href = "chapter1.html"
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Anchor = new Epub3NavAnchor()
                                            {
                                                Text = null,
                                                Title = null,
                                                Alt = "Test alt 3",
                                                Href = "chapter1.html"
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Anchor = new Epub3NavAnchor()
                                            {
                                                Text = null,
                                                Title = String.Empty,
                                                Alt = "Test alt 4",
                                                Href = "chapter1.html"
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Anchor = new Epub3NavAnchor()
                                            {
                                                Text = null,
                                                Title = null,
                                                Alt = null,
                                                Href = "chapter1.html"
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Anchor = new Epub3NavAnchor()
                                            {
                                                Text = null,
                                                Title = null,
                                                Alt = String.Empty,
                                                Href = "chapter1.html"
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Span = new Epub3NavSpan()
                                            {
                                                Text = null,
                                                Title = "Test title 7"
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Span = new Epub3NavSpan()
                                            {
                                                Text = String.Empty,
                                                Title = "Test title 8"
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Span = new Epub3NavSpan()
                                            {
                                                Text = null,
                                                Title = null,
                                                Alt = "Test alt 9"
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Span = new Epub3NavSpan()
                                            {
                                                Text = null,
                                                Title = String.Empty,
                                                Alt = "Test alt 10"
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Span = new Epub3NavSpan()
                                            {
                                                Text = null,
                                                Title = null,
                                                Alt = null
                                            }
                                        },
                                        new Epub3NavLi()
                                        {
                                            Span = new Epub3NavSpan()
                                            {
                                                Text = null,
                                                Title = null,
                                                Alt = String.Empty
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            EpubTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile(epubBookRef);
            EpubTextContentFileRef testTextContentFileRef = CreateTestHtmlFile(epubBookRef, "chapter1.html");
            epubBookRef.Content = CreateContentRef(testNavigationHtmlFileRef, testTextContentFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                CreateNavigationLink("Test title 1", "chapter1.html", testTextContentFileRef),
                CreateNavigationLink("Test title 2", "chapter1.html", testTextContentFileRef),
                CreateNavigationLink("Test alt 3", "chapter1.html", testTextContentFileRef),
                CreateNavigationLink("Test alt 4", "chapter1.html", testTextContentFileRef),
                CreateNavigationLink(String.Empty, "chapter1.html", testTextContentFileRef),
                CreateNavigationLink(String.Empty, "chapter1.html", testTextContentFileRef),
                CreateNavigationHeader("Test title 7"),
                CreateNavigationHeader("Test title 8"),
                CreateNavigationHeader("Test alt 9"),
                CreateNavigationHeader("Test alt 10"),
                CreateNavigationHeader(String.Empty),
                CreateNavigationHeader(String.Empty),
            };
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        private EpubTextContentFileRef CreateTestNavigationFile(EpubBookRef epubBookRef)
        {
            return CreateTestHtmlFile(epubBookRef, "toc.html");
        }

        private EpubTextContentFileRef CreateTestHtmlFile(EpubBookRef epubBookRef, string htmlFileName)
        {
            return new(epubBookRef, htmlFileName, EpubContentType.XHTML_1_1, "application/xhtml+xml");
        }

        private EpubNavigationItemRef CreateNavigationLink(string title, string htmlFileUrl, EpubTextContentFileRef htmlFileRef)
        {
            EpubNavigationItemRef result = EpubNavigationItemRef.CreateAsLink();
            result.Title = title;
            result.Link = new EpubNavigationItemLink(htmlFileUrl, CONTENT_DIRECTORY_PATH);
            result.HtmlContentFileRef = htmlFileRef;
            result.NestedItems = new List<EpubNavigationItemRef>();
            return result;
        }

        private EpubNavigationItemRef CreateNavigationHeader(string title)
        {
            EpubNavigationItemRef result = EpubNavigationItemRef.CreateAsHeader();
            result.Title = title;
            result.NestedItems = new List<EpubNavigationItemRef>();
            return result;
        }

        private EpubContentRef CreateContentRef(EpubTextContentFileRef navigationHtmlFile, params EpubTextContentFileRef[] htmlFiles)
        {
            EpubContentRef result = new()
            {
                Html = new Dictionary<string, EpubTextContentFileRef>()
            };
            if (navigationHtmlFile != null)
            {
                result.NavigationHtmlFile = navigationHtmlFile;
                result.Html[navigationHtmlFile.FileName] = navigationHtmlFile;
            }
            foreach (EpubTextContentFileRef htmlFile in htmlFiles)
            {
                result.Html[htmlFile.FileName] = htmlFile;
            }
            return result;
        }

        private void CompareNavigationItemRefLists(List<EpubNavigationItemRef> expected, List<EpubNavigationItemRef> actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                AssertUtils.CollectionsEqual(expected, actual, CompareNavigationItemRefs);
            }
        }

        private void CompareNavigationItemRefs(EpubNavigationItemRef expected, EpubNavigationItemRef actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Title, actual.Title);
            CompareNavigationItemLinks(expected.Link, actual.Link);
            Assert.Equal(expected.HtmlContentFileRef, actual.HtmlContentFileRef);
            CompareNavigationItemRefLists(expected.NestedItems, actual.NestedItems);
        }

        private void CompareNavigationItemLinks(EpubNavigationItemLink expected, EpubNavigationItemLink actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.ContentFileName, actual.ContentFileName);
                Assert.Equal(expected.ContentFilePathInEpubArchive, actual.ContentFilePathInEpubArchive);
                Assert.Equal(expected.Anchor, actual.Anchor);
            }
        }
    }
}
