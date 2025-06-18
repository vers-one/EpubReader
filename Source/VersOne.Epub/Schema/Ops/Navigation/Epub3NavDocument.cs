using System;
using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Parsed content of the EPUB 3 navigation document of the EPUB book.
    /// Navigation document includes human- and machine-readable content that facilitates navigation through the book.
    /// </para>
    /// <para>See <see href="https://www.w3.org/TR/epub/#sec-nav" /> for more information.</para>
    /// </summary>
    public class Epub3NavDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub3NavDocument" /> class.
        /// </summary>
        /// <param name="filePath">The absolute path of the EPUB 3 navigation document file in the EPUB archive.</param>
        /// <param name="navs">A list of navigation sections in the EPUB 3 navigation document.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="filePath" /> parameter is <c>null</c>.</exception>
        public Epub3NavDocument(string filePath, List<Epub3Nav>? navs = null)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            Navs = navs ?? new List<Epub3Nav>();
        }

        /// <summary>
        /// Gets the absolute path of the EPUB 3 navigation document file in the EPUB archive.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// <para>Gets a list of navigation sections in the EPUB 3 navigation document.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#sec-nav" /> for more information.</para>
        /// </summary>
        public List<Epub3Nav> Navs { get; }
    }
}
