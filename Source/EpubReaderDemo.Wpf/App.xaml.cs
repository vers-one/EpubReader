using System.Windows;
using EpubReaderDemo.Wpf.ViewModels;
using EpubReaderDemo.Wpf.WpfEnvironment;

namespace EpubReaderDemo.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LibraryViewModel libraryViewModel = new LibraryViewModel();
            IWindowContext libraryWindowContext = WindowManager.Instance.CreateWindow(libraryViewModel);
            libraryWindowContext.Closed += (sender, args) => Shutdown();
            libraryWindowContext.Show();
        }
    }
}
