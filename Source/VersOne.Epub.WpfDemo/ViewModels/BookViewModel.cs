using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VersOne.Epub.WpfDemo.Models;
using VersOne.Epub.WpfDemo.Utils;

namespace VersOne.Epub.WpfDemo.ViewModels
{
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

        public bool IsPreviousButtonVisible
        {
            get
            {
                return previousHtmlContentFile != null;
            }
        }

        public bool IsNextButtonVisible
        {
            get
            {
                return nextHtmlContentFile != null;
            }
        }

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
            if (currentEpubArchive != null)
            {
                currentEpubArchive.Dispose();
            }
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
