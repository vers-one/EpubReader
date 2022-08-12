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
        /// <para>Gets the optional unique identifier of the content pointer.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>Gets an IRI that resolves to a content document or a fragment within the EPUB book.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Source { get; internal set; }

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
