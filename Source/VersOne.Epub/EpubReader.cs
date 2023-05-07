using System.IO;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Environment.Implementation;
using VersOne.Epub.Internal;
using VersOne.Epub.Options;

namespace VersOne.Epub
{
    /// <summary>
    /// The main entry point of the EpubReader library. The methods in this class let the consumer to open/read EPUB books from a file or a <see cref="Stream" />.
    /// </summary>
    public static class EpubReader
    {
        private static readonly IEnvironmentDependencies environmentDependencies;

        static EpubReader()
        {
            environmentDependencies = new EnvironmentDependencies();
        }

        /// <summary>
        /// Opens the book synchronously without reading its content. The object returned by this method holds a handle to the EPUB file.
        /// </summary>
        /// <param name="filePath">Path to the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book reference. This object holds a handle to the EPUB file.</returns>
        public static EpubBookRef OpenBook(string filePath, EpubReaderOptions? epubReaderOptions = null)
        {
            BookRefReader bookRefReader = new(environmentDependencies, epubReaderOptions);
            return bookRefReader.OpenBook(filePath);
        }

        /// <summary>
        /// Opens the book synchronously without reading its content. The object returned by this method holds a handle to the EPUB file.
        /// </summary>
        /// <param name="stream">Seekable stream containing the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book reference. This object holds a handle to the EPUB file.</returns>
        public static EpubBookRef OpenBook(Stream stream, EpubReaderOptions? epubReaderOptions = null)
        {
            BookRefReader bookRefReader = new(environmentDependencies, epubReaderOptions);
            return bookRefReader.OpenBook(stream);
        }

        /// <summary>
        /// Opens the book asynchronously without reading its content. The object returned by this method holds a handle to the EPUB file.
        /// </summary>
        /// <param name="filePath">Path to the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book reference. This object holds a handle to the EPUB file.</returns>
        public static Task<EpubBookRef> OpenBookAsync(string filePath, EpubReaderOptions? epubReaderOptions = null)
        {
            BookRefReader bookRefReader = new(environmentDependencies, epubReaderOptions);
            return bookRefReader.OpenBookAsync(filePath);
        }

        /// <summary>
        /// Opens the book asynchronously without reading its content. The object returned by this method holds a handle to the EPUB file.
        /// </summary>
        /// <param name="stream">Seekable stream containing the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book reference. This object holds a handle to the EPUB file.</returns>
        public static Task<EpubBookRef> OpenBookAsync(Stream stream, EpubReaderOptions? epubReaderOptions = null)
        {
            BookRefReader bookRefReader = new(environmentDependencies, epubReaderOptions);
            return bookRefReader.OpenBookAsync(stream);
        }

        /// <summary>
        /// Opens the book synchronously and reads all of its content into the memory. The object returned by this method does not retain a handle to the EPUB file.
        /// </summary>
        /// <param name="filePath">Path to the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book with all its content. This object does not retain a handle to the EPUB file.</returns>
        public static EpubBook ReadBook(string filePath, EpubReaderOptions? epubReaderOptions = null)
        {
            BookReader bookReader = new(environmentDependencies, epubReaderOptions);
            return bookReader.ReadBook(filePath);
        }

        /// <summary>
        /// Opens the book synchronously and reads all of its content into the memory. The object returned by this method does not retain a handle to the EPUB file.
        /// </summary>
        /// <param name="stream">Seekable stream containing the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book with all its content. This object does not retain a handle to the EPUB file.</returns>
        public static EpubBook ReadBook(Stream stream, EpubReaderOptions? epubReaderOptions = null)
        {
            BookReader bookReader = new(environmentDependencies, epubReaderOptions);
            return bookReader.ReadBook(stream);
        }

        /// <summary>
        /// Opens the book asynchronously and reads all of its content into the memory. The object returned by this method does not retain a handle to the EPUB file.
        /// </summary>
        /// <param name="filePath">Path to the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book with all its content. This object does not retain a handle to the EPUB file.</returns>
        public static Task<EpubBook> ReadBookAsync(string filePath, EpubReaderOptions? epubReaderOptions = null)
        {
            BookReader bookReader = new(environmentDependencies, epubReaderOptions);
            return bookReader.ReadBookAsync(filePath);
        }

        /// <summary>
        /// Opens the book asynchronously and reads all of its content into the memory. The object returned by this method does not retain a handle to the EPUB file.
        /// </summary>
        /// <param name="stream">Seekable stream containing the EPUB file.</param>
        /// <param name="epubReaderOptions">Various options to configure the behavior of the EPUB reader.</param>
        /// <returns>EPUB book with all its content. This object does not retain a handle to the EPUB file.</returns>
        public static Task<EpubBook> ReadBookAsync(Stream stream, EpubReaderOptions? epubReaderOptions = null)
        {
            BookReader bookReader = new(environmentDependencies, epubReaderOptions);
            return bookReader.ReadBookAsync(stream);
        }
    }
}
