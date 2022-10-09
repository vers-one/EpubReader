using System;
using System.IO;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Utils;

namespace VersOne.Epub
{
    /// <summary>
    /// The base class for a content file reference within the EPUB archive (e.g., HTML files or images).
    /// Unlike <see cref="EpubContentFile" />, the classes derived from this base class contain only a reference to the file but don't contain its content.
    /// </summary>
    public abstract class EpubContentFileRef
    {
        private readonly ContentReaderOptions contentReaderOptions;
        private ReplacementContentFileEntry replacementContentFileEntry;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubContentFileRef" /> class with a specified EPUB book reference, a file name, a content type of the file,
        /// and a MIME type of the file's content.
        /// </summary>
        /// <param name="href">Relative file path or absolute URI of the content item (as it is specified in the EPUB manifest).</param>
        /// <param name="contentLocation">Location of the content item (local or remote).</param>
        /// <param name="contentType">The type of the content of the file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the file.</param>
        /// <param name="contentDirectoryPath">The content directory path which acts as a root directory for all content files within the EPUB book.</param>
        /// <param name="contentReaderOptions">Optional content reader options determining how to handle missing content files.</param>
        protected EpubContentFileRef(string href, EpubContentLocation contentLocation, EpubContentType contentType, string contentMimeType, string contentDirectoryPath,
            ContentReaderOptions contentReaderOptions = null)
        {
            ContentLocation = contentLocation;
            if (contentLocation == EpubContentLocation.LOCAL)
            {
                FileName = href;
                FilePathInEpubArchive = ZipPathUtils.Combine(contentDirectoryPath, FileName);
                Href = null;
            }
            else
            {
                FileName = null;
                FilePathInEpubArchive = null;
                Href = href;
            }
            ContentType = contentType;
            ContentMimeType = contentMimeType;
            this.contentReaderOptions = contentReaderOptions;
            replacementContentFileEntry = null;
        }

        /// <summary>
        /// Gets the relative file path of the content file (as it is specified in the EPUB manifest).
        /// Returns <c>null</c> if <see cref="ContentLocation" /> is <see cref="EpubContentLocation.REMOTE" />.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets the absolute file path of the content file in the EPUB archive.
        /// Returns <c>null</c> if <see cref="ContentLocation" /> is <see cref="EpubContentLocation.REMOTE" />.
        /// </summary>
        public string FilePathInEpubArchive { get; }

        /// <summary>
        /// Gets the absolute URI of the content item (as it is specified in the EPUB manifest).
        /// Returns <c>null</c> if <see cref="ContentLocation" /> is <see cref="EpubContentLocation.LOCAL" />.
        /// </summary>
        public string Href { get; internal set; }

        /// <summary>
        /// Gets the location of the content item (local or remote).
        /// </summary>
        public EpubContentLocation ContentLocation { get; internal set; }

        /// <summary>
        /// Gets the type of the content of the file.
        /// </summary>
        public EpubContentType ContentType { get; }

        /// <summary>
        /// Gets the MIME type of the content of the file.
        /// </summary>
        public string ContentMimeType { get; }

        /// <summary>
        /// Reads the whole content of the referenced file and returns it as a byte array.
        /// Throws <see cref="InvalidOperationException" /> if <see cref="ContentLocation" /> is <see cref="EpubContentLocation.REMOTE" />.
        /// </summary>
        /// <param name="epubFile">The reference to the EPUB file.</param>
        /// <returns>Content of the referenced file.</returns>
        public byte[] ReadContentAsBytes(IZipFile epubFile)
        {
            return ReadContentAsBytesAsync(epubFile).ExecuteAndUnwrapAggregateException();
        }

        /// <summary>
        /// Asynchronously reads the whole content of the referenced file and returns it as a byte array.
        /// Throws <see cref="InvalidOperationException" /> if <see cref="ContentLocation" /> is <see cref="EpubContentLocation.REMOTE" />.
        /// </summary>
        /// <param name="epubFile">The reference to the EPUB file.</param>
        /// <returns>A task that represents the asynchronous read operation. The value of the TResult parameter contains the content of the referenced file.</returns>
        public async Task<byte[]> ReadContentAsBytesAsync(IZipFile epubFile)
        {
            IZipFileEntry contentFileEntry = GetContentFileEntry(epubFile);
            byte[] content = new byte[(int)contentFileEntry.Length];
            using (Stream contentStream = contentFileEntry.Open())
            using (MemoryStream memoryStream = new MemoryStream(content))
            {
                await contentStream.CopyToAsync(memoryStream).ConfigureAwait(false);
            }
            return content;
        }

        /// <summary>
        /// Reads the whole content of the referenced file and returns it as a string.
        /// Throws <see cref="InvalidOperationException" /> if <see cref="ContentLocation" /> is <see cref="EpubContentLocation.REMOTE" />.
        /// </summary>
        /// <param name="epubFile">The reference to the EPUB file.</param>
        /// <returns>Content of the referenced file.</returns>
        public string ReadContentAsText(IZipFile epubFile)
        {
            return ReadContentAsTextAsync(epubFile).ExecuteAndUnwrapAggregateException();
        }

        /// <summary>
        /// Asynchronously reads the whole content of the referenced file and returns it as a string.
        /// Throws <see cref="InvalidOperationException" /> if <see cref="ContentLocation" /> is <see cref="EpubContentLocation.REMOTE" />.
        /// </summary>
        /// <param name="epubFile">The reference to the EPUB file.</param>
        /// <returns>A task that represents the asynchronous read operation. The value of the TResult parameter contains the content of the referenced file.</returns>
        public async Task<string> ReadContentAsTextAsync(IZipFile epubFile)
        {
            using (Stream contentStream = GetContentStream(epubFile))
            using (StreamReader streamReader = new StreamReader(contentStream))
            {
                return await streamReader.ReadToEndAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Opens the referenced file and returns a <see cref="Stream" /> to access its content.
        /// Throws <see cref="InvalidOperationException" /> if <see cref="ContentLocation" /> is <see cref="EpubContentLocation.REMOTE" />.
        /// </summary>
        /// <param name="epubFile">The reference to the EPUB file.</param>
        /// <returns>A <see cref="Stream" /> to access the referenced file's content.</returns>
        public Stream GetContentStream(IZipFile epubFile)
        {
            return GetContentFileEntry(epubFile).Open();
        }

        private IZipFileEntry GetContentFileEntry(IZipFile epubFile)
        {
            if (epubFile == null)
            {
                throw new ArgumentNullException(nameof(epubFile));
            }
            if (ContentLocation == EpubContentLocation.REMOTE)
            {
                throw new InvalidOperationException("Content cannot be retrieved for remote content items.");
            }
            if (replacementContentFileEntry != null)
            {
                return replacementContentFileEntry;
            }
            if (String.IsNullOrEmpty(FileName))
            {
                throw new EpubPackageException("EPUB parsing error: file name of the specified content file is empty.");
            }
            string contentFilePath = FilePathInEpubArchive;
            IZipFileEntry contentFileEntry = epubFile.GetEntry(contentFilePath);
            if (contentFileEntry == null)
            {
                bool throwMissingFileException = true;
                if (contentReaderOptions != null)
                {
                    ContentFileMissingEventArgs contentFileMissingEventArgs = new ContentFileMissingEventArgs(FileName, FilePathInEpubArchive, ContentType, ContentMimeType);
                    contentReaderOptions.RaiseContentFileMissingEvent(contentFileMissingEventArgs);
                    if (contentFileMissingEventArgs.ReplacementContentStream != null)
                    {
                        replacementContentFileEntry = new ReplacementContentFileEntry(contentFileMissingEventArgs.ReplacementContentStream);
                        contentFileEntry = replacementContentFileEntry;
                        throwMissingFileException = false;
                    }
                    else if (contentFileMissingEventArgs.SuppressException)
                    {
                        replacementContentFileEntry = new ReplacementContentFileEntry(new MemoryStream());
                        contentFileEntry = replacementContentFileEntry;
                        throwMissingFileException = false;
                    }
                }
                if (throwMissingFileException)
                {
                    throw new EpubContentException($"EPUB parsing error: file \"{contentFilePath}\" was not found in the EPUB file.", contentFilePath);
                }
            }
            if (contentFileEntry.Length > Int32.MaxValue)
            {
                throw new EpubContentException($"EPUB parsing error: file \"{contentFilePath}\" is larger than 2 GB.", contentFilePath);
            }
            return contentFileEntry;
        }

        private sealed class ReplacementContentFileEntry : IZipFileEntry
        {
            private readonly byte[] replacementStreamContent;

            public ReplacementContentFileEntry(Stream replacementStream)
            {
                using (replacementStream)
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    replacementStream.CopyTo(memoryStream);
                    replacementStreamContent = memoryStream.ToArray();
                }
            }

            public long Length => replacementStreamContent.Length;

            public Stream Open()
            {
                return new MemoryStream(replacementStreamContent);
            }
        }
    }
}
