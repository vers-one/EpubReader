using System.Diagnostics.CodeAnalysis;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB spine reader which is used for parsing the &lt;spine&gt; section
    /// of the EPUB OPF package file. This section represents the default reading order of the EPUB book.
    /// </summary>
    public class SpineReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpineReaderOptions"/> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="SpineReaderOptions" /> class with a predefined set of options.</param>
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Temporarily ignore unused 'preset' parameter.")]
        public SpineReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether EPUB spine reader should ignore the error when the manifest item referenced by
        /// a EPUB spine item is missing.
        /// If it's set to <c>false</c> and the manifest item with the given ID is not present, then
        /// the "Incorrect EPUB spine: item with IdRef = "..." is missing in the manifest" exception will be thrown.
        /// Default value is <c>false</c>.
        /// </summary>
        public bool IgnoreMissingManifestItems { get; set; }
    }
}
