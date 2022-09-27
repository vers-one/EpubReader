using VersOne.Epub.Options;

namespace VersOne.Epub.Test.Integration.Runner
{
    public class TestCase
    {
        public string Name { get; set; }
        public EpubReaderOptions Options { get; set; }
        public EpubBook ExpectedResult { get; set; }
    }
}
