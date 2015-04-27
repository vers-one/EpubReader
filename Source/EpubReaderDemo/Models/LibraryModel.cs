using EpubReaderDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpubReaderDemo.Models
{
    internal class LibraryModel
    {
        private const string COVER_IMAGES_FOLDER = "Covers";

        private readonly ApplicationContext applicationContext;

        public LibraryModel()
        {
            applicationContext = ApplicationContext.Instance;
        }

        public List<LibraryItemViewModel> GetLibraryItems()
        {
            return applicationContext.Settings.Books.Select(book => new LibraryItemViewModel
            {
                Title = book.Title,
                CoverImageSource = Path.Combine(COVER_IMAGES_FOLDER, String.Format("{0}.png", book.Id))
            }).ToList();
        }
    }
}
