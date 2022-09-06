using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class Epub3NavDocumentReaderTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string NAV_FILE_NAME = "toc.html";
        private const string NAV_FILE_PATH_IN_EPUB_ARCHIVE = $"{CONTENT_DIRECTORY_PATH}/{NAV_FILE_NAME}";

        private const string MINIMAL_NAV_FILE = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body />
            </html>
            """;

        private const string FULL_NAV_FILE = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
                  <h1>Table of Contents</h1>
                  <ol>
                    <li>
                      <span title="Test span title" alt="Test span alt">Test span header</span>
                      <ol>
                        <li>
                          <a href="chapter1.html" title="Test anchor title" alt="Test anchor alt">Chapter 1</a>
                        </li>
                      </ol>
                    </li>
                    <li>
                      <a epub:type="loi" href="illustrations.html">List of illustrations</a>
                    </li>
                  </ol>
                </nav>
                <nav epub:type="page-list" hidden="">
                  <h1>Page list</h1>
                  <ol hidden="">
                    <li>
                      <a href="chapter1.html#page-1">1</a>
                    </li>
                  </ol>
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H1_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <h1>Test header</h1>
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H2_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <h2>Test header</h2>
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H3_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <h3>Test header</h3>
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H4_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <h4>Test header</h4>
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H5_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <h5>Test header</h5>
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H6_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <h6>Test header</h6>
                </nav>
              </body>
            </html>
            """;

        private const string NAV_FILE_WITHOUT_HTML_ELEMENT = """
            <test />
            """;

        private const string NAV_FILE_WITHOUT_BODY_ELEMENT = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <test />
            </html>
            """;

        private EpubPackage MinimalEpubPackageWithNav =>
            new()
            {
                Manifest = new EpubManifest()
                {
                    new EpubManifestItem()
                    {
                        Id = "nav",
                        Href = NAV_FILE_NAME,
                        MediaType = "application/xhtml+xml",
                        Properties = new List<EpubManifestProperty>()
                        {
                            EpubManifestProperty.NAV
                        }
                    }
                }
            };

        private Epub3NavDocument MinimalEpub3NavDocument =>
            new()
            {
                Navs = new List<Epub3Nav>()
            };

        private Epub3NavDocument FullEpub3NavDocument =>
            new()
            {
                Navs = new List<Epub3Nav>()
                {
                    new Epub3Nav()
                    {
                        Type = Epub3NavStructuralSemanticsProperty.TOC,
                        IsHidden = false,
                        Head = "Table of Contents",
                        Ol = new Epub3NavOl()
                        {
                            IsHidden = false,
                            Lis = new List<Epub3NavLi>()
                            {
                                new Epub3NavLi()
                                {
                                    Span = new Epub3NavSpan()
                                    {
                                        Title = "Test span title",
                                        Alt = "Test span alt",
                                        Text = "Test span header"
                                    },
                                    ChildOl = new Epub3NavOl()
                                    {
                                        IsHidden = false,
                                        Lis = new List<Epub3NavLi>()
                                        {
                                            new Epub3NavLi()
                                            {
                                                Anchor = new Epub3NavAnchor()
                                                {
                                                    Href = "chapter1.html",
                                                    Title = "Test anchor title",
                                                    Alt = "Test anchor alt",
                                                    Text = "Chapter 1"
                                                }
                                            }
                                        }
                                    }
                                },
                                new Epub3NavLi()
                                {
                                    Anchor = new Epub3NavAnchor()
                                    {
                                        Type = Epub3NavStructuralSemanticsProperty.LOI,
                                        Href = "illustrations.html",
                                        Text = "List of illustrations"
                                    }
                                }
                            }
                        }
                    },
                    new Epub3Nav()
                    {
                        Type = Epub3NavStructuralSemanticsProperty.PAGE_LIST,
                        IsHidden = true,
                        Head = "Page list",
                        Ol = new Epub3NavOl()
                        {
                            IsHidden = true,
                            Lis = new List<Epub3NavLi>()
                            {
                                new Epub3NavLi()
                                {
                                    Anchor = new Epub3NavAnchor()
                                    {
                                        Href = "chapter1.html#page-1",
                                        Text = "1"
                                    }
                                }
                            }
                        }
                    }
                }
            };

        private static Epub3NavDocument MinimalEpub3NavDocumentWithHeader =>
            new()
            {
                Navs = new List<Epub3Nav>()
                {
                    new Epub3Nav()
                    {
                        Head = "Test header"
                    }
                }
            };

        public static IEnumerable<object[]> ReadEpub3NavDocumentAsyncWithMinimalNavFileWithHeaderTestData
        {
            get
            {
                yield return new object[] { MINIMAL_NAV_FILE_WITH_H1_HEADER, MinimalEpub3NavDocumentWithHeader };
                yield return new object[] { MINIMAL_NAV_FILE_WITH_H2_HEADER, MinimalEpub3NavDocumentWithHeader };
                yield return new object[] { MINIMAL_NAV_FILE_WITH_H3_HEADER, MinimalEpub3NavDocumentWithHeader };
                yield return new object[] { MINIMAL_NAV_FILE_WITH_H4_HEADER, MinimalEpub3NavDocumentWithHeader };
                yield return new object[] { MINIMAL_NAV_FILE_WITH_H5_HEADER, MinimalEpub3NavDocumentWithHeader };
                yield return new object[] { MINIMAL_NAV_FILE_WITH_H6_HEADER, MinimalEpub3NavDocumentWithHeader };
            }
        }


        [Fact(DisplayName = "Reading a minimal NAV file should succeed")]
        public async void ReadEpub3NavDocumentAsyncWithMinimalNavFileTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NAV_FILE, MinimalEpub3NavDocument);
        }

        [Fact(DisplayName = "Reading a full NAV file should succeed")]
        public async void ReadEpub3NavDocumentAsyncWithFullNavFileTest()
        {
            await TestSuccessfulReadOperation(FULL_NAV_FILE, FullEpub3NavDocument);
        }

        [Theory(DisplayName = "Reading minimal NAV packages with h1-h6 headers should succeed")]
        [MemberData(nameof(ReadEpub3NavDocumentAsyncWithMinimalNavFileWithHeaderTestData))]
        public async void ReadEpub3NavDocumentAsyncWithMinimalNavFileWithHeaderTest(string navFileContent, Epub3NavDocument expectedEpub3NavDocument)
        {
            await TestSuccessfulReadOperation(navFileContent, expectedEpub3NavDocument);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if EpubPackage is missing the NAV item and EpubVersion is not EPUB_2")]
        public async void ReadEpub3NavDocumentAsyncForEpub3WithoutNavManifestItemTest()
        {
            TestZipFile testZipFile = new();
            EpubPackage epubPackage = new()
            {
                EpubVersion = EpubVersion.EPUB_3,
                Manifest = new EpubManifest()
                {
                    new EpubManifestItem()
                    {
                        Id = "test",
                        Href = "test.html",
                        MediaType = "application/xhtml+xml"
                    }
                }
            };
            await Assert.ThrowsAsync<Epub3NavException>(() => Epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH,
                epubPackage, new EpubReaderOptions()));
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should return null if EpubPackage is missing the NAV item and EpubVersion is EPUB_2")]
        public async void ReadEpub3NavDocumentAsyncForEpub2WithoutNavManifestItemTest()
        {
            TestZipFile testZipFile = new();
            EpubPackage epubPackage = new()
            {
                EpubVersion = EpubVersion.EPUB_2,
                Manifest = new EpubManifest()
            };
            Epub3NavDocument epub3NavDocument = await Epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, epubPackage, new EpubReaderOptions());
            Assert.Null(epub3NavDocument);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if EPUB file is missing the NAV file specified in the EpubPackage")]
        public async void ReadEpub3NavDocumentAsyncWithoutNavFileTest()
        {
            TestZipFile testZipFile = new();
            await Assert.ThrowsAsync<Epub3NavException>(() =>
                Epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav, new EpubReaderOptions()));
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if the NCX file is larger than 2 GB")]
        public async void ReadEpub3NavDocumentAsyncWithLargeNavFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NAV_FILE_PATH_IN_EPUB_ARCHIVE, new Test4GbZipFileEntry());
            await Assert.ThrowsAsync<Epub3NavException>(() =>
                Epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav, new EpubReaderOptions()));
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if the NAV file is missing the 'html' XML element")]
        public async void ReadEpub3NavDocumentAsyncWithoutHtmlElement()
        {
            await TestFailingReadOperation(NAV_FILE_WITHOUT_HTML_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if the NAV file is missing the 'body' XML element")]
        public async void ReadEpub3NavDocumentAsyncWithoutBodyElement()
        {
            await TestFailingReadOperation(NAV_FILE_WITHOUT_BODY_ELEMENT);
        }

        private async Task TestSuccessfulReadOperation(string navFileContent, Epub3NavDocument expectedEpub3NavDocument, EpubReaderOptions epubReaderOptions = null)
        {
            TestZipFile testZipFile = CreateTestZipFileWithNavFile(navFileContent);
            Epub3NavDocument actualEpub3NavDocument = await Epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH,
                MinimalEpubPackageWithNav, epubReaderOptions ?? new EpubReaderOptions());
            Epub3NavDocumentComparer.CompareEpub3NavDocuments(expectedEpub3NavDocument, actualEpub3NavDocument);
        }

        private async Task TestFailingReadOperation(string navFileContent)
        {
            TestZipFile testZipFile = CreateTestZipFileWithNavFile(navFileContent);
            await Assert.ThrowsAsync<Epub3NavException>(() =>
                Epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav, new EpubReaderOptions()));
        }

        private TestZipFile CreateTestZipFileWithNavFile(string navFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NAV_FILE_PATH_IN_EPUB_ARCHIVE, new TestZipFileEntry(navFileContent));
            return testZipFile;
        }
    }
}
