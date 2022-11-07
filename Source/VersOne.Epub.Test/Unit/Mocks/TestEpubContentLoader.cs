using System.Text;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class TestEpubContentLoader : IEpubContentLoader
    {
        private readonly byte[] byteContent;
        private readonly Stream stream;

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
            return byteContent;
        }

        public Task<byte[]> LoadContentAsBytesAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return Task.FromResult(LoadContentAsBytes(contentFileRefMetadata));
        }

        public string LoadContentAsText(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return Encoding.UTF8.GetString(byteContent);
        }

        public Task<string> LoadContentAsTextAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return Task.FromResult(LoadContentAsText(contentFileRefMetadata));
        }

        public Stream GetContentStream(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return stream ?? new MemoryStream(byteContent);
        }

        public Task<Stream> GetContentStreamAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return Task.FromResult(GetContentStream(contentFileRefMetadata));
        }
    }
}
