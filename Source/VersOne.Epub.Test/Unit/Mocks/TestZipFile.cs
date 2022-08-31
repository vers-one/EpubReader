using VersOne.Epub.Environment;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class TestZipFile : IZipFile
    {
        readonly Dictionary<string, TestZipFileEntry> entries;

        public TestZipFile()
        {
            entries = new Dictionary<string, TestZipFileEntry>();
        }

        public void AddEntry(string entryName, string entryContent)
        {
            entries.Add(entryName, new TestZipFileEntry(entryContent));
        }

        public IZipFileEntry GetEntry(string entryName)
        {
            return entries.TryGetValue(entryName, out TestZipFileEntry value) ? value : null;
        }

        public void Dispose()
        {
        }
    }
}
