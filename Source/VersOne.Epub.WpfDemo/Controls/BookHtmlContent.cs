using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.WPF;
using VersOne.Epub.WpfDemo.ViewModels;

namespace VersOne.Epub.WpfDemo.Controls
{
    public class BookHtmlContent : HtmlPanel
    {
        public static readonly DependencyProperty EpubArchiveProperty = DependencyProperty.Register("EpubArchive", typeof(ZipArchive), typeof(BookHtmlContent));
        public static readonly DependencyProperty HtmlContentFileProperty = DependencyProperty.Register("HtmlContentFile", typeof(HtmlContentFileViewModel), typeof(BookHtmlContent), new PropertyMetadata(OnHtmlContentFileChanged));
        public static readonly DependencyProperty AnchorProperty = DependencyProperty.Register("Anchor", typeof(string), typeof(BookHtmlContent), new PropertyMetadata(OnAnchorChanged));

        private bool areFontsRegistered;
        private bool isContentLoaded;
        private string queuedScrollToAnchor;

        public BookHtmlContent()
        {
            areFontsRegistered = false;
            isContentLoaded = false;
            queuedScrollToAnchor = null;
        }

        public ZipArchive EpubArchive
        {
            get
            {
                return (ZipArchive)GetValue(EpubArchiveProperty);
            }
            set
            {
                SetValue(EpubArchiveProperty, value);
            }
        }

        public HtmlContentFileViewModel HtmlContentFile
        {
            get
            {
                return (HtmlContentFileViewModel)GetValue(HtmlContentFileProperty);
            }
            set
            {
                SetValue(HtmlContentFileProperty, value);
            }
        }

        public string Anchor
        {
            get
            {
                return (string)GetValue(AnchorProperty);
            }
            set
            {
                SetValue(AnchorProperty, value);
            }
        }

        protected override void OnImageLoad(HtmlImageLoadEventArgs e)
        {
            byte[] imageContent = GetImageContent(e.Src);
            if (imageContent != null)
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
            string styleSheetContent = GetStyleSheetContent(e.Src);
            if (styleSheetContent != null)
            {
                e.SetStyleSheet = styleSheetContent;
            }
            base.OnStylesheetLoad(e);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (queuedScrollToAnchor != null)
            {
                ScrollToElement(queuedScrollToAnchor);
                queuedScrollToAnchor = null;
            }
            isContentLoaded = true;
        }

        private static void OnAnchorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (!(dependencyObject is BookHtmlContent bookHtmlContent) || bookHtmlContent.HtmlContentFile == null || bookHtmlContent.Anchor == null)
            {
                return;
            }
            if (bookHtmlContent.isContentLoaded)
            {
                bookHtmlContent.ScrollToElement(bookHtmlContent.Anchor);
            }
            else
            {
                bookHtmlContent.queuedScrollToAnchor = bookHtmlContent.Anchor;
            }
        }

        private static void OnHtmlContentFileChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (!(dependencyObject is BookHtmlContent bookHtmlContent) || bookHtmlContent.HtmlContentFile == null)
            {
                return;
            }
            if (!bookHtmlContent.areFontsRegistered)
            {
                bookHtmlContent.RegisterFonts();
            }
            bookHtmlContent.isContentLoaded = false;
            bookHtmlContent.queuedScrollToAnchor = null;
            bookHtmlContent.Text = bookHtmlContent.HtmlContentFile.HtmlContent;
        }

        private byte[] GetImageContent(string imageFilePath)
        {
            if (HtmlContentFile.Images.TryGetValue(GetFullPath(HtmlContentFile.HtmlFilePathInEpubManifest, imageFilePath), out byte[] imageContent))
            {
                return imageContent;
            }
            ZipArchiveEntry zipArchiveEntry = EpubArchive.GetEntry(GetFullPath(HtmlContentFile.HtmlFilePathInEpubArchive, imageFilePath));
            if (zipArchiveEntry != null)
            {
                imageContent = new byte[(int)zipArchiveEntry.Length];
                using (Stream zipArchiveEntryStream = zipArchiveEntry.Open())
                using (MemoryStream memoryStream = new MemoryStream(imageContent))
                {
                    zipArchiveEntryStream.CopyTo(memoryStream);
                }
                return imageContent;
            }
            return null;
        }

        private string GetStyleSheetContent(string styleSheetFilePath)
        {
            if (HtmlContentFile.StyleSheets.TryGetValue(GetFullPath(HtmlContentFile.HtmlFilePathInEpubManifest, styleSheetFilePath), out string styleSheetContent))
            {
                return styleSheetContent;
            }
            ZipArchiveEntry zipArchiveEntry = EpubArchive.GetEntry(GetFullPath(HtmlContentFile.HtmlFilePathInEpubArchive, styleSheetFilePath));
            if (zipArchiveEntry != null)
            {
                using (Stream zipArchiveEntryStream = zipArchiveEntry.Open())
                using (StreamReader streamReader = new StreamReader(zipArchiveEntryStream))
                {
                    styleSheetContent = streamReader.ReadToEnd();
                }
                return styleSheetContent;
            }
            return null;
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
            if (fullPath.StartsWith("/"))
            {
                fullPath = fullPath.Length > 1 ? fullPath.Substring(1) : String.Empty;
            }
            return fullPath;
        }

        private void RegisterFonts()
        {
            foreach (KeyValuePair<string, byte[]> fontFile in HtmlContentFile.Fonts)
            {
                MemoryStream packageStream = new MemoryStream();
                Package package = Package.Open(packageStream, FileMode.Create, FileAccess.ReadWrite);
                Uri packageUri = new Uri("fonts://" + fontFile.Key);
                PackageStore.AddPackage(packageUri, package);
                Uri packPartUri = new Uri("/content", UriKind.Relative);
                PackagePart packPart = package.CreatePart(packPartUri, "font/content");
                packPart.GetStream().Write(fontFile.Value, 0, fontFile.Value.Length);
                Uri fontUri = PackUriHelper.Create(packageUri, packPart.Uri);
                foreach (FontFamily fontFamily in Fonts.GetFontFamilies(fontUri))
                {
                    HtmlRender.AddFontFamily(fontFamily);
                }
            }
            areFontsRegistered = true;
        }
    }
}
