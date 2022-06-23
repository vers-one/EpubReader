using System.Collections.Generic;

namespace VersOne.Epub
{
    public class EpubContent
    {
        public EpubByteContentFile Cover { get; set; }
        public EpubTextContentFile NavigationHtmlFile { get; set; }
        public Dictionary<string, EpubTextContentFile> Html { get; set; }
        public Dictionary<string, EpubTextContentFile> Css { get; set; }
        public Dictionary<string, EpubByteContentFile> Images { get; set; }
        public Dictionary<string, EpubByteContentFile> Fonts { get; set; }
        public Dictionary<string, EpubContentFile> AllFiles { get; set; }
    }
}
