using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal static class BookRefReader
    {
        public static EpubBookRef OpenBook(string filePath, EpubReaderOptions epubReaderOptions, IFileSystem fileSystem)
        {
            return OpenBookAsync(filePath, epubReaderOptions, fileSystem).ExecuteAndUnwrapAggregateException();
        }

        public static EpubBookRef OpenBook(Stream stream, EpubReaderOptions epubReaderOptions, IFileSystem fileSystem)
        {
            return OpenBookAsync(stream, epubReaderOptions, fileSystem).ExecuteAndUnwrapAggregateException();
        }

        public static Task<EpubBookRef> OpenBookAsync(string filePath, EpubReaderOptions epubReaderOptions, IFileSystem fileSystem)
        {
            if (!fileSystem.FileExists(filePath))
            {
                throw new FileNotFoundException("Specified EPUB file not found.", filePath);
            }
            return OpenBookAsync(GetZipFile(filePath, fileSystem), filePath, epubReaderOptions);
        }

        public static Task<EpubBookRef> OpenBookAsync(Stream stream, EpubReaderOptions epubReaderOptions, IFileSystem fileSystem)
        {
            return OpenBookAsync(GetZipFile(stream, fileSystem), null, epubReaderOptions);
        }

        private static async Task<EpubBookRef> OpenBookAsync(IZipFile zipFile, string filePath, EpubReaderOptions epubReaderOptions)
        {
            EpubBookRef result = null;
            if (epubReaderOptions == null)
            {
                epubReaderOptions = new EpubReaderOptions();
            }
            result = new EpubBookRef(zipFile);
            try
            {
                result.FilePath = filePath;
                result.Schema = await SchemaReader.ReadSchemaAsync(zipFile, epubReaderOptions).ConfigureAwait(false);
                result.Title = result.Schema.Package.Metadata.Titles.FirstOrDefault() ?? String.Empty;
                result.AuthorList = result.Schema.Package.Metadata.Creators.Select(creator => creator.Creator).ToList();
                result.Author = String.Join(", ", result.AuthorList);
                result.Description = result.Schema.Package.Metadata.Description;
                result.Content = await Task.Run(() => ContentReader.ParseContentMap(result, epubReaderOptions.ContentReaderOptions)).ConfigureAwait(false);
                return result;
            }
            catch
            {
                result.Dispose();
                throw;
            }
        }

        private static IZipFile GetZipFile(string filePath, IFileSystem fileSystem)
        {
            return fileSystem.OpenZipFile(filePath);
        }

        private static IZipFile GetZipFile(Stream stream, IFileSystem fileSystem)
        {
            return fileSystem.OpenZipFile(stream);
        }
    }
}
