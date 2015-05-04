using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpubReaderDemo.ViewModels
{
    public class ChapterContentViewModel : ViewModel
    {
        public ChapterContentViewModel(string htmlContent, Dictionary<string, byte[]> images, Dictionary<string, string> styleSheets, Dictionary<string, byte[]> fonts)
        {
            HtmlContent = htmlContent;
            Images = images;
            StyleSheets = styleSheets;
            Fonts = fonts;
        }

        public string HtmlContent { get; private set; }
        public Dictionary<string, byte[]> Images { get; private set; }
        public Dictionary<string, string> StyleSheets { get; private set; }
        public Dictionary<string, byte[]> Fonts { get; private set; }
    }
}
