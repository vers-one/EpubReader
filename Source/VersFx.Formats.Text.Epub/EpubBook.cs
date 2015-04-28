using System.Collections.Generic;
using VersFx.Formats.Text.Epub.Entities;

namespace VersFx.Formats.Text.Epub
{
    public class EpubBook
    {
        public EpubSchema Schema { get; set; }
        public List<EpubContentFile> ContentFiles { get; set; }
    }
}
