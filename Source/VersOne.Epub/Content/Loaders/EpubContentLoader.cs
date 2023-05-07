using System;
using System.IO;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal abstract class EpubContentLoader : IEpubContentLoader
    {
        private readonly IEnvironmentDependencies environmentDependencies;

        protected EpubContentLoader(IEnvironmentDependencies environmentDependencies)
        {
            this.environmentDependencies = environmentDependencies ?? throw new ArgumentNullException(nameof(environmentDependencies));
        }

        protected IEnvironmentDependencies EnvironmentDependencies => environmentDependencies;

        public abstract Task<byte[]> LoadContentAsBytesAsync(EpubContentFileRefMetadata contentFileRefMetadata);
        public abstract Task<string> LoadContentAsTextAsync(EpubContentFileRefMetadata contentFileRefMetadata);
        public abstract Task<Stream> GetContentStreamAsync(EpubContentFileRefMetadata contentFileRefMetadata);

        public byte[] LoadContentAsBytes(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return LoadContentAsBytesAsync(contentFileRefMetadata).ExecuteAndUnwrapAggregateException();
        }

        public string LoadContentAsText(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return LoadContentAsTextAsync(contentFileRefMetadata).ExecuteAndUnwrapAggregateException();
        }

        public Stream GetContentStream(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return GetContentStreamAsync(contentFileRefMetadata).ExecuteAndUnwrapAggregateException();
        }
    }
}
