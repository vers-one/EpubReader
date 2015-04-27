using EpubReaderDemo.Models;
using EpubReaderDemo.Utils;
using EpubReaderDemo.WpfEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EpubReaderDemo.ViewModels
{
    public class LibraryViewModel
    {
        private readonly IWindowManager windowManager;
        private readonly LibraryModel libraryModel;

        private Command addBookCommand;

        public LibraryViewModel()
        {
            windowManager = WindowManager.Instance;
            libraryModel = new LibraryModel();
            addBookCommand = null;
        }

        public ICommand AddBookCommand
        {
            get
            {
                if (addBookCommand == null)
                    addBookCommand = new Command(AddBook);
                return addBookCommand;
            }
        }

        private void AddBook()
        {
            OpenFileDialogParameters openFileDialogParameters = new OpenFileDialogParameters
            {
                Filter = "EPUB files (*.epub)|*.epub|All files (*.*)|*.*",
                Multiselect = true
            };
            OpenFileDialogResult openDialogResult = windowManager.ShowOpenFileDialog(openFileDialogParameters);
            if (openDialogResult.DialogResult)
            {
            }
        }
    }
}
