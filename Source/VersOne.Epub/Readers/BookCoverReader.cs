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
                throw new Exception($"Incorrect EPUB manifest: item with ID = \"{coverMetaItem.Content}\" is missing.");
            }
            if (!imageContentRefs.TryGetValue(coverManifestItem.Href, out EpubByteContentFileRef coverImageContentFileRef))
            {
                throw new Exception($"Incorrect EPUB manifest: item with href = \"{coverManifestItem.Href}\" is missing.");
            }
            return coverImageContentFileRef;
        }
    }
}
