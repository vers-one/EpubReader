﻿using System.Runtime.CompilerServices;
using VersOne.Epub.Options;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Integration.CustomTypeHandlers;
using VersOne.Epub.Test.Integration.JsonUtils;

namespace VersOne.Epub.Test.Integration.Runner
{
    public class TestRunner
    {
        private const string TEST_CASES_DIRECTORY_NAME = "TestCases";
        private const string TEST_CASES_FILE_NAME = "testcases.json";
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
            string testCasesPath = Path.Combine(testCaseDirectoryAbsolutePath, TEST_CASES_FILE_NAME);
            string testEpubPath = Path.Combine(testCaseDirectoryAbsolutePath, TEST_EPUB_FILE_NAME);
            List<TestCase>? testCases = ReadTestCases(testCasesPath, testEpubPath);
            Assert.NotNull(testCases);
            foreach (TestCase testCase in testCases)
            {
                if (testCase.ExpectedResult != null)
                {
                    EpubBook epubBook = EpubReader.ReadBook(testEpubPath, testCase.Options);
                    EpubBookComparer.CompareEpubBooks(testCase.ExpectedResult, epubBook);
                }
                else if (testCase.ExpectedException != null)
                {
                    bool exceptionThrown = false;
                    try
                    {
                        EpubReader.ReadBook(testEpubPath, testCase.Options);
                    }
                    catch (Exception actualException)
                    {
                        exceptionThrown = true;
                        Assert.Equal(actualException.GetType().Name, testCase.ExpectedException.Type);
                        if (testCase.ExpectedException.Message != null)
                        {
                            Assert.Equal(actualException.Message, testCase.ExpectedException.Message);
                        }
                    }
                    Assert.True(exceptionThrown, $"Expected exception was not thrown for the test case: {testCase.Name}");
                }
            }
        }

        // [Fact]
        public void GenerateTestCaseTemplate()
        {
            string testCaseDirectoryPath = "Features\\RemoteContent";
            string testCaseDirectoryAbsolutePath = Path.Combine(rootTestCasesDirectory, testCaseDirectoryPath);
            string testCasesPath = Path.Combine(testCaseDirectoryAbsolutePath, TEST_CASES_FILE_NAME);
            string testEpubPath = Path.Combine(testCaseDirectoryAbsolutePath, TEST_EPUB_FILE_NAME);
            EpubReaderOptions epubReaderOptions = new()
            {
                ContentDownloaderOptions = new ContentDownloaderOptions
                {
                    DownloadContent = true,
                    DownloaderUserAgent = "EpubReader Integration Test Runner (https://github.com/vers-one/EpubReader)"
                }
            };
            EpubBook epubBook = EpubReader.ReadBook(testEpubPath, epubReaderOptions);
            WriteTestCase(testCasesPath, testEpubPath, epubBook);
        }

        private static List<TestCase>? ReadTestCases(string testCasePath, string testEpubPath)
        {
            using CustomPropertyDependencies customPropertyDependencies = new(testEpubPath);
            using StreamReader streamReader = new(testCasePath);
            TestCaseJsonSerializer testCaseJsonSerializer = new(customPropertyDependencies);
            List<TestCase>? testCases = testCaseJsonSerializer.Deserialize(streamReader);
            return testCases;
        }

        private static void WriteTestCase(string testCasePath, string testEpubPath, EpubBook epubBook)
        {
            TestCase testCase = new
            (
                name: "Test case",
                expectedResult: epubBook
            );
            List<TestCase> testCases = new() { testCase };
            using CustomPropertyDependencies customPropertyDependencies = new(testEpubPath);
            using StreamWriter streamWriter = new(testCasePath);
            TestCaseJsonSerializer testCaseJsonSerializer = new(customPropertyDependencies);
            testCaseJsonSerializer.Serialize(streamWriter, testCases);
        }

        public static IEnumerable<object[]> GetTestCaseDirectories()
        {
            List<string> directories = new();
            GetTestCaseDirectories(rootTestCasesDirectory, directories);
            return directories.Select(directory => new[] { directory });
        }

        private static void GetTestCaseDirectories(string currentDirectory, List<string> directories)
        {
            if (File.Exists(Path.Combine(currentDirectory, TEST_CASES_FILE_NAME)))
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

        private static string GetRootTestCasesDirectory([CallerFilePath] string? callerFilePath = null)
        {
            string? runnerDirectory = Path.GetDirectoryName(callerFilePath);
            Assert.NotNull(runnerDirectory);
            DirectoryInfo? runnerDirectoryInfo = Directory.GetParent(runnerDirectory);
            Assert.NotNull(runnerDirectoryInfo);
            string integrationDirectory = runnerDirectoryInfo.FullName;
            return Path.Combine(integrationDirectory, TEST_CASES_DIRECTORY_NAME);
        }
    }
}
