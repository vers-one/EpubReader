namespace VersOne.Epub
{
    /// <summary>
    /// A container for all content file references within the EPUB book.
    /// </summary>
    public class EpubContentRef
    {
        /// <summary>
        /// Gets the content file reference for the cover image of the EPUB book or <c>null</c> if the book doesn't have a cover.
        /// </summary>
        public EpubLocalByteContentFileRef Cover { get; internal set; }

        /// <summary>
        /// Gets the file reference to the EPUB 3 navigation document of the EPUB book or <c>null</c> if the book doesn't have a EPUB 3 navigation document.
        /// </summary>
        public EpubLocalTextContentFileRef NavigationHtmlFile { get; internal set; }

        /// <summary>
        /// Gets all HTML/XHTML content file references of the EPUB book.
        /// </summary>
        public EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> Html { get; internal set; }

        /// <summary>
        /// Gets all CSS file references of the EPUB book.
        /// </summary>
        public EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> Css { get; internal set; }

        /// <summary>
        /// Gets all image file references of the EPUB book.
        /// </summary>
        public EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> Images { get; internal set; }

        /// <summary>
        /// Gets all embedded font file references of the EPUB book.
        /// </summary>
        public EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> Fonts { get; internal set; }

        /// <summary>
        /// Gets all content file references of the EPUB book.
        /// </summary>
        public EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef> AllFiles { get; internal set; }
    }
}
