using System.Collections.Generic;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal class SchemaReader
    {
        private readonly EpubReaderOptions epubReaderOptions;

        public SchemaReader(EpubReaderOptions? epubReaderOptions = null)
        {
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public async Task<EpubSchema> ReadSchemaAsync(IZipFile epubFile)
        {
            RootFilePathReader rootFilePathReader = new(epubReaderOptions);
            string rootFilePath = await rootFilePathReader.GetRootFilePathAsync(epubFile).ConfigureAwait(false);
            string contentDirectoryPath = ZipPathUtils.GetDirectoryPath(rootFilePath);
            PackageReader packageReader = new(epubReaderOptions);
            EpubPackage package = await packageReader.ReadPackageAsync(epubFile, rootFilePath).ConfigureAwait(false);
            Epub2NcxReader epub2NcxReader = new(epubReaderOptions);
            Epub2Ncx? epub2Ncx = await epub2NcxReader.ReadEpub2NcxAsync(epubFile, contentDirectoryPath, package).ConfigureAwait(false);
            Epub3NavDocumentReader epub3NavDocumentReader = new(epubReaderOptions);
            Epub3NavDocument? epub3NavDocument = await epub3NavDocumentReader.ReadEpub3NavDocumentAsync(epubFile, contentDirectoryPath, package).ConfigureAwait(false);
            SmilReader smilReader = new(epubReaderOptions);
            List<Smil> mediaOverlays = await smilReader.ReadAllSmilDocumentsAsync(epubFile, contentDirectoryPath, package).ConfigureAwait(false);
            return new(package, epub2Ncx, epub3NavDocument, mediaOverlays, contentDirectoryPath);
        }
    }
}
