using System;
using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    /// <summary>
    /// A file within the EPUB archive with its content represented as a byte array. It is used for images and font files.
    /// Unlike <see cref="EpubLocalByteContentFileRef" />, this class contains the whole content of the file.
    /// </summary>
    public class EpubLocalByteContentFile : EpubLocalContentFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubLocalByteContentFile" /> class.
        /// </summary>
        /// <param name="key">The content file key as it is declared in the EPUB manifest (in the <see cref="EpubManifestItem.Href" /> property).</param>
        /// <param name="contentType">The type of the content of the file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the file.</param>
        /// <param name="filePath">The absolute path of the local content file in the EPUB archive.</param>
        /// <param name="content">The content of the file.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="contentMimeType" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="filePath" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="content" /> parameter is <c>null</c>.</exception>
        public EpubLocalByteContentFile(string key, EpubContentType contentType, string contentMimeType, string filePath, byte[] content)
            : base(key, contentType, contentMimeType, filePath)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        public byte[] Content { get; }

        /// <summary>
        /// Gets the type of the content file which is always <see cref="EpubContentFileType.BYTE_ARRAY" /> for local byte content files.
        /// </summary>
        public override EpubContentFileType ContentFileType => EpubContentFileType.BYTE_ARRAY;
    }
}
