using System;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal class ContentReader
    {
        private readonly IEnvironmentDependencies environmentDependencies;
        private readonly EpubReaderOptions epubReaderOptions;

        public ContentReader(IEnvironmentDependencies environmentDependencies, EpubReaderOptions? epubReaderOptions = null)
        {
            this.environmentDependencies = environmentDependencies ?? throw new ArgumentNullException(nameof(environmentDependencies));
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public EpubContentRef ParseContentMap(EpubSchema epubSchema, IZipFile epubFile)
        {
            EpubLocalByteContentFileRef? cover;
            EpubLocalTextContentFileRef? navigationHtmlFile = null;
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> html = new();
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> css = new();
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> images = new();
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> fonts = new();
            EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef> allFiles = new();
            EpubLocalContentLoader localContentLoader = new(environmentDependencies, epubFile, epubSchema.ContentDirectoryPath, epubReaderOptions.ContentReaderOptions);
            EpubRemoteContentLoader? remoteContentLoader = null;
            foreach (EpubManifestItem manifestItem in epubSchema.Package.Manifest.Items)
            {
                string href = manifestItem.Href;
                EpubContentLocation contentLocation = href.Contains("://") ? EpubContentLocation.REMOTE : EpubContentLocation.LOCAL;
                string contentMimeType = manifestItem.MediaType;
                EpubContentType contentType = GetContentTypeByContentMimeType(contentMimeType);
                string contentDirectoryPath = epubSchema.ContentDirectoryPath;
                EpubContentFileRefMetadata contentFileRefMetadata = new(href, contentType, contentMimeType);
                switch (contentType)
                {
                    case EpubContentType.XHTML_1_1:
                    case EpubContentType.CSS:
                    case EpubContentType.OEB1_DOCUMENT:
                    case EpubContentType.OEB1_CSS:
                    case EpubContentType.XML:
                    case EpubContentType.DTBOOK:
                    case EpubContentType.DTBOOK_NCX:
                    case EpubContentType.SMIL:
                        if (contentLocation == EpubContentLocation.LOCAL)
                        {
                            string contentFilePath = ZipPathUtils.Combine(contentDirectoryPath, href);
                            EpubLocalTextContentFileRef localTextContentFile = new(contentFileRefMetadata, contentFilePath, localContentLoader);
                            switch (contentType)
                            {
                                case EpubContentType.XHTML_1_1:
                                    html.Local[href] = localTextContentFile;
                                    if (navigationHtmlFile == null && manifestItem.Properties != null && manifestItem.Properties.Contains(EpubManifestProperty.NAV))
                                    {
                                        navigationHtmlFile = localTextContentFile;
                                    }
                                    break;
                                case EpubContentType.CSS:
                                    css.Local[href] = localTextContentFile;
                                    break;
                            }
                            allFiles.Local[href] = localTextContentFile;
                        }
                        else
                        {
                            remoteContentLoader ??= new EpubRemoteContentLoader(environmentDependencies, epubReaderOptions.ContentDownloaderOptions);
                            EpubRemoteTextContentFileRef remoteTextContentFile = new(contentFileRefMetadata, remoteContentLoader);
                            switch (contentType)
                            {
                                case EpubContentType.XHTML_1_1:
                                    html.Remote[href] = remoteTextContentFile;
                                    if (manifestItem.Properties != null && manifestItem.Properties.Contains(EpubManifestProperty.NAV))
                                    {
                                        throw new EpubPackageException($"Incorrect EPUB manifest: EPUB 3 navigation document \"{href}\" cannot be a remote resource.");
                                    }
                                    break;
                                case EpubContentType.CSS:
                                    css.Remote[href] = remoteTextContentFile;
                                    break;
                            }
                            allFiles.Remote[href] = remoteTextContentFile;
                        }
                        break;
                    default:
                        if (contentLocation == EpubContentLocation.LOCAL)
                        {
                            string contentFilePath = ZipPathUtils.Combine(contentDirectoryPath, href);
                            EpubLocalByteContentFileRef localByteContentFile = new(contentFileRefMetadata, contentFilePath, localContentLoader);
                            switch (contentType)
                            {
                                case EpubContentType.IMAGE_GIF:
                                case EpubContentType.IMAGE_JPEG:
                                case EpubContentType.IMAGE_PNG:
                                case EpubContentType.IMAGE_SVG:
                                    images.Local[href] = localByteContentFile;
                                    break;
                                case EpubContentType.FONT_TRUETYPE:
                                case EpubContentType.FONT_OPENTYPE:
                                    fonts.Local[href] = localByteContentFile;
                                    break;
                            }
                            allFiles.Local[href] = localByteContentFile;
                        }
                        else
                        {
                            remoteContentLoader ??= new EpubRemoteContentLoader(environmentDependencies, epubReaderOptions.ContentDownloaderOptions);
                            EpubRemoteByteContentFileRef remoteByteContentFile = new(contentFileRefMetadata, remoteContentLoader);
                            switch (contentType)
                            {
                                case EpubContentType.IMAGE_GIF:
                                case EpubContentType.IMAGE_JPEG:
                                case EpubContentType.IMAGE_PNG:
                                case EpubContentType.IMAGE_SVG:
                                    images.Remote[href] = remoteByteContentFile;
                                    break;
                                case EpubContentType.FONT_TRUETYPE:
                                case EpubContentType.FONT_OPENTYPE:
                                    fonts.Remote[href] = remoteByteContentFile;
                                    break;
                            }
                            allFiles.Remote[href] = remoteByteContentFile;
                        }
                        break;
                }
            }
            cover = BookCoverReader.ReadBookCover(epubSchema, images);
            return new(cover, navigationHtmlFile, html, css, images, fonts, allFiles);
        }

        private static EpubContentType GetContentTypeByContentMimeType(string contentMimeType)
        {
            return contentMimeType.ToLowerInvariant() switch
            {
                "application/xhtml+xml" => EpubContentType.XHTML_1_1,
                "application/x-dtbook+xml" => EpubContentType.DTBOOK,
                "application/x-dtbncx+xml" => EpubContentType.DTBOOK_NCX,
                "text/x-oeb1-document" => EpubContentType.OEB1_DOCUMENT,
                "application/xml" => EpubContentType.XML,
                "text/css" => EpubContentType.CSS,
                "text/x-oeb1-css" => EpubContentType.OEB1_CSS,
                "image/gif" => EpubContentType.IMAGE_GIF,
                "image/jpeg" => EpubContentType.IMAGE_JPEG,
                "image/png" => EpubContentType.IMAGE_PNG,
                "image/svg+xml" => EpubContentType.IMAGE_SVG,
                "font/truetype" or "application/x-font-truetype" => EpubContentType.FONT_TRUETYPE,
                "font/opentype" or "application/vnd.ms-opentype" => EpubContentType.FONT_OPENTYPE,
                "application/smil+xml" => EpubContentType.SMIL,
                _ => EpubContentType.OTHER
            };
        }
    }
}
