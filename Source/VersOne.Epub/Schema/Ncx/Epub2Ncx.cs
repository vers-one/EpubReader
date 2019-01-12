using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    public class Epub2Ncx
    {
        public Epub2NcxHead Head { get; set; }
        public Epub2NcxDocTitle DocTitle { get; set; }
        public List<Epub2NcxDocAuthor> DocAuthors { get; set; }
        public Epub2NcxNavigationMap NavMap { get; set; }
        public Epub2NcxPageList PageList { get; set; }
        public List<Epub2NcxNavigationList> NavLists { get; set; }
    }
}
