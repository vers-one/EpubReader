namespace VersOne.Epub.Test.Unit.RefEntities
{
    public class EpubNavigationItemRefTests
    {
        [Fact(DisplayName = "Creating an instance of EpubNavigationItemRef with type = HEADER should succeed")]
        public void CreateAsHeaderTest()
        {
            EpubNavigationItemRef epubNavigationItemRef = EpubNavigationItemRef.CreateAsHeader();
            Assert.Equal(EpubNavigationItemType.HEADER, epubNavigationItemRef.Type);
        }

        [Fact(DisplayName = "Creating an instance of EpubNavigationItemRef with type = LINK should succeed")]
        public void CreateAsLinkTest()
        {
            EpubNavigationItemRef epubNavigationItemRef = EpubNavigationItemRef.CreateAsLink();
            Assert.Equal(EpubNavigationItemType.LINK, epubNavigationItemRef.Type);
        }

        [Fact(DisplayName = "ToString method should produce a string with the type, the title, and the number of nested items")]
        public void ToStringTest()
        {
            EpubNavigationItemRef epubNavigationItemRef = EpubNavigationItemRef.CreateAsHeader();
            epubNavigationItemRef.Title = "Chapter 1";
            epubNavigationItemRef.NestedItems = new List<EpubNavigationItemRef>
            {
                EpubNavigationItemRef.CreateAsHeader()
            };
            Assert.Equal("Type: HEADER, Title: Chapter 1, NestedItems.Count: 1", epubNavigationItemRef.ToString());
        }
    }
}
