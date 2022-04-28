namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB reader.
    /// </summary>
    public class EpubReaderOptions
    {
        public EpubReaderOptions()
        {
            PackageReaderOptions = new PackageReaderOptions();
            Epub2NcxReaderOptions = new Epub2NcxReaderOptions();
        }

        /// <summary>
        /// Gets or sets EPUB package reader options.
        /// </summary>
        public PackageReaderOptions PackageReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB 2 NCX (Navigation Center eXtended) reader options.
        /// </summary>
        public Epub2NcxReaderOptions Epub2NcxReaderOptions { get; set; }
    }
}
