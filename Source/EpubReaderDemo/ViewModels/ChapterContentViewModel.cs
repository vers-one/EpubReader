using System.Collections.Generic;

namespace EpubReaderDemo.ViewModels
{
    public class ChapterContentViewModel : ViewModel
    {
        public ChapterContentViewModel(string htmlFilePath, string htmlContent, Dictionary<string, byte[]> images, Dictionary<string, string> styleSheets, Dictionary<string, byte[]> fonts)
        {
            HtmlFilePath = htmlFilePath;
            HtmlContent = htmlContent;
            Images = images;
            StyleSheets = styleSheets;
            Fonts = fonts;
        }

        public string HtmlFilePath { get; private set; }
        public string HtmlContent { get; private set; }
        public Dictionary<string, byte[]> Images { get; private set; }
        public Dictionary<string, string> StyleSheets { get; private set; }
        public Dictionary<string, byte[]> Fonts { get; private set; }
    }
}
