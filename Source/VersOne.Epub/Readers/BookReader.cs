using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal static class BookReader
    {
        public static EpubBook ReadBook(string filePath, EpubReaderOptions epubReaderOptions, IFileSystem fileSystem)
        {
            return ReadBookAsync(filePath, epubReaderOptions, fileSystem).ExecuteAndUnwrapAggregateException();
        }

        public static EpubBook ReadBook(Stream stream, EpubReaderOptions epubReaderOptions, IFileSystem fileSystem)
        {
            return ReadBookAsync(stream, epubReaderOptions, fileSystem).ExecuteAndUnwrapAggregateException();
        }

        public static async Task<EpubBook> ReadBookAsync(string filePath, EpubReaderOptions epubReaderOptions, IFileSystem fileSystem)
        {
            EpubBookRef epubBookRef = await BookRefReader.OpenBookAsync(filePath, epubReaderOptions, fileSystem).ConfigureAwait(false);
            return await ReadBookAsync(epubBookRef).ConfigureAwait(false);
        }

        public static async Task<EpubBook> ReadBookAsync(Stream stream, EpubReaderOptions epubReaderOptions, IFileSystem fileSystem)
        {
            EpubBookRef epubBookRef = await BookRefReader.OpenBookAsync(stream, epubReaderOptions, fileSystem).ConfigureAwait(false);
            return await ReadBookAsync(epubBookRef).ConfigureAwait(false);
        }

        private static async Task<EpubBook> ReadBookAsync(EpubBookRef epubBookRef)
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
                List<EpubTextContentFileRef> htmlContentFileRefs = await epubBookRef.GetReadingOrderAsync().ConfigureAwait(false);
                result.ReadingOrder = ReadReadingOrder(result, htmlContentFileRefs);
                List<EpubNavigationItemRef> navigationItemRefs = await epubBookRef.GetNavigationAsync().ConfigureAwait(false);
                result.Navigation = navigationItemRefs != null ? ReadNavigation(result, navigationItemRefs) : null;
            }
            return result;
        }

        private static async Task<EpubContent> ReadContent(EpubContentRef contentRef)
        {
            EpubContent result = new EpubContent();
            result.Html = await ReadTextContentFiles(contentRef.Html).ConfigureAwait(false);
            result.Css = await ReadTextContentFiles(contentRef.Css).ConfigureAwait(false);
            result.Images = await ReadByteContentFiles(contentRef.Images).ConfigureAwait(false);
            result.Fonts = await ReadByteContentFiles(contentRef.Fonts).ConfigureAwait(false);
            result.AllFiles = new Dictionary<string, EpubContentFile>();
            foreach (KeyValuePair<string, EpubTextContentFile> textContentFile in result.Html.Concat(result.Css))
            {
                result.AllFiles.Add(textContentFile.Key, textContentFile.Value);
            }
            foreach (KeyValuePair<string, EpubByteContentFile> byteContentFile in result.Images.Concat(result.Fonts))
            {
                result.AllFiles.Add(byteContentFile.Key, byteContentFile.Value);
            }
            foreach (KeyValuePair<string, EpubContentFileRef> contentFileRef in contentRef.AllFiles)
            {
                if (!result.AllFiles.ContainsKey(contentFileRef.Key))
                {
                    if (contentFileRef.Value is EpubTextContentFileRef)
                    {
                        result.AllFiles.Add(contentFileRef.Key, await ReadTextContentFile(contentFileRef.Value).ConfigureAwait(false));
                    }
                    else
                    {
                        result.AllFiles.Add(contentFileRef.Key, await ReadByteContentFile(contentFileRef.Value).ConfigureAwait(false));
                    }
                }
            }
            if (contentRef.Cover != null)
            {
                result.Cover = result.Images[contentRef.Cover.FileName];
            }
            if (contentRef.NavigationHtmlFile != null)
            {
                result.NavigationHtmlFile = result.Html[contentRef.NavigationHtmlFile.FileName];
            }
            return result;
        }

        private static async Task<Dictionary<string, EpubTextContentFile>> ReadTextContentFiles(Dictionary<string, EpubTextContentFileRef> textContentFileRefs)
        {
            Dictionary<string, EpubTextContentFile> result = new Dictionary<string, EpubTextContentFile>();
            foreach (KeyValuePair<string, EpubTextContentFileRef> textContentFileRef in textContentFileRefs)
            {
                result.Add(textContentFileRef.Key, await ReadTextContentFile(textContentFileRef.Value).ConfigureAwait(false));
            }
            return result;
        }

        private static async Task<Dictionary<string, EpubByteContentFile>> ReadByteContentFiles(Dictionary<string, EpubByteContentFileRef> byteContentFileRefs)
        {
            Dictionary<string, EpubByteContentFile> result = new Dictionary<string, EpubByteContentFile>();
            foreach (KeyValuePair<string, EpubByteContentFileRef> byteContentFileRef in byteContentFileRefs)
            {
                result.Add(byteContentFileRef.Key, await ReadByteContentFile(byteContentFileRef.Value).ConfigureAwait(false));
            }
            return result;
        }

        private static async Task<EpubTextContentFile> ReadTextContentFile(EpubContentFileRef contentFileRef)
        {
            EpubTextContentFile result = new EpubTextContentFile
            {
                FileName = contentFileRef.FileName,
                FilePathInEpubArchive = contentFileRef.FilePathInEpubArchive,
                Href = contentFileRef.Href,
                ContentLocation = contentFileRef.ContentLocation,
                ContentType = contentFileRef.ContentType,
                ContentMimeType = contentFileRef.ContentMimeType
            };
            if (result.ContentLocation == EpubContentLocation.LOCAL)
            {
                result.Content = await contentFileRef.ReadContentAsTextAsync().ConfigureAwait(false);
            }
            else
            {
                result.Content = null;
            }
            return result;
        }

        private static async Task<EpubByteContentFile> ReadByteContentFile(EpubContentFileRef contentFileRef)
        {
            EpubByteContentFile result = new EpubByteContentFile
            {
                FileName = contentFileRef.FileName,
                FilePathInEpubArchive = contentFileRef.FilePathInEpubArchive,
                Href = contentFileRef.Href,
                ContentLocation = contentFileRef.ContentLocation,
                ContentType = contentFileRef.ContentType,
                ContentMimeType = contentFileRef.ContentMimeType
            };
            if (result.ContentLocation == EpubContentLocation.LOCAL)
            {
                result.Content = await contentFileRef.ReadContentAsBytesAsync().ConfigureAwait(false);
            }
            else
            {
                result.Content = null;
            }
            return result;
        }

        private static List<EpubTextContentFile> ReadReadingOrder(EpubBook epubBook, List<EpubTextContentFileRef> htmlContentFileRefs)
        {
            return htmlContentFileRefs.Select(htmlContentFileRef => epubBook.Content.Html[htmlContentFileRef.FileName]).ToList();
        }

        private static List<EpubNavigationItem> ReadNavigation(EpubBook epubBook, List<EpubNavigationItemRef> navigationItemRefs)
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
                    navigationItem.HtmlContentFile = epubBook.Content.Html[navigationItemRef.HtmlContentFileRef.FileName];
                }
                navigationItem.NestedItems = ReadNavigation(epubBook, navigationItemRef.NestedItems);
                result.Add(navigationItem);
            }
            return result;
        }
    }
}
