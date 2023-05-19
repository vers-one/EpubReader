using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents an exception thrown by the EpubReader due to a EPUB 3 navigation document parsing error.
    /// </summary>
    public class Epub3NavException : EpubSchemaException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub3NavException" /> class.
        /// </summary>
        public Epub3NavException()
            : base(EpubSchemaFileType.EPUB3_NAV_DOCUMENT)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Epub3NavException" /> class with a specified error.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public Epub3NavException(string message)
            : base(message, EpubSchemaFileType.EPUB3_NAV_DOCUMENT)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Epub3NavException" /> class with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <c>null</c> reference if no inner exception is specified.</param>
        public Epub3NavException(string message, Exception? innerException)
            : base(message, innerException, EpubSchemaFileType.EPUB3_NAV_DOCUMENT)
        {
        }
    }
}
