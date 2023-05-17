using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VersOne.Epub.Options;
using VersOne.Epub.WpfDemo.Entities;
using VersOne.Epub.WpfDemo.ViewModels;

namespace VersOne.Epub.WpfDemo.Models
{
    internal class BookModel
    {
        private readonly Settings settings;

        public BookModel()
        {
            settings = ApplicationContext.Instance.Settings;
        }

        public async Task<EpubBook> OpenBookAsync(int bookId)
        {
            EpubReaderOptions epubReaderOptions = new EpubReaderOptions();
            epubReaderOptions.ContentReaderOptions.ContentFileMissing += (sender, e) =>
            {
                e.SuppressException = true;
            };
            EpubBook epubBook = await EpubReader.ReadBookAsync(settings.Books.First(book => book.Id == bookId).FilePath, epubReaderOptions);
            return epubBook;
        }

        public List<NavigationItemViewModel> GetNavigation(EpubBook epubBook)
        {
            return GetNavigation(epubBook.Navigation);
        }

        public List<HtmlContentFileViewModel> GetReadingOrder(EpubBook epubBook)
        {
            Dictionary<string, byte[]> images = epubBook.Content.Images.Local.ToDictionary(imageFile => imageFile.Key, imageFile => imageFile.Content);
            Dictionary<string, string> styleSheets = epubBook.Content.Css.Local.ToDictionary(cssFile => cssFile.Key, cssFile => cssFile.Content);
            Dictionary<string, byte[]> fonts = epubBook.Content.Fonts.Local.ToDictionary(fontFile => fontFile.Key, fontFile => fontFile.Content);
            List<HtmlContentFileViewModel> result = new List<HtmlContentFileViewModel>();
            foreach (EpubLocalTextContentFile epubHtmlFile in epubBook.ReadingOrder)
            {
                HtmlContentFileViewModel htmlContentFileViewModel =
                    new HtmlContentFileViewModel(epubHtmlFile.Key, epubHtmlFile.FilePath, epubHtmlFile.Content, images, styleSheets, fonts);
                result.Add(htmlContentFileViewModel);
            }
            return result;
        }

        private List<NavigationItemViewModel> GetNavigation(List<EpubNavigationItem> epubNavigationItems)
        {
            List<NavigationItemViewModel> result = new List<NavigationItemViewModel>();
            foreach (EpubNavigationItem epubNavigationItem in epubNavigationItems)
            {
                List<NavigationItemViewModel> nestedItems = GetNavigation(epubNavigationItem.NestedItems);
                NavigationItemViewModel navigationItemViewModel;
                if (epubNavigationItem.Type == EpubNavigationItemType.HEADER)
                {
                    navigationItemViewModel = new NavigationItemViewModel(epubNavigationItem.Title, nestedItems);
                }
                else
                {
                    navigationItemViewModel = new NavigationItemViewModel(epubNavigationItem.Title, epubNavigationItem.Link.ContentFilePath, epubNavigationItem.Link.Anchor, nestedItems);
                }
                result.Add(navigationItemViewModel);
            }
            return result;
        }
    }
}
