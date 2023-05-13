using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal class BookReader
    {
        private readonly IEnvironmentDependencies environmentDependencies;
        private readonly EpubReaderOptions epubReaderOptions;

        public BookReader(IEnvironmentDependencies environmentDependencies, EpubReaderOptions? epubReaderOptions = null)
        {
            this.environmentDependencies = environmentDependencies ?? throw new ArgumentNullException(nameof(environmentDependencies));
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public EpubBook ReadBook(string filePath)
        {
            return ReadBookAsync(filePath).ExecuteAndUnwrapAggregateException();
        }

        public EpubBook ReadBook(Stream stream)
        {
            return ReadBookAsync(stream).ExecuteAndUnwrapAggregateException();
        }

        public async Task<EpubBook> ReadBookAsync(string filePath)
        {
            BookRefReader bookRefReader = new(environmentDependencies, epubReaderOptions);
            EpubBookRef epubBookRef = await bookRefReader.OpenBookAsync(filePath).ConfigureAwait(false);
            return await ReadBookAsync(epubBookRef).ConfigureAwait(false);
        }

        public async Task<EpubBook> ReadBookAsync(Stream stream)
        {
            BookRefReader bookRefReader = new(environmentDependencies, epubReaderOptions);
            EpubBookRef epubBookRef = await bookRefReader.OpenBookAsync(stream).ConfigureAwait(false);
            return await ReadBookAsync(epubBookRef).ConfigureAwait(false);
        }

        private static List<EpubLocalTextContentFile> ReadReadingOrder(EpubContent epubContent, List<EpubLocalTextContentFileRef> htmlContentFileRefs)
        {
            return htmlContentFileRefs.Select(htmlContentFileRef => epubContent.Html.Local[htmlContentFileRef.Key]).ToList();
        }

        private static List<EpubNavigationItem> ReadNavigation(EpubContent epubContent, List<EpubNavigationItemRef> navigationItemRefs)
        {
            List<EpubNavigationItem> result = new();
            foreach (EpubNavigationItemRef navigationItemRef in navigationItemRefs)
            {
                EpubNavigationItemType type = navigationItemRef.Type;
                string title = navigationItemRef.Title;
                EpubNavigationItemLink? link = navigationItemRef.Link;
                EpubLocalTextContentFile? htmlContentFile = null;

                if (navigationItemRef.HtmlContentFileRef != null)
                {
                    htmlContentFile = epubContent.Html.Local[navigationItemRef.HtmlContentFileRef.Key];
                }
                List<EpubNavigationItem> nestedItems = ReadNavigation(epubContent, navigationItemRef.NestedItems);
                result.Add(new EpubNavigationItem(type, title, link, htmlContentFile, nestedItems));
            }
            return result;
        }

        private static async Task<EpubLocalTextContentFile> ReadLocalTextContentFile(EpubLocalContentFileRef localContentFileRef)
        {
            string key = localContentFileRef.Key;
            EpubContentType contentType = localContentFileRef.ContentType;
            string contentMimeType = localContentFileRef.ContentMimeType;
            string filePath = localContentFileRef.FilePath;
            string content = await localContentFileRef.ReadContentAsTextAsync().ConfigureAwait(false);
            return new(key, contentType, contentMimeType, filePath, content);
        }

        private static async Task<EpubLocalByteContentFile> ReadLocalByteContentFile(EpubLocalContentFileRef localContentFileRef)
        {
            string key = localContentFileRef.Key;
            EpubContentType contentType = localContentFileRef.ContentType;
            string contentMimeType = localContentFileRef.ContentMimeType;
            string filePath = localContentFileRef.FilePath;
            byte[] content = await localContentFileRef.ReadContentAsBytesAsync().ConfigureAwait(false);
            return new(key, contentType, contentMimeType, filePath, content);
        }

        private async Task<EpubBook> ReadBookAsync(EpubBookRef epubBookRef)
        {
            using (epubBookRef)
            {
                string? filePath = epubBookRef.FilePath;
                EpubSchema schema = epubBookRef.Schema;
                string title = epubBookRef.Title;
                List<string> authorList = epubBookRef.AuthorList;
                string author = epubBookRef.Author;
                EpubContent content = await ReadContent(epubBookRef.Content).ConfigureAwait(false);
                byte[]? coverImage = await epubBookRef.ReadCoverAsync().ConfigureAwait(false);
                string? description = epubBookRef.Description;
                List<EpubLocalTextContentFileRef> htmlContentFileRefs = await epubBookRef.GetReadingOrderAsync().ConfigureAwait(false);
                List<EpubLocalTextContentFile> readingOrder = ReadReadingOrder(content, htmlContentFileRefs);
                List<EpubNavigationItemRef>? navigationItemRefs = await epubBookRef.GetNavigationAsync().ConfigureAwait(false);
                List<EpubNavigationItem>? navigation = navigationItemRefs != null ? ReadNavigation(content, navigationItemRefs) : null;
                return new(filePath, title, author, authorList, description, coverImage, readingOrder, navigation, schema, content);
            }
        }

        private async Task<EpubContent> ReadContent(EpubContentRef contentRef)
        {
            EpubLocalByteContentFile? cover = null;
            EpubLocalTextContentFile? navigationHtmlFile = null;
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> html = await ReadTextContentFiles(contentRef.Html).ConfigureAwait(false);
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> css = await ReadTextContentFiles(contentRef.Css).ConfigureAwait(false);
            EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile> images = await ReadByteContentFiles(contentRef.Images).ConfigureAwait(false);
            EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile> fonts = await ReadByteContentFiles(contentRef.Fonts).ConfigureAwait(false);
            EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile> audio = await ReadByteContentFiles(contentRef.Audio).ConfigureAwait(false);
            EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile> allFiles = new();
            foreach (KeyValuePair<string, EpubLocalTextContentFile> localTextContentFile in html.Local.Concat(css.Local))
            {
                allFiles.Local.Add(localTextContentFile.Key, localTextContentFile.Value);
            }
            foreach (KeyValuePair<string, EpubRemoteTextContentFile> remoteTextContentFile in html.Remote.Concat(css.Remote))
            {
                allFiles.Remote.Add(remoteTextContentFile.Key, remoteTextContentFile.Value);
            }
            foreach (KeyValuePair<string, EpubLocalByteContentFile> localByteContentFile in images.Local.Concat(fonts.Local).Concat(audio.Local))
            {
                allFiles.Local.Add(localByteContentFile.Key, localByteContentFile.Value);
            }
            foreach (KeyValuePair<string, EpubRemoteByteContentFile> remoteByteContentFile in images.Remote.Concat(fonts.Remote).Concat(audio.Remote))
            {
                allFiles.Remote.Add(remoteByteContentFile.Key, remoteByteContentFile.Value);
            }
            foreach (KeyValuePair<string, EpubLocalContentFileRef> localContentFileRef in contentRef.AllFiles.Local)
            {
                if (!allFiles.Local.ContainsKey(localContentFileRef.Key))
                {
                    if (localContentFileRef.Value is EpubLocalTextContentFileRef)
                    {
                        allFiles.Local.Add(localContentFileRef.Key, await ReadLocalTextContentFile(localContentFileRef.Value).ConfigureAwait(false));
                    }
                    else
                    {
                        allFiles.Local.Add(localContentFileRef.Key, await ReadLocalByteContentFile(localContentFileRef.Value).ConfigureAwait(false));
                    }
                }
            }
            foreach (KeyValuePair<string, EpubRemoteContentFileRef> remoteContentFileRef in contentRef.AllFiles.Remote)
            {
                if (!allFiles.Remote.ContainsKey(remoteContentFileRef.Key))
                {
                    if (remoteContentFileRef.Value is EpubRemoteTextContentFileRef)
                    {
                        allFiles.Remote.Add(remoteContentFileRef.Key, await DownloadRemoteTextContentFile(remoteContentFileRef.Value).ConfigureAwait(false));
                    }
                    else
                    {
                        allFiles.Remote.Add(remoteContentFileRef.Key, await DownloadRemoteByteContentFile(remoteContentFileRef.Value).ConfigureAwait(false));
                    }
                }
            }
            if (contentRef.Cover != null)
            {
                cover = images.Local[contentRef.Cover.Key];
            }
            if (contentRef.NavigationHtmlFile != null)
            {
                navigationHtmlFile = html.Local[contentRef.NavigationHtmlFile.Key];
            }
            return new(cover, navigationHtmlFile, html, css, images, fonts, audio, allFiles);
        }

        private async Task<EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>> ReadTextContentFiles(
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> textContentFileCollectionRef)
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> result = new();
            foreach (KeyValuePair<string, EpubLocalTextContentFileRef> localTextContentFileRef in textContentFileCollectionRef.Local)
            {
                result.Local.Add(localTextContentFileRef.Key, await ReadLocalTextContentFile(localTextContentFileRef.Value).ConfigureAwait(false));
            }
            foreach (KeyValuePair<string, EpubRemoteTextContentFileRef> remoteTextContentFileRef in textContentFileCollectionRef.Remote)
            {
                result.Remote.Add(remoteTextContentFileRef.Key, await DownloadRemoteTextContentFile(remoteTextContentFileRef.Value).ConfigureAwait(false));
            }
            return result;
        }

        private async Task<EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>> ReadByteContentFiles(
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> byteContentFileCollectionRef)
        {
            EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile> result = new();
            foreach (KeyValuePair<string, EpubLocalByteContentFileRef> localByteContentFileRef in byteContentFileCollectionRef.Local)
            {
                result.Local.Add(localByteContentFileRef.Key, await ReadLocalByteContentFile(localByteContentFileRef.Value).ConfigureAwait(false));
            }
            foreach (KeyValuePair<string, EpubRemoteByteContentFileRef> remoteByteContentFileRef in byteContentFileCollectionRef.Remote)
            {
                result.Remote.Add(remoteByteContentFileRef.Key, await DownloadRemoteByteContentFile(remoteByteContentFileRef.Value).ConfigureAwait(false));
            }
            return result;
        }

        private async Task<EpubRemoteTextContentFile> DownloadRemoteTextContentFile(EpubRemoteContentFileRef remoteContentFileRef)
        {
            string key = remoteContentFileRef.Key;
            EpubContentType contentType = remoteContentFileRef.ContentType;
            string contentMimeType = remoteContentFileRef.ContentMimeType;
            string url = remoteContentFileRef.Url;
            string? content = null;
            if (epubReaderOptions.ContentDownloaderOptions != null && epubReaderOptions.ContentDownloaderOptions.DownloadContent)
            {
                content = await remoteContentFileRef.DownloadContentAsTextAsync().ConfigureAwait(false);
            }
            return new(key, contentType, contentMimeType, url, content);
        }

        private async Task<EpubRemoteByteContentFile> DownloadRemoteByteContentFile(EpubRemoteContentFileRef remoteContentFileRef)
        {
            string key = remoteContentFileRef.Key;
            EpubContentType contentType = remoteContentFileRef.ContentType;
            string contentMimeType = remoteContentFileRef.ContentMimeType;
            string url = remoteContentFileRef.Url;
            byte[]? content = null;
            if (epubReaderOptions.ContentDownloaderOptions != null && epubReaderOptions.ContentDownloaderOptions.DownloadContent)
            {
                content = await remoteContentFileRef.DownloadContentAsBytesAsync().ConfigureAwait(false);
            }
            return new(key, contentType, contentMimeType, url, content);
        }
    }
}
