using System;

namespace VersOne.Epub
{
    public abstract class EpubReaderException : Exception
    {
        protected EpubReaderException()
        {
        }

        protected EpubReaderException(string message)
            : base(message)
        {
        }

        protected EpubReaderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
