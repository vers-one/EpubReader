namespace VersOne.Epub
{
    /// <summary>
    /// A container for all content file references within the EPUB book.
    /// </summary>
    public class EpubContentRef
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentRef" /> class.
        /// </summary>
        /// <param name="cover">The content file reference for the cover image of the EPUB book or <c>null</c> if the book doesn't have a cover.</param>
        /// <param name="navigationHtmlFile">
        /// The file reference to the EPUB 3 navigation document of the EPUB book or <c>null</c> if the book doesn't have a EPUB 3 navigation document.
        /// </param>
        /// <param name="html">
        /// All HTML/XHTML content file references of the EPUB book.
        /// </param>
        /// <param name="css">All CSS file references of the EPUB book.</param>
        /// <param name="images">All image file references of the EPUB book.</param>
        /// <param name="fonts">All embedded font file references of the EPUB book.</param>
        /// <param name="allFiles">All content file references of the EPUB book.</param>
        public EpubContentRef(EpubLocalByteContentFileRef? cover = null, EpubLocalTextContentFileRef? navigationHtmlFile = null,
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>? html = null,
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>? css = null,
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>? images = null,
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>? fonts = null,
            EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>? allFiles = null)
        {
            Cover = cover;
            NavigationHtmlFile = navigationHtmlFile;
            Html = html ?? new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>();
            Css = css ?? new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>();
            Images = images ?? new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>();
            Fonts = fonts ?? new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>();
            AllFiles = allFiles ?? new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>();
        }

        /// <summary>
        /// Gets the content file reference for the cover image of the EPUB book or <c>null</c> if the book doesn't have a cover.
        /// </summary>
        public EpubLocalByteContentFileRef? Cover { get; }

        /// <summary>
        /// Gets the file reference to the EPUB 3 navigation document of the EPUB book or <c>null</c> if the book doesn't have a EPUB 3 navigation document.
        /// </summary>
        public EpubLocalTextContentFileRef? NavigationHtmlFile { get; }

        /// <summary>
        /// Gets all HTML/XHTML content file references of the EPUB book.
        /// </summary>
        public EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> Html { get; }

        /// <summary>
        /// Gets all CSS file references of the EPUB book.
        /// </summary>
        public EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> Css { get; }

        /// <summary>
        /// Gets all image file references of the EPUB book.
        /// </summary>
        public EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> Images { get; }

        /// <summary>
        /// Gets all embedded font file references of the EPUB book.
        /// </summary>
        public EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> Fonts { get; }

        /// <summary>
        /// Gets all content file references of the EPUB book.
        /// </summary>
        public EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef> AllFiles { get; }
    }
}
