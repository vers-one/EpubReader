using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersFx.Formats.Text.Epub.Utils
{
    internal static class ZipPathUtils
    {
        public static string GetDirectoryPath(string filePath)
        {
            int lastSlashIndex = filePath.LastIndexOf('/');
            if (lastSlashIndex == -1)
                return String.Empty;
            else
                return filePath.Substring(0, lastSlashIndex);
        }

        public static string Combine(string directory, string fileName)
        {
            if (String.IsNullOrEmpty(directory))
                return fileName;
            else
                return String.Concat(directory, "/", fileName);
        }
    }
}
