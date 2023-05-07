# Getting Started

## 1. Create an app

In your command prompt, run the following command to create a .NET console app:

```cmd
dotnet new console -o MyApp
```

Then, navigate to the new directory created by the previous command:

```cmd
cd MyApp
```

## 2. Install Nuget packages

Install EpubReader package:

```cmd
dotnet add package VersOne.Epub
```

Also install HtmlAgilityPack package:

```cmd
dotnet add package HtmlAgilityPack
```

HtmlAgilityPack will be used to extract the plain text from the HTML files of the book.

## 3. Edit the code

Open the *Program.cs* file in the editor of your choice and replace the content of the file with the following:

```csharp
using System.Text;
using VersOne.Epub;
using HtmlAgilityPack;

// Load the book into memory
EpubBook book = EpubReader.ReadBook("test.epub");

// Print the title and the author of the book
Console.WriteLine($"Title: {book.Title}");
Console.WriteLine($"Author: {book.Author}");
Console.WriteLine();

// Print the table of contents
Console.WriteLine("TABLE OF CONTENTS:");
PrintTableOfContents();
Console.WriteLine();

// Print the text content of all chapters in the book
Console.WriteLine("CHAPTERS:");
PrintChapters();

void PrintTableOfContents()
{
    foreach (EpubNavigationItem navigationItem in book.Navigation)
    {
        PrintNavigationItem(navigationItem, 0);
    }
}

void PrintNavigationItem(EpubNavigationItem navigationItem, int identLevel)
{
    Console.Write(new string(' ', identLevel * 2));
    Console.WriteLine(navigationItem.Title);
    foreach (EpubNavigationItem nestedNavigationItem in navigationItem.NestedItems)
    {
        PrintNavigationItem(nestedNavigationItem, identLevel + 1);
    }
}

void PrintChapters()
{
    foreach (EpubTextContentFile textContentFile in book.ReadingOrder)
    {
        PrintTextContentFile(textContentFile);
    }
}

void PrintTextContentFile(EpubTextContentFile textContentFile)
{
    HtmlDocument htmlDocument = new();
    htmlDocument.LoadHtml(textContentFile.Content);
    StringBuilder sb = new();
    foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//text()"))
    {
        sb.AppendLine(node.InnerText.Trim());
    }
    string contentText = sb.ToString();
    Console.WriteLine(contentText);
    Console.WriteLine();
}
```

## 4. Run the app

Copy any EPUB file into the *MyApp* directory and rename the file to *test.epub*.

Then, run the app using the following command in the command prompt:

```cmd
dotnet run
```

The output should look similar to this:
```text
Title: Alice's Adventures in Wonderland
Author: Lewis Carroll

TABLE OF CONTENTS:
CHAPTER I. Down the Rabbit-Hole
CHAPTER II. The Pool of Tears
CHAPTER III. A Caucus-Race and a Long Tale
CHAPTER IV. The Rabbit Sends in a Little Bill
CHAPTER V. Advice from a Caterpillar
CHAPTER VI. Pig and Pepper
CHAPTER VII. A Mad Tea-Party
CHAPTER VIII. The Queen's Croquet-Ground
CHAPTER IX. The Mock Turtle's Story
CHAPTER X. The Lobster Quadrille
CHAPTER XI. Who Stole the Tarts?
CHAPTER XII. Alice's Evidence

CHAPTERS:
CHAPTER I. Down the Rabbit-Hole

Alice was beginning to get very tired of sitting by her sister on the bank, and of having nothing to do: once or twice she had peeped into the book her sister was reading, but it had no pictures or conversations in it, 'and what is the use of a book,' thought Alice 'without pictures or conversations?'
...
```
