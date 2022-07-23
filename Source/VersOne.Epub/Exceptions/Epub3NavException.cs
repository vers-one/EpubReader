using System;

namespace VersOne.Epub
{
    public class Epub3NavException : EpubSchemaException
    {
        public Epub3NavException()
            : base(EpubSchemaFileType.EPUB3_NAV_DOCUMENT)
        {
        }

        public Epub3NavException(string message)
            : base(message, EpubSchemaFileType.EPUB3_NAV_DOCUMENT)
        {
        }

        public Epub3NavException(string message, Exception innerException)
            : base(message, innerException, EpubSchemaFileType.EPUB3_NAV_DOCUMENT)
        {
        }
    }
}
