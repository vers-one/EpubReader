namespace VersOne.Epub
{
    /// <summary>
    /// The base class for a remote content file outside of the EPUB archive (e.g., an HTML file or an image).
    /// Unlike <see cref="EpubRemoteContentFileRef" />, the classes derived from this base class contain the whole content of the file.
    /// </summary>
    public abstract class EpubRemoteContentFile : EpubContentFile
    {
        /// <summary>
        /// Gets the absolute URI of the remote content file (as it is specified in the EPUB manifest).
        /// </summary>
        public string Url { get; internal set; }

        /// <summary>
        /// Gets the location of the content file which is always <see cref="EpubContentLocation.REMOTE" /> for remote content files.
        /// </summary>
        public override EpubContentLocation ContentLocation => EpubContentLocation.REMOTE;
    }
}
