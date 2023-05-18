using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents an exception thrown by the EpubReader due to a missing content file within the EPUB archive.
    /// </summary>
    public class EpubContentException : EpubReaderException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentException" /> class with a specified content file path.
        /// </summary>
        /// <param name="contentFilePath">The path of the content file that caused the error.</param>
        public EpubContentException(string contentFilePath)
        {
            ContentFilePath = contentFilePath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentException" /> class with a specified error and a specified content file path.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="contentFilePath">The path of the content file that caused the error.</param>
        public EpubContentException(string message, string contentFilePath)
            : base(message)
        {
            ContentFilePath = contentFilePath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentException" /> class with a specified error message, a reference to the inner exception
        /// that is the cause of this exception, and a specified content file path.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <c>null</c> reference if no inner exception is specified.</param>
        /// <param name="contentFilePath">The path of the content file that caused the error.</param>
        public EpubContentException(string message, Exception? innerException, string contentFilePath)
            : base(message, innerException)
        {
            ContentFilePath = contentFilePath;
        }

        /// <summary>
        /// Gets the path of the content file that caused the error.
        /// </summary>
        public string ContentFilePath { get; }
    }
}
