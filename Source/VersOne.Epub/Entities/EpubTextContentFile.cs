namespace VersOne.Epub
{
    /// <summary>
    /// A file within the EPUB archive with its content represented as a string. It is used mainly for HTML and CSS files.
    /// Unlike <see cref="EpubTextContentFileRef" />, this class contains the whole content of the file.
    /// </summary>
    public class EpubTextContentFile : EpubContentFile
    {
        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        public string Content { get; internal set; }
    }
}
