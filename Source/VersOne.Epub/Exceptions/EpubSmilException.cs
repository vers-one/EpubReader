using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents an exception thrown by the EpubReader due to a SMIL document parsing error.
    /// </summary>
    public class EpubSmilException : EpubReaderException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubSmilException" /> class with a specified SMIL file path.
        /// </summary>
        /// <param name="smilFilePath">The path of the SMIL file that caused the error.</param>
        public EpubSmilException(string smilFilePath)
        {
            SmilFilePath = smilFilePath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubSmilException" /> class with a specified error and a specified SMIL file path.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="smilFilePath">The path of the SMIL file that caused the error.</param>
        public EpubSmilException(string message, string smilFilePath)
            : base(message)
        {
            SmilFilePath = smilFilePath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubSmilException" /> class with a specified error message, a reference to the inner exception
        /// that is the cause of this exception, and a specified SMIL file path.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <c>null</c> reference if no inner exception is specified.</param>
        /// <param name="smilFilePath">The path of the SMIL file that caused the error.</param>
        public EpubSmilException(string message, Exception innerException, string smilFilePath)
            : base(message, innerException)
        {
            SmilFilePath = smilFilePath;
        }

        /// <summary>
        /// Gets the path of the SMIL file that caused the error.
        /// </summary>
        public string SmilFilePath { get; }
    }
}
