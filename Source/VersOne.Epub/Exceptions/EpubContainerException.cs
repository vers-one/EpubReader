using System;

namespace VersOne.Epub
{
    public class EpubContainerException : EpubSchemaException
    {
        public EpubContainerException()
            : base(EpubSchemaFileType.META_INF_CONTAINER)
        {
        }

        public EpubContainerException(string message)
            : base(message, EpubSchemaFileType.META_INF_CONTAINER)
        {
        }

        public EpubContainerException(string message, Exception innerException)
            : base(message, innerException, EpubSchemaFileType.META_INF_CONTAINER)
        {
        }
    }
}
