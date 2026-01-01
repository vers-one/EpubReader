using System.Xml;
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
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
                  <h1>Test header</h1>
                  <ol />
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H2_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
                  <h2>Test header</h2>
                  <ol />
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H3_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
                  <h3>Test header</h3>
                  <ol />
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H4_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
                  <h4>Test header</h4>
                  <ol />
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H5_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
                  <h5>Test header</h5>
                  <ol />
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITH_H6_HEADER = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
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
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc" />
              </body>
            </html>
            """;

        private const string NAV_FILE_WITH_EMPTY_LI_ELEMENT = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
                  <ol>
                    <li />
                  </ol>
                </nav>
              </body>
            </html>
            """;

        private const string NAV_FILE_WITH_ESCAPED_HREF_IN_A_ELEMENT = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
                  <ol>
                    <li>
                      <a href="chapter%31.html">Chapter 1</a>
                    </li>
                  </ol>
                </nav>
              </body>
            </html>
            """;

        private const string NAV_FILE_WITH_WITH_NON_TOP_LEVEL_NAV_ELEMENT = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <div>
                  <nav epub:type="toc">
                    <h1>Test header</h1>
                    <ol />
                  </nav>
                </div>
              </body>
            </html>
            """;

        private const string NAV_FILE_WITHOUT_TYPE_IN_NAV_ELEMENT = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav>
                  <ol>
                    <li>
                      <a href="chapter1.html">Chapter 1</a>
                    </li>
                  </ol>
                </nav>
              </body>
            </html>
            """;

        private const string MINIMAL_NAV_FILE_WITHOUT_TYPE_IN_NAV_ELEMENT = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav />
              </body>
            </html>
            """;

        private static EpubPackage MinimalEpubPackageWithNav =>
            new
            (
                uniqueIdentifier: null,
                epubVersion: EpubVersion.EPUB_3,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest
                (
                    items:
                    [
                        new
                        (
                            id: "nav",
                            href: NAV_FILE_NAME,
                            mediaType: "application/xhtml+xml",
                            properties:
                            [
                                EpubManifestProperty.NAV
                            ]
                        )
                    ]
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
                navs:
                [
                    new
                    (
                        type: Epub3StructuralSemanticsProperty.TOC,
                        isHidden: false,
                        head: "Table of Contents",
                        ol: new Epub3NavOl
                        (
                            isHidden: false,
                            lis:
                            [
                                new
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
                                        lis:
                                        [
                                            new
                                            (
                                                anchor:
                                                new
                                                (
                                                    href: "chapter1.html",
                                                    title: "Test anchor title",
                                                    alt: "Test anchor alt",
                                                    text: "Chapter 1"
                                                )
                                            )
                                        ]
                                    )
                                ),
                                new
                                (
                                    anchor:
                                    new
                                    (
                                        type: Epub3StructuralSemanticsProperty.LOI,
                                        href: "illustrations.html",
                                        text: "List of illustrations"
                                    )
                                )
                            ]
                        )
                    ),
                    new
                    (
                        type: Epub3StructuralSemanticsProperty.PAGE_LIST,
                        isHidden: true,
                        head: "Page list",
                        ol: new Epub3NavOl
                        (
                            isHidden: true,
                            lis:
                            [
                                new
                                (
                                    anchor: new Epub3NavAnchor
                                    (
                                        href: "chapter1.html#page-1",
                                        text: "1"
                                    )
                                )
                            ]
                        )
                    )
                ]
            );

        private static Epub3NavDocument MinimalEpub3NavDocumentWithHeader =>
            new
            (
                filePath: NAV_FILE_PATH,
                navs:
                [
                    new
                    (
                        type: Epub3StructuralSemanticsProperty.TOC,
                        isHidden: false,
                        head: "Test header",
                        ol: new Epub3NavOl()
                    )
                ]
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

        [Fact(DisplayName = "Constructing a Epub3NavDocumentReader instance with a non-null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNonNullEpubReaderOptionsTest()
        {
            _ = new Epub3NavDocumentReader(new EpubReaderOptions());
        }

        [Fact(DisplayName = "Constructing a Epub3NavDocumentReader instance with a null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNullEpubReaderOptionsTest()
        {
            _ = new Epub3NavDocumentReader(null);
        }

        [Fact(DisplayName = "Constructing a Epub3NavDocumentReader instance with a null Epub3NavDocumentReaderOptions property inside the epubReaderOptions parameter should succeed")]
        public void ConstructorWithNullEpub3NavDocumentReaderOptionsTest()
        {
            _ = new Epub3NavDocumentReader(new EpubReaderOptions
            {
                Epub3NavDocumentReaderOptions = null!
            });
        }

        [Fact(DisplayName = "Reading a minimal NAV file should succeed")]
        public async Task ReadEpub3NavDocumentAsyncWithMinimalNavFileTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NAV_FILE, MinimalEpub3NavDocument);
        }

        [Fact(DisplayName = "Reading a full NAV file should succeed")]
        public async Task ReadEpub3NavDocumentAsyncWithFullNavFileTest()
        {
            await TestSuccessfulReadOperation(FULL_NAV_FILE, FullEpub3NavDocument);
        }

        [Theory(DisplayName = "Reading minimal NAV packages with h1-h6 headers should succeed")]
        [MemberData(nameof(ReadEpub3NavDocumentAsyncWithMinimalNavFileWithHeaderTestData))]
        public async Task ReadEpub3NavDocumentAsyncWithMinimalNavFileWithHeaderTest(string navFileContent, Epub3NavDocument expectedEpub3NavDocument)
        {
            await TestSuccessfulReadOperation(navFileContent, expectedEpub3NavDocument);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if EpubPackage is missing the NAV item and EpubVersion is not EPUB_2 and no Epub3NavDocumentReaderOptions are provided")]
        public async Task ReadEpub3NavDocumentAsyncForEpub3WithoutNavManifestItemAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = new();
            EpubPackage epubPackage = new
            (
                uniqueIdentifier: null,
                epubVersion: EpubVersion.EPUB_3,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest
                (
                    items:
                    [
                        new
                        (
                            id: "test",
                            href: "test.html",
                            mediaType: "application/xhtml+xml"
                        )
                    ]
                ),
                spine: new EpubSpine(),
                guide: null
            );
            Epub3NavDocumentReader epub3NavDocumentReader = new();
            await Assert.ThrowsAsync<Epub3NavException>(() => epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, epubPackage));
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should return null if EpubPackage is missing the NAV item and EpubVersion is not EPUB_2 and IgnoreMissingNavManifestItemError = true")]
        public async Task ReadEpub3NavDocumentAsyncForEpub3WithoutNavManifestItemAndIgnoreMissingNavManifestItemErrorTest()
        {
            TestZipFile testZipFile = new();
            EpubPackage epubPackage = new
            (
                uniqueIdentifier: null,
                epubVersion: EpubVersion.EPUB_3,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest
                (
                    items:
                    [
                        new
                        (
                            id: "test",
                            href: "test.html",
                            mediaType: "application/xhtml+xml"
                        )
                    ]
                ),
                spine: new EpubSpine(),
                guide: null
            );
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub3NavDocumentReaderOptions = new()
                {
                    IgnoreMissingNavManifestItemError = true
                }
            };
            Epub3NavDocumentReader epub3NavDocumentReader = new(epubReaderOptions);
            Epub3NavDocument? actualEpub3NavDocument =
                await epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, epubPackage);
            Assert.Null(actualEpub3NavDocument);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should return null if EpubPackage is missing the NAV item and EpubVersion is EPUB_2")]
        public async Task ReadEpub3NavDocumentAsyncForEpub2WithoutNavManifestItemTest()
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
            Epub3NavDocumentReader epub3NavDocumentReader = new();
            Epub3NavDocument? actualEpub3NavDocument =
                await epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, epubPackage);
            Assert.Null(actualEpub3NavDocument);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if EPUB file is missing the NAV file specified in the EpubPackage and no Epub3NavDocumentReaderOptions are provided")]
        public async Task ReadEpub3NavDocumentAsyncWithoutNavFileAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = new();
            Epub3NavDocumentReader epub3NavDocumentReader = new();
            await Assert.ThrowsAsync<Epub3NavException>(() => epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav));
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should return null if EPUB file is missing the NAV file specified in the EpubPackage and IgnoreMissingNavFileError = true")]
        public async Task ReadEpub3NavDocumentAsyncWithoutNavFileAndIgnoreMissingNavFileErrorTest()
        {
            TestZipFile testZipFile = new();
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub3NavDocumentReaderOptions = new()
                {
                    IgnoreMissingNavFileError = true
                }
            };
            Epub3NavDocumentReader epub3NavDocumentReader = new(epubReaderOptions);
            Epub3NavDocument? actualEpub3NavDocument =
                await epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav);
            Assert.Null(actualEpub3NavDocument);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if the NAV file is larger than 2 GB and no Epub3NavDocumentReaderOptions are provided")]
        public async Task ReadEpub3NavDocumentAsyncWithLargeNavFileAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NAV_FILE_PATH, new Test4GbZipFileEntry());
            Epub3NavDocumentReader epub3NavDocumentReader = new();
            await Assert.ThrowsAsync<Epub3NavException>(() => epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav));
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should return null if the NAV file is larger than 2 GB and IgnoreNavFileIsTooLargeError = true")]
        public async Task ReadEpub3NavDocumentAsyncWithLargeNavFileAndIgnoreNavFileIsTooLargeErrorTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(NAV_FILE_PATH, new Test4GbZipFileEntry());
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub3NavDocumentReaderOptions = new()
                {
                    IgnoreNavFileIsTooLargeError = true
                }
            };
            Epub3NavDocumentReader epub3NavDocumentReader = new(epubReaderOptions);
            Epub3NavDocument? actualEpub3NavDocument =
                await epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav);
            Assert.Null(actualEpub3NavDocument);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException with an inner XmlException if the NAV file is not a valid XHTML file and no Epub3NavDocumentReaderOptions are provided")]
        public async Task ReadEpub3NavDocumentAsyncWithInvalidXhtmlFileAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = CreateTestZipFileWithNavFile("not a valid XHTML file");
            Epub3NavDocumentReader epub3NavDocumentReader = new();
            Epub3NavException outerException =
                await Assert.ThrowsAsync<Epub3NavException>(() =>
                    epub3NavDocumentReader.ReadEpub3NavDocumentAsync(testZipFile, CONTENT_DIRECTORY_PATH, MinimalEpubPackageWithNav));
            Assert.NotNull(outerException.InnerException);
            Assert.Equal(typeof(XmlException), outerException.InnerException.GetType());
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should return null if the NAV file is not a valid XHTML file and IgnoreNavFileIsNotValidXmlError = true")]
        public async Task ReadEpub3NavDocumentAsyncWithInvalidXhtmlFileAndIgnoreNavFileIsNotValidXmlErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub3NavDocumentReaderOptions = new()
                {
                    IgnoreNavFileIsNotValidXmlError = true
                }
            };
            await TestSuccessfulReadOperation("not a valid XHTML file", null, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if the NAV file is missing the 'html' XML element and no Epub3NavDocumentReaderOptions are provided")]
        public async Task ReadEpub3NavDocumentAsyncWithoutHtmlElementAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NAV_FILE_WITHOUT_HTML_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should return null if the NAV file is missing the 'html' XML element and IgnoreMissingHtmlElementError = true")]
        public async Task ReadEpub3NavDocumentAsyncWithoutHtmlElementAndIgnoreMissingHtmlElementErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub3NavDocumentReaderOptions = new()
                {
                    IgnoreMissingHtmlElementError = true
                }
            };
            await TestSuccessfulReadOperation(NAV_FILE_WITHOUT_HTML_ELEMENT, null, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if the NAV file is missing the 'body' XML element and no Epub3NavDocumentReaderOptions are provided")]
        public async Task ReadEpub3NavDocumentAsyncWithoutBodyElementAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NAV_FILE_WITHOUT_BODY_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should return null if the NAV file is missing the 'body' XML element and IgnoreMissingBodyElementError = true")]
        public async Task ReadEpub3NavDocumentAsyncWithoutBodyElementAndIgnoreMissingBodyElementErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub3NavDocumentReaderOptions = new()
                {
                    IgnoreMissingBodyElementError = true
                }
            };
            await TestSuccessfulReadOperation(NAV_FILE_WITHOUT_BODY_ELEMENT, null, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if the NAV file is missing the top 'ol' XML element and no Epub3NavDocumentReaderOptions are provided")]
        public async Task ReadEpub3NavDocumentAsyncWithoutTopOlElementAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NAV_FILE_WITHOUT_TOP_OL_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should succeed if the NAV file is missing the top 'ol' XML element and SkipNavsWithMissingOlElements = true")]
        public async Task ReadEpub3NavDocumentAsyncWithoutTopOlElementAndSkipNavsWithMissingOlElementsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub3NavDocumentReaderOptions = new()
                {
                    SkipNavsWithMissingOlElements = true
                }
            };
            await TestSuccessfulReadOperation(NAV_FILE_WITHOUT_TOP_OL_ELEMENT, MinimalEpub3NavDocument, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should throw Epub3NavException if the NAV file has an empty 'li' XML element and no Epub3NavDocumentReaderOptions are provided")]
        public async Task ReadEpub3NavDocumentAsyncWithEmptyLiElementAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(NAV_FILE_WITH_EMPTY_LI_ELEMENT);
        }

        [Fact(DisplayName = "ReadEpub3NavDocumentAsync should succeed if the NAV file has an empty 'li' XML element and SkipInvalidLiElements = true")]
        public async Task ReadEpub3NavDocumentAsyncWithEmptyLiElementAndSkipInvalidLiElementsTest()
        {
            Epub3NavDocument expectedEpub3NavDocument = MinimalEpub3NavDocument;
            expectedEpub3NavDocument.Navs.Add(
                new
                (
                    type: Epub3StructuralSemanticsProperty.TOC,
                    ol: new()
                )
            );
            EpubReaderOptions epubReaderOptions = new()
            {
                Epub3NavDocumentReaderOptions = new()
                {
                    SkipInvalidLiElements = true
                }
            };
            await TestSuccessfulReadOperation(NAV_FILE_WITH_EMPTY_LI_ELEMENT, expectedEpub3NavDocument, epubReaderOptions);
        }

        [Fact(DisplayName = "Reading a NAV file with a URI-escaped 'href' attribute in an 'a' XML element should succeed")]
        public async Task ReadEpub3NavDocumentAsyncWithEscapedAHrefTest()
        {
            Epub3NavDocument expectedEpub3NavDocument = new
            (
                filePath: NAV_FILE_PATH,
                navs:
                [
                    new
                    (
                        type: Epub3StructuralSemanticsProperty.TOC,
                        ol: new Epub3NavOl
                        (
                            lis:
                            [
                                new
                                (
                                    anchor:
                                    new
                                    (
                                        href: "chapter1.html",
                                        text: "Chapter 1"
                                    )
                                )
                            ]
                        )
                    )
                ]
            );
            await TestSuccessfulReadOperation(NAV_FILE_WITH_ESCAPED_HREF_IN_A_ELEMENT, expectedEpub3NavDocument);
        }

        [Fact(DisplayName = "Reading a NAV file with non-top-level 'nav' element should succeed")]
        public async Task ReadEpub3NavDocumentAsyncWithNonTopLevelNavElementTest()
        {
            await TestSuccessfulReadOperation(NAV_FILE_WITH_WITH_NON_TOP_LEVEL_NAV_ELEMENT, MinimalEpub3NavDocumentWithHeader);
        }

        [Fact(DisplayName = "'nav' elements without 'type' attribute should be ignored")]
        public async Task ReadEpub3NavDocumentAsyncWithNavElementWithoutTypeTest()
        {
            await TestSuccessfulReadOperation(NAV_FILE_WITHOUT_TYPE_IN_NAV_ELEMENT, MinimalEpub3NavDocument);
        }

        [Fact(DisplayName = "'nav' elements without 'type' attribute that don't conform to EPUB standard should not throw an exception")]
        public async Task ReadEpub3NavDocumentAsyncWithNonEpubNavElementWithoutTypeTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_NAV_FILE_WITHOUT_TYPE_IN_NAV_ELEMENT, MinimalEpub3NavDocument);
        }

        private static async Task TestSuccessfulReadOperation(string navFileContent, Epub3NavDocument? expectedEpub3NavDocument, EpubReaderOptions? epubReaderOptions = null)
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
