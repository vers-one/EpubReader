using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>NCX navigation target which represents a navigable element for the secondary navigation within the book (e.g., lists of notes, figures, tables, etc.).</para>
    /// <para>
    /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
    /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
    /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
    /// </para>
    /// </summary>
    public class Epub2NcxNavigationTarget
    {
        /// <summary>
        /// <para>Gets the unique identifier of the navigation target.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>Gets an optional description of the kind of structural unit the navigation target represents (e.g., 'note').</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Class { get; internal set; }

        /// <summary>
        /// <para>Gets a positive integer representing the numeric value associated with the navigation point within the navigation list.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Value { get; internal set; }

        /// <summary>
        /// <para>Gets an optional positive integer denoting the location of the content of the navigation target in the reading order.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string PlayOrder { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets a list of navigation labels providing textual description of the navigation target for the reader (e.g., a description of a footnote).
        /// A single navigation target can contain multiple labels in different languages.
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationLabel> NavigationLabels { get; internal set; }

        /// <summary>
        /// <para>Gets a pointer to the book's content associated with the navigation target.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public Epub2NcxContent Content { get; internal set; }
    }
}
