using System;

namespace VersOne.Epub.ConsoleDemo
{
    internal static class PrintNavigation
    {
        public static void Run(string filePath)
        {
            using (EpubBookRef bookRef = EpubReader.OpenBook(filePath))
            {
                Console.WriteLine("Navigation:");
                foreach (EpubNavigationItemRef navigationItemRef in bookRef.GetNavigation())
                {
                    PrintNavigationItem(navigationItemRef, 0);
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
