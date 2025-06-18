using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Represents a coverage of the EPUB book. A coverage is the spatial or temporal topic of the book,
    /// the spatial applicability of the book, or the jurisdiction under which the book is relevant.
    /// </para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub/#sec-opf-dcmes-optional-def" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.14" />,
    /// and <see href="http://purl.org/dc/elements/1.1/coverage" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataCoverage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataCoverage" /> class.
        /// </summary>
        /// <param name="coverage">The text content of this coverage.</param>
        /// <param name="id">The unique ID of this coverage or <c>null</c> if the coverage doesn't have an ID.</param>
        /// <param name="textDirection">The text direction of this coverage or <c>null</c> if the coverage doesn't specify a text direction.</param>
        /// <param name="language">The language of this coverage or <c>null</c> if the coverage doesn't specify the language.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="coverage" /> parameter is <c>null</c>.</exception>
        public EpubMetadataCoverage(string coverage, string? id = null, EpubTextDirection? textDirection = null, string? language = null)
        {
            Coverage = coverage ?? throw new ArgumentNullException(nameof(coverage));
            Id = id;
            TextDirection = textDirection;
            Language = language;
        }

        /// <summary>
        /// <para>Gets the text content of this coverage.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.14" />,
        /// and <see href="http://purl.org/dc/elements/1.1/coverage" /> for more information.
        /// </para>
        /// </summary>
        public string Coverage { get; }

        /// <summary>
        /// <para>Gets the unique ID of this coverage or <c>null</c> if the coverage doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the text direction of this coverage or <c>null</c> if the coverage doesn't specify a text direction.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-dir" /> for more information.</para>
        /// </summary>
        public EpubTextDirection? TextDirection { get; }

        /// <summary>
        /// <para>Gets the language of this coverage or <c>null</c> if the coverage doesn't specify the language.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-xml-lang" /> for more information.</para>
        /// </summary>
        public string? Language { get; }
    }
}
