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
                if (bookRef.Content.Html.TryGetValue(manifestItem.Href, out EpubTextContentFileRef htmlContentFileRef))
                {
                    result.Add(htmlContentFileRef);
                    continue;
                }

                // 2019-08-21 Fix: some ebooks seem to contain two items with id="cover", one of them is an image, and the other an XHTML file
                // thus, if the first attempt to get the HTML item fails, we try for a second item with the same Id
                manifestItem = bookRef.Schema.Package.Manifest.Where(item => item.Id == spineItemRef.IdRef).Skip(1).FirstOrDefault();
                if (manifestItem == null)
                {
                    throw new Exception($"Incorrect EPUB spine: item with IdRef = \"{spineItemRef.IdRef}\" is not HTML content");
                }
                if (bookRef.Content.Html.TryGetValue(manifestItem.Href, out EpubTextContentFileRef htmlContentFileRef2))
                {
                    result.Add(htmlContentFileRef2);
                    continue;
                }
                throw new Exception($"Incorrect EPUB manifest: item with href = \"{spineItemRef.IdRef}\" is missing in the book.");
            }
            return result;
        }
    }
}
