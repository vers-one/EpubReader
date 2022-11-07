namespace VersOne.Epub
{
    /// <summary>
    /// A file within the EPUB archive with its content represented as a byte array. It is used for images and font files.
    /// Unlike <see cref="EpubLocalByteContentFileRef" />, this class contains the whole content of the file.
    /// </summary>
    public class EpubLocalByteContentFile : EpubLocalContentFile
    {
        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        public byte[] Content { get; internal set; }

        /// <summary>
        /// Gets the type of the content file which is always <see cref="EpubContentFileType.BYTE_ARRAY" /> for local byte content files.
        /// </summary>
        public override EpubContentFileType ContentFileType => EpubContentFileType.BYTE_ARRAY;
    }
}
