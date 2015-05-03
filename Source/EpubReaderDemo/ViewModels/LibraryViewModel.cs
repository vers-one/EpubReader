using System.Collections.ObjectModel;
using System.Windows.Input;
using EpubReaderDemo.Models;
using EpubReaderDemo.Utils;
using EpubReaderDemo.WpfEnvironment;

namespace EpubReaderDemo.ViewModels
{
    public class LibraryViewModel : ViewModel
    {
        private readonly IWindowManager windowManager;
        private readonly LibraryModel libraryModel;

        private Command addBookCommand;
        private Command removeFromLibraryCommand;

        public LibraryViewModel()
        {
            windowManager = WindowManager.Instance;
            libraryModel = new LibraryModel();
            addBookCommand = null;
            removeFromLibraryCommand = null;
            RefreshLibrary();
        }

        public ObservableCollection<LibraryItemViewModel> Books { get; private set; }

        public ICommand AddBookCommand
        {
            get
            {
                if (addBookCommand == null)
                    addBookCommand = new Command(AddBook);
                return addBookCommand;
            }
        }

        public ICommand RemoveFromLibraryCommand
        {
            get
            {
                if (removeFromLibraryCommand == null)
                    removeFromLibraryCommand = new Command(param => RemoveBookFromLibrary(param as LibraryItemViewModel));
                return removeFromLibraryCommand;
            }
        }
        
        private void RefreshLibrary()
        {
            Books = new ObservableCollection<LibraryItemViewModel>(libraryModel.GetLibraryItems());
            OnPropertyChanged(() => Books);
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
                foreach (string selectedFilePath in openDialogResult.SelectedFilePaths)
                    libraryModel.AddBookToLibrary(selectedFilePath);
                RefreshLibrary();
            }
        }

        private void RemoveBookFromLibrary(LibraryItemViewModel book)
        {
            libraryModel.RemoveBookFromLibrary(book);
            RefreshLibrary();
        }
    }
}
