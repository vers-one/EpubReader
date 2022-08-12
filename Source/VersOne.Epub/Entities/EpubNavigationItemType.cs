namespace VersOne.Epub
{
    /// <summary>
    /// The type of the EPUB navigation item (from EPUB 2 NCX document for EPUB 2 books or from EPUB 3 NAV document for EPUB 3 books).
    /// </summary>
    public enum EpubNavigationItemType
    {
        /// <summary>
        /// A text-based header (i.e. not a navigation link).
        /// </summary>
        HEADER = 1,

        /// <summary>
        /// A navigation link.
        /// </summary>
        LINK
    }
}
