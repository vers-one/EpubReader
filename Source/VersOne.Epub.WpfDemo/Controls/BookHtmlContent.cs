using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VersOne.Epub.WpfDemo.ViewModels;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.WPF;
using System.Diagnostics;

namespace VersOne.Epub.WpfDemo.Controls
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
            string imageFilePath = GetFullPath(ChapterContent.HtmlFilePath, e.Src);
            if (ChapterContent.Images.TryGetValue(imageFilePath, out byte[] imageContent))
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
            string styleSheetFilePath = GetFullPath(ChapterContent.HtmlFilePath, e.Src);
            if (ChapterContent.StyleSheets.TryGetValue(styleSheetFilePath, out string styleSheetContent))
            {
                e.SetStyleSheet = styleSheetContent;
            }
            base.OnStylesheetLoad(e);
        }

        private static void OnChapterContentChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            BookHtmlContent bookHtmlContent = dependencyObject as BookHtmlContent;
            if (bookHtmlContent == null || bookHtmlContent.ChapterContent == null)
            {
                return;
            }
            if (!bookHtmlContent.areFontsRegistered)
            {
                bookHtmlContent.RegisterFonts();
            }
            bookHtmlContent.Text = bookHtmlContent.ChapterContent.HtmlContent;
        }

        private string GetFullPath(string htmlFilePath, string relativePath)
        {
            if (relativePath.StartsWith("/"))
            {
                return relativePath.Length > 1 ? relativePath.Substring(1) : String.Empty;
            }
            string basePath = Path.GetDirectoryName(htmlFilePath);
            while (relativePath.StartsWith("../"))
            {
                relativePath = relativePath.Length > 3 ? relativePath.Substring(3) : String.Empty;
                basePath = Path.GetDirectoryName(basePath);
            }
            string fullPath = String.Concat(basePath.Replace('\\', '/'), '/', relativePath);
            return fullPath.Length > 1 ? fullPath.StartsWith("/") ? fullPath.Substring(1) : fullPath : String.Empty;
        }

        private void RegisterFonts()
        {
            foreach (KeyValuePair<string, byte[]> fontFile in ChapterContent.Fonts)
            {
                try
                {
                    MemoryStream packageStream = new MemoryStream();
                    Package package = Package.Open(packageStream, FileMode.Create, FileAccess.ReadWrite);
                    Uri packageUri = new Uri(fontFile.Key + ":");
                    PackageStore.AddPackage(packageUri, package);
                    Uri packPartUri = new Uri("/content", UriKind.Relative);
                    PackagePart packPart = package.CreatePart(packPartUri, "font/content");
                    packPart.GetStream().Write(fontFile.Value, 0, fontFile.Value.Length);
                    Uri fontUri = PackUriHelper.Create(packageUri, packPart.Uri);
                    foreach (FontFamily fontFamilty in Fonts.GetFontFamilies(fontUri))
                    {
                        HtmlRender.AddFontFamily(fontFamilty);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            areFontsRegistered = true;
        }
    }
}
