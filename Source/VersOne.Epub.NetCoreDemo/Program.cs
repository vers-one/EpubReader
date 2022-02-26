using System;
using System.IO;

namespace VersOne.Epub.NetCoreDemo
{
    internal static class Program
    {
        private static void Main()
        {
            char input = '\0';
            while (input != 'Q')
            {
                Console.WriteLine("Select example:");
                Console.WriteLine("1. Print book navigation tree (table of contents)");
                Console.WriteLine("2. Extract plain text from the whole book");
                Console.WriteLine("3. Test the library by reading all EPUB files from a directory");
                Console.WriteLine("Q. Exit");
                input = Char.ToUpper(Console.ReadKey(true).KeyChar);
                Console.WriteLine();
                switch (input)
                {
                    case '1':
                        RunFileExample(PrintNavigation.Run);
                        break;
                    case '2':
                        RunFileExample(ExtractPlainText.Run);
                        break;
                    case '3':
                        RunDirectoryExample(TestDirectory.Run);
                        break;
                    case 'Q':
                        break;
                    default:
                        Console.WriteLine("Input is not recognized. Please try again.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        private static void RunFileExample(Action<string> example)
        {
            Console.Write("Enter the path to the EPUB file: ");
            string filePath = Console.ReadLine();
            Console.WriteLine();
            if (File.Exists(filePath) && Path.GetExtension(filePath).ToLower() == ".epub")
            {
                try
                {
                    example(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception was thrown:");
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist.");
                Console.WriteLine();
            }
        }

        private static void RunDirectoryExample(Action<string> example)
        {
            Console.Write("Enter the path to the directory with EPUB files: ");
            string directoryPath = Console.ReadLine();
            Console.WriteLine();
            if (Directory.Exists(directoryPath))
            {
                try
                {
                    example(directoryPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception was thrown:");
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Directory doesn't exist.");
                Console.WriteLine();
            }
        }
    }
}
