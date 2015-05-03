using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using EpubReaderDemo.Entities;
using EpubReaderDemo.ViewModels;
using VersFx.Formats.Text.Epub;

namespace EpubReaderDemo.Models
{
    internal class LibraryModel
    {
        private readonly ApplicationContext applicationContext;

        public LibraryModel()
        {
            applicationContext = ApplicationContext.Instance;
        }

        public List<LibraryItemViewModel> GetLibraryItems()
        {
            return applicationContext.Settings.Books.Select(book => new LibraryItemViewModel
            {
                Id = book.Id,
                Title = book.Title,
                CoverImage = book.HasCover ? ReadFile(GetBookCoverImageFilePath(book.Id)) : ReadResource(Constants.GENERIC_COVER_IMAGE_SOURCE)
            }).ToList();
        }

        public void AddBookToLibrary(string bookFilePath)
        {
            int bookId;
            if (applicationContext.Settings.Books.Any())
                bookId = applicationContext.Settings.Books.Max(bookItem => bookItem.Id) + 1;
            else
                bookId = 1;
            EpubReader epubReader = new EpubReader();
            EpubBook epubBook = epubReader.OpenBook(bookFilePath);
            Image bookCoverImage = epubReader.LoadCoverImage(epubBook);
            if (bookCoverImage != null)
            {
                if (!Directory.Exists(Constants.COVER_IMAGES_FOLDER))
                    Directory.CreateDirectory(Constants.COVER_IMAGES_FOLDER);
                using (Image resizedCoverImage = ResizeCover(bookCoverImage))
                    resizedCoverImage.Save(GetBookCoverImageFilePath(bookId), ImageFormat.Png);
                bookCoverImage.Dispose();
            }
            Book book = new Book
            {
                Id = bookId,
                FilePath = bookFilePath,
                Title = epubBook.Title,
                HasCover = bookCoverImage != null
            };
            applicationContext.Settings.Books.Add(book);
            applicationContext.SaveSettings();
        }

        public void RemoveBookFromLibrary(LibraryItemViewModel libraryItemViewModel)
        {
            string bookCoverImageFilePath = GetBookCoverImageFilePath(libraryItemViewModel.Id);
            if (File.Exists(bookCoverImageFilePath))
                File.Delete(bookCoverImageFilePath);
            applicationContext.Settings.Books.Remove(applicationContext.Settings.Books.First(book => book.Id == libraryItemViewModel.Id));
            applicationContext.SaveSettings();
        }

        private string GetBookCoverImageFilePath(int bookId)
        {
            return Path.Combine(applicationContext.CurrentDirectory, Constants.COVER_IMAGES_FOLDER, String.Format("{0}.png", bookId));
        }

        private Bitmap ResizeCover(Image image)
        {
            int width;
            int height;
            if (image.Width > Constants.COVER_IMAGE_MAX_WIDTH || image.Height > Constants.COVER_IMAGE_MAX_HEIGHT)
            {
                double zoom = Math.Max(image.Width / Constants.COVER_IMAGE_MAX_WIDTH, image.Height / Constants.COVER_IMAGE_MAX_HEIGHT);
                width = (int)Math.Truncate(image.Width / zoom);
                height = (int)Math.Truncate(image.Height / zoom);
            }
            else
            {
                width = image.Width;
                height = image.Height;
            }
            Bitmap result = new Bitmap(width, height);
            Rectangle resultRectangle = new Rectangle(0, 0, width, height);
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(image, resultRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            }
            return result;
        }

        private byte[] ReadFile(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private byte[] ReadResource(string resourceName)
        {
            using (Stream resourceStream = Application.GetResourceStream(new Uri(resourceName, UriKind.Relative)).Stream)
            using (MemoryStream memoryStream = new MemoryStream())
            {
                resourceStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
