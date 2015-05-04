using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using VersFx.Formats.Text.Epub.Entities;
using VersFx.Formats.Text.Epub.Schema.Opf;
using VersFx.Formats.Text.Epub.Utils;

namespace VersFx.Formats.Text.Epub.Readers
{
    internal static class ContentReader
    {
        public static EpubContent ReadContentFiles(ZipArchive epubArchive, EpubBook book)
        {
            EpubContent result = new EpubContent
            {
                Html = new Dictionary<string, EpubTextContentFile>(),
                Css = new Dictionary<string, EpubTextContentFile>(),
                Images = new Dictionary<string, EpubByteContentFile>(),
                Fonts = new Dictionary<string, EpubByteContentFile>(),
                AllFiles = new Dictionary<string, EpubContentFile>()
            };
            foreach (EpubManifestItem manifestItem in book.Schema.Package.Manifest)
            {
                string contentFilePath = ZipPathUtils.Combine(book.Schema.ContentDirectoryPath, manifestItem.Href);
                ZipArchiveEntry contentFileEntry = epubArchive.GetEntry(contentFilePath);
                if (contentFileEntry == null)
                    throw new Exception(String.Format("EPUB parsing error: file {0} not found in archive.", contentFilePath));
                if (contentFileEntry.Length > Int32.MaxValue)
                    throw new Exception(String.Format("EPUB parsing error: file {0} is bigger than 2 Gb.", contentFilePath));
                string fileName = manifestItem.Href;
                string contentMimeType = manifestItem.MediaType;
                EpubContentType contentType = GetContentTypeByContentMimeType(contentMimeType);
                switch (contentType)
                {
                    case EpubContentType.XHTML_1_1:
                    case EpubContentType.CSS:
                    case EpubContentType.OEB1_DOCUMENT:
                    case EpubContentType.OEB1_CSS:
                    case EpubContentType.XML:
                    case EpubContentType.DTBOOK:
                    case EpubContentType.DTBOOK_NCX:
                        EpubTextContentFile epubTextContentFile = new EpubTextContentFile
                        {
                            FileName = fileName,
                            ContentMimeType = contentMimeType,
                            ContentType = contentType
                        };
                        using (Stream contentStream = contentFileEntry.Open())
                        {
                            if (contentStream == null)
                                throw new Exception(String.Format("Incorrect EPUB file: content file \"{0}\" specified in manifest is not found", fileName));
                            using (StreamReader streamReader = new StreamReader(contentStream))
                                epubTextContentFile.Content = streamReader.ReadToEnd();
                        }
                        switch (contentType)
                        {
                            case EpubContentType.XHTML_1_1:
                                result.Html.Add(fileName, epubTextContentFile);
                                break;
                            case EpubContentType.CSS:
                                result.Css.Add(fileName, epubTextContentFile);
                                break;
                        }
                        result.AllFiles.Add(fileName, epubTextContentFile);
                        break;
                    default:
                        EpubByteContentFile epubByteContentFile = new EpubByteContentFile
                        {
                            FileName = fileName,
                            ContentMimeType = contentMimeType,
                            ContentType = contentType
                        };
                        using (Stream contentStream = contentFileEntry.Open())
                        {
                            if (contentStream == null)
                                throw new Exception(String.Format("Incorrect EPUB file: content file \"{0}\" specified in manifest is not found", fileName));
                            using (MemoryStream memoryStream = new MemoryStream((int)contentFileEntry.Length))
                            {
                                contentStream.CopyTo(memoryStream);
                                epubByteContentFile.Content = memoryStream.ToArray();
                            }
                        }
                        switch (contentType)
                        {
                            case EpubContentType.IMAGE_GIF:
                            case EpubContentType.IMAGE_JPEG:
                            case EpubContentType.IMAGE_PNG:
                            case EpubContentType.IMAGE_SVG:
                                result.Images.Add(fileName, epubByteContentFile);
                                break;
                            case EpubContentType.FONT_TRUETYPE:
                            case EpubContentType.FONT_OPENTYPE:
                                result.Fonts.Add(fileName, epubByteContentFile);
                                break;
                        }
                        result.AllFiles.Add(fileName, epubByteContentFile);
                        break;
                }
            }
            return result;
        }

        private static EpubContentType GetContentTypeByContentMimeType(string contentMimeType)
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
