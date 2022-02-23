using System.Collections.Generic;

namespace VersOne.Epub
{
    public class EpubNavigationItem
    {
        public EpubNavigationItem(EpubNavigationItemType type)
        {
            Type = type;
        }

        public EpubNavigationItemType Type { get; }
        public string Title { get; set; }
        public EpubNavigationItemLink Link { get; set; }
        public EpubTextContentFile HtmlContentFile { get; set; }
        public List<EpubNavigationItem> NestedItems { get; set; }

        public static EpubNavigationItem CreateAsHeader()
        {
            return new EpubNavigationItem(EpubNavigationItemType.HEADER);
        }

        public static EpubNavigationItem CreateAsLink()
        {
            return new EpubNavigationItem(EpubNavigationItemType.LINK);
        }

        public override string ToString()
        {
            return $"Type: {Type}, Title: {Title}, NestedItems.Count: {NestedItems.Count}";
        }
    }
}
