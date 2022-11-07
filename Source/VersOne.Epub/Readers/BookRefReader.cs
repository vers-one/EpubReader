using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal class BookRefReader
    {
        private readonly IEnvironmentDependencies environmentDependencies;
        private readonly EpubReaderOptions epubReaderOptions;

        public BookRefReader(IEnvironmentDependencies environmentDependencies, EpubReaderOptions epubReaderOptions)
        {
            this.environmentDependencies = environmentDependencies ?? throw new ArgumentNullException(nameof(environmentDependencies));
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public EpubBookRef OpenBook(string filePath)
        {
            return OpenBookAsync(filePath).ExecuteAndUnwrapAggregateException();
        }

        public EpubBookRef OpenBook(Stream stream)
        {
            return OpenBookAsync(stream).ExecuteAndUnwrapAggregateException();
        }

        public Task<EpubBookRef> OpenBookAsync(string filePath)
        {
            if (!environmentDependencies.FileSystem.FileExists(filePath))
            {
                throw new FileNotFoundException("Specified EPUB file not found.", filePath);
            }
            return OpenBookAsync(GetZipFile(filePath), filePath);
        }

        public Task<EpubBookRef> OpenBookAsync(Stream stream)
        {
            return OpenBookAsync(GetZipFile(stream), null);
        }

        private async Task<EpubBookRef> OpenBookAsync(IZipFile zipFile, string filePath)
        {
            EpubBookRef result = null;
            result = new EpubBookRef(zipFile);
            try
            {
                result.FilePath = filePath;
                SchemaReader schemaReader = new SchemaReader(epubReaderOptions);
                result.Schema = await schemaReader.ReadSchemaAsync(zipFile).ConfigureAwait(false);
                result.Title = result.Schema.Package.Metadata.Titles.FirstOrDefault() ?? String.Empty;
                result.AuthorList = result.Schema.Package.Metadata.Creators.Select(creator => creator.Creator).ToList();
                result.Author = String.Join(", ", result.AuthorList);
                result.Description = result.Schema.Package.Metadata.Description;
                ContentReader contentReader = new ContentReader(environmentDependencies, epubReaderOptions);
                result.Content = await Task.Run(() => contentReader.ParseContentMap(result)).ConfigureAwait(false);
                return result;
            }
            catch
            {
                result.Dispose();
                throw;
            }
        }

        private IZipFile GetZipFile(string filePath)
        {
            return environmentDependencies.FileSystem.OpenZipFile(filePath);
        }

        private IZipFile GetZipFile(Stream stream)
        {
            return environmentDependencies.FileSystem.OpenZipFile(stream);
        }
    }
}
