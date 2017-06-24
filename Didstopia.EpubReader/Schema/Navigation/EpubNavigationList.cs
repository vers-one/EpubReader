using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Didstopia.EpubReader.Schema.Navigation
{
    public class EpubNavigationList
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public List<EpubNavigationLabel> NavigationLabels { get; set; }
        public List<EpubNavigationTarget> NavigationTargets { get; set; }
    }
}
