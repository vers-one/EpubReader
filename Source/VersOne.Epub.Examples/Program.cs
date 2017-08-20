using System;
using System.IO;

namespace VersOne.Epub.Examples
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            char input = '\0';
            while (input != 'Q')
            {
                Console.WriteLine("Select example:");
                Console.WriteLine("1. Extract plain text from all chapters");
                Console.WriteLine("Q. Exit");
                input = Char.ToUpper(Console.ReadKey(true).KeyChar);
                switch (input)
                {
                    case '1':
                        RunExample(ExtractPlainText.Run);
                        break;
                    case 'Q':
                        break;
                    default:
                        Console.WriteLine("Input is not recognized. Please try again.");
                        break;
                }
            }
        }

        static void RunExample(Action<string> example)
        {
            Console.Write("Enter the path to the EPUB file: ");
            string filePath = Console.ReadLine();
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
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist.");
            }
        }
    }
}
