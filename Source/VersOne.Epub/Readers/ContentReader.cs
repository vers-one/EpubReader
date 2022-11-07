using System;
using System.Collections.Generic;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal class ContentReader
    {
        private readonly IEnvironmentDependencies environmentDependencies;
        private readonly EpubReaderOptions epubReaderOptions;

        public ContentReader(IEnvironmentDependencies environmentDependencies, EpubReaderOptions epubReaderOptions)
        {
            this.environmentDependencies = environmentDependencies ?? throw new ArgumentNullException(nameof(environmentDependencies));
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public EpubContentRef ParseContentMap(EpubBookRef bookRef)
        {
            EpubContentRef result = new EpubContentRef
            {
                Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalTextContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                },
                Css = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalTextContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                },
                Images = new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalByteContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
                },
                Fonts = new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalByteContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
                },
                AllFiles = new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteContentFileRef>()
                }
            };
            EpubLocalContentLoader localContentLoader =
                new EpubLocalContentLoader(environmentDependencies, epubReaderOptions.ContentReaderOptions, bookRef.EpubFile, bookRef.Schema.ContentDirectoryPath);
            EpubRemoteContentLoader remoteContentLoader = null;
            foreach (EpubManifestItem manifestItem in bookRef.Schema.Package.Manifest.Items)
            {
                string href = manifestItem.Href;
                EpubContentLocation contentLocation = href.Contains("://") ? EpubContentLocation.REMOTE : EpubContentLocation.LOCAL;
                string contentMimeType = manifestItem.MediaType;
                EpubContentType contentType = GetContentTypeByContentMimeType(contentMimeType);
                string contentDirectoryPath = bookRef.Schema.ContentDirectoryPath;
                EpubContentFileRefMetadata contentFileRefMetadata = new EpubContentFileRefMetadata(href, contentType, contentMimeType);
                switch (contentType)
                {
                    case EpubContentType.XHTML_1_1:
                    case EpubContentType.CSS:
                    case EpubContentType.OEB1_DOCUMENT:
                    case EpubContentType.OEB1_CSS:
                    case EpubContentType.XML:
                    case EpubContentType.DTBOOK:
                    case EpubContentType.DTBOOK_NCX:
                        if (contentLocation == EpubContentLocation.LOCAL)
                        {
                            string contentFilePath = ZipPathUtils.Combine(contentDirectoryPath, href);
                            EpubLocalTextContentFileRef localTextContentFile = new EpubLocalTextContentFileRef(contentFileRefMetadata, contentFilePath, localContentLoader);
                            switch (contentType)
                            {
                                case EpubContentType.XHTML_1_1:
                                    result.Html.Local[href] = localTextContentFile;
                                    if (result.NavigationHtmlFile == null && manifestItem.Properties != null && manifestItem.Properties.Contains(EpubManifestProperty.NAV))
                                    {
                                        result.NavigationHtmlFile = localTextContentFile;
                                    }
                                    break;
                                case EpubContentType.CSS:
                                    result.Css.Local[href] = localTextContentFile;
                                    break;
                            }
                            result.AllFiles.Local[href] = localTextContentFile;
                        }
                        else
                        {
                            if (remoteContentLoader == null)
                            {
                                remoteContentLoader = new EpubRemoteContentLoader(environmentDependencies, epubReaderOptions.ContentDownloaderOptions);
                            }
                            EpubRemoteTextContentFileRef remoteTextContentFile = new EpubRemoteTextContentFileRef(contentFileRefMetadata, remoteContentLoader);
                            switch (contentType)
                            {
                                case EpubContentType.XHTML_1_1:
                                    result.Html.Remote[href] = remoteTextContentFile;
                                    if (manifestItem.Properties != null && manifestItem.Properties.Contains(EpubManifestProperty.NAV))
                                    {
                                        throw new EpubPackageException($"Incorrect EPUB manifest: EPUB 3 navigation document \"{href}\" cannot be a remote resource.");
                                    }
                                    break;
                                case EpubContentType.CSS:
                                    result.Css.Remote[href] = remoteTextContentFile;
                                    break;
                            }
                            result.AllFiles.Remote[href] = remoteTextContentFile;
                        }
                        break;
                    default:
                        if (contentLocation == EpubContentLocation.LOCAL)
                        {
                            string contentFilePath = ZipPathUtils.Combine(contentDirectoryPath, href);
                            EpubLocalByteContentFileRef localByteContentFile = new EpubLocalByteContentFileRef(contentFileRefMetadata, contentFilePath, localContentLoader);
                            switch (contentType)
                            {
                                case EpubContentType.IMAGE_GIF:
                                case EpubContentType.IMAGE_JPEG:
                                case EpubContentType.IMAGE_PNG:
                                case EpubContentType.IMAGE_SVG:
                                    result.Images.Local[href] = localByteContentFile;
                                    break;
                                case EpubContentType.FONT_TRUETYPE:
                                case EpubContentType.FONT_OPENTYPE:
                                    result.Fonts.Local[href] = localByteContentFile;
                                    break;
                            }
                            result.AllFiles.Local[href] = localByteContentFile;
                        }
                        else
                        {
                            EpubRemoteByteContentFileRef remoteByteContentFile = new EpubRemoteByteContentFileRef(contentFileRefMetadata, remoteContentLoader);
                            switch (contentType)
                            {
                                case EpubContentType.IMAGE_GIF:
                                case EpubContentType.IMAGE_JPEG:
                                case EpubContentType.IMAGE_PNG:
                                case EpubContentType.IMAGE_SVG:
                                    result.Images.Remote[href] = remoteByteContentFile;
                                    break;
                                case EpubContentType.FONT_TRUETYPE:
                                case EpubContentType.FONT_OPENTYPE:
                                    result.Fonts.Remote[href] = remoteByteContentFile;
                                    break;
                            }
                            result.AllFiles.Remote[href] = remoteByteContentFile;
                        }
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
