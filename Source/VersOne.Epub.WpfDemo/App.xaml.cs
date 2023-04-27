using System.Windows;
using VersOne.Epub.WpfDemo.ViewModels;
using VersOne.Epub.WpfDemo.WpfEnvironment;

namespace VersOne.Epub.WpfDemo
{
    /// <summary>
    /// WPF demo application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the application.
        /// </summary>
        /// <param name="e"><see cref="Application.Startup" /> event data.</param>
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
