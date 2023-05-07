using System;
using System.IO.Compression;

namespace VersOne.Epub.Environment.Implementation
{
    internal class ZipFile : IZipFile
    {
        private readonly ZipArchive zipArchive;

        public ZipFile(ZipArchive zipArchive)
        {
            this.zipArchive = zipArchive ?? throw new ArgumentNullException(nameof(zipArchive));
            IsDisposed = false;
        }

        ~ZipFile()
        {
            Dispose(false);
        }

        public bool IsDisposed { get; private set; }

        public IZipFileEntry? GetEntry(string entryName)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(nameof(ZipFile));
            }
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
            if (!IsDisposed)
            {
                if (disposing)
                {
                    zipArchive.Dispose();
                }
                IsDisposed = true;
            }
        }
    }
}
