using System;

namespace VersOne.Epub.NetCoreDemo
{
    internal static class ListChapters
    {
        public static void Run(string filePath)
        {
            EpubBook book = EpubReader.ReadBook(filePath);
            foreach (EpubChapter chapter in book.Chapters)
            {
                PrintChapterTitle(chapter, 0);
            }
        }

        private static void PrintChapterTitle(EpubChapter chapter, int identLevel)
        {
            Console.Write(new string(' ', identLevel * 2));
            Console.WriteLine(chapter.Title);
            foreach (EpubChapter subChapter in chapter.SubChapters)
            {
                PrintChapterTitle(subChapter, identLevel + 1);
            }
        }
    }
}
