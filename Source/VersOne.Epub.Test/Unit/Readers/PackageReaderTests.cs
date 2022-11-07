using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class PackageReaderTests
    {
        private const string CONTAINER_FILE_PATH = "META-INF/container.xml";
        private const string OPF_FILE_PATH = "content.opf";

        private const string CONTAINER_FILE = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
                <rootfile media-type="application/oebps-package+xml" full-path="{OPF_FILE_PATH}"/>
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
            <package xmlns="http://www.idpf.org/2007/opf" xmlns:opf="http://www.idpf.org/2007/opf" xmlns:dc="http://purl.org/dc/elements/1.1/" version="3.0">
              <metadata>
                <dc:title>Test title 1</dc:title>
                <dc:title>Test title 2</dc:title>
                <dc:creator id="creator-1" opf:role="author" opf:file-as="Doe, John">John Doe</dc:creator>
                <dc:creator id="creator-2" opf:role="author" opf:file-as="Doe, Jane">Jane Doe</dc:creator>
                <dc:subject>Test subject 1</dc:subject>
                <dc:subject>Test subject 2</dc:subject>
                <dc:description>Test description</dc:description>
                <dc:publisher>Test publisher 1</dc:publisher>
                <dc:publisher>Test publisher 2</dc:publisher>
                <dc:contributor id="contributor-1" opf:role="editor" opf:file-as="Editor, John">John Editor</dc:contributor>
                <dc:contributor id="contributor-2" opf:role="editor" opf:file-as="Editor, Jane">Jane Editor</dc:contributor>
                <dc:date opf:event="creation">2021-12-31T23:59:59.123456Z</dc:date>
                <dc:date opf:event="publication">2022-01-23</dc:date>
                <dc:type>dictionary</dc:type>
                <dc:type>preview</dc:type>
                <dc:format>format-1</dc:format>
                <dc:format>format-2</dc:format>
                <dc:identifier id="identifier-1" opf:scheme="URI">https://example.com/books/123</dc:identifier>
                <dc:identifier id="identifier-2" opf:scheme="ISBN">9781234567890</dc:identifier>
                <dc:source>https://example.com/books/123/content-1.html</dc:source>
                <dc:source>https://example.com/books/123/content-2.html</dc:source>
                <dc:language>en</dc:language>
                <dc:language>is</dc:language>
                <dc:relation>https://example.com/books/123/related-1.html</dc:relation>
                <dc:relation>https://example.com/books/123/related-2.html</dc:relation>
                <dc:coverage>New York</dc:coverage>
                <dc:coverage>1700-1850</dc:coverage>
                <dc:rights>Public domain in the USA</dc:rights>
                <dc:rights>All rights reserved</dc:rights>
                <link id="link-1" rel="record" href="front.html#meta-json" media-type="application/xhtml+xml" />
                <link id="link-2" rel="record onix-record" href="https://example.com/onix/123" media-type="application/xml" properties="onix" />
                <link id="link-3" rel="record" href="book.atom" media-type="application/atom+xml;type=entry;profile=opds-catalog" />
                <link id="link-4" rel="voicing" refines="#title" href="title.mp3" media-type="audio/mpeg" />
                <meta name="cover" content="cover-image" />
                <meta id="meta-1" property="rendition:orientation">landscape</meta>
                <meta id="meta-2" property="identifier-type" refines="#identifier-2" scheme="onix:codelist5">123</meta>
                <meta id="meta-3" property="alternate-script" refines="#creator-1">Brynjólfur Sveinsson</meta>
              </metadata>
              <manifest>
                <item id="item-front" href="front.html" media-type="application/xhtml+xml" />
                <item id="cover" href="cover.html" media-type="application/xhtml+xml"/>
                <item id="cover-image" href="cover.jpg" media-type="image/jpeg" properties="cover-image" />
                <item id="item-css" href="styles.css" media-type="text/css" />
                <item id="item-font" href="font.ttf" media-type="application/x-font-truetype" />
                <item id="item-1" href="chapter1.html" media-type="application/xhtml+xml" media-overlay="item-1-audio" />
                <item id="item-1-audio" href="chapter1-audio.smil" media-type="application/smil+xml" />
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

        private const string OPF_FILE_WITH_EMPTY_ID_IN_MANIFEST_ITEM = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest>
                <item id=" " href="chapter1.html" media-type="application/xhtml+xml" />
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

        private const string OPF_FILE_WITH_EMPTY_HREF_IN_MANIFEST_ITEM = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest>
                <item id="item-1" href=" " media-type="application/xhtml+xml" />
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

        private const string OPF_FILE_WITH_EMPTY_MEDIA_TYPE_IN_MANIFEST_ITEM = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest>
                <item id="item-1" href="chapter1.html" media-type=" " />
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

        private const string OPF_FILE_WITH_EMPTY_IDREF_IN_SPINE_ITEMREF = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest />
              <spine>
                <itemref idref=" " />
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

        private const string OPF_FILE_WITH_EMPTY_TYPE_IN_GUIDE_REFERENCE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="2.0">
              <metadata />
              <manifest />
              <spine toc="ncx" />
              <guide>
                <reference type=" " href="toc.html" />
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

        private const string OPF_FILE_WITH_EMPTY_HREF_IN_GUIDE_REFERENCE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="2.0">
              <metadata />
              <manifest />
              <spine toc="ncx" />
              <guide>
                <reference type="toc" href=" " />
              </guide>
            </package>
            """;

        private static EpubMetadata EmptyMetadata =>
            new()
            {
                Titles = new List<string>(),
                Creators = new List<EpubMetadataCreator>(),
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
            };

        private static EpubPackage MinimalEpub2Package =>
            new()
            {
                EpubVersion = EpubVersion.EPUB_2,
                Metadata = EmptyMetadata,
                Manifest = new EpubManifest()
                {
                    Items = new List<EpubManifestItem>()
                },
                Spine = new EpubSpine()
                {
                    Toc = "ncx",
                    Items = new List<EpubSpineItemRef>()
                }
            };

        private static EpubPackage MinimalEpub3Package =>
            new()
            {
                EpubVersion = EpubVersion.EPUB_3,
                Metadata = EmptyMetadata,
                Manifest = new EpubManifest()
                {
                    Items = new List<EpubManifestItem>()
                },
                Spine = new EpubSpine()
                {
                    Items = new List<EpubSpineItemRef>()
                }
            };

        private static EpubPackage MinimalEpub31Package =>
            new()
            {
                EpubVersion = EpubVersion.EPUB_3_1,
                Metadata = EmptyMetadata,
                Manifest = new EpubManifest()
                {
                    Items = new List<EpubManifestItem>()
                },
                Spine = new EpubSpine()
                {
                    Items = new List<EpubSpineItemRef>()
                }
            };

        private static EpubPackage FullPackage
        {
            get
            {
                return new()
                {
                    EpubVersion = EpubVersion.EPUB_3,
                    Metadata = new EpubMetadata()
                    {
                        Titles = new List<string>()
                        {
                            "Test title 1",
                            "Test title 2"
                        },
                        Creators = new List<EpubMetadataCreator>()
                        {
                            new EpubMetadataCreator()
                            {
                                Id = "creator-1",
                                Role = "author",
                                FileAs = "Doe, John",
                                Creator = "John Doe"
                            },
                            new EpubMetadataCreator()
                            {
                                Id = "creator-2",
                                Role = "author",
                                FileAs = "Doe, Jane",
                                Creator = "Jane Doe"
                            }
                        },
                        Subjects = new List<string>()
                        {
                            "Test subject 1",
                            "Test subject 2"
                        },
                        Description = "Test description",
                        Publishers = new List<string>()
                        {
                            "Test publisher 1",
                            "Test publisher 2"
                        },
                        Contributors = new List<EpubMetadataContributor>()
                        {
                            new EpubMetadataContributor()
                            {
                                Id = "contributor-1",
                                Role = "editor",
                                FileAs = "Editor, John",
                                Contributor = "John Editor"
                            },
                            new EpubMetadataContributor()
                            {
                                Id = "contributor-2",
                                Role = "editor",
                                FileAs = "Editor, Jane",
                                Contributor = "Jane Editor"
                            }
                        },
                        Dates = new List<EpubMetadataDate>()
                        {
                            new EpubMetadataDate()
                            {
                                Event = "creation",
                                Date = "2021-12-31T23:59:59.123456Z"
                            },
                            new EpubMetadataDate()
                            {
                                Event = "publication",
                                Date = "2022-01-23"
                            }
                        },
                        Types = new List<string>()
                        {
                            "dictionary",
                            "preview"
                        },
                        Formats = new List<string>()
                        {
                            "format-1",
                            "format-2"
                        },
                        Identifiers = new List<EpubMetadataIdentifier>()
                        {
                            new EpubMetadataIdentifier()
                            {
                                Id = "identifier-1",
                                Scheme = "URI",
                                Identifier = "https://example.com/books/123"
                            },
                            new EpubMetadataIdentifier()
                            {
                                Id = "identifier-2",
                                Scheme = "ISBN",
                                Identifier = "9781234567890"
                            }
                        },
                        Sources = new List<string>()
                        {
                            "https://example.com/books/123/content-1.html",
                            "https://example.com/books/123/content-2.html"
                        },
                        Languages = new List<string>()
                        {
                            "en",
                            "is"
                        },
                        Relations = new List<string>()
                        {
                            "https://example.com/books/123/related-1.html",
                            "https://example.com/books/123/related-2.html"
                        },
                        Coverages = new List<string>()
                        {
                            "New York",
                            "1700-1850"
                        },
                        Rights = new List<string>()
                        {
                            "Public domain in the USA",
                            "All rights reserved"
                        },
                        Links = new List<EpubMetadataLink>()
                        {
                            new EpubMetadataLink()
                            {
                                Id = "link-1",
                                Relationships = new List<EpubMetadataLinkRelationship>()
                                {
                                    EpubMetadataLinkRelationship.RECORD
                                },
                                Href = "front.html#meta-json",
                                MediaType = "application/xhtml+xml"
                            },
                            new EpubMetadataLink()
                            {
                                Id = "link-2",
                                Relationships = new List<EpubMetadataLinkRelationship>()
                                {
                                    EpubMetadataLinkRelationship.RECORD,
                                    EpubMetadataLinkRelationship.ONIX_RECORD
                                },
                                Href = "https://example.com/onix/123",
                                MediaType = "application/xml",
                                Properties = new List<EpubMetadataLinkProperty>()
                                {
                                    EpubMetadataLinkProperty.ONIX
                                }
                            },
                            new EpubMetadataLink()
                            {
                                Id = "link-3",
                                Relationships = new List<EpubMetadataLinkRelationship>()
                                {
                                    EpubMetadataLinkRelationship.RECORD
                                },
                                Href = "book.atom",
                                MediaType = "application/atom+xml;type=entry;profile=opds-catalog"
                            },
                            new EpubMetadataLink()
                            {
                                Id = "link-4",
                                Relationships = new List<EpubMetadataLinkRelationship>()
                                {
                                    EpubMetadataLinkRelationship.VOICING
                                },
                                Refines = "#title",
                                Href = "title.mp3",
                                MediaType = "audio/mpeg"
                            }
                        },
                        MetaItems = new List<EpubMetadataMeta>()
                        {
                            new EpubMetadataMeta()
                            {
                                Name = "cover",
                                Content = "cover-image"
                            },
                            new EpubMetadataMeta()
                            {
                                Id = "meta-1",
                                Property = "rendition:orientation",
                                Content = "landscape"
                            },
                            new EpubMetadataMeta()
                            {
                                Id = "meta-2",
                                Property = "identifier-type",
                                Refines = "#identifier-2",
                                Scheme = "onix:codelist5",
                                Content = "123"
                            },
                            new EpubMetadataMeta()
                            {
                                Id = "meta-3",
                                Property = "alternate-script",
                                Refines = "#creator-1",
                                Content = "Brynjólfur Sveinsson"
                            }
                        }
                    },
                    Manifest = new EpubManifest()
                    {
                        Items = new List<EpubManifestItem>()
                        {
                            new EpubManifestItem()
                            {
                                Id = "item-front",
                                Href = "front.html",
                                MediaType = "application/xhtml+xml"
                            },
                            new EpubManifestItem()
                            {
                                Id = "cover",
                                Href = "cover.html",
                                MediaType = "application/xhtml+xml"
                            },
                            new EpubManifestItem()
                            {
                                Id = "cover-image",
                                Href = "cover.jpg",
                                MediaType = "image/jpeg",
                                Properties = new List<EpubManifestProperty>()
                                {
                                    EpubManifestProperty.COVER_IMAGE
                                }
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-css",
                                Href = "styles.css",
                                MediaType = "text/css"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-font",
                                Href = "font.ttf",
                                MediaType = "application/x-font-truetype"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-1",
                                Href = "chapter1.html",
                                MediaType = "application/xhtml+xml",
                                MediaOverlay = "item-1-audio"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-1-audio",
                                Href = "chapter1-audio.smil",
                                MediaType = "application/smil+xml"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-2",
                                Href = "chapter2.html",
                                MediaType = "application/xhtml+xml"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-2-fall",
                                Href = "chapter2.xml",
                                MediaType = "text/example+xml",
                                RequiredNamespace = "http://example.com/ns/example/",
                                RequiredModules = "ruby, server-side-image-map",
                                Fallback = "item-2",
                                FallbackStyle = "item-css"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-3",
                                Href = "chapter3.html",
                                MediaType = "application/xhtml+xml"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-3-fall",
                                Href = "chapter3.xml",
                                MediaType = "application/z3998-auth+xml",
                                Fallback = "item-3"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-3-remote-audio",
                                Href = "http://example.com/audio/123/chapter3.mp4",
                                MediaType = "audio/mp4"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-image",
                                Href = "image.jpg",
                                MediaType = "image/jpeg"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-title-audio",
                                Href = "title.mp3",
                                MediaType = "audio/mpeg"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-atom",
                                Href = "book.atom",
                                MediaType = "application/atom+xml"
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-toc",
                                Href = "toc.html",
                                MediaType = "application/xhtml+xml",
                                Properties = new List<EpubManifestProperty>()
                                {
                                    EpubManifestProperty.NAV
                                }
                            },
                            new EpubManifestItem()
                            {
                                Id = "ncx",
                                Href = "toc.ncx",
                                MediaType = "application/x-dtbncx+xml"
                            }
                        }
                    },
                    Spine = new EpubSpine()
                    {
                        Id = "spine",
                        PageProgressionDirection = EpubPageProgressionDirection.LEFT_TO_RIGHT,
                        Toc = "ncx",
                        Items = new List<EpubSpineItemRef>()
                        {
                            new EpubSpineItemRef()
                            {
                                Id = "itemref-1",
                                IdRef = "item-front",
                                IsLinear = true
                            },
                            new EpubSpineItemRef()
                            {
                                Id = "itemref-2",
                                IdRef = "item-toc",
                                IsLinear = false
                            },
                            new EpubSpineItemRef()
                            {
                                Id = "itemref-3",
                                IdRef = "item-1",
                                IsLinear = true
                            },
                            new EpubSpineItemRef()
                            {
                                Id = "itemref-4",
                                IdRef = "item-2",
                                IsLinear = true,
                                Properties = new List<EpubSpineProperty>()
                                {
                                    EpubSpineProperty.PAGE_SPREAD_LEFT
                                }
                            },
                            new EpubSpineItemRef()
                            {
                                Id = "itemref-5",
                                IdRef = "item-3",
                                IsLinear = true,
                                Properties = new List<EpubSpineProperty>()
                                {
                                    EpubSpineProperty.PAGE_SPREAD_RIGHT
                                }
                            }
                        }
                    },
                    Guide = new EpubGuide()
                    {
                        Items = new List<EpubGuideReference>()
                        {
                            new EpubGuideReference()
                            {
                                Type = "toc",
                                Title = "Contents",
                                Href = "toc.html"
                            }
                        }
                    }
                };
            }
        }

        private static EpubPackage Epub2PackageWithoutSpineToc =>
            new()
            {
                EpubVersion = EpubVersion.EPUB_2,
                Metadata = EmptyMetadata,
                Manifest = new EpubManifest()
                {
                    Items = new List<EpubManifestItem>()
                },
                Spine = new EpubSpine()
                {
                    Items = new List<EpubSpineItemRef>()
                }
            };


        public static IEnumerable<object[]> ReadMinimalPackageAsyncTestData
        {
            get
            {
                yield return new object[] { MINIMAL_EPUB2_OPF_FILE, MinimalEpub2Package };
                yield return new object[] { MINIMAL_EPUB3_OPF_FILE, MinimalEpub3Package };
                yield return new object[] { MINIMAL_EPUB3_1_OPF_FILE, MinimalEpub31Package };
            }
        }

        public static IEnumerable<object[]> ReadFullPackageAsyncTestData
        {
            get
            {
                yield return new object[] { FULL_OPF_FILE, FullPackage };
            }
        }

        [Theory(DisplayName = "Reading a minimal OPF package should succeed")]
        [MemberData(nameof(ReadMinimalPackageAsyncTestData))]
        public async void ReadMinimalPackageAsyncTest(string opfFileContent, EpubPackage expectedEpubPackage)
        {
            await TestSuccessfulReadOperation(opfFileContent, expectedEpubPackage);
        }

        [Theory(DisplayName = "Reading a full OPF package should succeed")]
        [MemberData(nameof(ReadFullPackageAsyncTestData))]
        public async void ReadFullPackageAsyncTest(string opfFileContent, EpubPackage expectedEpubPackage)
        {
            await TestSuccessfulReadOperation(opfFileContent, expectedEpubPackage);
        }
         
        [Fact(DisplayName = "Trying to read OPF package from the EPUB file with no OPF package should fail with EpubContainerException")]
        public async void ReadPackageWithNoOpfFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(CONTAINER_FILE_PATH, CONTAINER_FILE);
            PackageReader packageReader = new();
            await Assert.ThrowsAsync<EpubContainerException>(() => packageReader.ReadPackageAsync(testZipFile, OPF_FILE_PATH));
        }

        [Fact(DisplayName = "Trying to read OPF package with non-supported EPUB version should fail with EpubPackageException")]
        public async void ReadPackageWithNonSupportedEpubVersionTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITH_NON_SUPPORTED_EPUB_VERSION);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'package' XML node should fail with EpubPackageException")]
        public async void ReadPackageWithoutPackageNodeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_PACKAGE);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'metadata' XML node should fail with EpubPackageException")]
        public async void ReadPackageWithoutMetadataNodeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_METADATA);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'manifest' XML node should fail with EpubPackageException")]
        public async void ReadPackageWithoutManifestNodeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_MANIFEST);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'spine' XML node should fail with EpubPackageException")]
        public async void ReadPackageWithoutSpineNodeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_SPINE);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'id' attribute in a manifest item XML node should fail with EpubPackageException")]
        public async void ReadPackageWithoutManifestItemIdTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_ID_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package with empty 'id' attribute in a manifest item XML node should fail with EpubPackageException")]
        public async void ReadPackageWithEmptyManifestItemIdTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITH_EMPTY_ID_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'id' attribute in a manifest item XML node and null PackageReaderOptions should fail with EpubPackageException")]
        public async void ReadPackageWithoutManifestItemIdAndWithNullPackageReaderOptionsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = null
            };
            await TestFailingReadOperation(OPF_FILE_WITHOUT_ID_IN_MANIFEST_ITEM, epubReaderOptions);
        }

        [Fact(DisplayName = "Trying to read OPF package with empty 'id' attribute in a manifest item XML node and null PackageReaderOptions should fail with EpubPackageException")]
        public async void ReadPackageWithEmptyManifestItemIdAndWithNullPackageReaderOptionsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = null
            };
            await TestFailingReadOperation(OPF_FILE_WITH_EMPTY_ID_IN_MANIFEST_ITEM, epubReaderOptions);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'id' attribute in a manifest item XML node with SkipInvalidManifestItems = true should succeed")]
        public async void ReadPackageWithoutManifestItemIdWithSkippingInvalidManifestItemsTest()
        {
            await TestSuccessfulReadOperationWithSkippingInvalidManifestItems(OPF_FILE_WITHOUT_ID_IN_MANIFEST_ITEM, MinimalEpub3Package);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'href' attribute in a manifest item XML node should fail with EpubPackageException")]
        public async void ReadPackageWithoutManifestItemHrefTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_HREF_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package with empty 'href' attribute in a manifest item XML node should fail with EpubPackageException")]
        public async void ReadPackageWithEmptyManifestItemHrefTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITH_EMPTY_HREF_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'href' attribute in a manifest item XML node and null PackageReaderOptions should fail with EpubPackageException")]
        public async void ReadPackageWithoutManifestItemHrefAndWithNullPackageReaderOptionsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = null
            };
            await TestFailingReadOperation(OPF_FILE_WITHOUT_HREF_IN_MANIFEST_ITEM, epubReaderOptions);
        }

        [Fact(DisplayName = "Trying to read OPF package with empty 'href' attribute in a manifest item XML node and null PackageReaderOptions should fail with EpubPackageException")]
        public async void ReadPackageWithEmptyManifestItemHrefAndWithNullPackageReaderOptionsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = null
            };
            await TestFailingReadOperation(OPF_FILE_WITH_EMPTY_HREF_IN_MANIFEST_ITEM, epubReaderOptions);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'href' attribute in a manifest item XML node with SkipInvalidManifestItems = true should succeed")]
        public async void ReadPackageWithoutManifestItemHrefWithSkippingInvalidManifestItemsTest()
        {
            await TestSuccessfulReadOperationWithSkippingInvalidManifestItems(OPF_FILE_WITHOUT_HREF_IN_MANIFEST_ITEM, MinimalEpub3Package);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'media-type' attribute in a manifest item XML node should fail with EpubPackageException")]
        public async void ReadPackageWithoutManifestItemMediaTypeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_MEDIA_TYPE_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package with empty 'media-type' attribute in a manifest item XML node should fail with EpubPackageException")]
        public async void ReadPackageWithEmptyManifestItemMediaTypeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITH_EMPTY_MEDIA_TYPE_IN_MANIFEST_ITEM);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'media-type' attribute in a manifest item XML node and null PackageReaderOptions should fail with EpubPackageException")]
        public async void ReadPackageWithoutManifestItemMediaTypeAndWithNullPackageReaderOptionsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = null
            };
            await TestFailingReadOperation(OPF_FILE_WITHOUT_MEDIA_TYPE_IN_MANIFEST_ITEM, epubReaderOptions);
        }

        [Fact(DisplayName = "Trying to read OPF package with empty 'media-type' attribute in a manifest item XML node and null PackageReaderOptions should fail with EpubPackageException")]
        public async void ReadPackageWithEmptyManifestItemMediaTypeAndWithNullPackageReaderOptionsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = null
            };
            await TestFailingReadOperation(OPF_FILE_WITH_EMPTY_MEDIA_TYPE_IN_MANIFEST_ITEM, epubReaderOptions);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'media-type' attribute in a manifest item XML node with SkipInvalidManifestItems = true should succeed")]
        public async void ReadPackageWithoutManifestItemMediaTypeWithSkippingInvalidManifestItemsTest()
        {
            await TestSuccessfulReadOperationWithSkippingInvalidManifestItems(OPF_FILE_WITHOUT_MEDIA_TYPE_IN_MANIFEST_ITEM, MinimalEpub3Package);
        }

        [Fact(DisplayName = "Trying to read EPUB 2 OPF package without 'toc' attribute in the spine XML node should fail with EpubPackageException")]
        public async void ReadEpub2PackageWithoutSpineTocTest()
        {
            await TestFailingReadOperation(EPUB2_OPF_FILE_WITHOUT_SPINE_TOC);
        }

        [Fact(DisplayName = "Trying to read EPUB 2 OPF package with empty 'toc' attribute in the spine XML node should fail with EpubPackageException")]
        public async void ReadEpub2PackageWithEmptySpineTocTest()
        {
            await TestFailingReadOperation(EPUB2_OPF_FILE_WITH_EMPTY_SPINE_TOC);
        }

        [Fact(DisplayName = "Trying to read EPUB 2 OPF package without 'toc' attribute in the spine XML node and null PackageReaderOptionsshould fail with EpubPackageException")]
        public async void ReadEpub2PackageWithoutSpineTocAndWithNullPackageReaderOptionsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = null
            };
            await TestFailingReadOperation(EPUB2_OPF_FILE_WITHOUT_SPINE_TOC, epubReaderOptions);
        }

        [Fact(DisplayName = "Trying to read EPUB 2 OPF package with empty 'toc' attribute in the spine XML node and null PackageReaderOptionsshould fail with EpubPackageException")]
        public async void ReadEpub2PackageWithEmptySpineTocAndWithNullPackageReaderOptionsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                PackageReaderOptions = null
            };
            await TestFailingReadOperation(EPUB2_OPF_FILE_WITH_EMPTY_SPINE_TOC, epubReaderOptions);
        }

        [Fact(DisplayName = "Trying to read EPUB 2 OPF package without 'toc' attribute in the spine XML node with IgnoreMissingToc = true should succeed")]
        public async void ReadEpub2PackageWithoutSpineTocWithIgnoreMissingTocTest()
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
        public async void ReadPackageWithoutSpineItemRefIdRefTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_IDREF_IN_SPINE_ITEMREF);
        }

        [Fact(DisplayName = "Trying to read OPF package with empty 'idref' attribute in a spine item ref XML node should fail with EpubPackageException")]
        public async void ReadPackageWithEmptySpineItemRefIdRefTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITH_EMPTY_IDREF_IN_SPINE_ITEMREF);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'type' attribute in a guide reference XML node should fail with EpubPackageException")]
        public async void ReadPackageWithoutGuideReferenceTypeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_TYPE_IN_GUIDE_REFERENCE);
        }

        [Fact(DisplayName = "Trying to read OPF package with empty 'type' attribute in a guide reference XML node should fail with EpubPackageException")]
        public async void ReadPackageWithEmptyGuideReferenceTypeTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITH_EMPTY_TYPE_IN_GUIDE_REFERENCE);
        }

        [Fact(DisplayName = "Trying to read OPF package without 'href' attribute in a guide reference XML node should fail with EpubPackageException")]
        public async void ReadPackageWithoutGuideReferenceHrefTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITHOUT_HREF_IN_GUIDE_REFERENCE);
        }

        [Fact(DisplayName = "Trying to read OPF package with empty 'href' attribute in a guide reference XML node should fail with EpubPackageException")]
        public async void ReadPackageWithEmptyGuideReferenceHrefTest()
        {
            await TestFailingReadOperation(OPF_FILE_WITH_EMPTY_HREF_IN_GUIDE_REFERENCE);
        }

        private async Task TestSuccessfulReadOperation(string opfFileContent, EpubPackage expectedEpubPackage, EpubReaderOptions epubReaderOptions = null)
        {
            TestZipFile testZipFile = CreateTestZipFileWithOpfFile(opfFileContent);
            PackageReader packageReader = new(epubReaderOptions);
            EpubPackage actualEpubPackage = await packageReader.ReadPackageAsync(testZipFile, OPF_FILE_PATH);
            EpubPackageComparer.CompareEpubPackages(expectedEpubPackage, actualEpubPackage);
        }

        private Task TestSuccessfulReadOperationWithSkippingInvalidManifestItems(string opfFileContent, EpubPackage expectedEpubPackage)
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

        private async Task TestFailingReadOperation(string opfFileContent, EpubReaderOptions epubReaderOptions = null)
        {
            TestZipFile testZipFile = CreateTestZipFileWithOpfFile(opfFileContent);
            PackageReader packageReader = new(epubReaderOptions);
            await Assert.ThrowsAsync<EpubPackageException>(() => packageReader.ReadPackageAsync(testZipFile, OPF_FILE_PATH));
        }

        private TestZipFile CreateTestZipFileWithOpfFile(string opfFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(CONTAINER_FILE_PATH, CONTAINER_FILE);
            testZipFile.AddEntry(OPF_FILE_PATH, opfFileContent);
            return testZipFile;
        }
    }
}
