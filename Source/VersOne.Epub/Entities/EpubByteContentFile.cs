namespace VersOne.Epub
{
    /// <summary>
    /// A file within the EPUB archive with its content represented as a byte array. It is used for images and font files.
    /// Unlike <see cref="EpubByteContentFileRef" />, this class contains the whole content of the file.
    /// </summary>
    public class EpubByteContentFile : EpubContentFile
    {
        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        public byte[] Content { get; internal set; }
    }
}
