using System;
using System.Text;
using HtmlAgilityPack;

namespace VersOne.Epub.NetCoreDemo
{
    internal static class ExtractPlainText
    {
        public static void Run(string filePath)
        {
            EpubBook book = EpubReader.ReadBook(filePath);
            foreach (EpubChapter chapter in book.Chapters)
            {
                PrintChapter(chapter);
            }
        }

        private static void PrintChapter(EpubChapter chapter)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(chapter.HtmlContent);
            StringBuilder sb = new StringBuilder();
            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//text()"))
            {
                sb.AppendLine(node.InnerText.Trim());
            }
            string chapterTitle = chapter.Title;
            string chapterText = sb.ToString();
            Console.WriteLine("------------ ", chapterTitle, "------------ ");
            Console.WriteLine(chapterText);
            Console.WriteLine();
            foreach (EpubChapter subChapter in chapter.SubChapters)
            {
                PrintChapter(subChapter);
            }
        }
    }
}