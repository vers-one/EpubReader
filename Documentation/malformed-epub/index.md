# Handling Malformed EPUB files

EpubReader has a few configuration options to handle the most common cases of malformed EPUB files (i.e. the files that deviate from the EPUB specification). This can be done by creating an instance of the [`EpubReaderOptions`](xref:VersOne.Epub.Options.EpubReaderOptions) class, setting the appropriate properties (see the sections below for examples), and passing it to one of the methods of the [`EpubReader`](xref:VersOne.Epub.EpubReader) class.

## Missing TOC attribute in EPUB 2 spine

The `spine` element of the EPUB manifest contains the `toc` attribute which is [not required](https://www.w3.org/publishing/epub32/epub-packages.html#sec-pkg-spine) for EPUB 3 books but is [required](https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4) for EPUB 2 books. There are [some EPUB 2 books](https://github.com/vers-one/EpubReader/issues/41) that have the `toc` attribute missing which causes EpubReader to throw the *"Incorrect EPUB spine: TOC is missing"* exception.

[`PackageReaderOptions.IgnoreMissingToc`](xref:VersOne.Epub.Options.PackageReaderOptions#VersOne_Epub_Options_PackageReaderOptions_IgnoreMissingToc) property can be used to instruct EpubReader to ignore this error:

```csharp
EpubReaderOptions options = new()
{
    PackageReaderOptions = new PackageReaderOptions()
    {
        IgnoreMissingToc = true
    }
};
```

## Invalid EPUB manifest items

The [`item` element](https://www.w3.org/publishing/epub32/epub-packages.html#sec-item-elem) within the EPUB manifest has three required attributes: `id`, `href`, and `media-type`. There are [some EPUB books](https://github.com/vers-one/EpubReader/issues/47) that have at least one of those three attributes missing which causes EpubReader to throw the *"Incorrect EPUB manifest: item ... is missing"* exception.

[`PackageReaderOptions.SkipInvalidManifestItems`](xref:VersOne.Epub.Options.PackageReaderOptions#VersOne_Epub_Options_PackageReaderOptions_SkipInvalidManifestItems) property can be used to instruct EpubReader to ignore this error:

```csharp
EpubReaderOptions options = new()
{
    PackageReaderOptions = new PackageReaderOptions()
    {
        SkipInvalidManifestItems = true
    }
};
```

## Missing content attribute for EPUB 2 NCX navigation points

The `navPoint` element within the [EPUB 2 NCX navigation document](https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX) must contain a nested `content` element pointing to a content file associated with this navigation item. There are some EPUB 2 books that have navigation points without a nested `content` element which causes EpubReader to throw the *"EPUB parsing error: navigation point X should contain content"* exception.

[`Epub2NcxReaderOptions.IgnoreMissingContentForNavigationPoints`](xref:VersOne.Epub.Options.Epub2NcxReaderOptions#VersOne_Epub_Options_Epub2NcxReaderOptions_IgnoreMissingContentForNavigationPoints) property can be used to instruct EpubReader to skip such navigation points (as well as all their child navigation points):

```csharp
EpubReaderOptions options = new()
{
    Epub2NcxReaderOptions = new Epub2NcxReaderOptions()
    {
        IgnoreMissingContentForNavigationPoints = true
    }
};
```

## Handling XML 1.1 schema files

.NET [doesn't have](https://stackoverflow.com/questions/17231675/does-net-4-5-support-xml-1-1-yet-for-characters-invalid-in-xml-1-0) a built-in support for XML 1.1 files (only XML 1.0 files are currently supported). There are [some EPUB books](https://github.com/vers-one/EpubReader/issues/34) that have at least one of their schema files (typically the OPF package file) saved in XML 1.1 format, even though they don't use any XML 1.1 features. This causes EpubReader to throw an `XmlException` with the *"Version number '1.1' is invalid"* message.

[`XmlReaderOptions.SkipXmlHeaders`](xref:VersOne.Epub.Options.XmlReaderOptions#VersOne_Epub_Options_XmlReaderOptions_SkipXmlHeaders) property can be used to enable a workaround for handling XML 1.1 files in EpubReader:

```csharp
EpubReaderOptions options = new()
{
    XmlReaderOptions = new XmlReaderOptions()
    {
        SkipXmlHeaders = true
    }
};
```

> [!Important]
> Keep in mind that enabling this workaround adds an additional overhead for processing all schema files within EPUB book. If this property is set to `true`, EpubReader will check if an XML file contains a declaration (`<?xml version="..." encoding="UTF-8"?>`) in which case the reader will skip it before passing the file to the underlying .NET `XDocument` class.
