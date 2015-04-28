using VersFx.Formats.Text.Epub.Schema.Navigation;
using VersFx.Formats.Text.Epub.Schema.Opf;

namespace VersFx.Formats.Text.Epub.Entities
{
    public class EpubSchema
    {
        public EpubPackage Package { get; set; }
        public EpubNavigation Navigation { get; set; }
        public string ContentDirectoryPath { get; set; }
    }
}
