using System.Collections.Generic;

namespace VersOne.Epub
{
    /// <summary>
    /// A container for a subset of content files within the EPUB book.
    /// </summary>
    /// <typeparam name="TLocalContentFile">The type of the content files stored within the <see cref="Local" /> dictionary.</typeparam>
    /// <typeparam name="TRemoteContentFile">The type of the content files stored within the <see cref="Remote" /> dictionary.</typeparam>
    public class EpubContentCollection<TLocalContentFile, TRemoteContentFile>
        where TLocalContentFile : EpubLocalContentFile
        where TRemoteContentFile : EpubRemoteContentFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentCollection{TLocalContentFile, TRemoteContentFile}" /> class.
        /// </summary>
        /// <param name="local">Local content files to be stored within this container.</param>
        /// <param name="remote">Remote content files to be stored within this container.</param>
        public EpubContentCollection(Dictionary<string, TLocalContentFile>? local = null, Dictionary<string, TRemoteContentFile>? remote = null)
        {
            Local = local ?? new Dictionary<string, TLocalContentFile>();
            Remote = remote ?? new Dictionary<string, TRemoteContentFile>();
        }

        /// <summary>
        /// Gets a collection of key-value pairs where the key is the value of the <see cref="EpubContentFile.Key" /> property of the content file
        /// and the value is the content file itself. This collection contains only the local content files stored within this container.
        /// </summary>
        public Dictionary<string, TLocalContentFile> Local { get; }

        /// <summary>
        /// Gets a collection of key-value pairs where the key is the value of the <see cref="EpubContentFile.Key" /> property of the content file
        /// and the value is the content file itself. This collection contains only the remote content files stored within this container.
        /// </summary>
        public Dictionary<string, TRemoteContentFile> Remote { get; }
    }
}
