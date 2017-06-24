using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Didstopia.EpubReader.Utils
{
    internal static class UrlUtils
    {
        public static async Task<bool> FileExistsAtUrl(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";

                HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        public static bool FileIsUrl(string url)
        {
            Uri uriResult;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uriResult)) return false;
            if (uriResult == null) return false;

            return  uriResult.Scheme == "http" || 
                    uriResult.Scheme == "https" || 
                    uriResult.Scheme == "ftp";
        }

        public static async Task<string> UrlToFile(string url)
        {
            HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
            httpRequest.Method = "GET";

            HttpWebResponse httpResponse = await httpRequest.GetResponseAsync() as HttpWebResponse;

            var tempFilePath = Path.GetTempFileName();
            Stream httpResponseStream = httpResponse.GetResponseStream();
            using (var fs = File.Create(tempFilePath))
            {
                httpResponseStream.CopyTo(fs);
            }

            return tempFilePath;
        }
    }
}
