using VersOne.Epub.Internal;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class SpineReaderTests
    {
        [Fact(DisplayName = "Getting reading order for a minimal EPUB spine should succeed")]
        public void GetReadingOrderForMinimalSpineTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(new TestZipFile());
            List<EpubLocalTextContentFileRef> expectedReadingOrder = new();
            List<EpubLocalTextContentFileRef> actualReadingOrder = SpineReader.GetReadingOrder(epubBookRef);
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        [Fact(DisplayName = "Getting reading order for a typical EPUB spine should succeed")]
        public void GetReadingOrderForTypicalSpineTest()
        {
            TestZipFile testZipFile = new();
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(testZipFile);
            epubBookRef.Schema.Package.Spine = new EpubSpine()
            {
                Items = new List<EpubSpineItemRef>()
                {
                    new EpubSpineItemRef()
                    {
                        IdRef = "item-1"
                    },
                    new EpubSpineItemRef()
                    {
                        IdRef = "item-2"
                    }
                }
            };
            epubBookRef.Schema.Package.Manifest = new EpubManifest()
            {
                Items = new List<EpubManifestItem>()
                {
                    new EpubManifestItem()
                    {
                        Id = "item-1",
                        Href = "chapter1.html",
                        MediaType = "application/xhtml+xml"
                    },
                    new EpubManifestItem()
                    {
                        Id = "item-2",
                        Href = "chapter2.html",
                        MediaType = "application/xhtml+xml"
                    }
                }
            };
            EpubLocalTextContentFileRef expectedHtmlFileRef1 = CreateTestHtmlFileRef("chapter1.html");
            EpubLocalTextContentFileRef expectedHtmlFileRef2 = CreateTestHtmlFileRef("chapter2.html");
            epubBookRef.Content.Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
            {
                Local = new Dictionary<string, EpubLocalTextContentFileRef>()
                {
                    {
                        "chapter1.html",
                        expectedHtmlFileRef1
                    },
                    {
                        "chapter2.html",
                        expectedHtmlFileRef2
                    }
                },
                Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
            };
            List<EpubLocalTextContentFileRef> expectedReadingOrder = new()
            {
                expectedHtmlFileRef1,
                expectedHtmlFileRef2
            };
            List<EpubLocalTextContentFileRef> actualReadingOrder = SpineReader.GetReadingOrder(epubBookRef);
            Assert.Equal(expectedReadingOrder, actualReadingOrder);
        }

        [Fact(DisplayName = "GetReadingOrder should throw EpubPackageException if there is no manifest item with ID matching to the ID ref of a spine item")]
        public void GetReadingOrderWithMissingManifestItemTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(new TestZipFile());
            epubBookRef.Schema.Package.Spine = new EpubSpine()
            {
                Items = new List<EpubSpineItemRef>()
                {
                    new EpubSpineItemRef()
                    {
                        IdRef = "item-1"
                    }
                }
            };
            epubBookRef.Schema.Package.Manifest = new EpubManifest()
            {
                Items = new List<EpubManifestItem>()
                {
                    new EpubManifestItem()
                    {
                        Id = "item-2",
                        Href = "chapter2.html",
                        MediaType = "application/xhtml+xml"
                    }
                }
            };
            Assert.Throws<EpubPackageException>(() => SpineReader.GetReadingOrder(epubBookRef));
        }

        [Fact(DisplayName = "GetReadingOrder should throw EpubPackageException if the HTML content file referenced by a spine item is a remote resource")]
        public void GetReadingOrderWithRemoteHtmlContentFileTest()
        {
            string remoteFileHref = "https://example.com/books/123/chapter1.html";
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(new TestZipFile());
            epubBookRef.Schema.Package.Spine = new EpubSpine()
            {
                Items = new List<EpubSpineItemRef>()
                {
                    new EpubSpineItemRef()
                    {
                        IdRef = "item-1"
                    }
                }
            };
            epubBookRef.Schema.Package.Manifest = new EpubManifest()
            {
                Items = new List<EpubManifestItem>()
                {
                    new EpubManifestItem()
                    {
                        Id = "item-1",
                        Href = remoteFileHref,
                        MediaType = "application/xhtml+xml"
                    }
                }
            };
            epubBookRef.Content.Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
            {
                Local = new Dictionary<string, EpubLocalTextContentFileRef>(),
                Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                {
                    {
                        remoteFileHref,
                        new EpubRemoteTextContentFileRef(new EpubContentFileRefMetadata(remoteFileHref, EpubContentType.XHTML_1_1, "application/xhtml+xml"), new TestEpubContentLoader())
                    }
                }
            };
            Assert.Throws<EpubPackageException>(() => SpineReader.GetReadingOrder(epubBookRef));
        }

        [Fact(DisplayName = "GetReadingOrder should throw EpubPackageException if there is no HTML content file referenced by a manifest item")]
        public void GetReadingOrderWithMissingHtmlContentFileTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(new TestZipFile());
            epubBookRef.Schema.Package.Spine = new EpubSpine()
            {
                Items = new List<EpubSpineItemRef>()
                {
                    new EpubSpineItemRef()
                    {
                        IdRef = "item-1"
                    }
                }
            };
            epubBookRef.Schema.Package.Manifest = new EpubManifest()
            {
                Items = new List<EpubManifestItem>()
                {
                    new EpubManifestItem()
                    {
                        Id = "item-1",
                        Href = "chapter1.html",
                        MediaType = "application/xhtml+xml"
                    }
                }
            };
            epubBookRef.Content.Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
            {
                Local = new Dictionary<string, EpubLocalTextContentFileRef>(),
                Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
            };
            Assert.Throws<EpubPackageException>(() => SpineReader.GetReadingOrder(epubBookRef));
        }

        private EpubBookRef CreateEmptyEpubBookRef(TestZipFile testZipFile)
        {
            return new(testZipFile)
            {
                Schema = new EpubSchema()
                {
                    Package = new EpubPackage()
                    {
                        EpubVersion = EpubVersion.EPUB_3,
                        Metadata = new EpubMetadata()
                        {
                            Titles = new List<string>(),
                            Creators = new List<EpubMetadataCreator>(),
                            Subjects = new List<string>(),
                            Publishers = new List<string>(),
                            Contributors = new List<EpubMetadataContributor>(),
                            Dates = new List<EpubMetadataDate>(),
                            Types = new List<string>(),
                            Formats = new List<string>(),
                            Identifiers = new List<EpubMetadataIdentifier>(),
                            Sources = new List<string>(),
                            Languages = new List<string>(),
                            Relations = new List<string>(),
                            Coverages = new List<string>(),
                            Rights = new List<string>(),
                            Links = new List<EpubMetadataLink>(),
                            MetaItems = new List<EpubMetadataMeta>()
                        },
                        Manifest = new EpubManifest()
                        {
                            Items = new List<EpubManifestItem>()
                        },
                        Spine = new EpubSpine()
                        {
                            Items = new List<EpubSpineItemRef>()
                        }
                    }
                },
                Content = new EpubContentRef()
            };
        }

        private EpubLocalTextContentFileRef CreateTestHtmlFileRef(string fileName)
        {
            return new(new EpubContentFileRefMetadata(fileName, EpubContentType.XHTML_1_1, "application/xhtml+xml"), fileName, new TestEpubContentLoader());
        }
    }
}
