using VersOne.Epub.Internal;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

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
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
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
                            Items = new List<Epub2NcxNavigationPoint>()
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
                }
            };
            EpubLocalTextContentFileRef testTextContentFileRef = CreateTestHtmlFile("chapter1.html");
            epubBookRef.Content = CreateContentRef(null, testTextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem1 = CreateNavigationLink("Test label 1", "chapter1.html", testTextContentFileRef);
            EpubNavigationItemRef expectedNavigationItem2 = CreateNavigationLink("Test label 3", "chapter1.html#section-1", testTextContentFileRef);
            expectedNavigationItem1.NestedItems.Add(expectedNavigationItem2);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                expectedNavigationItem1
            };
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
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
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
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
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubLocalTextContentFileRef testTextContentFileRef1 = CreateTestHtmlFile("chapter1.html");
            EpubLocalTextContentFileRef testTextContentFileRef2 = CreateTestHtmlFile("chapter2.html");
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
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
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
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubLocalTextContentFileRef testTextContentFileRef = CreateTestHtmlFile("chapter1.html");
            epubBookRef.Content = CreateContentRef(testNavigationHtmlFileRef, testTextContentFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                CreateNavigationLink("Test text", "chapter1.html", testTextContentFileRef)
            };
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
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
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            epubBookRef.Content = CreateContentRef(testNavigationHtmlFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = new();
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
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
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            epubBookRef.Content = CreateContentRef(testNavigationHtmlFileRef);
            List<EpubNavigationItemRef> expectedNavigationItems = new()
            {
                CreateNavigationLink("Null href test", null, null),
                CreateNavigationLink("Non-existent href test", "non-existent.html", null)
            };
            List<EpubNavigationItemRef> actualNavigationItems = NavigationReader.GetNavigationItems(epubBookRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
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
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubLocalTextContentFileRef testTextContentFileRef = CreateTestHtmlFile("chapter1.html");
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
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(expectedNavigationItems, actualNavigationItems);
        }

        [Fact(DisplayName = "GetNavigationItems should throw EpubPackageException if the content file referenced by a navigation item is a remote resource")]
        public void GetNavigationItemsWithRemoteContentFileTest()
        {
            string remoteFileHref = "https://example.com/books/123/chapter1.html";
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
                                                Href = remoteFileHref
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            EpubLocalTextContentFileRef testNavigationHtmlFileRef = CreateTestNavigationFile();
            EpubContentFileRefMetadata testTextContentFileRefMetadata = new(remoteFileHref, EpubContentType.XHTML_1_1, "application/xhtml+xml");
            EpubRemoteTextContentFileRef testTextContentFileRef = new(testTextContentFileRefMetadata, new TestEpubContentLoader());
            epubBookRef.Content = new EpubContentRef()
            {
                NavigationHtmlFile = testNavigationHtmlFileRef,
                Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalTextContentFileRef>()
                    {
                        {
                            testNavigationHtmlFileRef.Key,
                            testNavigationHtmlFileRef
                        }
                    },
                    Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            testTextContentFileRef.Key,
                            testTextContentFileRef
                        }
                    }
                }
            };
            Assert.Throws<EpubPackageException>(() => NavigationReader.GetNavigationItems(epubBookRef));
        }

        private EpubLocalTextContentFileRef CreateTestNavigationFile()
        {
            return CreateTestHtmlFile("toc.html");
        }

        private EpubLocalTextContentFileRef CreateTestHtmlFile(string htmlFileName)
        {
            return new(new EpubContentFileRefMetadata(htmlFileName, EpubContentType.XHTML_1_1, "application/xhtml+xml"),
                $"{CONTENT_DIRECTORY_PATH}/{htmlFileName}", new TestEpubContentLoader());
        }

        private EpubNavigationItemRef CreateNavigationLink(string title, string htmlFileUrl, EpubLocalTextContentFileRef htmlFileRef)
        {
            return new()
            {
                Type = EpubNavigationItemType.LINK,
                Title = title,
                Link = new EpubNavigationItemLink(htmlFileUrl, CONTENT_DIRECTORY_PATH),
                HtmlContentFileRef = htmlFileRef,
                NestedItems = new List<EpubNavigationItemRef>()
            };
        }

        private EpubNavigationItemRef CreateNavigationHeader(string title)
        {
            return new()
            {
                Type = EpubNavigationItemType.HEADER,
                Title = title,
                NestedItems = new List<EpubNavigationItemRef>()
            };
        }

        private EpubContentRef CreateContentRef(EpubLocalTextContentFileRef navigationHtmlFile, params EpubLocalTextContentFileRef[] htmlFiles)
        {
            EpubContentRef result = new()
            {
                Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalTextContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                }
            };
            if (navigationHtmlFile != null)
            {
                result.NavigationHtmlFile = navigationHtmlFile;
                result.Html.Local[navigationHtmlFile.Key] = navigationHtmlFile;
            }
            foreach (EpubLocalTextContentFileRef htmlFile in htmlFiles)
            {
                result.Html.Local[htmlFile.Key] = htmlFile;
            }
            return result;
        }
    }
}
