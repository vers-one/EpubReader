using VersOne.Epub.Options;
using VersOne.Epub.Test.Integration.CustomSerialization;
using VersOne.Epub.Test.Integration.Types;

namespace VersOne.Epub.Test.Integration.Runner
{
    internal class TestCaseWriter
    {
        private readonly TestCaseSerializer testCaseSerializer;
        private readonly string rootTestCasesDirectory;
        private readonly string testCasesFileName;
        private readonly string testEpubFileName;

        public TestCaseWriter(TestCaseSerializer testCaseSerializer, string rootTestCasesDirectory, string testCasesFileName, string testEpubFileName)
        {
            this.testCaseSerializer = testCaseSerializer;
            this.rootTestCasesDirectory = rootTestCasesDirectory;
            this.testCasesFileName = testCasesFileName;
            this.testEpubFileName = testEpubFileName;
        }

        public void WriteRemoteContentTestCases()
        {
            string testCaseDirectoryPath = "Features\\RemoteContent";
            string testCaseDirectoryAbsolutePath = Path.Combine(rootTestCasesDirectory, testCaseDirectoryPath);
            string testCasesPath = Path.Combine(testCaseDirectoryAbsolutePath, testCasesFileName);
            string testEpubPath = Path.Combine(testCaseDirectoryAbsolutePath, testEpubFileName);
            TestCase testCase1 = new
            (
                name: "EPUB 3 book with remote content files - without options",
                expectedResult: EpubReader.ReadBook(testEpubPath)
            );
            EpubReaderOptions testCase2Options = new()
            {
                ContentDownloaderOptions = new ContentDownloaderOptions()
                {
                    DownloadContent = true,
                    DownloaderUserAgent = "EpubReader Integration Test Runner (https://github.com/vers-one/EpubReader)"
                }
            };
            TestCase testCase2 = new
            (
                name: "EPUB 3 book with remote content files - with options",
                options: testCase2Options,
                expectedResult: EpubReader.ReadBook(testEpubPath, testCase2Options)
            );
            WriteTestCases(testCasesPath, testEpubPath, new List<TestCase>() { testCase1, testCase2 });
        }

        private void WriteTestCases(string testCaseFilePath, string testEpubPath, List<TestCase> testCases)
        {
            testCaseSerializer.Serialize(testCaseFilePath, testEpubPath, testCases);
        }
    }
}
