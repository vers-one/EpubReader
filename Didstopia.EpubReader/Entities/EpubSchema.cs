using Didstopia.EpubReader.Schema.Navigation;
using Didstopia.EpubReader.Schema.Opf;

namespace Didstopia.EpubReader
{
    public class EpubSchema
    {
        public EpubPackage Package { get; set; }
        public EpubNavigation Navigation { get; set; }
        public string ContentDirectoryPath { get; set; }
    }
}
