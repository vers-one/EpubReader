using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersFx.Formats.Text.Epub.Schema.Navigation
{
    public class EpubNavigationPageTarget
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public EpubNavigationPageTargetType Type { get; set; }
        public string Class { get; set; }
        public string PlayOrder { get; set; }
        public List<EpubNavigationLabel> NavigationLabels { get; set; }
        public EpubNavigationContent Content { get; set; }
    }
}
