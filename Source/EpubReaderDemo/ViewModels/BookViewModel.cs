using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EpubReaderDemo.Models;
using EpubReaderDemo.Utils;
using VersFx.Formats.Text.Epub;

namespace EpubReaderDemo.ViewModels
{
    public class BookViewModel : ViewModel
    {
        private readonly BookModel bookModel;
        private readonly EpubBook epubBook;

        private Dictionary<string, byte[]> images;
        private Dictionary<string, string> styleSheets;
        private Dictionary<string, byte[]> fonts;
        private ChapterViewModel selectedChapter;
        private ChapterContentViewModel selectedChapterContent;
        private Command selectChapterCommand;

        public BookViewModel(int bookId)
        {
            bookModel = new BookModel();
            epubBook = bookModel.OpenBook(bookId);
            Contents = new ObservableCollection<ChapterViewModel>(bookModel.GetChapters(epubBook));
            images = epubBook.Content.Images.ToDictionary(imageFile => imageFile.Key, imageFile => imageFile.Value.Content);
            styleSheets = epubBook.Content.Css.ToDictionary(cssFile => cssFile.Key, cssFile => cssFile.Value.Content);
            fonts = epubBook.Content.Fonts.ToDictionary(fontFile => fontFile.Key, fontFile => fontFile.Value.Content);
            selectChapterCommand = null;
            selectedChapter = null;
            selectedChapterContent = null;
            if (Contents.Any())
                SelectChapter(Contents.First());
        }

        public ObservableCollection<ChapterViewModel> Contents { get; private set; }

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
                    selectChapterCommand = new Command(param => SelectChapter(param as ChapterViewModel));
                return selectChapterCommand;
            }
        }

        private void SelectChapter(ChapterViewModel chapterViewModel)
        {
            if (selectedChapter != null)
                selectedChapter.IsSelected = false;
            selectedChapter = chapterViewModel;
            selectedChapter.IsTreeItemExpanded = true;
            selectedChapter.IsSelected = true;
            SelectedChapterContent = new ChapterContentViewModel(selectedChapter.HtmlContent, images, styleSheets, fonts);
        }
    }
}
