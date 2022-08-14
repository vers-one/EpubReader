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
        public EpubReaderOptions()
        {
            PackageReaderOptions = new PackageReaderOptions();
            Epub2NcxReaderOptions = new Epub2NcxReaderOptions();
            XmlReaderOptions = new XmlReaderOptions();
        }

        /// <summary>
        /// Gets or sets EPUB OPF package reader options.
        /// </summary>
        public PackageReaderOptions PackageReaderOptions { get; set; }

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
