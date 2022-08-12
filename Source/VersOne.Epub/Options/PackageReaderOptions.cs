namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB package reader.
    /// </summary>
    public class PackageReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageReaderOptions" /> class.
        /// </summary>
        public PackageReaderOptions()
        {
            IgnoreMissingToc = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package reader should ignore missing TOC attribute in the EPUB 2 spine.
        /// If it's set to false and the TOC attribute is not present, then the "Incorrect EPUB spine: TOC is missing" exception will be thrown.
        /// Default value is false.
        /// </summary>
        public bool IgnoreMissingToc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the package reader should skip EPUB manifest items that are missing required attributes (id, href, or media-type).
        /// If it's set to false and one of the required attributes is not present, then one of the "Incorrect EPUB manifest: item ... is missing" exceptions will be thrown.
        /// Default value is false.
        /// </summary>
        public bool SkipInvalidManifestItems { get; set; }
    }
}
