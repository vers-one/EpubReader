using System.Text;
using VersOne.Epub.Environment;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class TestZipFileEntry : IZipFileEntry
    {
        private readonly byte[] byteContent;
        private readonly MemoryStream memoryStream;

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

        public long Length => memoryStream != null ? memoryStream.Length : byteContent.Length;

        public Stream Open()
        {
            return memoryStream ?? new MemoryStream(byteContent);
        }
    }
}
