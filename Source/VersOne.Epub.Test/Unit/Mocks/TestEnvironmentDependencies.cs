using VersOne.Epub.Environment;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class TestEnvironmentDependencies(IFileSystem? fileSystem = null, IContentDownloader? contentDownloader = null) : IEnvironmentDependencies
    {
        public IFileSystem FileSystem { get; set; } = fileSystem ?? new TestFileSystem();

        public IContentDownloader ContentDownloader { get; set; } = contentDownloader ?? new TestContentDownloader();
    }
}
