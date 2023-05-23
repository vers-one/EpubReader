using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;

namespace VersOne.Epub.Internal
{
    internal class RootFilePathReader
    {
        private readonly EpubReaderOptions epubReaderOptions;

        public RootFilePathReader(EpubReaderOptions? epubReaderOptions = null)
        {
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public async Task<string> GetRootFilePathAsync(IZipFile epubFile)
        {
            const string EPUB_CONTAINER_FILE_PATH = "META-INF/container.xml";
            IZipFileEntry? containerFileEntry = epubFile.GetEntry(EPUB_CONTAINER_FILE_PATH) ??
                throw new EpubContainerException($"EPUB parsing error: \"{EPUB_CONTAINER_FILE_PATH}\" file not found in the EPUB file.");
            XDocument containerDocument;
            using (Stream containerStream = containerFileEntry.Open())
            {
                containerDocument = await XmlUtils.LoadDocumentAsync(containerStream, epubReaderOptions.XmlReaderOptions).ConfigureAwait(false);
            }
            XNamespace cnsNamespace = "urn:oasis:names:tc:opendocument:xmlns:container";
            XAttribute? fullPathAttribute =
                containerDocument.Element(cnsNamespace + "container")?.Element(cnsNamespace + "rootfiles")?.Element(cnsNamespace + "rootfile")?.Attribute("full-path") ??
                throw new EpubContainerException("EPUB parsing error: root file path not found in the EPUB container.");
            return fullPathAttribute.Value;
        }
    }
}
