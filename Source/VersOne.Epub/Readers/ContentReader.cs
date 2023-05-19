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

        public ContentReader(IEnvironmentDependencies environmentDependencies, EpubReaderOptions? epubReaderOptions = null)
        {
            this.environmentDependencies = environmentDependencies ?? throw new ArgumentNullException(nameof(environmentDependencies));
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public EpubContentRef ParseContentMap(EpubSchema epubSchema, IZipFile epubFile)
        {
            EpubLocalByteContentFileRef? cover;
            EpubLocalTextContentFileRef? navigationHtmlFile = null;
            List<EpubLocalTextContentFileRef> htmlLocal = new();
            List<EpubRemoteTextContentFileRef> htmlRemote = new();
            List<EpubLocalTextContentFileRef> cssLocal = new();
            List<EpubRemoteTextContentFileRef> cssRemote = new();
            List<EpubLocalByteContentFileRef> imagesLocal = new();
            List<EpubRemoteByteContentFileRef> imagesRemote = new();
            List<EpubLocalByteContentFileRef> fontsLocal = new();
            List<EpubRemoteByteContentFileRef> fontsRemote = new();
            List<EpubLocalByteContentFileRef> audioLocal = new();
            List<EpubRemoteByteContentFileRef> audioRemote = new();
            List<EpubLocalContentFileRef> allFilesLocal = new();
            List<EpubRemoteContentFileRef> allFilesRemote = new();
            EpubLocalContentLoader localContentLoader = new(environmentDependencies, epubFile, epubSchema.ContentDirectoryPath, epubReaderOptions.ContentReaderOptions);
            EpubRemoteContentLoader? remoteContentLoader = null;
            foreach (EpubManifestItem manifestItem in epubSchema.Package.Manifest.Items)
            {
                string href = manifestItem.Href;
                EpubContentLocation contentLocation = ContentPathUtils.IsLocalPath(href) ? EpubContentLocation.LOCAL : EpubContentLocation.REMOTE;
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
                    case EpubContentType.SCRIPT:
                        if (contentLocation == EpubContentLocation.LOCAL)
                        {
                            string contentFilePath = ContentPathUtils.Combine(contentDirectoryPath, href);
                            EpubLocalTextContentFileRef localTextContentFile = new(contentFileRefMetadata, contentFilePath, localContentLoader);
                            switch (contentType)
                            {
                                case EpubContentType.XHTML_1_1:
                                    htmlLocal.Add(localTextContentFile);
                                    if (navigationHtmlFile == null && manifestItem.Properties != null && manifestItem.Properties.Contains(EpubManifestProperty.NAV))
                                    {
                                        navigationHtmlFile = localTextContentFile;
                                    }
                                    break;
                                case EpubContentType.CSS:
                                    cssLocal.Add(localTextContentFile);
                                    break;
                            }
                            allFilesLocal.Add(localTextContentFile);
                        }
                        else
                        {
                            remoteContentLoader ??= new EpubRemoteContentLoader(environmentDependencies, epubReaderOptions.ContentDownloaderOptions);
                            EpubRemoteTextContentFileRef remoteTextContentFile = new(contentFileRefMetadata, remoteContentLoader);
                            switch (contentType)
                            {
                                case EpubContentType.XHTML_1_1:
                                    if (manifestItem.Properties != null && manifestItem.Properties.Contains(EpubManifestProperty.NAV))
                                    {
                                        throw new EpubPackageException($"Incorrect EPUB manifest: EPUB 3 navigation document \"{href}\" cannot be a remote resource.");
                                    }
                                    htmlRemote.Add(remoteTextContentFile);
                                    break;
                                case EpubContentType.CSS:
                                    cssRemote.Add(remoteTextContentFile);
                                    break;
                            }
                            allFilesRemote.Add(remoteTextContentFile);
                        }
                        break;
                    default:
                        if (contentLocation == EpubContentLocation.LOCAL)
                        {
                            string contentFilePath = ContentPathUtils.Combine(contentDirectoryPath, href);
                            EpubLocalByteContentFileRef localByteContentFile = new(contentFileRefMetadata, contentFilePath, localContentLoader);
                            switch (contentType)
                            {
                                case EpubContentType.IMAGE_GIF:
                                case EpubContentType.IMAGE_JPEG:
                                case EpubContentType.IMAGE_PNG:
                                case EpubContentType.IMAGE_SVG:
                                case EpubContentType.IMAGE_WEBP:
                                    imagesLocal.Add(localByteContentFile);
                                    break;
                                case EpubContentType.FONT_TRUETYPE:
                                case EpubContentType.FONT_OPENTYPE:
                                case EpubContentType.FONT_SFNT:
                                case EpubContentType.FONT_WOFF:
                                case EpubContentType.FONT_WOFF2:
                                    fontsLocal.Add(localByteContentFile);
                                    break;
                                case EpubContentType.AUDIO_MP3:
                                case EpubContentType.AUDIO_MP4:
                                case EpubContentType.AUDIO_OGG:
                                    audioLocal.Add(localByteContentFile);
                                    break;
                            }
                            allFilesLocal.Add(localByteContentFile);
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
                                case EpubContentType.IMAGE_WEBP:
                                    imagesRemote.Add(remoteByteContentFile);
                                    break;
                                case EpubContentType.FONT_TRUETYPE:
                                case EpubContentType.FONT_OPENTYPE:
                                case EpubContentType.FONT_SFNT:
                                case EpubContentType.FONT_WOFF:
                                case EpubContentType.FONT_WOFF2:
                                    fontsRemote.Add(remoteByteContentFile);
                                    break;
                                case EpubContentType.AUDIO_MP3:
                                case EpubContentType.AUDIO_MP4:
                                case EpubContentType.AUDIO_OGG:
                                    audioRemote.Add(remoteByteContentFile);
                                    break;
                            }
                            allFilesRemote.Add(remoteByteContentFile);
                        }
                        break;
                }
            }
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> html = new(htmlLocal.AsReadOnly(), htmlRemote.AsReadOnly());
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> css = new(cssLocal.AsReadOnly(), cssRemote.AsReadOnly());
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> images = new(imagesLocal.AsReadOnly(), imagesRemote.AsReadOnly());
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> fonts = new(fontsLocal.AsReadOnly(), fontsRemote.AsReadOnly());
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> audio = new(audioLocal.AsReadOnly(), audioRemote.AsReadOnly());
            EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef> allFiles = new(allFilesLocal.AsReadOnly(), allFilesRemote.AsReadOnly());
            cover = BookCoverReader.ReadBookCover(epubSchema, images);
            return new(cover, navigationHtmlFile, html, css, images, fonts, audio, allFiles);
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
                "application/javascript" or "application/ecmascript" or "text/javascript" => EpubContentType.SCRIPT,
                "image/gif" => EpubContentType.IMAGE_GIF,
                "image/jpeg" => EpubContentType.IMAGE_JPEG,
                "image/png" => EpubContentType.IMAGE_PNG,
                "image/svg+xml" => EpubContentType.IMAGE_SVG,
                "image/webp" => EpubContentType.IMAGE_WEBP,
                "font/truetype" or "font/ttf" or "application/x-font-truetype" => EpubContentType.FONT_TRUETYPE,
                "font/opentype" or "font/otf" or "application/vnd.ms-opentype" => EpubContentType.FONT_OPENTYPE,
                "font/sfnt" or "application/font-sfnt" => EpubContentType.FONT_SFNT,
                "font/woff" or "application/font-woff" => EpubContentType.FONT_WOFF,
                "font/woff2" => EpubContentType.FONT_WOFF2,
                "application/smil+xml" => EpubContentType.SMIL,
                "audio/mpeg" => EpubContentType.AUDIO_MP3,
                "audio/mp4" => EpubContentType.AUDIO_MP4,
                "audio/ogg" or "audio/ogg; codecs=opus" => EpubContentType.AUDIO_OGG,
                _ => EpubContentType.OTHER
            };
        }
    }
}
