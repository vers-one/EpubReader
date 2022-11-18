using System;
using System.IO;
using System.Threading.Tasks;

namespace VersOne.Epub
{
    /// <summary>
    /// The base class for a content file reference within the EPUB archive (e.g., an HTML file or an image).
    /// Unlike <see cref="EpubLocalContentFile" />, the classes derived from this base class contain only a reference to the file but don't contain its content.
    /// </summary>
    public abstract class EpubLocalContentFileRef : EpubContentFileRef
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubLocalContentFileRef" /> class with a specified content file key, a content type of the file reference,
        /// a MIME type of the file's content, a EPUB file reference, a content directory path, and an optional content reader options.
        /// </summary>
        /// <param name="metadata">Metadata of this content file reference.</param>
        /// <param name="filePath">The absolute path of the local content file in the EPUB archive.</param>
        /// <param name="epubContentLoader">A reference to the EPUB content loader which provides methods to load the content of this file.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="metadata"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="filePath"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="epubContentLoader"/> parameter is <c>null</c>.</exception>
        protected EpubLocalContentFileRef(EpubContentFileRefMetadata metadata, string filePath, IEpubContentLoader epubContentLoader)
            : base(metadata, epubContentLoader)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        /// <summary>
        /// Gets the absolute path of the local content file in the EPUB archive.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Gets the location of the content file which is always <see cref="EpubContentLocation.LOCAL" /> for local content file references.
        /// </summary>
        public override EpubContentLocation ContentLocation => EpubContentLocation.LOCAL;

        /// <summary>
        /// Reads the whole content of the referenced file and returns it as a byte array.
        /// </summary>
        /// <returns>Content of the referenced file.</returns>
        public byte[] ReadContentAsBytes()
        {
            return EpubContentLoader.LoadContentAsBytes(Metadata);
        }

        /// <summary>
        /// Asynchronously reads the whole content of the referenced file and returns it as a byte array.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The value of the TResult parameter contains the content of the referenced file.</returns>
        public Task<byte[]> ReadContentAsBytesAsync()
        {
            return EpubContentLoader.LoadContentAsBytesAsync(Metadata);
        }

        /// <summary>
        /// Reads the whole content of the referenced file and returns it as a string.
        /// </summary>
        /// <returns>Content of the referenced file.</returns>
        public string ReadContentAsText()
        {
            return EpubContentLoader.LoadContentAsText(Metadata);
        }

        /// <summary>
        /// Asynchronously reads the whole content of the referenced file and returns it as a string.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The value of the TResult parameter contains the content of the referenced file.</returns>
        public Task<string> ReadContentAsTextAsync()
        {
            return EpubContentLoader.LoadContentAsTextAsync(Metadata);
        }

        /// <summary>
        /// Opens the referenced file and returns a <see cref="Stream" /> to access its content.
        /// </summary>
        /// <returns>A <see cref="Stream" /> to access the referenced file's content.</returns>
        public Stream GetContentStream()
        {
            return EpubContentLoader.GetContentStream(Metadata);
        }

        /// <summary>
        /// Opens the referenced file and returns a <see cref="Stream" /> to access its content.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous open operation. The value of the TResult parameter contains the <see cref="Stream" /> to access the referenced file's content.
        /// </returns>
        public Task<Stream> GetContentStreamAsync()
        {
            return EpubContentLoader.GetContentStreamAsync(Metadata);
        }
    }
}
