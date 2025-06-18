namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Navigation element which represents a heading, structure or other item of interest for navigation purposes within <see cref="Epub3NavOl" />
    /// (represented by the &lt;li&gt; HTML tag in the navigation document).
    /// </para>
    /// <para>See <see href="https://www.w3.org/TR/epub/#sec-nav-def-model" /> for more information.</para>
    /// </summary>
    public class Epub3NavLi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub3NavLi" /> class.
        /// </summary>
        /// <param name="anchor">
        /// The navigation link associated with the navigation element (represented by the &lt;a&gt; HTML tag in the navigation document)
        /// or <c>null</c> if the navigation element doesn't contain a link.
        /// </param>
        /// <param name="span">
        /// The navigation header associated with the navigation element (represented by the &lt;span&gt; HTML tag in the navigation document)
        /// or <c>null</c> if the navigation element doesn't contain a header.
        /// </param>
        /// <param name="childOl">
        /// The navigation sub-level within the navigation element (represented by the &lt;ol&gt; HTML tag in the navigation document)
        /// or <c>null</c> if the navigation element doesn't contain a sub-level.
        /// </param>
        public Epub3NavLi(Epub3NavAnchor? anchor = null, Epub3NavSpan? span = null, Epub3NavOl? childOl = null)
        {
            Anchor = anchor;
            Span = span;
            ChildOl = childOl;
        }

        /// <summary>
        /// <para>
        /// Gets the navigation link associated with the navigation element (represented by the &lt;a&gt; HTML tag in the navigation document)
        /// or <c>null</c> if the navigation element doesn't contain a link.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#sec-nav-def-model" /> for more information.</para>
        /// </summary>
        public Epub3NavAnchor? Anchor { get; }

        /// <summary>
        /// <para>
        /// Gets the navigation header associated with the navigation element (represented by the &lt;span&gt; HTML tag in the navigation document)
        /// or <c>null</c> if the navigation element doesn't contain a header.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#sec-nav-def-model" /> for more information.</para>
        /// </summary>
        public Epub3NavSpan? Span { get; }

        /// <summary>
        /// <para>
        /// Gets the navigation sub-level within the navigation element (represented by the &lt;ol&gt; HTML tag in the navigation document)
        /// or <c>null</c> if the navigation element doesn't contain a sub-level.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#sec-nav-def-model" /> for more information.</para>
        /// </summary>
        public Epub3NavOl? ChildOl { get; }
    }
}
