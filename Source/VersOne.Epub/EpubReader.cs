﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Utils;

namespace VersOne.Epub
{
    /// <summary>
    /// The main entry point of the EpubReader library. It can open/read EPUB books from a file or a <see cref="Stream" />.
    /// </summary>
    public static class EpubReader
    {
        private static IFileSystem FileSystem => EnvironmentDependencies.FileSystem;

        /// <summary>
        /// Opens the book synchronously without reading its content. The object returned by this method holds a handle to the EPUB file.
        /// </summary>
        /// <param name="filePath">Path to the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book reference. This object holds a handle to the EPUB file.</returns>
        public static EpubBookRef OpenBook(string filePath, EpubReaderOptions epubReaderOptions = null)
        {
            return OpenBookAsync(filePath, epubReaderOptions).ExecuteAndUnwrapAggregateException();
        }

        /// <summary>
        /// Opens the book synchronously without reading its content. The object returned by this method holds a handle to the EPUB file.
        /// </summary>
        /// <param name="stream">Seekable stream containing the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book reference. This object holds a handle to the EPUB file.</returns>
        public static EpubBookRef OpenBook(Stream stream, EpubReaderOptions epubReaderOptions = null)
        {
            return OpenBookAsync(stream, epubReaderOptions).ExecuteAndUnwrapAggregateException();
        }

        /// <summary>
        /// Opens the book asynchronously without reading its content. The object returned by this method holds a handle to the EPUB file.
        /// </summary>
        /// <param name="filePath">Path to the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book reference. This object holds a handle to the EPUB file.</returns>
        public static Task<EpubBookRef> OpenBookAsync(string filePath, EpubReaderOptions epubReaderOptions = null)
        {
            if (!FileSystem.FileExists(filePath))
            {
                throw new FileNotFoundException("Specified EPUB file not found.", filePath);
            }
            return OpenBookAsync(GetZipFile(filePath), filePath, epubReaderOptions);
        }

        /// <summary>
        /// Opens the book asynchronously without reading its content. The object returned by this method holds a handle to the EPUB file.
        /// </summary>
        /// <param name="stream">Seekable stream containing the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book reference. This object holds a handle to the EPUB file.</returns>
        public static Task<EpubBookRef> OpenBookAsync(Stream stream, EpubReaderOptions epubReaderOptions = null)
        {
            return OpenBookAsync(GetZipFile(stream), null, epubReaderOptions);
        }

        /// <summary>
        /// Opens the book synchronously and reads all of its content into the memory. The object returned by this method does not retain a handle to the EPUB file.
        /// </summary>
        /// <param name="filePath">Path to the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book with all its content. This object does not retain a handle to the EPUB file.</returns>
        public static EpubBook ReadBook(string filePath, EpubReaderOptions epubReaderOptions = null)
        {
            return ReadBookAsync(filePath, epubReaderOptions).ExecuteAndUnwrapAggregateException();
        }

        /// <summary>
        /// Opens the book synchronously and reads all of its content into the memory. The object returned by this method does not retain a handle to the EPUB file.
        /// </summary>
        /// <param name="stream">Seekable stream containing the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book with all its content. This object does not retain a handle to the EPUB file.</returns>
        public static EpubBook ReadBook(Stream stream, EpubReaderOptions epubReaderOptions = null)
        {
            return ReadBookAsync(stream, epubReaderOptions).ExecuteAndUnwrapAggregateException();
        }

        /// <summary>
        /// Opens the book asynchronously and reads all of its content into the memory. The object returned by this method does not retain a handle to the EPUB file.
        /// </summary>
        /// <param name="filePath">Path to the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book with all its content. This object does not retain a handle to the EPUB file.</returns>
        public static async Task<EpubBook> ReadBookAsync(string filePath, EpubReaderOptions epubReaderOptions = null)
        {
            EpubBookRef epubBookRef = await OpenBookAsync(filePath, epubReaderOptions).ConfigureAwait(false);
            return await ReadBookAsync(epubBookRef).ConfigureAwait(false);
        }

        /// <summary>
        /// Opens the book asynchronously and reads all of its content into the memory. The object returned by this method does not retain a handle to the EPUB file.
        /// </summary>
        /// <param name="stream">Seekable stream containing the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book with all its content. This object does not retain a handle to the EPUB file.</returns>
        public static async Task<EpubBook> ReadBookAsync(Stream stream, EpubReaderOptions epubReaderOptions = null)
        {
            EpubBookRef epubBookRef = await OpenBookAsync(stream, epubReaderOptions).ConfigureAwait(false);
            return await ReadBookAsync(epubBookRef).ConfigureAwait(false);
        }

        private static async Task<EpubBookRef> OpenBookAsync(IZipFile zipFile, string filePath, EpubReaderOptions epubReaderOptions)
        {
            EpubBookRef result = null;
            if (epubReaderOptions == null)
            {
                epubReaderOptions = new EpubReaderOptions();
            }
            result = new EpubBookRef(zipFile);
            try
            {
                result.FilePath = filePath;
                result.Schema = await SchemaReader.ReadSchemaAsync(zipFile, epubReaderOptions).ConfigureAwait(false);
                result.Title = result.Schema.Package.Metadata.Titles.FirstOrDefault() ?? String.Empty;
                result.AuthorList = result.Schema.Package.Metadata.Creators.Select(creator => creator.Creator).ToList();
                result.Author = String.Join(", ", result.AuthorList);
                result.Description = result.Schema.Package.Metadata.Description;
                result.Content = await Task.Run(() => ContentReader.ParseContentMap(result, epubReaderOptions.ContentReaderOptions)).ConfigureAwait(false);
                return result;
            }
            catch
            {
                result.Dispose();
                throw;
            }
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
                result.Navigation = ReadNavigation(result, navigationItemRefs);
            }
            return result;
        }

        private static IZipFile GetZipFile(string filePath)
        {
            return FileSystem.OpenZipFile(filePath);
        }

        private static IZipFile GetZipFile(Stream stream)
        {
            return FileSystem.OpenZipFile(stream);
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
                EpubNavigationItem navigationItem = new EpubNavigationItem(navigationItemRef.Type)
                {
                    Title = navigationItemRef.Title,
                    Link = navigationItemRef.Link,
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
