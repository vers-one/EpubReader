using System;

namespace VersOne.Epub.Environment
{
    public interface IZipFile : IDisposable
    {
        IZipFileEntry GetEntry(string entryName);
    }
}
