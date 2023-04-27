using VersOne.Epub.Options;

namespace VersOne.Epub.Test.Integration.Types
{
    public class TestCase
    {
        public TestCase(string name, EpubReaderOptions? options = null, EpubBook? expectedResult = null, TestCaseException? expectedException = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Options = options;
            ExpectedResult = expectedResult;
            ExpectedException = expectedException;
        }

        public string Name { get; }
        public EpubReaderOptions? Options { get; }
        public EpubBook? ExpectedResult { get; }
        public TestCaseException? ExpectedException { get; }
    }
}
