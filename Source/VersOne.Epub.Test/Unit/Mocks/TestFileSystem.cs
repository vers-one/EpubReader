using VersOne.Epub.Environment;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class TestFileSystem : IFileSystem
    {
        private readonly Dictionary<string, TestZipFile> testZipFilesByPath;
        private readonly Dictionary<Stream, TestZipFile> testZipFilesByStream;

        public TestFileSystem()
        {
            testZipFilesByPath = new Dictionary<string, TestZipFile>();
            testZipFilesByStream = new Dictionary<Stream, TestZipFile>();
        }

        public TestFileSystem(string epubFilePath, TestZipFile testEpubFile)
            : this()
        {
            AddTestZipFile(epubFilePath, testEpubFile);
        }

        public TestFileSystem(Stream epubFileStream, TestZipFile testEpubFile)
            : this()
        {
            AddTestZipFile(epubFileStream, testEpubFile);
        }

        public void AddTestZipFile(string path, TestZipFile testZipFile)
        {
            testZipFilesByPath.Add(path, testZipFile);
        }

        public void AddTestZipFile(Stream stream, TestZipFile testZipFile)
        {
            testZipFilesByStream.Add(stream, testZipFile);
        }

        public bool FileExists(string path)
        {
            return testZipFilesByPath.ContainsKey(path);
        }

        public IZipFile OpenZipFile(string path)
        {
            return testZipFilesByPath[path];
        }

        public IZipFile OpenZipFile(Stream stream)
        {
            return testZipFilesByStream[stream];
        }
    }
}
