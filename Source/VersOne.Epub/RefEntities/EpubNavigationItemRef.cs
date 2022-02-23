using System.Collections.Generic;

namespace VersOne.Epub
{
    public class EpubNavigationItemRef
    {
        public EpubNavigationItemRef(EpubNavigationItemType type)
        {
            Type = type;
        }

        public EpubNavigationItemType Type { get; }
        public string Title { get; set; }
        public EpubNavigationItemLink Link { get; set; }
        public EpubTextContentFileRef HtmlContentFileRef { get; set; }
        public List<EpubNavigationItemRef> NestedItems { get; set; }

        public static EpubNavigationItemRef CreateAsHeader()
        {
            return new EpubNavigationItemRef(EpubNavigationItemType.HEADER);
        }

        public static EpubNavigationItemRef CreateAsLink()
        {
            return new EpubNavigationItemRef(EpubNavigationItemType.LINK);
        }

        public override string ToString()
        {
            return $"Type: {Type}, Title: {Title}, NestedItems.Count: {NestedItems.Count}";
        }
    }
}
