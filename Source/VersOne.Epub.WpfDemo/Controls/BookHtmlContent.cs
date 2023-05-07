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
    /// <summary>
    /// WPF control that renders a single HTML content file of a EPUB book.
    /// </summary>
    public class BookHtmlContent : HtmlPanel
    {
        /// <summary>
        /// Identifies the <see cref="EpubArchive" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty EpubArchiveProperty = DependencyProperty.Register("EpubArchive", typeof(ZipArchive), typeof(BookHtmlContent));

        /// <summary>
        /// Identifies the <see cref="HtmlContentFile" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty HtmlContentFileProperty =
            DependencyProperty.Register("HtmlContentFile", typeof(HtmlContentFileViewModel), typeof(BookHtmlContent), new PropertyMetadata(OnHtmlContentFileChanged));

        /// <summary>
        /// Identifies the <see cref="Anchor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnchorProperty = DependencyProperty.Register("Anchor", typeof(string), typeof(BookHtmlContent), new PropertyMetadata(OnAnchorChanged));

        private bool areFontsRegistered;
        private bool isContentLoaded;
        private string queuedScrollToAnchor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookHtmlContent" /> class.
        /// </summary>
        public BookHtmlContent()
        {
            areFontsRegistered = false;
            isContentLoaded = false;
            queuedScrollToAnchor = null;
        }

        /// <summary>
        /// Gets or sets the EPUB archive that contains the HTML content file that needs to be rendered.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the HTML content file that needs to be rendered.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the anchor inside the HTML content file that needs to be scrolled to once the content is fully loaded.
        /// </summary>
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

        /// <summary>
        /// Loads the content of the image from the EPUB archive.
        /// </summary>
        /// <param name="e">An event parameter identifying the image that needs to be loaded.</param>
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

        /// <summary>
        /// Loads the content of the CSS stylesheet from the EPUB archive.
        /// </summary>
        /// <param name="e">An event parameter identifying the CSS stylesheet that needs to be loaded.</param>
        protected override void OnStylesheetLoad(HtmlStylesheetLoadEventArgs e)
        {
            string styleSheetContent = GetStyleSheetContent(e.Src);
            if (styleSheetContent != null)
            {
                e.SetStyleSheet = styleSheetContent;
            }
            base.OnStylesheetLoad(e);
        }

        /// <summary>
        /// Scrolls the rendered HTML content to the <see cref="Anchor" /> if needed.
        /// </summary>
        /// <param name="e">An parameter containing event data.</param>
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
            string fullPath = String.Concat(basePath.Replace('\\', '/'), '/', relativePath).TrimStart('/');
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
