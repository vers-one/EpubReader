using System;
using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    /// <summary>
    /// A file located outside of the EPUB archive with its content represented as a byte array. It is used for images and font files.
    /// Unlike <see cref="EpubRemoteByteContentFileRef" />, this class contains the whole content of the file.
    /// </summary>
    public class EpubRemoteByteContentFile : EpubRemoteContentFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubRemoteByteContentFile" /> class.
        /// </summary>
        /// <param name="key">The content file key as it is declared in the EPUB manifest (in the <see cref="EpubManifestItem.Href" /> property).</param>
        /// <param name="contentType">The type of the content of the file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the file.</param>
        /// <param name="url">The absolute URI of the remote content file (as it is specified in the EPUB manifest).</param>
        /// <param name="content">The content of the file.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="contentMimeType"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="url"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="content"/> parameter is <c>null</c>.</exception>
        public EpubRemoteByteContentFile(string key, EpubContentType contentType, string contentMimeType, string url, byte[]? content)
            : base(key, contentType, contentMimeType, url)
        {
            Content = content;
        }

        /// <summary>
        /// Gets the content of the file or <c>null</c> if the content has not been downloaded.
        /// </summary>
        public byte[]? Content { get; }

        /// <summary>
        /// Gets the type of the content file which is always <see cref="EpubContentFileType.BYTE_ARRAY" /> for remote byte content files.
        /// </summary>
        public override EpubContentFileType ContentFileType => EpubContentFileType.BYTE_ARRAY;
    }
}
