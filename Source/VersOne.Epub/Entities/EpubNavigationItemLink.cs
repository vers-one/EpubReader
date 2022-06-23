using VersOne.Epub.Internal;

namespace VersOne.Epub
{
    public class EpubNavigationItemLink
    {
        public EpubNavigationItemLink(string contentFileUrl, string baseDirectoryPath)
        {
            UrlParser urlParser = new UrlParser(contentFileUrl);
            ContentFileName = urlParser.Path;
            ContentFilePathInEpubArchive = ZipPathUtils.Combine(baseDirectoryPath, ContentFileName);
            Anchor = urlParser.Anchor;
        }

        public string ContentFileName { get; set; }
        public string ContentFilePathInEpubArchive { get; set; }
        public string Anchor { get; set; }
    }
}
