using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal class SchemaReader
    {
        private readonly EpubReaderOptions epubReaderOptions;

        public SchemaReader(EpubReaderOptions epubReaderOptions = null)
        {
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public async Task<EpubSchema> ReadSchemaAsync(IZipFile epubFile)
        {
            EpubSchema result = new EpubSchema();
            RootFilePathReader rootFilePathReader = new RootFilePathReader(epubReaderOptions);
            string rootFilePath = await rootFilePathReader.GetRootFilePathAsync(epubFile).ConfigureAwait(false);
            string contentDirectoryPath = ZipPathUtils.GetDirectoryPath(rootFilePath);
            result.ContentDirectoryPath = contentDirectoryPath;
            PackageReader packageReader = new PackageReader(epubReaderOptions);
            EpubPackage package = await packageReader.ReadPackageAsync(epubFile, rootFilePath).ConfigureAwait(false);
            result.Package = package;
            Epub2NcxReader epub2NcxReader = new Epub2NcxReader(epubReaderOptions);
            result.Epub2Ncx = await epub2NcxReader.ReadEpub2NcxAsync(epubFile, contentDirectoryPath, package).ConfigureAwait(false);
            Epub3NavDocumentReader epub3NavDocumentReader = new Epub3NavDocumentReader(epubReaderOptions);
            result.Epub3NavDocument = await epub3NavDocumentReader.ReadEpub3NavDocumentAsync(epubFile, contentDirectoryPath, package).ConfigureAwait(false);
            return result;
        }
    }
}
