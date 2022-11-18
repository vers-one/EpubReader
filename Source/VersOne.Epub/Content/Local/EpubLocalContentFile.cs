using System;
using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    /// <summary>
    /// The base class for a local content file within the EPUB archive (e.g., an HTML file or an image).
    /// Unlike <see cref="EpubLocalContentFileRef" />, the classes derived from this base class contain the whole content of the file.
    /// </summary>
    public abstract class EpubLocalContentFile : EpubContentFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubLocalContentFile" /> class.
        /// </summary>
        /// <param name="key">The content file key as it is declared in the EPUB manifest (in the <see cref="EpubManifestItem.Href" /> property).</param>
        /// <param name="contentType">The type of the content of the file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the file.</param>
        /// <param name="filePath">The absolute path of the local content file in the EPUB archive.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="contentMimeType"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="filePath"/> parameter is <c>null</c>.</exception>
        protected EpubLocalContentFile(string key, EpubContentType contentType, string contentMimeType, string filePath)
            : base(key, contentType, contentMimeType)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        /// <summary>
        /// Gets the absolute path of the local content file in the EPUB archive.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Gets the location of the content file which is always <see cref="EpubContentLocation.LOCAL" /> for local content files.
        /// </summary>
        public override EpubContentLocation ContentLocation => EpubContentLocation.LOCAL;
    }
}
