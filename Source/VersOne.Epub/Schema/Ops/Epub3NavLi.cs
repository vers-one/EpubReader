namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Navigation element which represents a heading, structure or other item of interest for navigation purposes within <see cref="Epub3NavOl" />
    /// (represented by the &lt;li&gt; HTML tag in the navigation document).
    /// </para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
    /// </summary>
    public class Epub3NavLi
    {
        /// <summary>
        /// <para>
        /// Gets the navigation link associated with the navigation element (represented by the &lt;a&gt; HTML tag in the navigation document)
        /// or <c>null</c> if the navigation element doesn't contain a link.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public Epub3NavAnchor Anchor { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets the navigation header associated with the navigation element (represented by the &lt;span&gt; HTML tag in the navigation document)
        /// or <c>null</c> if the navigation element doesn't contain a header.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public Epub3NavSpan Span { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets the navigation sub-level within the navigation element (represented by the &lt;ol&gt; HTML tag in the navigation document)
        /// or <c>null</c> if the navigation element doesn't contain a sub-level.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public Epub3NavOl ChildOl { get; internal set; }
    }
}
