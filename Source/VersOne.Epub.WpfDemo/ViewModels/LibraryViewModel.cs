using System.Collections.ObjectModel;
using System.Windows.Input;
using VersOne.Epub.WpfDemo.Models;
using VersOne.Epub.WpfDemo.Utils;
using VersOne.Epub.WpfDemo.WpfEnvironment;

namespace VersOne.Epub.WpfDemo.ViewModels
{
    /// <summary>
    /// View model for the <see cref="Views.LibraryView" />.
    /// </summary>
    public class LibraryViewModel : ViewModel
    {
        private readonly IWindowManager windowManager;
        private readonly LibraryModel libraryModel;

        private Command addBookCommand;
        private Command removeFromLibraryCommand;
        private Command openBookCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryViewModel" /> class.
        /// </summary>
        public LibraryViewModel()
        {
            windowManager = WindowManager.Instance;
            libraryModel = new LibraryModel();
            addBookCommand = null;
            removeFromLibraryCommand = null;
            openBookCommand = null;
            RefreshLibrary();
        }

        /// <summary>
        /// Gets a collection of library items (i.e. books shown in the library view).
        /// </summary>
        public ObservableCollection<LibraryItemViewModel> Books { get; private set; }

        /// <summary>
        /// Gets the command which is executed when the user clicks on the 'Add book' button in the library view.
        /// </summary>
        public ICommand AddBookCommand
        {
            get
            {
                if (addBookCommand == null)
                {
                    addBookCommand = new Command(AddBook);
                }
                return addBookCommand;
            }
        }

        /// <summary>
        /// Gets the command which is executed when the user clicks on the 'Remove from library' context menu item of a book in the library view.
        /// </summary>
        public ICommand RemoveFromLibraryCommand
        {
            get
            {
                if (removeFromLibraryCommand == null)
                {
                    removeFromLibraryCommand = new Command(param => RemoveBookFromLibrary(param as LibraryItemViewModel));
                }
                return removeFromLibraryCommand;
            }
        }

        /// <summary>
        /// Gets the command which is executed when the user clicks on a book in the library view.
        /// </summary>
        public ICommand OpenBookCommand
        {
            get
            {
                if (openBookCommand == null)
                {
                    openBookCommand = new Command(param => OpenBook(param as LibraryItemViewModel));
                }
                return openBookCommand;
            }
        }

        private void RefreshLibrary()
        {
            Books = new ObservableCollection<LibraryItemViewModel>(libraryModel.GetLibraryItems());
            NotifyPropertyChanged(nameof(Books));
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
                {
                    libraryModel.AddBookToLibrary(selectedFilePath);
                }
                RefreshLibrary();
            }
        }

        private void RemoveBookFromLibrary(LibraryItemViewModel book)
        {
            libraryModel.RemoveBookFromLibrary(book);
            RefreshLibrary();
        }

        private void OpenBook(LibraryItemViewModel book)
        {
            BookViewModel bookViewModel = new BookViewModel(book.Id);
            IWindowContext bookWindowContext = windowManager.CreateWindow(bookViewModel);
            bookWindowContext.ShowDialog();
        }
    }
}
