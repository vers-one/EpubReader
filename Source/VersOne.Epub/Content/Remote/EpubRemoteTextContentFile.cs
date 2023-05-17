using System;
using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    /// <summary>
    /// A file located outside of the EPUB archive with its content represented as a string. It is used mainly for HTML and CSS files.
    /// Unlike <see cref="EpubRemoteTextContentFileRef" />, this class contains the whole content of the file.
    /// </summary>
    public class EpubRemoteTextContentFile : EpubRemoteContentFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubRemoteTextContentFile" /> class.
        /// </summary>
        /// <param name="key">The content file key as it is declared in the EPUB manifest (in the <see cref="EpubManifestItem.Href" /> property).</param>
        /// <param name="contentType">The type of the content of the file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the file.</param>
        /// <param name="content">The content of the file.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="contentMimeType" /> parameter is <c>null</c>.</exception>
        public EpubRemoteTextContentFile(string key, EpubContentType contentType, string contentMimeType, string? content)
            : base(key, contentType, contentMimeType)
        {
            Content = content;
        }

        /// <summary>
        /// Gets the content of the file or <c>null</c> if the content has not been downloaded.
        /// </summary>
        public string? Content { get; }

        /// <summary>
        /// Gets the type of the content file which is always <see cref="EpubContentFileType.TEXT" /> for remote text content files.
        /// </summary>
        public override EpubContentFileType ContentFileType => EpubContentFileType.TEXT;
    }
}
