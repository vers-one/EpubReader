using System;
using System.Xml;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the SMIL (EPUB media overlay) document reader.
    /// </summary>
    public class SmilReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmilReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="SmilReaderOptions" /> class with a predefined set of options.</param>
        public SmilReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            switch (preset)
            {
                case EpubReaderOptionsPreset.IGNORE_ALL_ERRORS:
                    IgnoreMissingSmilFileError = true;
                    IgnoreSmilFileIsTooLargeError = true;
                    IgnoreSmilFileIsNotValidXmlError = true;
                    IgnoreMissingSmilElementError = true;
                    IgnoreMissingSmilVersionError = true;
                    IgnoreUnsupportedSmilVersionError = true;
                    IgnoreMissingBodyElementError = true;
                    IgnoreBodyMissingSeqOrParElementsError = true;
                    IgnoreSeqMissingSeqOrParElementsError = true;
                    SkipParsWithoutTextElements = true;
                    SkipTextsWithoutSrcAttributes = true;
                    SkipAudiosWithoutSrcAttributes = true;
                    break;
            }
        }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should ignore the error when the SMIL file referenced by
        /// a SMIL manifest item is missing in the EPUB file.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the content file referenced by a SMIL manifest item is not present,
        /// then the "EPUB parsing error: SMIL file ... not found in the EPUB file." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will skip this SMIL file.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingSmilFileError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should ignore the error when a SMIL file is larger than 2 GB.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the content file referenced by a SMIL manifest item is larger than 2 GB,
        /// then the "EPUB parsing error: SMIL file ... is larger than 2 GB." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will skip this SMIL file.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreSmilFileIsTooLargeError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should ignore the error when a SMIL file is not a valid XML file.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and an XML parsing error has occurred while trying to open a SMIL file,
        /// then the "EPUB parsing error: SMIL file ... is not a valid XML file." exception will be thrown
        /// with the original <see cref="XmlException" /> available through the <see cref="Exception.InnerException" /> property.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will skip this SMIL file.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreSmilFileIsNotValidXmlError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should ignore the error
        /// when a SMIL file is missing the 'smil' XML element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'smil' XML element is not present,
        /// then the "SMIL parsing error: smil XML element is missing in the file." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since 'smil' is the top-level XML element
        /// in the SMIL document, the reader will will skip this SMIL file in this case.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingSmilElementError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should ignore the error when the SMIL version is missing in the SMIL file.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'smil' XML element is missing the 'version' attribute,
        /// then the "SMIL parsing error: SMIL version is missing." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will ignore this error
        /// and use SMIL 3.0 specification to parse the rest of the SMIL document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingSmilVersionError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should ignore the error
        /// when SMIL version declared in the SMIL file is not supported by the reader.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'smil' XML element's 'version' attribute contains a version
        /// other than supported by the reader (currently, only '3.0'),
        /// then the "SMIL parsing error: unsupported SMIL version: ... ." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the reader will ignore this error
        /// and use SMIL 3.0 specification to parse the rest of the SMIL document.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreUnsupportedSmilVersionError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should ignore the error
        /// when a SMIL file is missing the 'body' XML element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'body' XML element is not present,
        /// then the "SMIL parsing error: body XML element is missing in the file." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since 'body' is an essential XML element
        /// in the SMIL document, the reader will will skip this SMIL file in this case.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingBodyElementError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should ignore the error
        /// when the 'body' XML element is missing a child 'seq' / 'par' XML element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'body' XML element has neither a 'seq' XML element nor a 'par' XML element,
        /// then the "SMIL parsing error: body XML element must contain at least one seq or par XML element." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since 'seq' and 'par' are
        /// the only container elements for the text and audio data in the SMIL file, the resulting parsed document will have no usable content.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreBodyMissingSeqOrParElementsError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should ignore the error
        /// when a 'seq' XML element is missing a child 'seq' / 'par' XML element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a 'seq' XML element has neither a nested 'seq' XML element nor a 'par' XML element,
        /// then the "SMIL parsing error: seq XML element must contain at least one nested seq or par XML element." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since 'seq' and 'par' are
        /// the only container elements for the text and audio data in the SMIL file,
        /// the resulting parsed SMIL sequence object will have no usable content.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreSeqMissingSeqOrParElementsError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should skip 'par' XML elements
        /// that are missing a child 'text' XML element.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the 'text' XML element is not present inside a 'par' XML element,
        /// then the "SMIL parsing error: par XML element must contain one text XML element." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case those 'par' XML elements will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipParsWithoutTextElements { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should skip 'text' XML elements that are missing the 'src' attribute.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a 'text' XML element is missing the 'src' attribute,
        /// then the "SMIL parsing error: text XML element must have an src attribute." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case those 'text' XML elements will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipTextsWithoutSrcAttributes { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the SMIL document reader should skip 'audio' XML elements that are missing the 'src' attribute.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and an 'audio' XML element is missing the 'src' attribute,
        /// then the "SMIL parsing error: audio XML element must have an src attribute." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case those 'audio' XML elements will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipAudiosWithoutSrcAttributes { get; set; }
    }
}
