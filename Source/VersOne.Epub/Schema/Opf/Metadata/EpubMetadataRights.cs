using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Represents the rights held in and over the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcmes-optional-def" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.15" />,
    /// and <see href="http://purl.org/dc/elements/1.1/rights" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataRights
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataRights" /> class.
        /// </summary>
        /// <param name="rights">The text content of the rights.</param>
        /// <param name="id">The unique ID of the rights or <c>null</c> if the rights don't have an ID.</param>
        /// <param name="textDirection">The text direction of the rights or <c>null</c> if the rights don't specify a text direction.</param>
        /// <param name="language">The language of the rights or <c>null</c> if the rights don't specify the language.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="rights" /> parameter is <c>null</c>.</exception>
        public EpubMetadataRights(string rights, string? id = null, EpubTextDirection? textDirection = null, string? language = null)
        {
            Rights = rights ?? throw new ArgumentNullException(nameof(rights));
            Id = id;
            TextDirection = textDirection;
            Language = language;
        }

        /// <summary>
        /// <para>Gets the text content of the rights.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.15" />,
        /// and <see href="http://purl.org/dc/elements/1.1/rights" /> for more information.
        /// </para>
        /// </summary>
        public string Rights { get; }

        /// <summary>
        /// <para>Gets the unique ID of the rights or <c>null</c> if the rights don't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the text direction of the rights or <c>null</c> if the rights don't specify a text direction.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-dir" /> for more information.</para>
        /// </summary>
        public EpubTextDirection? TextDirection { get; }

        /// <summary>
        /// <para>Gets the language of the rights or <c>null</c> if the rights don't specify the language.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-xml-lang" /> for more information.</para>
        /// </summary>
        public string? Language { get; }
    }
}
