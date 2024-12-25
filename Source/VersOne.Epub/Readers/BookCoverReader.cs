using System;
using System.Collections.Generic;
using System.Linq;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal static class BookCoverReader
    {
        public static EpubLocalByteContentFileRef? ReadBookCover(
            EpubSchema epubSchema, EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs,
            BookCoverReaderOptions bookCoverReaderOptions)
        {
            EpubLocalByteContentFileRef? result;
            if (epubSchema.Package.EpubVersion == EpubVersion.EPUB_3 || epubSchema.Package.EpubVersion == EpubVersion.EPUB_3_1)
            {
                result = ReadEpub3Cover(epubSchema, imageContentRefs);
                result ??= ReadEpub2Cover(epubSchema, imageContentRefs, bookCoverReaderOptions);
            }
            else
            {
                result = ReadEpub2Cover(epubSchema, imageContentRefs, bookCoverReaderOptions);
            }
            return result;
        }

        private static EpubLocalByteContentFileRef? ReadEpub2Cover(
            EpubSchema epubSchema, EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs,
            BookCoverReaderOptions bookCoverReaderOptions)
        {
            EpubLocalByteContentFileRef? result = ReadEpub2CoverFromMetadata(epubSchema, imageContentRefs, bookCoverReaderOptions);
            result ??= ReadEpub2CoverFromGuide(epubSchema, imageContentRefs);
            return result;
        }

        private static EpubLocalByteContentFileRef? ReadEpub2CoverFromMetadata(
            EpubSchema epubSchema, EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs,
            BookCoverReaderOptions bookCoverReaderOptions)
        {
            List<EpubMetadataMeta> metaItems = epubSchema.Package.Metadata.MetaItems;
            if (!metaItems.Any())
            {
                return null;
            }
            EpubMetadataMeta coverMetaItem = metaItems.Find(metaItem => metaItem.Name.CompareOrdinalIgnoreCase("cover"));
            if (coverMetaItem == null)
            {
                return null;
            }
            if (String.IsNullOrEmpty(coverMetaItem.Content))
            {
                throw new EpubPackageException("Incorrect EPUB metadata: cover item content is missing.");
            }
            EpubManifestItem? coverManifestItem =
                epubSchema.Package.Manifest.Items.Find(manifestItem => manifestItem.Id.CompareOrdinalIgnoreCase(coverMetaItem.Content));
            if (coverManifestItem == null)
            {
                if (bookCoverReaderOptions.Epub2MetadataIgnoreMissingManifestItem)
                {
                    return null;
                }
                throw new EpubPackageException($"Incorrect EPUB manifest: item with ID = \"{coverMetaItem.Content}\"" +
                    " referenced in EPUB 2 cover metadata is missing.");
            }
            EpubLocalByteContentFileRef result = GetCoverImageContentRef(imageContentRefs, coverManifestItem.Href) ??
                throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{coverManifestItem.Href}\" is missing.");
            return result;
        }

        private static EpubLocalByteContentFileRef? ReadEpub2CoverFromGuide(
            EpubSchema epubSchema, EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs)
        {
            if (epubSchema.Package.Guide != null)
            {
                foreach (EpubGuideReference guideReference in epubSchema.Package.Guide.Items)
                {
                    if (guideReference.Type.ToLowerInvariant() == "cover")
                    {
                        EpubLocalByteContentFileRef? coverImageContentFileRef = GetCoverImageContentRef(imageContentRefs, guideReference.Href);
                        if (coverImageContentFileRef != null)
                        {
                            return coverImageContentFileRef;
                        }
                    }
                }
            }
            return null;
        }

        private static EpubLocalByteContentFileRef? ReadEpub3Cover(
            EpubSchema epubSchema, EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs)
        {
            EpubManifestItem coverManifestItem = epubSchema.Package.Manifest.Items.Find(manifestItem => manifestItem.Properties != null &&
                manifestItem.Properties.Contains(EpubManifestProperty.COVER_IMAGE));
            if (coverManifestItem == null || coverManifestItem.Href == null)
            {
                return null;
            }
            EpubLocalByteContentFileRef result = GetCoverImageContentRef(imageContentRefs, coverManifestItem.Href) ??
                throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{coverManifestItem.Href}\" is missing.");
            return result;
        }

        private static EpubLocalByteContentFileRef? GetCoverImageContentRef(
            EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef> imageContentRefs, string coverImageContentFileKey)
        {
            if (imageContentRefs.ContainsRemoteFileRefWithUrl(coverImageContentFileKey))
            {
                throw new EpubPackageException($"Incorrect EPUB manifest: EPUB cover image \"{coverImageContentFileKey}\" cannot be a remote resource.");
            }
            if (!imageContentRefs.TryGetLocalFileRefByKey(coverImageContentFileKey, out EpubLocalByteContentFileRef coverImageContentFileRef))
            {
                return null;
            }
            return coverImageContentFileRef;
        }
    }
}
