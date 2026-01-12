using System.Collections.ObjectModel;
using VersOne.Epub.Options;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.TestData;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.Content.Collections
{
    public class EpubContentCollectionTests
    {
        private static ReadOnlyCollection<EpubLocalTextContentFile> Local
        {
            get
            {
                List<EpubLocalTextContentFile> list =
                [
                    TestEpubContent.Chapter1File,
                    TestEpubContent.Chapter2File
                ];
                return list.AsReadOnly();
            }
        }

        private static ReadOnlyCollection<EpubRemoteTextContentFile> Remote
        {
            get
            {
                List<EpubRemoteTextContentFile> list =
                [
                    TestEpubContent.RemoteHtmlContentFile,
                    TestEpubContent.RemoteCssContentFile
                ];
                return list.AsReadOnly();
            }
        }

        private static ReadOnlyCollection<EpubLocalTextContentFile> LocalWithDuplicateKeys
        {
            get
            {
                string duplicateKey = CHAPTER1_FILE_NAME;
                return new
                (
                    [
                        new
                        (
                            key: duplicateKey,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE,
                            filePath: CHAPTER1_FILE_PATH,
                            content: TestEpubFiles.CHAPTER1_FILE_CONTENT
                        ),
                        new
                        (
                            key: duplicateKey,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE,
                            filePath: CHAPTER2_FILE_PATH,
                            content: TestEpubFiles.CHAPTER2_FILE_CONTENT
                        )
                    ]
                );
            }
        }

        private static ReadOnlyCollection<EpubLocalTextContentFile> LocalWithDuplicateFilePaths
        {
            get
            {
                string duplicateFilePath = CHAPTER1_FILE_PATH;
                return new
                (
                    [
                        new
                        (
                            key: CHAPTER1_FILE_NAME,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE,
                            filePath: duplicateFilePath,
                            content: TestEpubFiles.CHAPTER1_FILE_CONTENT
                        ),
                        new
                        (
                            key: CHAPTER2_FILE_NAME,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE,
                            filePath: duplicateFilePath,
                            content: TestEpubFiles.CHAPTER2_FILE_CONTENT
                        )
                    ]
                );
            }
        }

        private static ReadOnlyCollection<EpubRemoteTextContentFile> RemoteWithDuplicateKeys
        {
            get
            {
                string duplicateKey = REMOTE_HTML_CONTENT_FILE_HREF;
                return new
                (
                    [
                        new
                        (
                            key: duplicateKey,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE,
                            content: TestEpubFiles.REMOTE_HTML_FILE_CONTENT
                        ),
                        new
                        (
                            key: duplicateKey,
                            contentType: CSS_CONTENT_TYPE,
                            contentMimeType: CSS_CONTENT_MIME_TYPE,
                            content: TestEpubFiles.REMOTE_CSS_FILE_CONTENT
                        )
                    ]
                );
            }
        }

        [Fact(DisplayName = "Constructing a EpubContentCollection instance with default parameters should succeed")]
        public void ConstructorWithNonDefaultParametersTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new();
            Assert.NotNull(epubContentCollection.Local);
            Assert.Empty(epubContentCollection.Local);
            Assert.NotNull(epubContentCollection.Remote);
            Assert.Empty(epubContentCollection.Remote);
        }

        [Fact(DisplayName = "Constructing a EpubContentCollection instance with null local parameter should succeed")]
        public void ConstructorWithNullLocalParameterTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(null, Remote);
            Assert.NotNull(epubContentCollection.Local);
            Assert.Empty(epubContentCollection.Local);
            EpubContentComparer.CompareEpubRemoteTextContentFileLists(Remote, epubContentCollection.Remote);
        }

        [Fact(DisplayName = "Constructing a EpubContentCollection instance with null remote parameter should succeed")]
        public void ConstructorWithNullRemoteParameterTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, null);
            EpubContentComparer.CompareEpubLocalTextContentFileLists(Local, epubContentCollection.Local);
            Assert.NotNull(epubContentCollection.Remote);
            Assert.Empty(epubContentCollection.Remote);
        }

        [Fact(DisplayName = "Constructing a EpubContentCollection instance with null contentReaderOptions parameter should succeed")]
        public void ConstructorWithNullContentReaderOptionsParameterTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, Remote, null);
            EpubContentComparer.CompareEpubLocalTextContentFileLists(Local, epubContentCollection.Local);
            EpubContentComparer.CompareEpubRemoteTextContentFileLists(Remote, epubContentCollection.Remote);
        }

        [Fact(DisplayName = "Constructing a EpubContentCollection instance with all non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, Remote, new ContentReaderOptions());
            EpubContentComparer.CompareEpubLocalTextContentFileLists(Local, epubContentCollection.Local);
            EpubContentComparer.CompareEpubRemoteTextContentFileLists(Remote, epubContentCollection.Remote);
        }

        [Fact(DisplayName = "Constructor should throw EpubPackageException if local parameter contains content files with duplicate keys and no ContentReaderOptions are provided")]
        public void ConstructorWithLocalDuplicateKeysAndDefaultOptionsTest()
        {
            Assert.Throws<EpubPackageException>(() =>
                new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>(LocalWithDuplicateKeys, Remote));
        }

        [Fact(DisplayName = "Constructor should succeed when local parameter contains content files with duplicate keys and SkipItemsWithDuplicateHrefs = true")]
        public void ConstructorWithLocalDuplicateKeysAndSkipItemsWithDuplicateHrefsTest()
        {
            ContentReaderOptions contentReaderOptions = new()
            {
                SkipItemsWithDuplicateHrefs = true
            };
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection =
                new(LocalWithDuplicateKeys, Remote, contentReaderOptions);
            Assert.Single(epubContentCollection.Local);
            EpubContentComparer.CompareEpubLocalTextContentFiles(LocalWithDuplicateKeys[0], epubContentCollection.Local[0]);
        }

        [Fact(DisplayName = "Constructor should throw EpubPackageException if local parameter contains content files with duplicate file paths and no ContentReaderOptions are provided")]
        public void ConstructorWithLocalDuplicateFilePathsAndDefaultOptionsTest()
        {
            Assert.Throws<EpubPackageException>(() =>
                new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>(LocalWithDuplicateFilePaths, Remote));
        }

        [Fact(DisplayName = "Constructor should succeed when local parameter contains content files with duplicate file paths and SkipItemsWithDuplicateFilePaths = true")]
        public void ConstructorWithLocalDuplicateFilePathsAndSkipItemsWithDuplicateFilePathsTest()
        {
            ContentReaderOptions contentReaderOptions = new()
            {
                SkipItemsWithDuplicateFilePaths = true
            };
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection =
                new(LocalWithDuplicateFilePaths, Remote, contentReaderOptions);
            Assert.Single(epubContentCollection.Local);
            EpubContentComparer.CompareEpubLocalTextContentFiles(LocalWithDuplicateFilePaths[0], epubContentCollection.Local[0]);
        }

        [Fact(DisplayName = "Constructor should throw EpubPackageException if remote parameter contains content files with duplicate URLs and no ContentReaderOptions are provided")]
        public void ConstructorWithRemoteDuplicateUrlsAndDefaultOptionsTest()
        {
            Assert.Throws<EpubPackageException>(() =>
                new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>(Local, RemoteWithDuplicateKeys));
        }

        [Fact(DisplayName = "Constructor should succeed when remote parameter contains content files with duplicate URLs and SkipItemsWithDuplicateUrls = true")]
        public void ConstructorWithRemoteDuplicateUrlsAndSkipItemsWithDuplicateUrlsTest()
        {
            ContentReaderOptions contentReaderOptions = new()
            {
                SkipItemsWithDuplicateUrls = true
            };
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection =
                new(Local, RemoteWithDuplicateKeys, contentReaderOptions);
            Assert.Single(epubContentCollection.Remote);
            EpubContentComparer.CompareEpubRemoteTextContentFiles(RemoteWithDuplicateKeys[0], epubContentCollection.Remote[0]);
        }

        [Fact(DisplayName = "ContainsLocalFileWithKey should return true if the local file with the given key exists and false otherwise")]
        public void ContainsLocalFileWithKeyWithNonNullKeyTest()
        {
            string existingKey = CHAPTER1_FILE_NAME;
            string nonExistingKey = CHAPTER2_FILE_NAME;
            ReadOnlyCollection<EpubLocalTextContentFile> local = new
            (
                [
                    new
                    (
                        key: existingKey,
                        contentType: HTML_CONTENT_TYPE,
                        contentMimeType: HTML_CONTENT_MIME_TYPE,
                        filePath: CHAPTER1_FILE_PATH,
                        content: TestEpubFiles.CHAPTER1_FILE_CONTENT
                    )
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(local, Remote);
            Assert.True(epubContentCollection.ContainsLocalFileWithKey(existingKey));
            Assert.False(epubContentCollection.ContainsLocalFileWithKey(nonExistingKey));
        }

        [Fact(DisplayName = "ContainsLocalFileWithKey should throw ArgumentNullException if key argument is null")]
        public void ContainsLocalFileWithKeyWithNullKeyTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollection.ContainsLocalFileWithKey(null!));
        }

        [Fact(DisplayName = "GetLocalFileByKey should return the local file with the given key if it exists")]
        public void GetLocalFileByKeyWithExistingKeyTest()
        {
            string existingKey = CHAPTER1_FILE_NAME;
            EpubLocalTextContentFile expectedFile = new
            (
                key: existingKey,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                filePath: CHAPTER1_FILE_PATH,
                content: TestEpubFiles.CHAPTER1_FILE_CONTENT
            );
            ReadOnlyCollection<EpubLocalTextContentFile> local = new
            (
                [
                    expectedFile
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(local, Remote);
            EpubLocalTextContentFile actualFile = epubContentCollection.GetLocalFileByKey(existingKey);
            EpubContentComparer.CompareEpubLocalTextContentFiles(expectedFile, actualFile);
        }

        [Fact(DisplayName = "GetLocalFileByKey should throw EpubContentCollectionException if the local file with the given key doesn't exist")]
        public void GetLocalFileByKeyWithNonExistingKeyTest()
        {
            string nonExistingKey = CHAPTER2_FILE_NAME;
            ReadOnlyCollection<EpubLocalTextContentFile> local = new
            (
                [
                    new
                    (
                        key: CHAPTER1_FILE_NAME,
                        contentType: HTML_CONTENT_TYPE,
                        contentMimeType: HTML_CONTENT_MIME_TYPE,
                        filePath: CHAPTER1_FILE_PATH,
                        content: TestEpubFiles.CHAPTER1_FILE_CONTENT
                    )
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(local, Remote);
            Assert.Throws<EpubContentCollectionException>(() => epubContentCollection.GetLocalFileByKey(nonExistingKey));
        }

        [Fact(DisplayName = "GetLocalFileByKey should throw ArgumentNullException if key argument is null")]
        public void GetLocalFileByKeyWithNullKeyTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollection.GetLocalFileByKey(null!));
        }

        [Fact(DisplayName = "TryGetLocalFileByKey should return true with the local file with the given key if it exists and false with null reference otherwise")]
        public void TryGetLocalFileByKeyWithNonNullKeyTest()
        {
            string existingKey = CHAPTER1_FILE_NAME;
            string nonExistingKey = CHAPTER2_FILE_NAME;
            EpubLocalTextContentFile expectedFile = new
            (
                key: existingKey,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                filePath: CHAPTER1_FILE_PATH,
                content: TestEpubFiles.CHAPTER1_FILE_CONTENT
            );
            ReadOnlyCollection<EpubLocalTextContentFile> local = new
            (
                [
                    expectedFile
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(local, Remote);
            Assert.True(epubContentCollection.TryGetLocalFileByKey(existingKey, out EpubLocalTextContentFile? actualFileForExistingKey));
            EpubContentComparer.CompareEpubLocalTextContentFiles(expectedFile, actualFileForExistingKey);
            Assert.False(epubContentCollection.TryGetLocalFileByKey(nonExistingKey, out EpubLocalTextContentFile? actualFileForNonExistingKey));
            Assert.Null(actualFileForNonExistingKey);
        }

        [Fact(DisplayName = "TryGetLocalFileByKey should throw ArgumentNullException if key argument is null")]
        public void TryGetLocalFileByKeyWithNullKeyTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollection.TryGetLocalFileByKey(null!, out _));
        }

        [Fact(DisplayName = "ContainsLocalFileWithFilePath should return true if the local file with the given file path exists and false otherwise")]
        public void ContainsLocalFileWithFilePathWithNonNullFilePathTest()
        {
            string existingFilePath = CHAPTER1_FILE_PATH;
            string nonExistingFilePath = CHAPTER2_FILE_PATH;
            ReadOnlyCollection<EpubLocalTextContentFile> local = new
            (
                [
                    new
                    (
                        key: CHAPTER1_FILE_NAME,
                        contentType: HTML_CONTENT_TYPE,
                        contentMimeType: HTML_CONTENT_MIME_TYPE,
                        filePath: existingFilePath,
                        content: TestEpubFiles.CHAPTER1_FILE_CONTENT
                    )
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(local, Remote);
            Assert.True(epubContentCollection.ContainsLocalFileWithFilePath(existingFilePath));
            Assert.False(epubContentCollection.ContainsLocalFileWithFilePath(nonExistingFilePath));
        }

        [Fact(DisplayName = "ContainsLocalFileWithFilePath should throw ArgumentNullException if filePath argument is null")]
        public void ContainsLocalFileWithFilePathWithNullFilePathTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollection.ContainsLocalFileWithFilePath(null!));
        }

        [Fact(DisplayName = "GetLocalFileByFilePath should return the local file with the given file path if it exists")]
        public void GetLocalFileByFilePathWithExistingFilePathTest()
        {
            string existingFilePath = CHAPTER1_FILE_PATH;
            EpubLocalTextContentFile expectedFile = new
            (
                key: CHAPTER1_FILE_NAME,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                filePath: existingFilePath,
                content: TestEpubFiles.CHAPTER1_FILE_CONTENT
            );
            ReadOnlyCollection<EpubLocalTextContentFile> local = new
            (
                [
                    expectedFile
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(local, Remote);
            EpubLocalTextContentFile actualFile = epubContentCollection.GetLocalFileByFilePath(existingFilePath);
            EpubContentComparer.CompareEpubLocalTextContentFiles(expectedFile, actualFile);
        }

        [Fact(DisplayName = "GetLocalFileByFilePath should throw EpubContentCollectionException if the local file with the given file path doesn't exist")]
        public void GetLocalFileByFilePathWithNonExistingFilePathTest()
        {
            string nonExistingFilePath = CHAPTER2_FILE_PATH;
            ReadOnlyCollection<EpubLocalTextContentFile> local = new
            (
                [
                    new
                    (
                        key: CHAPTER1_FILE_NAME,
                        contentType: HTML_CONTENT_TYPE,
                        contentMimeType: HTML_CONTENT_MIME_TYPE,
                        filePath: CHAPTER1_FILE_PATH,
                        content: TestEpubFiles.CHAPTER1_FILE_CONTENT
                    )
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(local, Remote);
            Assert.Throws<EpubContentCollectionException>(() => epubContentCollection.GetLocalFileByFilePath(nonExistingFilePath));
        }

        [Fact(DisplayName = "GetLocalFileByFilePath should throw ArgumentNullException if filePath argument is null")]
        public void GetLocalFileByFilePathWithNullFilePathTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollection.GetLocalFileByFilePath(null!));
        }

        [Fact(DisplayName = "TryGetLocalFileByFilePath should return true with the local file with the given file path if it exists and false with null reference otherwise")]
        public void TryGetLocalFileByFilePathWithNonNullFilePathTest()
        {
            string existingFilePath = CHAPTER1_FILE_PATH;
            string nonExistingFilePath = CHAPTER2_FILE_PATH;
            EpubLocalTextContentFile expectedFile = new
            (
                key: CHAPTER1_FILE_NAME,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                filePath: existingFilePath,
                content: TestEpubFiles.CHAPTER1_FILE_CONTENT
            );
            ReadOnlyCollection<EpubLocalTextContentFile> local = new
            (
                [
                    expectedFile
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(local, Remote);
            Assert.True(epubContentCollection.TryGetLocalFileByFilePath(existingFilePath, out EpubLocalTextContentFile? actualFileForExistingFilePath));
            EpubContentComparer.CompareEpubLocalTextContentFiles(expectedFile, actualFileForExistingFilePath);
            Assert.False(epubContentCollection.TryGetLocalFileByFilePath(nonExistingFilePath, out EpubLocalTextContentFile? actualFileForNonExistingFilePath));
            Assert.Null(actualFileForNonExistingFilePath);
        }

        [Fact(DisplayName = "TryGetLocalFileByFilePath should throw ArgumentNullException if filePath argument is null")]
        public void TryGetLocalFileByFilePathWithNullFilePathTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollection.TryGetLocalFileByFilePath(null!, out _));
        }

        [Fact(DisplayName = "ContainsRemoteFileWithUrl should return true if the remote file with the given URL exists and false otherwise")]
        public void ContainsRemoteFileWithUrlWithNonNullUrlTest()
        {
            string existingUrl = REMOTE_HTML_CONTENT_FILE_HREF;
            string nonExistingUrl = REMOTE_CSS_CONTENT_FILE_HREF;
            ReadOnlyCollection<EpubRemoteTextContentFile> remote = new
            (
                [
                    new
                    (
                        key: existingUrl,
                        contentType: HTML_CONTENT_TYPE,
                        contentMimeType: HTML_CONTENT_MIME_TYPE,
                        content: TestEpubFiles.REMOTE_HTML_FILE_CONTENT
                    )
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, remote);
            Assert.True(epubContentCollection.ContainsRemoteFileWithUrl(existingUrl));
            Assert.False(epubContentCollection.ContainsRemoteFileWithUrl(nonExistingUrl));
        }

        [Fact(DisplayName = "ContainsRemoteFileWithUrl should throw ArgumentNullException if url argument is null")]
        public void ContainsRemoteFileWithUrlWithNullUrlTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollection.ContainsRemoteFileWithUrl(null!));
        }

        [Fact(DisplayName = "GetRemoteFileByUrl should return the remote file with the given URL if it exists")]
        public void GetRemoteFileByUrlWithExistingUrlTest()
        {
            string existingUrl = REMOTE_HTML_CONTENT_FILE_HREF;
            EpubRemoteTextContentFile expectedFile = new
            (
                key: existingUrl,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                content: TestEpubFiles.REMOTE_HTML_FILE_CONTENT
            );
            ReadOnlyCollection<EpubRemoteTextContentFile> remote = new
            (
                [
                    expectedFile
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, remote);
            EpubRemoteTextContentFile actualFile = epubContentCollection.GetRemoteFileByUrl(existingUrl);
            EpubContentComparer.CompareEpubRemoteTextContentFiles(expectedFile, actualFile);
        }

        [Fact(DisplayName = "GetRemoteFileByUrl should throw EpubContentCollectionException if the remote file with the given URL doesn't exist")]
        public void GetRemoteFileByUrlWithNonExistingUrlTest()
        {
            string nonExistingUrl = REMOTE_CSS_CONTENT_FILE_HREF;
            ReadOnlyCollection<EpubRemoteTextContentFile> remote = new
            (
                [
                    new
                    (
                        key: REMOTE_HTML_CONTENT_FILE_HREF,
                        contentType: HTML_CONTENT_TYPE,
                        contentMimeType: HTML_CONTENT_MIME_TYPE,
                        content: TestEpubFiles.REMOTE_HTML_FILE_CONTENT
                    )
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, remote);
            Assert.Throws<EpubContentCollectionException>(() => epubContentCollection.GetRemoteFileByUrl(nonExistingUrl));
        }

        [Fact(DisplayName = "GetRemoteFileByUrl should throw ArgumentNullException if url argument is null")]
        public void GetRemoteFileByUrlWithNullUrlTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollection.GetRemoteFileByUrl(null!));
        }

        [Fact(DisplayName = "TryGetRemoteFileByUrl should return true with the remote file with the given URL if it exists and false with null reference otherwise")]
        public void TryGetRemoteFileByUrlWithNonNullUrlTest()
        {
            string existingUrl = REMOTE_HTML_CONTENT_FILE_HREF;
            string nonExistingUrl = REMOTE_CSS_CONTENT_FILE_HREF;
            EpubRemoteTextContentFile expectedFile = new
            (
                key: existingUrl,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                content: TestEpubFiles.REMOTE_HTML_FILE_CONTENT
            );
            ReadOnlyCollection<EpubRemoteTextContentFile> remote = new
            (
                [
                    expectedFile
                ]
            );
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, remote);
            Assert.True(epubContentCollection.TryGetRemoteFileByUrl(existingUrl, out EpubRemoteTextContentFile? actualFileForExistingUrl));
            EpubContentComparer.CompareEpubRemoteTextContentFiles(expectedFile, actualFileForExistingUrl);
            Assert.False(epubContentCollection.TryGetRemoteFileByUrl(nonExistingUrl, out EpubRemoteTextContentFile? actualFileForNonExistingUrl));
            Assert.Null(actualFileForNonExistingUrl);
        }

        [Fact(DisplayName = "TryGetRemoteFileByUrl should throw ArgumentNullException if url argument is null")]
        public void TryGetRemoteFileByUrlWithNullUrlTest()
        {
            EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile> epubContentCollection = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollection.TryGetRemoteFileByUrl(null!, out _));
        }
    }
}
