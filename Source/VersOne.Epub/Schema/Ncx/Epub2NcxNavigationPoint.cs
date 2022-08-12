using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// NCX navigation point which contains a description of the target and a pointer to content.
    /// It is used for the primary navigation within the book (e.g., for the table of contents).
    /// </para>
    /// <para>
    /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
    /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
    /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
    /// </para>
    /// </summary>
    public class Epub2NcxNavigationPoint
    {
        /// <summary>
        /// <para>Gets the unique identifier of the navigation point.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>Gets an optional description of the kind of structural unit the navigation point represents (e.g., 'chapter', 'section').</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Class { get; internal set; }

        /// <summary>
        /// <para>Gets an optional positive integer denoting the location of the content of the navigation point in the reading order.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string PlayOrder { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets a list of navigation labels providing textual description of the navigation point for the reader.
        /// It generally contains the heading of the referenced section of the book.
        /// A single navigation point can contain multiple labels in different languages.
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationLabel> NavigationLabels { get; internal set; }

        /// <summary>
        /// <para>Gets a pointer to the book's content associated with the navigation point.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public Epub2NcxContent Content { get; internal set; }

        /// <summary>
        /// <para>Gets a list of child navigation points constituting the nested navigational hierarchy within the navigation point.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationPoint> ChildNavigationPoints { get; internal set; }

        /// <summary>
        /// Returns a string containing the values of the <see cref="Id" /> and <see cref="Epub2NcxContent.Source" /> properties for debugging purposes.
        /// </summary>
        /// <returns>A string containing the values of the <see cref="Id" /> and <see cref="Epub2NcxContent.Source" /> properties.</returns>
        public override string ToString()
        {
            return $"Id: {Id}, Content.Source: {Content.Source}";
        }
    }
}
