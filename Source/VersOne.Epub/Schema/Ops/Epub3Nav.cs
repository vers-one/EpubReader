namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Navigation section in the <see cref="Epub3NavDocument" />.</para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
    /// </summary>
    public class Epub3Nav
    {
        /// <summary>
        /// <para>Gets the type of the navigation section.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-types" /> for more information.</para>
        /// </summary>
        public Epub3NavStructuralSemanticsProperty? Type { get; internal set; }

        /// <summary>
        /// <para>Gets a value indicating whether the navigation section should be hidden from the reader or not.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-hidden" /> for more information.</para>
        /// </summary>
        public bool IsHidden { get; internal set; }

        /// <summary>
        /// <para>Gets the header of the navigation section.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string Head { get; internal set; }

        /// <summary>
        /// <para>Gets the top navigation level of the navigation section (represented by the &lt;ol&gt; HTML tag in the navigation document).</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public Epub3NavOl Ol { get; internal set; }
    }
}
