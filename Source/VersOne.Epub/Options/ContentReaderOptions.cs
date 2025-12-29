using System;
using System.Diagnostics.CodeAnalysis;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB content reader which is used for loading local content files.
    /// </summary>
    public class ContentReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="ContentReaderOptions" /> class with a predefined set of options.</param>
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Temporarily ignore unused 'preset' parameter.")]
        public ContentReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
        }

        /// <summary>
        /// Occurs when a local content file is listed in the EPUB manifest but the content reader is unable to find it in the EPUB archive.
        /// This event lets the application to be notified of such errors and to decide how EPUB content reader should handle the missing file.
        /// </summary>
        public event EventHandler<ContentFileMissingEventArgs>? ContentFileMissing;

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether content reader should ignore the error when the EPUB 3 navigation manifest item
        /// is referencing a remote resource.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the content file referenced by the navigation manifest item is a remote resource
        /// (i.e. the 'href' attribute of the manifest item is a remote URL),
        /// then the "Incorrect EPUB manifest: EPUB 3 navigation document ... cannot be a remote resource." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the content reader will ignore the error.
        /// The navigation content file will still be added to the collection of remote HTML/XHTML content files, even if the error was ignored.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreRemoteEpub3NavigationFileError { get; set; }

        internal void RaiseContentFileMissingEvent(ContentFileMissingEventArgs contentFileMissingEventArgs)
        {
            ContentFileMissing?.Invoke(this, contentFileMissingEventArgs);
        }
    }
}
