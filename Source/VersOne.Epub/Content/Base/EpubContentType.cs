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
        /// A file with either 'application/javascript', 'application/ecmascript', or 'text/javascript' MIME type.
        /// </summary>
        SCRIPT,

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
        /// A file with 'image/webp' MIME type.
        /// </summary>
        IMAGE_WEBP,

        /// <summary>
        /// A file with either 'font/truetype', 'font/ttf', or 'application/x-font-truetype' MIME type.
        /// </summary>
        FONT_TRUETYPE,

        /// <summary>
        /// A file with either 'font/opentype', 'font/otf' or 'application/vnd.ms-opentype' MIME type.
        /// </summary>
        FONT_OPENTYPE,

        /// <summary>
        /// A file with 'font/sfnt' or 'application/font-sfnt' MIME type.
        /// </summary>
        FONT_SFNT,

        /// <summary>
        /// A file with either 'font/woff' or 'application/font-woff' MIME type.
        /// </summary>
        FONT_WOFF,

        /// <summary>
        /// A file with 'font/woff2' MIME type.
        /// </summary>
        FONT_WOFF2,

        /// <summary>
        /// A file with 'application/smil+xml' MIME type.
        /// </summary>
        SMIL,

        /// <summary>
        /// A file with 'audio/mpeg' MIME type.
        /// </summary>
        AUDIO_MP3,

        /// <summary>
        /// A file with 'audio/mp4' MIME type.
        /// </summary>
        AUDIO_MP4,

        /// <summary>
        /// A file with either 'audio/ogg' or 'audio/ogg; codecs=opus' MIME type.
        /// </summary>
        AUDIO_OGG,

        /// <summary>
        /// A file with a MIME type which is not present in this enumeration.
        /// </summary>
        OTHER
    }
}
