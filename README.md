# EpubReader
.NET library for reading EPUB files.

Supports .NET Standard >= 1.3 and .NET Framework >= 4.6.

Supports EPUB 2 (2.0, 2.0.1) and EPUB 3 (3.0, 3.0.1, 3.1, 3.2).

[Download](#download-latest-stable-release) | [WPF & .NET Core demo apps](#demo-apps)

## Example
```csharp
// Opens a book and reads all of its content into memory
EpubBook epubBook = EpubReader.ReadBook("alice_in_wonderland.epub");

            
// COMMON PROPERTIES

// Book's title
string title = epubBook.Title;

// Book's authors (comma separated list)
string author = epubBook.Author;

// Book's authors (list of authors names)
List<string> authors = epubBook.AuthorList;

// Book's cover image (null if there is no cover)
byte[] coverImageContent = epubBook.CoverImage;
if (coverImageContent != null)
{
    using (MemoryStream coverImageStream = new MemoryStream(coverImageContent))
    {
        Image coverImage = Image.FromStream(coverImageStream);
    }
}
            
// TABLE OF CONTENTS

// Enumerating chapters
foreach (EpubNavigationItem chapter in epubBook.Navigation)
{
    // Title of chapter
    string chapterTitle = chapter.Title;
                
    // Nested chapters
    List<EpubNavigationItem> subChapters = chapter.NestedItems;
}

// READING ORDER

// Enumerating the whole text content of the book in the order of reading
foreach (EpubTextContentFile textContentFile in book.ReadingOrder)
{
    // HTML of current text content file
    string htmlContent = textContentFile.Content;
}

            
// CONTENT

// Book's content (HTML files, stlylesheets, images, fonts, etc.)
EpubContent bookContent = epubBook.Content;

            
// IMAGES

// All images in the book (file name is the key)
Dictionary<string, EpubByteContentFile> images = bookContent.Images;

EpubByteContentFile firstImage = images.Values.First();

// Content type (e.g. EpubContentType.IMAGE_JPEG, EpubContentType.IMAGE_PNG)
EpubContentType contentType = firstImage.ContentType;

// MIME type (e.g. "image/jpeg", "image/png")
string mimeType = firstImage.ContentMimeType;

// Creating Image class instance from the content
using (MemoryStream imageStream = new MemoryStream(firstImage.Content))
{
    Image image = Image.FromStream(imageStream);
}

// Cover metadata
if (bookContent.Cover != null)
{
    string coverFileName = bookContent.Cover.FileName;
    EpubContentType coverContentType = bookContent.Cover.ContentType;
    string coverMimeType = bookContent.Cover.ContentMimeType;
}

// HTML & CSS

// All XHTML files in the book (file name is the key)
Dictionary<string, EpubTextContentFile> htmlFiles = bookContent.Html;

// All CSS files in the book (file name is the key)
Dictionary<string, EpubTextContentFile> cssFiles = bookContent.Css;

// Entire HTML content of the book
foreach (EpubTextContentFile htmlFile in htmlFiles.Values)
{
    string htmlContent = htmlFile.Content;
}

// All CSS content in the book
foreach (EpubTextContentFile cssFile in cssFiles.Values)
{
    string cssContent = cssFile.Content;
}


// OTHER CONTENT

// All fonts in the book (file name is the key)
Dictionary<string, EpubByteContentFile> fonts = bookContent.Fonts;

// All files in the book (including HTML, CSS, images, fonts, and other types of files)
Dictionary<string, EpubContentFile> allFiles = bookContent.AllFiles;


// ACCESSING RAW SCHEMA INFORMATION

// EPUB OPF data
EpubPackage package = epubBook.Schema.Package;

// Enumerating book's contributors
foreach (EpubMetadataContributor contributor in package.Metadata.Contributors)
{
    string contributorName = contributor.Contributor;
    string contributorRole = contributor.Role;
}

// EPUB 2 NCX data
Epub2Ncx epub2Ncx = epubBook.Schema.Epub2Ncx;

// Enumerating EPUB 2 NCX metadata
foreach (Epub2NcxHeadMeta meta in epub2Ncx.Head)
{
    string metadataItemName = meta.Name;
    string metadataItemContent = meta.Content;
}

// EPUB 3 navigation
Epub3NavDocument epub3NavDocument = epubBook.Schema.Epub3NavDocument;

// Accessing structural semantics data of the head item
StructuralSemanticsProperty? ssp = epub3NavDocument.Navs.First().Type;
```

## More examples

1. [How to extract the plain text of the whole book.](https://github.com/vers-one/EpubReader/tree/master/Source/VersOne.Epub.ConsoleDemo/ExtractPlainText.cs)
2. [How to extract the table of contents.](https://github.com/vers-one/EpubReader/tree/master/Source/VersOne.Epub.ConsoleDemo/PrintNavigation.cs)
3. [How to iterate over all EPUB files in a directory and collect some statistics.](https://github.com/vers-one/EpubReader/tree/master/Source/VersOne.Epub.ConsoleDemo/TestDirectory.cs)

## Download latest stable release
[Via NuGet package from nuget.org](https://www.nuget.org/packages/VersOne.Epub)

DLL file from GitHub: [for .NET Framework](https://github.com/vers-one/EpubReader/releases/latest/download/VersOne.Epub.Net46.zip) (38.3 KB) / [for .NET Core](https://github.com/vers-one/EpubReader/releases/latest/download/VersOne.Epub.NetCore.zip) (38.4 KB) / [for .NET Standard](https://github.com/vers-one/EpubReader/releases/latest/download/VersOne.Epub.NetStandard.zip) (38.4 KB)

## Demo apps
[Download WPF demo app](https://github.com/vers-one/EpubReader/releases/latest/download/WpfDemo.zip) (WpfDemo.zip, 479 KB)

This .NET Framework application demonstrates how to open EPUB books and extract their content using the library.

HTML renderer used in this demo app may have difficulties while rendering HTML content for some of the books if the HTML structure is too complicated.

[Download .NET Core console demo app](https://github.com/vers-one/EpubReader/releases/latest/download/NetCoreDemo.zip) (NetCoreDemo.zip, 17.6 MB)

This .NET Core console application demonstrates how to open EPUB books and retrieve their text content.
