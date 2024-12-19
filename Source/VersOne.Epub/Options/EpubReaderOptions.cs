namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB reader.
    /// </summary>
    public class EpubReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="EpubReaderOptions" /> class with a predefined set of options.</param>
        public EpubReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            PackageReaderOptions = new PackageReaderOptions(preset);
            ContentReaderOptions = new ContentReaderOptions(preset);
            ContentDownloaderOptions = new ContentDownloaderOptions(preset);
            Epub2NcxReaderOptions = new Epub2NcxReaderOptions(preset);
            XmlReaderOptions = new XmlReaderOptions(preset);
        }

        /// <summary>
        /// Gets or sets EPUB OPF package reader options.
        /// </summary>
        public PackageReaderOptions PackageReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB content reader options which is used for loading local content files.
        /// </summary>
        public ContentReaderOptions ContentReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB content downloader options which is used for downloading remote content files.
        /// </summary>
        public ContentDownloaderOptions ContentDownloaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB 2 NCX navigation document reader options.
        /// </summary>
        public Epub2NcxReaderOptions Epub2NcxReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets XML reader options.
        /// </summary>
        public XmlReaderOptions XmlReaderOptions { get; set; }
    }
}
