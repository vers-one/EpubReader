namespace VersOne.Epub.Environment.Implementation
{
    internal class EnvironmentDependencies : IEnvironmentDependencies
    {
        public EnvironmentDependencies()
        {
            FileSystem = new FileSystem();
            ContentDownloader = new ContentDownloader();
        }

        public IFileSystem FileSystem { get; }

        public IContentDownloader ContentDownloader { get; }
    }
}
