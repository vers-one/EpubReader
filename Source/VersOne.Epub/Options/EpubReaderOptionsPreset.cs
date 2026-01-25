namespace VersOne.Epub.Options
{
    /// <summary>
    /// A predefined set of options for the <see cref="EpubReaderOptions" /> class.
    /// </summary>
    public enum EpubReaderOptionsPreset
    {
        /// <summary>
        /// All EPUB validation checks are enabled. If the file doesn't conform to the EPUB standard, an exception will be thrown.
        /// This is the default option.
        /// </summary>
        STRICT = 1,

        /// <summary>
        /// Ignore EPUB validation errors that are most common for the real-world EPUB books.
        /// </summary>
        RELAXED = 2,

        /// <summary>
        /// Turn off all EPUB validation checks. EpubReader will try to salvage as much data as possible without throwing any EPUB validation exceptions.
        /// </summary>
        IGNORE_ALL_ERRORS = 3
    }
}
