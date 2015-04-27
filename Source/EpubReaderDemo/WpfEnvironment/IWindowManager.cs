using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpubReaderDemo.WpfEnvironment
{
    internal interface IWindowManager
    {
        IWindowContext CreateWindow(object viewModel);
        IWindowContext FindActiveWindow();
        OpenFileDialogResult ShowOpenFileDialog(OpenFileDialogParameters openFileDialogParameters);
    }
}
