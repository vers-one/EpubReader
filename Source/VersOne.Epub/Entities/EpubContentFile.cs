namespace VersOne.Epub
{
    /// <summary>
    /// The base class for content files within the EPUB archive (e.g., HTML files or images).
    /// Unlike <see cref="EpubContentFileRef" />, the classes derived from this base class contain the whole content of the file.
    /// </summary>
    public abstract class EpubContentFile
    {
        /// <summary>
        /// Gets the relative file path of the content file (as it is specified in the EPUB manifest).
        /// Returns <c>null</c> if <see cref="ContentLocation" /> is <see cref="EpubContentLocation.REMOTE" />.
        /// </summary>
        public string FileName { get; internal set; }

        /// <summary>
        /// Gets the absolute file path of the content file in the EPUB archive.
        /// Returns <c>null</c> if <see cref="ContentLocation" /> is <see cref="EpubContentLocation.REMOTE" />.
        /// </summary>
        public string FilePathInEpubArchive { get; internal set; }

        /// <summary>
        /// Gets the absolute URI of the content item (as it is specified in the EPUB manifest).
        /// Returns <c>null</c> if <see cref="ContentLocation" /> is <see cref="EpubContentLocation.LOCAL" />.
        /// </summary>
        public string Href { get; internal set; }

        /// <summary>
        /// Gets the location of the content item (local or remote).
        /// </summary>
        public EpubContentLocation ContentLocation { get; internal set; }

        /// <summary>
        /// Gets the type of the content of the file.
        /// </summary>
        public EpubContentType ContentType { get; internal set; }

        /// <summary>
        /// Gets the MIME type of the content of the file.
        /// </summary>
        public string ContentMimeType { get; internal set; }
    }
}
