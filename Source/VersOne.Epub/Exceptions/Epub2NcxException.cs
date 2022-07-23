using System;

namespace VersOne.Epub
{
    public class Epub2NcxException : EpubSchemaException
    {
        public Epub2NcxException()
            : base(EpubSchemaFileType.EPUB2_NCX)
        {
        }

        public Epub2NcxException(string message)
            : base(message, EpubSchemaFileType.EPUB2_NCX)
        {
        }

        public Epub2NcxException(string message, Exception innerException)
            : base(message, innerException, EpubSchemaFileType.EPUB2_NCX)
        {
        }
    }
}
