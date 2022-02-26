using VersOne.Epub.Environment.Implementation;

namespace VersOne.Epub.Environment
{
    internal static class EnvironmentDependencies
    {
        static EnvironmentDependencies()
        {
            FileSystem = new FileSystem();
        }

        public static IFileSystem FileSystem { get; internal set; }
    }
}
