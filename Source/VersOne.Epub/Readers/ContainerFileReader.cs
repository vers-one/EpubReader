using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;

namespace VersOne.Epub.Internal
{
    internal class ContainerFileReader
    {
        private readonly EpubReaderOptions epubReaderOptions;
        private readonly ContainerFileReaderOptions containerFileReaderOptions;

        public ContainerFileReader(EpubReaderOptions? epubReaderOptions = null)
        {
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
            this.containerFileReaderOptions = this.epubReaderOptions.ContainerFileReaderOptions ?? new ContainerFileReaderOptions();
        }

        public async Task<string?> GetPackageFilePathAsync(IZipFile epubFile)
        {
            const string EPUB_CONTAINER_FILE_PATH = "META-INF/container.xml";
            IZipFileEntry? containerFileEntry = epubFile.GetEntry(EPUB_CONTAINER_FILE_PATH);
            if (containerFileEntry == null)
            {
                if (containerFileReaderOptions.IgnoreMissingContainerFile)
                {
                    return null;
                }
                throw new EpubContainerException($"EPUB parsing error: \"{EPUB_CONTAINER_FILE_PATH}\" file not found in the EPUB file.");
            }
            XDocument containerDocument;
            try
            {
                using Stream containerStream = containerFileEntry.Open();
                containerDocument = await XmlUtils.LoadDocumentAsync(containerStream, epubReaderOptions.XmlReaderOptions).ConfigureAwait(false);
            }
            catch (XmlException xmlException)
            {
                if (containerFileReaderOptions.IgnoreContainerFileIsNotValidXmlError)
                {
                    return null;
                }
                throw new EpubContainerException("EPUB parsing error: EPUB OCF container file is not a valid XML file.", xmlException);
            }
            XNamespace cnsNamespace = "urn:oasis:names:tc:opendocument:xmlns:container";
            XAttribute? fullPathAttribute = containerDocument.Element(cnsNamespace + "container")?.Element(cnsNamespace + "rootfiles")?.
                Element(cnsNamespace + "rootfile")?.Attribute("full-path");
            if (fullPathAttribute == null)
            {
                if (containerFileReaderOptions.IgnoreMissingPackageFilePathError)
                {
                    return null;
                }
                throw new EpubContainerException("EPUB parsing error: OPF package file path not found in the EPUB container.");
            }
            return fullPathAttribute.Value;
        }
    }
}
