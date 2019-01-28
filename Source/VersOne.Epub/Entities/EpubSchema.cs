using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    public class EpubSchema
    {
        public EpubPackage Package { get; set; }
        public Epub2Ncx Epub2Ncx { get; set; }
        public Epub3NavDocument Epub3NavDocument { get; set; }
        public string ContentDirectoryPath { get; set; }
    }
}
