using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using VersFx.Formats.Text.Epub.Entities;
using VersFx.Formats.Text.Epub.Readers;
using VersFx.Formats.Text.Epub.Schema.Navigation;
using VersFx.Formats.Text.Epub.Schema.Opf;
using VersFx.Formats.Text.Epub.Utils;

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
                book.Schema = SchemaReader.ReadSchema(epubArchive);
                book.Title = book.Schema.Package.Metadata.Titles.FirstOrDefault() ?? String.Empty;
                book.Chapters = LoadChapters(book, epubArchive);
                book.ContentFiles = LoadContentFiles(book, epubArchive);
            }
            return book;
        }

        public Image LoadCoverImage(EpubBook book)
        {
            List<EpubMetadataMeta> metaItems = book.Schema.Package.Metadata.MetaItems;
            if (metaItems == null || !metaItems.Any())
                return null;
            EpubMetadataMeta coverMetaItem = metaItems.FirstOrDefault(metaItem => String.Compare(metaItem.Name, "cover", StringComparison.OrdinalIgnoreCase) == 0);
            if (coverMetaItem == null)
                return null;
            if (String.IsNullOrEmpty(coverMetaItem.Content))
                throw new Exception("Incorrect EPUB metadata: cover item content is missing");
            EpubManifestItem coverManifestItem = book.Schema.Package.Manifest.FirstOrDefault(manifestItem => String.Compare(manifestItem.Id, coverMetaItem.Content, StringComparison.OrdinalIgnoreCase) == 0);
            if (coverManifestItem == null)
                throw new Exception(String.Format("Incorrect EPUB manifest: item with ID = \"{0}\" is missing", coverMetaItem.Content));
            using (ZipArchive epubArchive = ZipFile.OpenRead(book.FilePath))
            {
                Stream coverImageStream = ContentFileReader.OpenContentFile(epubArchive, book.Schema.ContentDirectoryPath, coverManifestItem.Href);
                if (coverImageStream == null)
                    throw new Exception(String.Format("Incorrect EPUB file: cover image \"{0}\" specified in metadata is not found", coverManifestItem.Href));
                return Image.FromStream(coverImageStream);
            }
        }

        public List<EpubChapter> LoadChapters(EpubBook book)
        {
            using (ZipArchive epubArchive = ZipFile.OpenRead(book.FilePath))
                return LoadChapters(book, epubArchive);
        }

        public List<EpubContentFile> LoadContentFiles(EpubBook book)
        {
            using (ZipArchive epubArchive = ZipFile.OpenRead(book.FilePath))
                return LoadContentFiles(book, epubArchive);
        }

        private List<EpubChapter> LoadChapters(EpubBook book, ZipArchive epubArchive)
        {
            return LoadChapters(book, book.Schema.Navigation.NavMap, epubArchive);
        }

        private List<EpubChapter> LoadChapters(EpubBook book, List<EpubNavigationPoint> navigationPoints, ZipArchive epubArchive)
        {
            List<EpubChapter> result = new List<EpubChapter>();
            foreach (EpubNavigationPoint navigationPoint in navigationPoints)
            {
                EpubChapter chapter = new EpubChapter();
                chapter.Title = navigationPoint.NavigationLabels.First().Text;
                int contentSourceAnchorCharIndex = navigationPoint.Content.Source.IndexOf('#');
                if (contentSourceAnchorCharIndex == -1)
                    chapter.ContentFileName = navigationPoint.Content.Source;
                else
                {
                    chapter.ContentFileName = navigationPoint.Content.Source.Substring(0, contentSourceAnchorCharIndex);
                    chapter.Anchor = navigationPoint.Content.Source.Substring(contentSourceAnchorCharIndex + 1);
                }
                EpubManifestItem contentManifestItem = book.Schema.Package.Manifest.FirstOrDefault(manifestItem => String.Compare(manifestItem.Href, chapter.ContentFileName, StringComparison.OrdinalIgnoreCase) == 0);
                if (contentManifestItem == null)
                    throw new Exception(String.Format("Incorrect EPUB manifest: item with href = \"{0}\" is missing", chapter.ContentFileName));
                chapter.ContentMimeType = contentManifestItem.MediaType;
                chapter.ContentType = GetContentTypeByContentMimeType(chapter.ContentMimeType);
                if (chapter.ContentType == EpubContentType.XHTML_1_1)
                {
                    Stream contentStream = ContentFileReader.OpenContentFile(epubArchive, book.Schema.ContentDirectoryPath, chapter.ContentFileName);
                    if (contentStream == null)
                        throw new Exception(String.Format("Incorrect EPUB file: content file \"{0}\" specified in manifest is not found", chapter.ContentFileName));
                    using (StreamReader streamReader = new StreamReader(contentStream))
                        chapter.HtmlContent = streamReader.ReadToEnd();
                }
                chapter.SubChapters = LoadChapters(book, navigationPoint.ChildNavigationPoints, epubArchive);
                result.Add(chapter);
            }
            return result;
        }

        private List<EpubContentFile> LoadContentFiles(EpubBook book, ZipArchive epubArchive)
        {
            return ContentFileReader.ReadContentFiles(epubArchive, book.Schema.ContentDirectoryPath, book.Schema.Package.Manifest);
        }

        private EpubContentType GetContentTypeByContentMimeType(string contentMimeType)
        {
            switch (contentMimeType.ToLowerInvariant())
            {
                case "application/xhtml+xml":
                    return EpubContentType.XHTML_1_1;
                case "application/x-dtbook+xml":
                    return EpubContentType.DTBOOK;
                case "application/x-dtbncx+xml":
                    return EpubContentType.DTBOOK_NCX;
                case "text/x-oeb1-document":
                    return EpubContentType.OEB1_DOCUMENT;
                case "application/xml":
                    return EpubContentType.XML;
                case "text/css":
                    return EpubContentType.CSS;
                case "text/x-oeb1-css":
                    return EpubContentType.OEB1_CSS;
                case "image/gif":
                    return EpubContentType.IMAGE_GIF;
                case "image/jpeg":
                    return EpubContentType.IMAGE_JPEG;
                case "image/png":
                    return EpubContentType.IMAGE_PNG;
                case "image/svg+xml":
                    return EpubContentType.IMAGE_SVG;
                case "font/truetype":
                    return EpubContentType.FONT_TRUETYPE;
                case "font/opentype":
                    return EpubContentType.FONT_OPENTYPE;
                case "application/vnd.ms-opentype":
                    return EpubContentType.FONT_OPENTYPE;
                default:
                    return EpubContentType.OTHER;
            }
        }
    }
}
