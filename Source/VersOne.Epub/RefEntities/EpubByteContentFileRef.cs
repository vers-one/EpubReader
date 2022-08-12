using System.Threading.Tasks;

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
        /// <param name="fileName">Relative file path of the content file (as it is specified in the EPUB manifest).</param>
        /// <param name="contentType">The type of the content of the file.</param>
        /// <param name="contentMimeType">The MIME type of the content of the file.</param>
        public EpubByteContentFileRef(EpubBookRef epubBookRef, string fileName, EpubContentType contentType, string contentMimeType)
            : base(epubBookRef, fileName, contentType, contentMimeType)
        {
        }

        /// <summary>
        /// Reads the whole content of the referenced file and returns it as a byte array.
        /// </summary>
        /// <returns>Content of the referenced file.</returns>
        public byte[] ReadContent()
        {
            return ReadContentAsBytes();
        }

        /// <summary>
        /// Asynchronously reads the whole content of the referenced file and returns it as a byte array.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The value of the TResult parameter contains the content of the referenced file.</returns>
        public Task<byte[]> ReadContentAsync()
        {
            return ReadContentAsBytesAsync();
        }
    }
}
