using EpubReaderDemo.ViewModels;
using EpubReaderDemo.WpfEnvironment;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EpubReaderDemo
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
