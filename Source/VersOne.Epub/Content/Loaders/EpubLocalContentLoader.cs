using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;

namespace VersOne.Epub.Internal
{
    internal class EpubLocalContentLoader : EpubContentLoader
    {
        private readonly ContentReaderOptions contentReaderOptions;
        private readonly IZipFile epubFile;
        private readonly string contentDirectoryPath;
        private readonly Dictionary<string, ReplacementContentFileEntry> replacementContentFileEntries;

        public EpubLocalContentLoader(IEnvironmentDependencies environmentDependencies, ContentReaderOptions contentReaderOptions, IZipFile epubFile, string contentDirectoryPath)
            : base(environmentDependencies)
        {
            this.contentReaderOptions = contentReaderOptions ?? new ContentReaderOptions();
            this.epubFile = epubFile ?? throw new ArgumentNullException(nameof(epubFile));
            this.contentDirectoryPath = contentDirectoryPath ?? throw new ArgumentNullException(nameof(contentDirectoryPath));
            replacementContentFileEntries = new Dictionary<string, ReplacementContentFileEntry>();
        }

        public override async Task<byte[]> LoadContentAsBytesAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            IZipFileEntry contentFileEntry = GetContentFileEntry(contentFileRefMetadata);
            byte[] content = new byte[(int)contentFileEntry.Length];
            using (Stream contentStream = contentFileEntry.Open())
            using (MemoryStream memoryStream = new MemoryStream(content))
            {
                await contentStream.CopyToAsync(memoryStream).ConfigureAwait(false);
            }
            return content;
        }

        public override async Task<string> LoadContentAsTextAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            using (Stream contentStream = GetContentFileEntry(contentFileRefMetadata).Open())
            using (StreamReader streamReader = new StreamReader(contentStream))
            {
                return await streamReader.ReadToEndAsync().ConfigureAwait(false);
            }
        }

        public override Task<Stream> GetContentStreamAsync(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            return Task.FromResult(GetContentFileEntry(contentFileRefMetadata).Open());
        }

        private IZipFileEntry GetContentFileEntry(EpubContentFileRefMetadata contentFileRefMetadata)
        {
            if (replacementContentFileEntries.TryGetValue(contentFileRefMetadata.Key, out ReplacementContentFileEntry replacementContentFileEntry))
            {
                return replacementContentFileEntry;
            }
            if (epubFile.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(epubFile), "EPUB file stored within this local content file loader has been disposed.");
            }
            string contentFilePath = ZipPathUtils.Combine(contentDirectoryPath, contentFileRefMetadata.Key);
            IZipFileEntry contentFileEntry = epubFile.GetEntry(contentFilePath);
            if (contentFileEntry == null)
            {
                bool throwMissingFileException = true;
                ContentFileMissingEventArgs contentFileMissingEventArgs =
                    new ContentFileMissingEventArgs(contentFileRefMetadata.Key, contentFilePath, contentFileRefMetadata.ContentType, contentFileRefMetadata.ContentMimeType);
                contentReaderOptions.RaiseContentFileMissingEvent(contentFileMissingEventArgs);
                if (contentFileMissingEventArgs.ReplacementContentStream != null)
                {
                    replacementContentFileEntry = new ReplacementContentFileEntry(contentFileMissingEventArgs.ReplacementContentStream);
                    contentFileEntry = replacementContentFileEntry;
                    throwMissingFileException = false;
                }
                else if (contentFileMissingEventArgs.SuppressException)
                {
                    replacementContentFileEntry = new ReplacementContentFileEntry(new MemoryStream());
                    contentFileEntry = replacementContentFileEntry;
                    throwMissingFileException = false;
                }
                if (throwMissingFileException)
                {
                    throw new EpubContentException($"EPUB parsing error: file \"{contentFilePath}\" was not found in the EPUB file.", contentFilePath);
                }
            }
            if (contentFileEntry.Length > Int32.MaxValue)
            {
                throw new EpubContentException($"EPUB parsing error: file \"{contentFilePath}\" is larger than 2 GB.", contentFilePath);
            }
            if (replacementContentFileEntry != null)
            {
                replacementContentFileEntries.Add(contentFileRefMetadata.Key, replacementContentFileEntry);
            }
            return contentFileEntry;
        }
    }
}
