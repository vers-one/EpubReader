using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Pointer to the book's content associated with a navigational element.</para>
    /// <para>
    /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
    /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
    /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
    /// </para>
    /// </summary>
    public class Epub2NcxContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxContent" /> class with the specified source.
        /// </summary>
        /// <param name="source">An IRI that resolves to a content document or a fragment within the EPUB book.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="source" /> parameter is <c>null</c>.</exception>
        public Epub2NcxContent(string source)
            : this(null, source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxContent" /> class with specified ID and source.
        /// </summary>
        /// <param name="id">The unique identifier of the content pointer or <c>null</c> if the identifier is not specified.</param>
        /// <param name="source">An IRI that resolves to a content document or a fragment within the EPUB book.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="source" /> parameter is <c>null</c>.</exception>
        public Epub2NcxContent(string? id, string source)
        {
            Id = id;
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// <para>Gets the unique identifier of the content pointer or <c>null</c> if the identifier is not specified.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets an IRI that resolves to a content document or a fragment within the EPUB book.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Returns a string containing the value of the <see cref="Source" /> property for debugging purposes.
        /// </summary>
        /// <returns>A string containing the value of the <see cref="Source" /> property.</returns>
        public override string ToString()
        {
            return "Source: " + Source;
        }
    }
}
