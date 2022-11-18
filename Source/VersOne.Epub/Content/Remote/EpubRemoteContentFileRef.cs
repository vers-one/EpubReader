using System;
using System.IO;
using System.Threading.Tasks;

namespace VersOne.Epub
{
    /// <summary>
    /// The base class for a content file reference located outside of the EPUB archive (e.g., an HTML file or an image).
    /// Unlike <see cref="EpubRemoteContentFile" />, the classes derived from this base class contain only a reference to the file but don't contain its content.
    /// </summary>
    public abstract class EpubRemoteContentFileRef : EpubContentFileRef
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubRemoteContentFileRef" /> class with a specified content file key, a content type of the file reference,
        /// a MIME type of the file's content, and content downloader options.
        /// </summary>
        /// <param name="metadata">Metadata of this content file reference.</param>
        /// <param name="epubContentLoader">A reference to the EPUB content loader which provides methods to download the content of this file.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="metadata"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="epubContentLoader"/> parameter is <c>null</c>.</exception>
        protected EpubRemoteContentFileRef(EpubContentFileRefMetadata metadata, IEpubContentLoader epubContentLoader)
            : base(metadata, epubContentLoader)
        {
        }

        /// <summary>
        /// Gets the absolute URI of the remote content file (as it is specified in the EPUB manifest).
        /// </summary>
        public string Url => Key;

        /// <summary>
        /// Gets the location of the content file which is always <see cref="EpubContentLocation.REMOTE" /> for remote content file references.
        /// </summary>
        public override EpubContentLocation ContentLocation => EpubContentLocation.REMOTE;

        /// <summary>
        /// Downloads the whole content of the referenced file and returns it as a byte array.
        /// </summary>
        /// <returns>Content of the referenced file.</returns>
        public byte[] DownloadContentAsBytes()
        {
            return EpubContentLoader.LoadContentAsBytes(Metadata);
        }

        /// <summary>
        /// Asynchronously downloads the whole content of the referenced file and returns it as a byte array.
        /// </summary>
        /// <returns>A task that represents the asynchronous download operation. The value of the TResult parameter contains the content of the referenced file.</returns>
        public Task<byte[]> DownloadContentAsBytesAsync()
        {
            return EpubContentLoader.LoadContentAsBytesAsync(Metadata);
        }

        /// <summary>
        /// Downloads the whole content of the referenced file and returns it as a string.
        /// </summary>
        /// <returns>Content of the referenced file.</returns>
        public string DownloadContentAsText()
        {
            return EpubContentLoader.LoadContentAsText(Metadata);
        }

        /// <summary>
        /// Asynchronously downloads the whole content of the referenced file and returns it as a string.
        /// </summary>
        /// <returns>A task that represents the asynchronous download operation. The value of the TResult parameter contains the content of the referenced file.</returns>
        public Task<string> DownloadContentAsTextAsync()
        {
            return EpubContentLoader.LoadContentAsTextAsync(Metadata);
        }

        /// <summary>
        /// Starts the download of the referenced file and returns a <see cref="Stream" /> to access its content.
        /// </summary>
        /// <returns>A <see cref="Stream" /> to access the referenced file's content.</returns>
        public Stream GetDownloadingContentStream()
        {
            return EpubContentLoader.GetContentStream(Metadata);
        }

        /// <summary>
        /// Starts the download of the referenced file and returns a <see cref="Stream" /> to access its content.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous download operation.
        /// The value of the TResult parameter contains a <see cref="Stream" /> to access the referenced file's content.
        /// </returns>
        public Task<Stream> GetDownloadingContentStreamAsync()
        {
            return EpubContentLoader.GetContentStreamAsync(Metadata);
        }
    }
}
