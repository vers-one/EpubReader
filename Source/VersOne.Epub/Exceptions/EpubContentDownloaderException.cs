using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents an exception thrown by the EpubReader due to a remote content downloading error.
    /// </summary>
    public class EpubContentDownloaderException : EpubReaderException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentDownloaderException" /> class with a specified URI of the remote content file.
        /// </summary>
        /// <param name="remoteContentFileUrl">The absolute URI of the remote content file that caused the error.</param>
        public EpubContentDownloaderException(string remoteContentFileUrl)
        {
            RemoteContentFileUrl = remoteContentFileUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentDownloaderException" /> class with a specified error and a specified URI of the remote content file.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="remoteContentFileUrl">The absolute URI of the remote content file that caused the error.</param>
        public EpubContentDownloaderException(string message, string remoteContentFileUrl)
            : base(message)
        {
            RemoteContentFileUrl = remoteContentFileUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentDownloaderException" /> class with a specified error message, a reference to the inner exception
        /// that is the cause of this exception, and a specified URI of the remote content file.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <c>null</c> reference if no inner exception is specified.</param>
        /// <param name="remoteContentFileUrl">The absolute URI of the remote content file that caused the error.</param>
        public EpubContentDownloaderException(string message, Exception innerException, string remoteContentFileUrl)
            : base(message, innerException)
        {
            RemoteContentFileUrl = remoteContentFileUrl;
        }

        /// <summary>
        /// Gets absolute URI of the remote content file that caused the error.
        /// </summary>
        public string RemoteContentFileUrl { get; }
    }
}
