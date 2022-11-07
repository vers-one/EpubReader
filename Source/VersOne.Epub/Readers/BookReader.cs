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

        public BookReader(IEnvironmentDependencies environmentDependencies, EpubReaderOptions epubReaderOptions)
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
            BookRefReader bookRefReader = new BookRefReader(environmentDependencies, epubReaderOptions);
            EpubBookRef epubBookRef = await bookRefReader.OpenBookAsync(filePath).ConfigureAwait(false);
            return await ReadBookAsync(epubBookRef).ConfigureAwait(false);
        }

        public async Task<EpubBook> ReadBookAsync(Stream stream)
        {
            BookRefReader bookRefReader = new BookRefReader(environmentDependencies, epubReaderOptions);
            EpubBookRef epubBookRef = await bookRefReader.OpenBookAsync(stream).ConfigureAwait(false);
            return await ReadBookAsync(epubBookRef).ConfigureAwait(false);
        }

        private static List<EpubLocalTextContentFile> ReadReadingOrder(EpubContent epubContent, List<EpubLocalTextContentFileRef> htmlContentFileRefs)
        {
            return htmlContentFileRefs.Select(htmlContentFileRef => epubContent.Html.Local[htmlContentFileRef.Key]).ToList();
        }

        private static List<EpubNavigationItem> ReadNavigation(EpubContent epubContent, List<EpubNavigationItemRef> navigationItemRefs)
        {
            List<EpubNavigationItem> result = new List<EpubNavigationItem>();
            foreach (EpubNavigationItemRef navigationItemRef in navigationItemRefs)
            {
                EpubNavigationItem navigationItem = new EpubNavigationItem()
                {
                    Type = navigationItemRef.Type,
                    Title = navigationItemRef.Title,
                    Link = navigationItemRef.Link
                };
                if (navigationItemRef.HtmlContentFileRef != null)
                {
                    navigationItem.HtmlContentFile = epubContent.Html.Local[navigationItemRef.HtmlContentFileRef.Key];
                }
                navigationItem.NestedItems = ReadNavigation(epubContent, navigationItemRef.NestedItems);
                result.Add(navigationItem);
            }
            return result;
        }

        private static async Task<EpubLocalTextContentFile> ReadLocalTextContentFile(EpubLocalContentFileRef localContentFileRef)
        {
            return new EpubLocalTextContentFile
            {
                Key = localContentFileRef.Key,
                ContentType = localContentFileRef.ContentType,
                ContentMimeType = localContentFileRef.ContentMimeType,
                FilePath = localContentFileRef.FilePath,
                Content = await localContentFileRef.ReadContentAsTextAsync().ConfigureAwait(false)
            };
        }

        private static async Task<EpubLocalByteContentFile> ReadLocalByteContentFile(EpubLocalContentFileRef localContentFileRef)
        {
            return new EpubLocalByteContentFile
            {
                Key = localContentFileRef.Key,
                ContentType = localContentFileRef.ContentType,
                ContentMimeType = localContentFileRef.ContentMimeType,
                FilePath = localContentFileRef.FilePath,
                Content = await localContentFileRef.ReadContentAsBytesAsync().ConfigureAwait(false)
            };
        }

        private async Task<EpubBook> ReadBookAsync(EpubBookRef epubBookRef)
        {
            EpubBook result = new EpubBook();
            using (epubBookRef)
            {
                result.FilePath = epubBookRef.FilePath;
                result.Schema = epubBookRef.Schema;
                result.Title = epubBookRef.Title;
                result.AuthorList = epubBookRef.AuthorList;
                result.Author = epubBookRef.Author;
                result.Content = await ReadContent(epubBookRef.Content).ConfigureAwait(false);
                result.CoverImage = await epubBookRef.ReadCoverAsync().ConfigureAwait(false);
                result.Description = epubBookRef.Description;
                List<EpubLocalTextContentFileRef> htmlContentFileRefs = await epubBookRef.GetReadingOrderAsync().ConfigureAwait(false);
                result.ReadingOrder = ReadReadingOrder(result.Content, htmlContentFileRefs);
                List<EpubNavigationItemRef> navigationItemRefs = await epubBookRef.GetNavigationAsync().ConfigureAwait(false);
                result.Navigation = navigationItemRefs != null ? ReadNavigation(result.Content, navigationItemRefs) : null;
            }
            return result;
        }

        private async Task<EpubContent> ReadContent(EpubContentRef contentRef)
        {
            EpubContent result = new EpubContent();
            result.Html = await ReadTextContentFiles(contentRef.Html).ConfigureAwait(false);
            result.Css = await ReadTextContentFiles(contentRef.Css).ConfigureAwait(false);
            result.Images = await ReadByteContentFiles(contentRef.Images).ConfigureAwait(false);
            result.Fonts = await ReadByteContentFiles(contentRef.Fonts).ConfigureAwait(false);
            result.AllFiles = new EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile>()
            {
                Local = new Dictionary<string, EpubLocalContentFile>(),
                Remote = new Dictionary<string, EpubRemoteContentFile>()
            };
            foreach (KeyValuePair<string, EpubLocalTextContentFile> localTextContentFile in result.Html.Local.Concat(result.Css.Local))
            {
                result.AllFiles.Local.Add(localTextContentFile.Key, localTextContentFile.Value);
            }
            foreach (KeyValuePair<string, EpubRemoteTextContentFile> remoteTextContentFile in result.Html.Remote.Concat(result.Css.Remote))
            {
                result.AllFiles.Remote.Add(remoteTextContentFile.Key, remoteTextContentFile.Value);
            }
            foreach (KeyValuePair<string, EpubLocalByteContentFile> localByteContentFile in result.Images.Local.Concat(result.Fonts.Local))
            {
                result.AllFiles.Local.Add(localByteContentFile.Key, localByteContentFile.Value);
            }
            foreach (KeyValuePair<string, EpubRemoteByteContentFile> remoteByteContentFile in result.Images.Remote.Concat(result.Fonts.Remote))
            {
                result.AllFiles.Remote.Add(remoteByteContentFile.Key, remoteByteContentFile.Value);
            }
            foreach (KeyValuePair<string, EpubLocalContentFileRef> localContentFileRef in contentRef.AllFiles.Local)
            {
                if (!result.AllFiles.Local.ContainsKey(localContentFileRef.Key))
                {
                    if (localContentFileRef.Value is EpubLocalTextContentFileRef)
                    {
                        result.AllFiles.Local.Add(localContentFileRef.Key, await ReadLocalTextContentFile(localContentFileRef.Value).ConfigureAwait(false));
                    }
                    else
                    {
                        result.AllFiles.Local.Add(localContentFileRef.Key, await ReadLocalByteContentFile(localContentFileRef.Value).ConfigureAwait(false));
                    }
                }
            }
            foreach (KeyValuePair<string, EpubRemoteContentFileRef> remoteContentFileRef in contentRef.AllFiles.Remote)
            {
                if (!result.AllFiles.Remote.ContainsKey(remoteContentFileRef.Key))
                {
                    if (remoteContentFileRef.Value is EpubRemoteTextContentFileRef)
                    {
                        result.AllFiles.Remote.Add(
                            remoteContentFileRef.Key, await DownloadRemoteTextContentFile(remoteContentFileRef.Value).ConfigureAwait(false));
                    }
                    else
                    {
                        result.AllFiles.Remote.Add(
                            remoteContentFileRef.Key, await DownloadRemoteByteContentFile(remoteContentFileRef.Value).ConfigureAwait(false));
                    }
                }
            }
            if (contentRef.Cover != null)
            {
                result.Cover = result.Images.Local[contentRef.Cover.Key];
            }
            if (contentRef.NavigationHtmlFile != null)
            {
                result.NavigationHtmlFile = result.Html.Local[contentRef.NavigationHtmlFile.Key];
            }
            return result;
        }

        private async Task<EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>> ReadTextContentFiles(
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> textContentFileCollectionRef)
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> result = new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>()
            {
                Local = new Dictionary<string, EpubLocalTextContentFile>(),
                Remote = new Dictionary<string, EpubRemoteTextContentFile>()
            };
            foreach (KeyValuePair<string, EpubLocalTextContentFileRef> localTextContentFileRef in textContentFileCollectionRef.Local)
            {
                result.Local.Add(localTextContentFileRef.Key, await ReadLocalTextContentFile(localTextContentFileRef.Value).ConfigureAwait(false));
            }
            foreach (KeyValuePair<string, EpubRemoteTextContentFileRef> remoteTextContentFileRef in textContentFileCollectionRef.Remote)
            {
                result.Remote.Add(
                    remoteTextContentFileRef.Key, await DownloadRemoteTextContentFile(remoteTextContentFileRef.Value).ConfigureAwait(false));
            }
            return result;
        }

        private async Task<EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>> ReadByteContentFiles(
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> byteContentFileCollectionRef)
        {
            EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile> result = new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>()
            {
                Local = new Dictionary<string, EpubLocalByteContentFile>(),
                Remote = new Dictionary<string, EpubRemoteByteContentFile>()
            };
            foreach (KeyValuePair<string, EpubLocalByteContentFileRef> localByteContentFileRef in byteContentFileCollectionRef.Local)
            {
                result.Local.Add(localByteContentFileRef.Key, await ReadLocalByteContentFile(localByteContentFileRef.Value).ConfigureAwait(false));
            }
            foreach (KeyValuePair<string, EpubRemoteByteContentFileRef> remoteByteContentFileRef in byteContentFileCollectionRef.Remote)
            {
                result.Remote.Add(
                    remoteByteContentFileRef.Key, await DownloadRemoteByteContentFile(remoteByteContentFileRef.Value).ConfigureAwait(false));
            }
            return result;
        }

        private async Task<EpubRemoteTextContentFile> DownloadRemoteTextContentFile(EpubRemoteContentFileRef remoteContentFileRef)
        {
            EpubRemoteTextContentFile result = new EpubRemoteTextContentFile();
            result.Key = remoteContentFileRef.Key;
            result.ContentType = remoteContentFileRef.ContentType;
            result.ContentMimeType = remoteContentFileRef.ContentMimeType;
            result.Url = remoteContentFileRef.Url;
            if (epubReaderOptions.ContentDownloaderOptions != null)
            {
                if (epubReaderOptions.ContentDownloaderOptions.DownloadContent)
                {
                    result.Content = await remoteContentFileRef.DownloadContentAsTextAsync().ConfigureAwait(false);
                }
            }
            else
            {
                result.Content = null;
            }
            return result;
        }

        private async Task<EpubRemoteByteContentFile> DownloadRemoteByteContentFile(EpubRemoteContentFileRef remoteContentFileRef)
        {
            return new EpubRemoteByteContentFile
            {
                Key = remoteContentFileRef.Key,
                ContentType = remoteContentFileRef.ContentType,
                ContentMimeType = remoteContentFileRef.ContentMimeType,
                Url = remoteContentFileRef.Url,
                Content = epubReaderOptions.ContentDownloaderOptions?.DownloadContent == true ? await remoteContentFileRef.DownloadContentAsBytesAsync().ConfigureAwait(false) : null
            };
        }
    }
}
