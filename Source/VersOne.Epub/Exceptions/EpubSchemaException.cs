using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents a base class for exceptions thrown by the EpubReader due to a schema parsing error.
    /// </summary>
    public abstract class EpubSchemaException : EpubReaderException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubSchemaException" /> class with a specified schema file type.
        /// </summary>
        /// <param name="schemaFileType">The type of the schema file that caused the error.</param>
        protected EpubSchemaException(EpubSchemaFileType schemaFileType)
        {
            SchemaFileType = schemaFileType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubSchemaException" /> class with a specified error and a specified schema file type.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="schemaFileType">The type of the schema file that caused the error.</param>
        protected EpubSchemaException(string message, EpubSchemaFileType schemaFileType)
            : base(message)
        {
            SchemaFileType = schemaFileType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubSchemaException" /> class with a specified error message, a reference to the inner exception
        /// that is the cause of this exception, and a specified schema file type.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <c>null</c> reference if no inner exception is specified.</param>
        /// <param name="schemaFileType">The type of the schema file that caused the error.</param>
        protected EpubSchemaException(string message, Exception? innerException, EpubSchemaFileType schemaFileType)
            : base(message, innerException)
        {
            SchemaFileType = schemaFileType;
        }

        /// <summary>
        /// Gets the type of the schema file that caused the error.
        /// </summary>
        public EpubSchemaFileType SchemaFileType { get; }
    }
}
