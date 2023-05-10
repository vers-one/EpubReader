using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Represents a type of the EPUB book. Types are used to indicate that the EPUB book is of a specialized type
    /// (e.g., annotations or a dictionary packaged in EPUB format).
    /// </para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dctype" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.8" />,
    /// and <see href="http://purl.org/dc/elements/1.1/type" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataType" /> class.
        /// </summary>
        /// <param name="type">The name of the type.</param>
        /// <param name="id">The unique ID of this type metadata element or <c>null</c> if the type metadata element doesn't have an ID.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="type" /> parameter is <c>null</c>.</exception>
        public EpubMetadataType(string type, string? id = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Id = id;
        }

        /// <summary>
        /// <para>Gets the name of the type.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dctype" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.8" />,
        /// and <see href="http://purl.org/dc/elements/1.1/type" /> for more information.
        /// </para>
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// <para>Gets the unique ID of this type metadata element or <c>null</c> if the type metadata element doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }
    }
}
