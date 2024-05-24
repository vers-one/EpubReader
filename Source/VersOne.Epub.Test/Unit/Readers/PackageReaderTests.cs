using Newtonsoft.Json;
using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;
using Xunit.Abstractions;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class PackageReaderTests
    {
        public class ReadPackageTestData : IXunitSerializable
        {
            public required string Name { get; init; }
            public required string OpfFileContent { get; init; }
            public required EpubPackage ExpectedEpubPackage { get; init; }

            public void Deserialize(IXunitSerializationInfo info)
            {
                JsonConvert.PopulateObject(info.GetValue<string>("Value"), this);
            }

            public void Serialize(IXunitSerializationInfo info)
            {
                info.AddValue("Value", JsonConvert.SerializeObject(this));
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private const string CONTAINER_FILE_PATH = "META-INF/container.xml";
        private const string OPF_FILE_PATH = "content.opf";

        private const string CONTAINER_FILE = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
                <rootfile media-type="application/oebps-package+xml" full-path="{OPF_FILE_PATH}" />
              </rootfiles>
            </container>
            """;

        private const string MINIMAL_EPUB2_OPF_FILE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="2.0">
              <metadata />
              <manifest />
              <spine toc="ncx" />
            </package>
            """;

        private const string MINIMAL_EPUB3_OPF_FILE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest />
              <spine />
            </package>
            """;

        private const string MINIMAL_EPUB3_1_OPF_FILE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.1">
              <metadata />
              <manifest />
              <spine />
            </package>
            """;

        private const string FULL_OPF_FILE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" xmlns:opf="http://www.idpf.org/2007/opf" xmlns:dc="http://purl.org/dc/elements/1.1/" version="3.0"
                     unique-identifier="book-uid" id="package-id" dir="ltr" prefix="foaf: http://xmlns.com/foaf/spec/" xml:lang="en">
              <metadata>
                <dc:title>Test title</dc:title>
                <dc:creator>John Doe</dc:creator>
                <dc:identifier id="book-uid">9781234567890</dc:identifier>
                <dc:language>en</dc:language>
                <meta property="dcterms:modified">2021-12-31T00:00:00Z</meta>
              </metadata>
              <manifest id="manifest-id">
                <item id="item-front" href="front.html" media-type="application/xhtml+xml" />
                <item id="cover" href="cover.html" media-type="application/xhtml+xml" />
                <item id="cover-image" href="cover.jpg" media-type="image/jpeg" properties="cover-image" />
                <item id="item-css" href="styles.css" media-type="text/css" />
                <item id="item-font" href="font.ttf" media-type="application/x-font-truetype" />
                <item id="item-1" href="chapter1.html" media-type="application/xhtml+xml" media-overlay="item-1-smil" />
                <item id="item-1-smil" href="chapter1.smil" media-type="application/smil+xml" />
                <item id="item-2" href="chapter2.html" media-type="application/xhtml+xml" />
                <item id="item-2-fall" href="chapter2.xml" media-type="text/example+xml"
                      required-namespace="http://example.com/ns/example/" required-modules="ruby, server-side-image-map"
                      fallback="item-2" fallback-style="item-css" />
                <item id="item-3" href="chapter3.html" media-type="application/xhtml+xml" />
                <item id="item-3-fall" href="chapter3.xml" media-type="application/z3998-auth+xml" fallback="item-3" />
                <item id="item-3-remote-audio" href="http://example.com/audio/123/chapter3.mp4" media-type="audio/mp4" />
                <item id="item-image" href="image.jpg" media-type="image/jpeg" />
                <item id="item-title-audio" href="title.mp3" media-type="audio/mpeg" />
                <item id="item-atom" href="book.atom" media-type="application/atom+xml" />
                <item id="item-toc" href="toc.html" media-type="application/xhtml+xml" properties="nav" />
                <item id="ncx" href="toc.ncx" media-type="application/x-dtbncx+xml" />
              </manifest>
              <spine id="spine" page-progression-direction="ltr" toc="ncx">
                <itemref id="itemref-1" idref="item-front" />
                <itemref id="itemref-2" idref="item-toc" linear="no" />
                <itemref id="itemref-3" idref="item-1" linear="yes" />
                <itemref id="itemref-4" idref="item-2" linear="yes" properties="page-spread-left" />
                <itemref id="itemref-5" idref="item-3" linear="yes" properties="page-spread-right" />
              </spine>
              <guide>
                <reference type="toc" title="Contents" href="toc.html" />
              </guide>
              <collection role="http://example.org/roles/group" id="collection-1" dir="ltr" xml:lang="en">
                <metadata>
                  <dc:title id="collection-1-title" dir="ltr" xml:lang="en">Test title for collection 1</dc:title>
                </metadata>
                <collection role="http://example.org/roles/unit" id="collection-2" dir="rtl" xml:lang="is">
                  <metadata>
                    <dc:title id="collection-2-title" dir="auto" xml:lang="is">Test title for collection 2</dc:title>
                  </metadata>
                </collection>
                <link id="collection-1-link" rel="record onix-record" href="https://example.com/onix/123" media-type="application/xml" properties="onix" />
              </collection>
            </package>
            """;

        private const string OPF_FILE_WITH_NON_SUPPORTED_EPUB_VERSION = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="1.0">
              <metadata />
              <manifest />
              <spine />
            </package>
            """;

        private const string OPF_FILE_WITHOUT_PACKAGE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <not-a-package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest />
              <spine />
            </not-a-package>
            """;

        private const string OPF_FILE_WITHOUT_METADATA = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <manifest />
              <spine />
            </package>
            """;

        private const string OPF_FILE_WITHOUT_MANIFEST = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <spine />
            </package>
            """;

        private const string OPF_FILE_WITHOUT_SPINE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest />
            </package>
            """;

        private const string OPF_FILE_WITHOUT_VERSION_IN_PACKAGE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf">
              <metadata />
              <manifest />
              <spine />
            </package>
            """;

        private const string OPF_FILE_WITHOUT_ID_IN_MANIFEST_ITEM = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest>
                <item href="chapter1.html" media-type="application/xhtml+xml" />
              </manifest>
              <spine />
            </package>
            """;

        private const string OPF_FILE_WITH_ESCAPED_HREF_IN_MANIFEST_ITEM = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest>
                <item id="item-1" href="chapter%31.html" media-type="application/xhtml+xml" />
              </manifest>
              <spine />
            </package>
            """;

        private const string OPF_FILE_WITHOUT_HREF_IN_MANIFEST_ITEM = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest>
                <item id="item-1" media-type="application/xhtml+xml" />
              </manifest>
              <spine />
            </package>
            """;

        private const string OPF_FILE_WITHOUT_MEDIA_TYPE_IN_MANIFEST_ITEM = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest>
                <item id="item-1" href="chapter1.html" />
              </manifest>
              <spine />
            </package>
            """;

        private const string OPF_FILE_WITH_DUPLICATING_MANIFEST_ITEM_IDS = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest>
                <item id="unique" href="chapter1.html" media-type="application/xhtml+xml" />
                <item id="duplicate" href="chapter2.html" media-type="application/xhtml+xml" />
                <item id="duplicate" href="chapter3.html" media-type="application/xhtml+xml" />
              </manifest>
              <spine />
            </package>
            """;

        private const string OPF_FILE_WITH_DUPLICATING_MANIFEST_ITEM_HREFS = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest>
                <item id="item-1" href="unique.html" media-type="application/xhtml+xml" />
                <item id="item-2" href="duplicate.html" media-type="application/xhtml+xml" />
                <item id="item-3" href="duplicate.html" media-type="application/xhtml+xml" />
              </manifest>
              <spine />
            </package>
            """;

        private const string EPUB2_OPF_FILE_WITHOUT_SPINE_TOC = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="2.0">
              <metadata />
              <manifest />
              <spine />
            </package>
            """;

        private const string EPUB2_OPF_FILE_WITH_EMPTY_SPINE_TOC = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="2.0">
              <metadata />
              <manifest />
              <spine toc=" " />
            </package>
            """;

        private const string OPF_FILE_WITHOUT_IDREF_IN_SPINE_ITEMREF = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest />
              <spine>
                <itemref />
              </spine>
            </package>
            """;

        private const string OPF_FILE_WITHOUT_TYPE_IN_GUIDE_REFERENCE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="2.0">
              <metadata />
              <manifest />
              <spine toc="ncx" />
              <guide>
                <reference href="toc.html" />
              </guide>
            </package>
            """;

        private const string OPF_FILE_WITHOUT_HREF_IN_GUIDE_REFERENCE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="2.0">
              <metadata />
              <manifest />
              <spine toc="ncx" />
              <guide>
                <reference type="toc" />
              </guide>
            </package>
            """;

        private const string OPF_FILE_WITHOUT_ROLE_IN_COLLECTION = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest />
              <spine />
              <collection />
            </package>
            """;

        private static EpubPackage MinimalEpub2Package =>
            new
            (
                uniqueIdentifier: null,
                epubVersion: EpubVersion.EPUB_2,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest(),
                spine: new EpubSpine
                (
                    toc: "ncx"
                ),
                guide: null
            );

        private static EpubPackage MinimalEpub3Package =>
            new
            (
                uniqueIdentifier: null,
                epubVersion: EpubVersion.EPUB_3,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest(),
                spine: new EpubSpine(),
                guide: null
            );

        private static EpubPackage MinimalEpub31Package =>
            new
            (
                uniqueIdentifier: null,
                epubVersion: EpubVersion.EPUB_3_1,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest(),
                spine: new EpubSpine(),
                guide: null
            );

        private static EpubPackage FullPackage =>
            new
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
                    },
                    languages: new List<EpubMetadataLanguage>()
                    {
                        new
                        (
                            language: "en"
                        )
                    },
                    metaItems: new List<EpubMetadataMeta>()
                    {
                        new
                        (
                            name: null,
                            content: "2021-12-31T00:00:00Z",
                            property: "dcterms:modified"
                        )
                    }
                ),
                manifest: new EpubManifest
                (
                    id: "manifest-id",
                    items: new List<EpubManifestItem>()
                    {
                        new
                        (
                            id: "item-front",
                            href: "front.html",
                            mediaType: "application/xhtml+xml"
                        ),
                        new
                        (
                            id: "cover",
                            href: "cover.html",
                            mediaType: "application/xhtml+xml"
                        ),
                        new
                        (
                            id: "cover-image",
                            href: "cover.jpg",
                            mediaType: "image/jpeg",
                            properties: new List<EpubManifestProperty>()
                            {
                                EpubManifestProperty.COVER_IMAGE
                            }
                        ),
                        new
                        (
                            id: "item-css",
                            href: "styles.css",
                            mediaType: "text/css"
                        ),
                        new
                        (
                            id: "item-font",
                            href: "font.ttf",
                            mediaType: "application/x-font-truetype"
                        ),
                        new
                        (
                            id: "item-1",
                            href: "chapter1.html",
                            mediaType: "application/xhtml+xml",
                            mediaOverlay: "item-1-smil",
                            requiredNamespace: null,
                            requiredModules: null,
                            fallback: null,
                            fallbackStyle: null,
                            properties: null
                        ),
                        new
                        (
                            id: "item-1-smil",
                            href: "chapter1.smil",
                            mediaType: "application/smil+xml"
                        ),
                        new
                        (
                            id: "item-2",
                            href: "chapter2.html",
                            mediaType: "application/xhtml+xml"
                        ),
                        new
                        (
                            id: "item-2-fall",
                            href: "chapter2.xml",
                            mediaType: "text/example+xml",
                            mediaOverlay: null,
                            requiredNamespace: "http://example.com/ns/example/",
                            requiredModules: "ruby, server-side-image-map",
                            fallback: "item-2",
                            fallbackStyle: "item-css",
                            properties: null
                        ),
                        new
                        (
                            id: "item-3",
                            href: "chapter3.html",
                            mediaType: "application/xhtml+xml"
                        ),
                        new
                        (
                            id: "item-3-fall",
                            href: "chapter3.xml",
                            mediaType: "application/z3998-auth+xml",
                            mediaOverlay: null,
                            requiredNamespace: null,
                            requiredModules: null,
                            fallback: "item-3",
                            fallbackStyle: null,
                            properties: null
                        ),
                        new
                        (
                            id: "item-3-remote-audio",
                            href: "http://example.com/audio/123/chapter3.mp4",
                            mediaType: "audio/mp4"
                        ),
                        new
                        (
                            id: "item-image",
                            href: "image.jpg",
                            mediaType: "image/jpeg"
                        ),
                        new
                        (
                            id: "item-title-audio",
                            href: "title.mp3",
                            mediaType: "audio/mpeg"
                        ),
                        new
                        (
                            id: "item-atom",
                            href: "book.atom",
                            mediaType: "application/atom+xml"
                        ),
                        new
                        (
                            id: "item-toc",
                            href: "toc.html",
                            mediaType: "application/xhtml+xml",
                            properties: new List<EpubManifestProperty>
                            {
                                EpubManifestProperty.NAV
                            }
                        ),
                        new
                        (
                            id: "ncx",
                            href: "toc.ncx",
                            mediaType: "application/x-dtbncx+xml"
                        )
                    }
                ),
                spine: new EpubSpine
                (
                    id: "spine",
                    pageProgressionDirection: EpubPageProgressionDirection.LEFT_TO_RIGHT,
                    toc: "ncx",
                    items: new List<EpubSpineItemRef>()
                    {
                        new
                        (
                            id: "itemref-1",
                            idRef: "item-front",
                            isLinear: true
                        ),
                        new
                        (
                            id: "itemref-2",
                            idRef: "item-toc",
                            isLinear: false
                        ),
                        new
                        (
                            id: "itemref-3",
                            idRef: "item-1",
                            isLinear: true
                        ),
                        new
                        (
                            id: "itemref-4",
                            idRef: "item-2",
                            isLinear: true,
                            properties: new List<EpubSpineProperty>()
                            {
                                EpubSpineProperty.PAGE_SPREAD_LEFT
                            }
                        ),
                        new
                        (
                            id: "itemref-5",
                            idRef: "item-3",
                            isLinear: true,
                            properties: new List<EpubSpineProperty>()
                            {
                                EpubSpineProperty.PAGE_SPREAD_RIGHT
                            }
                        )
                    }
                ),
                guide: new EpubGuide
                (
                    items: new List<EpubGuideReference>()
                    {
                        new
                        (
                            type: "toc",
                            title: "Contents",
                            href: "toc.html"
                        )
                    }
                ),
                collections: new List<EpubCollection>()
                {
                    new
                    (
                        role: "http://example.org/roles/group",
                        metadata: new EpubMetadata
                        (
                            titles: new List<EpubMetadataTitle>()
                            {
                                new
                                (
                                    title: "Test title for collection 1",
                                    id: "collection-1-title",
                                    textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                                    language: "en"
                                )
                            }
                        ),
                        nestedCollections: new List<EpubCollection>()
                        {
                            new
                            (
                                role: "http://example.org/roles/unit",
                                metadata: new EpubMetadata
                                (
                                    titles: new List<EpubMetadataTitle>()
                                    {
                                        new
                                        (
                                            title: "Test title for collection 2",
                                            id: "collection-2-title",
                                            textDirection: EpubTextDirection.AUTO,
                                            language: "is"
                                        )
                                    }
                                ),
                                nestedCollections: new List<EpubCollection>(),
                                links: new List<EpubMetadataLink>(),
                                id: "collection-2",
                                textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                                language: "is"
                            )
                        },
                        links: new List<EpubMetadataLink>()
                        {
                            new
                            (
                                href: "https://example.com/onix/123",
                                id: "collection-1-link",
                                mediaType: "application/xml",
                                properties: new List<EpubMetadataLinkProperty>()
                                {
                                    EpubMetadataLinkProperty.ONIX
                                },
                                relationships: new List<EpubMetadataLinkRelationship>()
                                {
                                    EpubMetadataLinkRelationship.RECORD,
                                    EpubMetadataLinkRelationship.ONIX_RECORD
                                },
                                hrefLanguage: null
                            )
                        },
                        id: "collection-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    )
                },
                id: "package-id",
                textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                prefix: "foaf: http://xmlns.com/foaf/spec/",
                language: "en"
            );

        private static EpubPackage Epub2PackageWithoutSpineToc =>
            new
            (
                uniqueIdentifier: null,
                epubVersion: EpubVersion.EPUB_2,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest(),
                spine: new EpubSpine(),
                guide: null
            );

        public static TheoryData<ReadPackageTestData> ReadMinimalPackageAsyncTestData
        {
            get
            {
                return new()
                {
                    new ReadPackageTestData { Name = "Minimal EPUB 2 package", OpfFileContent = MINIMAL_EPUB2_OPF_FILE, ExpectedEpubPackage = MinimalEpub2Package },
                    new ReadPackageTestData { Name = "Minimal EPUB 3 package", OpfFileContent = MINIMAL_EPUB3_OPF_FILE, ExpectedEpubPackage = MinimalEpub3Package },
                    new ReadPackageTestData { Name = "Minimal EPUB 3.1 package", OpfFileContent = MINIMAL_EPUB3_1_OPF_FILE, ExpectedEpubPackage = MinimalEpub31Package }
                };
            }
        }

        public static TheoryData<ReadPackageTestData> ReadFullPackageAsyncTestData
        {
            get
            {
                return new()
                {
                    new ReadPackageTestData { Name = "Full EPUB 3 package", OpfFileContent = FULL_OPF_FILE, ExpectedEpubPackage = FullPackage },
                };
            }
        }

        [Fact(DisplayName = "Constructing a PackageReader instance with a non-null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNonNullEpubReaderOptionsTest()
        {
            _ = new PackageReader(new EpubReaderOptions());
        }

        [Fact(DisplayName = "Constructing a PackageReader instance with a null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNullEpubReaderOptionsTest()
        {
            _ = new PackageReader(null);
        }

        [Theory(DisplayName = "Reading a minimal OPF package should succeed")]
        [MemberData(nameof(ReadMinimalPackageAsyncTestData))]
        public async Task ReadMinimalPackageAsyncTest(ReadPackageTestData testData)
        {
            await TestSuccessfulReadOperation(testData.OpfFileContent, testData.ExpectedEpubPackage);
        }

        [Theory(DisplayName = "Reading a full OPF package should succeed")]
        [MemberData(nameof(ReadFullPackageAsyncTestData))]
        public async Task ReadFullPackageAsyncTest(ReadPackageTestData testData)
        {
            await TestSuccessfulReadOperation(testData.OpfFileContent, testData.ExpectedEpubPackage);
        }
         
        [Fact(DisplayName = "Trying to read OPF package from the EPUB file with no OPF package should fail with EpubContainerException")]
        public async Task ReadPackageWithNoOpfFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(CONTAINER_FILE_PATH, CONTAINER_FILE);
            PackageReader packageReader = new();
            await Assert.ThrowsAsync<EpubContainerException>(() => packageReader.ReadPackageAsync(testZipFile, OPF_FILE_PATH));
        }

        [Fact(DisplayName = "Trying to read OPF package with non-supported EPUB version should fail with EpubPackageException")]
        public async Task ReadPackageWithNonSupportedEpubVersionTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITH_NON_SUPPORTED_EPUB_VERSION);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'package' XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutPackageNodeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_PACKAGE);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'metadata' XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutMetadataNodeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_METADATA);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'manifest' XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutManifestNodeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_MANIFEST);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'spine' XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutSpineNodeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_SPINE);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'version' attribute in a package XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutVersionTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_VERSION_IN_PACKAGE);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'id' attribute in a manifest item XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutManifestItemIdTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_ID_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'id' attribute in a manifest item XML node and null PackageReaderOptions should fail with EpubPackageException")]
        public async Task ReadPackageWithoutManifestItemIdWithNullPackageReaderOptionsTest()
        {
            await TestFailingReadOperationWithNullPackageReaderOptions(OPF_FILE_WITHOUT_ID_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'id' attribute in a manifest item XML node with SkipInvalidManifestItems = true should succeed")]
        public async Task ReadPackageWithoutManifestItemIdWithSkippingInvalidManifestItemsTest()
        {
            await TestSuccessfulReadOperationWithSkippingInvalidManifestItems(OPF_FILE_WITHOUT_ID_IN_MANIFEST_ITEM, MinimalEpub3Package);
        }

        [Fact(DisplayName = "Read an OPF package with a URI-escaped 'href' attribute in a manifest item XML node should succeed")]
        public async Task ReadPackageWithEscapedManifestItemHrefTest()
        {
            EpubPackage expectedPackage = new
            (
                uniqueIdentifier: null,
                epubVersion: EpubVersion.EPUB_3,
                metadata: new EpubMetadata(),
                manifest: new EpubManifest
                (
                    id: null,
                    items: new List<EpubManifestItem>()
                    {
                        new
                        (
                            id: "item-1",
                            href: "chapter1.html",
                            mediaType: "application/xhtml+xml"
                        )
                    }
                ),
                spine: new EpubSpine(),
                guide: null
            );
            await TestSuccessfulReadOperationWithSkippingInvalidManifestItems(OPF_FILE_WITH_ESCAPED_HREF_IN_MANIFEST_ITEM, expectedPackage);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'href' attribute in a manifest item XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutManifestItemHrefTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_HREF_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'href' attribute in a manifest item XML node and null PackageReaderOptions should fail with EpubPackageException")]
        public async Task ReadPackageWithoutManifestItemHrefWithNullPackageReaderOptionsTest()
        {
            await TestFailingReadOperationWithNullPackageReaderOptions(OPF_FILE_WITHOUT_HREF_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'href' attribute in a manifest item XML node with SkipInvalidManifestItems = true should succeed")]
        public async Task ReadPackageWithoutManifestItemHrefWithSkippingInvalidManifestItemsTest()
        {
            await TestSuccessfulReadOperationWithSkippingInvalidManifestItems(OPF_FILE_WITHOUT_HREF_IN_MANIFEST_ITEM, MinimalEpub3Package);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'media-type' attribute in a manifest item XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutManifestItemMediaTypeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_MEDIA_TYPE_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'media-type' attribute in a manifest item XML node and null PackageReaderOptions should fail with EpubPackageException")]
        public async Task ReadPackageWithoutManifestItemMediaTypeWithNullPackageReaderOptionsTest()
        {
            await TestFailingReadOperationWithNullPackageReaderOptions(OPF_FILE_WITHOUT_MEDIA_TYPE_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'media-type' attribute in a manifest item XML node with SkipInvalidManifestItems = true should succeed")]
        public async Task ReadPackageWithoutManifestItemMediaTypeWithSkippingInvalidManifestItemsTest()
        {
            await TestSuccessfulReadOperationWithSkippingInvalidManifestItems(OPF_FILE_WITHOUT_MEDIA_TYPE_IN_MANIFEST_ITEM, MinimalEpub3Package);
        }

        [Fact(DisplayName = "Trying to read OPF package with duplicating 'id' attributes in manifest item XML nodes should fail with EpubPackageException")]
        public async Task ReadPackageWithDuplicatingManifestItemIdsTest()
        {
            await TestFailingReadOperationWithNullPackageReaderOptions(OPF_FILE_WITH_DUPLICATING_MANIFEST_ITEM_IDS);
        }

        [Fact(DisplayName = "Trying to read OPF package with duplicating 'href' attributes in manifest item XML nodes should fail with EpubPackageException")]
        public async Task ReadPackageWithDuplicatingManifestItemHrefsTest()
        {
            await TestFailingReadOperationWithNullPackageReaderOptions(OPF_FILE_WITH_DUPLICATING_MANIFEST_ITEM_HREFS);
        }

        [Fact(DisplayName = "Trying to read EPUB 2 OPF package without 'toc' attribute in the spine XML node should fail with EpubPackageException")]
        public async Task ReadEpub2PackageWithoutSpineTocTest()
        {
            await TestFailingReadOperation(EPUB2_OPF_FILE_WITHOUT_SPINE_TOC);
        }

        [Fact(DisplayName = "Trying to read EPUB 2 OPF package without 'toc' attribute in the spine XML node and null PackageReaderOptions should fail with EpubPackageException")]
        public async Task ReadEpub2PackageWithoutSpineTocWithNullPackageReaderOptionsTest()
        {
            await TestFailingReadOperationWithNullPackageReaderOptions(EPUB2_OPF_FILE_WITHOUT_SPINE_TOC);
        }

        [Fact(DisplayName = "Trying to read EPUB 2 OPF package with empty 'toc' attribute in the spine XML node should fail with EpubPackageException")]
        public async Task ReadEpub2PackageWithEmptySpineTocTest()
        {
            await TestFailingReadOperation(EPUB2_OPF_FILE_WITH_EMPTY_SPINE_TOC);
        }

        [Fact(DisplayName = "Trying to read EPUB 2 OPF package with empty 'toc' attribute in the spine XML node and null PackageReaderOptions should fail with EpubPackageException")]
        public async Task ReadEpub2PackageWithEmptySpineTocWithNullPackageReaderOptionsTest()
        {
            await TestFailingReadOperationWithNullPackageReaderOptions(EPUB2_OPF_FILE_WITH_EMPTY_SPINE_TOC);
        }

        [Fact(DisplayName = "Trying to read EPUB 2 OPF package without 'toc' attribute in the spine XML node with IgnoreMissingToc = true should succeed")]
        public async Task ReadEpub2PackageWithoutSpineTocWithIgnoreMissingTocTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = new PackageReaderOptions()
                {
                    IgnoreMissingToc = true
                }
            };
            await TestSuccessfulReadOperation(EPUB2_OPF_FILE_WITHOUT_SPINE_TOC, Epub2PackageWithoutSpineToc, epubReaderOptions);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'idref' attribute in a spine item ref XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutSpineItemRefIdRefTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_IDREF_IN_SPINE_ITEMREF);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'type' attribute in a guide reference XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutGuideReferenceTypeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_TYPE_IN_GUIDE_REFERENCE);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'href' attribute in a guide reference XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutGuideReferenceHrefTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_HREF_IN_GUIDE_REFERENCE);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'role' attribute in a collection XML node should fail with EpubPackageException")]
        public async Task ReadPackageWithoutCollectionRoleTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_ROLE_IN_COLLECTION);
        }

        private static async Task TestSuccessfulReadOperation(string opfFileContent, EpubPackage expectedEpubPackage, EpubReaderOptions? epubReaderOptions = null)
        {
            TestZipFile testZipFile = CreateTestZipFileWithOpfFile(opfFileContent);
            PackageReader packageReader = new(epubReaderOptions ?? new EpubReaderOptions());
            EpubPackage actualEpubPackage = await packageReader.ReadPackageAsync(testZipFile, OPF_FILE_PATH);
            EpubPackageComparer.CompareEpubPackages(expectedEpubPackage, actualEpubPackage);
        }

        private static Task TestSuccessfulReadOperationWithSkippingInvalidManifestItems(string opfFileContent, EpubPackage expectedEpubPackage)
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = new PackageReaderOptions()
                {
                    SkipInvalidManifestItems = true
                }
            };
            return TestSuccessfulReadOperation(opfFileContent, expectedEpubPackage, epubReaderOptions);
        }

        private static async Task TestFailingReadOperation(string opfFileContent, EpubReaderOptions? epubReaderOptions = null)
        {
            TestZipFile testZipFile = CreateTestZipFileWithOpfFile(opfFileContent);
            PackageReader packageReader = new(epubReaderOptions ?? new EpubReaderOptions());
            await Assert.ThrowsAsync<EpubPackageException>(() => packageReader.ReadPackageAsync(testZipFile, OPF_FILE_PATH));
        }

        private static Task TestFailingReadOperationWithNullPackageReaderOptions(string opfFileContent)
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = null!
            };
            return TestFailingReadOperation(opfFileContent, epubReaderOptions);
        }

        private static TestZipFile CreateTestZipFileWithOpfFile(string opfFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(CONTAINER_FILE_PATH, CONTAINER_FILE);
            testZipFile.AddEntry(OPF_FILE_PATH, opfFileContent);
            return testZipFile;
        }
    }
}
