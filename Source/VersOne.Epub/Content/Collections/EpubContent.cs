namespace VersOne.Epub
{
    /// <summary>
    /// A container for all content files within the EPUB book.
    /// </summary>
    public class EpubContent
    {
        /// <summary>
        /// Gets the content file for the cover image of the EPUB book or <c>null</c> if the book doesn't have a cover.
        /// </summary>
        public EpubLocalByteContentFile Cover { get; internal set; }

        /// <summary>
        /// Gets the EPUB 3 navigation document of the EPUB book or <c>null</c> if the book doesn't have a EPUB 3 navigation document.
        /// </summary>
        public EpubLocalTextContentFile NavigationHtmlFile { get; internal set; }

        /// <summary>
        /// Gets all HTML/XHTML content files of the EPUB book.
        /// </summary>
        public EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> Html { get; internal set; }

        /// <summary>
        /// Gets all CSS files of the EPUB book.
        /// </summary>
        public EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> Css { get; internal set; }

        /// <summary>
        /// Gets all image files of the EPUB book.
        /// </summary>
        public EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile> Images { get; internal set; }

        /// <summary>
        /// Gets all embedded font files of the EPUB book.
        /// </summary>
        public EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile> Fonts { get; internal set; }

        /// <summary>
        /// Gets all content files of the EPUB book.
        /// </summary>
        public EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile> AllFiles { get; internal set; }
    }
}
