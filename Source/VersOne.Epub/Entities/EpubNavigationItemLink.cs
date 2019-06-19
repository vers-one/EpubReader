using VersOne.Epub.Internal;

namespace VersOne.Epub
{
    public class EpubNavigationItemLink
    {
        public EpubNavigationItemLink()
        {
        }

        public EpubNavigationItemLink(string url)
        {
            UrlParser urlParser = new UrlParser(url);
            ContentFileName = urlParser.Path;
            Anchor = urlParser.Anchor;
        }

        public string ContentFileName { get; set; }
        public string Anchor { get; set; }
    }
}
