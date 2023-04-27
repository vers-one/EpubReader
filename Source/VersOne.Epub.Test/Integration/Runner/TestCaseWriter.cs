using VersOne.Epub.Options;
using VersOne.Epub.Test.Integration.CustomSerialization;
using VersOne.Epub.Test.Integration.Types;

namespace VersOne.Epub.Test.Integration.Runner
{
    internal class TestCaseWriter
    {
        private delegate List<TestCase> TestCaseGenerator(string testEpubFilePath);

        private readonly TestCaseSerializer testCaseSerializer;
        private readonly string rootTestCasesDirectory;
        private readonly string testCasesFileName;
        private readonly string testEpubFileName;
        private readonly Dictionary<string, TestCaseGenerator> testCaseGenerators;

        public TestCaseWriter(TestCaseSerializer testCaseSerializer, string rootTestCasesDirectory, string testCasesFileName, string testEpubFileName)
        {
            this.testCaseSerializer = testCaseSerializer;
            this.rootTestCasesDirectory = rootTestCasesDirectory;
            this.testCasesFileName = testCasesFileName;
            this.testEpubFileName = testEpubFileName;
            testCaseGenerators = new()
            {
                { @"Typical\EPUB2", GenerateTypicalEpub2BookTestCases },
                { @"Typical\EPUB3", GenerateTypicalEpub3BookTestCases },
                { @"Bugfixes\53", GenerateIssue53TestCases },
                { @"Bugfixes\55", GenerateIssue55TestCases },
                { @"Bugfixes\57", GenerateIssue57TestCases },
                { @"Features\RemoteContent", GenerateRemoteContentTestCases },
                { @"Malformed\InvalidManifestItems\EPUB2", GenerateInvalidManifestItemsEpub2TestCases },
                { @"Malformed\InvalidManifestItems\EPUB3", GenerateInvalidManifestItemsEpub3TestCases },
                { @"Malformed\MissingContentForNavigationPoint", GenerateMissingContentForNavigationPointTestCases },
                { @"Malformed\MissingToc", GenerateMissingTocTestCases },
                { @"Workarounds\Xml11", GenerateXml11TestCases }
            };
        }

        public void WriteAllTestCases()
        {
            foreach (KeyValuePair<string, TestCaseGenerator> testCaseGenerator in testCaseGenerators)
            {
                string testCaseDirectoryPath = testCaseGenerator.Key;
                string testCaseDirectoryAbsolutePath = Path.Combine(rootTestCasesDirectory, testCaseDirectoryPath);
                string testCaseFilePath = Path.Combine(testCaseDirectoryAbsolutePath, testCasesFileName);
                string testEpubFilePath = Path.Combine(testCaseDirectoryAbsolutePath, testEpubFileName);
                List<TestCase> testCases = testCaseGenerator.Value(testEpubFilePath);
                WriteTestCases(testCaseFilePath, testEpubFilePath, testCases);
            }
        }

        private List<TestCase> GenerateTypicalEpub2BookTestCases(string testEpubFilePath)
        {
            return new()
            {
                new TestCase
                (
                    name: "Typical EPUB 2 book",
                    expectedResult: EpubReader.ReadBook(testEpubFilePath)
                )
            };
        }

        private List<TestCase> GenerateTypicalEpub3BookTestCases(string testEpubFilePath)
        {
            return new()
            {
                new TestCase
                (
                    name: "Typical EPUB 3 book",
                    expectedResult: EpubReader.ReadBook(testEpubFilePath)
                )
            };
        }

        private List<TestCase> GenerateIssue53TestCases(string testEpubFilePath)
        {
            return new()
            {
                new TestCase
                (
                    name: "Issue #53: EPUB schema metadata/date/event and metadata/identifier/scheme attributes are not being parsed",
                    expectedResult: EpubReader.ReadBook(testEpubFilePath)
                )
            };
        }

        private List<TestCase> GenerateIssue55TestCases(string testEpubFilePath)
        {
            return new()
            {
                new TestCase
                (
                    name: "Issue #55: EPUB 2 NCX navigation list parsing issues",
                    expectedResult: EpubReader.ReadBook(testEpubFilePath)
                )
            };
        }

        private List<TestCase> GenerateIssue57TestCases(string testEpubFilePath)
        {
            return new()
            {
                new TestCase
                (
                    name: "Issue #57: Cover extracting issue for EPUB 2 books without a cover and a guide",
                    expectedResult: EpubReader.ReadBook(testEpubFilePath)
                )
            };
        }

        private List<TestCase> GenerateRemoteContentTestCases(string testEpubFilePath)
        {
            EpubReaderOptions optionsWithDownloadingRemoteContentEnabled = new()
            {
                ContentDownloaderOptions = new ContentDownloaderOptions()
                {
                    DownloadContent = true,
                    DownloaderUserAgent = "EpubReader Integration Test Runner (https://github.com/vers-one/EpubReader)"
                }
            };
            return new()
            {
                new TestCase
                (
                    name: "EPUB 3 book with remote content files - without options",
                    expectedResult: EpubReader.ReadBook(testEpubFilePath)
                ),
                new TestCase
                (
                    name: "EPUB 3 book with remote content files - with options",
                    options: optionsWithDownloadingRemoteContentEnabled,
                    expectedResult: EpubReader.ReadBook(testEpubFilePath, optionsWithDownloadingRemoteContentEnabled)
                )
            };
        }

        private List<TestCase> GenerateInvalidManifestItemsEpub2TestCases(string testEpubFilePath)
        {
            EpubReaderOptions optionsWithSkippingInvalidManifestItemsEnabled = new()
            {
                PackageReaderOptions = new PackageReaderOptions()
                {
                    SkipInvalidManifestItems = true
                }
            };
            return new()
            {
                new TestCase
                (
                    name: "EPUB 2 book with invalid manifest items - without options",
                    expectedException: new TestCaseException
                    (
                        type: "EpubPackageException",
                        message: "Incorrect EPUB manifest: item ID is missing."
                    )
                ),
                new TestCase
                (
                    name: "EPUB 2 book with invalid manifest items - with options",
                    options: optionsWithSkippingInvalidManifestItemsEnabled,
                    expectedResult: EpubReader.ReadBook(testEpubFilePath, optionsWithSkippingInvalidManifestItemsEnabled)
                )
            };
        }

        private List<TestCase> GenerateInvalidManifestItemsEpub3TestCases(string testEpubFilePath)
        {
            EpubReaderOptions optionsWithSkippingInvalidManifestItemsEnabled = new()
            {
                PackageReaderOptions = new PackageReaderOptions()
                {
                    SkipInvalidManifestItems = true
                }
            };
            return new()
            {
                new TestCase
                (
                    name: "EPUB 3 book with invalid manifest items - without options",
                    expectedException: new TestCaseException
                    (
                        type: "EpubPackageException",
                        message: "Incorrect EPUB manifest: item ID is missing."
                    )
                ),
                new TestCase
                (
                    name: "EPUB 3 book with invalid manifest items - with options",
                    options: optionsWithSkippingInvalidManifestItemsEnabled,
                    expectedResult: EpubReader.ReadBook(testEpubFilePath, optionsWithSkippingInvalidManifestItemsEnabled)
                )
            };
        }

        private List<TestCase> GenerateMissingContentForNavigationPointTestCases(string testEpubFilePath)
        {
            EpubReaderOptions optionsWithIgnoringMissingContentForNavigationPointsEnabled = new()
            {
                Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
                {
                    IgnoreMissingContentForNavigationPoints = true
                }
            };
            return new()
            {
                new TestCase
                (
                    name: "EPUB 2 book with missing 'content' attribute for EPUB 2 NCX navigation point - without options",
                    expectedException: new TestCaseException
                    (
                        type: "Epub2NcxException",
                        message: "EPUB parsing error: navigation point \"navpoint-2\" should contain content."
                    )
                ),
                new TestCase
                (
                    name: "EPUB 2 book with missing 'content' attribute for EPUB 2 NCX navigation point - with options",
                    options: optionsWithIgnoringMissingContentForNavigationPointsEnabled,
                    expectedResult: EpubReader.ReadBook(testEpubFilePath, optionsWithIgnoringMissingContentForNavigationPointsEnabled)
                )
            };
        }

        private List<TestCase> GenerateMissingTocTestCases(string testEpubFilePath)
        {
            EpubReaderOptions optionsWithIgnoringMissingTocEnabled = new()
            {
                PackageReaderOptions = new PackageReaderOptions()
                {
                    IgnoreMissingToc = true
                }
            };
            return new()
            {
                new TestCase
                (
                    name: "EPUB 2 book with missing TOC attribute - without options",
                    expectedException: new TestCaseException
                    (
                        type: "EpubPackageException",
                        message: "Incorrect EPUB spine: TOC is missing."
                    )
                ),
                new TestCase
                (
                    name: "EPUB 2 book with missing TOC attribute - with options",
                    options: optionsWithIgnoringMissingTocEnabled,
                    expectedResult: EpubReader.ReadBook(testEpubFilePath, optionsWithIgnoringMissingTocEnabled)
                )
            };
        }

        private List<TestCase> GenerateXml11TestCases(string testEpubFilePath)
        {
            EpubReaderOptions optionsWithSkippingXmlHeadersEnabled = new()
            {
                XmlReaderOptions = new XmlReaderOptions()
                {
                    SkipXmlHeaders = true
                }
            };
            return new()
            {
                new TestCase
                (
                    name: "EPUB 2 book with XML 1.1 files - without options",
                    expectedException: new TestCaseException
                    (
                        type: "XmlException"
                    )
                ),
                new TestCase
                (
                    name: "EPUB 2 book with XML 1.1 files - with options",
                    options: optionsWithSkippingXmlHeadersEnabled,
                    expectedResult: EpubReader.ReadBook(testEpubFilePath, optionsWithSkippingXmlHeadersEnabled)
                )
            };
        }

        private void WriteTestCases(string testCaseFilePath, string testEpubFilePath, List<TestCase> testCases)
        {
            testCaseSerializer.Serialize(testCaseFilePath, testEpubFilePath, testCases);
        }
    }
}
