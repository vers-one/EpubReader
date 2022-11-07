using System.Collections.Generic;
using System.Linq;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal static class SpineReader
    {
        public static List<EpubLocalTextContentFileRef> GetReadingOrder(EpubBookRef bookRef)
        {
            List<EpubLocalTextContentFileRef> result = new List<EpubLocalTextContentFileRef>();
            foreach (EpubSpineItemRef spineItemRef in bookRef.Schema.Package.Spine.Items)
            {
                EpubManifestItem manifestItem = bookRef.Schema.Package.Manifest.Items.FirstOrDefault(item => item.Id == spineItemRef.IdRef);
                if (manifestItem == null)
                {
                    throw new EpubPackageException($"Incorrect EPUB spine: item with IdRef = \"{spineItemRef.IdRef}\" is missing in the manifest.");
                }
                if (bookRef.Content.Html.Remote.ContainsKey(manifestItem.Href))
                {
                    throw new EpubPackageException($"Incorrect EPUB manifest: EPUB spine item \"{manifestItem.Href}\" cannot be a remote resource.");
                }
                if (!bookRef.Content.Html.Local.TryGetValue(manifestItem.Href, out EpubLocalTextContentFileRef htmlContentFileRef))
                {
                    throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{spineItemRef.IdRef}\" is missing in the book.");
                }
                result.Add(htmlContentFileRef);
            }
            return result;
        }
    }
}
