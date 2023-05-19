using System.Collections.ObjectModel;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;
using VersOne.Epub.Test.Unit.TestData;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.Content.Collections
{
    public class EpubContentCollectionRefTests
    {
        private static ReadOnlyCollection<EpubLocalTextContentFileRef> Local
        {
            get
            {
                List<EpubLocalTextContentFileRef> list = new()
                {
                    TestEpubContentRef.Chapter1FileRef,
                    TestEpubContentRef.Chapter2FileRef
                };
                return list.AsReadOnly();
            }
        }

        private static ReadOnlyCollection<EpubRemoteTextContentFileRef> Remote
        {
            get
            {
                List<EpubRemoteTextContentFileRef> list = new()
                {
                    TestEpubContentRef.RemoteHtmlContentFileRef,
                    TestEpubContentRef.RemoteCssContentFileRef
                };
                return list.AsReadOnly();
            }
        }

        private static IEpubContentLoader ContentLoader => new TestEpubContentLoader();

        [Fact(DisplayName = "Constructing a EpubContentCollectionRef instance with default parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new();
            Assert.NotNull(epubContentCollectionRef.Local);
            Assert.Empty(epubContentCollectionRef.Local);
            Assert.NotNull(epubContentCollectionRef.Remote);
            Assert.Empty(epubContentCollectionRef.Remote);
        }

        [Fact(DisplayName = "Constructing a EpubContentCollectionRef instance with null local parameter should succeed")]
        public void ConstructorWithNullLocalParameterTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(null, Remote);
            Assert.NotNull(epubContentCollectionRef.Local);
            Assert.Empty(epubContentCollectionRef.Local);
            CompareRemoteTextRefReadOnlyCollections(Remote, epubContentCollectionRef.Remote);
        }

        [Fact(DisplayName = "Constructing a EpubContentCollectionRef instance with null remote parameter should succeed")]
        public void ConstructorWithNullRemoteParameterTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, null);
            CompareLocalTextRefReadOnlyCollections(Local, epubContentCollectionRef.Local);
            Assert.NotNull(epubContentCollectionRef.Remote);
            Assert.Empty(epubContentCollectionRef.Remote);
        }

        [Fact(DisplayName = "Constructor should throw EpubPackageException if local parameter contains content files with duplicate keys")]
        public void ConstructorWithLocalDuplicateKeysTest()
        {
            string duplicateKey = CHAPTER1_FILE_NAME;
            ReadOnlyCollection<EpubLocalTextContentFileRef> localWithDuplicateKeys = new
            (
                new List<EpubLocalTextContentFileRef>()
                {
                    new EpubLocalTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: duplicateKey,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        filePath: CHAPTER1_FILE_PATH,
                        epubContentLoader: ContentLoader
                    ),
                    new EpubLocalTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: duplicateKey,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        filePath: CHAPTER2_FILE_PATH,
                        epubContentLoader: ContentLoader
                    )
                }
            );
            Assert.Throws<EpubPackageException>(() => new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(localWithDuplicateKeys, Remote));
        }

        [Fact(DisplayName = "Constructor should throw EpubPackageException if local parameter contains content files with duplicate file paths")]
        public void ConstructorWithLocalDuplicateFilePathsTest()
        {
            string duplicateFilePath = CHAPTER1_FILE_PATH;
            ReadOnlyCollection<EpubLocalTextContentFileRef> localWithDuplicateFilePaths = new
            (
                new List<EpubLocalTextContentFileRef>()
                {
                    new EpubLocalTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: CHAPTER1_FILE_NAME,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        filePath: duplicateFilePath,
                        epubContentLoader: ContentLoader
                    ),
                    new EpubLocalTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: CHAPTER2_FILE_NAME,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        filePath: duplicateFilePath,
                        epubContentLoader: ContentLoader
                    )
                }
            );
            Assert.Throws<EpubPackageException>(() => new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(localWithDuplicateFilePaths, Remote));
        }

        [Fact(DisplayName = "Constructor should throw EpubPackageException if remote parameter contains content files with duplicate URLs")]
        public void ConstructorWithRemoteDuplicateUrlsTest()
        {
            string duplicateKey = REMOTE_HTML_CONTENT_FILE_HREF;
            ReadOnlyCollection<EpubRemoteTextContentFileRef> remoteWithDuplicateKeys = new
            (
                new List<EpubRemoteTextContentFileRef>()
                {
                    new EpubRemoteTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: duplicateKey,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        epubContentLoader: ContentLoader
                    ),
                    new EpubRemoteTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: duplicateKey,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        epubContentLoader: ContentLoader
                    )
                }
            );
            Assert.Throws<EpubPackageException>(() => new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(Local, remoteWithDuplicateKeys));
        }

        [Fact(DisplayName = "ContainsLocalFileRefWithKey should return true if the local file reference with the given key exists and false otherwise")]
        public void ContainsLocalFileWithKeyWithNonNullKeyTest()
        {
            string existingKey = CHAPTER1_FILE_NAME;
            string nonExistingKey = CHAPTER2_FILE_NAME;
            ReadOnlyCollection<EpubLocalTextContentFileRef> local = new
            (
                new List<EpubLocalTextContentFileRef>()
                {
                    new EpubLocalTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: existingKey,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        filePath: CHAPTER1_FILE_PATH,
                        epubContentLoader: ContentLoader
                    )
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(local, Remote);
            Assert.True(epubContentCollectionRef.ContainsLocalFileRefWithKey(existingKey));
            Assert.False(epubContentCollectionRef.ContainsLocalFileRefWithKey(nonExistingKey));
        }

        [Fact(DisplayName = "ContainsLocalFileRefWithKey should throw ArgumentNullException if key argument is null")]
        public void ContainsLocalFileWithKeyWithNullKeyTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollectionRef.ContainsLocalFileRefWithKey(null!));
        }

        [Fact(DisplayName = "GetLocalFileRefByKey should return the local file reference with the given key if it exists")]
        public void GetLocalFileByKeyWithExistingKeyTest()
        {
            string existingKey = CHAPTER1_FILE_NAME;
            EpubLocalTextContentFileRef expectedFileRef = new
            (
                metadata: new EpubContentFileRefMetadata
                (
                    key: existingKey,
                    contentType: HTML_CONTENT_TYPE,
                    contentMimeType: HTML_CONTENT_MIME_TYPE
                ),
                filePath: CHAPTER1_FILE_PATH,
                epubContentLoader: ContentLoader
            );
            ReadOnlyCollection<EpubLocalTextContentFileRef> local = new
            (
                new List<EpubLocalTextContentFileRef>()
                {
                    expectedFileRef
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(local, Remote);
            EpubLocalTextContentFileRef actualFileRef = epubContentCollectionRef.GetLocalFileRefByKey(existingKey);
            EpubContentRefComparer.CompareEpubLocalContentFileRefs(expectedFileRef, actualFileRef);
        }

        [Fact(DisplayName = "GetLocalFileRefByKey should throw EpubContentCollectionRefException if the local file reference with the given key doesn't exist")]
        public void GetLocalFileByKeyWithNonExistingKeyTest()
        {
            string nonExistingKey = CHAPTER2_FILE_NAME;
            ReadOnlyCollection<EpubLocalTextContentFileRef> local = new
            (
                new List<EpubLocalTextContentFileRef>()
                {
                    new EpubLocalTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: CHAPTER1_FILE_NAME,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        filePath: CHAPTER1_FILE_PATH,
                        epubContentLoader: ContentLoader
                    )
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(local, Remote);
            Assert.Throws<EpubContentCollectionRefException>(() => epubContentCollectionRef.GetLocalFileRefByKey(nonExistingKey));
        }

        [Fact(DisplayName = "GetLocalFileRefByKey should throw ArgumentNullException if key argument is null")]
        public void GetLocalFileByKeyWithNullKeyTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollectionRef.GetLocalFileRefByKey(null!));
        }

        [Fact(DisplayName = "TryGetLocalFileRefByKey should return true with the local file reference with the given key if it exists and false with null reference otherwise")]
        public void TryGetLocalFileByKeyWithNonNullKeyTest()
        {
            string existingKey = CHAPTER1_FILE_NAME;
            string nonExistingKey = CHAPTER2_FILE_NAME;
            EpubLocalTextContentFileRef expectedFileRef = new
            (
                metadata: new EpubContentFileRefMetadata
                (
                    key: existingKey,
                    contentType: HTML_CONTENT_TYPE,
                    contentMimeType: HTML_CONTENT_MIME_TYPE
                ),
                filePath: CHAPTER1_FILE_PATH,
                epubContentLoader: ContentLoader
            );
            ReadOnlyCollection<EpubLocalTextContentFileRef> local = new
            (
                new List<EpubLocalTextContentFileRef>()
                {
                    expectedFileRef
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(local, Remote);
            Assert.True(epubContentCollectionRef.TryGetLocalFileRefByKey(existingKey, out EpubLocalTextContentFileRef actualFileRefForExistingKey));
            EpubContentRefComparer.CompareEpubLocalContentFileRefs(expectedFileRef, actualFileRefForExistingKey);
            Assert.False(epubContentCollectionRef.TryGetLocalFileRefByKey(nonExistingKey, out EpubLocalTextContentFileRef actualFileRefForNonExistingKey));
            Assert.Null(actualFileRefForNonExistingKey);
        }

        [Fact(DisplayName = "TryGetLocalFileRefByKey should throw ArgumentNullException if key argument is null")]
        public void TryGetLocalFileByKeyWithNullKeyTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollectionRef.TryGetLocalFileRefByKey(null!, out _));
        }

        [Fact(DisplayName = "ContainsLocalFileRefWithFilePath should return true if the local file reference with the given file path exists and false otherwise")]
        public void ContainsLocalFileWithFilePathWithNonNullFilePathTest()
        {
            string existingFilePath = CHAPTER1_FILE_PATH;
            string nonExistingFilePath = CHAPTER2_FILE_PATH;
            ReadOnlyCollection<EpubLocalTextContentFileRef> local = new
            (
                new List<EpubLocalTextContentFileRef>()
                {
                    new EpubLocalTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: CHAPTER1_FILE_NAME,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        filePath: existingFilePath,
                        epubContentLoader: ContentLoader
                    )
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(local, Remote);
            Assert.True(epubContentCollectionRef.ContainsLocalFileRefWithFilePath(existingFilePath));
            Assert.False(epubContentCollectionRef.ContainsLocalFileRefWithFilePath(nonExistingFilePath));
        }

        [Fact(DisplayName = "ContainsLocalFileRefWithFilePath should throw ArgumentNullException if filePath argument is null")]
        public void ContainsLocalFileWithFilePathWithNullFilePathTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollectionRef.ContainsLocalFileRefWithFilePath(null!));
        }

        [Fact(DisplayName = "GetLocalFileRefByFilePath should return the local file reference with the given file path if it exists")]
        public void GetLocalFileByFilePathWithExistingFilePathTest()
        {
            string existingFilePath = CHAPTER1_FILE_PATH;
            EpubLocalTextContentFileRef expectedFileRef = new
            (
                metadata: new EpubContentFileRefMetadata
                (
                    key: CHAPTER1_FILE_NAME,
                    contentType: HTML_CONTENT_TYPE,
                    contentMimeType: HTML_CONTENT_MIME_TYPE
                ),
                filePath: existingFilePath,
                epubContentLoader: ContentLoader
            );
            ReadOnlyCollection<EpubLocalTextContentFileRef> local = new
            (
                new List<EpubLocalTextContentFileRef>()
                {
                    expectedFileRef
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(local, Remote);
            EpubLocalTextContentFileRef actualFileRef = epubContentCollectionRef.GetLocalFileRefByFilePath(existingFilePath);
            EpubContentRefComparer.CompareEpubLocalContentFileRefs(expectedFileRef, actualFileRef);
        }

        [Fact(DisplayName = "GetLocalFileRefByFilePath should throw EpubContentCollectionRefException if the local file reference with the given file path doesn't exist")]
        public void GetLocalFileByFilePathWithNonExistingFilePathTest()
        {
            string nonExistingFilePath = CHAPTER2_FILE_PATH;
            ReadOnlyCollection<EpubLocalTextContentFileRef> local = new
            (
                new List<EpubLocalTextContentFileRef>()
                {
                    new EpubLocalTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: CHAPTER1_FILE_NAME,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        filePath: CHAPTER1_FILE_PATH,
                        epubContentLoader: ContentLoader
                    )
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(local, Remote);
            Assert.Throws<EpubContentCollectionRefException>(() => epubContentCollectionRef.GetLocalFileRefByFilePath(nonExistingFilePath));
        }

        [Fact(DisplayName = "GetLocalFileRefByFilePath should throw ArgumentNullException if filePath argument is null")]
        public void GetLocalFileByFilePathWithNullFilePathTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollectionRef.GetLocalFileRefByFilePath(null!));
        }

        [Fact(DisplayName = "TryGetLocalFileRefByFilePath should return true with the local file reference with the given file path if it exists and false with null reference otherwise")]
        public void TryGetLocalFileByFilePathWithNonNullFilePathTest()
        {
            string existingFilePath = CHAPTER1_FILE_PATH;
            string nonExistingFilePath = CHAPTER2_FILE_PATH;
            EpubLocalTextContentFileRef expectedFileRef = new
            (
                metadata: new EpubContentFileRefMetadata
                (
                    key: CHAPTER1_FILE_NAME,
                    contentType: HTML_CONTENT_TYPE,
                    contentMimeType: HTML_CONTENT_MIME_TYPE
                ),
                filePath: existingFilePath,
                epubContentLoader: ContentLoader
            );
            ReadOnlyCollection<EpubLocalTextContentFileRef> local = new
            (
                new List<EpubLocalTextContentFileRef>()
                {
                    expectedFileRef
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(local, Remote);
            Assert.True(epubContentCollectionRef.TryGetLocalFileRefByFilePath(existingFilePath, out EpubLocalTextContentFileRef actualFileRefForExistingFilePath));
            EpubContentRefComparer.CompareEpubLocalContentFileRefs(expectedFileRef, actualFileRefForExistingFilePath);
            Assert.False(epubContentCollectionRef.TryGetLocalFileRefByFilePath(nonExistingFilePath, out EpubLocalTextContentFileRef actualFileRefForNonExistingFilePath));
            Assert.Null(actualFileRefForNonExistingFilePath);
        }

        [Fact(DisplayName = "TryGetLocalFileRefByFilePath should throw ArgumentNullException if filePath argument is null")]
        public void TryGetLocalFileByFilePathWithNullFilePathTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollectionRef.TryGetLocalFileRefByFilePath(null!, out _));
        }

        [Fact(DisplayName = "ContainsRemoteFileRefWithUrl should return true if the remote file reference with the given URL exists and false otherwise")]
        public void ContainsRemoteFileWithUrlWithNonNullUrlTest()
        {
            string existingUrl = REMOTE_HTML_CONTENT_FILE_HREF;
            string nonExistingUrl = REMOTE_CSS_CONTENT_FILE_HREF;
            ReadOnlyCollection<EpubRemoteTextContentFileRef> remote = new
            (
                new List<EpubRemoteTextContentFileRef>()
                {
                    new EpubRemoteTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: existingUrl,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        epubContentLoader: ContentLoader
                    )
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, remote);
            Assert.True(epubContentCollectionRef.ContainsRemoteFileRefWithUrl(existingUrl));
            Assert.False(epubContentCollectionRef.ContainsRemoteFileRefWithUrl(nonExistingUrl));
        }

        [Fact(DisplayName = "ContainsRemoteFileRefWithUrl should throw ArgumentNullException if url argument is null")]
        public void ContainsRemoteFileWithUrlWithNullUrlTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollectionRef.ContainsRemoteFileRefWithUrl(null!));
        }

        [Fact(DisplayName = "GetRemoteFileRefByUrl should return the remote file reference with the given URL if it exists")]
        public void GetRemoteFileByUrlWithExistingUrlTest()
        {
            string existingUrl = REMOTE_HTML_CONTENT_FILE_HREF;
            EpubRemoteTextContentFileRef expectedFileRef = new
            (
                metadata: new EpubContentFileRefMetadata
                (
                    key: existingUrl,
                    contentType: HTML_CONTENT_TYPE,
                    contentMimeType: HTML_CONTENT_MIME_TYPE
                ),
                epubContentLoader: ContentLoader
            );
            ReadOnlyCollection<EpubRemoteTextContentFileRef> remote = new
            (
                new List<EpubRemoteTextContentFileRef>()
                {
                    expectedFileRef
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, remote);
            EpubRemoteTextContentFileRef actualFileRef = epubContentCollectionRef.GetRemoteFileRefByUrl(existingUrl);
            EpubContentRefComparer.CompareEpubRemoteContentFileRefs(expectedFileRef, actualFileRef);
        }

        [Fact(DisplayName = "GetRemoteFileRefByUrl should throw EpubContentCollectionRefException if the remote file reference with the given URL doesn't exist")]
        public void GetRemoteFileByUrlWithNonExistingUrlTest()
        {
            string nonExistingUrl = REMOTE_CSS_CONTENT_FILE_HREF;
            ReadOnlyCollection<EpubRemoteTextContentFileRef> remote = new
            (
                new List<EpubRemoteTextContentFileRef>()
                {
                    new EpubRemoteTextContentFileRef
                    (
                        metadata: new EpubContentFileRefMetadata
                        (
                            key: REMOTE_HTML_CONTENT_FILE_HREF,
                            contentType: HTML_CONTENT_TYPE,
                            contentMimeType: HTML_CONTENT_MIME_TYPE
                        ),
                        epubContentLoader: ContentLoader
                    )
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, remote);
            Assert.Throws<EpubContentCollectionRefException>(() => epubContentCollectionRef.GetRemoteFileRefByUrl(nonExistingUrl));
        }

        [Fact(DisplayName = "GetRemoteFileRefByUrl should throw ArgumentNullException if url argument is null")]
        public void GetRemoteFileByUrlWithNullUrlTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollectionRef.GetRemoteFileRefByUrl(null!));
        }

        [Fact(DisplayName = "TryGetRemoteFileRefByUrl should return true with the remote file reference with the given URL if it exists and false with null reference otherwise")]
        public void TryGetRemoteFileByUrlWithNonNullUrlTest()
        {
            string existingUrl = REMOTE_HTML_CONTENT_FILE_HREF;
            string nonExistingUrl = REMOTE_CSS_CONTENT_FILE_HREF;
            EpubRemoteTextContentFileRef expectedFileRef = new
            (
                metadata: new EpubContentFileRefMetadata
                (
                    key: existingUrl,
                    contentType: HTML_CONTENT_TYPE,
                    contentMimeType: HTML_CONTENT_MIME_TYPE
                ),
                epubContentLoader: ContentLoader
            );
            ReadOnlyCollection<EpubRemoteTextContentFileRef> remote = new
            (
                new List<EpubRemoteTextContentFileRef>()
                {
                    expectedFileRef
                }
            );
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, remote);
            Assert.True(epubContentCollectionRef.TryGetRemoteFileRefByUrl(existingUrl, out EpubRemoteTextContentFileRef actualFileRefForExistingUrl));
            EpubContentRefComparer.CompareEpubRemoteContentFileRefs(expectedFileRef, actualFileRefForExistingUrl);
            Assert.False(epubContentCollectionRef.TryGetRemoteFileRefByUrl(nonExistingUrl, out EpubRemoteTextContentFileRef actualFileRefForNonExistingUrl));
            Assert.Null(actualFileRefForNonExistingUrl);
        }

        [Fact(DisplayName = "TryGetRemoteFileRefByUrl should throw ArgumentNullException if url argument is null")]
        public void TryGetRemoteFileByUrlWithNullUrlTest()
        {
            EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef> epubContentCollectionRef = new(Local, Remote);
            Assert.Throws<ArgumentNullException>(() => epubContentCollectionRef.TryGetRemoteFileRefByUrl(null!, out _));
        }

        private static void CompareLocalTextRefReadOnlyCollections(ReadOnlyCollection<EpubLocalTextContentFileRef> expected, ReadOnlyCollection<EpubLocalTextContentFileRef> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, EpubContentRefComparer.CompareEpubLocalContentFileRefs);
        }

        private static void CompareRemoteTextRefReadOnlyCollections(ReadOnlyCollection<EpubRemoteTextContentFileRef> expected, ReadOnlyCollection<EpubRemoteTextContentFileRef> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, EpubContentRefComparer.CompareEpubRemoteContentFileRefs);
        }
    }
}
