using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    public class Epub2NcxPageTarget
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public Epub2NcxPageTargetType Type { get; set; }
        public string Class { get; set; }
        public string PlayOrder { get; set; }
        public List<Epub2NcxNavigationLabel> NavigationLabels { get; set; }
        public Epub2NcxContent Content { get; set; }
    }
}
