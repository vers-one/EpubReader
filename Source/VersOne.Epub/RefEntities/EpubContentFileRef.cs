using System;
using System.IO;
using System.Threading.Tasks;
using VersOne.Epub.Environment;
using VersOne.Epub.Internal;

namespace VersOne.Epub
{
    public abstract class EpubContentFileRef
    {
        private readonly EpubBookRef epubBookRef;

        protected EpubContentFileRef(EpubBookRef epubBookRef, string fileName, EpubContentType contentType, string contentMimeType)
        {
            this.epubBookRef = epubBookRef;
            FileName = fileName;
            FilePathInEpubArchive = ZipPathUtils.Combine(epubBookRef.Schema.ContentDirectoryPath, FileName);
            ContentType = contentType;
            ContentMimeType = contentMimeType;
        }

        public string FileName { get; }
        public string FilePathInEpubArchive { get; }
        public EpubContentType ContentType { get; }
        public string ContentMimeType { get; }

        public byte[] ReadContentAsBytes()
        {
            return ReadContentAsBytesAsync().Result;
        }

        public async Task<byte[]> ReadContentAsBytesAsync()
        {
            IZipFileEntry contentFileEntry = GetContentFileEntry();
            byte[] content = new byte[(int)contentFileEntry.Length];
            using (Stream contentStream = OpenContentStream(contentFileEntry))
            using (MemoryStream memoryStream = new MemoryStream(content))
            {
                await contentStream.CopyToAsync(memoryStream).ConfigureAwait(false);
            }
            return content;
        }

        public string ReadContentAsText()
        {
            return ReadContentAsTextAsync().Result;
        }

        public async Task<string> ReadContentAsTextAsync()
        {
            using (Stream contentStream = GetContentStream())
            using (StreamReader streamReader = new StreamReader(contentStream))
            {
                return await streamReader.ReadToEndAsync().ConfigureAwait(false);
            }
        }

        public Stream GetContentStream()
        {
            return OpenContentStream(GetContentFileEntry());
        }

        private IZipFileEntry GetContentFileEntry()
        {
            if (String.IsNullOrEmpty(FileName))
            {
                throw new EpubPackageException("EPUB parsing error: file name of the specified content file is empty.");
            }
            string contentFilePath = FilePathInEpubArchive;
            IZipFileEntry contentFileEntry = epubBookRef.EpubFile.GetEntry(contentFilePath);
            if (contentFileEntry == null)
            {
                throw new EpubContentException($"EPUB parsing error: file \"{contentFilePath}\" was not found in the EPUB file.", contentFilePath);
            }
            if (contentFileEntry.Length > Int32.MaxValue)
            {
                throw new EpubContentException($"EPUB parsing error: file \"{contentFilePath}\" is larger than 2 GB.", contentFilePath);
            }
            return contentFileEntry;
        }

        private Stream OpenContentStream(IZipFileEntry contentFileEntry)
        {
            Stream contentStream = contentFileEntry.Open();
            if (contentStream == null)
            {
                throw new EpubContentException($"Incorrect EPUB file: content file \"{FileName}\" specified in the manifest was not found in the EPUB file.", FilePathInEpubArchive);
            }
            return contentStream;
        }
    }
}
