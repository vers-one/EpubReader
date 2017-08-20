using System.Windows;
using VersOne.Epub.WpfDemo.ViewModels;
using VersOne.Epub.WpfDemo.WpfEnvironment;

namespace VersOne.Epub.WpfDemo
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
