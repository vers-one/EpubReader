using System;
using System.Collections.Generic;
using System.Linq;
using VersOne.Epub.Schema;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal static class BookCoverReader
    {
        public static EpubByteContentFileRef ReadBookCover(EpubSchema epubSchema, Dictionary<string, EpubByteContentFileRef> imageContentRefs)
        {
            EpubByteContentFileRef result;
            if (epubSchema.Package.EpubVersion == EpubVersion.EPUB_3 || epubSchema.Package.EpubVersion == EpubVersion.EPUB_3_1)
            {
                result = ReadEpub3Cover(epubSchema, imageContentRefs);
                if (result == null)
                {
                    result = ReadEpub2Cover(epubSchema, imageContentRefs);
                }
            }
            else
            {
                result = ReadEpub2Cover(epubSchema, imageContentRefs);
            }
            return result;
        }

        private static EpubByteContentFileRef ReadEpub2Cover(EpubSchema epubSchema, Dictionary<string, EpubByteContentFileRef> imageContentRefs)
        {
            EpubByteContentFileRef result = ReadEpub2CoverFromMetadata(epubSchema, imageContentRefs);
            if (result == null)
            {
                result = ReadEpub2CoverFromGuide(epubSchema, imageContentRefs);
            }
            return result;
        }

        private static EpubByteContentFileRef ReadEpub2CoverFromMetadata(EpubSchema epubSchema, Dictionary<string, EpubByteContentFileRef> imageContentRefs)
        {
            List<EpubMetadataMeta> metaItems = epubSchema.Package.Metadata.MetaItems;
            if (metaItems == null || !metaItems.Any())
            {
                return null;
            }
            EpubMetadataMeta coverMetaItem = metaItems.FirstOrDefault(metaItem => metaItem.Name.CompareOrdinalIgnoreCase("cover"));
            if (coverMetaItem == null)
            {
                return null;
            }
            if (String.IsNullOrEmpty(coverMetaItem.Content))
            {
                throw new EpubPackageException("Incorrect EPUB metadata: cover item content is missing.");
            }
            EpubManifestItem coverManifestItem = epubSchema.Package.Manifest.FirstOrDefault(manifestItem => manifestItem.Id.CompareOrdinalIgnoreCase(coverMetaItem.Content));
            if (coverManifestItem == null)
            {
                throw new EpubPackageException($"Incorrect EPUB manifest: item with ID = \"{coverMetaItem.Content}\" is missing.");
            }
            if (coverManifestItem.Href == null)
            {
                return null;
            }
            if (!imageContentRefs.TryGetValue(coverManifestItem.Href, out EpubByteContentFileRef coverImageContentFileRef))
            {
                throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{coverManifestItem.Href}\" is missing.");
            }
            return coverImageContentFileRef;
        }

        private static EpubByteContentFileRef ReadEpub2CoverFromGuide(EpubSchema epubSchema, Dictionary<string, EpubByteContentFileRef> imageContentRefs)
        {
            if (epubSchema.Package.Guide != null)
            {
                foreach (EpubGuideReference guideReference in epubSchema.Package.Guide)
                {
                    if (guideReference.Type.ToLowerInvariant() == "cover" && imageContentRefs.TryGetValue(guideReference.Href, out EpubByteContentFileRef coverImageContentFileRef))
                    {
                        return coverImageContentFileRef;
                    }
                }
            }
            return null;
        }

        private static EpubByteContentFileRef ReadEpub3Cover(EpubSchema epubSchema, Dictionary<string, EpubByteContentFileRef> imageContentRefs)
        {
            EpubManifestItem coverManifestItem =
                epubSchema.Package.Manifest.FirstOrDefault(manifestItem => manifestItem.Properties != null && manifestItem.Properties.Contains(EpubManifestProperty.COVER_IMAGE));
            if (coverManifestItem == null || coverManifestItem.Href == null)
            {
                return null;
            }
            if (!imageContentRefs.TryGetValue(coverManifestItem.Href, out EpubByteContentFileRef coverImageContentFileRef))
            {
                throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{coverManifestItem.Href}\" is missing.");
            }
            return coverImageContentFileRef;
        }
    }
}
