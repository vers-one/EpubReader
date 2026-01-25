using System;
using System.Xml;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB 2 NCX navigation document reader.
    /// </summary>
    public class Epub2NcxReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="Epub2NcxReaderOptions" /> class with a predefined set of options.</param>
        public Epub2NcxReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            switch (preset)
            {
                case EpubReaderOptionsPreset.RELAXED:
                    SkipNavigationPointsWithMissingIds = true;
                    break;
                case EpubReaderOptionsPreset.IGNORE_ALL_ERRORS:
                    IgnoreMissingTocManifestItemError = true;
                    IgnoreMissingTocFileError = true;
                    IgnoreTocFileIsTooLargeError = true;
                    IgnoreTocFileIsNotValidXmlError = true;
                    IgnoreMissingNcxElementError = true;
                    IgnoreMissingHeadElementError = true;
                    IgnoreMissingDocTitleElementError = true;
                    IgnoreMissingNavMapElementError = true;
                    SkipInvalidMetaElements = true;
                    SkipNavigationPointsWithMissingIds = true;
                    AllowNavigationPointsWithoutLabels = true;
                    IgnoreMissingContentForNavigationPoints = true;
                    SkipInvalidNavigationLabels = true;
                    SkipInvalidNavigationContent = true;
                    ReplaceMissingPageTargetTypesWithUnknown = true;
                    AllowNavigationPageTargetsWithoutLabels = true;
                    AllowNavigationListsWithoutLabels = true;
                    SkipInvalidNavigationTargets = true;
                    AllowNavigationTargetsWithoutLabels = true;
                    break;
            }
        }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when the manifest item referenced by
        /// the EPUB spine's 'toc' attribute is missing.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the TOC manifest item is missing in the EPUB manifest,
        /// then the "EPUB parsing error: TOC item ... not found in EPUB manifest." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will treat the EPUB file
        /// as if it doesn't have a EPUB 2 NCX document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingTocManifestItemError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when the TOC file referenced by
        /// the TOC manifest item is missing in the EPUB file.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the content file referenced by the TOC manifest item is not present,
        /// then the "EPUB parsing error: TOC file ... not found in the EPUB file." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will treat the EPUB file
        /// as if it doesn't have a EPUB 2 NCX document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingTocFileError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when the TOC file is larger than 2 GB.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the content file referenced by the TOC manifest item is larger than 2 GB,
        /// then the "EPUB parsing error: TOC file ... is larger than 2 GB." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will treat the EPUB file
        /// as if it doesn't have a EPUB 2 NCX document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreTocFileIsTooLargeError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when the TOC file is not a valid XML file.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and an XML parsing error has occurred while trying to open the TOC file,
        /// then the "EPUB parsing error: TOC file is not a valid XML file." exception will be thrown with the original <see cref="XmlException" />
        /// available through the <see cref="Exception.InnerException" /> property.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will treat the EPUB file
        /// as if it doesn't have a EPUB 2 NCX document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreTocFileIsNotValidXmlError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when the TOC file is missing the 'ncx' XML element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'ncx' XML element is not present,
        /// then the "EPUB parsing error: TOC file does not contain ncx element." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since 'ncx' is the top-level XML element
        /// in the EPUB 2 NCX file, the reader will treat this case as if the EPUB file doesn't have a EPUB 2 NCX document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingNcxElementError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when the TOC file is missing the 'head' XML element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'head' XML element is not present,
        /// then the "EPUB parsing error: TOC file does not contain head element." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will create an empty
        /// <see cref="Epub2NcxHead" /> object for the <see cref="Epub2Ncx.Head" /> property.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingHeadElementError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when the TOC file is missing the 'docTitle' XML element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'docTitle' XML element is not present,
        /// then the "EPUB parsing error: TOC file does not contain docTitle element." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will set
        /// the <see cref="Epub2Ncx.DocTitle" /> property to <c>null</c>.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingDocTitleElementError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when the TOC file is missing the 'navMap' XML element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'navMap' XML element is not present,
        /// then the "EPUB parsing error: TOC file does not contain navMap element." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will create an empty
        /// <see cref="Epub2NcxNavigationMap" /> object for the <see cref="Epub2Ncx.NavMap" /> property.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingNavMapElementError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should skip 'meta' XML elements that are missing required attributes
        /// ('name' and 'content').
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and one of the required attributes is not present,
        /// then one of the "Incorrect EPUB navigation meta: meta ... is missing." exceptions will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the items with the missing attributes will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipInvalidMetaElements { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should skip navigation points that are missing the 'id' attribute.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'id' attribute is not present,
        /// then the "Incorrect EPUB navigation point: point ID is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the navigation points with the missing attribute will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipNavigationPointsWithMissingIds { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when a navigation point doesn't have any labels.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a navigation point without any labels is found,
        /// then the "EPUB parsing error: navigation point ... should contain at least one navigation label." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will skip the label count validation.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool AllowNavigationPointsWithoutLabels { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should skip navigation points that are missing the 'content' XML element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'content' element is not present,
        /// then the "EPUB parsing error: navigation point ... should contain content." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the navigation points with the missing content will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingContentForNavigationPoints { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should skip navigation labels that are missing the 'text' XML element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'text' element is not present,
        /// then the "Incorrect EPUB navigation label: label text element is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the navigation labels with the missing text will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipInvalidNavigationLabels { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should skip navigation content elements that are missing the 'src' attribute.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'src' attribute is not present,
        /// then the "Incorrect EPUB navigation content: content source is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case both the navigation content with the missing attribute and the element it belongs to
        /// (navigation point, navigation target, or navigation page target) will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipInvalidNavigationContent { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should replace missing navigation page target types with unknown types.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'type' attribute is not present on a 'pageTarget' XML node,
        /// then the "Incorrect EPUB navigation page target: page target type is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will ignore the error
        /// and will set the <see cref="Epub2NcxPageTarget.Type" /> property to <see cref="Epub2NcxPageTargetType.UNKNOWN" />.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool ReplaceMissingPageTargetTypesWithUnknown { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when a navigation page target doesn't have any labels.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a navigation page target without any labels is found,
        /// then the "Incorrect EPUB navigation page target: at least one navLabel element is required." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will skip the label count validation.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool AllowNavigationPageTargetsWithoutLabels { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when a navigation list doesn't have any labels.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a navigation list without any labels is found,
        /// then the "Incorrect EPUB navigation list: at least one navLabel element is required." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will skip the label count validation.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool AllowNavigationListsWithoutLabels { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should skip navigation targets that are missing the 'id' attribute.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'id' attribute is not present,
        /// then the "Incorrect EPUB navigation target: navigation target ID is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>,
        /// in which case the navigation targets with the missing attribute will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipInvalidNavigationTargets { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 2 NCX reader should ignore the error when a navigation target doesn't have any labels.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a navigation target without any labels is found,
        /// then the "Incorrect EPUB navigation target: at least one navLabel element is required." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will skip the label count validation.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool AllowNavigationTargetsWithoutLabels { get; set; }
    }
}
