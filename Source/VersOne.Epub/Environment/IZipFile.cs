using System;

namespace VersOne.Epub.Environment
{
    /// <summary>
    /// Represents a ZIP archive file.
    /// </summary>
    public interface IZipFile : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this file was disposed or not.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Retrieves a wrapper for the specified entry in the ZIP archive.
        /// </summary>
        /// <param name="entryName">A path, relative to the root of the archive, that identifies the entry to retrieve.</param>
        /// <returns>A wrapper for the specified entry in the archive or <c>null</c> if the entry does not exist in the archive.</returns>
        IZipFileEntry? GetEntry(string entryName);
    }
}
