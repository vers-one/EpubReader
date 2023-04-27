# Class Reference

## Core classes

|Class Name                                                        |Description                                                               |
|------------------------------------------------------------------|--------------------------------------------------------------------------|
|[`EpubReader`](xref:VersOne.Epub.EpubReader)                      |The main static class that provides methods like [`OpenBook`](xref:VersOne.Epub.EpubReader#VersOne_Epub_EpubReader_OpenBook_System_IO_Stream_VersOne_Epub_Options_EpubReaderOptions_) and [`ReadBook`](xref:VersOne.Epub.EpubReader#VersOne_Epub_EpubReader_ReadBook_System_IO_Stream_VersOne_Epub_Options_EpubReaderOptions_)|
|[`EpubBook`](xref:VersOne.Epub.EpubBook)                          |Represents a EPUB book with all its content and metadata                  |
|[`EpubBookRef`](xref:VersOne.Epub.EpubBookRef)                    |Represents a EPUB book with its metadata but with no content              |
|[`EpubContent`](xref:VersOne.Epub.EpubContent)                    |A container for all content files within the EPUB book                    |
|[`EpubSchema`](xref:VersOne.Epub.EpubSchema)                      |Parsed content of all EPUB schema files                                   |
|[`EpubReaderOptions`](xref:VersOne.Epub.Options.EpubReaderOptions)|Various options to configure the behavior of the EPUB reader              |

## Namespaces

|Namespace                                                  |Description                                                                                            |
|-----------------------------------------------------------|-------------------------------------------------------------------------------------------------------|
|[`VersOne.Epub`](xref:VersOne.Epub)                        |The main namespace containing core classes like [`EpubReader`](xref:VersOne.Epub.EpubReader) and [`EpubBook`](xref:VersOne.Epub.EpubBook)|
|[`VersOne.Epub.Options`](xref:VersOne.Epub.Options)        |Contains classes that configure the behavior of the EPUB reader                                        |
|[`VersOne.Epub.Schema`](xref:VersOne.Epub.Schema)          |Contains classes representing the raw EPUB schema fields                                               |
|[`VersOne.Epub.Environment`](xref:VersOne.Epub.Environment)|Contains interfaces that provide environment abstractions (e.g. file system) to facilitate unit testing|
