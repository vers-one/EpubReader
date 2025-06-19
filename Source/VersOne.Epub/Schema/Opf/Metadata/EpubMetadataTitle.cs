using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Represents an instance of a name given to the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dctitle" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.1" />,
    /// and <see href="http://purl.org/dc/elements/1.1/title" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataTitle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataTitle" /> class.
        /// </summary>
        /// <param name="title">The text content of this title.</param>
        /// <param name="id">The unique ID of this title or <c>null</c> if the title doesn't have an ID.</param>
        /// <param name="textDirection">The text direction of this title or <c>null</c> if the title doesn't specify a text direction.</param>
        /// <param name="language">The language of this title or <c>null</c> if the title doesn't specify the language.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="title" /> parameter is <c>null</c>.</exception>
        public EpubMetadataTitle(string title, string? id = null, EpubTextDirection? textDirection = null, string? language = null)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Id = id;
            TextDirection = textDirection;
            Language = language;
        }

        /// <summary>
        /// <para>Gets the text content of this title.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dctitle" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.1" />,
        /// and <see href="http://purl.org/dc/elements/1.1/title" /> for more information.
        /// </para>
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// <para>Gets the unique ID of this title or <c>null</c> if the title doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the text direction of this title or <c>null</c> if the title doesn't specify a text direction.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-dir" /> for more information.</para>
        /// </summary>
        public EpubTextDirection? TextDirection { get; }

        /// <summary>
        /// <para>Gets the language of this title or <c>null</c> if the title doesn't specify the language.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-xml-lang" /> for more information.</para>
        /// </summary>
        public string? Language { get; }
    }
}
