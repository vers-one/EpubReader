using System.Threading.Tasks;

namespace VersOne.Epub
{
    /// <summary>
    /// A reference for a file located outside of the EPUB archive which allows to read its content as a string.
    /// Unlike <see cref="EpubRemoteTextContentFile" />, this class contains only a reference to the file but doesn't contain its content.
    /// </summary>
    public class EpubRemoteTextContentFileRef : EpubRemoteContentFileRef
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubRemoteTextContentFileRef" /> class with a specified content file key, a content type of the file reference,
        /// a MIME type of the file's content, and content downloader options.
        /// </summary>
        /// <param name="metadata">Metadata of this content file reference.</param>
        /// <param name="epubContentLoader">A reference to the EPUB content loader which provides methods to download the content of this file.</param>
        public EpubRemoteTextContentFileRef(EpubContentFileRefMetadata metadata, IEpubContentLoader epubContentLoader)
            : base(metadata, epubContentLoader)
        {
        }

        /// <summary>
        /// Gets the type of the content file which is always <see cref="EpubContentFileType.TEXT" /> for remote text content file references.
        /// </summary>
        public override EpubContentFileType ContentFileType => EpubContentFileType.TEXT;

        /// <summary>
        /// Downloads the whole content of the referenced file and returns it as a string.
        /// </summary>
        /// <returns>Content of the referenced file.</returns>
        public string DownloadContent()
        {
            return DownloadContentAsText();
        }

        /// <summary>
        /// Asynchronously downloads the whole content of the referenced file and returns it as a string.
        /// </summary>
        /// <returns>A task that represents the asynchronous download operation. The value of the TResult parameter contains the content of the referenced file.</returns>
        public Task<string> DownloadContentAsync()
        {
            return DownloadContentAsTextAsync();
        }
    }
}
