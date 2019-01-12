using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    public class Epub2NcxNavigationList
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public List<Epub2NcxNavigationLabel> NavigationLabels { get; set; }
        public List<Epub2NcxNavigationTarget> NavigationTargets { get; set; }
    }
}
