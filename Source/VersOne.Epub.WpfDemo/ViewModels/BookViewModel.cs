using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VersOne.Epub.WpfDemo.Models;
using VersOne.Epub.WpfDemo.Utils;

namespace VersOne.Epub.WpfDemo.ViewModels
{
    /// <summary>
    /// View model for the <see cref="Views.BookView" />.
    /// </summary>
    public class BookViewModel : ViewModel
    {
        private readonly BookModel bookModel;

        private bool isLoading;
        private ObservableCollection<NavigationItemViewModel> navigation;
        private ObservableCollection<HtmlContentFileViewModel> readingOrder;
        private ZipArchive currentEpubArchive;
        private HtmlContentFileViewModel currentHtmlContentFile;
        private HtmlContentFileViewModel previousHtmlContentFile;
        private HtmlContentFileViewModel nextHtmlContentFile;
        private string currentAnchor;
        private Command navigateCommand;
        private Command previousCommand;
        private Command nextCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookViewModel" /> class.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book which should contain the same value as <see cref="Entities.Book.Id" />.</param>
        public BookViewModel(int bookId)
        {
            bookModel = new BookModel();
            isLoading = true;
            currentEpubArchive = null;
            currentHtmlContentFile = null;
            previousHtmlContentFile = null;
            nextHtmlContentFile = null;
            currentAnchor = null;
            navigateCommand = null;
            previousCommand = null;
            nextCommand = null;
            bookModel.OpenBookAsync(bookId).ContinueWith(epubBook => BookOpened(epubBook), TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Gets a collection of navigation items which are used to render the navigation tree in the <see cref="Views.BookView" />.
        /// </summary>
        public ObservableCollection<NavigationItemViewModel> Navigation
        {
            get
            {
                return navigation;
            }
            private set
            {
                navigation = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets a collection of HTML content files which determine the order in which they are rendered in the <see cref="Views.BookView" />.
        /// </summary>
        public ObservableCollection<HtmlContentFileViewModel> ReadingOrder
        {
            get
            {
                return readingOrder;
            }
            private set
            {
                readingOrder = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the book is still loading or not.
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            private set
            {
                isLoading = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the reference to the EPUB book file currently opened in the <see cref="Views.BookView" />.
        /// </summary>
        public ZipArchive CurrentEpubArchive
        {
            get
            {
                return currentEpubArchive;
            }
            set
            {
                currentEpubArchive = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the reference to a view model of the HTML content file from <see cref="CurrentEpubArchive" /> currently rendered in the <see cref="Views.BookView" />.
        /// </summary>
        public HtmlContentFileViewModel CurrentHtmlContentFile
        {
            get
            {
                return currentHtmlContentFile;
            }
            set
            {
                currentHtmlContentFile = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the button to navigate to the previous content file in the reading order should be visible or not.
        /// </summary>
        public bool IsPreviousButtonVisible
        {
            get
            {
                return previousHtmlContentFile != null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the button to navigate to the next content file in the reading order should be visible or not.
        /// </summary>
        public bool IsNextButtonVisible
        {
            get
            {
                return nextHtmlContentFile != null;
            }
        }

        /// <summary>
        /// Gets or sets the name of the anchor in the <see cref="CurrentHtmlContentFile" /> which is used to determine the place
        /// where the content should be scrolled to once it is fully loaded.
        /// </summary>
        public string CurrentAnchor
        {
            get
            {
                return currentAnchor;
            }
            set
            {
                currentAnchor = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the command which is executed when the user clicks on a navigation link in the navigation tree in the <see cref="Views.BookView" />.
        /// </summary>
        public ICommand NavigateCommand
        {
            get
            {
                if (navigateCommand == null)
                {
                    navigateCommand = new Command(param => Navigate(param as NavigationItemViewModel));
                }
                return navigateCommand;
            }
        }

        /// <summary>
        /// Gets the command which is executed when the user clicks on the '&lt; PREVIOUS' button in the <see cref="Views.BookView" />.
        /// </summary>
        public ICommand PreviousCommand
        {
            get
            {
                if (previousCommand == null)
                {
                    previousCommand = new Command(NavigateToPreviousItemInReadingOrder);
                }
                return previousCommand;
            }
        }

        /// <summary>
        /// Gets the command which is executed when the user clicks on the 'NEXT &gt;' button in the <see cref="Views.BookView" />.
        /// </summary>
        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                {
                    nextCommand = new Command(NavigateToNextItemInReadingOrder);
                }
                return nextCommand;
            }
        }

        private void BookOpened(Task<EpubBook> task)
        {
            EpubBook epubBook = task.Result;
            currentEpubArchive?.Dispose();
            CurrentEpubArchive = ZipFile.OpenRead(epubBook.FilePath);
            Navigation = new ObservableCollection<NavigationItemViewModel>(bookModel.GetNavigation(epubBook));
            ReadingOrder = new ObservableCollection<HtmlContentFileViewModel>(bookModel.GetReadingOrder(epubBook));
            if (ReadingOrder.Any())
            {
                CurrentHtmlContentFile = ReadingOrder.First();
                if (ReadingOrder.Count > 1)
                {
                    nextHtmlContentFile = ReadingOrder[1];
                }
            }
            IsLoading = false;
            NotifyPropertyChanged(nameof(IsPreviousButtonVisible));
            NotifyPropertyChanged(nameof(IsNextButtonVisible));
        }

        private void Navigate(NavigationItemViewModel navigationItemViewModel)
        {
            navigationItemViewModel.IsTreeItemExpanded = true;
            if (navigationItemViewModel.IsLink)
            {
                Navigate(ReadingOrder.FirstOrDefault(htmlContentFile => htmlContentFile.HtmlFilePathInEpubArchive == navigationItemViewModel.FilePathInEpubArchive));
                CurrentAnchor = navigationItemViewModel.Anchor;
            }
        }

        private void Navigate(HtmlContentFileViewModel targetHtmlContentFileViewModel)
        {
            if (targetHtmlContentFileViewModel == null)
            {
                CurrentHtmlContentFile = null;
                previousHtmlContentFile = null;
                nextHtmlContentFile = null;
            }
            else if (CurrentHtmlContentFile != targetHtmlContentFileViewModel)
            {
                CurrentHtmlContentFile = targetHtmlContentFileViewModel;
                int currentReadingOrderItemIndex = ReadingOrder.IndexOf(CurrentHtmlContentFile);
                if (currentReadingOrderItemIndex != -1)
                {
                    if (currentReadingOrderItemIndex > 0)
                    {
                        previousHtmlContentFile = ReadingOrder[currentReadingOrderItemIndex - 1];
                    }
                    else
                    {
                        previousHtmlContentFile = null;
                    }
                    if (currentReadingOrderItemIndex < ReadingOrder.Count - 1)
                    {
                        nextHtmlContentFile = ReadingOrder[currentReadingOrderItemIndex + 1];
                    }
                    else
                    {
                        nextHtmlContentFile = null;
                    }
                }
                else
                {
                    previousHtmlContentFile = null;
                    nextHtmlContentFile = null;
                }
            }
            NotifyPropertyChanged(nameof(IsPreviousButtonVisible));
            NotifyPropertyChanged(nameof(IsNextButtonVisible));
        }

        private void NavigateToPreviousItemInReadingOrder()
        {
            if (previousHtmlContentFile != null)
            {
                Navigate(previousHtmlContentFile);
            }
        }

        private void NavigateToNextItemInReadingOrder()
        {
            if (nextHtmlContentFile != null)
            {
                Navigate(nextHtmlContentFile);
            }
        }
    }
}
