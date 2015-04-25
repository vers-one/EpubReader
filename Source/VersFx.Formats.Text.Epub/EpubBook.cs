using System.Collections.Generic;
using VersFx.Formats.Text.Epub.Schema.Navigation;
using VersFx.Formats.Text.Epub.Schema.Opf;

namespace VersFx.Formats.Text.Epub
{
    public class EpubBook
    {
        public EpubPackage Package { get; set; }
        public EpubNavigation Navigation { get; set; }
        public List<EpubContentFile> ContentFiles { get; set; }
    }
}
