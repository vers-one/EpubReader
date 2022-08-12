namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Navigation header associated with a <see cref="Epub3NavLi" /> (represented by the &lt;span&gt; HTML tag in the navigation document).</para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
    /// </summary>
    public class Epub3NavSpan
    {
        /// <summary>
        /// <para>Gets the textual content of the navigation header (represented by the inner content of the &lt;span&gt; HTML tag in the navigation document).</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets the alternative title of the navigation header (represented by the 'title' HTML attribute in the navigation document)
        /// or <c>null</c> if the alternative title is not specified.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets the alternative text of the navigation header (represented by the 'title' HTML attribute in the navigation document)
        /// or <c>null</c> if the alternative text is not specified.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string Alt { get; internal set; }
    }
}
