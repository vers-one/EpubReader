using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EpubReaderDemo.ViewModels;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.WPF;

namespace EpubReaderDemo.Controls
{
    public class BookHtmlContent : HtmlPanel
    {
        public static readonly DependencyProperty ChapterContentProperty = DependencyProperty.Register("ChapterContent", typeof(ChapterContentViewModel), typeof(BookHtmlContent), new PropertyMetadata(OnChapterContentChanged));

        private bool areFontsRegistered;

        public BookHtmlContent()
        {
            areFontsRegistered = false;
        }

        public ChapterContentViewModel ChapterContent
        {
            get
            {
                return (ChapterContentViewModel)GetValue(ChapterContentProperty);
            }
            set
            {
                SetValue(ChapterContentProperty, value);
            }
        }

        protected override void OnImageLoad(HtmlImageLoadEventArgs e)
        {
            byte[] imageContent;
            if (ChapterContent.Images.TryGetValue(e.Src, out imageContent))
            {
                using (MemoryStream imageStream = new MemoryStream(imageContent))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = imageStream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                    e.Callback(bitmapImage);
                }
                e.Handled = true;
            }
            base.OnImageLoad(e);
        }

        protected override void OnStylesheetLoad(HtmlStylesheetLoadEventArgs e)
        {
            string styleSheetContent;
            if (ChapterContent.StyleSheets.TryGetValue(e.Src, out styleSheetContent))
                e.SetStyleSheet = styleSheetContent;
            base.OnStylesheetLoad(e);
        }

        private static void OnChapterContentChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            BookHtmlContent bookHtmlContent = dependencyObject as BookHtmlContent;
            if (bookHtmlContent == null || bookHtmlContent.ChapterContent == null)
                return;
            if (!bookHtmlContent.areFontsRegistered)
                bookHtmlContent.RegisterFonts();
            bookHtmlContent.Text = bookHtmlContent.ChapterContent.HtmlContent;
        }

        private void RegisterFonts()
        {
            foreach (KeyValuePair<string, byte[]> fontFile in ChapterContent.Fonts)
            {
                MemoryStream packageStream = new MemoryStream();
                Package package = Package.Open(packageStream, FileMode.Create, FileAccess.ReadWrite);
                Uri packageUri = new Uri(fontFile.Key + ":");
                PackageStore.AddPackage(packageUri, package);
                Uri packPartUri = new Uri("/content", UriKind.Relative);
                PackagePart packPart = package.CreatePart(packPartUri, "font/content");
                packPart.GetStream().Write(fontFile.Value, 0, fontFile.Value.Length);
                Uri fontUri = PackUriHelper.Create(packageUri, packPart.Uri);
                foreach (FontFamily fontFamilty in System.Windows.Media.Fonts.GetFontFamilies(fontUri))
                    HtmlRender.AddFontFamily(fontFamilty);
            }
            areFontsRegistered = true;
        }
    }
}
