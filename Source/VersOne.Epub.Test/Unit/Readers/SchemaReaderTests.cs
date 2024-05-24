using VersOne.Epub.Internal;
using VersOne.Epub.Options;
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
        private const string SMIL_FILE_NAME = "chapter1.smil";
        private const string SMIL_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{SMIL_FILE_NAME}";

        private const string META_INF_CONTAINER_FILE = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
                <rootfile media-type="application/oebps-package+xml" full-path="{OPF_PACKAGE_FILE_PATH}" />
              </rootfiles>
            </container>
            """;

        private const string OPF_PACKAGE_FILE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" xmlns:opf="http://www.idpf.org/2007/opf" xmlns:dc="http://purl.org/dc/elements/1.1/" version="3.0" unique-identifier="book-uid">
              <metadata>
                <dc:title>Test title</dc:title>
                <dc:creator>John Doe</dc:creator>
                <dc:identifier id="book-uid">9781234567890</dc:identifier>
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

        private const string SMIL_FILE = """
            <smil xmlns="http://www.w3.org/ns/SMIL" version="3.0">
                <body>
                    <par id="sentence1">
                        <text src="chapter1.html#sentence1"/>
                        <audio src="chapter1_audio.mp3" clipBegin="0s" clipEnd="10s"/>
                    </par>
                    <par id="sentence2">
                        <text src="chapter1.html#sentence2"/>
                        <audio src="chapter1_audio.mp3" clipBegin="10s" clipEnd="20s"/>
                    </par>
                    <par id="sentence3">
                        <text src="chapter1.html#sentence3"/>
                        <audio src="chapter1_audio.mp3" clipBegin="20s" clipEnd="30s"/>
                    </par>
                </body>
            </smil>
            """;

        [Fact(DisplayName = "Constructing a SchemaReader instance with a non-null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNonNullEpubReaderOptionsTest()
        {
            _ = new SchemaReader(new EpubReaderOptions());
        }

        [Fact(DisplayName = "Constructing a SchemaReader instance with a null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNullEpubReaderOptionsTest()
        {
            _ = new SchemaReader(null);
        }

        [Fact(DisplayName = "Reading a typical schema from a EPUB file should succeed")]
        public async Task ReadSchemaAsyncTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(EPUB_CONTAINER_FILE_PATH, META_INF_CONTAINER_FILE);
            testZipFile.AddEntry(OPF_PACKAGE_FILE_PATH, OPF_PACKAGE_FILE);
            testZipFile.AddEntry(NCX_FILE_PATH, NCX_FILE);
            testZipFile.AddEntry(NAV_FILE_PATH, NAV_FILE);
            testZipFile.AddEntry(SMIL_FILE_PATH, SMIL_FILE);
            EpubSchema expectedEpubSchema = new
            (
                package: new EpubPackage
                (
                    uniqueIdentifier: "book-uid",
                    epubVersion: EpubVersion.EPUB_3,
                    metadata: new EpubMetadata
                    (
                        titles: new List<EpubMetadataTitle>()
                        {
                            new
                            (
                                title: "Test title"
                            )
                        },
                        creators: new List<EpubMetadataCreator>()
                        {
                            new
                            (
                                creator: "John Doe"
                            )
                        },
                        identifiers: new List<EpubMetadataIdentifier>()
                        {
                            new
                            (
                                identifier: "9781234567890",
                                id: "book-uid"
                            )
                        }
                    ),
                    manifest: new EpubManifest
                    (
                        items: new List<EpubManifestItem>()
                        {
                            new
                            (
                                id: "item-1",
                                href: "chapter1.html",
                                mediaType: "application/xhtml+xml"
                            ),
                            new
                            (
                                id: "item-2",
                                href: "chapter2.html",
                                mediaType: "application/xhtml+xml"
                            ),
                            new
                            (
                                id: "item-toc",
                                href: NAV_FILE_NAME,
                                mediaType: "application/xhtml+xml",
                                properties: new List<EpubManifestProperty>()
                                {
                                    EpubManifestProperty.NAV
                                }
                            ),
                            new
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
                            new
                            (
                                id: "itemref-1",
                                idRef: "item-1",
                                isLinear: true
                            ),
                            new
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
                            new
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
                            new
                            (
                                id: "navpoint-1",
                                navigationLabels: new List<Epub2NcxNavigationLabel>()
                                {
                                    new
                                    (
                                        text: "Chapter 1"
                                    )
                                },
                                content: new Epub2NcxContent
                                (
                                    source: "chapter1.html"
                                )
                            ),
                            new
                            (
                                id: "navpoint-2",
                                navigationLabels: new List<Epub2NcxNavigationLabel>()
                                {
                                    new
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
                        new
                        (
                            type: Epub3StructuralSemanticsProperty.TOC,
                            ol: new Epub3NavOl
                            (
                                lis: new List<Epub3NavLi>()
                                {
                                    new
                                    (
                                        anchor: new Epub3NavAnchor
                                        (
                                            href: "chapter1.html",
                                            text: "Chapter 1"
                                        )
                                    ),
                                    new
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
                ),
                mediaOverlays: new List<Smil>()
                {
                    new
                    (
                        id: null,
                        version: SmilVersion.SMIL_3,
                        epubPrefix: null,
                        head: null,
                        body: new SmilBody
                        (
                            id: null,
                            epubTypes: null,
                            epubTextRef: null,
                            seqs: new List<SmilSeq>(),
                            pars: new List<SmilPar>()
                            {
                                new
                                (
                                    id: "sentence1",
                                    epubTypes: null,
                                    text: new SmilText
                                    (
                                        id: null,
                                        src: "chapter1.html#sentence1"
                                    ),
                                    audio: new SmilAudio
                                    (
                                        id: null,
                                        src: "chapter1_audio.mp3",
                                        clipBegin: "0s",
                                        clipEnd: "10s"
                                    )
                                ),
                                new
                                (
                                    id: "sentence2",
                                    epubTypes: null,
                                    text: new SmilText
                                    (
                                        id: null,
                                        src: "chapter1.html#sentence2"
                                    ),
                                    audio: new SmilAudio
                                    (
                                        id: null,
                                        src: "chapter1_audio.mp3",
                                        clipBegin: "10s",
                                        clipEnd: "20s"
                                    )
                                ),
                                new
                                (
                                    id: "sentence3",
                                    epubTypes: null,
                                    text: new SmilText
                                    (
                                        id: null,
                                        src: "chapter1.html#sentence3"
                                    ),
                                    audio: new SmilAudio
                                    (
                                        id: null,
                                        src: "chapter1_audio.mp3",
                                        clipBegin: "20s",
                                        clipEnd: "30s"
                                    )
                                )
                            }
                        )
                    )
                },
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
            SchemaReader schemaReader = new();
            EpubSchema actualEpubSchema = await schemaReader.ReadSchemaAsync(testZipFile);
            EpubSchemaComparer.CompareEpubSchemas(expectedEpubSchema, actualEpubSchema);
        }
    }
}
