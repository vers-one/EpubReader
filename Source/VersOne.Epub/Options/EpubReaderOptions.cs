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
            PackageReaderOptions = new(preset);
            BookCoverReaderOptions = new(preset);
            ContainerFileReaderOptions = new(preset);
            ContentDownloaderOptions = new();
            ContentReaderOptions = new(preset);
            Epub2NcxReaderOptions = new(preset);
            Epub3NavDocumentReaderOptions = new(preset);
            MetadataReaderOptions = new(preset);
            NavigationReaderOptions = new(preset);
            SmilReaderOptions = new(preset);
            SpineReaderOptions = new(preset);
            XmlReaderOptions = new(preset);
        }

        /// <summary>
        /// Gets or sets EPUB OPF package reader options.
        /// </summary>
        public PackageReaderOptions PackageReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB content reader options which is used for loading the EPUB book cover image.
        /// </summary>
        public BookCoverReaderOptions BookCoverReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB OCF container file reader options.
        /// </summary>
        public ContainerFileReaderOptions ContainerFileReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB content downloader options which is used for downloading remote content files.
        /// </summary>
        public ContentDownloaderOptions ContentDownloaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB content reader options which is used for loading local content files.
        /// </summary>
        public ContentReaderOptions ContentReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB 2 NCX navigation document reader options.
        /// </summary>
        public Epub2NcxReaderOptions Epub2NcxReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB 3 navigation document reader options.
        /// </summary>
        public Epub3NavDocumentReaderOptions Epub3NavDocumentReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB book meta information reader options.
        /// </summary>
        public MetadataReaderOptions MetadataReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB navigation reader options.
        /// </summary>
        public NavigationReaderOptions NavigationReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets SMIL (EPUB media overlay) document reader options.
        /// </summary>
        public SmilReaderOptions SmilReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets EPUB spine reader options which is used for parsing the default reading order of the EPUB book.
        /// </summary>
        public SpineReaderOptions SpineReaderOptions { get; set; }

        /// <summary>
        /// Gets or sets XML reader options.
        /// </summary>
        public XmlReaderOptions XmlReaderOptions { get; set; }
    }
}
