using System.Xml.Linq;
using VersOne.Epub.Internal;
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
                titles: new List<EpubMetadataTitle>()
                {
                    new EpubMetadataTitle
                    (
                        title: "Test title 1",
                        id: "title-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new EpubMetadataTitle
                    (
                        title: "Test title 2",
                        id: "title-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                },
                creators: new List<EpubMetadataCreator>()
                {
                    new EpubMetadataCreator
                    (
                        role: "author",
                        id: "creator-1",
                        fileAs: "Doe, John",
                        creator: "John Doe",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new EpubMetadataCreator
                    (
                        role: "author",
                        id: "creator-2",
                        fileAs: "Doe, Jane",
                        creator: "Jane Doe",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                },
                subjects: new List<EpubMetadataSubject>()
                {
                    new EpubMetadataSubject
                    (
                        subject: "Test subject 1",
                        id: "subject-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new EpubMetadataSubject
                    (
                        subject: "Test subject 2",
                        id: "subject-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                },
                descriptions: new List<EpubMetadataDescription>()
                {
                    new EpubMetadataDescription
                    (
                        description: "Test description 1",
                        id: "description-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new EpubMetadataDescription
                    (
                        description: "Test description 2",
                        id: "description-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                },
                publishers: new List<EpubMetadataPublisher>()
                {
                    new EpubMetadataPublisher
                    (
                        publisher: "Test publisher 1",
                        id: "publisher-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new EpubMetadataPublisher
                    (
                        publisher: "Test publisher 2",
                        id: "publisher-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                },
                contributors: new List<EpubMetadataContributor>()
                {
                    new EpubMetadataContributor
                    (
                        role: "editor",
                        id: "contributor-1",
                        fileAs: "Editor, John",
                        contributor: "John Editor",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new EpubMetadataContributor
                    (
                        role: "editor",
                        id: "contributor-2",
                        fileAs: "Editor, Jane",
                        contributor: "Jane Editor",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                },
                dates: new List<EpubMetadataDate>()
                {
                    new EpubMetadataDate
                    (
                        @event: "creation",
                        id: "date-1",
                        date: "2021-12-31T23:59:59.123456Z"
                    ),
                    new EpubMetadataDate
                    (
                        @event: "publication",
                        id: "date-2",
                        date: "2022-01-23"
                    )
                },
                types: new List<EpubMetadataType>()
                {
                    new EpubMetadataType
                    (
                        type: "dictionary",
                        id: "type-1"
                    ),
                    new EpubMetadataType
                    (
                        type: "preview",
                        id: "type-2"
                    )
                },
                formats: new List<EpubMetadataFormat>()
                {
                    new EpubMetadataFormat
                    (
                        format: "format-1",
                        id: "format-1"
                    ),
                    new EpubMetadataFormat
                    (
                        format: "format-2",
                        id: "format-2"
                    )
                },
                identifiers: new List<EpubMetadataIdentifier>()
                {
                    new EpubMetadataIdentifier
                    (
                        identifier: "https://example.com/books/123",
                        id: "identifier-1",
                        scheme: "URI"
                    ),
                    new EpubMetadataIdentifier
                    (
                        identifier: "9781234567890",
                        id: "identifier-2",
                        scheme: "ISBN"
                    )
                },
                sources: new List<EpubMetadataSource>()
                {
                    new EpubMetadataSource
                    (
                        source: "https://example.com/books/123/content-1.html",
                        id: "source-1"
                    ),
                    new EpubMetadataSource
                    (
                        source: "https://example.com/books/123/content-2.html",
                        id: "source-2"
                    )
                },
                languages: new List<EpubMetadataLanguage>()
                {
                    new EpubMetadataLanguage
                    (
                        language: "en",
                        id: "language-1"
                    ),
                    new EpubMetadataLanguage
                    (
                        language: "is",
                        id: "language-2"
                    )
                },
                relations: new List<EpubMetadataRelation>()
                {
                    new EpubMetadataRelation
                    (
                        relation: "https://example.com/books/123/related-1.html",
                        id: "relation-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new EpubMetadataRelation
                    (
                        relation: "https://example.com/books/123/related-2.html",
                        id: "relation-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                },
                coverages: new List<EpubMetadataCoverage>()
                {
                    new EpubMetadataCoverage
                    (
                        coverage: "New York",
                        id: "coverage-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new EpubMetadataCoverage
                    (
                        coverage: "1700-1850",
                        id: "coverage-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                },
                rights: new List<EpubMetadataRights>()
                {
                    new EpubMetadataRights
                    (
                        rights: "Public domain in the USA",
                        id: "rights-1",
                        textDirection: EpubTextDirection.LEFT_TO_RIGHT,
                        language: "en"
                    ),
                    new EpubMetadataRights
                    (
                        rights: "All rights reserved",
                        id: "rights-2",
                        textDirection: EpubTextDirection.RIGHT_TO_LEFT,
                        language: "is"
                    )
                },
                links: new List<EpubMetadataLink>()
                {
                    new EpubMetadataLink
                    (
                        id: "link-1",
                        href: "front.html#meta-json",
                        mediaType: "application/xhtml+xml",
                        properties: null,
                        refines: null,
                        relationships: new List<EpubMetadataLinkRelationship>()
                        {
                            EpubMetadataLinkRelationship.RECORD
                        },
                        hrefLanguage: "en"
                    ),
                    new EpubMetadataLink
                    (
                        id: "link-2",
                        href: "https://example.com/onix/123",
                        mediaType: "application/xml",
                        properties: new List<EpubMetadataLinkProperty>()
                        {
                            EpubMetadataLinkProperty.ONIX
                        },
                        refines: null,
                        relationships: new List<EpubMetadataLinkRelationship>()
                        {
                            EpubMetadataLinkRelationship.RECORD,
                            EpubMetadataLinkRelationship.ONIX_RECORD
                        },
                        hrefLanguage: null
                    ),
                    new EpubMetadataLink
                    (
                        id: "link-3",
                        href: "book.atom",
                        mediaType: "application/atom+xml;type=entry;profile=opds-catalog",
                        properties: null,
                        refines: null,
                        relationships: new List<EpubMetadataLinkRelationship>()
                        {
                            EpubMetadataLinkRelationship.RECORD
                        },
                        hrefLanguage: null
                    ),
                    new EpubMetadataLink
                    (
                        id: "link-4",
                        href: "title.mp3",
                        mediaType: "audio/mpeg",
                        properties: null,
                        refines: "#title-1",
                        relationships: new List<EpubMetadataLinkRelationship>()
                        {
                            EpubMetadataLinkRelationship.VOICING
                        },
                        hrefLanguage: null
                    )
                },
                metaItems: new List<EpubMetadataMeta>()
                {
                    new EpubMetadataMeta
                    (
                        name: "cover",
                        content: "cover-image"
                    ),
                    new EpubMetadataMeta
                    (
                        name: null,
                        content: "landscape",
                        id: "meta-1",
                        refines: null,
                        property: "rendition:orientation",
                        scheme: null
                    ),
                    new EpubMetadataMeta
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
                    new EpubMetadataMeta
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
                }
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

        [Fact(DisplayName = "Trying to read metadata XML without 'href' attribute in a metadata link XML node should fail with EpubPackageException")]
        public void ReadPackageWithoutMetadataLinkHrefTest()
        {
            TestFailingReadOperation(METADATA_XML_WITHOUT_HREF_IN_METADATA_LINK);
        }

        [Fact(DisplayName = "Trying to read metadata XML without 'rel' attribute in a metadata link XML node should fail with EpubPackageException")]
        public void ReadPackageWithoutMetadataLinkRelTest()
        {
            TestFailingReadOperation(METADATA_XML_WITHOUT_REL_IN_METADATA_LINK);
        }


        private static void TestSuccessfulReadOperation(string metadataXml, EpubMetadata expectedEpubMetadata)
        {
            XElement metadataNode = XElement.Parse(metadataXml);
            EpubMetadata actualEpubMetadata = MetadataReader.ReadMetadata(metadataNode);
            EpubMetadataComparer.CompareEpubMetadatas(expectedEpubMetadata, actualEpubMetadata);
        }

        private static void TestFailingReadOperation(string metadataXml)
        {
            XElement metadataNode = XElement.Parse(metadataXml);
            Assert.Throws<EpubPackageException>(() => MetadataReader.ReadMetadata(metadataNode));
        }
    }
}
