using VersOne.Epub.Internal;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class SchemaReaderTests
    {
        private const string EPUB_CONTAINER_FILE_PATH = "META-INF/container.xml";
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string OPF_PACKAGE_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/content.opf";
        private const string NCX_FILE_NAME = "toc.ncx";
        private const string NCX_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{NCX_FILE_NAME}";
        private const string NAV_FILE_NAME = "toc.html";
        private const string NAV_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{NAV_FILE_NAME}";

        private const string META_INF_CONTAINER_FILE = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
                <rootfile media-type="application/oebps-package+xml" full-path="{OPF_PACKAGE_FILE_PATH}"/>
              </rootfiles>
            </container>
            """;

        private const string OPF_PACKAGE_FILE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" xmlns:opf="http://www.idpf.org/2007/opf" xmlns:dc="http://purl.org/dc/elements/1.1/" version="3.0">
              <metadata>
                <dc:title>Test title</dc:title>
                <dc:creator>John Doe</dc:creator>
              </metadata>
              <manifest>
                <item id="item-1" href="chapter1.html" media-type="application/xhtml+xml" />
                <item id="item-2" href="chapter2.html" media-type="application/xhtml+xml" />
                <item id="item-toc" href="{NAV_FILE_NAME}" media-type="application/xhtml+xml" properties="nav" />
                <item id="ncx" href="{NCX_FILE_NAME}" media-type="application/x-dtbncx+xml" />
              </manifest>
              <spine toc="ncx">
                <itemref id="itemref-1" idref="item-1" />
                <itemref id="itemref-2" idref="item-2" />
              </spine>
            </package>
            """;

        private const string NCX_FILE = """
            <?xml version='1.0' encoding='UTF-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/" version="2005-1">
              <head>
                <meta name="dtb:uid" content="9781234567890" />
              </head>
              <docTitle>
                <text>Test title</text>
              </docTitle>
              <docAuthor>
                <text>John Doe</text>
              </docAuthor>
              <navMap>
                <navPoint id="navpoint-1">
                  <navLabel>
                    <text>Chapter 1</text>
                  </navLabel>
                  <content src="chapter1.html" />
                </navPoint>
                <navPoint id="navpoint-2">
                  <navLabel>
                    <text>Chapter 2</text>
                  </navLabel>
                  <content src="chapter2.html" />
                </navPoint>
              </navMap>
            </ncx>
            """;

        private const string NAV_FILE = """
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
                  <ol>
                    <li>
                        <a href="chapter1.html">Chapter 1</a>
                    </li>
                    <li>
                        <a href="chapter2.html">Chapter 2</a>
                    </li>
                  </ol>
                </nav>
              </body>
            </html>
            """;

        [Fact(DisplayName = "Reading a typical schema from a EPUB file should succeed")]
        public async void ReadSchemaAsyncTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH, META_INF_CONTAINER_FILE);
            testZipFile.AddEntry(OPF_PACKAGE_FILE_PATH, OPF_PACKAGE_FILE);
            testZipFile.AddEntry(NCX_FILE_PATH, NCX_FILE);
            testZipFile.AddEntry(NAV_FILE_PATH, NAV_FILE);
            EpubSchema expectedEpubSchema = new
            (
                contentDirectoryPath: CONTENT_DIRECTORY_PATH,
                package: new EpubPackage
                (
                    epubVersion: EpubVersion.EPUB_3,
                    metadata: new EpubMetadata
                    (
                        titles: new List<string>()
                        {
                            "Test title"
                        },
                        creators: new List<EpubMetadataCreator>()
                        {
                            new EpubMetadataCreator
                            (
                                creator: "John Doe"
                            )
                        }
                    ),
                    manifest: new EpubManifest
                    (
                        items: new List<EpubManifestItem>()
                        {
                            new EpubManifestItem
                            (
                                id: "item-1",
                                href: "chapter1.html",
                                mediaType: "application/xhtml+xml"
                            ),
                            new EpubManifestItem
                            (
                                id: "item-2",
                                href: "chapter2.html",
                                mediaType: "application/xhtml+xml"
                            ),
                            new EpubManifestItem
                            (
                                id: "item-toc",
                                href: NAV_FILE_NAME,
                                mediaType: "application/xhtml+xml",
                                properties: new List<EpubManifestProperty>()
                                {
                                    EpubManifestProperty.NAV
                                }
                            ),
                            new EpubManifestItem
                            (
                                id: "ncx",
                                href: NCX_FILE_NAME,
                                mediaType: "application/x-dtbncx+xml"
                            )
                        }
                    ),
                    spine: new EpubSpine
                    (
                        toc: "ncx",
                        items: new List<EpubSpineItemRef>()
                        {
                            new EpubSpineItemRef
                            (
                                id: "itemref-1",
                                idRef: "item-1",
                                isLinear: true
                            ),
                            new EpubSpineItemRef
                            (
                                id: "itemref-2",
                                idRef: "item-2",
                                isLinear: true
                            )
                        }
                    ),
                    guide: null
                ),
                epub2Ncx: new Epub2Ncx
                (
                    filePath: NCX_FILE_PATH,
                    head: new Epub2NcxHead
                    (
                        items: new List<Epub2NcxHeadMeta>()
                        {
                            new Epub2NcxHeadMeta
                            (
                                name: "dtb:uid",
                                content: "9781234567890"
                            )
                        }
                    ),
                    docTitle: "Test title",
                    docAuthors: new List<string>()
                    {
                        "John Doe"
                    },
                    navMap: new Epub2NcxNavigationMap
                    (
                        items: new List<Epub2NcxNavigationPoint>()
                        {
                            new Epub2NcxNavigationPoint
                            (
                                id: "navpoint-1",
                                navigationLabels: new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel
                                    (
                                        text: "Chapter 1"
                                    )
                                },
                                content: new Epub2NcxContent
                                (
                                    source: "chapter1.html"
                                )
                            ),
                            new Epub2NcxNavigationPoint
                            (
                                id: "navpoint-2",
                                navigationLabels: new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel
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
                    )
                ),
                epub3NavDocument: new Epub3NavDocument
                (
                    filePath: NAV_FILE_PATH,
                    navs: new List<Epub3Nav>()
                    {
                        new Epub3Nav
                        (
                            type: Epub3NavStructuralSemanticsProperty.TOC,
                            ol: new Epub3NavOl
                            (
                                lis: new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            href: "chapter1.html",
                                            text: "Chapter 1"
                                        )
                                    ),
                                    new Epub3NavLi
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            href: "chapter2.html",
                                            text: "Chapter 2"
                                        )
                                    )
                                }
                            )
                        )
                    }
                )
            );
            SchemaReader schemaReader = new();
            EpubSchema actualEpubSchema = await schemaReader.ReadSchemaAsync(testZipFile);
            EpubSchemaComparer.CompareEpubSchemas(expectedEpubSchema, actualEpubSchema);
        }
    }
}
