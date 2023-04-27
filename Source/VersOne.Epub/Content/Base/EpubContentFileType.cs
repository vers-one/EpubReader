namespace VersOne.Epub
{
    /// <summary>
    /// The type of a single EPUB content file (e.g. a chapter or an image).
    /// </summary>
    public enum EpubContentFileType
    {
        /// <summary>
        /// Content file represents a textual content (e.g. an HTML or a CSS file).
        /// </summary>
        TEXT = 1,

        /// <summary>
        /// Content file represents a binary content (e.g. an image or a font file).
        /// </summary>
        BYTE_ARRAY
    }
}
