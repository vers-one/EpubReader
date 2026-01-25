using VersOne.Epub.Schema;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB book meta information reader.
    /// </summary>
    public class MetadataReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="MetadataReaderOptions" /> class with a predefined set of options.</param>
        public MetadataReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            switch (preset)
            {
                case EpubReaderOptionsPreset.IGNORE_ALL_ERRORS:
                    SkipLinksWithoutHrefs = true;
                    IgnoreLinkWithoutRelError = true;
                    break;
            }
        }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB book meta information reader should skip 'link' XML elements
        /// that are missing the required 'href' attribute.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'href' attribute is not present,
        /// then the "Incorrect EPUB metadata link: href is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case those 'link' XML elements will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipLinksWithoutHrefs { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB book meta information reader should ignore the error
        /// when a 'link' XML element is missing the 'rel' attribute.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'rel' attribute is not present,
        /// then the "Incorrect EPUB metadata link: rel is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will ignore this error
        /// and will initialize the <see cref="EpubMetadataLink.Relationships" /> property with an empty list.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreLinkWithoutRelError { get; set; }
    }
}
