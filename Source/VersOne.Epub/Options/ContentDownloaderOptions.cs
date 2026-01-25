using VersOne.Epub.Environment;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB content downloader which is used for downloading remote content files.
    /// </summary>
    public class ContentDownloaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentDownloaderOptions" /> class.
        /// </summary>
        public ContentDownloaderOptions()
        {
            DownloadContent = false;
            DownloaderUserAgent = null;
            CustomContentDownloader = null;
        }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the content downloader should download remote resources specified in the EPUB manifest.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c>, then <see cref="EpubRemoteTextContentFile.Content" /> and
        /// <see cref="EpubRemoteByteContentFile.Content" /> will always be <c>null</c> and <see cref="EpubRemoteContentFileRef" /> will throw
        /// a "Downloading remote content is prohibited by the ContentDownloaderOptions.DownloadContent option" exception.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool DownloadContent { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets the user agent presented by the content downloader.
        /// </para>
        /// <para>
        /// This value is used by the built-in downloader to set the <c>User-Agent</c> header of the HTTP request.
        /// If this value is set to <c>null</c>, then the following user agent value will be used: "EpubReader/version" where "version" is
        /// the current version of the EpubReader library.
        /// </para>
        /// <para>
        /// Default value is <c>null</c>.
        /// </para>
        /// </summary>
        public string? DownloaderUserAgent { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a reference to the custom content downloader.
        /// </para>
        /// <para>
        /// If it's set to <c>null</c>, the built-in content downloader will be used to download remote content files.
        /// </para>
        /// <para>
        /// Default value is <c>null</c>.
        /// </para>
        /// </summary>
        public IContentDownloader? CustomContentDownloader { get; set; }
    }
}
