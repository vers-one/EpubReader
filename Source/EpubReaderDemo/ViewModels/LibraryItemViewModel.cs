using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpubReaderDemo.ViewModels
{
    public class LibraryItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] CoverImage { get; set; }
    }
}
