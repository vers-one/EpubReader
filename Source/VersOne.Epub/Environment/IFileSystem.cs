using System.IO;

namespace VersOne.Epub.Environment
{
    internal interface IFileSystem
    {
        bool FileExists(string path);
        IZipFile OpenZipFile(string path);
        IZipFile OpenZipFile(Stream stream);
    }
}
