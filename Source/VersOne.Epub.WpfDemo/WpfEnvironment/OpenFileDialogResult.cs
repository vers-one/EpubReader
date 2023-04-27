using System.Collections.Generic;

namespace VersOne.Epub.WpfDemo.WpfEnvironment
{
    internal class OpenFileDialogResult
    {
        public bool DialogResult { get; set; }
        public List<string> SelectedFilePaths { get; set; }
    }
}
