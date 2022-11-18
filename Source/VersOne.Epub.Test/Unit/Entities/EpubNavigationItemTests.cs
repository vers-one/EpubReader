using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.TestData;

namespace VersOne.Epub.Test.Unit.Entities
{
    public class EpubNavigationItemTests
    {
        private const EpubNavigationItemType TYPE = EpubNavigationItemType.HEADER;
        private const string TITLE = "Chapter 1";

        private static EpubNavigationItemLink Link =>
            new
            (
                contentFileUrl: "../content/chapter1.html#section1",
                baseDirectoryPath: "OPS/toc"
            );

        private static List<EpubNavigationItem> NestedItems =>
            new()
            {
                new EpubNavigationItem
                (
                    type: EpubNavigationItemType.HEADER,
                    title: "Nested item",
                    link: null,
                    htmlContentFile: null,
                    nestedItems: null
                )
            };

        [Fact(DisplayName = "Constructing a EpubNavigationItem instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubNavigationItem epubNavigationItem = new(TYPE, TITLE, Link, TestEpubContent.Chapter1File, NestedItems);
            Assert.Equal(TYPE, epubNavigationItem.Type);
            Assert.Equal(TITLE, epubNavigationItem.Title);
            EpubNavigationItemComparer.CompareNavigationItemLinks(Link, epubNavigationItem.Link);
            EpubContentComparer.CompareEpubLocalTextContentFiles(TestEpubContent.Chapter1File, epubNavigationItem.HtmlContentFile);
            EpubNavigationItemComparer.CompareNavigationItemLists(NestedItems, epubNavigationItem.NestedItems);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if title parameter is null")]
        public void ConstructorWithNullTitleTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubNavigationItem(TYPE, null!, Link, TestEpubContent.Chapter1File, NestedItems));
        }

        [Fact(DisplayName = "Constructing a EpubNavigationItem instance with null link parameter should succeed")]
        public void ConstructorWithNullLinkParameterTest()
        {
            EpubNavigationItem epubNavigationItem = new(TYPE, TITLE, null, TestEpubContent.Chapter1File, NestedItems);
            Assert.Equal(TYPE, epubNavigationItem.Type);
            Assert.Equal(TITLE, epubNavigationItem.Title);
            EpubNavigationItemComparer.CompareNavigationItemLinks(null, epubNavigationItem.Link);
            EpubContentComparer.CompareEpubLocalTextContentFiles(TestEpubContent.Chapter1File, epubNavigationItem.HtmlContentFile);
            EpubNavigationItemComparer.CompareNavigationItemLists(NestedItems, epubNavigationItem.NestedItems);
        }

        [Fact(DisplayName = "Constructing a EpubNavigationItem instance with null htmlContentFile parameter should succeed")]
        public void ConstructorWithNullHtmlContentFileParameterTest()
        {
            EpubNavigationItem epubNavigationItem = new(TYPE, TITLE, Link, null, NestedItems);
            Assert.Equal(TYPE, epubNavigationItem.Type);
            Assert.Equal(TITLE, epubNavigationItem.Title);
            EpubNavigationItemComparer.CompareNavigationItemLinks(Link, epubNavigationItem.Link);
            EpubContentComparer.CompareEpubLocalTextContentFiles(null, epubNavigationItem.HtmlContentFile);
            EpubNavigationItemComparer.CompareNavigationItemLists(NestedItems, epubNavigationItem.NestedItems);
        }

        [Fact(DisplayName = "Constructing a EpubNavigationItem instance with null nestedItems parameter should succeed")]
        public void ConstructorWithNullNestedItemsParameterTest()
        {
            EpubNavigationItem epubNavigationItem = new(TYPE, TITLE, Link, TestEpubContent.Chapter1File, null);
            Assert.Equal(TYPE, epubNavigationItem.Type);
            Assert.Equal(TITLE, epubNavigationItem.Title);
            EpubNavigationItemComparer.CompareNavigationItemLinks(Link, epubNavigationItem.Link);
            EpubContentComparer.CompareEpubLocalTextContentFiles(TestEpubContent.Chapter1File, epubNavigationItem.HtmlContentFile);
            EpubNavigationItemComparer.CompareNavigationItemLists(new List<EpubNavigationItem>(), epubNavigationItem.NestedItems);
        }

        [Fact(DisplayName = "ToString method should produce a string with the type, the title, and the number of nested items")]
        public void ToStringTest()
        {
            EpubNavigationItem epubNavigationItem = new
            (
                type: TYPE,
                title: TITLE,
                link: null,
                htmlContentFile: null,
                nestedItems: NestedItems
            );
            Assert.Equal("Type: HEADER, Title: Chapter 1, NestedItems.Count: 1", epubNavigationItem.ToString());
        }
    }
}
