using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Encapsulates meta information for the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub-33/#sec-pkg-metadata" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadata" /> class.
        /// </summary>
        /// <param name="titles">A list of titles of the EPUB book.</param>
        /// <param name="creators">A list of creators of the EPUB book.</param>
        /// <param name="subjects">A list of subjects of the EPUB book.</param>
        /// <param name="descriptions">A list of descriptions of the EPUB book.</param>
        /// <param name="publishers">A list of publishers of the EPUB book.</param>
        /// <param name="contributors">A list of names of persons, organizations, etc. that played a secondary role in the creation of the content of the EPUB book.</param>
        /// <param name="dates">A list of dates of the events associated with the EPUB book (e.g. publication date).</param>
        /// <param name="types">A list of types of the EPUB book.</param>
        /// <param name="formats">A list of file formats, physical media, or dimensions of the EPUB book.</param>
        /// <param name="identifiers">A list of identifiers associated with the EPUB book, such as a UUID, DOI, or ISBN.</param>
        /// <param name="sources">A list of sources of the EPUB book.</param>
        /// <param name="languages">A list of languages of the content of the EPUB book.</param>
        /// <param name="relations">A list of related resources of the EPUB book.</param>
        /// <param name="coverages">A list of coverages of the EPUB book.</param>
        /// <param name="rights">A list of rights held in and over the EPUB book.</param>
        /// <param name="links">A list of metadata links of the EPUB book.</param>
        /// <param name="metaItems">A list of generic metadata items of the EPUB book.</param>
        public EpubMetadata(List<EpubMetadataTitle>? titles = null, List<EpubMetadataCreator>? creators = null, List<EpubMetadataSubject>? subjects = null,
            List<EpubMetadataDescription>? descriptions = null, List<EpubMetadataPublisher>? publishers = null, List<EpubMetadataContributor>? contributors = null,
            List<EpubMetadataDate>? dates = null, List<EpubMetadataType>? types = null, List<EpubMetadataFormat>? formats = null, List<EpubMetadataIdentifier>? identifiers = null,
            List<EpubMetadataSource>? sources = null, List<EpubMetadataLanguage>? languages = null, List<EpubMetadataRelation>? relations = null,
            List<EpubMetadataCoverage>? coverages = null, List<EpubMetadataRights>? rights = null, List<EpubMetadataLink>? links = null, List<EpubMetadataMeta>? metaItems = null)
        {
            Titles = titles ?? new List<EpubMetadataTitle>();
            Creators = creators ?? new List<EpubMetadataCreator>();
            Subjects = subjects ?? new List<EpubMetadataSubject>();
            Descriptions = descriptions ?? new List<EpubMetadataDescription>();
            Publishers = publishers ?? new List<EpubMetadataPublisher>();
            Contributors = contributors ?? new List<EpubMetadataContributor>();
            Dates = dates ?? new List<EpubMetadataDate>();
            Types = types ?? new List<EpubMetadataType>();
            Formats = formats ?? new List<EpubMetadataFormat>();
            Identifiers = identifiers ?? new List<EpubMetadataIdentifier>();
            Sources = sources ?? new List<EpubMetadataSource>();
            Languages = languages ?? new List<EpubMetadataLanguage>();
            Relations = relations ?? new List<EpubMetadataRelation>();
            Coverages = coverages ?? new List<EpubMetadataCoverage>();
            Rights = rights ?? new List<EpubMetadataRights>();
            Links = links ?? new List<EpubMetadataLink>();
            MetaItems = metaItems ?? new List<EpubMetadataMeta>();
        }

        /// <summary>
        /// <para>Gets a list of titles. Each element in this list represents an instance of a name given to the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dctitle" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.1" />,
        /// and <see href="http://purl.org/dc/elements/1.1/title" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataTitle> Titles { get; }

        /// <summary>
        /// <para>
        /// Gets a list of creators. Each element in this list represents the name of a person, organization, etc. responsible for the creation of the content of the EPUB book.
        /// </para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dccreator" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.2" />,
        /// and <see href="http://purl.org/dc/elements/1.1/creator" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataCreator> Creators { get; }

        /// <summary>
        /// <para>Gets a list of subjects. Each element in this list identifies a subject of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcsubject" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.3" />,
        /// and <see href="http://purl.org/dc/elements/1.1/subject" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataSubject> Subjects { get; }

        /// <summary>
        /// <para>Gets a list of descriptions of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.4" />,
        /// and <see href="http://purl.org/dc/elements/1.1/description" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataDescription> Descriptions { get; }

        /// <summary>
        /// <para>Gets a list of publishers of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.5" />,
        /// and <see href="http://purl.org/dc/elements/1.1/publisher" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataPublisher> Publishers { get; }

        /// <summary>
        /// <para>Gets a list of names of persons, organizations, etc. that played a secondary role in the creation of the content of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dccontributor" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.6" />,
        /// and <see href="http://purl.org/dc/elements/1.1/contributor" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataContributor> Contributors { get; }

        /// <summary>
        /// <para>Gets a list of dates of the events associated with the EPUB book (e.g. publication date).</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcdate" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.7" />,
        /// and <see href="http://purl.org/dc/elements/1.1/date" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataDate> Dates { get; }

        /// <summary>
        /// <para>
        /// Gets a list of types of the EPUB book. Types are used to indicate that the EPUB book is of a specialized type
        /// (e.g., annotations or a dictionary packaged in EPUB format).
        /// </para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dctype" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.8" />,
        /// and <see href="http://purl.org/dc/elements/1.1/type" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataType> Types { get; }

        /// <summary>
        /// <para>Gets a list of file formats, physical media, or dimensions of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.9" />,
        /// and <see href="http://purl.org/dc/elements/1.1/format" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataFormat> Formats { get; }

        /// <summary>
        /// <para>Gets a list of identifiers associated with the EPUB book, such as a UUID, DOI, or ISBN.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcidentifier" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.10" />,
        /// and <see href="http://purl.org/dc/elements/1.1/identifier" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataIdentifier> Identifiers { get; }

        /// <summary>
        /// <para>Gets a list of sources. A source is a related resource from which the EPUB book is derived.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.11" />,
        /// and <see href="http://purl.org/dc/elements/1.1/source" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataSource> Sources { get; }

        /// <summary>
        /// <para>Gets a list of languages of the content of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dclanguage" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.12" />,
        /// and <see href="http://purl.org/dc/elements/1.1/language" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataLanguage> Languages { get; }

        /// <summary>
        /// <para>Gets a list of related resources of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.13" />,
        /// and <see href="http://purl.org/dc/elements/1.1/relation" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataRelation> Relations { get; }

        /// <summary>
        /// <para>
        /// Gets a list of coverages of the EPUB book. A coverage is the spatial or temporal topic of the book,
        /// the spatial applicability of the book, or the jurisdiction under which the book is relevant.
        /// </para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.14" />,
        /// and <see href="http://purl.org/dc/elements/1.1/coverage" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataCoverage> Coverages { get; }

        /// <summary>
        /// <para>Gets a list of rights held in and over the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.15" />,
        /// and <see href="http://purl.org/dc/elements/1.1/rights" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataRights> Rights { get; }

        /// <summary>
        /// <para>Gets a list of metadata links. Links are used to associate resources with the EPUB book, such as metadata records.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-link-elem" /> for more information.</para>
        /// </summary>
        public List<EpubMetadataLink> Links { get; }

        /// <summary>
        /// <para>Gets a list of generic metadata items of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-meta-elem" />,
        /// <see href="https://www.w3.org/TR/epub-33/#sec-opf2-meta" />,
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataMeta> MetaItems { get; }
    }
}
