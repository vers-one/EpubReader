using VersOne.Epub.Options;
using VersOne.Epub.Test.Integration.CustomTypeHandlers;
using VersOne.Epub.Test.Integration.JsonUtils;
using VersOne.Epub.Test.Integration.Types;

namespace VersOne.Epub.Test.Integration.Runner
{
    internal class TestCaseWriter
    {
        private readonly string rootTestCasesDirectory;
        private readonly string testCasesFileName;
        private readonly string testEpubFileName;

        public TestCaseWriter(string rootTestCasesDirectory, string testCasesFileName, string testEpubFileName)
        {
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

        private static void WriteTestCases(string testCaseFilePath, string testEpubPath, List<TestCase> testCases)
        {
            using TestCasesSerializationContext testCasesSerializationContext = new(testEpubPath);
            TestCaseSerializer testCaseSerializer = new(testCasesSerializationContext);
            testCaseSerializer.Serialize(testCaseFilePath, testCases);
        }
    }
}
