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
        /// Initializes a new instance of the <see cref="EpubContentCollectionRef{TLocalContentFileRef, TRemoteContentFileRef}" /> class.
        /// </summary>
        /// <param name="local">Local content file references to be stored within this container.</param>
        /// <param name="remote">Remote content file references to be stored within this container.</param>
        public EpubContentCollectionRef(Dictionary<string, TLocalContentFileRef>? local = null, Dictionary<string, TRemoteContentFileRef>? remote = null)
        {
            Local = local ?? new Dictionary<string, TLocalContentFileRef>();
            Remote = remote ?? new Dictionary<string, TRemoteContentFileRef>();
        }

        /// <summary>
        /// Gets a collection of key-value pairs where the key is the value of the <see cref="EpubContentFile.Key" /> property of the content file reference
        /// and the value is the content file reference itself. This collection contains only the local content file references stored within this container.
        /// </summary>
        public Dictionary<string, TLocalContentFileRef> Local { get; }

        /// <summary>
        /// Gets a collection of key-value pairs where the key is the value of the <see cref="EpubContentFile.Key" /> property of the content file reference
        /// and the value is the content file reference itself. This collection contains only the remote content file references stored within this container.
        /// </summary>
        public Dictionary<string, TRemoteContentFileRef> Remote { get; }
    }
}
