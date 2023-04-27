using System;
using System.Threading.Tasks;

namespace VersOne.Epub
{
    /// <summary>
    /// A reference for a file located outside of the EPUB archive which allows to read its content as a byte array.
    /// Unlike <see cref="EpubRemoteByteContentFile" />, this class contains only a reference to the file but doesn't contain its content.
    /// </summary>
    public class EpubRemoteByteContentFileRef : EpubRemoteContentFileRef
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubRemoteByteContentFileRef" /> class with a specified content file key, a content type of the file reference,
        /// a MIME type of the file's content, and content downloader options.
        /// </summary>
        /// <param name="metadata">Metadata of this content file reference.</param>
        /// <param name="epubContentLoader">A reference to the EPUB content loader which provides methods to download the content of this file.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="metadata" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="epubContentLoader" /> parameter is <c>null</c>.</exception>
        public EpubRemoteByteContentFileRef(EpubContentFileRefMetadata metadata, IEpubContentLoader epubContentLoader)
            : base(metadata, epubContentLoader)
        {
        }

        /// <summary>
        /// Gets the type of the content file which is always <see cref="EpubContentFileType.BYTE_ARRAY" /> for remote byte content file references.
        /// </summary>
        public override EpubContentFileType ContentFileType => EpubContentFileType.BYTE_ARRAY;

        /// <summary>
        /// Downloads the whole content of the referenced file and returns it as a byte array.
        /// </summary>
        /// <returns>Content of the referenced file.</returns>
        public byte[] DownloadContent()
        {
            return DownloadContentAsBytes();
        }

        /// <summary>
        /// Asynchronously downloads the whole content of the referenced file and returns it as a byte array.
        /// </summary>
        /// <returns>A task that represents the asynchronous download operation. The value of the TResult parameter contains the content of the referenced file.</returns>
        public Task<byte[]> DownloadContentAsync()
        {
            return DownloadContentAsBytesAsync();
        }
    }
}
