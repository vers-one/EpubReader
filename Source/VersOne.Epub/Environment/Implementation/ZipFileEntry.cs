using System.IO;
using System.IO.Compression;

namespace VersOne.Epub.Environment.Implementation
{
    internal class ZipFileEntry : IZipFileEntry
    {
        private readonly ZipArchiveEntry zipArchiveEntry;

        public ZipFileEntry(ZipArchiveEntry zipArchiveEntry)
        {
            this.zipArchiveEntry = zipArchiveEntry;
        }

        public long Length => zipArchiveEntry.Length;

        public Stream Open()
        {
            return zipArchiveEntry.Open();
        }
    }
}
