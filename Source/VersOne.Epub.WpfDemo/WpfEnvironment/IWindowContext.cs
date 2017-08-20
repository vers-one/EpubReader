using System;
using System.Windows;

namespace VersOne.Epub.WpfDemo.WpfEnvironment
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
