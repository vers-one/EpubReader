using System.Collections.Generic;

namespace VersOne.Epub.WpfDemo.ViewModels
{
    public class HtmlContentFileViewModel : ViewModel
    {
        public HtmlContentFileViewModel(string htmlFilePath, string htmlContent, Dictionary<string, byte[]> images, Dictionary<string, string> styleSheets, Dictionary<string, byte[]> fonts)
        {
            HtmlFilePath = htmlFilePath;
            HtmlContent = htmlContent;
            Images = images;
            StyleSheets = styleSheets;
            Fonts = fonts;
        }

        public string HtmlFilePath { get; }
        public string HtmlContent { get; }
        public Dictionary<string, byte[]> Images { get; }
        public Dictionary<string, string> StyleSheets { get; }
        public Dictionary<string, byte[]> Fonts { get; }
    }
}
