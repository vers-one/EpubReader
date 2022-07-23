using System;

namespace VersOne.Epub
{
    public class EpubContentException : EpubReaderException
    {
        public EpubContentException(string contentFilePath)
        {
            ContentFilePath = contentFilePath;
        }

        public EpubContentException(string message, string contentFilePath)
            : base(message)
        {
            ContentFilePath = contentFilePath;
        }

        public EpubContentException(string message, Exception innerException, string contentFilePath)
            : base(message, innerException)
        {
            ContentFilePath = contentFilePath;
        }

        public string ContentFilePath { get; }
    }
}
