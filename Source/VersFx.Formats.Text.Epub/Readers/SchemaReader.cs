using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersFx.Formats.Text.Epub.Entities;
using VersFx.Formats.Text.Epub.Schema.Navigation;
using VersFx.Formats.Text.Epub.Schema.Opf;

namespace VersFx.Formats.Text.Epub.Readers
{
    internal static class SchemaReader
    {
        public static EpubSchema ReadSchema(ZipArchive epubArchive)
        {
            EpubSchema result = new EpubSchema();
            string rootFilePath = RootFilePathReader.GetRootFilePath(epubArchive);
            string contentDirectoryPath = Path.GetDirectoryName(rootFilePath);
            result.ContentDirectoryPath = contentDirectoryPath;
            EpubPackage package = PackageReader.ReadPackage(epubArchive, rootFilePath);
            result.Package = package;
            EpubNavigation navigation = NavigationReader.ReadNavigation(epubArchive, contentDirectoryPath, package);
            result.Navigation = navigation;
            return result;
        }
    }
}
