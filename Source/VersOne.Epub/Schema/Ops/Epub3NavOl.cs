using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    public class Epub3NavOl
    {
        public bool IsHidden { get; set; }
        public List<Epub3NavLi> Lis { get; set; }
    }
}
