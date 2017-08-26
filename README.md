# EpubReader
.NET library for reading EPUB files.

Supports .NET Standard 2.0 (.NET Framework >= 4.6.1 and .NET Core >= 2.0).

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
            
// CHAPTERS

// Enumerating chapters
foreach (EpubChapter chapter in epubBook.Chapters)
{
    // Title of chapter
    string chapterTitle = chapter.Title;
                
    // HTML content of current chapter
    string chapterHtmlContent = chapter.HtmlContent;

    // Nested chapters
    List<EpubChapter> subChapters = chapter.SubChapters;
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
string mimeContentType = firstImage.ContentMimeType;

// Creating Image class instance from the content
using (MemoryStream imageStream = new MemoryStream(firstImage.Content))
{
    Image image = Image.FromStream(imageStream);
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

// EPUB NCX data
EpubNavigation navigation = epubBook.Schema.Navigation;

// Enumerating NCX metadata
foreach (EpubNavigationHeadMeta meta in navigation.Head)
{
    string metadataItemName = meta.Name;
    string metadataItemContent = meta.Content;
}
```

## More examples
[How to extract plain text from all chapters.](https://github.com/versfx/EpubReader/tree/master/Source/VersOne.Epub.NetCoreDemo/ExtractPlainText.cs)

## Download latest stable release
[Via NuGet package from nuget.org](https://www.nuget.org/packages/VersOne.Epub)

[DLL file from GitHub](https://github.com/vers-one/EpubReader/releases/download/v2.0.1/VersOne.Epub.dll) (VersOne.Epub.dll, 65.5 KB)

## Demo apps
[Download WPF demo app ](https://github.com/vers-one/EpubReader/releases/download/v2.0.1/WpfDemo.zip) (WpfDemo.zip, 442 KB)

This .NET Framework application demonstrates how to open EPUB books and extract their content using the library.

HTML renderer used in this demo app may be a little bit slow for some books.

[Download .NET Core console demo app](https://github.com/vers-one/EpubReader/releases/download/v2.0.1/NetCoreDemo.zip) (NetCoreDemo.zip, 26.7 MB)

This .NET Core console application demonstrates how to open EPUB books and retrieve their text content.
