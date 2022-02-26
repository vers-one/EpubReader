using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal static class SchemaReader
    {
        public static async Task<EpubSchema> ReadSchemaAsync(IZipFile epubFile)
        {
            EpubSchema result = new EpubSchema();
            string rootFilePath = await RootFilePathReader.GetRootFilePathAsync(epubFile).ConfigureAwait(false);
            string contentDirectoryPath = ZipPathUtils.GetDirectoryPath(rootFilePath);
            result.ContentDirectoryPath = contentDirectoryPath;
            EpubPackage package = await PackageReader.ReadPackageAsync(epubFile, rootFilePath).ConfigureAwait(false);
            result.Package = package;
            result.Epub2Ncx = await Epub2NcxReader.ReadEpub2NcxAsync(epubFile, contentDirectoryPath, package).ConfigureAwait(false);
            result.Epub3NavDocument = await Epub3NavDocumentReader.ReadEpub3NavDocumentAsync(epubFile, contentDirectoryPath, package).ConfigureAwait(false);
            return result;
        }
    }
}
