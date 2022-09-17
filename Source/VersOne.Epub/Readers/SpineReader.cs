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
            foreach (EpubSpineItemRef spineItemRef in bookRef.Schema.Package.Spine.Items)
            {
                EpubManifestItem manifestItem = bookRef.Schema.Package.Manifest.Items.FirstOrDefault(item => item.Id == spineItemRef.IdRef);
                if (manifestItem == null)
                {
                    throw new EpubPackageException($"Incorrect EPUB spine: item with IdRef = \"{spineItemRef.IdRef}\" is missing in the manifest.");
                }
                if (!bookRef.Content.Html.TryGetValue(manifestItem.Href, out EpubTextContentFileRef htmlContentFileRef))
                {
                    throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{spineItemRef.IdRef}\" is missing in the book.");
                }
                result.Add(htmlContentFileRef);
            }
            return result;
        }
    }
}
