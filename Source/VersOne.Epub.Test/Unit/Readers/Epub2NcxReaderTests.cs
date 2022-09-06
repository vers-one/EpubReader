using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class Epub2NcxReaderTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string NCX_FILE_NAME = "toc.ncx";
        private const string NCX_FILE_PATH_IN_EPUB_ARCHIVE = $"{CONTENT_DIRECTORY_PATH}/{NCX_FILE_NAME}";
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
                    <content id="content-1-1" src="chapter1.html#section-1"/>
                  </navPoint>
                  <navPoint id="navpoint-1-2" class="section">
                    <navLabel>
                      <text>Chapter 1.2</text>
                    </navLabel>
                    <content id="content-1-2" src="chapter1.html#section-2"/>
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
                  <content src="front.html"/>
                </pageTarget>
                <pageTarget type="normal">
                  <navLabel>
                    <text>2</text>
                  </navLabel>
                  <content id="content-2" src="chapter1.html#page-2"/>
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
                  <content src="chapter1.html#illustration-1"/>
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
                  <content src="chapter1.html#table-1"/>
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
                  <content src="chapter1.html#page-1"/>
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
                  <content src="chapter1.html#page-1"/>
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
                  <content src="chapter1.html#page-1"/>
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

        private EpubPackage MinimalEpubPackageWithNcx =>
            new()
            {
                Manifest = new EpubManifest()
                {
                    new EpubManifestItem()
                    {
                        Id = TOC_ID,
                        Href = NCX_FILE_NAME,
                        MediaType = "application/x-dtbncx+xml"
                    }
                },
                Spine = new EpubSpine()
                {
                    Toc = TOC_ID
                }
            };

        private Epub2Ncx MinimalEpub2Ncx =>
            new()
            {
                Head = new Epub2NcxHead(),
                DocTitle = null,
                DocAuthors = new List<string>(),
                NavMap = new Epub2NcxNavigationMap(),
                PageList = null,
                NavLists = new List<Epub2NcxNavigationList>()
            };

        private Epub2Ncx FullEpub2Ncx =>
            new()
            {
                Head = new Epub2NcxHead()
                {
                    new Epub2NcxHeadMeta()
                    {
                        Name = "dtb:uid",
                        Content = "9781234567890"
                    },
                    new Epub2NcxHeadMeta()
                    {
                        Name = "dtb:depth",
                        Content = "1"
                    },
                    new Epub2NcxHeadMeta()
                    {
                        Name = "dtb:generator",
                        Content = "EpubWriter"
                    },
                    new Epub2NcxHeadMeta()
                    {
                        Name = "dtb:totalPageCount",
                        Content = "0"
                    },
                    new Epub2NcxHeadMeta()
                    {
                        Name = "dtb:maxPageNumber",
                        Content = "0"
                    },
                    new Epub2NcxHeadMeta()
                    {
                        Name = "location",
                        Content = "https://example.com/books/123/ncx",
                        Scheme = "URI"
                    }
                },
                DocTitle = "Test title",
                DocAuthors = new List<string>()
                {
                    "John Doe",
                    "Jane Doe"
                },
                NavMap = new Epub2NcxNavigationMap()
                {
                    new Epub2NcxNavigationPoint()
                    {
                        Id = "navpoint-1",
                        Class = "chapter",
                        PlayOrder = "1",
                        NavigationLabels = new List<Epub2NcxNavigationLabel>()
                        {
                            new Epub2NcxNavigationLabel()
                            {
                                Text = "Chapter 1"
                            },
                            new Epub2NcxNavigationLabel()
                            {
                                Text = "Capitolo 1"
                            }
                        },
                        Content = new Epub2NcxContent()
                        {
                            Id = "content-1",
                            Source = "chapter1.html"
                        },
                        ChildNavigationPoints = new List<Epub2NcxNavigationPoint>()
                        {
                            new Epub2NcxNavigationPoint()
                            {
                                Id = "navpoint-1-1",
                                Class = "section",
                                NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Chapter 1.1"
                                    }
                                },
                                Content = new Epub2NcxContent()
                                {
                                    Id = "content-1-1",
                                    Source = "chapter1.html#section-1"
                                },
                                ChildNavigationPoints = new List<Epub2NcxNavigationPoint>()
                            },
                            new Epub2NcxNavigationPoint()
                            {
                                Id = "navpoint-1-2",
                                Class = "section",
                                NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Chapter 1.2"
                                    }
                                },
                                Content = new Epub2NcxContent()
                                {
                                    Id = "content-1-2",
                                    Source = "chapter1.html#section-2"
                                },
                                ChildNavigationPoints = new List<Epub2NcxNavigationPoint>()
                            }
                        }
                    },
                    new Epub2NcxNavigationPoint()
                    {
                        Id = "navpoint-2",
                        NavigationLabels = new List<Epub2NcxNavigationLabel>()
                        {
                            new Epub2NcxNavigationLabel()
                            {
                                Text = "Chapter 2"
                            }
                        },
                        Content = new Epub2NcxContent()
                        {
                            Source = "chapter2.html"
                        },
                        ChildNavigationPoints = new List<Epub2NcxNavigationPoint>()
                    }
                },
                PageList = new Epub2NcxPageList()
                {
                    new Epub2NcxPageTarget()
                    {
                        Id = "page-target-1",
                        Value = "1",
                        Type = Epub2NcxPageTargetType.FRONT,
                        Class = "front-matter",
                        PlayOrder = "1",
                        NavigationLabels = new List<Epub2NcxNavigationLabel>()
                        {
                            new Epub2NcxNavigationLabel()
                            {
                                Text = "1"
                            },
                            new Epub2NcxNavigationLabel()
                            {
                                Text = "I"
                            }
                        },
                        Content = new Epub2NcxContent()
                        {
                            Source = "front.html"
                        }
                    },
                    new Epub2NcxPageTarget()
                    {
                        Type = Epub2NcxPageTargetType.NORMAL,
                        NavigationLabels = new List<Epub2NcxNavigationLabel>()
                        {
                            new Epub2NcxNavigationLabel()
                            {
                                Text = "2"
                            }
                        },
                        Content = new Epub2NcxContent
                        {
                            Id = "content-2",
                            Source = "chapter1.html#page-2"
                        }
                    }
                },
                NavLists = new List<Epub2NcxNavigationList>()
                {
                    new Epub2NcxNavigationList()
                    {
                        Id = "navlist-1",
                        Class = "navlist-illustrations",
                        NavigationLabels = new List<Epub2NcxNavigationLabel>()
                        {
                            new Epub2NcxNavigationLabel()
                            {
                                Text = "List of Illustrations"
                            },
                            new Epub2NcxNavigationLabel()
                            {
                                Text = "Illustrazioni"
                            }
                        },
                        NavigationTargets = new List<Epub2NcxNavigationTarget>()
                        {
                            new Epub2NcxNavigationTarget()
                            {
                                Id = "navtarget-1",
                                Value = "Illustration 1",
                                Class = "illustration",
                                PlayOrder = "1",
                                NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Illustration 1"
                                    },
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Illustrazione 1"
                                    }
                                },
                                Content = new Epub2NcxContent()
                                {
                                    Source = "chapter1.html#illustration-1"
                                }
                            }
                        }
                    },
                    new Epub2NcxNavigationList()
                    {
                        Id = "navlist-2",
                        Class = "navlist-tables",
                        NavigationLabels = new List<Epub2NcxNavigationLabel>()
                        {
                            new Epub2NcxNavigationLabel()
                            {
                                Text = "List of Tables"
                            }
                        },
                        NavigationTargets = new List<Epub2NcxNavigationTarget>()
                        {
                            new Epub2NcxNavigationTarget()
                            {
                                Id = "navtarget-2",
                                NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Tables"
                                    }
                                }
                            },
                            new Epub2NcxNavigationTarget()
                            {
                                Id = "navtarget-3",
                                NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Table 1"
                                    }
                                },
                                Content = new Epub2NcxContent()
                                {
                                    Source = "chapter1.html#table-1"
                                }
                            }
                        }
                    }
                }
            };

        private Epub2Ncx MinimalEpub2NcxWithUnknownPageTargetType =>
            new()
            {
                Head = new Epub2NcxHead(),
                DocTitle = null,
                DocAuthors = new List<string>(),
                NavMap = new Epub2NcxNavigationMap(),
                PageList = new Epub2NcxPageList()
                {
                    new Epub2NcxPageTarget()
                    {
                        Type = Epub2NcxPageTargetType.UNKNOWN,
                        NavigationLabels = new List<Epub2NcxNavigationLabel>()
                        {
                            new Epub2NcxNavigationLabel()
                            {
                                Text = "1"
                            }
                        },
                        Content = new Epub2NcxContent()
                        {
                            Source = "chapter1.html#page-1"
                        }
                    }
                },
                NavLists = new List<Epub2NcxNavigationList>()
            };


        [Fact(DisplayName = "Reading a minimal NCX file should succeed")]
        public async void ReadEpub2NcxAsyncWithMinimalNcxFileTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NCX_FILE, MinimalEpub2Ncx);
        }

        [Fact(DisplayName = "Reading a full NCX file should succeed")]
        public async void ReadEpub2NcxAsyncWithFullNcxFileTest()
        {
            await TestSuccessfulReadOperation(FULL_NCX_FILE, FullEpub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should return null if EpubPackage is missing spine TOC")]
        public async void ReadEpub2NcxAsyncWithoutTocTest()
        {
            TestZipFile testZipFile = new();
            EpubPackage epubPackage = new()
            {
                Spine = new EpubSpine()
            };
            Epub2Ncx epub2Ncx = await Epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, epubPackage, new EpubReaderOptions());
            Assert.Null(epub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if EpubPackage is missing the manifest item referenced by the spine TOC")]
        public async void ReadEpub2NcxAsyncWithoutTocManifestItemTest()
        {
            TestZipFile testZipFile = new();
            EpubPackage epubPackage = new()
            {
                Manifest = new EpubManifest(),
                Spine = new EpubSpine()
                {
                    Toc = TOC_ID
                }
            };
            await Assert.ThrowsAsync<Epub2NcxException>(() => Epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, epubPackage, new EpubReaderOptions()));
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if EPUB file is missing the NCX file specified in the EpubPackage")]
        public async void ReadEpub2NcxAsyncWithoutNcxFileTest()
        {
            TestZipFile testZipFile = new();
            await Assert.ThrowsAsync<Epub2NcxException>(() =>
                Epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx, new EpubReaderOptions()));
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file is larger than 2 GB")]
        public async void ReadEpub2NcxAsyncWithLargeNcxFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NCX_FILE_PATH_IN_EPUB_ARCHIVE, new Test4GbZipFileEntry());
            await Assert.ThrowsAsync<Epub2NcxException>(() =>
                Epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx, new EpubReaderOptions()));
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'ncx' XML element")]
        public async void ReadEpub2NcxAsyncWithoutNcxElementTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NCX_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'head' XML element")]
        public async void ReadEpub2NcxAsyncWithoutHeadElementTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_HEAD_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'docTitle' XML element")]
        public async void ReadEpub2NcxAsyncWithoutDocTitleElementTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_DOCTITLE_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if the NCX file has no 'navMap' XML element")]
        public async void ReadEpub2NcxAsyncWithoutNavMapElementTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVMAP_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'meta' XML element has no 'name' attribute")]
        public async void ReadEpub2NcxAsyncWithoutMetaNameTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_META_NAME_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'meta' XML element has no 'content' attribute")]
        public async void ReadEpub2NcxAsyncWithoutMetaContentTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_META_CONTENT_ATTRIBUTE);
        }

        [Fact(DisplayName = "Reading an NCX file with unknown XML element in the 'docTitle' element should succeed")]
        public async void ReadEpub2NcxAsyncWithUnknownElementInDocTitleTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NCX_FILE_WITH_UNKNOWN_ELEMENT_IN_DOCTITLE, MinimalEpub2Ncx);
        }

        [Fact(DisplayName = "Reading an NCX file with unknown XML element in the 'docAuthor' element should succeed")]
        public async void ReadEpub2NcxAsyncWithUnknownElementInDocAuthorTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NCX_FILE_WITH_UNKNOWN_ELEMENT_IN_DOCAUTHOR, MinimalEpub2Ncx);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navpoint' XML element has no 'id' attribute")]
        public async void ReadEpub2NcxAsyncWithoutNavPointIdTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVPOINT_ID_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navpoint' XML element has no 'navlabel' elements")]
        public async void ReadEpub2NcxAsyncWithoutNavPointNavLabelsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVPOINT_NAVLABEL_ELEMENTS);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navpoint' XML element has no 'content' element")]
        public async void ReadEpub2NcxAsyncWithoutNavPointContentTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVPOINT_CONTENT_ELEMENT);
        }

        [Fact(DisplayName = "Reading an NCX file without 'content' element in a 'navpoint' XML element with IgnoreMissingContentForNavigationPoints = true should succeed")]
        public async void ReadEpub2NcxAsyncWithoutNavPointContentWithIgnoreMissingContentForNavigationPointsTest()
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
        public async void ReadEpub2NcxAsyncWithoutNavLabelTextTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVLABEL_TEXT_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'content' XML element has no 'src' attribute")]
        public async void ReadEpub2NcxAsyncWithoutContentSrcTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_CONTENT_SRC_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'pageTarget' XML element has no 'type' attribute")]
        public async void ReadEpub2NcxAsyncWithoutPageTargetTypeTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_PAGETARGET_TYPE_ATTRIBUTE);
        }

        [Fact(DisplayName = "Reading an NCX file with unknown value of the 'type' attribute of a 'pageTarget' XML element should succeed")]
        public async void ReadEpub2NcxAsyncWithUnknownPageTargetTypeTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NCX_FILE_WITH_UNKNOWN_PAGETARGET_TYPE, MinimalEpub2NcxWithUnknownPageTargetType);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'pageTarget' XML element has no 'navlabel' elements")]
        public async void ReadEpub2NcxAsyncWithoutPageTargetNavLabelsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_PAGETARGET_NAVLABEL_ELEMENTS);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navList' XML element has no 'navlabel' elements")]
        public async void ReadEpub2NcxAsyncWithoutNavListNavLabelsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVLIST_NAVLABEL_ELEMENTS);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navTarget' XML element has no 'id' attribute")]
        public async void ReadEpub2NcxAsyncWithoutNavTargetIdTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVTARGET_ID_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadEpub2NcxAsync should throw Epub2NcxException if a 'navTarget' XML element has no 'navlabel' elements")]
        public async void ReadEpub2NcxAsyncWithoutNavTargetNavLabelsTest()
        {
            await TestFailingReadOperation(NCX_FILE_WITHOUT_NAVTARGET_NAVLABEL_ELEMENTS);
        }

        private async Task TestSuccessfulReadOperation(string ncxFileContent, Epub2Ncx expectedEpub2Ncx, EpubReaderOptions epubReaderOptions = null)
        {
            TestZipFile testZipFile = CreateTestZipFileWithNcxFile(ncxFileContent);
            Epub2Ncx actualEpub2Ncx =
                await Epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx, epubReaderOptions ?? new EpubReaderOptions());
            Epub2NcxComparer.CompareEpub2Ncxes(expectedEpub2Ncx, actualEpub2Ncx);
        }

        private async Task TestFailingReadOperation(string ncxFileContent)
        {
            TestZipFile testZipFile = CreateTestZipFileWithNcxFile(ncxFileContent);
            await Assert.ThrowsAsync<Epub2NcxException>(() =>
                Epub2NcxReader.ReadEpub2NcxAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNcx, new EpubReaderOptions()));
        }

        private TestZipFile CreateTestZipFileWithNcxFile(string ncxFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NCX_FILE_PATH_IN_EPUB_ARCHIVE, new TestZipFileEntry(ncxFileContent));
            return testZipFile;
        }
    }
}
