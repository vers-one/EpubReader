using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Didstopia.EpubReader.Schema.Opf
{
    public class EpubSpineItemRef
    {
        public string IdRef { get; set; }
        public bool IsLinear { get; set; }

        public override string ToString()
        {
            return String.Concat("IdRef: ", IdRef);
        }
    }
}
