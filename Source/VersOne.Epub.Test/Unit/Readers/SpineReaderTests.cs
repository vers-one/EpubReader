using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class SpineReaderTests
    {
        private static EpubSchema MinimalEpubSchema =>
            CreateEpubSchema
            (
                manifest: new
                (
                    items:
                    [
                        new
                        (
                            id: "item-1",
                            href: "chapter1.html",
                            mediaType: "application/xhtml+xml"
                        )
                    ]
                ),
                spine: new
                (
                    items:
                    [
                        new
                        (
                            idRef: "item-1"
                        )
                    ]
                )
            );

        private static EpubSchema EpubSchemaWithMissingManifestItem =>
            CreateEpubSchema
            (
                manifest: new
                (
                    items:
                    [
                        new
                        (
                            id: "item-2",
                            href: "chapter2.html",
                            mediaType: "application/xhtml+xml"
                        )
                    ]
                ),
                spine: new
                (
                    items:
                    [
                        new
                        (
                            idRef: "item-1"
                        )
                    ]
                )
            );

        private static EpubSchema EpubSchemaWithRemoteFile =>
            CreateEpubSchema
            (
                manifest: new
                (
                    items:
                    [
                        new
                        (
                            id: "item-1",
                            href: "https://example.com/books/123/chapter1.html",
                            mediaType: "application/xhtml+xml"
                        )
                    ]
                ),
                spine: new
                (
                    items:
                    [
                        new
                        (
                            idRef: "item-1"
                        )
                    ]
                )
            );

        [Fact(DisplayName = "Getting reading order for a minimal EPUB spine should succeed")]
        public void GetReadingOrderForMinimalSpineTest()
        {
            EpubSchema epubSchema = CreateEpubSchema();
            EpubContentRef epubContentRef = new();
            List<EpubLocalTextContentFileRef> expectedReadingOrder = [];
            List<EpubLocalTextContentFileRef> actualReadingOrder = SpineReader.GetReadingOrder(epubSchema, epubContentRef, new SpineReaderOptions());
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        [Fact(DisplayName = "Getting reading order for a typical EPUB spine should succeed")]
        public void GetReadingOrderForTypicalSpineTest()
        {
            EpubSchema epubSchema = CreateEpubSchema
            (
                manifest: new
                (
                    items:
                    [
                        new
                        (
                            id: "item-1",
                            href: "chapter1.html",
                            mediaType: "application/xhtml+xml"
                        ),
                        new
                        (
                            id: "item-2",
                            href: "chapter2.html",
                            mediaType: "application/xhtml+xml"
                        )
                    ]
                ),
                spine: new EpubSpine
                (
                    items:
                    [
                        new
                        (
                            idRef: "item-1"
                        ),
                        new
                        (
                            idRef: "item-2"
                        )
                    ]
                )
            );
            EpubLocalTextContentFileRef expectedHtmlFileRef1 = CreateTestHtmlFileRef("chapter1.html");
            EpubLocalTextContentFileRef expectedHtmlFileRef2 = CreateTestHtmlFileRef("chapter2.html");
            List<EpubLocalTextContentFileRef> expectedHtmlLocal =
            [
                expectedHtmlFileRef1,
                expectedHtmlFileRef2
            ];
            EpubContentRef epubContentRef = new
            (
                html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(expectedHtmlLocal.AsReadOnly())
            );
            List<EpubLocalTextContentFileRef> expectedReadingOrder =
            [
                expectedHtmlFileRef1,
                expectedHtmlFileRef2
            ];
            List<EpubLocalTextContentFileRef> actualReadingOrder = SpineReader.GetReadingOrder(epubSchema, epubContentRef, new SpineReaderOptions());
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        [Fact(DisplayName = "GetReadingOrder should throw EpubPackageException if there is no manifest item with ID matching to the ID ref of a spine item and no SpineReaderOptions are provided")]
        public void GetReadingOrderWithMissingManifestItemAndDefaultOptionsTest()
        {
            EpubSchema epubSchema = EpubSchemaWithMissingManifestItem;
            EpubContentRef epubContentRef = new();
            Assert.Throws<EpubPackageException>(() => SpineReader.GetReadingOrder(epubSchema, epubContentRef, new SpineReaderOptions()));
        }

        [Fact(DisplayName = "GetReadingOrder should skip non-existent manifest items when IgnoreMissingManifestItems = true")]
        public void GetReadingOrderWithMissingManifestItemAndIgnoreMissingManifestItemsTest()
        {
            EpubSchema epubSchema = EpubSchemaWithMissingManifestItem;
            EpubContentRef epubContentRef = new();
            SpineReaderOptions spineReaderOptions = new()
            {
                IgnoreMissingManifestItems = true
            };
            List<EpubLocalTextContentFileRef> expectedReadingOrder = [];
            List<EpubLocalTextContentFileRef> actualReadingOrder = SpineReader.GetReadingOrder(epubSchema, epubContentRef, spineReaderOptions);
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        [Fact(DisplayName = "GetReadingOrder should throw EpubPackageException if the HTML content file referenced by a spine item is a remote resource and no SpineReaderOptions are provided")]
        public void GetReadingOrderWithRemoteHtmlContentFileAndDefaultOptionsTest()
        {
            EpubSchema epubSchema = EpubSchemaWithRemoteFile;
            EpubContentRef epubContentRef = CreateEpubContentRefForTestRemoteFile(epubSchema.Package.Manifest.Items[0].Href);
            Assert.Throws<EpubPackageException>(() => SpineReader.GetReadingOrder(epubSchema, epubContentRef, new SpineReaderOptions()));
        }

        [Fact(DisplayName = "GetReadingOrder should skip spine items referencing remote HTML content files when SkipSpineItemsReferencingRemoteContent = true")]
        public void GetReadingOrderWithRemoteHtmlContentFileAndSkipSpineItemsReferencingRemoteContentTest()
        {
            EpubSchema epubSchema = EpubSchemaWithRemoteFile;
            EpubContentRef epubContentRef = CreateEpubContentRefForTestRemoteFile(epubSchema.Package.Manifest.Items[0].Href);
            SpineReaderOptions spineReaderOptions = new()
            {
                SkipSpineItemsReferencingRemoteContent = true
            };
            List<EpubLocalTextContentFileRef> expectedReadingOrder = [];
            List<EpubLocalTextContentFileRef> actualReadingOrder = SpineReader.GetReadingOrder(epubSchema, epubContentRef, spineReaderOptions);
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        [Fact(DisplayName = "GetReadingOrder should throw EpubPackageException if there is no HTML content file referenced by a manifest item and no SpineReaderOptions are provided")]
        public void GetReadingOrderWithMissingHtmlContentFileAndDefaultOptionsTest()
        {
            EpubSchema epubSchema = MinimalEpubSchema;
            EpubContentRef epubContentRef = new();
            Assert.Throws<EpubPackageException>(() => SpineReader.GetReadingOrder(epubSchema, epubContentRef, new SpineReaderOptions()));
        }

        [Fact(DisplayName = "GetReadingOrder should skip spine items referencing missing HTML content files when IgnoreMissingContentFiles = true")]
        public void GetReadingOrderWithMissingHtmlContentFileAndIgnoreMissingContentFilesTest()
        {
            EpubSchema epubSchema = MinimalEpubSchema;
            EpubContentRef epubContentRef = new();
            SpineReaderOptions spineReaderOptions = new()
            {
                IgnoreMissingContentFiles = true
            };
            List<EpubLocalTextContentFileRef> expectedReadingOrder = [];
            List<EpubLocalTextContentFileRef> actualReadingOrder = SpineReader.GetReadingOrder(epubSchema, epubContentRef, spineReaderOptions);
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        private static EpubSchema CreateEpubSchema(EpubManifest? manifest = null, EpubSpine? spine = null)
        {
            return new
            (
                package: new
                (
                    uniqueIdentifier: null,
                    epubVersion: EpubVersion.EPUB_3,
                    metadata: new(),
                    manifest: manifest ?? new(),
                    spine: spine ?? new(),
                    guide: null
                ),
                epub2Ncx: null,
                epub3NavDocument: null,
                mediaOverlays: null,
                contentDirectoryPath: String.Empty
            );
        }

        private static EpubLocalTextContentFileRef CreateTestHtmlFileRef(string fileName)
        {
            return new(new EpubContentFileRefMetadata(fileName, EpubContentType.XHTML_1_1, "application/xhtml+xml"), fileName, new TestEpubContentLoader());
        }

        private static EpubContentRef CreateEpubContentRefForTestRemoteFile(string fileUrl)
        {
            List<EpubRemoteTextContentFileRef> htmlRemote =
            [
                new
               (
                   metadata: new
                   (
                       key: fileUrl,
                       contentType: EpubContentType.XHTML_1_1,
                       contentMimeType: "application/xhtml+xml"
                   ),
                   epubContentLoader: new TestEpubContentLoader()
               )
            ];
            EpubContentRef epubContentRef = new
            (
                html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(null, htmlRemote.AsReadOnly())
            );
            return epubContentRef;
        }
    }
}
