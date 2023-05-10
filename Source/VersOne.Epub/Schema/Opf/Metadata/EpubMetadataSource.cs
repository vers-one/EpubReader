using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Represents a source of the EPUB book. A source is a related resource from which the EPUB book is derived.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcmes-optional-def" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.11" />,
    /// and <see href="http://purl.org/dc/elements/1.1/source" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataSource" /> class.
        /// </summary>
        /// <param name="source">The source value.</param>
        /// <param name="id">The unique ID of this source metadata element or <c>null</c> if the source metadata element doesn't have an ID.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="source" /> parameter is <c>null</c>.</exception>
        public EpubMetadataSource(string source, string? id = null)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Id = id;
        }

        /// <summary>
        /// <para>Gets the source value.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcmes-optional-def" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.11" />,
        /// and <see href="http://purl.org/dc/elements/1.1/source" /> for more information.
        /// </para>
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// <para>Gets the unique ID of this source metadata element or <c>null</c> if the source metadata element doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }
    }
}
