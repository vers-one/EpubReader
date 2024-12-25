using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Internal;
using VersOne.Epub.Options;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents a EPUB book with its metadata. An instance of this class holds a reference to the EPUB file to load book's content on demand.
    /// </summary>
    public class EpubBookRef : IDisposable
    {
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubBookRef" /> class.
        /// </summary>
        /// <param name="epubFile">Reference to the EPUB file.</param>
        /// <param name="filePath">The path to the EPUB file or <c>null</c> if the EPUB file is being loaded from a stream.</param>
        /// <param name="title">The title of the book.</param>
        /// <param name="author">The comma separated list of the book's authors.</param>
        /// <param name="authorList">The list of book's authors names.</param>
        /// <param name="description">The book's description or <c>null</c> if the description is not present in the book.</param>
        /// <param name="schema">The parsed EPUB schema of the book.</param>
        /// <param name="content">The collection of references to the book's content files within the EPUB archive.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="epubFile" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="title" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="author" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="schema" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="content" /> parameter is <c>null</c>.</exception>
        public EpubBookRef(
            IZipFile epubFile, string? filePath, string title, string author, List<string>? authorList, string? description, EpubSchema schema,
            EpubContentRef content, EpubReaderOptions? epubReaderOptions = null)
        {
            EpubFile = epubFile ?? throw new ArgumentNullException(nameof(epubFile));
            FilePath = filePath;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            AuthorList = authorList ?? new List<string>();
            Description = description;
            Schema = schema ?? throw new ArgumentNullException(nameof(schema));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            EpubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
            isDisposed = false;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="EpubBookRef" /> class.
        /// </summary>
        ~EpubBookRef()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the path to the EPUB file or <c>null</c> if the EPUB file was loaded from a stream.
        /// </summary>
        public string? FilePath { get; }

        /// <summary>
        /// Gets the title of the book.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the comma separated list of the book's authors.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Gets the list of book's authors names.
        /// </summary>
        public List<string> AuthorList { get; }

        /// <summary>
        /// Gets the book's description or <c>null</c> if the description is not present in the book.
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// Gets the parsed EPUB schema of the book.
        /// </summary>
        public EpubSchema Schema { get; }

        /// <summary>
        /// Gets the collection of references to the book's content files within the EPUB archive.
        /// </summary>
        public EpubContentRef Content { get; }

        /// <summary>
        /// Gets the reference to the EPUB file.
        /// </summary>
        public IZipFile EpubFile { get; }

        /// <summary>
        /// Gets the options that configure the behavior of the EPUB reader.
        /// </summary>
        public EpubReaderOptions EpubReaderOptions { get; }

        /// <summary>
        /// Loads the book's cover image from the EPUB file.
        /// </summary>
        /// <returns>Book's cover image or <c>null</c> if there is no cover.</returns>
        public byte[]? ReadCover()
        {
            return ReadCoverAsync().Result;
        }

        /// <summary>
        /// Asynchronously loads the book's cover image from the EPUB file.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous load operation. The value of the TResult parameter contains the book's cover image or <c>null</c> if there is no cover.
        /// </returns>
        public async Task<byte[]?> ReadCoverAsync()
        {
            if (Content.Cover == null)
            {
                return null;
            }
            return await Content.Cover.ReadContentAsBytesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Processes the reading order of the EPUB book and returns a list of text content file references in the order of reading intended by the author.
        /// </summary>
        /// <returns>A list of text content file references in the order of reading.</returns>
        public List<EpubLocalTextContentFileRef> GetReadingOrder()
        {
            return GetReadingOrderAsync().Result;
        }

        /// <summary>
        /// Asynchronously processes the reading order of the EPUB book and returns a list of text content file references in the order of reading intended by the author.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous processing operation.
        /// The value of the TResult parameter contains a list of text content file references in the order of reading.
        /// </returns>
        public async Task<List<EpubLocalTextContentFileRef>> GetReadingOrderAsync()
        {
            return await Task.Run(() => SpineReader.GetReadingOrder(Schema, Content, EpubReaderOptions.SpineReaderOptions)).ConfigureAwait(false);
        }

        /// <summary>
        /// Processes the navigational information of the EPUB book and returns a list of its navigation elements (typically the table of contents).
        /// </summary>
        /// <returns>A list of navigation elements of the book or <c>null</c> if the book doesn't have navigation information.</returns>
        public List<EpubNavigationItemRef>? GetNavigation()
        {
            return GetNavigationAsync().Result;
        }

        /// <summary>
        /// Asynchronously processes the navigational information of the EPUB book and returns a list of its navigation elements (typically the table of contents).
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous processing operation.
        /// The value of the TResult parameter contains a list of navigation elements of the book or <c>null</c> if the book doesn't have navigation information.
        /// </returns>
        public async Task<List<EpubNavigationItemRef>?> GetNavigationAsync()
        {
            return await Task.Run(() => NavigationReader.GetNavigationItems(Schema, Content)).ConfigureAwait(false);
        }

        /// <summary>
        /// Releases the managed resources used by the <see cref="EpubBookRef" />.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the resources used by the <see cref="EpubBookRef" />.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// This class has no unmanaged resources, so the value of <c>false</c> causes this method to not release any resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    EpubFile.Dispose();
                }
                isDisposed = true;
            }
        }
    }
}
