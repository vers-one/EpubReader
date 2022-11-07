using System.Threading.Tasks;

namespace VersOne.Epub
{
    /// <summary>
    /// A reference for a file within the EPUB archive which allows to read its content as a byte array.
    /// Unlike <see cref="EpubLocalByteContentFile" />, this class contains only a reference to the file but doesn't contain its content.
    /// </summary>
    public class EpubLocalByteContentFileRef : EpubLocalContentFileRef
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubLocalByteContentFileRef" /> class with a specified EPUB book reference, a file name, a content type of the file,
        /// and a MIME type of the file's content.
        /// </summary>
        /// <param name="metadata">Metadata of this content file reference.</param>
        /// <param name="filePath">The absolute path of the local content file in the EPUB archive.</param>
        /// <param name="epubContentLoader">A reference to the EPUB content loader which provides methods to load the content of this file.</param>
        public EpubLocalByteContentFileRef(EpubContentFileRefMetadata metadata, string filePath, IEpubContentLoader epubContentLoader)
            : base(metadata, filePath, epubContentLoader)
        {
        }

        /// <summary>
        /// Gets the type of the content file which is always <see cref="EpubContentFileType.BYTE_ARRAY" /> for local byte content file references.
        /// </summary>
        public override EpubContentFileType ContentFileType => EpubContentFileType.BYTE_ARRAY;

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
