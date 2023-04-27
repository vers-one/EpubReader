namespace VersOne.Epub.Environment
{
    /// <summary>
    /// Contains all environment dependencies of EpubReader library (e.g. methods to interact with the file system or to download files).
    /// </summary>
    internal interface IEnvironmentDependencies
    {
        /// <summary>
        /// Gets an implementation for the <see cref="IFileSystem" /> interface that contains various file system related operations.
        /// </summary>
        IFileSystem FileSystem { get; }

        /// <summary>
        /// Gets an implementation for the <see cref="IContentDownloader" /> interface that contains methods for downloading remote content files.
        /// </summary>
        IContentDownloader ContentDownloader { get; }
    }
}
