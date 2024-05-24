using System.Collections.Generic;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal static class SpineReader
    {
        public static List<EpubLocalTextContentFileRef> GetReadingOrder(EpubSchema epubSchema, EpubContentRef epubContentRef)
        {
            List<EpubLocalTextContentFileRef> result = new();
            foreach (EpubSpineItemRef spineItemRef in epubSchema.Package.Spine.Items)
            {
                EpubManifestItem manifestItem = epubSchema.Package.Manifest.Items.Find(item => item.Id == spineItemRef.IdRef) ??
                    throw new EpubPackageException($"Incorrect EPUB spine: item with IdRef = \"{spineItemRef.IdRef}\" is missing in the manifest.");
                if (epubContentRef.Html.ContainsRemoteFileRefWithUrl(manifestItem.Href))
                {
                    throw new EpubPackageException($"Incorrect EPUB manifest: EPUB spine item \"{manifestItem.Href}\" cannot be a remote resource.");
                }
                if (!epubContentRef.Html.TryGetLocalFileRefByKey(manifestItem.Href, out EpubLocalTextContentFileRef htmlContentFileRef))
                {
                    throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{spineItemRef.IdRef}\" is missing in the book.");
                }
                result.Add(htmlContentFileRef);
            }
            return result;
        }
    }
}
