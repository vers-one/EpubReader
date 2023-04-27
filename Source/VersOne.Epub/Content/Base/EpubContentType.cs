namespace VersOne.Epub
{
    /// <summary>
    /// The type of the content in a EPUB content file.
    /// </summary>
    public enum EpubContentType
    {
        /// <summary>
        /// A file with 'application/xhtml+xml' MIME type.
        /// </summary>
        XHTML_1_1 = 1,

        /// <summary>
        /// A file with 'application/x-dtbook+xml' MIME type.
        /// </summary>
        DTBOOK,

        /// <summary>
        /// A file with 'application/x-dtbncx+xml' MIME type.
        /// </summary>
        DTBOOK_NCX,

        /// <summary>
        /// A file with 'text/x-oeb1-document' MIME type.
        /// </summary>
        OEB1_DOCUMENT,

        /// <summary>
        /// A file with 'application/xml' MIME type.
        /// </summary>
        XML,

        /// <summary>
        /// A file with 'text/css' MIME type.
        /// </summary>
        CSS,

        /// <summary>
        /// A file with 'text/x-oeb1-css' MIME type.
        /// </summary>
        OEB1_CSS,

        /// <summary>
        /// A file with 'image/gif' MIME type.
        /// </summary>
        IMAGE_GIF,

        /// <summary>
        /// A file with 'image/jpeg' MIME type.
        /// </summary>
        IMAGE_JPEG,

        /// <summary>
        /// A file with 'image/png' MIME type.
        /// </summary>
        IMAGE_PNG,

        /// <summary>
        /// A file with 'image/svg+xml' MIME type.
        /// </summary>
        IMAGE_SVG,

        /// <summary>
        /// A file with either 'font/truetype' or 'application/x-font-truetype' MIME type.
        /// </summary>
        FONT_TRUETYPE,

        /// <summary>
        /// A file with either 'font/opentype' or 'application/vnd.ms-opentype' MIME type.
        /// </summary>
        FONT_OPENTYPE,

        /// <summary>
        /// A file with 'application/smil+xml' MIME type.
        /// </summary>
        SMIL,

        /// <summary>
        /// A file with a MIME type which is not present in this enumeration.
        /// </summary>
        OTHER
    }
}
