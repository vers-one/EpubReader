using System.Collections.Generic;

namespace EpubReaderDemo.Wpf.WpfEnvironment
{
    internal class OpenFileDialogResult
    {
        public bool DialogResult { get; set; }
        public List<string> SelectedFilePaths { get; set; }
    }
}
