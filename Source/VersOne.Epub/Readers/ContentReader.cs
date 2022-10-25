using System.Collections.Generic;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal static class ContentReader
    {
        public static EpubContentRef ParseContentMap(EpubBookRef bookRef, ContentReaderOptions contentReaderOptions)
        {
            EpubContentRef result = new EpubContentRef
            {
                Html = new Dictionary<string, EpubTextContentFileRef>(),
                Css = new Dictionary<string, EpubTextContentFileRef>(),
                Images = new Dictionary<string, EpubByteContentFileRef>(),
                Fonts = new Dictionary<string, EpubByteContentFileRef>(),
                AllFiles = new Dictionary<string, EpubContentFileRef>()
            };
            foreach (EpubManifestItem manifestItem in bookRef.Schema.Package.Manifest.Items)
            {
                string href = manifestItem.Href;
                EpubContentLocation contentLocation = href.Contains("://") ? EpubContentLocation.REMOTE : EpubContentLocation.LOCAL;
                string contentMimeType = manifestItem.MediaType;
                EpubContentType contentType = GetContentTypeByContentMimeType(contentMimeType);
                IZipFile epubFile = bookRef.EpubFile;
                string contentDirectoryPath = bookRef.Schema.ContentDirectoryPath;
                switch (contentType)
                {
                    case EpubContentType.XHTML_1_1:
                    case EpubContentType.CSS:
                    case EpubContentType.OEB1_DOCUMENT:
                    case EpubContentType.OEB1_CSS:
                    case EpubContentType.XML:
                    case EpubContentType.DTBOOK:
                    case EpubContentType.DTBOOK_NCX:
                        EpubTextContentFileRef epubTextContentFile =
                            new EpubTextContentFileRef(href, contentLocation, contentType, contentMimeType, epubFile, contentDirectoryPath, contentReaderOptions);
                        switch (contentType)
                        {
                            case EpubContentType.XHTML_1_1:
                                result.Html[href] = epubTextContentFile;
                                if (result.NavigationHtmlFile == null && manifestItem.Properties != null && manifestItem.Properties.Contains(EpubManifestProperty.NAV))
                                {
                                    result.NavigationHtmlFile = epubTextContentFile;
                                }
                                break;
                            case EpubContentType.CSS:
                                result.Css[href] = epubTextContentFile;
                                break;
                        }
                        result.AllFiles[href] = epubTextContentFile;
                        break;
                    default:
                        EpubByteContentFileRef epubByteContentFile =
                            new EpubByteContentFileRef(href, contentLocation, contentType, contentMimeType, epubFile, contentDirectoryPath, contentReaderOptions);
                        switch (contentType)
                        {
                            case EpubContentType.IMAGE_GIF:
                            case EpubContentType.IMAGE_JPEG:
                            case EpubContentType.IMAGE_PNG:
                            case EpubContentType.IMAGE_SVG:
                                result.Images[href] = epubByteContentFile;
                                break;
                            case EpubContentType.FONT_TRUETYPE:
                            case EpubContentType.FONT_OPENTYPE:
                                result.Fonts[href] = epubByteContentFile;
                                break;
                        }
                        result.AllFiles[href] = epubByteContentFile;
                        break;
                }
            }
            result.Cover = BookCoverReader.ReadBookCover(bookRef.Schema, result.Images);
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
                case "application/x-font-truetype":
                    return EpubContentType.FONT_TRUETYPE;
                case "font/opentype":
                case "application/vnd.ms-opentype":
                    return EpubContentType.FONT_OPENTYPE;
                default:
                    return EpubContentType.OTHER;
            }
        }
    }
}
