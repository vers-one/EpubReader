using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace VersOne.Epub.Environment.Implementation
{
    internal class ContentDownloader : IContentDownloader
    {
        private static readonly HttpClient httpClient;

        static ContentDownloader()
        {
            httpClient = new HttpClient();
        }

        public async Task<Stream> DownloadAsync(string url, string userAgent)
        {
            try
            {
                using HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, url);
                httpRequestMessage.Headers.Add("User-Agent", userAgent);
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead);
                httpResponseMessage.EnsureSuccessStatusCode();
                return await httpResponseMessage.Content.ReadAsStreamAsync();
            }
            catch (Exception exception)
            {
                throw new EpubContentDownloaderException("There was an error while downloading a remote content file.", exception, url);
            }
        }
    }
}
