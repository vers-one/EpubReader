using VersOne.Epub.Schema;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB navigation reader.
    /// </summary>
    public class NavigationReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="NavigationReaderOptions" /> class with a predefined set of options.</param>
        public NavigationReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            switch (preset)
            {
                case EpubReaderOptionsPreset.RELAXED:
                    SkipNavigationItemsReferencingMissingContent = true;
                    break;
                case EpubReaderOptionsPreset.IGNORE_ALL_ERRORS:
                    AllowEpub2NavigationItemsWithEmptyTitles = true;
                    SkipRemoteNavigationItems = true;
                    SkipNavigationItemsReferencingMissingContent = true;
                    break;
            }
        }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB navigation reader should ignore the error
        /// when a EPUB 2 navigation point has no navigation labels.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and <see cref="Epub2NcxNavigationPoint.NavigationLabels" /> collection is empty,
        /// then the "Incorrect EPUB 2 NCX: navigation point ... should contain at least one navigation label." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will ignore this error
        /// and will set the <see cref="EpubNavigationItemRef.Title" /> property to an empty string.
        /// </para>
        /// <para>
        /// This property has no effect on EPUB 3 navigation item validation
        /// as the EPUB 3 specification doesn't require the navigation items to have titles.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool AllowEpub2NavigationItemsWithEmptyTitles { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB navigation reader should skip navigation items that are referencing remote resources.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a navigation item is pointing to an external URL outside of the EPUB book,
        /// then the "Incorrect EPUB 2 NCX: content source ... cannot be a remote resource." exception (for EPUB 2 NCX documents) or
        /// the "Incorrect EPUB 3 navigation document: anchor href ... cannot be a remote resource." exception (for EPUB 3 navigation documents)
        /// will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case those navigation items will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipRemoteNavigationItems { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB navigation reader should skip navigation items referencing local content
        /// which is missing in the EPUB manifest.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a navigation item is pointing to a URL inside the EPUB book which is not present in the EPUB manifest,
        /// then the "Incorrect EPUB 2 NCX: content source ... not found in EPUB manifest." exception (for EPUB 2 NCX documents) or
        /// the "Incorrect EPUB 3 navigation document: target for anchor href ... not found in EPUB manifest." exception (for EPUB 3 navigation documents)
        /// will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case those navigation items will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipNavigationItemsReferencingMissingContent { get; set; }
    }
}
