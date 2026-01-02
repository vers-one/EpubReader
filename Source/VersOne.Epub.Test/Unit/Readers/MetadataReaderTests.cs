using System.Xml.Linq;
using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class MetadataReaderTests
    {
        private const string MINIMAL_METADATA_XML = "<metadata />";

        private const string FULL_METADATA_XML = $"""
            <metadata xmlns="http://www.idpf.org/2007/opf" xmlns:opf="http://www.idpf.org/2007/opf" xmlns:dc="http://purl.org/dc/elements/1.1/">
              <dc:title id="title-1" dir="ltr" lang="en">Test title 1</dc:title>
              <dc:title id="title-2" dir="rtl" lang="is">Test title 2</dc:title>
              <dc:creator id="creator-1" opf:role="author" opf:file-as="Doe, John" dir="ltr" lang="en">John Doe</dc:creator>
              <dc:creator id="creator-2" opf:role="author" opf:file-as="Doe, Jane" dir="rtl" lang="is">Jane Doe</dc:creator>
              <dc:subject id="subject-1" dir="ltr" lang="en">Test subject 1</dc:subject>
              <dc:subject id="subject-2" dir="rtl" lang="is">Test subject 2</dc:subject>
              <dc:description id="description-1" dir="ltr" lang="en">Test description 1</dc:description>
              <dc:description id="description-2" dir="rtl" lang="is">Test description 2</dc:description>
              <dc:publisher id="publisher-1" dir="ltr" lang="en">Test publisher 1</dc:publisher>
              <dc:publisher id="publisher-2" dir="rtl" lang="is">Test publisher 2</dc:publisher>
              <dc:contributor id="contributor-1" opf:role="editor" opf:file-as="Editor, John" dir="ltr" lang="en">John Editor</dc:contributor>
              <dc:contributor id="contributor-2" opf:role="editor" opf:file-as="Editor, Jane" dir="rtl" lang="is">Jane Editor</dc:contributor>
              <dc:date id="date-1" opf:event="creation">2021-12-31T23:59:59.123456Z</dc:date>
              <dc:date id="date-2" opf:event="publication">2022-01-23</dc:date>
              <dc:type id="type-1">dictionary</dc:type>
              <dc:type id="type-2">preview</dc:type>
              <dc:format id="format-1">format-1</dc:format>
              <dc:format id="format-2">format-2</dc:format>
              <dc:identifier id="identifier-1" opf:scheme="URI">https://example.com/books/123</dc:identifier>
              <dc:identifier id="identifier-2" opf:scheme="ISBN">9781234567890</dc:identifier>
              <dc:source id="source-1">https://example.com/books/123/content-1.html</dc:source>
              <dc:source id="source-2">https://example.com/books/123/content-2.html</dc:source>
              <dc:language id="language-1">en</dc:language>
              <dc:language id="language-2">is</dc:language>
              <dc:relation id="relation-1" dir="ltr" lang="en">https://example.com/books/123/related-1.html</dc:relation>
              <dc:relation id="relation-2" dir="rtl" lang="is">https://example.com/books/123/related-2.html</dc:relation>
              <dc:coverage id="coverage-1" dir="ltr" lang="en">New York</dc:coverage>
              <dc:coverage id="coverage-2" dir="rtl" lang="is">1700-1850</dc:coverage>
              <dc:rights id="rights-1" dir="ltr" lang="en">Public domain in the USA</dc:rights>
              <dc:rights id="rights-2" dir="rtl" lang="is">All rights reserved</dc:rights>
              <link id="link-1" rel="record" href="front.html#meta-json" media-type="application/xhtml+xml" hreflang="en" />
              <link id="link-2" rel="record onix-record" href="https://example.com/onix/123" media-type="application/xml" properties="onix" />
              <link id="link-3" rel="record" href="book.atom" media-type="application/atom+xml;type=entry;profile=opds-catalog" />
              <link id="link-4" rel="voicing" refines="#title-1" href="title.mp3" media-type="audio/mpeg" />
              <meta name="cover" content="cover-image" />
              <meta id="meta-1" property="rendition:orientation">landscape</meta>
              <meta id="meta-2" property="identifier-type" refines="#identifier-2" scheme="onix:codelist5" dir="ltr" lang="en">123</meta>
              <meta id="meta-3" property="alternate-script" refines="#creator-1" dir="rtl" lang="is">Brynjólfur Sveinsson</meta>
            </metadata>
            """;

        private const string METADATA_XML_WITHOUT_HREF_IN_METADATA_LINK = $"""
            <metadata>
              <link rel="record" />
            </metadata>
            """;

        private const string METADATA_XML_WITHOUT_REL_IN_METADATA_LINK = $"""
            <metadata>
              <link href="chapter.html" />
            </metadata>
            """;

        private static EpubMetadata MinimalMetadata => new();

        private static EpubMetadata FullMetadata =>
            new
            (
                titles:
                [
                    new
                    (
                        title: "Test title 1",
                        id: "title-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new
                    (
                        title: "Test title 2",
                        id: "title-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                ],
                creators:
                [
                    new
                    (
                        role: "author",
                        id: "creator-1",
                        fileAs: "Doe, John",
                        creator: "John Doe",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new
                    (
                        role: "author",
                        id: "creator-2",
                        fileAs: "Doe, Jane",
                        creator: "Jane Doe",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                ],
                subjects:
                [
                    new
                    (
                        subject: "Test subject 1",
                        id: "subject-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new
                    (
                        subject: "Test subject 2",
                        id: "subject-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                ],
                descriptions:
                [
                    new
                    (
                        description: "Test description 1",
                        id: "description-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new
                    (
                        description: "Test description 2",
                        id: "description-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                ],
                publishers:
                [
                    new
                    (
                        publisher: "Test publisher 1",
                        id: "publisher-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new
                    (
                        publisher: "Test publisher 2",
                        id: "publisher-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                ],
                contributors:
                [
                    new
                    (
                        role: "editor",
                        id: "contributor-1",
                        fileAs: "Editor, John",
                        contributor: "John Editor",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new
                    (
                        role: "editor",
                        id: "contributor-2",
                        fileAs: "Editor, Jane",
                        contributor: "Jane Editor",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                ],
                dates:
                [
                    new
                    (
                        @event: "creation",
                        id: "date-1",
                        date: "2021-12-31T23:59:59.123456Z"
                    ),
                    new
                    (
                        @event: "publication",
                        id: "date-2",
                        date: "2022-01-23"
                    )
                ],
                types:
                [
                    new
                    (
                        type: "dictionary",
                        id: "type-1"
                    ),
                    new
                    (
                        type: "preview",
                        id: "type-2"
                    )
                ],
                formats:
                [
                    new
                    (
                        format: "format-1",
                        id: "format-1"
                    ),
                    new
                    (
                        format: "format-2",
                        id: "format-2"
                    )
                ],
                identifiers:
                [
                    new
                    (
                        identifier: "https://example.com/books/123",
                        id: "identifier-1",
                        scheme: "URI"
                    ),
                    new
                    (
                        identifier: "9781234567890",
                        id: "identifier-2",
                        scheme: "ISBN"
                    )
                ],
                sources:
                [
                    new
                    (
                        source: "https://example.com/books/123/content-1.html",
                        id: "source-1"
                    ),
                    new
                    (
                        source: "https://example.com/books/123/content-2.html",
                        id: "source-2"
                    )
                ],
                languages:
                [
                    new
                    (
                        language: "en",
                        id: "language-1"
                    ),
                    new
                    (
                        language: "is",
                        id: "language-2"
                    )
                ],
                relations:
                [
                    new
                    (
                        relation: "https://example.com/books/123/related-1.html",
                        id: "relation-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new
                    (
                        relation: "https://example.com/books/123/related-2.html",
                        id: "relation-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                ],
                coverages:
                [
                    new
                    (
                        coverage: "New York",
                        id: "coverage-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new
                    (
                        coverage: "1700-1850",
                        id: "coverage-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                ],
                rights:
                [
                    new
                    (
                        rights: "Public domain in the USA",
                        id: "rights-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new
                    (
                        rights: "All rights reserved",
                        id: "rights-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                ],
                links:
                [
                    new
                    (
                        id: "link-1",
                        href: "front.html#meta-json",
                        mediaType: "application/xhtml+xml",
                        properties: null,
                        refines: null,
                        relationships:
                        [
                            EpubMetadataLinkRelationship.RECORD
                        ],
                        hrefLanguage: "en"
                    ),
                    new
                    (
                        id: "link-2",
                        href: "https://example.com/onix/123",
                        mediaType: "application/xml",
                        properties:
                        [
                            EpubMetadataLinkProperty.ONIX
                        ],
                        refines: null,
                        relationships:
                        [
                            EpubMetadataLinkRelationship.RECORD,
                            EpubMetadataLinkRelationship.ONIX_RECORD
                        ],
                        hrefLanguage: null
                    ),
                    new
                    (
                        id: "link-3",
                        href: "book.atom",
                        mediaType: "application/atom+xml;type=entry;profile=opds-catalog",
                        properties: null,
                        refines: null,
                        relationships:
                        [
                            EpubMetadataLinkRelationship.RECORD
                        ],
                        hrefLanguage: null
                    ),
                    new
                    (
                        id: "link-4",
                        href: "title.mp3",
                        mediaType: "audio/mpeg",
                        properties: null,
                        refines: "#title-1",
                        relationships:
                        [
                            EpubMetadataLinkRelationship.VOICING
                        ],
                        hrefLanguage: null
                    )
                ],
                metaItems:
                [
                    new
                    (
                        name: "cover",
                        content: "cover-image"
                    ),
                    new
                    (
                        name: null,
                        content: "landscape",
                        id: "meta-1",
                        refines: null,
                        property: "rendition:orientation",
                        scheme: null
                    ),
                    new
                    (
                        name: null,
                        content: "123",
                        id: "meta-2",
                        refines: "#identifier-2",
                        property: "identifier-type",
                        scheme: "onix:codelist5",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new
                    (
                        name: null,
                        content: "Brynjólfur Sveinsson",
                        id: "meta-3",
                        refines: "#creator-1",
                        property: "alternate-script",
                        scheme: null,
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                ]
            );

        [Fact(DisplayName = "Reading a minimal metadata XML should succeed")]
        public void ReadMinimalMetadataTest()
        {
            TestSuccessfulReadOperation(MINIMAL_METADATA_XML, MinimalMetadata);
        }

        [Fact(DisplayName = "Reading a full metadata XML should succeed")]
        public void ReadFullMetadataTest()
        {
            TestSuccessfulReadOperation(FULL_METADATA_XML, FullMetadata);
        }

        [Fact(DisplayName = "ReadMetadata should throw EpubPackageException when a 'link' XML node doesn't have the 'href' attribute and no MetadataReaderOptions are provided")]
        public void ReadPackageWithoutMetadataLinkHrefAndDefaultOptionsTest()
        {
            TestFailingReadOperation(METADATA_XML_WITHOUT_HREF_IN_METADATA_LINK);
        }

        [Fact(DisplayName = "ReadMetadata should skip 'link' XML nodes without the 'href' attribute when SkipLinksWithoutHrefs = true")]
        public void ReadPackageWithoutMetadataLinkHrefAndSkipLinksWithoutHrefsTest()
        {
            MetadataReaderOptions metadataReaderOptions = new()
            {
                SkipLinksWithoutHrefs = true
            };
            TestSuccessfulReadOperation(METADATA_XML_WITHOUT_HREF_IN_METADATA_LINK, MinimalMetadata, metadataReaderOptions);
        }

        [Fact(DisplayName = "ReadMetadata should throw EpubPackageException when a 'link' XML node doesn't have the 'rel' attribute and no MetadataReaderOptions are provided")]
        public void ReadPackageWithoutMetadataLinkRelAndDefaultOptionsTest()
        {
            TestFailingReadOperation(METADATA_XML_WITHOUT_REL_IN_METADATA_LINK);
        }

        [Fact(DisplayName = "ReadMetadata should succeed when a 'link' XML node doesn't have the 'rel' attribute and IgnoreLinkWithoutRelError = true")]
        public void ReadPackageWithoutMetadataLinkRelAndIgnoreLinkWithoutRelErrorTest()
        {
            EpubMetadata expectedEpubMetadata =
                new
                (
                    links:
                    [
                        new
                        (
                            href: "chapter.html" 
                        )
                    ]
                );
            MetadataReaderOptions metadataReaderOptions = new()
            {
                IgnoreLinkWithoutRelError = true
            };
            TestSuccessfulReadOperation(METADATA_XML_WITHOUT_REL_IN_METADATA_LINK, expectedEpubMetadata, metadataReaderOptions);
        }

        private static void TestSuccessfulReadOperation(string metadataXml, EpubMetadata expectedEpubMetadata,
            MetadataReaderOptions? metadataReaderOptions = null)
        {
            XElement metadataNode = XElement.Parse(metadataXml);
            EpubMetadata actualEpubMetadata = MetadataReader.ReadMetadata(metadataNode, metadataReaderOptions ?? new MetadataReaderOptions());
            EpubMetadataComparer.CompareEpubMetadatas(expectedEpubMetadata, actualEpubMetadata);
        }

        private static void TestFailingReadOperation(string metadataXml)
        {
            XElement metadataNode = XElement.Parse(metadataXml);
            Assert.Throws<EpubPackageException>(() => MetadataReader.ReadMetadata(metadataNode, new MetadataReaderOptions()));
        }
    }
}
