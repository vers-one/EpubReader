using System;
using System.Xml;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB 3 navigation document reader.
    /// </summary>
    public class Epub3NavDocumentReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub3NavDocumentReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">
        /// An optional preset to initialize the <see cref="Epub3NavDocumentReaderOptions" /> class with a predefined set of options.
        /// </param>
        public Epub3NavDocumentReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            switch (preset)
            {
                case EpubReaderOptionsPreset.IGNORE_ALL_ERRORS:
                    IgnoreMissingNavManifestItemError = true;
                    IgnoreMissingNavFileError = true;
                    IgnoreNavFileIsTooLargeError = true;
                    IgnoreNavFileIsNotValidXmlError = true;
                    IgnoreMissingHtmlElementError = true;
                    IgnoreMissingBodyElementError = true;
                    SkipNavsWithMissingOlElements = true;
                    SkipInvalidLiElements = true;
                    break;
            }
        }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 3 navigation document reader should ignore the error when the book doesn't have
        /// a manifest item with the 'nav' property.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the NAV manifest item is missing in the EPUB manifest,
        /// then the "EPUB parsing error: NAV item not found in EPUB manifest." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will treat the EPUB file
        /// as if it doesn't have a EPUB 3 navigation document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingNavManifestItemError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 3 navigation document reader should ignore the error when the NAV file referenced by
        /// the NAV manifest item is missing in the EPUB file.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the content file referenced by the NAV manifest item is not present,
        /// then the "EPUB parsing error: navigation file ... not found in the EPUB file." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will treat the EPUB file
        /// as if it doesn't have a EPUB 3 navigation document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingNavFileError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 3 navigation document reader should ignore the error when the NAV file is larger than 2 GB.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the content file referenced by the NAV manifest item is larger than 2 GB,
        /// then the "EPUB parsing error: navigation file ... is larger than 2 GB." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will treat the EPUB file
        /// as if it doesn't have a EPUB 3 navigation document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreNavFileIsTooLargeError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 3 navigation document reader should ignore the error
        /// when the NAV file is not a valid XHTML file.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and an XML parsing error has occurred while trying to open the NAV file,
        /// then the "EPUB parsing error: navigation file is not a valid XHTML file." exception will be thrown
        /// with the original <see cref="XmlException" /> available through the <see cref="Exception.InnerException" /> property.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will treat the EPUB file
        /// as if it doesn't have a EPUB 3 navigation document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreNavFileIsNotValidXmlError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 3 navigation document reader should ignore the error
        /// when the NAV file is missing the 'html' element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'html' element is not present,
        /// then the "EPUB parsing error: navigation file does not contain html element." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since 'html' is the top-level element
        /// in the navigation document, the reader will treat this case as if the EPUB file doesn't have a EPUB 3 navigation document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingHtmlElementError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 3 navigation document reader should ignore the error
        /// when the NAV file is missing the 'body' element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'body' element is not present,
        /// then the "EPUB parsing error: navigation file does not contain body element." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since 'body' is an essential element
        /// in the navigation document, the reader will treat this case as if the EPUB file doesn't have a EPUB 3 navigation document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingBodyElementError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 3 navigation document reader should skip
        /// 'nav' elements that are missing a child 'ol' element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'ol' element is not present inside a 'nav' element,
        /// then the "EPUB parsing error: 'nav' element in the navigation file does not contain a required 'ol' element." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case those 'nav' elements will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipNavsWithMissingOlElements { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the EPUB 3 navigation document reader should skip
        /// 'li' elements that are missing required child elements.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and neither the 'a' element nor the 'span' element are present inside an 'li' element,
        /// then the "EPUB parsing error: 'li' element in the navigation file must contain either an 'a' element or a 'span' element."
        /// exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case those 'li' elements will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipInvalidLiElements { get; set; }
    }
}
