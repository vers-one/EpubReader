using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using VersFx.Formats.Text.Epub.Entities;
using VersFx.Formats.Text.Epub.Readers;
using VersFx.Formats.Text.Epub.Schema.Opf;

namespace VersFx.Formats.Text.Epub
{
    public class EpubReader
    {
        public EpubBook OpenBook(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Specified epub file not found.", filePath);
            EpubBook book = new EpubBook();
            book.FilePath = filePath;
            using (ZipArchive epubArchive = ZipFile.OpenRead(filePath))
            {
                EpubSchema schema = SchemaReader.ReadSchema(epubArchive);
                book.Schema = schema;
                book.Title = book.Schema.Package.Metadata.Titles.FirstOrDefault() ?? String.Empty;
                List<EpubContentFile> contentFiles = ContentFileReader.ReadContentFiles(epubArchive, book.Schema.ContentDirectoryPath, book.Schema.Package.Manifest);
                book.ContentFiles = contentFiles;
            }
            return book;
        }

        public Image GetCoverImage(EpubBook book)
        {
            List<EpubMetadataMeta> metaItems = book.Schema.Package.Metadata.MetaItems;
            if (metaItems == null || !metaItems.Any())
                return null;
            EpubMetadataMeta coverMetaItem = metaItems.FirstOrDefault(metaItem => String.Compare(metaItem.Name, "cover", StringComparison.OrdinalIgnoreCase) == 0);
            if (coverMetaItem == null)
                return null;
            if (String.IsNullOrEmpty(coverMetaItem.Content))
                throw new Exception("Incorrect EPUB metadata: cover item content is missing");
            EpubManifestItem coverManifestItem = book.Schema.Package.Manifest.FirstOrDefault(manifestItem => String.CompareOrdinal(manifestItem.Id, coverMetaItem.Content) == 0);
            if (coverManifestItem == null)
                throw new Exception(String.Format("Incorrect EPUB manifest: item with ID = \"{0}\" is missing", coverMetaItem.Content));
            string contentFilePath = String.Concat(book.Schema.ContentDirectoryPath, "/", coverManifestItem.Href);
            using (ZipArchive epubArchive = ZipFile.OpenRead(book.FilePath))
            {
                Stream coverImageStream = ContentFileReader.OpenContentFile(epubArchive, contentFilePath);
                if (coverImageStream == null)
                    throw new Exception(String.Format("Incorrect EPUB file: cover image \"{0}\" specified in metadata is not found", contentFilePath));
                return Image.FromStream(coverImageStream);
            }
        }
    }
}
