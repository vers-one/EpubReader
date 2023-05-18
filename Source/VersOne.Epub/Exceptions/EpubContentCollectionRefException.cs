using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents an exception thrown by the EpubReader due to an invalid operation
    /// performed on an instance of the <see cref="EpubContentCollectionRef{TLocalContentFileRef, TRemoteContentFileRef}" /> class.
    /// </summary>
    public class EpubContentCollectionRefException : EpubReaderException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentCollectionRefException" /> class.
        /// </summary>
        public EpubContentCollectionRefException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentCollectionRefException" /> class with a specified error.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EpubContentCollectionRefException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentCollectionRefException" /> class with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <c>null</c> reference if no inner exception is specified.</param>
        public EpubContentCollectionRefException(string message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
