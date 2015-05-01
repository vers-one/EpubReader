using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using VersFx.Formats.Text.Epub.Entities;
using VersFx.Formats.Text.Epub.Schema.Opf;
using VersFx.Formats.Text.Epub.Utils;

namespace VersFx.Formats.Text.Epub.Readers
{
    internal static class ContentFileReader
    {
        public static List<EpubContentFile> ReadContentFiles(ZipArchive epubArchive, string contentDirectoryPath, EpubManifest manifest)
        {
            List<EpubContentFile> result = new List<EpubContentFile>();
            foreach (EpubManifestItem manifestItem in manifest)
            {
                string contentFilePath = ZipPathUtils.Combine(contentDirectoryPath, manifestItem.Href);
                ZipArchiveEntry contentFileEntry = epubArchive.GetEntry(contentFilePath);
                if (contentFileEntry == null)
                    throw new Exception(String.Format("EPUB parsing error: file {0} not found in archive.", contentFilePath));
                if (contentFileEntry.Length > Int32.MaxValue)
                    throw new Exception(String.Format("EPUB parsing error: file {0} is bigger than 2 Gb.", contentFilePath));
                EpubContentFile contentFile = new EpubContentFile();
                contentFile.FileName = manifestItem.Href;
                using (Stream contentFileStream = contentFileEntry.Open())
                using (MemoryStream memoryStream = new MemoryStream((int)contentFileEntry.Length))
                {
                    contentFileStream.CopyTo(memoryStream);
                    contentFile.Content = memoryStream.ToArray();
                }
                result.Add(contentFile);
            }
            return result;
        }

        public static Stream OpenContentFile(ZipArchive epubArchive, string contentFilePath)
        {
            ZipArchiveEntry contentFileEntry = epubArchive.GetEntry(contentFilePath);
            if (contentFileEntry == null)
                return null;
            return contentFileEntry.Open();
        }
    }
}
