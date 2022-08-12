using System.Collections.Generic;

namespace VersOne.Epub.WpfDemo.ViewModels
{
    /// <summary>
    /// View model for the <see cref="Controls.BookHtmlContent" /> control.
    /// </summary>
    public class HtmlContentFileViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlContentFileViewModel" /> class using the specified relative file path in the EPUB manifest,
        /// absolute file path in the EPUB archive, HTML content, and dictionaries of images, stylesheets, and fonts.
        /// </summary>
        /// <param name="htmlFilePathInEpubManifest">The relative file path of the HTML content file as it is specified in the EPUB manifest.</param>
        /// <param name="htmlFilePathInEpubArchive">The absolute file path of the HTML content file in the EPUB archive.</param>
        /// <param name="htmlContent">The content of the HTML file that needs to be rendered in the <see cref="Controls.BookHtmlContent" /> control.</param>
        /// <param name="images">The dictionary of image files in the EPUB book keyed by their relative file paths in the EPUB manifest.</param>
        /// <param name="styleSheets">The dictionary of CSS stylesheet files in the EPUB book keyed by their relative file paths in the EPUB manifest.</param>
        /// <param name="fonts">The dictionary of font files in the EPUB book keyed by their relative file paths in the EPUB manifest.</param>
        public HtmlContentFileViewModel(string htmlFilePathInEpubManifest, string htmlFilePathInEpubArchive, string htmlContent, Dictionary<string, byte[]> images,
            Dictionary<string, string> styleSheets, Dictionary<string, byte[]> fonts)
        {
            HtmlFilePathInEpubManifest = htmlFilePathInEpubManifest;
            HtmlFilePathInEpubArchive = htmlFilePathInEpubArchive;
            HtmlContent = htmlContent;
            Images = images;
            StyleSheets = styleSheets;
            Fonts = fonts;
        }

        /// <summary>
        /// Gets the relative file path of the HTML content file as it is specified in the EPUB manifest.
        /// </summary>
        public string HtmlFilePathInEpubManifest { get; }

        /// <summary>
        /// Gets the absolute file path of the HTML content file in the EPUB archive.
        /// </summary>
        public string HtmlFilePathInEpubArchive { get; }

        /// <summary>
        /// Gets the content of the HTML file to be rendered in the <see cref="Controls.BookHtmlContent" /> control.
        /// </summary>
        public string HtmlContent { get; }

        /// <summary>
        /// Gets the dictionary of image files in the EPUB book keyed by their relative file paths in the EPUB manifest.
        /// </summary>
        public Dictionary<string, byte[]> Images { get; }

        /// <summary>
        /// Gets the dictionary of CSS stylesheet files in the EPUB book keyed by their relative file paths in the EPUB manifest.
        /// </summary>
        public Dictionary<string, string> StyleSheets { get; }

        /// <summary>
        /// Gets the dictionary of font files in the EPUB book keyed by their relative file paths in the EPUB manifest.
        /// </summary>
        public Dictionary<string, byte[]> Fonts { get; }
    }
}
