using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Didstopia.EpubReader.Schema.Navigation
{
    public class EpubNavigationContent
    {
        public string Id { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return String.Concat("Source: " + Source);
        }
    }
}
