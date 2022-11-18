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
        /// Initializes a new instance of the <see cref="Epub2NcxPageTarget" /> class with specified type, navigation labels, and content.
        /// </summary>
        /// <param name="type">The type of the page represented by the page target.</param>
        /// <param name="navigationLabels">A list of navigation labels providing textual description of the page target for the reader.</param>
        /// <param name="content">A pointer to the book's content associated with the page target or <c>null</c> if no such pointer is specified.</param>
        public Epub2NcxPageTarget(Epub2NcxPageTargetType type, List<Epub2NcxNavigationLabel>? navigationLabels, Epub2NcxContent? content)
            : this(null, null, type, null, null, navigationLabels, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxPageTarget" /> class with specified ID, value, type, class, play order, navigation labels, and content.
        /// </summary>
        /// <param name="id">The optional unique identifier of the page target.</param>
        /// <param name="value">A positive integer representing the numeric value associated with a page or <c>null</c> if no numeric value is provided.</param>
        /// <param name="type">The type of the page represented by the page target.</param>
        /// <param name="class">An optional description of the kind of the page represented by the page target.</param>
        /// <param name="playOrder">An optional positive integer denoting the location of the page content in the reading order.</param>
        /// <param name="navigationLabels">A list of navigation labels providing textual description of the page target for the reader.</param>
        /// <param name="content">A pointer to the book's content associated with the page target or <c>null</c> if no such pointer is specified.</param>
        public Epub2NcxPageTarget(string? id, string? value, Epub2NcxPageTargetType type, string? @class, string? playOrder,
            List<Epub2NcxNavigationLabel>? navigationLabels, Epub2NcxContent? content)
        {
            Id = id;
            Value = value;
            Type = type;
            Class = @class;
            PlayOrder = playOrder;
            NavigationLabels = navigationLabels ?? new List<Epub2NcxNavigationLabel>();
            Content = content;
        }

        /// <summary>
        /// <para>Gets the optional unique identifier of the page target.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets a positive integer representing the numeric value associated with a page or <c>null</c> if no numeric value is provided.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string? Value { get; }

        /// <summary>
        /// <para>Gets the type of the page represented by the page target.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public Epub2NcxPageTargetType Type { get; }

        /// <summary>
        /// <para>Gets an optional description of the kind of the page represented by the page target.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string? Class { get; }

        /// <summary>
        /// <para>Gets an optional positive integer denoting the location of the page content in the reading order.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string? PlayOrder { get; }

        /// <summary>
        /// <para>
        /// Gets a list of navigation labels providing textual description of the page target for the reader.
        /// A single page target can contain multiple labels in different languages.
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationLabel> NavigationLabels { get; }

        /// <summary>
        /// <para>Gets a pointer to the book's content associated with the page target or <c>null</c> if no such pointer is specified.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public Epub2NcxContent? Content { get; }
    }
}
