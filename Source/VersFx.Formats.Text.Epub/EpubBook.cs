using System.Collections.Generic;
using VersFx.Formats.Text.Epub.Entities;

namespace VersFx.Formats.Text.Epub
{
    public class EpubBook
    {
        public string FilePath { get; set; }
        public EpubSchema Schema { get; set; }
        public List<EpubContentFile> ContentFiles { get; set; }
    }
}
