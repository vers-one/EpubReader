using System.IO;
using System.Threading.Tasks;

namespace VersOne.Epub
{
    /// <summary>
    /// Provides methods for loading content files.
    /// </summary>
    public interface IEpubContentLoader
    {
        /// <summary>
        /// Loads the whole content of the file and returns it as a byte array.
        /// </summary>
        /// <param name="contentFileRefMetadata">Metadata of the content file reference to load.</param>
        /// <returns>Content of the file.</returns>
        byte[] LoadContentAsBytes(EpubContentFileRefMetadata contentFileRefMetadata);

        /// <summary>
        /// Asynchronously loads the whole content of the file and returns it as a byte array.
        /// </summary>
        /// <param name="contentFileRefMetadata">Metadata of the content file reference to load.</param>
        /// <returns>A task that represents the asynchronous load operation. The value of the TResult parameter contains the content of the file.</returns>
        Task<byte[]> LoadContentAsBytesAsync(EpubContentFileRefMetadata contentFileRefMetadata);

        /// <summary>
        /// Loads the whole content of the file and returns it as a string.
        /// </summary>
        /// <param name="contentFileRefMetadata">Metadata of the content file reference to load.</param>
        /// <returns>Content of the file.</returns>
        string LoadContentAsText(EpubContentFileRefMetadata contentFileRefMetadata);

        /// <summary>
        /// Asynchronously loads the whole content of the file and returns it as a string.
        /// </summary>
        /// <param name="contentFileRefMetadata">Metadata of the content file reference to load.</param>
        /// <returns>A task that represents the asynchronous load operation. The value of the TResult parameter contains the content of the file.</returns>
        Task<string> LoadContentAsTextAsync(EpubContentFileRefMetadata contentFileRefMetadata);

        /// <summary>
        /// Opens the file and returns a <see cref="Stream" /> to access its content.
        /// </summary>
        /// <param name="contentFileRefMetadata">Metadata of the content file reference to load.</param>
        /// <returns>A <see cref="Stream" /> to access the file's content.</returns>
        Stream GetContentStream(EpubContentFileRefMetadata contentFileRefMetadata);

        /// <summary>
        /// Asynchronously opens the file and returns a <see cref="Stream" /> to access its content.
        /// </summary>
        /// <param name="contentFileRefMetadata">Metadata of the content file reference to load.</param>
        /// <returns>
        /// A task that represents the asynchronous open operation. The value of the TResult parameter contains the <see cref="Stream" /> to access the file's content.
        /// </returns>
        Task<Stream> GetContentStreamAsync(EpubContentFileRefMetadata contentFileRefMetadata);
    }
}
