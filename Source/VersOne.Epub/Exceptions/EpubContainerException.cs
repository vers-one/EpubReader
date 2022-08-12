using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents an exception thrown by the EpubReader due to an OCF container parsing error.
    /// </summary>
    public class EpubContainerException : EpubSchemaException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContainerException" /> class.
        /// </summary>
        public EpubContainerException()
            : base(EpubSchemaFileType.META_INF_CONTAINER)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContainerException" /> class with a specified error.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EpubContainerException(string message)
            : base(message, EpubSchemaFileType.META_INF_CONTAINER)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContainerException" /> class with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public EpubContainerException(string message, Exception innerException)
            : base(message, innerException, EpubSchemaFileType.META_INF_CONTAINER)
        {
        }
    }
}
