namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB package reader.
    /// </summary>
    public class PackageReaderOptions
    {
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
    }
}
