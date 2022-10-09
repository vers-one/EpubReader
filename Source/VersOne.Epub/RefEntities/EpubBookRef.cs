﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Internal;

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
        public EpubBookRef(IZipFile epubFile)
        {
            EpubFile = epubFile;
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
        /// Gets the path to the EPUB file.
        /// </summary>
        public string FilePath { get; internal set; }

        /// <summary>
        /// Gets the title of the book.
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// Gets the comma separated list of the book's authors.
        /// </summary>
        public string Author { get; internal set; }

        /// <summary>
        /// Gets the list of book's authors names.
        /// </summary>
        public List<string> AuthorList { get; internal set; }

        /// <summary>
        /// Gets the book's description or <c>null</c> if the description is not present in the book.
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Gets the parsed EPUB schema of the book.
        /// </summary>
        public EpubSchema Schema { get; internal set; }

        /// <summary>
        /// Gets the collection of references to the book's content files within the EPUB archive.
        /// </summary>
        public EpubContentRef Content { get; internal set; }

        /// <summary>
        /// Gets the reference to the EPUB file.
        /// </summary>
        public IZipFile EpubFile { get; private set; }

        /// <summary>
        /// Loads the book's cover image from the EPUB file.
        /// </summary>
        /// <returns>Book's cover image or <c>null</c> if there is no cover.</returns>
        public byte[] ReadCover()
        {
            return ReadCoverAsync().Result;
        }

        /// <summary>
        /// Asynchronously loads the book's cover image from the EPUB file.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous load operation. The value of the TResult parameter contains the book's cover image or <c>null</c> if there is no cover.
        /// </returns>
        public async Task<byte[]> ReadCoverAsync()
        {
            if (Content.Cover == null)
            {
                return null;
            }
            return await Content.Cover.ReadContentAsBytesAsync(EpubFile).ConfigureAwait(false);
        }

        /// <summary>
        /// Processes the reading order of the EPUB book and returns a list of text content file references in the order of reading intended by the author.
        /// </summary>
        /// <returns>A list of text content file references in the order of reading.</returns>
        public List<EpubTextContentFileRef> GetReadingOrder()
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
        public async Task<List<EpubTextContentFileRef>> GetReadingOrderAsync()
        {
            return await Task.Run(() => SpineReader.GetReadingOrder(this)).ConfigureAwait(false);
        }

        /// <summary>
        /// Processes the navigational information of the EPUB book and returns a list of its navigation elements (typically the table of contents).
        /// </summary>
        /// <returns>A list of navigation elements of the book or <c>null</c> if the book doesn't have navigation information.</returns>
        public List<EpubNavigationItemRef> GetNavigation()
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
        public async Task<List<EpubNavigationItemRef>> GetNavigationAsync()
        {
            return await Task.Run(() => NavigationReader.GetNavigationItems(this)).ConfigureAwait(false);
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
                    EpubFile?.Dispose();
                }
                isDisposed = true;
            }
        }
    }
}
