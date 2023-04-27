namespace VersOne.Epub
{
    /// <summary>
    /// A container for all content files within the EPUB book.
    /// </summary>
    public class EpubContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContent" /> class.
        /// </summary>
        /// <param name="cover">Content file for the cover image of the EPUB book or <c>null</c> if the book doesn't have a cover.</param>
        /// <param name="navigationHtmlFile">EPUB 3 navigation document of the EPUB book or <c>null</c> if the book doesn't have a EPUB 3 navigation document.</param>
        /// <param name="html">All HTML/XHTML content files of the EPUB book.</param>
        /// <param name="css">All CSS files of the EPUB book.</param>
        /// <param name="images">All image files of the EPUB book.</param>
        /// <param name="fonts">All embedded font files of the EPUB book.</param>
        /// <param name="allFiles">All content files of the EPUB book.</param>
        public EpubContent(EpubLocalByteContentFile? cover = null, EpubLocalTextContentFile? navigationHtmlFile = null,
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>? html = null,
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>? css = null,
            EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>? images = null,
            EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>? fonts = null,
            EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile>? allFiles = null)
        {
            Cover = cover;
            NavigationHtmlFile = navigationHtmlFile;
            Html = html ?? new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>();
            Css = css ?? new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>();
            Images = images ?? new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>();
            Fonts = fonts ?? new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>();
            AllFiles = allFiles ?? new EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile>();
        }

        /// <summary>
        /// Gets the content file for the cover image of the EPUB book or <c>null</c> if the book doesn't have a cover.
        /// </summary>
        public EpubLocalByteContentFile? Cover { get; }

        /// <summary>
        /// Gets the EPUB 3 navigation document of the EPUB book or <c>null</c> if the book doesn't have a EPUB 3 navigation document.
        /// </summary>
        public EpubLocalTextContentFile? NavigationHtmlFile { get; }

        /// <summary>
        /// Gets all HTML/XHTML content files of the EPUB book.
        /// </summary>
        public EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> Html { get; }

        /// <summary>
        /// Gets all CSS files of the EPUB book.
        /// </summary>
        public EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> Css { get; }

        /// <summary>
        /// Gets all image files of the EPUB book.
        /// </summary>
        public EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile> Images { get; }

        /// <summary>
        /// Gets all embedded font files of the EPUB book.
        /// </summary>
        public EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile> Fonts { get; }

        /// <summary>
        /// Gets all content files of the EPUB book.
        /// </summary>
        public EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile> AllFiles { get; }
    }
}
