using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Encapsulates meta information for the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-pkg-metadata" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadata" /> class.
        /// </summary>
        /// <param name="titles">A list of titles.</param>
        /// <param name="creators">A list of creators.</param>
        /// <param name="subjects">A list of subjects.</param>
        /// <param name="description">The description of the EPUB book.</param>
        /// <param name="publishers">A list of publishers of the EPUB book.</param>
        /// <param name="contributors">A list of names of persons, organizations, etc. that played a secondary role in the creation of the content of the EPUB book.</param>
        /// <param name="dates">A list of dates of the events associated with the EPUB book (e.g. publication date).</param>
        /// <param name="types">A list of types of the EPUB book.</param>
        /// <param name="formats">A list of file formats, physical media, or dimensions of the EPUB book.</param>
        /// <param name="identifiers">A list of identifiers associated with the EPUB book, such as a UUID, DOI, or ISBN.</param>
        /// <param name="sources">A list of sources.</param>
        /// <param name="languages">A list of languages of the content of the EPUB book.</param>
        /// <param name="relations">A list of related resources of the EPUB book.</param>
        /// <param name="coverages">A list of coverages of the EPUB book.</param>
        /// <param name="rights">A list of rights held in and over the EPUB book.</param>
        /// <param name="links">A list of metadata links.</param>
        /// <param name="metaItems">A list of generic metadata items of the EPUB book.</param>
        public EpubMetadata(List<string>? titles = null, List<EpubMetadataCreator>? creators = null, List<string>? subjects = null, string? description = null,
            List<string>? publishers = null, List<EpubMetadataContributor>? contributors = null, List<EpubMetadataDate>? dates = null, List<string>? types = null,
            List<string>? formats = null, List<EpubMetadataIdentifier>? identifiers = null, List<string>? sources = null, List<string>? languages = null,
            List<string>? relations = null, List<string>? coverages = null, List<string>? rights = null, List<EpubMetadataLink>? links = null, List<EpubMetadataMeta>? metaItems = null)
        {
            Titles = titles ?? new List<string>();
            Creators = creators ?? new List<EpubMetadataCreator>();
            Subjects = subjects ?? new List<string>();
            Description = description;
            Publishers = publishers ?? new List<string>();
            Contributors = contributors ?? new List<EpubMetadataContributor>();
            Dates = dates ?? new List<EpubMetadataDate>();
            Types = types ?? new List<string>();
            Formats = formats ?? new List<string>();
            Identifiers = identifiers ?? new List<EpubMetadataIdentifier>();
            Sources = sources ?? new List<string>();
            Languages = languages ?? new List<string>();
            Relations = relations ?? new List<string>();
            Coverages = coverages ?? new List<string>();
            Rights = rights ?? new List<string>();
            Links = links ?? new List<EpubMetadataLink>();
            MetaItems = metaItems ?? new List<EpubMetadataMeta>();
        }

        /// <summary>
        /// <para>Gets a list of titles. Each element in this list represents an instance of a name given to the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dctitle" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.1" />,
        /// and <see href="http://purl.org/dc/elements/1.1/title" /> for more information.
        /// </para>
        /// </summary>
        public List<string> Titles { get; }

        /// <summary>
        /// <para>
        /// Gets a list of creators. Each element in this list represents the name of a person, organization, etc. responsible for the creation of the content of the EPUB book.
        /// </para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccreator" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.2" />,
        /// and <see href="http://purl.org/dc/elements/1.1/creator" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataCreator> Creators { get; }

        /// <summary>
        /// <para>Gets a list of subjects. Each element in this list identifies the subject of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcsubject" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.3" />,
        /// and <see href="http://purl.org/dc/elements/1.1/subject" /> for more information.
        /// </para>
        /// </summary>
        public List<string> Subjects { get; }

        /// <summary>
        /// <para>Gets the description of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.4" />,
        /// and <see href="http://purl.org/dc/elements/1.1/description" /> for more information.
        /// </para>
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// <para>Gets a list of publishers of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.5" />,
        /// and <see href="http://purl.org/dc/elements/1.1/publisher" /> for more information.
        /// </para>
        /// </summary>
        public List<string> Publishers { get; }

        /// <summary>
        /// <para>Gets a list of names of persons, organizations, etc. that played a secondary role in the creation of the content of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccontributor" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.6" />,
        /// and <see href="http://purl.org/dc/elements/1.1/contributor" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataContributor> Contributors { get; }

        /// <summary>
        /// <para>Gets a list of dates of the events associated with the EPUB book (e.g. publication date).</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcdate" />,
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
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dctype" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.8" />,
        /// and <see href="http://purl.org/dc/elements/1.1/type" /> for more information.
        /// </para>
        /// </summary>
        public List<string> Types { get; }

        /// <summary>
        /// <para>Gets a list of file formats, physical media, or dimensions of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.9" />,
        /// and <see href="http://purl.org/dc/elements/1.1/format" /> for more information.
        /// </para>
        /// </summary>
        public List<string> Formats { get; }

        /// <summary>
        /// <para>Gets a list of identifiers associated with the EPUB book, such as a UUID, DOI, or ISBN.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcidentifier" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.10" />,
        /// and <see href="http://purl.org/dc/elements/1.1/identifier" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataIdentifier> Identifiers { get; }

        /// <summary>
        /// <para>Gets a list of sources. A source is a related resource from which the EPUB book is derived.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.11" />,
        /// and <see href="http://purl.org/dc/elements/1.1/source" /> for more information.
        /// </para>
        /// </summary>
        public List<string> Sources { get; }

        /// <summary>
        /// <para>Gets a list of languages of the content of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dclanguage" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.12" />,
        /// and <see href="http://purl.org/dc/elements/1.1/language" /> for more information.
        /// </para>
        /// </summary>
        public List<string> Languages { get; }

        /// <summary>
        /// <para>Gets a list of related resources of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.13" />,
        /// and <see href="http://purl.org/dc/elements/1.1/relation" /> for more information.
        /// </para>
        /// </summary>
        public List<string> Relations { get; }

        /// <summary>
        /// <para>
        /// Gets a list of coverages of the EPUB book. A coverage is the spatial or temporal topic of the book,
        /// the spatial applicability of the book, or the jurisdiction under which the book is relevant.
        /// </para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.14" />,
        /// and <see href="http://purl.org/dc/elements/1.1/coverage" /> for more information.
        /// </para>
        /// </summary>
        public List<string> Coverages { get; }

        /// <summary>
        /// <para>Gets a list of rights held in and over the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.15" />,
        /// and <see href="http://purl.org/dc/elements/1.1/rights" /> for more information.
        /// </para>
        /// </summary>
        public List<string> Rights { get; }

        /// <summary>
        /// <para>Gets a list of metadata links. Links are used to associate resources with the EPUB book, such as metadata records.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-link-elem" /> for more information.</para>
        /// </summary>
        public List<EpubMetadataLink> Links { get; }

        /// <summary>
        /// <para>Gets a list of generic metadata items of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-meta-elem" />,
        /// <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf2-meta" />,
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataMeta> MetaItems { get; }
    }
}
