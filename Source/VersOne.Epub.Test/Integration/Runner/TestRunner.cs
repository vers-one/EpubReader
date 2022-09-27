using System.Runtime.CompilerServices;
using VersOne.Epub.Environment;
using VersOne.Epub.Environment.Implementation;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Integration.JsonUtils;
using VersOne.Epub.Test.Unit.TestData;

namespace VersOne.Epub.Test.Integration.Runner
{
    public class TestRunner
    {
        private const string TEST_CASES_DIRECTORY_NAME = "TestCases";
        private const string TEST_CASE_FILE_NAME = "testcase.json";
        private const string TEST_EPUB_FILE_NAME = "test.epub";

        private static readonly string rootTestCasesDirectory;

        static TestRunner()
        {
            rootTestCasesDirectory = GetRootTestCasesDirectory();
        }

        [Theory(DisplayName = "Integration test")]
        [MemberData(nameof(GetTestCaseDirectories))]
        public void Run(string testCaseDirectoryPath)
        {
            string testCaseDirectoryAbsolutePath = Path.Combine(rootTestCasesDirectory, testCaseDirectoryPath);
            string testCasePath = Path.Combine(testCaseDirectoryAbsolutePath, TEST_CASE_FILE_NAME);
            string testEpubPath = Path.Combine(testCaseDirectoryAbsolutePath, TEST_EPUB_FILE_NAME);
            TestCase testCase = ReadTestCase(testCasePath, testEpubPath);
            EpubBook epubBook = EpubReader.ReadBook(testEpubPath);
            EpubBookComparer.CompareEpubBooks(testCase.ExpectedResult, epubBook);
        }

        private TestCase ReadTestCase(string testCasePath, string testEpubPath)
        {
            using TestCaseExtensionDataHandler testCaseExtensionDataHandler = new(testEpubPath);
            using StreamReader streamReader = new(testCasePath);
            TestCaseJsonSerializer testCaseJsonSerializer = new(testCaseExtensionDataHandler);
            TestCase testCase = testCaseJsonSerializer.Deserialize(streamReader);
            return testCase;
        }

        private void WriteTestCase(string testCasePath, string testEpubPath, EpubBook epubBook)
        {
            TestCase testCase = new()
            {                                                                                 
                Name = "Test case",
                ExpectedResult = epubBook
            };
            using TestCaseExtensionDataHandler testCaseExtensionDataHandler = new(testEpubPath);
            using StreamWriter streamWriter = new(testCasePath);
            TestCaseJsonSerializer testCaseJsonSerializer = new(testCaseExtensionDataHandler);
            testCaseJsonSerializer.Serialize(streamWriter, testCase);
        }

        public static IEnumerable<object[]> GetTestCaseDirectories()
        {
            List<string> directories = new();
            GetTestCaseDirectories(rootTestCasesDirectory, directories);
            return directories.Select(directory => new[] { directory });
        }

        private static void GetTestCaseDirectories(string currentDirectory, List<string> directories)
        {
            if (File.Exists(Path.Combine(currentDirectory, TEST_CASE_FILE_NAME)))
            {
                directories.Add(currentDirectory[(rootTestCasesDirectory.Length + 1)..]);
            }
            else
            {
                foreach (string subdirectory in Directory.GetDirectories(currentDirectory))
                {
                    GetTestCaseDirectories(subdirectory, directories);
                }
            }
        }

        private static string GetRootTestCasesDirectory([CallerFilePath] string callerFilePath = null)
        {
            string runnerDirectory = Path.GetDirectoryName(callerFilePath);
            string integrationDirectory = Directory.GetParent(runnerDirectory).FullName;
            return Path.Combine(integrationDirectory, TEST_CASES_DIRECTORY_NAME);
        }
    }
}
