using System.Text;
using VersOne.Epub.Environment;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class TestZipFileEntry : IZipFileEntry
    {
        private readonly byte[]? byteContent;
        private readonly MemoryStream? memoryStream;

        public TestZipFileEntry(string textContent)
        {
            byteContent = Encoding.UTF8.GetBytes(textContent);
            memoryStream = null;
        }

        public TestZipFileEntry(byte[] byteContent)
        {
            this.byteContent = byteContent;
            memoryStream = null;
        }

        public TestZipFileEntry(MemoryStream memoryStream)
        {
            byteContent = null;
            this.memoryStream = memoryStream;
        }

        public long Length
        {
            get
            {
                if (memoryStream != null)
                {
                    return memoryStream.Length;
                }
                else if (byteContent != null)
                {
                    return byteContent.Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Stream Open()
        {
            if (memoryStream != null)
            {
                return memoryStream;
            }
            else if (byteContent != null)
            {
                return new MemoryStream(byteContent);
            }
            else
            {
                return new MemoryStream();
            }
        }
    }
}
