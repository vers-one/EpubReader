using System;

namespace VersOne.Epub
{
    public abstract class EpubSchemaException : EpubReaderException
    {
        protected EpubSchemaException(EpubSchemaFileType schemaFileType)
        {
            SchemaFileType = schemaFileType;
        }

        protected EpubSchemaException(string message, EpubSchemaFileType schemaFileType)
            : base(message)
        {
            SchemaFileType = schemaFileType;
        }

        protected EpubSchemaException(string message, Exception innerException, EpubSchemaFileType schemaFileType)
            : base(message, innerException)
        {
            SchemaFileType = schemaFileType;
        }

        public EpubSchemaFileType SchemaFileType { get; }
    }
}
