using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;

namespace VersOne.Epub.Internal
{
    internal class EpubRemoteContentLoader : EpubContentLoader
    {
        private readonly ContentDownloaderOptions contentDownloaderOptions;
        private readonly IContentDownloader contentDownloader;
        private readonly string userAgent;

        public EpubRemoteContentLoader(IEnvironmentDependencies environmentDependencies, ContentDownloaderOptions contentDownloaderOptions)
            : base(environmentDependencies)
        {
            this.contentDownloaderOptions = contentDownloaderOptions ?? new ContentDownloaderOptions();
            contentDownloader = this.contentDownloaderOptions.CustomContentDownloader ?? EnvironmentDependencies.ContentDownloader;
            userAgent = this.contentDownloaderOptions.DownloaderUserAgent ??
                "EpubReader/" + typeof(EpubRemoteContentLoader).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        }

        public override async Task<byte[]> LoadContentAsBytesAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            using (Stream contentStream = await GetContentStreamAsync(contentFileRefMetadata))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await contentStream.CopyToAsync(memoryStream).ConfigureAwait(false);
                return memoryStream.ToArray();
            }
        }

        public override async Task<string> LoadContentAsTextAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            using (Stream contentStream = await GetContentStreamAsync(contentFileRefMetadata))
            using (StreamReader streamReader = new StreamReader(contentStream))
            {
                return await streamReader.ReadToEndAsync().ConfigureAwait(false);
            }
        }

        public override Task<Stream> GetContentStreamAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            if (!contentDownloaderOptions.DownloadContent)
            {
                throw new EpubContentDownloaderException("Downloading remote content is prohibited by the ContentDownloaderOptions.DownloadContent option.", contentFileRefMetadata.Key);
            }
            return contentDownloader.DownloadAsync(contentFileRefMetadata.Key, userAgent);
        }
    }
}
