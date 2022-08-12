using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// NCX navigation list which contains a distinct, flat set of navigable elements for the secondary navigation within the book (e.g., lists of notes, figures, tables, etc.).
    /// </para>
    /// <para>
    /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
    /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
    /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
    /// </para>
    /// </summary>
    public class Epub2NcxNavigationList
    {
        /// <summary>
        /// <para>Gets the optional unique identifier of the content pointer.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>Gets an optional description of the type of objects contained in the navigation list (e.g., 'note').</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Class { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets a list of navigation labels providing textual description of the navigation list for the reader.
        /// A single navigation list can contain multiple labels in different languages.
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationLabel> NavigationLabels { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets a list of navigation targets which represent navigable elements for the secondary navigation within the book (e.g., lists of notes, figures, tables, etc.).
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationTarget> NavigationTargets { get; internal set; }
    }
}
