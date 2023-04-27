using System.Text;
using VersOne.Epub.Environment;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class TestContentDownloader : IContentDownloader
    {
        private readonly Dictionary<string, byte[]> remoteFiles;
        private readonly Dictionary<string, Stream> streams;

        public TestContentDownloader()
        {
            remoteFiles = new Dictionary<string, byte[]>();
            streams = new Dictionary<string, Stream>();
            LastUserAgent = null;
        }

        public TestContentDownloader(string url, string textContent)
            : this()
        {
            AddTextRemoteFile(url, textContent);
        }

        public TestContentDownloader(string url, byte[] byteContent)
            : this()
        {
            AddByteRemoteFile(url, byteContent);
        }

        public TestContentDownloader(string url, Stream stream)
            : this()
        {
            AddStream(url, stream);
        }

        public string? LastUserAgent { get; private set; }

        public void AddTextRemoteFile(string url, string content)
        {
            remoteFiles.Add(url, Encoding.UTF8.GetBytes(content));
        }

        public void AddByteRemoteFile(string url, byte[] content)
        {
            remoteFiles.Add(url, content);
        }

        public void AddStream(string url, Stream stream)
        {
            streams.Add(url, stream);
        }

        public Task<Stream> DownloadAsync(string url, string userAgent)
        {
            LastUserAgent = userAgent;
            Stream? result;
            if (remoteFiles.TryGetValue(url, out byte[]? fileContent))
            {
                result = new MemoryStream(fileContent);
            }
            else if (!streams.TryGetValue(url, out result))
            {
                throw new Exception("File not found.");
            }
            return Task.FromResult(result);
        }
    }
}
