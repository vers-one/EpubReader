using System;
using System.IO;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Provides data for the <see cref="ContentReaderOptions.ContentFileMissing" /> event.
    /// </summary>
    public class ContentFileMissingEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentFileMissingEventArgs" /> class with a specified file name, an absolute file path, a content type of the file,
        /// and a MIME type of the file's content.
        /// </summary>
        /// <param name="fileKey">Relative file path of the missing content file (as it is specified in the EPUB manifest).</param>
        /// <param name="filePath">Absolute file path of the missing content file in the EPUB archive.</param>
        /// <param name="contentType">The type of the content of the missing file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the missing file.</param>
        public ContentFileMissingEventArgs(string fileKey, string filePath, EpubContentType contentType, string contentMimeType)
        {
            FileKey = fileKey;
            FilePath = filePath;
            ContentType = contentType;
            ContentMimeType = contentMimeType;
            SuppressException = false;
            ReplacementContentStream = null;
        }

        /// <summary>
        /// Gets the relative file path of the missing content file (as it is specified in the EPUB manifest).
        /// </summary>
        public string FileKey { get; }

        /// <summary>
        /// Gets the absolute file path of the missing content file in the EPUB archive.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Gets the type of the content of the missing file.
        /// </summary>
        public EpubContentType ContentType { get; }

        /// <summary>
        /// Gets the MIME type of the content of the missing file.
        /// </summary>
        public string ContentMimeType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the EPUB content reader should suppress the exception for the missing file. If it is set to <c>true</c>
        /// and the replacement content stream is not provided (via the <see cref="ReplacementContentStream" /> property), then the EPUB content reader will treat
        /// the missing file as an existing but empty file.
        /// Default value is <c>false</c>.
        /// </summary>
        public bool SuppressException { get; set; }

        /// <summary>
        /// Gets or sets the replacement content stream. This property allows the application to provide a replacement content for the missing file in the form of
        /// a <see cref="Stream" />. When the content stream is provided, the EPUB content reader will not throw an exception for the missing file,
        /// regardless of the value of the <see cref="SuppressException" /> property. The content of the stream is read only once, after which it will be cached
        /// in the EPUB content reader. The stream will be closed after its content is fully read.
        /// </summary>
        public Stream? ReplacementContentStream { get; set; }
    }
}
