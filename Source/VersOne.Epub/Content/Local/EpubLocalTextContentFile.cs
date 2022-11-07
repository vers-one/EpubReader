namespace VersOne.Epub
{
    /// <summary>
    /// A file within the EPUB archive with its content represented as a string. It is used mainly for HTML and CSS files.
    /// Unlike <see cref="EpubLocalTextContentFileRef" />, this class contains the whole content of the file.
    /// </summary>
    public class EpubLocalTextContentFile : EpubLocalContentFile
    {
        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        public string Content { get; internal set; }

        /// <summary>
        /// Gets the type of the content file which is always <see cref="EpubContentFileType.TEXT" /> for local text content files.
        /// </summary>
        public override EpubContentFileType ContentFileType => EpubContentFileType.TEXT;
    }
}
