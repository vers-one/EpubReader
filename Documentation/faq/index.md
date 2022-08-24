# Frequently Asked Questions

## How to write EPUB files?

EpubReader is only for reading EPUB files. It doesn't have a support to write them.

## How to display a EPUB book in my application?

EpubReader allows to parse and extract the content of a EPUB book but it doesn't have any means to render the extracted content. Since a EPUB book is essentially just a collection of HTML files (along with CSS, images, fonts, etc), you need a way to render these HTML files in the same way a web browser does.

[WPF demo app](https://github.com/vers-one/EpubReader/tree/master/Source/VersOne.Epub.WpfDemo/) contains an example of how to render a EPUB book in a WPF application but the third-party HTML renderer used in this demo app is far from perfect and may have difficulties while rendering the content if the HTML structure is too complicated.

## How to convert EPUB to PDF?

EPUB and PDF are two very different formats. EPUB is a flow-layout format which means that it usually doesn't have page breaks and can be easily scaled to occupy the whole screen of the device regardless of its size and aspect ratio. PDF on the other hand is a fixed-layout paged-based format, i.e. it determines how the whole page should look like. For example, if you increase the font size in your EPUB reader, you will be reducing the amount of text that can fit on the "page" of the reader and the number of "pages" will go up. If you scale the content of a PDF book, the number of pages will stay the same but the content of the current page may not fit into the screen of the reader.

Due to all this, there is no straightforward way to convert a EPUB book to PDF. In order to do that, you'll need to [render the EPUB book first](#how-to-display-a-epub-book-in-my-application) (allowing the end user to customize the page size, styles, fonts, font sizes, margins, table of contents' rendering options and so on) and then write the rendered content as a PDF file.

## What are the differences between `EpubBook` and `EpubBookRef` classes?

[`EpubBook`](xref:VersOne.Epub.EpubBook) is returned by [`EpubReader.ReadBook`](xref:VersOne.Epub.EpubReader#VersOne_Epub_EpubReader_ReadBook_System_IO_Stream_VersOne_Epub_Options_EpubReaderOptions_) and [`EpubReader.ReadBookAsync`](xref:VersOne.Epub.EpubReader#VersOne_Epub_EpubReader_ReadBookAsync_System_IO_Stream_VersOne_Epub_Options_EpubReaderOptions_) methods. [`EpubBookRef`](xref:VersOne.Epub.EpubBookRef) is returned by [`EpubReader.OpenBook`](xref:VersOne.Epub.EpubReader#VersOne_Epub_EpubReader_OpenBook_System_IO_Stream_VersOne_Epub_Options_EpubReaderOptions_) and [`EpubReader.OpenBookAsync`](xref:VersOne.Epub.EpubReader#VersOne_Epub_EpubReader_OpenBookAsync_System_IO_Stream_VersOne_Epub_Options_EpubReaderOptions_) methods.

Both `EpubBook` and `EpubBookRef` classes contain parsed metadata of the EPUB book. However there are two main differences between them:
1. Book content:
    * `EpubBook` has all the content of the book (HTML, CSS, images and other files) loaded into the memory.
    * `EpubBookRef` doesn't have the content loaded. Instead it lets the consumer to load individual content files on demand.
2. File handle:
    * `EpubBook`, once it's created, doesn't keep the EPUB file open, which means that the file can be changed or even deleted.
    * `EpubBookRef` contains a reference to the EPUB file (to be able to load content files on demand). Instances of this class need to be disposed (either by calling the `Dispose` method or by enclosing it into a `using()` block) to release the file handle.

In the end, you should use `EpubBookRef` if you don't want the whole content of the file to be loaded into memory (for example, you are concerned about the memory consumption or the time it takes to load the whole content). Conversely, you should use `EpubBook` if you don't want the EPUB file to remain open for the whole lifetime of the `EpubBookRef` instance.

