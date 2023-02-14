using System;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubNavigationItemRef" /> class with specified type, title, and nested items.
        /// </summary>
        /// <param name="type">The value determining whether this navigation item acts as a header or as a navigation link.</param>
        /// <param name="title">The title of the navigation element (which is either the text of the header or the title of the navigation link).</param>
        /// <param name="nestedItems">A list of child navigation elements constituting the nested navigational hierarchy within the navigation element.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="title" /> parameter is <c>null</c>.</exception>
        public EpubNavigationItemRef(EpubNavigationItemType type, string title, List<EpubNavigationItemRef>? nestedItems = null)
            : this(type, title, null, null, nestedItems)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubNavigationItemRef" /> class with specified type, title, link, HTML content file reference, and nested items.
        /// </summary>
        /// <param name="type">The value determining whether this navigation item acts as a header or as a navigation link.</param>
        /// <param name="title">The title of the navigation element (which is either the text of the header or the title of the navigation link).</param>
        /// <param name="link">The link of the navigation element if it acts as a navigation link or <c>null</c> if it acts as a header.</param>
        /// <param name="htmlContentFileRef">
        /// The EPUB content file reference for the navigation element or <c>null</c> if the value of the <paramref name="link" /> parameter is <c>null</c>.
        /// </param>
        /// <param name="nestedItems">A list of child navigation elements constituting the nested navigational hierarchy within the navigation element.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="title" /> parameter is <c>null</c>.</exception>
        public EpubNavigationItemRef(EpubNavigationItemType type, string title, EpubNavigationItemLink? link, EpubLocalTextContentFileRef? htmlContentFileRef,
            List<EpubNavigationItemRef>? nestedItems = null)
        {
            Type = type;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Link = link;
            HtmlContentFileRef = htmlContentFileRef;
            NestedItems = nestedItems ?? new List<EpubNavigationItemRef>();
        }

        /// <summary>
        /// Gets the value determining whether this navigation item acts as a header or as a navigation link.
        /// </summary>
        public EpubNavigationItemType Type { get;  }

        /// <summary>
        /// Gets the title of the navigation element (which is either the text of the header or the title of the navigation link).
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the link of the navigation element if it acts as a navigation link or <c>null</c> if it acts as a header.
        /// </summary>
        public EpubNavigationItemLink? Link { get; }

        /// <summary>
        /// Gets the EPUB content file reference for the navigation element or <c>null</c> if the value of the <see cref="Link" /> property is <c>null</c>.
        /// </summary>
        public EpubLocalTextContentFileRef? HtmlContentFileRef { get; }

        /// <summary>
        /// <para>Gets a list of child navigation elements constituting the nested navigational hierarchy within the navigation element.</para>
        /// </summary>
        public List<EpubNavigationItemRef> NestedItems { get; }

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
