using System;
using System.Collections.Generic;
using System.Linq;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal static class ChapterReader
    {
        public static List<EpubChapterRef> GetChapters(EpubBookRef bookRef)
        {
            return GetChapters(bookRef, bookRef.Schema.Navigation.NavMap);
        }

        public static List<EpubChapterRef> GetChapters(EpubBookRef bookRef, List<Epub2NcxNavigationPoint> navigationPoints)
        {
            List<EpubChapterRef> result = new List<EpubChapterRef>();
            foreach (Epub2NcxNavigationPoint navigationPoint in navigationPoints)
            {
                string contentFileName;
                string anchor;
                int contentSourceAnchorCharIndex = navigationPoint.Content.Source.IndexOf('#');
                if (contentSourceAnchorCharIndex == -1)
                {
                    contentFileName = navigationPoint.Content.Source;
                    anchor = null;
                }
                else
                {
                    contentFileName = navigationPoint.Content.Source.Substring(0, contentSourceAnchorCharIndex);
                    anchor = navigationPoint.Content.Source.Substring(contentSourceAnchorCharIndex + 1);
                }
                if (!bookRef.Content.Html.TryGetValue(contentFileName, out EpubTextContentFileRef htmlContentFileRef))
                {
                    throw new Exception(String.Format("Incorrect EPUB manifest: item with href = \"{0}\" is missing.", contentFileName));
                }
                EpubChapterRef chapterRef = new EpubChapterRef(htmlContentFileRef);
                chapterRef.ContentFileName = contentFileName;
                chapterRef.Anchor = anchor;
                chapterRef.Title = navigationPoint.NavigationLabels.First().Text;
                chapterRef.SubChapters = GetChapters(bookRef, navigationPoint.ChildNavigationPoints);
                result.Add(chapterRef);
            }
            return result;
        }
    }
}
