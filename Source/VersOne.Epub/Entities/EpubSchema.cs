using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    public class EpubSchema
    {
        public EpubPackage Package { get; set; }
        public Epub2Ncx Navigation { get; set; }
        public string ContentDirectoryPath { get; set; }
    }
}
