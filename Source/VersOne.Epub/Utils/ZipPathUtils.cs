using System;

namespace VersOne.Epub.Internal
{
    internal static class ZipPathUtils
    {
        public static string GetDirectoryPath(string filePath)
        {
            int lastSlashIndex = filePath.LastIndexOf('/');
            if (lastSlashIndex == -1)
            {
                return String.Empty;
            }
            else
            {
                return filePath.Substring(0, lastSlashIndex);
            }
        }

        public static string Combine(string directory, string fileName)
        {
            if (String.IsNullOrEmpty(directory))
            {
                return fileName;
            }
            else
            {
                while (fileName.StartsWith("../"))
                {
                    int idx = directory.LastIndexOf("/");
                    directory = idx > 0 ? directory.Substring(0, idx) : string.Empty;
                    fileName = fileName.Substring(3);
                }

                return string.IsNullOrEmpty(directory) ? fileName : String.Concat(directory, "/", fileName);
            }
        }
    }
}
