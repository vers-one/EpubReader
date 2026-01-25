namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB book cover reader which is used for loading the EPUB book cover image.
    /// </summary>
    public class BookCoverReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookCoverReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="BookCoverReaderOptions" /> class with a predefined set of options.</param>
        public BookCoverReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            switch (preset)
            {
                case EpubReaderOptionsPreset.RELAXED:
                    Epub2MetadataIgnoreMissingManifestItem = true;
                    break;
                case EpubReaderOptionsPreset.IGNORE_ALL_ERRORS:
                    Epub2MetadataIgnoreMissingContent = true;
                    Epub2MetadataIgnoreMissingManifestItem = true;
                    Epub2MetadataIgnoreMissingContentFile = true;
                    Epub3IgnoreMissingContentFile = true;
                    IgnoreRemoteContentFileError = true;
                    break;
            }
        }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether EPUB 2 book cover reader should ignore the error when the EPUB 2 cover metadata item
        /// contains an empty or a whitespace value.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the EPUB 2 cover metadata node is empty or contains only whitespace characters, then
        /// the "Incorrect EPUB metadata: cover item content is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the EPUB 2 book cover reader will ignore the invalid cover metadata item. The cover information may still be read from
        /// the EPUB 2 guide, if it's present there.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool Epub2MetadataIgnoreMissingContent { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether EPUB 2 book cover reader should ignore the error when the manifest item referenced by
        /// the EPUB 2 cover metadata item is missing.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the manifest item with the given ID is not present, then
        /// the "Incorrect EPUB manifest: item with ID = ... referenced in EPUB 2 cover metadata is missing" exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the EPUB 2 book cover reader will ignore the invalid manifest item. The cover information may still be read from
        /// the EPUB 2 guide, if it's present there.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool Epub2MetadataIgnoreMissingManifestItem { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether EPUB 2 book cover reader should ignore the error when the image content file referenced by
        /// the EPUB 2 cover metadata item is missing.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the content file referenced by the manifest item which in turn is referenced by
        /// the EPUB 2 cover metadata item is not present, then the "Incorrect EPUB manifest: item with href = ... is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the EPUB 2 book cover reader will ignore the invalid item. The cover information may still be read from
        /// the EPUB 2 guide, if it's present there.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool Epub2MetadataIgnoreMissingContentFile { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether EPUB 3 book cover reader should ignore the error when the image content file referenced by
        /// the EPUB 3 cover manifest item is missing.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the content file referenced by the cover manifest item is not present,
        /// then the "Incorrect EPUB manifest: item with href = ... is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the EPUB 3 book cover reader will ignore the invalid item.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool Epub3IgnoreMissingContentFile { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether EPUB book cover reader should ignore the error when the EPUB cover manifest item
        /// is referencing a remote resource.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the content file referenced by the cover manifest item is a remote resource
        /// (i.e. the 'href' attribute of the manifest item is a remote URL),
        /// then the "Incorrect EPUB manifest: EPUB cover image ... cannot be a remote resource." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the EPUB book cover reader will ignore the invalid item.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreRemoteContentFileError { get; set; }
    }
}
