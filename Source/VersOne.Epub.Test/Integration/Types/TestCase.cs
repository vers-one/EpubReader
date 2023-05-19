using VersOne.Epub.Options;

namespace VersOne.Epub.Test.Integration.Types
{
    public class TestCase
    {
        public TestCase(string name, EpubReaderOptions? options = null, List<EpubContentFile>? contentFiles = null, EpubBook? expectedResult = null,
            TestCaseException? expectedException = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Options = options;
            ContentFiles = contentFiles ?? GetContentFiles(expectedResult);
            ExpectedResult = expectedResult;
            ExpectedException = expectedException;
        }

        public string Name { get; }
        public EpubReaderOptions? Options { get; }
        public List<EpubContentFile>? ContentFiles { get; }
        public EpubBook? ExpectedResult { get; }
        public TestCaseException? ExpectedException { get; }

        private static List<EpubContentFile>? GetContentFiles(EpubBook? epubBook)
        {
            if (epubBook == null)
            {
                return null;
            }
            List<EpubContentFile> result = new(epubBook.Content.AllFiles.Local.Count + epubBook.Content.AllFiles.Remote.Count);
            foreach (EpubContentFile epubContentFile in epubBook.Content.AllFiles.Local)
            {
                result.Add(epubContentFile);
            }
            foreach (EpubContentFile epubContentFile in epubBook.Content.AllFiles.Remote)
            {
                result.Add(epubContentFile);
            }
            return result;
        }
    }
}
