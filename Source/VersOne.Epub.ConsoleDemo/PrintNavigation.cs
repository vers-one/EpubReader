using System;
using System.Collections.Generic;

namespace VersOne.Epub.ConsoleDemo
{
    internal static class PrintNavigation
    {
        public static void Run(string filePath)
        {
            using (EpubBookRef bookRef = EpubReader.OpenBook(filePath))
            {
                Console.WriteLine("Navigation:");
                List<EpubNavigationItemRef>? navigation = bookRef.GetNavigation();
                if (navigation != null)
                {
                    foreach (EpubNavigationItemRef navigationItemRef in navigation)
                    {
                        PrintNavigationItem(navigationItemRef, 0);
                    }
                }
            }
            Console.WriteLine();
        }

        private static void PrintNavigationItem(EpubNavigationItemRef navigationItemRef, int identLevel)
        {
            Console.Write(new string(' ', identLevel * 2));
            Console.WriteLine(navigationItemRef.Title);
            foreach (EpubNavigationItemRef nestedNavigationItemRef in navigationItemRef.NestedItems)
            {
                PrintNavigationItem(nestedNavigationItemRef, identLevel + 1);
            }
        }
    }
}
