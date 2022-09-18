using System;
using System.Threading.Tasks;
using VersOne.Epub.Options;

namespace VersOne.Epub
{
    /// <summary>
    /// A reference for a file within the EPUB archive which allows to read its content as a byte array.
    /// Unlike <see cref="EpubByteContentFile" />, this class contains only a reference to the file but doesn't contain its content.
    /// </summary>
    public class EpubByteContentFileRef : EpubContentFileRef
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubByteContentFileRef" /> class with a specified EPUB book reference, a file name, a content type of the file,
        /// and a MIME type of the file's content.
        /// </summary>
        /// <param name="epubBookRef">EPUB book reference object which contains this content file reference.</param>
        /// <param name="href">Relative file path or absolute URI of the content item (as it is specified in the EPUB manifest).</param>
        /// <param name="contentLocation">Location of the content item (local or remote).</param>
        /// <param name="contentType">The type of the content of the file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the file.</param>
        /// <param name="contentReaderOptions">Optional content reader options determining how to handle missing content files.</param>
        public EpubByteContentFileRef(EpubBookRef epubBookRef, string href, EpubContentLocation contentLocation, EpubContentType contentType, string contentMimeType,
            ContentReaderOptions contentReaderOptions = null)
            : base(epubBookRef, href, contentLocation, contentType, contentMimeType, contentReaderOptions)
        {
        }

        /// <summary>
        /// Reads the whole content of the referenced file and returns it as a byte array.
        /// Throws <see cref="InvalidOperationException" /> if <see cref="EpubContentFileRef.ContentLocation" /> is <see cref="EpubContentLocation.REMOTE" />.
        /// </summary>
        /// <returns>Content of the referenced file.</returns>
        public byte[] ReadContent()
        {
            return ReadContentAsBytes();
        }

        /// <summary>
        /// Asynchronously reads the whole content of the referenced file and returns it as a byte array.
        /// Throws <see cref="InvalidOperationException" /> if <see cref="EpubContentFileRef.ContentLocation" /> is <see cref="EpubContentLocation.REMOTE" />.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The value of the TResult parameter contains the content of the referenced file.</returns>
        public Task<byte[]> ReadContentAsync()
        {
            return ReadContentAsBytesAsync();
        }
    }
}
