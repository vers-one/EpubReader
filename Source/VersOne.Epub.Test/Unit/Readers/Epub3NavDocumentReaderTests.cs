using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class Epub3NavDocumentReaderTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string NAV_FILE_NAME = "toc.html";
        private const string NAV_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{NAV_FILE_NAME}";

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
                  <ol />
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H2_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <h2>Test header</h2>
                  <ol />
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H3_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <h3>Test header</h3>
                  <ol />
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H4_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <h4>Test header</h4>
                  <ol />
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H5_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <h5>Test header</h5>
                  <ol />
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H6_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <h6>Test header</h6>
                  <ol />
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

        private const string NAV_FILE_WITHOUT_TOP_OL_ELEMENT = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav />
              </body>
            </html>
            """;

        private const string NAV_FILE_WITH_EMPTY_LI_ELEMENT = """
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body>
                <nav>
                  <ol>
                    <li />
                  </ol>
                </nav>
              </body>
            </html>
            """;

        private static EpubPackage MinimalEpubPackageWithNav =>
            new
            (
                epubVersion: EpubVersion.EPUB_3,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest
                (
                    items: new List<EpubManifestItem>()
                    {
                        new EpubManifestItem
                        (
                            id: "nav",
                            href: NAV_FILE_NAME,
                            mediaType: "application/xhtml+xml",
                            properties: new List<EpubManifestProperty>()
                            {
                                EpubManifestProperty.NAV
                            }
                        )
                    }
                ),
                spine: new EpubSpine(),
                guide: null
            );

        private static Epub3NavDocument MinimalEpub3NavDocument =>
            new
            (
                filePath: NAV_FILE_PATH
            );

        private static Epub3NavDocument FullEpub3NavDocument =>
            new
            (
                filePath: NAV_FILE_PATH,
                navs: new List<Epub3Nav>()
                {
                    new Epub3Nav
                    (
                        type: Epub3NavStructuralSemanticsProperty.TOC,
                        isHidden: false,
                        head: "Table of Contents",
                        ol: new Epub3NavOl
                        (
                            isHidden: false,
                            lis: new List<Epub3NavLi>()
                            {
                                new Epub3NavLi
                                (
                                    span: new Epub3NavSpan
                                    (
                                        title: "Test span title",
                                        alt: "Test span alt",
                                        text: "Test span header"
                                    ),
                                    childOl: new Epub3NavOl
                                    (
                                        isHidden: false,
                                        lis: new List<Epub3NavLi>()
                                        {
                                            new Epub3NavLi
                                            (
                                                anchor: new Epub3NavAnchor
                                                (
                                                    href: "chapter1.html",
                                                    title: "Test anchor title",
                                                    alt: "Test anchor alt",
                                                    text: "Chapter 1"
                                                )
                                            )
                                        }
                                    )
                                ),
                                new Epub3NavLi
                                (
                                    anchor: new Epub3NavAnchor
                                    (
                                        type: Epub3NavStructuralSemanticsProperty.LOI,
                                        href: "illustrations.html",
                                        text: "List of illustrations"
                                    )
                                )
                            }
                        )
                    ),
                    new Epub3Nav
                    (
                        type: Epub3NavStructuralSemanticsProperty.PAGE_LIST,
                        isHidden: true,
                        head: "Page list",
                        ol: new Epub3NavOl
                        (
                            isHidden: true,
                            lis: new List<Epub3NavLi>()
                            {
                                new Epub3NavLi
                                (
                                    anchor: new Epub3NavAnchor
                                    (
                                        href: "chapter1.html#page-1",
                                        text: "1"
                                    )
                                )
                            }
                        )
                    )
                }
            );

        private static Epub3NavDocument MinimalEpub3NavDocumentWithHeader =>
            new
            (
                filePath: NAV_FILE_PATH,
                navs: new List<Epub3Nav>()
                {
                    new Epub3Nav
                    (
                        type: null,
                        isHidden: false,
                        head: "Test header",
                        ol: new Epub3NavOl()
                    )
                }
            );

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
            EpubPackage epubPackage = new
            (
                epubVersion: EpubVersion.EPUB_3,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest
                (
                    items: new List<EpubManifestItem>()
                    {
                        new EpubManifestItem
                        (
                            id: "test",
                            href: "test.html",
                            mediaType: "application/xhtml+xml"
                        )
                    }
                ),
                spine: new EpubSpine(),
                guide: null
            );
            Epub3NavDocumentReader epub3NavDocumentReader = new();
            await Assert.ThrowsAsync<Epub3NavException>(() => epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, epubPackage));
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should return null if EpubPackage is missing the NAV item and EpubVersion is EPUB_2")]
        public async void ReadEpub3NavDocumentAsyncForEpub2WithoutNavManifestItemTest()
        {
            TestZipFile testZipFile = new();
            EpubPackage epubPackage = new
            (
                epubVersion: EpubVersion.EPUB_2,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest(),
                spine: new EpubSpine(),
                guide: null
            );
            Epub3NavDocumentReader epub3NavDocumentReader = new();
            Epub3NavDocument? epub3NavDocument = await epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, epubPackage);
            Assert.Null(epub3NavDocument);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if EPUB file is missing the NAV file specified in the EpubPackage")]
        public async void ReadEpub3NavDocumentAsyncWithoutNavFileTest()
        {
            TestZipFile testZipFile = new();
            Epub3NavDocumentReader epub3NavDocumentReader = new();
            await Assert.ThrowsAsync<Epub3NavException>(() => epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav));
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if the NCX file is larger than 2 GB")]
        public async void ReadEpub3NavDocumentAsyncWithLargeNavFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NAV_FILE_PATH, new Test4GbZipFileEntry());
            Epub3NavDocumentReader epub3NavDocumentReader = new();
            await Assert.ThrowsAsync<Epub3NavException>(() => epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav));
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

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if the NAV file is missing the top 'ol' XML element")]
        public async void ReadEpub3NavDocumentAsyncWithoutTopOlElement()
        {
            await TestFailingReadOperation(NAV_FILE_WITHOUT_TOP_OL_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if the NAV file has an empty 'li' XML element")]
        public async void ReadEpub3NavDocumentAsyncWithEmptyLiElement()
        {
            await TestFailingReadOperation(NAV_FILE_WITH_EMPTY_LI_ELEMENT);
        }

        private static async Task TestSuccessfulReadOperation(string navFileContent, Epub3NavDocument expectedEpub3NavDocument, EpubReaderOptions? epubReaderOptions = null)
        {
            TestZipFile testZipFile = CreateTestZipFileWithNavFile(navFileContent);
            Epub3NavDocumentReader epub3NavDocumentReader = new(epubReaderOptions ?? new EpubReaderOptions());
            Epub3NavDocument? actualEpub3NavDocument = await epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav);
            Epub3NavDocumentComparer.CompareEpub3NavDocuments(expectedEpub3NavDocument, actualEpub3NavDocument);
        }

        private static async Task TestFailingReadOperation(string navFileContent)
        {
            TestZipFile testZipFile = CreateTestZipFileWithNavFile(navFileContent);
            Epub3NavDocumentReader epub3NavDocumentReader = new();
            await Assert.ThrowsAsync<Epub3NavException>(() => epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav));
        }

        private static TestZipFile CreateTestZipFileWithNavFile(string navFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NAV_FILE_PATH, new TestZipFileEntry(navFileContent));
            return testZipFile;
        }
    }
}
