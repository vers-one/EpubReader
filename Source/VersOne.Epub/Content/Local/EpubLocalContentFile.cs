namespace VersOne.Epub
{
    /// <summary>
    /// The base class for a local content file within the EPUB archive (e.g., an HTML file or an image).
    /// Unlike <see cref="EpubLocalContentFileRef" />, the classes derived from this base class contain the whole content of the file.
    /// </summary>
    public abstract class EpubLocalContentFile : EpubContentFile
    {
        /// <summary>
        /// Gets the absolute path of the local content file in the EPUB archive.
        /// </summary>
        public string FilePath { get; internal set; }

        /// <summary>
        /// Gets the location of the content file which is always <see cref="EpubContentLocation.LOCAL" /> for local content files.
        /// </summary>
        public override EpubContentLocation ContentLocation => EpubContentLocation.LOCAL;
    }
}
