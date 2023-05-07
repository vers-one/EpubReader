using System.IO;

namespace VersOne.Epub.Environment
{
    /// <summary>
    /// Provides methods for various file system related operations.
    /// </summary>
    internal interface IFileSystem
    {
        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        /// <param name="path">The file to check.</param>
        /// <returns><c>true</c> if path contains the name of an existing file; otherwise, <c>false</c>.</returns>
        bool FileExists(string path);

        /// <summary>
        /// Opens a ZIP archive for reading at the specified path.
        /// </summary>
        /// <param name="path">The path to the archive to open.</param>
        /// <returns>The opened ZIP archive.</returns>
        IZipFile OpenZipFile(string path);

        /// <summary>
        /// Opens a ZIP archive for reading from the specified stream.
        /// </summary>
        /// <param name="stream">The stream containing the archive to open.</param>
        /// <returns>The opened ZIP archive.</returns>
        IZipFile OpenZipFile(Stream stream);
    }
}
