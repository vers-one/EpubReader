using System;
using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    /// <summary>
    /// The base class for content file references.
    /// </summary>
    public abstract class EpubContentFileRef
    {
        private readonly EpubContentFileRefMetadata metadata;
        private readonly IEpubContentLoader epubContentLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentFileRef" /> class with a specified metadata and a specified content loader.
        /// </summary>
        /// <param name="metadata">Metadata of this content file reference.</param>
        /// <param name="epubContentLoader">A reference to the EPUB content loader which provides methods to load the content of this file.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="metadata" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="epubContentLoader" /> parameter is <c>null</c>.</exception>
        protected EpubContentFileRef(EpubContentFileRefMetadata metadata, IEpubContentLoader epubContentLoader)
        {
            this.metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
            this.epubContentLoader = epubContentLoader ?? throw new ArgumentNullException(nameof(epubContentLoader));
        }

        /// <summary>
        /// Gets the content file reference key as it is declared in the EPUB manifest (in the <see cref="EpubManifestItem.Href" /> property).
        /// This is either a relative path of the content file within the EPUB archive (for local content file references)
        /// or an absolute URI of the content file outside of the EPUB archive (for remote content file references).
        /// </summary>
        public string Key => metadata.Key;

        /// <summary>
        /// Gets the type of the content of the file (e.g. <see cref="EpubContentType.XHTML_1_1" /> or <see cref="EpubContentType.IMAGE_JPEG" />).
        /// </summary>
        public EpubContentType ContentType => metadata.ContentType;

        /// <summary>
        /// Gets the MIME type of the content of the file.
        /// </summary>
        public string ContentMimeType => metadata.ContentMimeType;

        /// <summary>
        /// Gets the location of the content file (local or remote).
        /// </summary>
        public abstract EpubContentLocation ContentLocation { get; }

        /// <summary>
        /// Gets the type of the content file reference (text or byte array).
        /// </summary>
        public abstract EpubContentFileType ContentFileType { get; }

        /// <summary>
        /// Gets the metadata of this content file reference.
        /// </summary>
        protected EpubContentFileRefMetadata Metadata => metadata;

        /// <summary>
        /// Gets the reference to the EPUB content loader which provides methods to load the content of this file.
        /// </summary>
        protected IEpubContentLoader EpubContentLoader => epubContentLoader;
    }
}
