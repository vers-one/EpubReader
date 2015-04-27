using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpubReaderDemo.WpfEnvironment
{
    internal class OpenFileDialogParameters
    {
        public string Filter { get; set; }
        public bool Multiselect { get; set; }
        public string InitialDirectory { get; set; }
    }
}
