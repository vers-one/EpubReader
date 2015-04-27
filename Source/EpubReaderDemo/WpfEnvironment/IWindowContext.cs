using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EpubReaderDemo.WpfEnvironment
{
    public interface IWindowContext
    {
        string ViewName { get; }
        Window Window { get; }
        object DataContext { get; }

        event EventHandler Activated;
        event EventHandler Showing;
        event EventHandler Closing;
        event EventHandler Closed;

        void Show(bool showMaximized = false);
        bool? ShowDialog(IWindowContext ownerWindowContext = null, bool showMaximized = false);
        bool? ShowDialog(IntPtr ownerHandle, bool showMaximized = false);
        void Close();
        void CloseDialog(bool dialogResult);
        void Focus();
    }
}
