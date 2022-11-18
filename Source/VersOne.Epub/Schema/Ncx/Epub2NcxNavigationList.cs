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
        /// Initializes a new instance of the <see cref="Epub2NcxNavigationList" /> class.
        /// </summary>
        /// <param name="id">The optional unique identifier of the content pointer.</param>
        /// <param name="class">An optional description of the type of objects contained in the navigation list (e.g., 'note').</param>
        /// <param name="navigationLabels">A list of navigation labels providing textual description of the navigation list for the reader.</param>
        /// <param name="navigationTargets">
        /// A list of navigation targets which represent navigable elements for the secondary navigation within the book (e.g., lists of notes, figures, tables, etc.).
        /// </param>
        public Epub2NcxNavigationList(string? id, string? @class, List<Epub2NcxNavigationLabel>? navigationLabels, List<Epub2NcxNavigationTarget>? navigationTargets)
        {
            Id = id;
            Class = @class;
            NavigationLabels = navigationLabels ?? new List<Epub2NcxNavigationLabel>();
            NavigationTargets = navigationTargets ?? new List<Epub2NcxNavigationTarget>();
        }

        /// <summary>
        /// <para>Gets the optional unique identifier of the content pointer.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets an optional description of the type of objects contained in the navigation list (e.g., 'note').</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string? Class { get; }

        /// <summary>
        /// <para>
        /// Gets a list of navigation labels providing textual description of the navigation list for the reader.
        /// A single navigation list can contain multiple labels in different languages.
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationLabel> NavigationLabels { get; }

        /// <summary>
        /// <para>
        /// Gets a list of navigation targets which represent navigable elements for the secondary navigation within the book (e.g., lists of notes, figures, tables, etc.).
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationTarget> NavigationTargets { get; }
    }
}
