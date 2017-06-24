using System.IO.Compression;
using System.Threading.Tasks;
using Didstopia.EpubReader.Schema.Navigation;
using Didstopia.EpubReader.Schema.Opf;
using Didstopia.EpubReader.Utils;

namespace Didstopia.EpubReader.Readers
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
            EpubNavigation navigation = await NavigationReader.ReadNavigationAsync(epubArchive, contentDirectoryPath, package).ConfigureAwait(false);
            result.Navigation = navigation;
            return result;
        }
    }
}
