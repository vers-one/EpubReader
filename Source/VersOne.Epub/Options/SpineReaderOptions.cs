namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB spine reader which is used for parsing the &lt;spine&gt; section
    /// of the EPUB OPF package file. This section represents the default reading order of the EPUB book.
    /// </summary>
    public class SpineReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpineReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="SpineReaderOptions" /> class with a predefined set of options.</param>
        public SpineReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            switch (preset)
            {
                case EpubReaderOptionsPreset.IGNORE_ALL_ERRORS:
                    IgnoreMissingManifestItems = true;
                    SkipSpineItemsReferencingRemoteContent = true;
                    IgnoreMissingContentFiles = true;
                    break;
            }
        }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether EPUB spine reader should ignore the error when the manifest item referenced by
        /// a EPUB spine item is missing.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the manifest item with the given ID is not present, then
        /// the "Incorrect EPUB spine: item with IdRef = ... is missing in the manifest" exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader
        /// will skip the invalid spine item. As a result, the <see cref="EpubBook.ReadingOrder" /> collection
        /// and the data returned by the <see cref="EpubBookRef.GetReadingOrder" /> and
        /// the <see cref="EpubBookRef.GetReadingOrderAsync" /> methods will be missing the text content
        /// referenced by the invalid spine item.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingManifestItems { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether EPUB spine reader should skip EPUB spine items referencing remote HTML files.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a EPUB spine item is referencing a manifest item
        /// which is pointing to an external URL outside of the EPUB book,
        /// then the "Incorrect EPUB manifest: EPUB spine item ... cannot be a remote resource." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader
        /// will skip the invalid spine item. As a result, the <see cref="EpubBook.ReadingOrder" /> collection
        /// and the data returned by the <see cref="EpubBookRef.GetReadingOrder" /> and
        /// the <see cref="EpubBookRef.GetReadingOrderAsync" /> methods will be missing the text content
        /// referenced by the invalid spine item.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipSpineItemsReferencingRemoteContent { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether EPUB spine reader should ignore the error when the content file referenced by
        /// a EPUB spine item is missing.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and and a EPUB spine item is referencing a manifest item
        /// which is pointing to a non-existent HTML file within the EPUB book,
        /// then the "Incorrect EPUB manifest: HTML content file with href = ... is missing in the book." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader
        /// will skip the invalid spine item. As a result, the <see cref="EpubBook.ReadingOrder" /> collection
        /// and the data returned by the <see cref="EpubBookRef.GetReadingOrder" /> and
        /// the <see cref="EpubBookRef.GetReadingOrderAsync" /> methods will be missing the text content
        /// referenced by the invalid spine item.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingContentFiles { get; set; }
    }
}
