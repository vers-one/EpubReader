namespace VersOne.Epub
{
    /// <summary>
    /// The location of a single EPUB content file (e.g. a chapter or an image).
    /// </summary>
    public enum EpubContentLocation
    {
        /// <summary>
        /// Content file is located inside the EPUB file.
        /// </summary>
        LOCAL = 1,

        /// <summary>
        /// Content file is located outside the EPUB file and available via an absolute URI.
        /// </summary>
        REMOTE
    }
}
