using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Comparers;
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
            EpubSchema expectedEpubSchema = new()
            {
                ContentDirectoryPath = CONTENT_DIRECTORY_PATH,
                Package = new EpubPackage()
                {
                    EpubVersion = EpubVersion.EPUB_3,
                    Metadata = new EpubMetadata()
                    {
                        Titles = new List<string>()
                        {
                            "Test title"
                        },
                        Creators = new List<EpubMetadataCreator>()
                        {
                            new EpubMetadataCreator()
                            {
                                Creator = "John Doe"
                            }
                        },
                        Subjects = new List<string>(),
                        Publishers = new List<string>(),
                        Contributors = new List<EpubMetadataContributor>(),
                        Dates = new List<EpubMetadataDate>(),
                        Types = new List<string>(),
                        Formats = new List<string>(),
                        Identifiers = new List<EpubMetadataIdentifier>(),
                        Sources = new List<string>(),
                        Languages = new List<string>(),
                        Relations = new List<string>(),
                        Coverages = new List<string>(),
                        Rights = new List<string>(),
                        Links = new List<EpubMetadataLink>(),
                        MetaItems = new List<EpubMetadataMeta>()
                    },
                    Manifest = new EpubManifest()
                    {
                        Items = new List<EpubManifestItem>()
                        {
                            new EpubManifestItem()
                            {
                                Id = "item-1",
                                Href = "chapter1.html",
                                MediaType = "application/xhtml+xml"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-2",
                                Href = "chapter2.html",
                                MediaType = "application/xhtml+xml"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-toc",
                                Href = NAV_FILE_NAME,
                                MediaType = "application/xhtml+xml",
                                Properties = new List<EpubManifestProperty>()
                                {
                                    EpubManifestProperty.NAV
                                }
                            },
                            new EpubManifestItem()
                            {
                                Id = "ncx",
                                Href = NCX_FILE_NAME,
                                MediaType = "application/x-dtbncx+xml"
                            }
                        }
                    },
                    Spine = new EpubSpine()
                    {
                        Toc = "ncx",
                        Items = new List<EpubSpineItemRef>()
                        {
                            new EpubSpineItemRef()
                            {
                                Id = "itemref-1",
                                IdRef = "item-1",
                                IsLinear = true
                            },
                            new EpubSpineItemRef()
                            {
                                Id = "itemref-2",
                                IdRef = "item-2",
                                IsLinear = true
                            }
                        }
                    }
                },
                Epub2Ncx = new Epub2Ncx()
                {
                    Head = new Epub2NcxHead()
                    {
                        Items = new List<Epub2NcxHeadMeta>()
                        {
                            new Epub2NcxHeadMeta()
                            {
                                Name = "dtb:uid",
                                Content = "9781234567890"
                            }
                        }
                    },
                    DocTitle = "Test title",
                    DocAuthors = new List<string>()
                    {
                        "John Doe"
                    },
                    NavMap = new Epub2NcxNavigationMap()
                    {
                        Items = new List<Epub2NcxNavigationPoint>()
                        {
                            new Epub2NcxNavigationPoint()
                            {
                                Id = "navpoint-1",
                                NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Chapter 1"
                                    }
                                },
                                Content = new Epub2NcxContent()
                                {
                                    Source = "chapter1.html"
                                },
                                ChildNavigationPoints = new List<Epub2NcxNavigationPoint>()
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
                        }
                    },
                    NavLists = new List<Epub2NcxNavigationList>()
                },
                Epub3NavDocument = new Epub3NavDocument()
                {
                    Navs = new List<Epub3Nav>()
                    {
                        new Epub3Nav()
                        {
                            Type = Epub3NavStructuralSemanticsProperty.TOC,
                            Ol = new Epub3NavOl()
                            {
                                Lis = new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi()
                                    {
                                        Anchor = new Epub3NavAnchor()
                                        {
                                            Href = "chapter1.html",
                                            Text = "Chapter 1"
                                        }
                                    },
                                    new Epub3NavLi()
                                    {
                                        Anchor = new Epub3NavAnchor()
                                        {
                                            Href = "chapter2.html",
                                            Text = "Chapter 2"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            EpubSchema actualEpubSchema = await SchemaReader.ReadSchemaAsync(testZipFile, new EpubReaderOptions());
            EpubSchemaComparer.CompareEpubSchemas(expectedEpubSchema, actualEpubSchema);
        }
    }
}
