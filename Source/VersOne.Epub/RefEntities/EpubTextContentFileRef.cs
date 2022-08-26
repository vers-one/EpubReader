using System.Threading.Tasks;
using VersOne.Epub.Options;

namespace VersOne.Epub
{
    /// <summary>
    /// A reference for a file within the EPUB archive which allows to read its content as a string.
    /// Unlike <see cref="EpubTextContentFile" />, this class contains only a reference to the file but doesn't contain its content.
    /// </summary>
    public class EpubTextContentFileRef : EpubContentFileRef
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubTextContentFileRef" /> class with a specified EPUB book reference, a file name, a content type of the file,
        /// and a MIME type of the file's content.
        /// </summary>
        /// <param name="epubBookRef">EPUB book reference object which contains this content file reference.</param>
        /// <param name="fileName">Relative file path of the content file (as it is specified in the EPUB manifest).</param>
        /// <param name="contentType">The type of the content of the file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the file.</param>
        /// <param name="contentReaderOptions">Optional content reader options determining how to handle missing content files.</param>
        public EpubTextContentFileRef(EpubBookRef epubBookRef, string fileName, EpubContentType contentType, string contentMimeType, ContentReaderOptions contentReaderOptions = null)
            : base(epubBookRef, fileName, contentType, contentMimeType, contentReaderOptions)
        {
        }

        /// <summary>
        /// Reads the whole content of the referenced file and returns it as a string.
        /// </summary>
        /// <returns>Content of the referenced file.</returns>
        public string ReadContent()
        {
            return ReadContentAsText();
        }

        /// <summary>
        /// Asynchronously reads the whole content of the referenced file and returns it as a string.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The value of the TResult parameter contains the content of the referenced file.</returns>
        public Task<string> ReadContentAsync()
        {
            return ReadContentAsTextAsync();
        }
    }
}
