using System.IO;

namespace VersOne.Epub.Environment
{
    public interface IZipFileEntry
    {
        long Length { get; }

        Stream Open();
    }
}
