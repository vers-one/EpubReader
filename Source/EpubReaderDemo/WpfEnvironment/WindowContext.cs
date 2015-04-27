using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace EpubReaderDemo.WpfEnvironment
{
    internal class WindowContext : IWindowContext
    {
        private IWindowManager windowManager;

        internal WindowContext(IWindowManager windowManager, string viewName, Window window, object dataContext)
        {
            this.windowManager = windowManager;
            ViewName = viewName;
            Window = window;
            DataContext = dataContext;
            Window.Activated += Window_Activated;
            Window.Closing += Window_Closing;
            Window.Closed += Window_Closed;
        }

        public string ViewName { get; private set; }
        public Window Window { get; private set; }
        public object DataContext { get; private set; }

        public event EventHandler Activated;
        public event EventHandler Showing;
        public event EventHandler Closing;
        public event EventHandler Closed;

        public void Show(bool showMaximized = false)
        {
            if (!Window.IsVisible)
            {
                OnShowing();
                Window.WindowState = GetWindowState(showMaximized);
                Window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Window.Show();
            }
            else
            {
                if (Window.WindowState == WindowState.Minimized)
                    Window.WindowState = WindowState.Normal;
                else
                    Window.Activate();
            }
        }

        public bool? ShowDialog(IWindowContext ownerWindowContext = null, bool showMaximized = false)
        {
            OnShowing();
            if (ownerWindowContext == null)
                ownerWindowContext = windowManager.FindActiveWindow();
            Window.Owner = ownerWindowContext.Window;
            return ShowDialog(showMaximized, true);
        }

        public bool? ShowDialog(IntPtr ownerHandle, bool showMaximized = false)
        {
            OnShowing();
            var windowInteropHelper = new WindowInteropHelper(Window);
            windowInteropHelper.Owner = ownerHandle;
            return ShowDialog(showMaximized, ownerHandle != IntPtr.Zero);
        }

        public void Close()
        {
            Window.Close();
        }

        public void CloseDialog(bool dialogResult)
        {
            Window.DialogResult = dialogResult;
        }

        public void Focus()
        {
            Window.Focus();
        }

        protected virtual void OnActivated()
        {
            EventHandler handler = Activated;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnShowing()
        {
            EventHandler handler = Showing;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnClosing()
        {
            EventHandler handler = Closing;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnClosed()
        {
            EventHandler handler = Closed;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private bool? ShowDialog(bool showMaximized, bool hasOwner)
        {
            Window.WindowStartupLocation = hasOwner ? WindowStartupLocation.CenterOwner : WindowStartupLocation.CenterScreen;
            Window.ShowInTaskbar = false;
            Window.WindowState = GetWindowState(showMaximized);
            return Window.ShowDialog();
        }

        private WindowState GetWindowState(bool isMaximized)
        {
            return isMaximized ? WindowState.Maximized : WindowState.Normal;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            OnActivated();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            OnClosing();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            OnClosed();
            Window.Closing -= Window_Closing;
            Window.Closed -= Window_Closed;
        }
    }
}
