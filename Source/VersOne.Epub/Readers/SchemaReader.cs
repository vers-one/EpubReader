using System;
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
            result.Epub2Ncx = await Epub2NcxReader.ReadEpub2NcxAsync(epubArchive, contentDirectoryPath, package).ConfigureAwait(false);
            try
            {
                result.Epub3NavDocument = await Epub3NavDocumentReader.ReadEpub3NavDocumentAsync(epubArchive, contentDirectoryPath, package).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result.Epub3NavDocument = null;
            }
            return result;
        }
    }
}
