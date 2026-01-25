using System;
using System.Xml;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB package reader.
    /// </summary>
    public class PackageReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="PackageReaderOptions" /> class with a predefined set of options.</param>
        public PackageReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            switch (preset)
            {
                case EpubReaderOptionsPreset.RELAXED:
                    FallbackEpubVersion = EpubVersion.EPUB_2;
                    IgnoreMissingToc = true;
                    break;
                case EpubReaderOptionsPreset.IGNORE_ALL_ERRORS:
                    IgnoreMissingPackageFile = true;
                    IgnorePackageFileIsNotValidXmlError = true;
                    IgnoreMissingPackageNode = true;
                    FallbackEpubVersion = EpubVersion.EPUB_2;
                    IgnoreMissingMetadataNode = true;
                    IgnoreMissingManifestNode = true;
                    IgnoreMissingSpineNode = true;
                    IgnoreMissingToc = true;
                    SkipInvalidManifestItems = true;
                    SkipDuplicateManifestItemIds = true;
                    SkipDuplicateManifestHrefs = true;
                    SkipInvalidSpineItems = true;
                    SkipInvalidGuideReferences = true;
                    SkipInvalidCollections = true;
                    break;
            }
        }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should ignore the missing OPF package file error.
        /// </para>
        /// <para>
        /// The path to the OPF package file is defined by the 'rootfile' element in the 'META-INF/container.xml' file.
        /// If this property is set to <c>false</c> and the OPF package file is not present,
        /// then the "EPUB parsing error: OPF package file not found in the EPUB file." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since the OPF package is the main file
        /// describing the EPUB book, the <see cref="EpubReader" /> class methods will return <c>null</c> in this case.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingPackageFile { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should ignore the error when the package file is not a valid XML file.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and an XML parsing error has occurred while trying to open the OPF package file,
        /// then the "EPUB parsing error: package file is not a valid XML file." exception will be thrown
        /// with the original <see cref="XmlException" /> available through the <see cref="Exception.InnerException" /> property.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since the OPF package is the main file
        /// describing the EPUB book, the <see cref="EpubReader" /> class methods will return <c>null</c> in this case.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnorePackageFileIsNotValidXmlError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should ignore the missing 'package' XML element in the OPF package file.
        /// </para>
        /// <para>
        /// If this property is set to <c>false</c> and the 'package' XML element is not present,
        /// then the "EPUB parsing error: package XML element not found in the package file." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since 'package' is the top-level XML element
        /// in the OPF package file, the <see cref="EpubReader" /> class methods will return <c>null</c> in this case.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingPackageNode { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should use a fallback EPUB version when the 'version' attribute
        /// of the 'package' XML element in the OPF package file is missing or when the attribute's value is set to an unsupported EPUB version.
        /// </para>
        /// <para>
        /// If this property is set to <c>null</c> and the 'version' attribute is not present,
        /// then the "EPUB parsing error: EPUB version is not specified in the package." exception will be thrown.
        /// Alternatively, if the attribute's value is set to an unsupported EPUB version,
        /// then the "Unsupported EPUB version: ..." exception will be thrown.
        /// Those exceptions can be suppressed by setting this property to an explicit fallback EPUB version (e.g. <see cref="EpubVersion.EPUB_3" />),
        /// in which case the rest of the EPUB schema will be parsed according to that EPUB version specification.
        /// </para>
        /// <para>
        /// Default value is <c>null</c>.
        /// </para>
        /// </summary>
        public EpubVersion? FallbackEpubVersion { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should ignore the missing 'metadata' XML element in the OPF package file.
        /// </para>
        /// <para>
        /// If this property is set to <c>false</c> and the 'metadata' XML element is not present,
        /// then the "EPUB parsing error: metadata not found in the package." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case all properties of the <see cref="EpubMetadata" />
        /// object will be set to the default / empty values. Common metadata fields include the book's titles, the authors, and the descriptions,
        /// so even if this exception is suppressed, the <see cref="EpubBook.Title" />, the <see cref="EpubBook.Author" />,
        /// and the <see cref="EpubBook.Description" /> properties will be empty.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingMetadataNode { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should ignore the missing 'manifest' XML element in the OPF package file.
        /// </para>
        /// <para>
        /// If this property is set to <c>false</c> and the 'manifest' XML element is not present,
        /// then the "EPUB parsing error: manifest not found in the package." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case all properties of the <see cref="EpubManifest" />
        /// object will be set to the default / empty values. The EPUB manifest contains the list of content files within the EPUB book
        /// (HTML and CSS files, images, fonts, and so on), so even if this exception is suppressed,
        /// the EPUB book returned by the <see cref="EpubReader" /> will have no content.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingManifestNode { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should ignore the missing 'spine' XML element in the OPF package file.
        /// </para>
        /// <para>
        /// If this property is set to <c>false</c> and the 'spine' XML element is not present,
        /// then the "EPUB parsing error: spine not found in the package." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case all properties of the <see cref="EpubSpine" />
        /// object will be set to the default / empty values. The EPUB spine determines the reading order within the EPUB book
        /// so even if this exception is suppressed, the <see cref="EpubBook.ReadingOrder" /> property will be empty.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingSpineNode { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should ignore the missing TOC attribute in the EPUB 2 spine.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the TOC attribute is not present, then the "Incorrect EPUB spine: TOC is missing." exception will be thrown.
        /// This property has no effect if the EPUB version of the book is not <see cref="EpubVersion.EPUB_2" /> or if the 'spine' XML element
        /// is missing in the OPF package file and the <see cref="IgnoreMissingSpineNode" /> property is set to <c>true</c>.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingToc { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should skip EPUB manifest items that are missing required attributes
        /// ('id', 'href', or 'media-type').
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and one of the required attributes is not present,
        /// then one of the "Incorrect EPUB manifest: item ... is missing." exceptions will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the items with the missing attributes will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipInvalidManifestItems { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should skip EPUB manifest items that have duplicate ID values.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and an item with a duplicate ID was found,
        /// then the "Incorrect EPUB manifest: item with ID = ... is not unique." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case only the first item within each duplicate item set
        /// will be added to the <see cref="EpubManifest.Items" /> collection and the others will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipDuplicateManifestItemIds { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should skip EPUB manifest items that have duplicate 'href' attribute values.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and an item with a duplicate href value was found,
        /// then the "Incorrect EPUB manifest: item with href = ... is not unique." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case only the first item within each duplicate item set
        /// will be added to the <see cref="EpubManifest.Items" /> collection and the others will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipDuplicateManifestHrefs { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should skip EPUB spine items that are missing the required 'idref' attribute.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the required attribute is not present,
        /// then the "Incorrect EPUB spine: item ID ref is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the items with the missing 'idref' attribute will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipInvalidSpineItems { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should skip EPUB guide items that are missing required attributes ('type' and 'href').
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and one of the required attributes is not present,
        /// then one of the "Incorrect EPUB guide: item ... is missing." exceptions will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the items with the missing attributes will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipInvalidGuideReferences { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the package reader should skip EPUB collections that are missing the required 'role' attribute.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the required attribute is not present,
        /// then the "Incorrect EPUB collection: collection role is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the collections with the missing 'role' attribute will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipInvalidCollections { get; set; }
    }
}
