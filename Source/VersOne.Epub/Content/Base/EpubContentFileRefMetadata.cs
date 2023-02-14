using System;
using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    /// <summary>
    /// Metadata for a content file reference.
    /// </summary>
    public class EpubContentFileRefMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentFileRefMetadata" /> class with a specified content file key, a content type of the file reference,
        /// and a MIME type of the file's content.
        /// </summary>
        /// <param name="key">The key of the content file as it is declared in the EPUB manifest (in the <see cref="EpubManifestItem.Href" /> property).</param>
        /// <param name="contentType">The type of the content of the file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the file.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="key" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="key" /> parameter is an empty string.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="contentMimeType" /> parameter is <c>null</c>.</exception>
        public EpubContentFileRefMetadata(string key, EpubContentType contentType, string contentMimeType)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            if (key == String.Empty)
            {
                throw new ArgumentException("Content file name cannot be empty.", nameof(key));
            }
            ContentType = contentType;
            ContentMimeType = contentMimeType ?? throw new ArgumentNullException(nameof(contentMimeType));
        }

        /// <summary>
        /// Gets the content file key as it is declared in the EPUB manifest (in the <see cref="EpubManifestItem.Href" /> property).
        /// This is either a relative path of the content file within the EPUB archive (for local content files)
        /// or an absolute URI of the content file outside of the EPUB archive (for remote content files).
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the type of the content of the file (e.g. <see cref="EpubContentType.XHTML_1_1" /> or <see cref="EpubContentType.IMAGE_JPEG" />).
        /// </summary>
        public EpubContentType ContentType { get; }

        /// <summary>
        /// Gets the MIME type of the content of the file.
        /// </summary>
        public string ContentMimeType { get; }
    }
}
