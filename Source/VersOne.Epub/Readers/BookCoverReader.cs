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
                throw new Exception("Incorrect EPUB metadata: cover item content is missing.");
            }

            EpubByteContentFileRef coverImageContentFileRef;
            EpubManifestItem coverManifestItem = epubSchema.Package.Manifest.FirstOrDefault(manifestItem => manifestItem.Id.CompareOrdinalIgnoreCase(coverMetaItem.Content));
            if (null != coverManifestItem?.Href && imageContentRefs.TryGetValue(coverManifestItem.Href, out coverImageContentFileRef))
            {
                return coverImageContentFileRef;
            }

            // For non-standard ebooks, we try several other ways...
            if (null != coverManifestItem) // we have found the item but there was no corresponding image ...
            {
                // some ebooks seem to contain more than one item with Id="cover"
                // thus we test if there is a second item, and whether that is an image....
                coverManifestItem = epubSchema.Package.Manifest.Where(manifestItem => manifestItem.Id.CompareOrdinalIgnoreCase(coverMetaItem.Content)).Skip(1).FirstOrDefault(); ;
                if (null != coverManifestItem?.Href && imageContentRefs.TryGetValue(coverManifestItem.Href, out coverImageContentFileRef))
                {
                    return coverImageContentFileRef;
                }
            }

            // we have still not found the item
            // 2019-08-20 Hotfix: if coverManifestItem is not found by its Id, then try it with its Href - some ebooks refer to the image directly!
            coverManifestItem = epubSchema.Package.Manifest.FirstOrDefault(manifestItem => manifestItem.Href.CompareOrdinalIgnoreCase(coverMetaItem.Content));
            if (null != coverManifestItem?.Href && imageContentRefs.TryGetValue(coverManifestItem.Href, out coverImageContentFileRef))
            {
                return coverImageContentFileRef;
            }
            // 2019-08-24 if it is still not found, then try to find an Id named cover
            coverManifestItem = epubSchema.Package.Manifest.FirstOrDefault(manifestItem => manifestItem.Id.CompareOrdinalIgnoreCase(coverMetaItem.Name));
            if (null != coverManifestItem?.Href && imageContentRefs.TryGetValue(coverManifestItem.Href, out coverImageContentFileRef))
            {
                return coverImageContentFileRef;
            }
            // 2019-08-24 if it is still not found, then try to find it in the guide
            var guideItem = epubSchema.Package.Guide.FirstOrDefault(reference => reference.Title.CompareOrdinalIgnoreCase(coverMetaItem.Name));
            if (null != guideItem?.Href && imageContentRefs.TryGetValue(guideItem.Href, out coverImageContentFileRef))
            {
                return coverImageContentFileRef;
            }


            throw new Exception($"Incorrect EPUB manifest: item with ID = \"{coverMetaItem.Content}\" is missing or no corresponding image was found.");
        }
    }
}
