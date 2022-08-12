using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Parsed content of the EPUB 3 navigation document of the EPUB book.
    /// Navigation document includes human- and machine-readable content that facilitates navigation through the book.
    /// </para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav" /> for more information.</para>
    /// </summary>
    public class Epub3NavDocument
    {
        /// <summary>
        /// <para>Gets a list of navigation sections in the EPUB 3 navigation document.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav" /> for more information.</para>
        /// </summary>
        public List<Epub3Nav> Navs { get; internal set; }
    }
}
