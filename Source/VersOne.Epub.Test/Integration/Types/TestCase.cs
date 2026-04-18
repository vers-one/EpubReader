using VersOne.Epub.Options;

namespace VersOne.Epub.Test.Integration.Types
{
    public class TestCase(string name, EpubReaderOptions? options = null, List<EpubContentFile>? contentFiles = null, EpubBook? expectedResult = null,
        TestCaseException? expectedException = null)
    {
        public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
        public EpubReaderOptions? Options { get; } = options;
        public List<EpubContentFile>? ContentFiles { get; } = contentFiles ?? GetContentFiles(expectedResult);
        public EpubBook? ExpectedResult { get; } = expectedResult;
        public TestCaseException? ExpectedException { get; } = expectedException;

        private static List<EpubContentFile>? GetContentFiles(EpubBook? epubBook)
        {
            if (epubBook == null)
            {
                return null;
            }
            List<EpubContentFile> result = [.. epubBook.Content.AllFiles.Local, .. epubBook.Content.AllFiles.Remote];
            return result;
        }
    }
}
