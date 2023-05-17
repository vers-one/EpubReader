using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            return htmlContentFileRefs.Select(htmlContentFileRef => epubContent.Html.GetLocalFileByKey(htmlContentFileRef.Key)).ToList();
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
                    htmlContentFile = epubContent.Html.GetLocalFileByKey(navigationItemRef.HtmlContentFileRef.Key);
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
            Dictionary<string, EpubLocalContentFile> allFilesLocal = new();
            Dictionary<string, EpubRemoteContentFile> allFilesRemote = new();
            foreach (EpubLocalTextContentFile localTextContentFile in html.Local.Concat(css.Local))
            {
                allFilesLocal.Add(localTextContentFile.Key, localTextContentFile);
            }
            foreach (EpubRemoteTextContentFile remoteTextContentFile in html.Remote.Concat(css.Remote))
            {
                allFilesRemote.Add(remoteTextContentFile.Key, remoteTextContentFile);
            }
            foreach (EpubLocalByteContentFile localByteContentFile in images.Local.Concat(fonts.Local).Concat(audio.Local))
            {
                allFilesLocal.Add(localByteContentFile.Key, localByteContentFile);
            }
            foreach (EpubRemoteByteContentFile remoteByteContentFile in images.Remote.Concat(fonts.Remote).Concat(audio.Remote))
            {
                allFilesRemote.Add(remoteByteContentFile.Key, remoteByteContentFile);
            }
            foreach (EpubLocalContentFileRef localContentFileRef in contentRef.AllFiles.Local)
            {
                if (!allFilesLocal.ContainsKey(localContentFileRef.Key))
                {
                    if (localContentFileRef is EpubLocalTextContentFileRef)
                    {
                        allFilesLocal.Add(localContentFileRef.Key, await ReadLocalTextContentFile(localContentFileRef).ConfigureAwait(false));
                    }
                    else
                    {
                        allFilesLocal.Add(localContentFileRef.Key, await ReadLocalByteContentFile(localContentFileRef).ConfigureAwait(false));
                    }
                }
            }
            foreach (EpubRemoteContentFileRef remoteContentFileRef in contentRef.AllFiles.Remote)
            {
                if (!allFilesRemote.ContainsKey(remoteContentFileRef.Key))
                {
                    if (remoteContentFileRef is EpubRemoteTextContentFileRef)
                    {
                        allFilesRemote.Add(remoteContentFileRef.Key, await DownloadRemoteTextContentFile(remoteContentFileRef).ConfigureAwait(false));
                    }
                    else
                    {
                        allFilesRemote.Add(remoteContentFileRef.Key, await DownloadRemoteByteContentFile(remoteContentFileRef).ConfigureAwait(false));
                    }
                }
            }
            EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile> allFiles =
                new(new ReadOnlyCollection<EpubLocalContentFile>(allFilesLocal.Values.ToList()), new ReadOnlyCollection<EpubRemoteContentFile>(allFilesRemote.Values.ToList()));
            if (contentRef.Cover != null)
            {
                cover = images.GetLocalFileByKey(contentRef.Cover.Key);
            }
            if (contentRef.NavigationHtmlFile != null)
            {
                navigationHtmlFile = html.GetLocalFileByKey(contentRef.NavigationHtmlFile.Key);
            }
            return new(cover, navigationHtmlFile, html, css, images, fonts, audio, allFiles);
        }

        private async Task<EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>> ReadTextContentFiles(
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> textContentFileCollectionRef)
        {
            List<EpubLocalTextContentFile> local = new();
            List<EpubRemoteTextContentFile> remote = new();
            foreach (EpubLocalTextContentFileRef localTextContentFileRef in textContentFileCollectionRef.Local)
            {
                local.Add(await ReadLocalTextContentFile(localTextContentFileRef).ConfigureAwait(false));
            }
            foreach (EpubRemoteTextContentFileRef remoteTextContentFileRef in textContentFileCollectionRef.Remote)
            {
                remote.Add(await DownloadRemoteTextContentFile(remoteTextContentFileRef).ConfigureAwait(false));
            }
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> result = new(local.AsReadOnly(), remote.AsReadOnly());
            return result;
        }

        private async Task<EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>> ReadByteContentFiles(
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> byteContentFileCollectionRef)
        {
            List<EpubLocalByteContentFile> local = new();
            List<EpubRemoteByteContentFile> remote = new();
            foreach (EpubLocalByteContentFileRef localByteContentFileRef in byteContentFileCollectionRef.Local)
            {
                local.Add(await ReadLocalByteContentFile(localByteContentFileRef).ConfigureAwait(false));
            }
            foreach (EpubRemoteByteContentFileRef remoteByteContentFileRef in byteContentFileCollectionRef.Remote)
            {
                remote.Add(await DownloadRemoteByteContentFile(remoteByteContentFileRef).ConfigureAwait(false));
            }
            EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile> result = new();
            return result;
        }

        private async Task<EpubRemoteTextContentFile> DownloadRemoteTextContentFile(EpubRemoteContentFileRef remoteContentFileRef)
        {
            string key = remoteContentFileRef.Key;
            EpubContentType contentType = remoteContentFileRef.ContentType;
            string contentMimeType = remoteContentFileRef.ContentMimeType;
            string? content = null;
            if (epubReaderOptions.ContentDownloaderOptions != null && epubReaderOptions.ContentDownloaderOptions.DownloadContent)
            {
                content = await remoteContentFileRef.DownloadContentAsTextAsync().ConfigureAwait(false);
            }
            return new(key, contentType, contentMimeType, content);
        }

        private async Task<EpubRemoteByteContentFile> DownloadRemoteByteContentFile(EpubRemoteContentFileRef remoteContentFileRef)
        {
            string key = remoteContentFileRef.Key;
            EpubContentType contentType = remoteContentFileRef.ContentType;
            string contentMimeType = remoteContentFileRef.ContentMimeType;
            byte[]? content = null;
            if (epubReaderOptions.ContentDownloaderOptions != null && epubReaderOptions.ContentDownloaderOptions.DownloadContent)
            {
                content = await remoteContentFileRef.DownloadContentAsBytesAsync().ConfigureAwait(false);
            }
            return new(key, contentType, contentMimeType, content);
        }
    }
}
