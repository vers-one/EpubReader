namespace VersOne.Epub
{
    /// <summary>
    /// Type of the EPUB schema file.
    /// </summary>
    public enum EpubSchemaFileType
    {
        /// <summary>
        /// A schema file type which is not present in this enumeration.
        /// </summary>
        UNKNOWN = 0,

        /// <summary>
        /// EPUB OCF 'META-INF\container.xml' file.
        /// </summary>
        META_INF_CONTAINER,

        /// <summary>
        /// EPUB OPF (Open Packaging Format) file.
        /// </summary>
        OPF_PACKAGE,

        /// <summary>
        /// EPUB 2 NCX navigation document file.
        /// </summary>
        EPUB2_NCX,

        /// <summary>
        /// EPUB 3 navigation document file.
        /// </summary>
        EPUB3_NAV_DOCUMENT
    }
}
