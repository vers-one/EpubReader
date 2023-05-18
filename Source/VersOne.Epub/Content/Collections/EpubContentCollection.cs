using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VersOne.Epub
{
    /// <summary>
    /// A container for a subset of content files within the EPUB book.
    /// </summary>
    /// <typeparam name="TLocalContentFile">The type of the content files stored within the <see cref="Local" /> collection.</typeparam>
    /// <typeparam name="TRemoteContentFile">The type of the content files stored within the <see cref="Remote" /> collection.</typeparam>
    public class EpubContentCollection<TLocalContentFile, TRemoteContentFile>
        where TLocalContentFile : EpubLocalContentFile
        where TRemoteContentFile : EpubRemoteContentFile
    {
        private readonly Dictionary<string, TLocalContentFile> localByKey;
        private readonly Dictionary<string, TLocalContentFile> localByFilePath;
        private readonly Dictionary<string, TRemoteContentFile> remoteByUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentCollection{TLocalContentFile, TRemoteContentFile}" /> class.
        /// </summary>
        /// <param name="local">Local content files to be stored within this container.</param>
        /// <param name="remote">Remote content files to be stored within this container.</param>
        public EpubContentCollection(ReadOnlyCollection<TLocalContentFile>? local = null, ReadOnlyCollection<TRemoteContentFile>? remote = null)
        {
            Local = local ?? new ReadOnlyCollection<TLocalContentFile>(new List<TLocalContentFile>());
            Remote = remote ?? new ReadOnlyCollection<TRemoteContentFile>(new List<TRemoteContentFile>());
            localByKey = new Dictionary<string, TLocalContentFile>();
            localByFilePath = new Dictionary<string, TLocalContentFile>();
            foreach (TLocalContentFile localContentFile in Local)
            {
                if (localByKey.ContainsKey(localContentFile.Key))
                {
                    throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{localContentFile.Key}\" is not unique.");
                }
                localByKey.Add(localContentFile.Key, localContentFile);
                if (localByFilePath.ContainsKey(localContentFile.FilePath))
                {
                    throw new EpubPackageException($"Incorrect EPUB manifest: item with absolute file path = \"{localContentFile.FilePath}\" is not unique.");
                }
                localByFilePath.Add(localContentFile.FilePath, localContentFile);
            }
            remoteByUrl = new Dictionary<string, TRemoteContentFile>();
            foreach (TRemoteContentFile remoteContentFile in Remote)
            {
                if (remoteByUrl.ContainsKey(remoteContentFile.Url))
                {
                    throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{remoteContentFile.Url}\" is not unique.");
                }
                remoteByUrl.Add(remoteContentFile.Url, remoteContentFile);
            }
        }

        /// <summary>
        /// Gets a collection of local content files stored within this container.
        /// </summary>
        public ReadOnlyCollection<TLocalContentFile> Local { get; }

        /// <summary>
        /// Gets a collection of remote content files stored within this container.
        /// </summary>
        public ReadOnlyCollection<TRemoteContentFile> Remote { get; }

        /// <summary>
        /// Determines whether a local content file with the specified <see cref="EpubContentFile.Key" /> value exists in this container.
        /// </summary>
        /// <param name="key">The <see cref="EpubContentFile.Key" /> value of the local content file to locate in this container.</param>
        /// <returns>
        /// <c>true</c> if the local content file with the specified <see cref="EpubContentFile.Key" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is <c>null</c>.</exception>
        public bool ContainsLocalFileWithKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return localByKey.ContainsKey(key);
        }

        /// <summary>
        /// Gets the local content file with the specified <see cref="EpubContentFile.Key" /> value.
        /// </summary>
        /// <param name="key">The <see cref="EpubContentFile.Key" /> of the local content file to get.</param>
        /// <returns>The local content file with the specified <see cref="EpubContentFile.Key" /> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is <c>null</c>.</exception>
        /// <exception cref="EpubContentCollectionException">
        /// Local content file with the specified <see cref="EpubContentFile.Key" /> value does not exist in this container.
        /// </exception>
        public TLocalContentFile GetLocalFileByKey(string key)
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
                throw new EpubContentCollectionException($"Local content file with key = \"{key}\" does not exist in this container.");
            }
        }

        /// <summary>
        /// Gets the local content file with the specified <see cref="EpubContentFile.Key" /> value.
        /// </summary>
        /// <param name="key">The <see cref="EpubContentFile.Key" /> of the local content file to get.</param>
        /// <param name="localContentFile">
        /// When this method returns, contains the local content file with the specified <see cref="EpubContentFile.Key" /> value,
        /// if such local content file exists in the container; otherwise, <c>null</c>.</param>
        /// <returns>
        /// <c>true</c> if the local content file with the specified <see cref="EpubContentFile.Key" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is <c>null</c>.</exception>
        public bool TryGetLocalFileByKey(string key, out TLocalContentFile localContentFile)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return localByKey.TryGetValue(key, out localContentFile);
        }

        /// <summary>
        /// Determines whether a local content file with the specified <see cref="EpubLocalContentFile.FilePath" /> value exists in this container.
        /// </summary>
        /// <param name="filePath">The <see cref="EpubLocalContentFile.FilePath" /> value of the local content file to locate in this container.</param>
        /// <returns>
        /// <c>true</c> if the local content file with the specified <see cref="EpubLocalContentFile.FilePath" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath" /> is <c>null</c>.</exception>
        public bool ContainsLocalFileWithFilePath(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            return localByFilePath.ContainsKey(filePath);
        }

        /// <summary>
        /// Gets the local content file with the specified <see cref="EpubLocalContentFile.FilePath" /> value.
        /// </summary>
        /// <param name="filePath">The <see cref="EpubLocalContentFile.FilePath" /> of the local content file to get.</param>
        /// <returns>The local content file with the specified <see cref="EpubLocalContentFile.FilePath" /> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath" /> is <c>null</c>.</exception>
        /// <exception cref="EpubContentCollectionException">
        /// Local content file with the specified <see cref="EpubLocalContentFile.FilePath" /> value does not exist in this container.
        /// </exception>
        public TLocalContentFile GetLocalFileByFilePath(string filePath)
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
                throw new EpubContentCollectionException($"Local content file with file path = \"{filePath}\" does not exist in this container.");
            }
        }

        /// <summary>
        /// Gets the local content file with the specified <see cref="EpubLocalContentFile.FilePath" /> value.
        /// </summary>
        /// <param name="filePath">The <see cref="EpubLocalContentFile.FilePath" /> of the local content file to get.</param>
        /// <param name="localContentFile">
        /// When this method returns, contains the local content file with the specified <see cref="EpubLocalContentFile.FilePath" /> value,
        /// if such local content file exists in the container; otherwise, <c>null</c>.</param>
        /// <returns>
        /// <c>true</c> if the local content file with the specified <see cref="EpubLocalContentFile.FilePath" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath" /> is <c>null</c>.</exception>
        public bool TryGetLocalFileByFilePath(string filePath, out TLocalContentFile localContentFile)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            return localByKey.TryGetValue(filePath, out localContentFile);
        }

        /// <summary>
        /// Determines whether a remote content file with the specified <see cref="EpubRemoteContentFile.Url" /> value exists in this container.
        /// </summary>
        /// <param name="url">The <see cref="EpubRemoteContentFile.Url" /> value of the remote content file to locate in this container.</param>
        /// <returns>
        /// <c>true</c> if the remote content file with the specified <see cref="EpubRemoteContentFile.Url" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="url" /> is <c>null</c>.</exception>
        public bool ContainsRemoteFileWithUrl(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }
            return remoteByUrl.ContainsKey(url);
        }

        /// <summary>
        /// Gets the remote content file with the specified <see cref="EpubRemoteContentFile.Url" /> value.
        /// </summary>
        /// <param name="url">The <see cref="EpubRemoteContentFile.Url" /> of the remote content file to get.</param>
        /// <returns>The remote content file with the specified <see cref="EpubRemoteContentFile.Url" /> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url" /> is <c>null</c>.</exception>
        /// <exception cref="EpubContentCollectionException">
        /// Remote content file with the specified <see cref="EpubRemoteContentFile.Url" /> value does not exist in this container.
        /// </exception>
        public TRemoteContentFile GetRemoteFileByUrl(string url)
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
                throw new EpubContentCollectionException($"Remote content file with URL = \"{url}\" does not exist in this container.");
            }
        }

        /// <summary>
        /// Gets the remote content file with the specified <see cref="EpubRemoteContentFile.Url" /> value.
        /// </summary>
        /// <param name="url">The <see cref="EpubRemoteContentFile.Url" /> of the remote content file to get.</param>
        /// <param name="remoteContentFile">
        /// When this method returns, contains the remote content file with the specified <see cref="EpubRemoteContentFile.Url" /> value,
        /// if such remote content file exists in the container; otherwise, <c>null</c>.</param>
        /// <returns>
        /// <c>true</c> if the remote content file with the specified <see cref="EpubRemoteContentFile.Url" /> value exists in this container; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="url" /> is <c>null</c>.</exception>
        public bool TryGetRemoteFileByUrl(string url, out TRemoteContentFile remoteContentFile)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }
            return remoteByUrl.TryGetValue(url, out remoteContentFile);
        }
    }
}
