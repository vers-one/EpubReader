using VersOne.Epub.Environment;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class Test4GbZipFileEntry : IZipFileEntry
    {
        private const long SIZE_4GB = (long)4 * 1024 * 1024 * 1024;

        public long Length => SIZE_4GB;

        public long CompressedLength => SIZE_4GB;

        public Stream Open()
        {
            return new MemoryStream(); 
        }
    }
}
