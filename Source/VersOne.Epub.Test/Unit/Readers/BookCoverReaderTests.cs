using VersOne.Epub.Internal;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class BookCoverReaderTests
    {
        private const string LOCAL_COVER_FILE_NAME = "cover.jpg";
        private const string REMOTE_COVER_FILE_HREF = "https://example.com/books/123/cover.jpg";
        private const EpubContentType COVER_FILE_CONTENT_TYPE = EpubContentType.IMAGE_JPEG;
        private const string COVER_FILE_CONTENT_MIME_TYPE = "image/jpeg";

        [Fact(DisplayName = "ReadBookCover should return null for a EPUB 2 book without a cover")]
        public void ReadBookCoverForEpub2WithoutCoverTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "ReadBookCover should return null for a EPUB 3 book without a cover")]
        public void ReadBookCoverForEpub3WithoutCoverTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_3);
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "Extracting cover from EPUB 2 book with a cover defined in the metadata should succeed")]
        public void ReadBookCoverForEpub2WithCoverInMetadataTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = "cover-image"
            });
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = LOCAL_COVER_FILE_NAME,
                MediaType = COVER_FILE_CONTENT_MIME_TYPE
            });
            EpubLocalByteContentFileRef expectedCoverImageFileRef = CreateLocalTestImageFileRef();
            TestSuccessfulReadOperation(epubSchema, expectedCoverImageFileRef);
        }

        [Fact(DisplayName = "Extracting cover from EPUB 2 book with a cover defined in the guide should succeed")]
        public void ReadBookCoverForEpub2WithCoverInGuideTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Guide = new EpubGuide()
            {
                Items = new List<EpubGuideReference>()
                {
                    new EpubGuideReference()
                    {
                        Type = "cover",
                        Href = LOCAL_COVER_FILE_NAME
                    }
                }
            };
            EpubLocalByteContentFileRef expectedCoverImageFileRef = CreateLocalTestImageFileRef();
            TestSuccessfulReadOperation(epubSchema, expectedCoverImageFileRef);
        }

        [Fact(DisplayName = "ReadBookCover should return null if the cover defined in the guide doesn't exist among the content image files")]
        public void ReadBookCoverForEpub2WithCoverInGuideReferencingNonExistingImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Guide = new EpubGuide()
            {
                Items = new List<EpubGuideReference>()
                {
                    new EpubGuideReference()
                    {
                        Type = "cover",
                        Href = LOCAL_COVER_FILE_NAME
                    }
                }
            };
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "Extracting cover from EPUB 3 book with a cover defined in the manifest should succeed")]
        public void ReadBookCoverForEpub3WithCoverInManifestTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_3);
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = LOCAL_COVER_FILE_NAME,
                MediaType = COVER_FILE_CONTENT_MIME_TYPE,
                Properties = new List<EpubManifestProperty>()
                {
                    EpubManifestProperty.COVER_IMAGE
                }
            });
            EpubLocalByteContentFileRef expectedCoverImageFileRef = CreateLocalTestImageFileRef();
            TestSuccessfulReadOperation(epubSchema, expectedCoverImageFileRef);
        }

        [Fact(DisplayName = "ReadBookCover should not fail if Metadata.MetaItems is set to null in a EPUB 2 book")]
        public void ReadBookCoverForEpub2WithNullMetaItemsTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems = null;
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs = CreateImageContentRefs();
            EpubLocalByteContentFileRef coverImageFileRef = BookCoverReader.ReadBookCover(epubSchema, imageContentRefs);
            Assert.Null(coverImageFileRef);
        }

        [Fact(DisplayName = "ReadBookCover should not fail if Metadata.MetaItems has no cover item in a EPUB 2 book")]
        public void ReadBookCoverForEpub2WithNoCoverMetaItemTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "other-item",
                Content = "some content"
            });
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the Content property in the cover meta item is null")]
        public void ReadBookCoverForEpub2WithNullCoverMetaItemContentTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = null
            });
            TestFailingReadOperation(epubSchema);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the Content property in the cover meta item is empty")]
        public void ReadBookCoverForEpub2WithEmptyCoverMetaItemContentTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = String.Empty
            });
            TestFailingReadOperation(epubSchema);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the manifest item with the ID specified in the cover meta item is missing")]
        public void ReadBookCoverForEpub2WithMissingManifestItemTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = "cover-image"
            });
            TestFailingReadOperation(epubSchema);
        }

        [Fact(DisplayName = "ReadBookCover should not fail if the Href property in the EPUB 2 cover manifest item is null")]
        public void ReadBookCoverForEpub2WithNullHrefInCoverManifestItemTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = "cover-image"
            });
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = null,
                MediaType = COVER_FILE_CONTENT_MIME_TYPE
            });
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover manifest item is missing in the EPUB 2 file")]
        public void ReadBookCoverForEpub2WithMissingManifestItemImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = "cover-image"
            });
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = LOCAL_COVER_FILE_NAME,
                MediaType = COVER_FILE_CONTENT_MIME_TYPE
            });
            TestFailingReadOperation(epubSchema);
        }

        [Fact(DisplayName = "ReadBookCover should return null if a EPUB 2 book has no cover in the metadata and no cover references in the guide")]
        public void ReadBookCoverForEpub2WithNoCoverInMetadataAndGuideTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Guide = new EpubGuide()
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
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "ReadBookCover should return null if a EPUB 3 book has no cover in the manifest")]
        public void ReadBookCoverForEpub3WithNoCoverInManifestTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_3);
            epubSchema.Package.Manifest = new EpubManifest()
            {
                Items = new List<EpubManifestItem>()
                {
                    new EpubManifestItem()
                    {
                        Id = "test-image",
                        Href = "test.jpg",
                        MediaType = COVER_FILE_CONTENT_MIME_TYPE
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
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "ReadBookCover should return null if the Href property in the EPUB 3 cover manifest item is null")]
        public void ReadBookCoverForEpub3WithNullHrefInCoverManifestItemTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_3);
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = null,
                MediaType = COVER_FILE_CONTENT_MIME_TYPE,
                Properties = new List<EpubManifestProperty>()
                {
                    EpubManifestProperty.COVER_IMAGE
                }
            });
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover manifest item is missing in the EPUB 3 file")]
        public void ReadBookCoverForEpub3WithMissingManifestItemImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_3);
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = LOCAL_COVER_FILE_NAME,
                MediaType = COVER_FILE_CONTENT_MIME_TYPE,
                Properties = new List<EpubManifestProperty>()
                {
                    EpubManifestProperty.COVER_IMAGE
                }
            });
            TestFailingReadOperation(epubSchema);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover manifest item in the EPUB 2 file is a remote resource")]
        public void ReadBookCoverForEpub2WithRemoteManifestItemImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta()
            {
                Name = "cover",
                Content = "cover-image"
            });
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = REMOTE_COVER_FILE_HREF,
                MediaType = COVER_FILE_CONTENT_MIME_TYPE
            });
            EpubRemoteByteContentFileRef remoteTestImageFileRef = CreateRemoteTestImageFileRef();
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs = CreateImageContentRefs(remoteImageFileRef: remoteTestImageFileRef);
            TestFailingReadOperation(epubSchema, imageContentRefs);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover guide item in the EPUB 2 file is a remote resource")]
        public void ReadBookCoverForEpub2WithRemoteGuideItemImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Guide = new EpubGuide()
            {
                Items = new List<EpubGuideReference>()
                {
                    new EpubGuideReference()
                    {
                        Type = "cover",
                        Href = REMOTE_COVER_FILE_HREF
                    }
                }
            };
            EpubRemoteByteContentFileRef remoteTestImageFileRef = CreateRemoteTestImageFileRef();
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs = CreateImageContentRefs(remoteImageFileRef: remoteTestImageFileRef);
            TestFailingReadOperation(epubSchema, imageContentRefs);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover manifest item in the EPUB 3 file is a remote resource")]
        public void ReadBookCoverForEpub3WithRemoteManifestItemImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_3);
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem()
            {
                Id = "cover-image",
                Href = REMOTE_COVER_FILE_HREF,
                MediaType = COVER_FILE_CONTENT_MIME_TYPE,
                Properties = new List<EpubManifestProperty>()
                {
                    EpubManifestProperty.COVER_IMAGE
                }
            });
            EpubRemoteByteContentFileRef remoteTestImageFileRef = CreateRemoteTestImageFileRef();
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs = CreateImageContentRefs(remoteImageFileRef: remoteTestImageFileRef);
            TestFailingReadOperation(epubSchema, imageContentRefs);
        }

        private void TestSuccessfulReadOperation(EpubSchema epubSchema, EpubLocalByteContentFileRef expectedLocalCoverImageFileRef)
        {
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs;
            if (expectedLocalCoverImageFileRef != null)
            {
                imageContentRefs = CreateImageContentRefs(localImageFileRef: expectedLocalCoverImageFileRef);
            }
            else
            {
                imageContentRefs = new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalByteContentFileRef>(),
                    Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
                };
            }
            EpubLocalByteContentFileRef actualCoverImageFileRef = BookCoverReader.ReadBookCover(epubSchema, imageContentRefs);
            Assert.Equal(expectedLocalCoverImageFileRef, actualCoverImageFileRef);
        }

        private void TestFailingReadOperation(EpubSchema epubSchema, EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs = null)
        {
            imageContentRefs ??= new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
            {
                Local = new Dictionary<string, EpubLocalByteContentFileRef>(),
                Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
            };
            Assert.Throws<EpubPackageException>(() => BookCoverReader.ReadBookCover(epubSchema, imageContentRefs));
        }

        private EpubSchema CreateEmptyEpubSchema(EpubVersion epubVersion)
        {
            return new EpubSchema()
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
            };
        }

        private EpubLocalByteContentFileRef CreateLocalTestImageFileRef()
        {
            EpubContentFileRefMetadata localImageFileRefMetadata = new(LOCAL_COVER_FILE_NAME, COVER_FILE_CONTENT_TYPE, COVER_FILE_CONTENT_MIME_TYPE);
            return new(localImageFileRefMetadata, LOCAL_COVER_FILE_NAME, new TestEpubContentLoader());
        }

        private EpubRemoteByteContentFileRef CreateRemoteTestImageFileRef()
        {
            EpubContentFileRefMetadata remoteImageFileRefMetadata = new(REMOTE_COVER_FILE_HREF, COVER_FILE_CONTENT_TYPE, COVER_FILE_CONTENT_MIME_TYPE);
            return new(remoteImageFileRefMetadata, new TestEpubContentLoader());
        }

        private EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> CreateImageContentRefs(
            EpubLocalByteContentFileRef localImageFileRef = null, EpubRemoteByteContentFileRef remoteImageFileRef = null)
        {
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> result = new()
            {
                Local = new Dictionary<string, EpubLocalByteContentFileRef>(),
                Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
            };
            if (localImageFileRef != null)
            {
                result.Local[localImageFileRef.Key] = localImageFileRef;
            }
            if (remoteImageFileRef != null)
            {
                result.Remote[remoteImageFileRef.Key] = remoteImageFileRef;
            }
            return result;
        }
    }
}
