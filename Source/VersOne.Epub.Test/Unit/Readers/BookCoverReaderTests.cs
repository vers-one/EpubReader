using VersOne.Epub.Internal;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class BookCoverReaderTests
    {
        [Fact(DisplayName = "ReadBookCover should return null for a EPUB 2 book without a cover")]
        public void ReadBookCoverForEpub2WithoutCoverTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_2);
            TestSuccessfulReadOperation(epubBookRef, null);
        }

        [Fact(DisplayName = "ReadBookCover should return null for a EPUB 3 book without a cover")]
        public void ReadBookCoverForEpub3WithoutCoverTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_3);
            TestSuccessfulReadOperation(epubBookRef, null);
        }

        [Fact(DisplayName = "Extracting cover from EPUB 2 book with a cover defined in the metadata should succeed")]
        public void ReadBookCoverForEpub2WithCoverInMetadataTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_2);
            epubBookRef.Schema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = "cover-image"
            });
            epubBookRef.Schema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = "cover.jpg",
                MediaType = "image/jpeg"
            });
            EpubByteContentFileRef expectedCoverImageFileRef = CreateTestImageFileRef(epubBookRef);
            TestSuccessfulReadOperation(epubBookRef, expectedCoverImageFileRef);
        }

        [Fact(DisplayName = "Extracting cover from EPUB 2 book with a cover defined in the guide should succeed")]
        public void ReadBookCoverForEpub2WithCoverInGuideTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_2);
            epubBookRef.Schema.Package.Guide = new EpubGuide()
            {
                Items = new List<EpubGuideReference>()
                {
                    new EpubGuideReference()
                    {
                        Type = "cover",
                        Href = "cover.jpg"
                    }
                }
            };
            EpubByteContentFileRef expectedCoverImageFileRef = CreateTestImageFileRef(epubBookRef);
            TestSuccessfulReadOperation(epubBookRef, expectedCoverImageFileRef);
        }

        [Fact(DisplayName = "Extracting cover from EPUB 3 book with a cover defined in the manifest should succeed")]
        public void ReadBookCoverForEpub3WithCoverInManifestTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_3);
            epubBookRef.Schema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = "cover.jpg",
                MediaType = "image/jpeg",
                Properties = new List<EpubManifestProperty>()
                {
                    EpubManifestProperty.COVER_IMAGE
                }
            });
            EpubByteContentFileRef expectedCoverImageFileRef = CreateTestImageFileRef(epubBookRef);
            TestSuccessfulReadOperation(epubBookRef, expectedCoverImageFileRef);
        }

        [Fact(DisplayName = "ReadBookCover should not fail if Metadata.MetaItems is set to null in a EPUB 2 book")]
        public void ReadBookCoverForEpub2WithNullMetaItemsTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_2);
            epubBookRef.Schema.Package.Metadata.MetaItems = null;
            Dictionary<string, EpubByteContentFileRef> imageContentRefs = new();
            EpubByteContentFileRef coverImageFileRef = BookCoverReader.ReadBookCover(epubBookRef.Schema, imageContentRefs);
            Assert.Null(coverImageFileRef);
        }

        [Fact(DisplayName = "ReadBookCover should not fail if Metadata.MetaItems has no cover item in a EPUB 2 book")]
        public void ReadBookCoverForEpub2WithNoCoverMetaItemTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_2);
            epubBookRef.Schema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "other-item",
                Content = "some content"
            });
            TestSuccessfulReadOperation(epubBookRef, null);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the Content property in the cover meta item is null")]
        public void ReadBookCoverForEpub2WithNullCoverMetaItemContentTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_2);
            epubBookRef.Schema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = null
            });
            TestFailingReadOperation(epubBookRef);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the Content property in the cover meta item is empty")]
        public void ReadBookCoverForEpub2WithEmptyCoverMetaItemContentTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_2);
            epubBookRef.Schema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = String.Empty
            });
            TestFailingReadOperation(epubBookRef);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the manifest item with the ID specified in the cover meta item is missing")]
        public void ReadBookCoverForEpub2WithMissingManifestItemTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_2);
            epubBookRef.Schema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = "cover-image"
            });
            TestFailingReadOperation(epubBookRef);
        }

        [Fact(DisplayName = "ReadBookCover should not fail if the Href property in the EPUB 2 cover manifest item is null")]
        public void ReadBookCoverForEpub2WithNullHrefInCoverManifestItemTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_2);
            epubBookRef.Schema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = "cover-image"
            });
            epubBookRef.Schema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = null,
                MediaType = "image/jpeg"
            });
            TestSuccessfulReadOperation(epubBookRef, null);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover manifest item is missing in the EPUB 2 file")]
        public void ReadBookCoverForEpub2WithMissingManifestItemImageTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_2);
            epubBookRef.Schema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = "cover-image"
            });
            epubBookRef.Schema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = "cover.jpg",
                MediaType = "image/jpeg"
            });
            TestFailingReadOperation(epubBookRef);
        }

        [Fact(DisplayName = "ReadBookCover should return null if a EPUB 2 book has no cover in the metadata and no cover references in the guide")]
        public void ReadBookCoverForEpub2WithNoCoverInMetadataAndGuideTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_2);
            epubBookRef.Schema.Package.Guide = new EpubGuide()
            {
                Items = new List<EpubGuideReference>()
                {
                    new EpubGuideReference()
                    {
                        Type = "test-type",
                        Href = "test.jpg"
                    }
                }
            };
            TestSuccessfulReadOperation(epubBookRef, null);
        }

        [Fact(DisplayName = "ReadBookCover should return null if a EPUB 3 book has no cover in the manifest")]
        public void ReadBookCoverForEpub3WithNoCoverInManifestTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_3);
            epubBookRef.Schema.Package.Manifest = new EpubManifest()
            {
                Items = new List<EpubManifestItem>()
                {
                    new EpubManifestItem()
                    {
                        Id = "test-image",
                        Href = "test.jpg",
                        MediaType = "image/jpeg"
                    },
                    new EpubManifestItem()
                    {
                        Id = "test-item-with-property",
                        Href = "toc.html",
                        MediaType = "application/xhtml+xml",
                        Properties = new List<EpubManifestProperty>()
                        {
                            EpubManifestProperty.NAV
                        }
                    }
                }
            };
            TestSuccessfulReadOperation(epubBookRef, null);
        }

        [Fact(DisplayName = "ReadBookCover should return null if the Href property in the EPUB 3 cover manifest item is null")]
        public void ReadBookCoverForEpub3WithNullHrefInCoverManifestItemTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_3);
            epubBookRef.Schema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = null,
                MediaType = "image/jpeg",
                Properties = new List<EpubManifestProperty>()
                {
                    EpubManifestProperty.COVER_IMAGE
                }
            });
            TestSuccessfulReadOperation(epubBookRef, null);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover manifest item is missing in the EPUB 3 file")]
        public void ReadBookCoverForEpub3WithMissingManifestItemImageTest()
        {
            EpubBookRef epubBookRef = CreateEmptyEpubBookRef(EpubVersion.EPUB_3);
            epubBookRef.Schema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = "cover.jpg",
                MediaType = "image/jpeg",
                Properties = new List<EpubManifestProperty>()
                {
                    EpubManifestProperty.COVER_IMAGE
                }
            });
            TestFailingReadOperation(epubBookRef);
        }

        private void TestSuccessfulReadOperation(EpubBookRef epubBookRef, EpubByteContentFileRef expectedCoverImageFileRef)
        {
            Dictionary<string, EpubByteContentFileRef> imageContentRefs =
                expectedCoverImageFileRef != null ? CreateImageContentRefs(expectedCoverImageFileRef) : new Dictionary<string, EpubByteContentFileRef>();
            EpubByteContentFileRef actualCoverImageFileRef = BookCoverReader.ReadBookCover(epubBookRef.Schema, imageContentRefs);
            Assert.Equal(expectedCoverImageFileRef, actualCoverImageFileRef);
        }

        private void TestFailingReadOperation(EpubBookRef epubBookRef)
        {
            Assert.Throws<EpubPackageException>(() => BookCoverReader.ReadBookCover(epubBookRef.Schema, new Dictionary<string, EpubByteContentFileRef>()));
        }

        private EpubBookRef CreateEmptyEpubBookRef(EpubVersion epubVersion)
        {
            return new(new TestZipFile())
            {
                Schema = new EpubSchema()
                {
                    Package = new EpubPackage()
                    {
                        EpubVersion = epubVersion,
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
                        Spine = new EpubSpine(),
                        Guide = null
                    }
                }
            };
        }

        private EpubByteContentFileRef CreateTestImageFileRef(EpubBookRef epubBookRef)
        {
            return new(epubBookRef, "cover.jpg", EpubContentType.IMAGE_JPEG, "image/jpeg");
        }

        private Dictionary<string, EpubByteContentFileRef> CreateImageContentRefs(EpubByteContentFileRef imageFileRef)
        {
            return new()
            {
                {
                    imageFileRef.FileName,
                    imageFileRef
                }
            };
        }
    }
}
