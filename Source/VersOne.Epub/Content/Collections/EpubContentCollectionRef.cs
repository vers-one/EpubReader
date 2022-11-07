using System.Collections.Generic;

namespace VersOne.Epub
{
    /// <summary>
    /// A container for a subset of content file references within the EPUB book.
    /// </summary>
    /// <typeparam name="TLocalContentFileRef">The type of the content file references stored within the <see cref="Local" /> dictionary.</typeparam>
    /// <typeparam name="TRemoteContentFileRef">The type of the content file references stored within the <see cref="Remote" /> dictionary.</typeparam>
    public class EpubContentCollectionRef<TLocalContentFileRef, TRemoteContentFileRef>
        where TLocalContentFileRef : EpubLocalContentFileRef
        where TRemoteContentFileRef : EpubRemoteContentFileRef
    {
        /// <summary>
        /// Gets a collection of key-value pairs where the key is the value of the <see cref="EpubContentFile.Key" /> property of the content file reference
        /// and the value is the content file reference itself. This collection contains only the local content file references stored within this container.
        /// </summary>
        public Dictionary<string, TLocalContentFileRef> Local { get; internal set; }

        /// <summary>
        /// Gets a collection of key-value pairs where the key is the value of the <see cref="EpubContentFile.Key" /> property of the content file reference
        /// and the value is the content file reference itself. This collection contains only the remote content file references stored within this container.
        /// </summary>
        public Dictionary<string, TRemoteContentFileRef> Remote { get; internal set; }
    }
}
