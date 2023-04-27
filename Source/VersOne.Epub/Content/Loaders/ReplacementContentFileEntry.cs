using System.IO;
using VersOne.Epub.Environment;

namespace VersOne.Epub
{
    internal class ReplacementContentFileEntry : IZipFileEntry
    {
        private readonly byte[] replacementStreamContent;

        public ReplacementContentFileEntry(Stream replacementStream)
        {
            using (replacementStream)
            using (MemoryStream memoryStream = new())
            {
                replacementStream.CopyTo(memoryStream);
                replacementStreamContent = memoryStream.ToArray();
            }
        }

        public long Length => replacementStreamContent.Length;

        public Stream Open()
        {
            return new MemoryStream(replacementStreamContent);
        }
    }
}
