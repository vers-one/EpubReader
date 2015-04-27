using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpubReaderDemo.WpfEnvironment
{
    internal class OpenFileDialogResult
    {
        public bool DialogResult { get; set; }
        public List<string> SelectedFilePaths { get; set; }
    }
}
