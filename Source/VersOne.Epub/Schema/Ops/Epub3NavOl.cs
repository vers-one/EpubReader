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
        /// Initializes a new instance of the <see cref="Epub3NavOl" /> class with the specified list of navigation items.
        /// </summary>
        /// <param name="lis">
        /// A list of headings, structures or other items of interest for navigation purposes (represented by the &lt;li&gt; HTML tag in the navigation document).
        /// </param>
        public Epub3NavOl(List<Epub3NavLi>? lis = null)
            : this(false, lis)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Epub3NavOl" /> class with specified hidden flag and the list of navigation items.
        /// </summary>
        /// <param name="isHidden">A value indicating whether the navigation level should be hidden from the reader or not.</param>
        /// <param name="lis">
        /// A list of headings, structures or other items of interest for navigation purposes (represented by the &lt;li&gt; HTML tag in the navigation document).
        /// </param>
        public Epub3NavOl(bool isHidden, List<Epub3NavLi>? lis)
        {
            IsHidden = isHidden;
            Lis = lis ?? new List<Epub3NavLi>();
        }

        /// <summary>
        /// <para>Gets a value indicating whether the navigation level should be hidden from the reader or not.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-hidden" /> for more information.</para>
        /// </summary>
        public bool IsHidden { get; }

        /// <summary>
        /// <para>Gets a list of headings, structures or other items of interest for navigation purposes (represented by the &lt;li&gt; HTML tag in the navigation document).</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav-def-model" /> for more information.</para>
        /// </summary>
        public List<Epub3NavLi> Lis { get; }
    }
}
