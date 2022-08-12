using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents a base class for exceptions thrown by the EpubReader.
    /// </summary>
    public abstract class EpubReaderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubReaderException" /> class.
        /// </summary>
        protected EpubReaderException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubReaderException" /> class with a specified error.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        protected EpubReaderException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubReaderException" /> class with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        protected EpubReaderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
