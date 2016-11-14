using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpubReaderDemo.Entities;
using EpubReaderDemo.ViewModels;
using VersFx.Formats.Text.Epub;

namespace EpubReaderDemo.Models
{
    internal class BookModel
    {
        private readonly ApplicationContext applicationContext;
        private readonly Settings settings;

        public BookModel()
        {
            applicationContext = ApplicationContext.Instance;
            settings = applicationContext.Settings;
        }

        public async Task<EpubBook> OpenBookAsync(int bookId)
        {
            EpubBook epubBook = await EpubReader.ReadBookAsync(settings.Books.First(book => book.Id == bookId).FilePath);
            return epubBook;
        }

        public List<ChapterViewModel> GetChapters(EpubBook epubBook)
        {
            return GetChapters(epubBook.Chapters);
        }

        private List<ChapterViewModel> GetChapters(List<EpubChapter> epubChapters)
        {
            List<ChapterViewModel> result = new List<ChapterViewModel>();
            foreach (EpubChapter epubChapter in epubChapters)
            {
                List<ChapterViewModel> subChapters = GetChapters(epubChapter.SubChapters);
                ChapterViewModel chapterViewModel = new ChapterViewModel(epubChapter.ContentFileName, epubChapter.Title, subChapters, epubChapter.HtmlContent);
                result.Add(chapterViewModel);
            }
            return result;
        }
    }
}
