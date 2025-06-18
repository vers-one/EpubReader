using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Represents a file format, physical media, or dimensions of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub/#sec-opf-dcmes-optional-def" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.9" />,
    /// and <see href="http://purl.org/dc/elements/1.1/format" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataFormat" /> class.
        /// </summary>
        /// <param name="format">The format value.</param>
        /// <param name="id">The unique ID of this format metadata element or <c>null</c> if the format metadata element doesn't have an ID.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="format" /> parameter is <c>null</c>.</exception>
        public EpubMetadataFormat(string format, string? id = null)
        {
            Format = format ?? throw new ArgumentNullException(nameof(format));
            Id = id;
        }

        /// <summary>
        /// <para>Gets the format value.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub/#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.9" />,
        /// and <see href="http://purl.org/dc/elements/1.1/format" /> for more information.
        /// </para>
        /// </summary>
        public string Format { get; }

        /// <summary>
        /// <para>Gets the unique ID of this format metadata element or <c>null</c> if the format metadata element doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }
    }
}
