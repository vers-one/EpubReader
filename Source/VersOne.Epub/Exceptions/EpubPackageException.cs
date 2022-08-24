using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents an exception thrown by the EpubReader due to an OPF package parsing error.
    /// </summary>
    public class EpubPackageException : EpubSchemaException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubPackageException" /> class.
        /// </summary>
        public EpubPackageException()
            : base(EpubSchemaFileType.OPF_PACKAGE)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubPackageException" /> class with a specified error.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EpubPackageException(string message)
            : base(message, EpubSchemaFileType.OPF_PACKAGE)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubPackageException" /> class with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <c>null</c> reference if no inner exception is specified.</param>
        public EpubPackageException(string message, Exception innerException)
            : base(message, innerException, EpubSchemaFileType.OPF_PACKAGE)
        {
        }
    }
}
