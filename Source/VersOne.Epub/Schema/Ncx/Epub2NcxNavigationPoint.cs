using System;
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
        /// Initializes a new instance of the <see cref="Epub2NcxNavigationPoint" /> class with specified ID, navigation labels, and content.
        /// </summary>
        /// <param name="id">The unique identifier of the navigation point.</param>
        /// <param name="navigationLabels">A list of navigation labels providing textual description of the navigation point for the reader.</param>
        /// <param name="content">A pointer to the book's content associated with the navigation point.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="id" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="content" /> parameter is <c>null</c>.</exception>
        public Epub2NcxNavigationPoint(string id, List<Epub2NcxNavigationLabel>? navigationLabels, Epub2NcxContent content)
            : this(id, null, null, navigationLabels, content, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxNavigationPoint" /> class with specified ID, class, play order, navigation labels, content, and child navigation points.
        /// </summary>
        /// <param name="id">The unique identifier of the navigation point.</param>
        /// <param name="class">An optional description of the kind of structural unit the navigation point represents (e.g., 'chapter', 'section').</param>
        /// <param name="playOrder">An optional positive integer denoting the location of the content of the navigation point in the reading order.</param>
        /// <param name="navigationLabels">A list of navigation labels providing textual description of the navigation point for the reader.</param>
        /// <param name="content">A pointer to the book's content associated with the navigation point.</param>
        /// <param name="childNavigationPoints">A list of child navigation points constituting the nested navigational hierarchy within the navigation point.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="id" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="content" /> parameter is <c>null</c>.</exception>
        public Epub2NcxNavigationPoint(string id, string? @class, string? playOrder, List<Epub2NcxNavigationLabel>? navigationLabels, Epub2NcxContent content,
            List<Epub2NcxNavigationPoint>? childNavigationPoints)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Class = @class;
            PlayOrder = playOrder;
            NavigationLabels = navigationLabels ?? new List<Epub2NcxNavigationLabel>();
            Content = content ?? throw new ArgumentNullException(nameof(content));
            ChildNavigationPoints = childNavigationPoints ?? new List<Epub2NcxNavigationPoint>();
        }

        /// <summary>
        /// <para>Gets the unique identifier of the navigation point.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// <para>Gets an optional description of the kind of structural unit the navigation point represents (e.g., 'chapter', 'section').</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string? Class { get; }

        /// <summary>
        /// <para>Gets an optional positive integer denoting the location of the content of the navigation point in the reading order.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string? PlayOrder { get; }

        /// <summary>
        /// <para>
        /// Gets a list of navigation labels providing textual description of the navigation point for the reader.
        /// It generally contains the heading of the referenced section of the book.
        /// A single navigation point can contain multiple labels in different languages.
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationLabel> NavigationLabels { get; }

        /// <summary>
        /// <para>Gets a pointer to the book's content associated with the navigation point.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public Epub2NcxContent Content { get; }

        /// <summary>
        /// <para>Gets a list of child navigation points constituting the nested navigational hierarchy within the navigation point.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationPoint> ChildNavigationPoints { get; }

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
