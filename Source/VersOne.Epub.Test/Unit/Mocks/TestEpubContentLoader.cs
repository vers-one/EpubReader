using System.Text;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class TestEpubContentLoader : IEpubContentLoader
    {
        private readonly byte[]? byteContent;
        private readonly Stream? stream;

        public TestEpubContentLoader()
        {
            byteContent = Array.Empty<byte>();
            stream = null;
        }

        public TestEpubContentLoader(string textContent)
        {
            byteContent = Encoding.UTF8.GetBytes(textContent);
            stream = null;
        }

        public TestEpubContentLoader(byte[] byteContent)
        {
            this.byteContent = byteContent;
            stream = null;
        }

        public TestEpubContentLoader(Stream stream)
        {
            byteContent = null;
            this.stream = stream;
        }

        public byte[] LoadContentAsBytes(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return byteContent ?? Array.Empty<byte>();
        }

        public Task<byte[]> LoadContentAsBytesAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return Task.FromResult(LoadContentAsBytes(contentFileRefMetadata));
        }

        public string LoadContentAsText(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return byteContent != null ? Encoding.UTF8.GetString(byteContent) : String.Empty;
        }

        public Task<string> LoadContentAsTextAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return Task.FromResult(LoadContentAsText(contentFileRefMetadata));
        }

        public Stream GetContentStream(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            if (stream != null)
            {
                return stream;
            }
            else if (byteContent != null)
            {
                return new MemoryStream(byteContent);
            }
            else
            {
                return new MemoryStream();
            }
        }

        public Task<Stream> GetContentStreamAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return Task.FromResult(GetContentStream(contentFileRefMetadata));
        }
    }
}
