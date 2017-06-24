# ePub Reader

A .NET Standard library for reading ePub files.

**NOTE:** This is an extensively modified fork of [versfx's EpubReader](https://github.com/versfx/EpubReader).
This fork aims to be a cross-platform and modern library, using latest technologies such as .NET Standard.

## Example

**NOTE:** The examples below may not be up to date, but will be fixed as this fork matures and is deemed stable for production use.

```csharp
// Opens a book and reads all of its content into the memory
EpubBook epubBook = EpubReader.ReadBook("alice_in_wonderland.epub");

            
// COMMON PROPERTIES

// Book's title
string title = epubBook.Title;

// Book's authors (comma separated list)
string author = epubBook.Author;

// Book's authors (list of authors names)
List<string> authors = epubBook.AuthorList;

// Book's cover image (null if there is no cover)
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