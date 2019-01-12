using System.IO.Compression;
using System.Threading.Tasks;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal static class SchemaReader
    {
        public static async Task<EpubSchema> ReadSchemaAsync(ZipArchive epubArchive)
        {
            EpubSchema result = new EpubSchema();
            string rootFilePath = await RootFilePathReader.GetRootFilePathAsync(epubArchive).ConfigureAwait(false);
            string contentDirectoryPath = ZipPathUtils.GetDirectoryPath(rootFilePath);
            result.ContentDirectoryPath = contentDirectoryPath;
            EpubPackage package = await PackageReader.ReadPackageAsync(epubArchive, rootFilePath).ConfigureAwait(false);
            result.Package = package;
            if (package.EpubVersion == EpubVersion.EPUB_2)
            {
                Epub2Ncx epub2Ncx = await Epub2NcxReader.ReadEpub2NcxAsync(epubArchive, contentDirectoryPath, package).ConfigureAwait(false);
                result.Navigation = epub2Ncx;
            }
            else
            {
                Epub3NavDocument epub3NavDocumentepub3Nav = await Epub3NavDocumentReader.ReadEpub3NavDocumentAsync(epubArchive, contentDirectoryPath, package).ConfigureAwait(false);
            }
            return result;
        }
    }
}
