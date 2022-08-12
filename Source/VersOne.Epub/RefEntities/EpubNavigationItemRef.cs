using System.Collections.Generic;

namespace VersOne.Epub
{
    /// <summary>
    /// A single navigation element of the book (typically within the table of contents).
    /// Depending on the value of the <see cref="Type" /> property, it can act either as a header or as a navigation link.
    /// Unlike <see cref="EpubNavigationItem" />, this class contains the <see cref="HtmlContentFileRef" /> property
    /// which (if it's not <c>null</c>) holds only the reference to the file but doesn't have its content.
    /// </summary>
    public class EpubNavigationItemRef
    {
        private EpubNavigationItemRef(EpubNavigationItemType type)
        {
            Type = type;
        }

        /// <summary>
        /// Gets the value determining whether this navigation item acts as a header or as a navigation link.
        /// </summary>
        public EpubNavigationItemType Type { get; }

        /// <summary>
        /// Gets the title of the navigation element (which is either the text of the header or the title of the navigation link).
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// Gets the link of the navigation element if it acts as a navigation link or <c>null</c> if it acts as a header.
        /// </summary>
        public EpubNavigationItemLink Link { get; internal set; }

        /// <summary>
        /// Gets the EPUB content file reference for the navigation element or <c>null</c> of the value of the <see cref="Link" /> property is <c>null</c>.
        /// </summary>
        public EpubTextContentFileRef HtmlContentFileRef { get; internal set; }

        /// <summary>
        /// <para>Gets a list of child navigation elements constituting the nested navigational hierarchy within the navigation element.</para>
        /// </summary>
        public List<EpubNavigationItemRef> NestedItems { get; internal set; }

        /// <summary>
        /// Creates a new instance of <see cref="EpubNavigationItemRef" /> class with its <see cref="Type" /> property set to <see cref="EpubNavigationItemType.HEADER" />.
        /// </summary>
        /// <returns>A new instance of <see cref="EpubNavigationItemRef" /> class.</returns>
        public static EpubNavigationItemRef CreateAsHeader()
        {
            return new EpubNavigationItemRef(EpubNavigationItemType.HEADER);
        }

        /// <summary>
        /// Creates a new instance of <see cref="EpubNavigationItemRef" /> class with its <see cref="Type" /> property set to <see cref="EpubNavigationItemType.LINK" />.
        /// </summary>
        /// <returns>A new instance of <see cref="EpubNavigationItemRef" /> class.</returns>
        public static EpubNavigationItemRef CreateAsLink()
        {
            return new EpubNavigationItemRef(EpubNavigationItemType.LINK);
        }

        /// <summary>
        /// Returns a string containing the values of the <see cref="Type" /> and <see cref="Title" /> properties
        /// and the number of the elements in the <see cref="NestedItems" /> property for debugging purposes.
        /// </summary>
        /// <returns>
        /// A string containing the values of the <see cref="Type" /> and <see cref="Title" /> properties
        /// and the number of the elements in the <see cref="NestedItems" /> property.
        /// </returns>
        public override string ToString()
        {
            return $"Type: {Type}, Title: {Title}, NestedItems.Count: {NestedItems.Count}";
        }
    }
}
