using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersFx.Formats.Text.Epub.Schema.Opf
{
    public class EpubPackage
    {
        public EpubVersion EpubVersion { get; set; }
        public EpubMetadata Metadata { get; set; }
        public EpubManifest Manifest { get; set; }
        public EpubSpine Spine { get; set; }
        public EpubGuide Guide { get; set; }
    }
}
