using VersOne.Epub.Environment;

namespace VersOne.Epub.Test.Unit.Mocks
{
    internal class TestZipFile : IZipFile
    {
        private readonly Dictionary<string, IZipFileEntry> entries;

        public TestZipFile()
        {
            entries = new Dictionary<string, IZipFileEntry>();
            IsDisposed = false;
        }

        public bool IsDisposed { get; private set; }

        public void AddEntry(string entryName, IZipFileEntry entry)
        {
            entries.Add(entryName, entry);
        }

        public void AddEntry(string entryName, string entryContent)
        {
            entries.Add(entryName, new TestZipFileEntry(entryContent));
        }

        public void AddEntry(string entryName, byte[] entryContent)
        {
            entries.Add(entryName, new TestZipFileEntry(entryContent));
        }

        public IZipFileEntry GetEntry(string entryName)
        {
            return entries.TryGetValue(entryName, out IZipFileEntry value) ? value : null;
        }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}
