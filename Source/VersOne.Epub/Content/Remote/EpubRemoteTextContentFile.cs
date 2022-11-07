namespace VersOne.Epub
{
    /// <summary>
    /// A file located outside of the EPUB archive with its content represented as a string. It is used mainly for HTML and CSS files.
    /// Unlike <see cref="EpubRemoteTextContentFileRef" />, this class contains the whole content of the file.
    /// </summary>
    public class EpubRemoteTextContentFile : EpubRemoteContentFile
    {
        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        public string Content { get; internal set; }

        /// <summary>
        /// Gets the type of the content file which is always <see cref="EpubContentFileType.TEXT" /> for remote text content files.
        /// </summary>
        public override EpubContentFileType ContentFileType => EpubContentFileType.TEXT;
    }
}
