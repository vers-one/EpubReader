using System;

namespace VersOne.Epub
{
    public class EpubPackageException : EpubSchemaException
    {
        public EpubPackageException()
            : base(EpubSchemaFileType.OPF_PACKAGE)
        {
        }

        public EpubPackageException(string message)
            : base(message, EpubSchemaFileType.OPF_PACKAGE)
        {
        }

        public EpubPackageException(string message, Exception innerException)
            : base(message, innerException, EpubSchemaFileType.OPF_PACKAGE)
        {
        }
    }
}
