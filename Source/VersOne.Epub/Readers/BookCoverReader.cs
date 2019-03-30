using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VersOne.Epub.Schema;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal static class BookCoverReader
    {
        public static async Task<byte[]> ReadBookCoverAsync(EpubBookRef bookRef)
        {
            List<EpubMetadataMeta> metaItems = bookRef.Schema.Package.Metadata.MetaItems;
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
            EpubManifestItem coverManifestItem = bookRef.Schema.Package.Manifest.FirstOrDefault(manifestItem => manifestItem.Id.CompareOrdinalIgnoreCase(coverMetaItem.Content));
            if (coverManifestItem == null)
            {
                throw new Exception($"Incorrect EPUB manifest: item with ID = \"{coverMetaItem.Content}\" is missing.");
            }
            if (!bookRef.Content.Images.TryGetValue(coverManifestItem.Href, out EpubByteContentFileRef coverImageContentFileRef))
            {
                throw new Exception($"Incorrect EPUB manifest: item with href = \"{coverManifestItem.Href}\" is missing.");
            }
            byte[] coverImageContent = await coverImageContentFileRef.ReadContentAsBytesAsync().ConfigureAwait(false);
            return coverImageContent;
        }
    }
}
