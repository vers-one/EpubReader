using System;
using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    /// <summary>
    /// A file within the EPUB archive with its content represented as a string. It is used mainly for HTML and CSS files.
    /// Unlike <see cref="EpubLocalTextContentFileRef" />, this class contains the whole content of the file.
    /// </summary>
    public class EpubLocalTextContentFile : EpubLocalContentFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubLocalTextContentFile" /> class.
        /// </summary>
        /// <param name="key">The content file key as it is declared in the EPUB manifest (in the <see cref="EpubManifestItem.Href" /> property).</param>
        /// <param name="contentType">The type of the content of the file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the file.</param>
        /// <param name="filePath">The absolute path of the local content file in the EPUB archive.</param>
        /// <param name="content">The content of the file.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="contentMimeType"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="filePath"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="content"/> parameter is <c>null</c>.</exception>
        public EpubLocalTextContentFile(string key, EpubContentType contentType, string contentMimeType, string filePath, string content)
            : base(key, contentType, contentMimeType, filePath)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets the type of the content file which is always <see cref="EpubContentFileType.TEXT" /> for local text content files.
        /// </summary>
        public override EpubContentFileType ContentFileType => EpubContentFileType.TEXT;
    }
}
