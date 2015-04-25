using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersFx.Formats.Text.Epub.Schema.Navigation
{
    public class EpubNavigationPoint
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string PlayOrder { get; set; }
        public List<EpubNavigationLabel> NavigationLabels { get; set; }
        public EpubNavigationContent Content { get; set; }
        public List<EpubNavigationPoint> ChildNavigationPoints { get; set; }

        public override string ToString()
        {
            return String.Format("Id: {0}, Content.Source: {1}", Id, Content.Source);
        }
    }
}
