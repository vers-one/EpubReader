using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    public class Epub2NcxNavigationPoint
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string PlayOrder { get; set; }
        public List<Epub2NcxNavigationLabel> NavigationLabels { get; set; }
        public Epub2NcxContent Content { get; set; }
        public List<Epub2NcxNavigationPoint> ChildNavigationPoints { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Content.Source: {Content.Source}";
        }
    }
}
