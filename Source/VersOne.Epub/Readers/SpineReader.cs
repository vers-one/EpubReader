using System;
using System.Collections.Generic;
using System.Linq;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal static class SpineReader
    {
        public static List<EpubTextContentFileRef> GetReadingOrder(EpubBookRef bookRef)
        {
            List<EpubTextContentFileRef> result = new List<EpubTextContentFileRef>();
            foreach (EpubSpineItemRef spineItemRef in bookRef.Schema.Package.Spine)
            {
                EpubManifestItem manifestItem = bookRef.Schema.Package.Manifest.FirstOrDefault(item => item.Id == spineItemRef.IdRef);
                if (manifestItem == null)
                {
                    throw new Exception($"Incorrect EPUB spine: item with IdRef = \"{spineItemRef.IdRef}\" is missing in the manifest.");
                }
                if (!bookRef.Content.Html.TryGetValue(manifestItem.Href, out EpubTextContentFileRef htmlContentFileRef))
                {
                    throw new Exception($"Incorrect EPUB manifest: item with href = \"{spineItemRef.IdRef}\" is missing in the book.");
                }
                result.Add(htmlContentFileRef);
            }
            return result;
        }
    }
}
