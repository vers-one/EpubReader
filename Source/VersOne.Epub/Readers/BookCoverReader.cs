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
            EpubManifestItem coverManifestItem = epubSchema.Package.Manifest.FirstOrDefault(manifestItem => manifestItem.Id.CompareOrdinalIgnoreCase(coverMetaItem.Content));
            if (coverManifestItem == null)
            {
                // 2019-08-20 Hotfix: if coverManifestItem is not found by its Id, then try it with its Href - some ebooks refer to the image directly!
                coverManifestItem = epubSchema.Package.Manifest.FirstOrDefault(manifestItem => manifestItem.Href.CompareOrdinalIgnoreCase(coverMetaItem.Content));
                if (null == coverManifestItem)
                {
                    throw new Exception($"Incorrect EPUB manifest: item with ID = \"{coverMetaItem.Content}\" is missing.");
                }
            }
            if (imageContentRefs.TryGetValue(coverManifestItem.Href, out EpubByteContentFileRef coverImageContentFileRef))
            {
                return coverImageContentFileRef;
            }

            // 2019-08-21 Fix some ebooks seem to contain more than one item with Id="cover"
            // thus we test if there is a second item....

            coverManifestItem = epubSchema.Package.Manifest.Where(manifestItem => manifestItem.Id.CompareOrdinalIgnoreCase(coverMetaItem.Content)).Skip(1).FirstOrDefault(); ;
            if (coverManifestItem == null)
            {
                // 2019-08-20 Hotfix: if coverManifestItem is not found by its Id, then try it with its Href - some ebooks refer to the image directly!
                coverManifestItem = epubSchema.Package.Manifest.FirstOrDefault(manifestItem => manifestItem.Href.CompareOrdinalIgnoreCase(coverMetaItem.Content));

                if (null == coverManifestItem)
                {
                    throw new Exception($"Incorrect EPUB manifest: item with ID = \"{coverMetaItem.Content}\" is missing.");
                }
            }
            if (imageContentRefs.TryGetValue(coverManifestItem.Href, out EpubByteContentFileRef coverImageContentFileRef2))
            {
                return coverImageContentFileRef2;
            }
            throw new Exception($"Incorrect EPUB manifest: item with href = \"{coverManifestItem.Href}\" is missing.");
        }
    }
}
