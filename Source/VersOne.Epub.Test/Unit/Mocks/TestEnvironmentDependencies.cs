using VersOne.Epub.Environment;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class TestEnvironmentDependencies : IEnvironmentDependencies
    {
        public TestEnvironmentDependencies(IFileSystem? fileSystem = null, IContentDownloader? contentDownloader = null)
        {
            FileSystem = fileSystem ?? new TestFileSystem();
            ContentDownloader = contentDownloader ?? new TestContentDownloader();
        }

        public IFileSystem FileSystem { get; set; }

        public IContentDownloader ContentDownloader { get; set; }
    }
}
