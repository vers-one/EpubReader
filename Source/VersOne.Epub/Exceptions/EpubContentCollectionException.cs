using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents an exception thrown by the EpubReader due to an invalid operation
    /// performed on an instance of the <see cref="EpubContentCollection{TLocalContentFile, TRemoteContentFile}" /> class.
    /// </summary>
    public class EpubContentCollectionException : EpubReaderException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentCollectionException" /> class.
        /// </summary>
        public EpubContentCollectionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentCollectionException" /> class with a specified error.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EpubContentCollectionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentCollectionException" /> class with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <c>null</c> reference if no inner exception is specified.</param>
        public EpubContentCollectionException(string message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
