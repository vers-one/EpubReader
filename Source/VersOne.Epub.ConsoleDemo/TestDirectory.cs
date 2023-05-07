using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VersOne.Epub.ConsoleDemo
{
    internal static class TestDirectory
    {
        public static void Run(string directoryPath)
        {
            int totalFiles = 0;
            Dictionary<string, int> filesByVersion = new();
            List<string> filesWithErrors = new();
            TestEpubDirectory(directoryPath, ref totalFiles, filesByVersion, filesWithErrors);
            if (totalFiles == 0)
            {
                Console.WriteLine("No EPUB files found.");
            }
            else
            {
                if (filesByVersion.Any(version => version.Value > 0))
                {
                    Console.WriteLine("Statistics");
                    Console.WriteLine("-----------------------------------");
                    Console.Write("Versions: ");
                    bool firstItem = true;
                    foreach (string epubVersionString in filesByVersion.Keys.OrderBy(version => version))
                    {
                        if (firstItem)
                        {
                            firstItem = false;
                        }
                        else
                        {
                            Console.Write(", ");
                        }
                        Console.Write(epubVersionString + ": " + filesByVersion[epubVersionString]);
                    }
                    Console.WriteLine(".");
                }
                if (filesWithErrors.Any())
                {
                    Console.WriteLine("Files with errors:");
                    foreach (string filePath in filesWithErrors)
                    {
                        Console.WriteLine(filePath);
                    }
                }
                else
                {
                    Console.WriteLine("No errors.");
                }
                Console.WriteLine();
            }
        }

        private static void TestEpubDirectory(string directoryPath, ref int totalFiles, Dictionary<string, int> filesByVersion, List<string> filesWithErrors)
        {
            foreach (string subdirectoryPath in Directory.EnumerateDirectories(directoryPath))
            {
                TestEpubDirectory(subdirectoryPath, ref totalFiles, filesByVersion, filesWithErrors);
            }
            foreach (string filePath in Directory.EnumerateFiles(directoryPath, "*.epub"))
            {
                TestEpubFile(filePath, filesByVersion, filesWithErrors);
                totalFiles++;
            }
        }

        private static void TestEpubFile(string epubFilePath, Dictionary<string, int> filesByVersion, List<string> filesWithErrors)
        {
            Console.WriteLine($"File: {epubFilePath}");
            Console.WriteLine("-----------------------------------");
            try
            {
                using EpubBookRef bookRef = EpubReader.OpenBook(epubFilePath);
                string epubVersionString = bookRef.Schema.Package.GetVersionString();
                if (filesByVersion.TryGetValue(epubVersionString, out int fileCount))
                {
                    fileCount++;
                }
                else
                {
                    fileCount = 1;
                }
                filesByVersion[epubVersionString] = fileCount;
                Console.WriteLine($"EPUB version: {epubVersionString}");
                Console.WriteLine($"Total files: {bookRef.Content.AllFiles.Local.Count}, HTML files: {bookRef.Content.Html.Local.Count}," +
                    $" CSS files: {bookRef.Content.Css.Local.Count}, image files: {bookRef.Content.Images.Local.Count}, font files: {bookRef.Content.Fonts.Local.Count}.");
                Console.WriteLine($"Reading order: {bookRef.GetReadingOrder().Count} file(s).");
                Console.WriteLine("Navigation:");
                foreach (EpubNavigationItemRef navigationItemRef in bookRef.GetNavigation())
                {
                    PrintNavigationItem(navigationItemRef, 0);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                filesWithErrors.Add(epubFilePath);
            }
            Console.WriteLine();
        }

        private static void PrintNavigationItem(EpubNavigationItemRef navigationItemRef, int identLevel)
        {
            Console.Write(new string(' ', identLevel * 2));
            Console.WriteLine(navigationItemRef.Title);
            foreach (EpubNavigationItemRef nestedNavigationItemRef in navigationItemRef.NestedItems)
            {
                PrintNavigationItem(nestedNavigationItemRef, identLevel + 1);
            }
        }
    }
}
