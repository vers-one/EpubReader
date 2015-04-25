using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersFx.Formats.Text.Epub.Schema.Opf
{
    public class EpubMetadata
    {
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Contributor { get; set; }
        public string Coverage { get; set; }
        public string Creator { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
        public string Publisher { get; set; }
        public string Relation { get; set; }
        public string Rights { get; set; }
        public string Source { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
    }
}
