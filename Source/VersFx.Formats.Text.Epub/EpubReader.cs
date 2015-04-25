using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using VersFx.Formats.Text.Epub.Readers;
using VersFx.Formats.Text.Epub.Schema.Navigation;
using VersFx.Formats.Text.Epub.Schema.Opf;

namespace VersFx.Formats.Text.Epub
{
    public class EpubReader
    {
        public EpubBook OpenBook(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Specified epub file not found.", filePath);
            using (ZipArchive epubArchive = ZipFile.OpenRead(filePath))
            {
                string rootFilePath = RootFilePathReader.GetRootFilePath(epubArchive);
                EpubBook book = new EpubBook();
                EpubPackage package = PackageReader.ReadPackage(epubArchive, rootFilePath);
                book.Package = package;
                string baseDirectoryEntryPath = Path.GetDirectoryName(rootFilePath);
                EpubNavigation navigation = NavigationReader.ReadNavigation(epubArchive, baseDirectoryEntryPath, package);
                book.Navigation = navigation;
                List<EpubContentFile> contentFiles = ContentFilesReader.ReadContentFiles(epubArchive, baseDirectoryEntryPath, package.Manifest);
                book.ContentFiles = contentFiles;
                return book;
            }
        }
    }
}
