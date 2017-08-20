using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<ChapterViewModel> contents;
        private Dictionary<string, byte[]> images;
        private Dictionary<string, string> styleSheets;
        private Dictionary<string, byte[]> fonts;
        private ChapterViewModel selectedChapter;
        private ChapterContentViewModel selectedChapterContent;
        private Command selectChapterCommand;

        public BookViewModel(int bookId)
        {
            bookModel = new BookModel();
            isLoading = true;
            selectChapterCommand = null;
            selectedChapter = null;
            selectedChapterContent = null;
            bookModel.OpenBookAsync(bookId).ContinueWith(epubBook => BookOpened(epubBook), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public ObservableCollection<ChapterViewModel> Contents
        {
            get
            {
                return contents;
            }
            private set
            {
                contents = value;
                OnPropertyChanged(() => Contents);
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
                OnPropertyChanged(() => IsLoading);
            }
        }

        public ChapterContentViewModel SelectedChapterContent
        {
            get
            {
                return selectedChapterContent;
            }
            set
            {
                selectedChapterContent = value;
                OnPropertyChanged(() => SelectedChapterContent);
            }
        }

        public ICommand SelectChapterCommand
        {
            get
            {
                if (selectChapterCommand == null)
                {
                    selectChapterCommand = new Command(param => SelectChapter(param as ChapterViewModel));
                }
                return selectChapterCommand;
            }
        }

        private void BookOpened(Task<EpubBook> task)
        {
            EpubBook epubBook = task.Result;
            Contents = new ObservableCollection<ChapterViewModel>(bookModel.GetChapters(epubBook));
            images = epubBook.Content.Images.ToDictionary(imageFile => imageFile.Key, imageFile => imageFile.Value.Content);
            styleSheets = epubBook.Content.Css.ToDictionary(cssFile => cssFile.Key, cssFile => cssFile.Value.Content);
            fonts = epubBook.Content.Fonts.ToDictionary(fontFile => fontFile.Key, fontFile => fontFile.Value.Content);
            if (Contents.Any())
            {
                SelectChapter(Contents.First());
            }
            IsLoading = false;
        }

        private void SelectChapter(ChapterViewModel chapterViewModel)
        {
            if (selectedChapter != null)
            {
                selectedChapter.IsSelected = false;
            }
            selectedChapter = chapterViewModel;
            selectedChapter.IsTreeItemExpanded = true;
            selectedChapter.IsSelected = true;
            SelectedChapterContent = new ChapterContentViewModel(selectedChapter.FilePath, selectedChapter.HtmlContent, images, styleSheets, fonts);
        }
    }
}
