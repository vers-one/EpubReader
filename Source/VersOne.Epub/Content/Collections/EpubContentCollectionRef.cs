using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VersOne.Epub
{
    /// <summary>
    /// A container for a subset of content file references within the EPUB book.
    /// </summary>
    /// <typeparam name="TLocalContentFileRef">The type of the content file references stored within the <see cref="Local" /> collection.</typeparam>
    /// <typeparam name="TRemoteContentFileRef">The type of the content file references stored within the <see cref="Remote" /> collection.</typeparam>
    public class EpubContentCollectionRef<TLocalContentFileRef, TRemoteContentFileRef>
        where TLocalContentFileRef : EpubLocalContentFileRef
        where TRemoteContentFileRef : EpubRemoteContentFileRef
    {
        private readonly Dictionary<string, TLocalContentFileRef> localByKey;
        private readonly Dictionary<string, TLocalContentFileRef> localByFilePath;
        private readonly Dictionary<string, TRemoteContentFileRef> remoteByUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentCollectionRef{TLocalContentFileRef, TRemoteContentFileRef}" /> class.
        /// </summary>
        /// <param name="local">Local content file references to be stored within this container.</param>
        /// <param name="remote">Remote content file references to be stored within this container.</param>
        public EpubContentCollectionRef(ReadOnlyCollection<TLocalContentFileRef>? local = null, ReadOnlyCollection<TRemoteContentFileRef>? remote = null)
        {
            Local = local ?? new ReadOnlyCollection<TLocalContentFileRef>(new List<TLocalContentFileRef>());
            Remote = remote ?? new ReadOnlyCollection<TRemoteContentFileRef>(new List<TRemoteContentFileRef>());
            localByKey = new Dictionary<string, TLocalContentFileRef>();
            localByFilePath = new Dictionary<string, TLocalContentFileRef>();
            foreach (TLocalContentFileRef localContentFileRef in Local)
            {
                if (localByKey.ContainsKey(localContentFileRef.Key))
                {
                    throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{localContentFileRef.Key}\" is not unique.");
                }
                localByKey.Add(localContentFileRef.Key, localContentFileRef);
                if (localByFilePath.ContainsKey(localContentFileRef.FilePath))
                {
                    throw new EpubPackageException($"Incorrect EPUB manifest: item with absolute file path = \"{localContentFileRef.FilePath}\" is not unique.");
                }
                localByFilePath.Add(localContentFileRef.FilePath, localContentFileRef);
            }
            remoteByUrl = new Dictionary<string, TRemoteContentFileRef>();
            foreach (TRemoteContentFileRef remoteContentFileRef in Remote)
            {
                if (remoteByUrl.ContainsKey(remoteContentFileRef.Url))
                {
                    throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{remoteContentFileRef.Url}\" is not unique.");
                }
                remoteByUrl.Add(remoteContentFileRef.Url, remoteContentFileRef);
            }
        }

        /// <summary>
        /// Gets a collection of local content file references stored within this container.
        /// </summary>
        public ReadOnlyCollection<TLocalContentFileRef> Local { get; }

        /// <summary>
        /// Gets a collection of remote content file references stored within this container.
        /// </summary>
        public ReadOnlyCollection<TRemoteContentFileRef> Remote { get; }

        /// <summary>
        /// Determines whether a local content file reference with the specified <see cref="EpubContentFileRef.Key" /> value exists in this container.
        /// </summary>
        /// <param name="key">The <see cref="EpubContentFileRef.Key" /> value of the local content file reference to locate in this container.</param>
        /// <returns>
        /// <c>true</c> if the local content file reference with the specified <see cref="EpubContentFileRef.Key" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is <c>null</c>.</exception>
        public bool ContainsLocalFileRefWithKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return localByKey.ContainsKey(key);
        }

        /// <summary>
        /// Gets the local content file reference with the specified <see cref="EpubContentFileRef.Key" /> value.
        /// </summary>
        /// <param name="key">The <see cref="EpubContentFileRef.Key" /> of the local content file reference to get.</param>
        /// <returns>The local content file reference with the specified <see cref="EpubContentFileRef.Key" /> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is <c>null</c>.</exception>
        /// <exception cref="EpubContentCollectionRefException">
        /// Local content file reference with the specified <see cref="EpubContentFileRef.Key" /> value does not exist in this container.
        /// </exception>
        public TLocalContentFileRef GetLocalFileRefByKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            try
            {
                return localByKey[key];
            }
            catch (KeyNotFoundException)
            {
                throw new EpubContentCollectionRefException($"Local content file reference with key = \"{key}\" does not exist in this container.");
            }
        }

        /// <summary>
        /// Gets the local content file reference with the specified <see cref="EpubContentFileRef.Key" /> value.
        /// </summary>
        /// <param name="key">The <see cref="EpubContentFileRef.Key" /> of the local content file reference to get.</param>
        /// <param name="localContentFileRef">
        /// When this method returns, contains the local content file reference with the specified <see cref="EpubContentFileRef.Key" /> value,
        /// if such local content file reference exists in the container; otherwise, <c>null</c>.</param>
        /// <returns>
        /// <c>true</c> if the local content file reference with the specified <see cref="EpubContentFileRef.Key" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is <c>null</c>.</exception>
        public bool TryGetLocalFileRefByKey(string key, out TLocalContentFileRef localContentFileRef)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return localByKey.TryGetValue(key, out localContentFileRef);
        }

        /// <summary>
        /// Determines whether a local content file reference with the specified <see cref="EpubLocalContentFileRef.FilePath" /> value exists in this container.
        /// </summary>
        /// <param name="filePath">The <see cref="EpubLocalContentFileRef.FilePath" /> value of the local content file reference to locate in this container.</param>
        /// <returns>
        /// <c>true</c> if the local content file reference with the specified <see cref="EpubLocalContentFileRef.FilePath" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath" /> is <c>null</c>.</exception>
        public bool ContainsLocalFileRefWithFilePath(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            return localByFilePath.ContainsKey(filePath);
        }

        /// <summary>
        /// Gets the local content file reference with the specified <see cref="EpubLocalContentFileRef.FilePath" /> value.
        /// </summary>
        /// <param name="filePath">The <see cref="EpubLocalContentFileRef.FilePath" /> of the local content file reference to get.</param>
        /// <returns>The local content file reference with the specified <see cref="EpubLocalContentFileRef.FilePath" /> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath" /> is <c>null</c>.</exception>
        /// <exception cref="EpubContentCollectionRefException">
        /// Local content file reference with the specified <see cref="EpubLocalContentFileRef.FilePath" /> value does not exist in this container.
        /// </exception>
        public TLocalContentFileRef GetLocalFileRefByFilePath(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            try
            {
                return localByFilePath[filePath];
            }
            catch (KeyNotFoundException)
            {
                throw new EpubContentCollectionRefException($"Local content file reference with file path = \"{filePath}\" does not exist in this container.");
            }
        }

        /// <summary>
        /// Gets the local content file reference with the specified <see cref="EpubLocalContentFileRef.FilePath" /> value.
        /// </summary>
        /// <param name="filePath">The <see cref="EpubLocalContentFileRef.FilePath" /> of the local content file reference to get.</param>
        /// <param name="localContentFileRef">
        /// When this method returns, contains the local content file reference with the specified <see cref="EpubLocalContentFileRef.FilePath" /> value,
        /// if such local content file reference exists in the container; otherwise, <c>null</c>.</param>
        /// <returns>
        /// <c>true</c> if the local content file reference with the specified <see cref="EpubLocalContentFileRef.FilePath" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath" /> is <c>null</c>.</exception>
        public bool TryGetLocalFileRefByFilePath(string filePath, out TLocalContentFileRef localContentFileRef)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            return localByFilePath.TryGetValue(filePath, out localContentFileRef);
        }

        /// <summary>
        /// Determines whether a remote content file reference with the specified <see cref="EpubRemoteContentFileRef.Url" /> value exists in this container.
        /// </summary>
        /// <param name="url">The <see cref="EpubRemoteContentFileRef.Url" /> value of the remote content file reference to locate in this container.</param>
        /// <returns>
        /// <c>true</c> if the remote content file reference with the specified <see cref="EpubRemoteContentFileRef.Url" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="url" /> is <c>null</c>.</exception>
        public bool ContainsRemoteFileRefWithUrl(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }
            return remoteByUrl.ContainsKey(url);
        }

        /// <summary>
        /// Gets the remote content file reference with the specified <see cref="EpubRemoteContentFileRef.Url" /> value.
        /// </summary>
        /// <param name="url">The <see cref="EpubRemoteContentFileRef.Url" /> of the remote content file reference to get.</param>
        /// <returns>The remote content file reference with the specified <see cref="EpubRemoteContentFileRef.Url" /> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url" /> is <c>null</c>.</exception>
        /// <exception cref="EpubContentCollectionRefException">
        /// Remote content file reference with the specified <see cref="EpubRemoteContentFileRef.Url" /> value does not exist in this container.
        /// </exception>
        public TRemoteContentFileRef GetRemoteFileRefByUrl(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }
            try
            {
                return remoteByUrl[url];
            }
            catch (KeyNotFoundException)
            {
                throw new EpubContentCollectionRefException($"Remote content file reference with URL = \"{url}\" does not exist in this container.");
            }
        }

        /// <summary>
        /// Gets the remote content file reference with the specified <see cref="EpubRemoteContentFileRef.Url" /> value.
        /// </summary>
        /// <param name="url">The <see cref="EpubRemoteContentFileRef.Url" /> of the remote content file reference to get.</param>
        /// <param name="remoteContentFileRef">
        /// When this method returns, contains the remote content file reference with the specified <see cref="EpubRemoteContentFileRef.Url" /> value,
        /// if such remote content file reference exists in the container; otherwise, <c>null</c>.</param>
        /// <returns>
        /// <c>true</c> if the remote content file reference with the specified <see cref="EpubRemoteContentFileRef.Url" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="url" /> is <c>null</c>.</exception>
        public bool TryGetRemoteFileRefByUrl(string url, out TRemoteContentFileRef remoteContentFileRef)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }
            return remoteByUrl.TryGetValue(url, out remoteContentFileRef);
        }
    }
}
