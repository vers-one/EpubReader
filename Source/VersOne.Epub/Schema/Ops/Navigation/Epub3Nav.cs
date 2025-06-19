﻿using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Navigation section in the <see cref="Epub3NavDocument" />.</para>
    /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-nav-def-model" /> for more information.</para>
    /// </summary>
    public class Epub3Nav
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub3Nav" /> class with specified type and navigation level.
        /// </summary>
        /// <param name="type">The type of the navigation section or <c>null</c> if the type is not specified.</param>
        /// <param name="ol">The top navigation level of the navigation section (represented by the &lt;ol&gt; HTML tag in the navigation document).</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ol" /> parameter is <c>null</c>.</exception>
        public Epub3Nav(Epub3StructuralSemanticsProperty? type, Epub3NavOl ol)
            : this(type, false, null, ol)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Epub3Nav" /> class with specified type, hidden flag, head, and navigation level.
        /// </summary>
        /// <param name="type">The type of the navigation section or <c>null</c> if the type is not specified.</param>
        /// <param name="isHidden">A value indicating whether the navigation section should be hidden from the reader or not.</param>
        /// <param name="head">The header of the navigation section or <c>null</c> if the header is not present.</param>
        /// <param name="ol">The top navigation level of the navigation section (represented by the &lt;ol&gt; HTML tag in the navigation document).</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ol" /> parameter is <c>null</c>.</exception>
        public Epub3Nav(Epub3StructuralSemanticsProperty? type, bool isHidden, string? head, Epub3NavOl ol)
        {
            Type = type;
            IsHidden = isHidden;
            Head = head;
            Ol = ol ?? throw new ArgumentNullException(nameof(ol));
        }

        /// <summary>
        /// <para>Gets the type of the navigation section or <c>null</c> if the type is not specified.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-nav-def-types" /> for more information.</para>
        /// </summary>
        public Epub3StructuralSemanticsProperty? Type { get; }

        /// <summary>
        /// <para>Gets a value indicating whether the navigation section should be hidden from the reader or not.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-nav-doc-use-spine" /> for more information.</para>
        /// </summary>
        public bool IsHidden { get; }

        /// <summary>
        /// <para>Gets the header of the navigation section or <c>null</c> if the header is not present.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-nav-def-model" /> for more information.</para>
        /// </summary>
        public string? Head { get; }

        /// <summary>
        /// <para>Gets the top navigation level of the navigation section (represented by the &lt;ol&gt; HTML tag in the navigation document).</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-nav-def-model" /> for more information.</para>
        /// </summary>
        public Epub3NavOl Ol { get; }
    }
}
