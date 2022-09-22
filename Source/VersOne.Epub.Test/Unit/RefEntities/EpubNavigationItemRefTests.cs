namespace VersOne.Epub.Test.Unit.RefEntities
{
    public class EpubNavigationItemRefTests
    {
        [Fact(DisplayName = "ToString method should produce a string with the type, the title, and the number of nested items")]
        public void ToStringTest()
        {
            EpubNavigationItemRef epubNavigationItemRef = new()
            {
                Type = EpubNavigationItemType.HEADER,
                Title = "Chapter 1",
                NestedItems = new List<EpubNavigationItemRef>
                {
                    new EpubNavigationItemRef()
                }
            };
            Assert.Equal("Type: HEADER, Title: Chapter 1, NestedItems.Count: 1", epubNavigationItemRef.ToString());
        }
    }
}
