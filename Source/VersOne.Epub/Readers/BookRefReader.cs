using System;
using System.Collections.Generic;
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

        public BookRefReader(IEnvironmentDependencies environmentDependencies, EpubReaderOptions? epubReaderOptions = null)
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

        private async Task<EpubBookRef> OpenBookAsync(IZipFile epubFile, string? filePath)
        {
            SchemaReader schemaReader = new(epubReaderOptions);
            EpubSchema schema = await schemaReader.ReadSchemaAsync(epubFile).ConfigureAwait(false);
            string title = schema.Package.Metadata.Titles.FirstOrDefault() ?? String.Empty;
            List<string> authorList = schema.Package.Metadata.Creators.Select(creator => creator.Creator).ToList();
            string author = String.Join(", ", authorList);
            string? description = schema.Package.Metadata.Description;
            ContentReader contentReader = new(environmentDependencies, epubReaderOptions);
            EpubContentRef content = await Task.Run(() => contentReader.ParseContentMap(schema, epubFile)).ConfigureAwait(false);
            return new(epubFile, filePath, title, author, authorList, description, schema, content);
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
