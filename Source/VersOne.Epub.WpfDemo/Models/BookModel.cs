﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VersOne.Epub.WpfDemo.Entities;
using VersOne.Epub.WpfDemo.ViewModels;

namespace VersOne.Epub.WpfDemo.Models
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

        public List<NavigationItemViewModel> GetNavigation(EpubBook epubBook)
        {
            return GetNavigation(epubBook.Navigation);
        }

        public List<HtmlContentFileViewModel> GetReadingOrder(EpubBook epubBook)
        {
            Dictionary<string, byte[]> images = epubBook.Content.Images.ToDictionary(imageFile => imageFile.Key, imageFile => imageFile.Value.Content);
            Dictionary<string, string> styleSheets = epubBook.Content.Css.ToDictionary(cssFile => cssFile.Key, cssFile => cssFile.Value.Content);
            Dictionary<string, byte[]> fonts = epubBook.Content.Fonts.ToDictionary(fontFile => fontFile.Key, fontFile => fontFile.Value.Content);
            List<HtmlContentFileViewModel> result = new List<HtmlContentFileViewModel>();
            foreach (EpubTextContentFile epubHtmlFile in epubBook.ReadingOrder)
            {
                HtmlContentFileViewModel htmlContentFileViewModel = new HtmlContentFileViewModel(epubHtmlFile.FileName, epubHtmlFile.Content, images,
                    styleSheets, fonts);
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
                    navigationItemViewModel = new NavigationItemViewModel(epubNavigationItem.Title, epubNavigationItem.Link.ContentFileName,
                        epubNavigationItem.Link.Anchor, nestedItems);
                }
                result.Add(navigationItemViewModel);
            }
            return result;
        }
    }
}
