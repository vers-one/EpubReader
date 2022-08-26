using System;
using System.IO.Compression;

namespace VersOne.Epub.Environment.Implementation
{
    internal class ZipFile : IZipFile
    {
        private readonly ZipArchive zipArchive;
        private bool isDisposed;

        public ZipFile(ZipArchive zipArchive)
        {
            this.zipArchive = zipArchive;
        }

        ~ZipFile()
        {
            Dispose(false);
        }

        public IZipFileEntry GetEntry(string entryName)
        {
            ZipArchiveEntry zipArchiveEntry = zipArchive.GetEntry(entryName);
            return zipArchiveEntry != null ? new ZipFileEntry(zipArchiveEntry) : null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    zipArchive?.Dispose();
                }
                isDisposed = true;
            }
        }
    }
}
