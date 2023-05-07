using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents an exception thrown by the EpubReader due to a EPUB 2 NCX document parsing error.
    /// </summary>
    public class Epub2NcxException : EpubSchemaException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxException" /> class.
        /// </summary>
        public Epub2NcxException()
            : base(EpubSchemaFileType.EPUB2_NCX)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxException" /> class with a specified error.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public Epub2NcxException(string message)
            : base(message, EpubSchemaFileType.EPUB2_NCX)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxException" /> class with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <c>null</c> reference if no inner exception is specified.</param>
        public Epub2NcxException(string message, Exception innerException)
            : base(message, innerException, EpubSchemaFileType.EPUB2_NCX)
        {
        }
    }
}
