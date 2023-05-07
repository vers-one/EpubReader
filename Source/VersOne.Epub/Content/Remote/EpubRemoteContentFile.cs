using System;
using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    /// <summary>
    /// The base class for a remote content file outside of the EPUB archive (e.g., an HTML file or an image).
    /// Unlike <see cref="EpubRemoteContentFileRef" />, the classes derived from this base class contain the whole content of the file.
    /// </summary>
    public abstract class EpubRemoteContentFile : EpubContentFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubRemoteContentFile" /> class.
        /// </summary>
        /// <param name="key">The content file key as it is declared in the EPUB manifest (in the <see cref="EpubManifestItem.Href" /> property).</param>
        /// <param name="contentType">The type of the content of the file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the file.</param>
        /// <param name="url">The absolute URI of the remote content file (as it is specified in the EPUB manifest).</param>
        /// <exception cref="ArgumentNullException">The <paramref name="contentMimeType" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="url" /> parameter is <c>null</c>.</exception>
        protected EpubRemoteContentFile(string key, EpubContentType contentType, string contentMimeType, string url)
            : base(key, contentType, contentMimeType)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }

        /// <summary>
        /// Gets the absolute URI of the remote content file (as it is specified in the EPUB manifest).
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Gets the location of the content file which is always <see cref="EpubContentLocation.REMOTE" /> for remote content files.
        /// </summary>
        public override EpubContentLocation ContentLocation => EpubContentLocation.REMOTE;
    }
}
