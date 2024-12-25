namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB book cover reader which is used for loading the EPUB book cover image.
    /// </summary>
    public class BookCoverReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookCoverReaderOptions"/> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="BookCoverReaderOptions" /> class with a predefined set of options.</param>
        public BookCoverReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            switch (preset)
            {
                case EpubReaderOptionsPreset.RELAXED:
                case EpubReaderOptionsPreset.IGNORE_ALL_ERRORS:
                    Epub2MetadataIgnoreMissingManifestItem = true;
                    break;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether EPUB 2 book cover reader should ignore the error when the manifest item referenced by
        /// the EPUB 2 cover metadata item is missing.
        /// If it's set to <c>false</c> and the manifest item with the given ID is not present, then
        /// the "Incorrect EPUB manifest: item with ID = "..." referenced in EPUB 2 cover metadata is missing" exception will be thrown.
        /// Default value is <c>false</c>.
        /// </summary>
        public bool Epub2MetadataIgnoreMissingManifestItem { get; set; }
    }
}
