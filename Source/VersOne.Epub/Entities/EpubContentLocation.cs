namespace VersOne.Epub
{
    /// <summary>
    /// The location of a single EPUB content item (e.g. a chapter or an image).
    /// </summary>
    public enum EpubContentLocation
    {
        /// <summary>
        /// Content item is located inside the EPUB file.
        /// </summary>
        LOCAL = 1,

        /// <summary>
        /// Content item is located outside the EPUB file and available via absolute URI.
        /// </summary>
        REMOTE
    }
}
