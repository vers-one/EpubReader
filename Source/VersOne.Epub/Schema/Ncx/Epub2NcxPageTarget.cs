using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>NCX page target. It provides pagination information for a single page within the EPUB book.</para>
    /// <para>
    /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
    /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
    /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
    /// </para>
    /// </summary>
    public class Epub2NcxPageTarget
    {
        /// <summary>
        /// <para>Gets the optional unique identifier of the page target.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>Gets a positive integer representing the numeric value associated with a page.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Value { get; internal set; }

        /// <summary>
        /// <para>Gets the type of the page represented by the page target.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public Epub2NcxPageTargetType Type { get; internal set; }

        /// <summary>
        /// <para>Gets an optional description of the kind of the page represented by the page target.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Class { get; internal set; }

        /// <summary>
        /// <para>Gets an optional positive integer denoting the location of the page content in the reading order.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string PlayOrder { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets a list of navigation labels providing textual description of the page target for the reader.
        /// A single page target can contain multiple labels in different languages.
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationLabel> NavigationLabels { get; internal set; }

        /// <summary>
        /// <para>Gets a pointer to the book's content associated with the page target.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public Epub2NcxContent Content { get; internal set; }
    }
}
