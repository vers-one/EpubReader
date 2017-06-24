using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Didstopia.EpubReader.Schema.Navigation
{
    public class EpubNavigationTarget
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string Value { get; set; }
        public string PlayOrder { get; set; }
        public List<EpubNavigationLabel> NavigationLabels { get; set; }
        public EpubNavigationContent Content { get; set; }
    }
}
