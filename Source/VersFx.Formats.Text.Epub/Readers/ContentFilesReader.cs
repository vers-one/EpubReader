using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using VersFx.Formats.Text.Epub.Entities;
using VersFx.Formats.Text.Epub.Schema.Opf;

namespace VersFx.Formats.Text.Epub.Readers
{
    internal static class ContentFilesReader
    {
        public static List<EpubContentFile> ReadContentFiles(ZipArchive epubArchive, string baseDirectoryEntryPath, EpubManifest manifest)
        {
            List<EpubContentFile> result = new List<EpubContentFile>();
            foreach (EpubManifestItem manifestItem in manifest)
            {
                string contentFileEntryPath = String.Concat(baseDirectoryEntryPath, "/", manifestItem.Href);
                ZipArchiveEntry contentFileEntry = epubArchive.GetEntry(contentFileEntryPath);
                if (contentFileEntry == null)
                    throw new Exception(String.Format("EPUB parsing error: file {0} not found in archive.", contentFileEntryPath));
                if (contentFileEntry.Length > Int32.MaxValue)
                    throw new Exception(String.Format("EPUB parsing error: file {0} is bigger than 2 Gb.", contentFileEntryPath));
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
    }
}
