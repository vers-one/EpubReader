using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Represents a publisher of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub/#sec-opf-dcmes-optional-def" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.5" />,
    /// and <see href="http://purl.org/dc/elements/1.1/publisher" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataPublisher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataPublisher" /> class.
        /// </summary>
        /// <param name="publisher">The name of this publisher.</param>
        /// <param name="id">The unique ID of this publisher or <c>null</c> if the publisher doesn't have an ID.</param>
        /// <param name="textDirection">The text direction of this publisher or <c>null</c> if the publisher doesn't specify a text direction.</param>
        /// <param name="language">The language of this publisher or <c>null</c> if the publisher doesn't specify the language.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="publisher" /> parameter is <c>null</c>.</exception>
        public EpubMetadataPublisher(string publisher, string? id = null, EpubTextDirection? textDirection = null, string? language = null)
        {
            Publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            Id = id;
            TextDirection = textDirection;
            Language = language;
        }

        /// <summary>
        /// <para>Gets the name of this publisher.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.5" />,
        /// and <see href="http://purl.org/dc/elements/1.1/publisher" /> for more information.
        /// </para>
        /// </summary>
        public string Publisher { get; }

        /// <summary>
        /// <para>Gets the unique ID of this publisher or <c>null</c> if the publisher doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the text direction of this publisher or <c>null</c> if the publisher doesn't specify a text direction.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-dir" /> for more information.</para>
        /// </summary>
        public EpubTextDirection? TextDirection { get; }

        /// <summary>
        /// <para>Gets the language of this publisher or <c>null</c> if the publisher doesn't specify the language.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-xml-lang" /> for more information.</para>
        /// </summary>
        public string? Language { get; }
    }
}
