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
            foreach (EpubTextContentFile textContentFile in book.ReadingOrder)
            {
                PrintTextContentFile(textContentFile);
            }
        }

        private static void PrintTextContentFile(EpubTextContentFile textContentFile)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(textContentFile.Content);
            StringBuilder sb = new StringBuilder();
            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//text()"))
            {
                sb.AppendLine(node.InnerText.Trim());
            }
            string contentText = sb.ToString();
            Console.WriteLine(contentText);
            Console.WriteLine();
        }
    }
}