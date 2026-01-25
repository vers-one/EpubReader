using System;
using System.Xml;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB OCF container file reader.
    /// </summary>
    public class ContainerFileReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerFileReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">
        /// An optional preset to initialize the <see cref="ContainerFileReaderOptions" /> class with a predefined set of options.
        /// </param>
        public ContainerFileReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            switch (preset)
            {
                case EpubReaderOptionsPreset.IGNORE_ALL_ERRORS:
                    IgnoreMissingContainerFile = true;
                    IgnoreContainerFileIsNotValidXmlError = true;
                    IgnoreMissingPackageFilePathError = true;
                    break;
            }
        }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the container file reader should ignore the missing EPUB OCF container file error.
        /// </para>
        /// <para>
        /// The EPUB OCF container is the 'META-INF/container.xml' file inside the EPUB archive.
        /// If this property is set to <c>false</c> and the container file is not present,
        /// then the "EPUB parsing error: "META-INF/container.xml" file not found in the EPUB file." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since the OCF container is the main file
        /// describing the location of the OPF package file, the <see cref="EpubReader" /> class methods will return <c>null</c> in this case.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingContainerFile { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the container reader should ignore the error when the EPUB OCF container file is not a valid XML file.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and an XML parsing error has occurred while trying to open the OCF container file,
        /// then the "EPUB parsing error: EPUB OCF container file is not a valid XML file." exception will be thrown
        /// with the original <see cref="XmlException" /> available through the <see cref="Exception.InnerException" /> property.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since the OCF container is the main file
        /// describing the location of the OPF package file, the <see cref="EpubReader" /> class methods will return <c>null</c> in this case.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreContainerFileIsNotValidXmlError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the container reader should ignore the error when the OPF package file path is missing
        /// in the OCF container document.
        /// </para>
        /// <para>
        /// The path to the OPF package file is determined by the 'full-path' attribute of the
        /// 'urn:oasis:names:tc:opendocument:xmlns:container:container/rootfiles/rootfile' XML element.
        /// If it's set to <c>false</c> and the OPF package file is not found at the specified attribute,
        /// then the "EPUB parsing error: OPF package file path not found in the EPUB container." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>. However, since the OPF package is the main file
        /// describing the EPUB book, the <see cref="EpubReader" /> class methods will return <c>null</c> in this case.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingPackageFilePathError { get; set; }
    }
}
