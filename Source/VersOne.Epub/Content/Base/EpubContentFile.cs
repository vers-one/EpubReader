using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    /// <summary>
    /// The base class for content files and content file references declared in the EPUB manifest (e.g., HTML files or images).
    /// </summary>
    public abstract class EpubContentFile
    {
        /// <summary>
        /// Gets the content file key as it is declared in the EPUB manifest (in the <see cref="EpubManifestItem.Href" /> property).
        /// This is either a relative path of the content file within the EPUB archive (for local content files)
        /// or an absolute URI of the content file outside of the EPUB archive (for remote content files).
        /// </summary>
        public string Key { get; internal set; }

        /// <summary>
        /// Gets the type of the content of the file (e.g. <see cref="EpubContentType.XHTML_1_1" /> or <see cref="EpubContentType.IMAGE_JPEG" />).
        /// </summary>
        public EpubContentType ContentType { get; internal set; }

        /// <summary>
        /// Gets the MIME type of the content of the file.
        /// </summary>
        public string ContentMimeType { get; internal set; }

        /// <summary>
        /// Gets the location of the content file (local or remote).
        /// </summary>
        public abstract EpubContentLocation ContentLocation { get; }

        /// <summary>
        /// Gets the type of the content file (text or byte array).
        /// </summary>
        public abstract EpubContentFileType ContentFileType { get; }
    }
}
