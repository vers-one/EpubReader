namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Navigation link associated with a <see cref="Epub3NavLi" /> (represented by the &lt;a&gt; HTML tag in the navigation document).</para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
    /// </summary>
    public class Epub3NavAnchor
    {
        /// <summary>
        /// <para>Gets an IRI that resolves to a content document or a fragment within the EPUB book (represented by the 'href' HTML attribute in the navigation document).</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string Href { get; internal set; }

        /// <summary>
        /// <para>Gets the textual content of the navigation link (represented by the inner content of the &lt;a&gt; HTML tag in the navigation document).</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets the alternative title of the navigation link (represented by the 'title' HTML attribute in the navigation document)
        /// or <c>null</c> if the alternative title is not specified.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets the alternative text of the navigation link (represented by the 'title' HTML attribute in the navigation document)
        /// or <c>null</c> if the alternative text is not specified.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string Alt { get; internal set; }

        /// <summary>
        /// <para>Gets the type of the structural semantics of the navigation link (represented by the 'epub:type' HTML attribute in the navigation document).</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-nav-landmarks" /> for more information.</para>
        /// </summary>
        public Epub3NavStructuralSemanticsProperty? Type { get; internal set; }
    }
}
