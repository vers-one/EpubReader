using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using VersFx.Formats.Text.Epub.Entities;
using VersFx.Formats.Text.Epub.Readers;

namespace VersFx.Formats.Text.Epub
{
    public class EpubReader
    {
        public EpubBook OpenBook(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Specified epub file not found.", filePath);
            EpubBook book = new EpubBook();
            using (ZipArchive epubArchive = ZipFile.OpenRead(filePath))
            {
                EpubSchema schema = SchemaReader.ReadSchema(epubArchive);
                book.Schema = schema;
                List<EpubContentFile> contentFiles = ContentFilesReader.ReadContentFiles(epubArchive, book.Schema.ContentDirectoryPath, book.Schema.Package.Manifest);
                book.ContentFiles = contentFiles;
            }
            return book;
        }
    }
}
