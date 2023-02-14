using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Navigation link associated with a <see cref="Epub3NavLi" /> (represented by the &lt;a&gt; HTML tag in the navigation document).</para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
    /// </summary>
    public class Epub3NavAnchor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub3NavAnchor" /> class.
        /// </summary>
        /// <param name="href">
        /// An IRI that resolves to a content document or a fragment within the EPUB book (represented by the 'href' HTML attribute in the navigation document)
        /// or <c>null</c> if the anchor doesn't contain an IRI.
        /// </param>
        /// <param name="text">The textual content of the navigation link (represented by the inner content of the &lt;a&gt; HTML tag in the navigation document).</param>
        /// <param name="title">
        /// The alternative title of the navigation link (represented by the 'title' HTML attribute in the navigation document)
        /// or <c>null</c> if the alternative title is not specified.
        /// </param>
        /// <param name="alt">
        /// The alternative text of the navigation link (represented by the 'title' HTML attribute in the navigation document)
        /// or <c>null</c> if the alternative text is not specified.
        /// </param>
        /// <param name="type">
        /// The type of the structural semantics of the navigation link (represented by the 'epub:type' HTML attribute in the navigation document)
        /// or <c>null</c> if the anchor doesn't specify a type.
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="text" /> parameter is <c>null</c>.</exception>
        public Epub3NavAnchor(string? href, string text, string? title = null, string? alt = null, Epub3StructuralSemanticsProperty? type = null)
        {
            Href = href;
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Title = title;
            Alt = alt;
            Type = type;
        }

        /// <summary>
        /// <para>
        /// Gets an IRI that resolves to a content document or a fragment within the EPUB book (represented by the 'href' HTML attribute in the navigation document)
        /// or <c>null</c> if the anchor doesn't contain an IRI.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string? Href { get; }

        /// <summary>
        /// <para>Gets the textual content of the navigation link (represented by the inner content of the &lt;a&gt; HTML tag in the navigation document).</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// <para>
        /// Gets the alternative title of the navigation link (represented by the 'title' HTML attribute in the navigation document)
        /// or <c>null</c> if the alternative title is not specified.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string? Title { get; }

        /// <summary>
        /// <para>
        /// Gets the alternative text of the navigation link (represented by the 'title' HTML attribute in the navigation document)
        /// or <c>null</c> if the alternative text is not specified.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public string? Alt { get; }

        /// <summary>
        /// <para>
        /// Gets the type of the structural semantics of the navigation link (represented by the 'epub:type' HTML attribute in the navigation document)
        /// or <c>null</c> if the anchor doesn't specify a type.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-nav-landmarks" /> for more information.</para>
        /// </summary>
        public Epub3StructuralSemanticsProperty? Type { get; }
    }
}
