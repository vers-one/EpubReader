using System;
using System.Collections.Generic;
using System.Linq;
using VersFx.Formats.Text.Epub.Schema.Navigation;

namespace VersFx.Formats.Text.Epub.Readers
{
    internal static class ChapterReader
    {
        public static List<EpubChapterRef> GetChapters(EpubBookRef bookRef)
        {
            return GetChapters(bookRef, bookRef.Schema.Navigation.NavMap);
        }

        public static List<EpubChapterRef> GetChapters(EpubBookRef bookRef, List<EpubNavigationPoint> navigationPoints)
        {
            List<EpubChapterRef> result = new List<EpubChapterRef>();
            foreach (EpubNavigationPoint navigationPoint in navigationPoints)
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
                EpubTextContentFileRef htmlContentFileRef;
                if (!bookRef.Content.Html.TryGetValue(contentFileName, out htmlContentFileRef))
                    throw new Exception(String.Format("Incorrect EPUB manifest: item with href = \"{0}\" is missing.", contentFileName));
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
