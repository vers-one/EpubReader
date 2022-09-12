namespace VersOne.Epub.Test.Unit.Entities
{
    public class EpubNavigationItemTests
    {
        [Fact(DisplayName = "Creating an instance of EpubNavigationItem with type = HEADER should succeed")]
        public void CreateAsHeaderTest()
        {
            EpubNavigationItem epubNavigationItem = EpubNavigationItem.CreateAsHeader();
            Assert.Equal(EpubNavigationItemType.HEADER, epubNavigationItem.Type);
        }

        [Fact(DisplayName = "Creating an instance of EpubNavigationItem with type = LINK should succeed")]
        public void CreateAsLinkTest()
        {
            EpubNavigationItem epubNavigationItem = EpubNavigationItem.CreateAsLink();
            Assert.Equal(EpubNavigationItemType.LINK, epubNavigationItem.Type);
        }

        [Fact(DisplayName = "ToString method should produce a string with the type, the title, and the number of nested items")]
        public void ToStringTest()
        {
            EpubNavigationItem epubNavigationItem = EpubNavigationItem.CreateAsHeader();
            epubNavigationItem.Title = "Chapter 1";
            epubNavigationItem.NestedItems = new List<EpubNavigationItem>
            {
                EpubNavigationItem.CreateAsHeader()
            };
            Assert.Equal("Type: HEADER, Title: Chapter 1, NestedItems.Count: 1", epubNavigationItem.ToString());
        }
    }
}
