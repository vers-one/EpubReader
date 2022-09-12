using System.Text;
using VersOne.Epub.Environment;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class TestZipFileEntry : IZipFileEntry
    {
        private readonly byte[] byteContent;

        public TestZipFileEntry(string textContent)
        {
            byteContent = Encoding.UTF8.GetBytes(textContent);
        }

        public TestZipFileEntry(byte[] byteContent)
        {
            this.byteContent = byteContent;
        }

        public long Length => byteContent.Length;

        public Stream Open()
        {
            return new MemoryStream(byteContent);
        }
    }
}
