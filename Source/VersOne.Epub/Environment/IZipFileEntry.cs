using System.IO;

namespace VersOne.Epub.Environment
{
    /// <summary>
    /// Represents a compressed file within a ZIP archive.
    /// </summary>
    public interface IZipFileEntry
    {
        /// <summary>
        /// Gets the uncompressed size of the entry in the ZIP archive.
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Opens the entry from the ZIP archive.
        /// </summary>
        /// <returns>The stream that represents the contents of the entry.</returns>
        Stream Open();
    }
}
