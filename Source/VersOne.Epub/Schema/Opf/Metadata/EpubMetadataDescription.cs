using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Represents a description of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub/#sec-opf-dcmes-optional-def" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.4" />,
    /// and <see href="http://purl.org/dc/elements/1.1/description" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataDescription" /> class.
        /// </summary>
        /// <param name="description">The text content of this description.</param>
        /// <param name="id">The unique ID of this description or <c>null</c> if the description doesn't have an ID.</param>
        /// <param name="textDirection">The text direction of this description or <c>null</c> if the description doesn't specify a text direction.</param>
        /// <param name="language">The language of this description or <c>null</c> if the description doesn't specify the language.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="description" /> parameter is <c>null</c>.</exception>
        public EpubMetadataDescription(string description, string? id = null, EpubTextDirection? textDirection = null, string? language = null)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Id = id;
            TextDirection = textDirection;
            Language = language;
        }

        /// <summary>
        /// <para>Gets the text content of this description.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.4" />,
        /// and <see href="http://purl.org/dc/elements/1.1/description" /> for more information.
        /// </para>
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// <para>Gets the unique ID of this description or <c>null</c> if the description doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the text direction of this description or <c>null</c> if the description doesn't specify a text direction.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-dir" /> for more information.</para>
        /// </summary>
        public EpubTextDirection? TextDirection { get; }

        /// <summary>
        /// <para>Gets the language of this description or <c>null</c> if the description doesn't specify the language.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-xml-lang" /> for more information.</para>
        /// </summary>
        public string? Language { get; }
    }
}
