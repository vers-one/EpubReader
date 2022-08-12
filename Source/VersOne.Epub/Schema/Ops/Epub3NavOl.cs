using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Top navigation level or a sub-level in the <see cref="Epub3Nav" /> (represented by the &lt;ol&gt; HTML tag in the navigation document).</para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
    /// </summary>
    public class Epub3NavOl
    {
        /// <summary>
        /// <para>Gets a value indicating whether the navigation level should be hidden from the reader or not.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-hidden" /> for more information.</para>
        /// </summary>
        public bool IsHidden { get; internal set; }

        /// <summary>
        /// <para>Gets a list of headings, structures or other items of interest for navigation purposes (represented by the &lt;li&gt; HTML tag in the navigation document).</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public List<Epub3NavLi> Lis { get; internal set; }
    }
}
