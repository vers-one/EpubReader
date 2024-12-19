namespace VersOne.Epub.Options
{
    /// <summary>
    /// A predefined set of options for the <see cref="EpubReaderOptions" /> class.
    /// </summary>
    public enum EpubReaderOptionsPreset
    {
        /// <summary>
        /// Ignore EPUB validation errors that are most common in the real-world EPUB books. This is the default option.
        /// </summary>
        RELAXED = 1,

        /// <summary>
        /// All EPUB validation checks are enabled. If the file doesn't conform to the EPUB standard, an exception will be thrown.
        /// </summary>
        STRICT = 2,

        /// <summary>
        /// Turn off all EPUB validation checks. EpubReader will try to salvage as much data as possible without throwing any EPUB validation exceptions.
        /// </summary>
        IGNORE_ALL_ERRORS = 3
    }
}
