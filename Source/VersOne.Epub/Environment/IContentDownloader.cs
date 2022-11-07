using System.IO;
using System.Threading.Tasks;

namespace VersOne.Epub.Environment
{
    /// <summary>
    /// Provides methods for downloading remote content files.
    /// </summary>
    public interface IContentDownloader
    {
        /// <summary>
        /// Starts downloading the remote content file.
        /// </summary>
        /// <param name="url">The absolute URI of the remote content file.</param>
        /// <param name="userAgent">The value for the <c>User-Agent</c> header of the HTTP request.</param>
        /// <returns>
        /// A task that represents the asynchronous download operation. The value of the TResult parameter contains the readable stream for the content of the remote content file.
        /// </returns>
        Task<Stream> DownloadAsync(string url, string userAgent);
    }
}
