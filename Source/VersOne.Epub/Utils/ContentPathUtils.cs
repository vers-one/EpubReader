using System;

namespace VersOne.Epub.Internal
{
    internal static class ContentPathUtils
    {
        private const string DIRECTORY_UP = "../";

        public static bool IsLocalPath(string path) => path != null ? !path.Contains("://") : throw new ArgumentNullException(nameof(path));

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
            else if (String.IsNullOrEmpty(fileName))
            {
                return directory;
            }
            else
            {
                while (fileName.StartsWith(DIRECTORY_UP))
                {
                    int lastDirectorySlashIndex = directory.LastIndexOf('/');
                    directory = lastDirectorySlashIndex != -1 ? directory.Substring(0, lastDirectorySlashIndex) : String.Empty;
                    fileName = fileName.Substring(DIRECTORY_UP.Length);
                }
                return String.IsNullOrEmpty(directory) ? fileName : String.Concat(directory, '/', fileName);
            }
        }
    }
}
