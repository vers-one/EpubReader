using VersOne.Epub.Environment;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class Test4GbZipFileEntry : IZipFileEntry
    {
        public long Length => (long)4 * 1024 * 1024 * 1024;

        public Stream Open()
        {
            return new MemoryStream(); 
        }
    }
}
