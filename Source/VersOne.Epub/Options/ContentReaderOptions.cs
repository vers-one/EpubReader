using System;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB content reader which is used for loading local content files.
    /// </summary>
    public class ContentReaderOptions
    {
        /// <summary>
        /// Occurs when a local content file is listed in the EPUB manifest but the content reader is unable to find it in the EPUB archive.
        /// This event lets the application to be notified of such errors and to decide how EPUB content reader should handle the missing file.
        /// </summary>
        public event EventHandler<ContentFileMissingEventArgs>? ContentFileMissing;

        internal void RaiseContentFileMissingEvent(ContentFileMissingEventArgs contentFileMissingEventArgs)
        {
            ContentFileMissing?.Invoke(this, contentFileMissingEventArgs);
        }
    }
}
