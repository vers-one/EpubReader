using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class SpineReaderTests
    {
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
                manifest: new EpubManifest
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

        [Fact(DisplayName = "GetReadingOrder should throw EpubPackageException if there is no manifest item with ID matching to the ID ref of a spine item and SpineReaderOptions.IgnoreMissingManifestItems is false")]
        public void GetReadingOrderWithMissingManifestItemWithoutIgnoringErrorsTest()
        {
            EpubSchema epubSchema = CreateEpubSchema
            (
                manifest: new EpubManifest
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
                spine: new EpubSpine
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
            EpubContentRef epubContentRef = new();
            Assert.Throws<EpubPackageException>(() => SpineReader.GetReadingOrder(epubSchema, epubContentRef, new SpineReaderOptions()));
        }

        [Fact(DisplayName = "GetReadingOrder should skip non-existent manifest items if SpineReaderOptions.IgnoreMissingManifestItems is true")]
        public void GetReadingOrderWithMissingManifestItemWithIgnoringErrorsTest()
        {
            EpubSchema epubSchema = CreateEpubSchema
            (
                manifest: new EpubManifest
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
                spine: new EpubSpine
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
            EpubContentRef epubContentRef = new();
            SpineReaderOptions spineReaderOptions = new()
            {
                IgnoreMissingManifestItems = true
            };
            List<EpubLocalTextContentFileRef> expectedReadingOrder = [];
            List<EpubLocalTextContentFileRef> actualReadingOrder = SpineReader.GetReadingOrder(epubSchema, epubContentRef, spineReaderOptions);
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        [Fact(DisplayName = "GetReadingOrder should throw EpubPackageException if there is no HTML content file referenced by a manifest item")]
        public void GetReadingOrderWithMissingHtmlContentFileTest()
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
            EpubContentRef epubContentRef = new();
            Assert.Throws<EpubPackageException>(() => SpineReader.GetReadingOrder(epubSchema, epubContentRef, new SpineReaderOptions()));
        }

        [Fact(DisplayName = "GetReadingOrder should throw EpubPackageException if the HTML content file referenced by a spine item is a remote resource")]
        public void GetReadingOrderWithRemoteHtmlContentFileTest()
        {
            string remoteFileHref = "https://example.com/books/123/chapter1.html";
            EpubSchema epubSchema = CreateEpubSchema
            (
                manifest: new
                (
                    items:
                    [
                        new
                        (
                            id: "item-1",
                            href: remoteFileHref,
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
            List<EpubRemoteTextContentFileRef> htmlRemote =
            [
                new
                (
                    metadata: new
                    (
                        key: remoteFileHref,
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
            Assert.Throws<EpubPackageException>(() => SpineReader.GetReadingOrder(epubSchema, epubContentRef, new SpineReaderOptions()));
        }

        private static EpubSchema CreateEpubSchema(EpubManifest? manifest = null, EpubSpine? spine = null)
        {
            return new
            (
                package: new
                (
                    uniqueIdentifier: null,
                    epubVersion: EpubVersion.EPUB_3,
                    metadata: new EpubMetadata(),
                    manifest: manifest ?? new EpubManifest(),
                    spine: spine ?? new EpubSpine(),
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
    }
}
