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
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta
            (
                name: "cover",
                content: "cover-image"
            ));
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem
            (
                id: "cover-image",
                href: LOCAL_COVER_FILE_NAME,
                mediaType: COVER_FILE_CONTENT_MIME_TYPE
            ));
            EpubLocalByteContentFileRef expectedCoverImageFileRef = CreateLocalTestImageFileRef();
            TestSuccessfulReadOperation(epubSchema, expectedCoverImageFileRef);
        }

        [Fact(DisplayName = "Extracting cover from EPUB 2 book with a cover defined in the guide should succeed")]
        public void ReadBookCoverForEpub2WithCoverInGuideTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema
            (
                epubVersion: EpubVersion.EPUB_2,
                guide: new EpubGuide
                (
                    items: new List<EpubGuideReference>()
                    {
                        new EpubGuideReference
                        (
                            type: "cover",
                            title: null,
                            href: LOCAL_COVER_FILE_NAME
                        )
                    }
                )
            );
            EpubLocalByteContentFileRef expectedCoverImageFileRef = CreateLocalTestImageFileRef();
            TestSuccessfulReadOperation(epubSchema, expectedCoverImageFileRef);
        }

        [Fact(DisplayName = "ReadBookCover should return null if the cover defined in the guide doesn't exist among the content image files")]
        public void ReadBookCoverForEpub2WithCoverInGuideReferencingNonExistingImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema
            (
                epubVersion: EpubVersion.EPUB_2,
                guide: new EpubGuide
                (
                    items: new List<EpubGuideReference>()
                    {
                        new EpubGuideReference
                        (
                            type: "cover",
                            title: null,
                            href: LOCAL_COVER_FILE_NAME
                        )
                    }
                )
            );
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "Extracting cover from EPUB 3 book with a cover defined in the manifest should succeed")]
        public void ReadBookCoverForEpub3WithCoverInManifestTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_3);
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem
            (
                id: "cover-image",
                href: LOCAL_COVER_FILE_NAME,
                mediaType: COVER_FILE_CONTENT_MIME_TYPE,
                properties: new List<EpubManifestProperty>()
                {
                    EpubManifestProperty.COVER_IMAGE
                }
            ));
            EpubLocalByteContentFileRef expectedCoverImageFileRef = CreateLocalTestImageFileRef();
            TestSuccessfulReadOperation(epubSchema, expectedCoverImageFileRef);
        }

        [Fact(DisplayName = "ReadBookCover should not fail if Metadata.MetaItems has no cover item in a EPUB 2 book")]
        public void ReadBookCoverForEpub2WithNoCoverMetaItemTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta
            (
                name: "other-item",
                content: "some content"
            ));
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the Content property in the cover meta item is empty")]
        public void ReadBookCoverForEpub2WithEmptyCoverMetaItemContentTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta
            (
                name: "cover",
                content: String.Empty
            ));
            TestFailingReadOperation(epubSchema);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the manifest item with the ID specified in the cover meta item is missing")]
        public void ReadBookCoverForEpub2WithMissingManifestItemTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta
            (
                name: "cover",
                content: "cover-image"
            ));
            TestFailingReadOperation(epubSchema);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover manifest item is missing in the EPUB 2 file")]
        public void ReadBookCoverForEpub2WithMissingManifestItemImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta
            (
                name: "cover",
                content: "cover-image"
            ));
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem
            (
                id: "cover-image",
                href: LOCAL_COVER_FILE_NAME,
                mediaType: COVER_FILE_CONTENT_MIME_TYPE
            ));
            TestFailingReadOperation(epubSchema);
        }

        [Fact(DisplayName = "ReadBookCover should return null if a EPUB 2 book has no cover in the metadata and no cover references in the guide")]
        public void ReadBookCoverForEpub2WithNoCoverInMetadataAndGuideTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema
            (
                epubVersion: EpubVersion.EPUB_2,
                guide: new EpubGuide
                (
                    items: new List<EpubGuideReference>()
                    {
                        new EpubGuideReference
                        (
                            type: "test-type",
                            title: null,
                            href: "test.jpg"
                        )
                    }
                )
            );
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "ReadBookCover should return null if a EPUB 3 book has no cover in the manifest")]
        public void ReadBookCoverForEpub3WithNoCoverInManifestTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema
            (
                epubVersion: EpubVersion.EPUB_3,
                manifest: new EpubManifest
                (
                    items: new List<EpubManifestItem>()
                    {
                        new EpubManifestItem
                        (
                            id: "test-image",
                            href: "test.jpg",
                            mediaType: COVER_FILE_CONTENT_MIME_TYPE
                        ),
                        new EpubManifestItem
                        (
                            id: "test-item-with-property",
                            href: "toc.html",
                            mediaType: "application/xhtml+xml",
                            properties: new List<EpubManifestProperty>()
                            {
                                EpubManifestProperty.NAV
                            }
                        )
                    }
                )
            );
            TestSuccessfulReadOperation(epubSchema, null);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover manifest item is missing in the EPUB 3 file")]
        public void ReadBookCoverForEpub3WithMissingManifestItemImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_3);
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem
            (
                id: "cover-image",
                href: LOCAL_COVER_FILE_NAME,
                mediaType: COVER_FILE_CONTENT_MIME_TYPE,
                properties: new List<EpubManifestProperty>()
                {
                    EpubManifestProperty.COVER_IMAGE
                }
            ));
            TestFailingReadOperation(epubSchema);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover manifest item in the EPUB 2 file is a remote resource")]
        public void ReadBookCoverForEpub2WithRemoteManifestItemImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_2);
            epubSchema.Package.Metadata.MetaItems.Add(new EpubMetadataMeta
            (
                name: "cover",
                content: "cover-image"
            ));
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem
            (
                id: "cover-image",
                href: REMOTE_COVER_FILE_HREF,
                mediaType: COVER_FILE_CONTENT_MIME_TYPE
            ));
            EpubRemoteByteContentFileRef remoteTestImageFileRef = CreateRemoteTestImageFileRef();
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs = CreateImageContentRefs(remoteImageFileRef: remoteTestImageFileRef);
            TestFailingReadOperation(epubSchema, imageContentRefs);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover guide item in the EPUB 2 file is a remote resource")]
        public void ReadBookCoverForEpub2WithRemoteGuideItemImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema
            (
                epubVersion: EpubVersion.EPUB_2,
                guide: new EpubGuide
                (
                    items: new List<EpubGuideReference>()
                    {
                        new EpubGuideReference
                        (
                            type: "cover",
                            title: null,
                            href: REMOTE_COVER_FILE_HREF
                        )
                    }
                )
            );
            EpubRemoteByteContentFileRef remoteTestImageFileRef = CreateRemoteTestImageFileRef();
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs = CreateImageContentRefs(remoteImageFileRef: remoteTestImageFileRef);
            TestFailingReadOperation(epubSchema, imageContentRefs);
        }

        [Fact(DisplayName = "ReadBookCover should throw EpubPackageException if the image referenced by the cover manifest item in the EPUB 3 file is a remote resource")]
        public void ReadBookCoverForEpub3WithRemoteManifestItemImageTest()
        {
            EpubSchema epubSchema = CreateEmptyEpubSchema(EpubVersion.EPUB_3);
            epubSchema.Package.Manifest.Items.Add(new EpubManifestItem
            (
                id: "cover-image",
                href: REMOTE_COVER_FILE_HREF,
                mediaType: COVER_FILE_CONTENT_MIME_TYPE,
                properties: new List<EpubManifestProperty>()
                {
                    EpubManifestProperty.COVER_IMAGE
                }
            ));
            EpubRemoteByteContentFileRef remoteTestImageFileRef = CreateRemoteTestImageFileRef();
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs = CreateImageContentRefs(remoteImageFileRef: remoteTestImageFileRef);
            TestFailingReadOperation(epubSchema, imageContentRefs);
        }

        private static void TestSuccessfulReadOperation(EpubSchema epubSchema, EpubLocalByteContentFileRef? expectedLocalCoverImageFileRef)
        {
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs =
                expectedLocalCoverImageFileRef != null
                ? CreateImageContentRefs(localImageFileRef: expectedLocalCoverImageFileRef)
                : new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>();
            EpubLocalByteContentFileRef? actualCoverImageFileRef = BookCoverReader.ReadBookCover(epubSchema, imageContentRefs);
            Assert.Equal(expectedLocalCoverImageFileRef, actualCoverImageFileRef);
        }

        private static void TestFailingReadOperation(EpubSchema epubSchema, EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>? imageContentRefs = null)
        {
            imageContentRefs ??= new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>();
            Assert.Throws<EpubPackageException>(() => BookCoverReader.ReadBookCover(epubSchema, imageContentRefs));
        }

        private static EpubSchema CreateEmptyEpubSchema(EpubVersion epubVersion, EpubManifest? manifest = null, EpubGuide? guide = null)
        {
            return new EpubSchema
            (
                package: new EpubPackage
                (
                    epubVersion: epubVersion,
                    metadata: new EpubMetadata(),
                    manifest: manifest ?? new EpubManifest(),
                    spine: new EpubSpine(),
                    guide: guide
                ),
                epub2Ncx: null,
                epub3NavDocument: null,
                contentDirectoryPath: String.Empty
            );
        }

        private static EpubLocalByteContentFileRef CreateLocalTestImageFileRef()
        {
            EpubContentFileRefMetadata localImageFileRefMetadata = new(LOCAL_COVER_FILE_NAME, COVER_FILE_CONTENT_TYPE, COVER_FILE_CONTENT_MIME_TYPE);
            return new(localImageFileRefMetadata, LOCAL_COVER_FILE_NAME, new TestEpubContentLoader());
        }

        private static EpubRemoteByteContentFileRef CreateRemoteTestImageFileRef()
        {
            EpubContentFileRefMetadata remoteImageFileRefMetadata = new(REMOTE_COVER_FILE_HREF, COVER_FILE_CONTENT_TYPE, COVER_FILE_CONTENT_MIME_TYPE);
            return new(remoteImageFileRefMetadata, new TestEpubContentLoader());
        }

        private static EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> CreateImageContentRefs(
            EpubLocalByteContentFileRef? localImageFileRef = null, EpubRemoteByteContentFileRef? remoteImageFileRef = null)
        {
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> result = new();
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
