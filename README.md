# EpubReader
.NET library for reading EPUB files

## Demo app
[Download (EpubReaderDemo.zip, 399 KB)](https://github.com/versfx/EpubReader/releases/download/v1.0/EpubReaderDemo.zip)

Warning: HTML renderer used in this demo app may be a little bit slow for some books.

## Example
```csharp
// Opening a book
EpubBook epubBook = EpubReader.OpenBook("alice_in_wonderland.epub");

            
// COMMON PROPERTIES

// Book's title
string title = epubBook.Title;

// Book's authors (comma separated list)
string authors = epubBook.Authors;

// Book's cover image (null if there are no cover)
Image coverImage = epubBook.CoverImage;

            
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

// All images in the book (key is file name)
Dictionary<string, EpubByteContentFile> images = bookContent.Images;

EpubByteContentFile firstImage = images.Values.First();

// Content type (e.g. EpubContentType.IMAGE_JPEG, EpubContentType.IMAGE_PNG)
EpubContentType contentType = firstImage.ContentType;

// MIME type (e.g. "image/jpeg", "image/png")
string mimeContentType = firstImage.ContentMimeType;

// Creating Image class instance from content
using (MemoryStream imageStream = new MemoryStream(firstImage.Content))
{
    Image image = Image.FromStream(imageStream);
}


// HTML & CSS

// All XHTML files in the book (key is file name)
Dictionary<string, EpubTextContentFile> htmlFiles = bookContent.Html;

// All CSS files in the book (key is file name)
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

// All fonts in the book (key is file name)
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
