using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Represents a related resource of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcmes-optional-def" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.13" />,
    /// and <see href="http://purl.org/dc/elements/1.1/relation" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataRelation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataRelation" /> class.
        /// </summary>
        /// <param name="relation">The text content of this relation.</param>
        /// <param name="id">The unique ID of this relation or <c>null</c> if the relation doesn't have an ID.</param>
        /// <param name="textDirection">The text direction of this relation or <c>null</c> if the relation doesn't specify a text direction.</param>
        /// <param name="language">The language of this relation or <c>null</c> if the relation doesn't specify the language.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="relation" /> parameter is <c>null</c>.</exception>
        public EpubMetadataRelation(string relation, string? id = null, EpubTextDirection? textDirection = null, string? language = null)
        {
            Relation = relation ?? throw new ArgumentNullException(nameof(relation));
            Id = id;
            TextDirection = textDirection;
            Language = language;
        }

        /// <summary>
        /// <para>Gets the text content of this relation.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.13" />,
        /// and <see href="http://purl.org/dc/elements/1.1/relation" /> for more information.
        /// </para>
        /// </summary>
        public string Relation { get; }

        /// <summary>
        /// <para>Gets the unique ID of this relation or <c>null</c> if the relation doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the text direction of this relation or <c>null</c> if the relation doesn't specify a text direction.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-dir" /> for more information.</para>
        /// </summary>
        public EpubTextDirection? TextDirection { get; }

        /// <summary>
        /// <para>Gets the language of this relation or <c>null</c> if the relation doesn't specify the language.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-xml-lang" /> for more information.</para>
        /// </summary>
        public string? Language { get; }
    }
}
