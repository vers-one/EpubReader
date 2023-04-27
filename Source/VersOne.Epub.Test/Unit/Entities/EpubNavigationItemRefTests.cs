using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.TestData;

namespace VersOne.Epub.Test.Unit.Entities
{
    public class EpubNavigationItemRefTests
    {
        private const EpubNavigationItemType TYPE = EpubNavigationItemType.HEADER;
        private const string TITLE = "Chapter 1";

        private static EpubNavigationItemLink Link =>
            new
            (
                contentFileUrl: "../content/chapter1.html#section1",
                baseDirectoryPath: "OPS/toc"
            );

        private static List<EpubNavigationItemRef> NestedItems =>
            new()
            {
                new EpubNavigationItemRef
                (
                    type: EpubNavigationItemType.HEADER,
                    title: "Nested item"
                )
            };

        [Fact(DisplayName = "Constructing a EpubNavigationItemRef instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubNavigationItemRef epubNavigationItemRef = new(TYPE, TITLE, Link, TestEpubContentRef.Chapter1FileRef, NestedItems);
            Assert.Equal(TYPE, epubNavigationItemRef.Type);
            Assert.Equal(TITLE, epubNavigationItemRef.Title);
            EpubNavigationItemComparer.CompareNavigationItemLinks(Link, epubNavigationItemRef.Link);
            EpubContentRefComparer.CompareEpubLocalContentFileRefs(TestEpubContentRef.Chapter1FileRef, epubNavigationItemRef.HtmlContentFileRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(NestedItems, epubNavigationItemRef.NestedItems);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if title parameter is null")]
        public void ConstructorWithNullTitleTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubNavigationItemRef(TYPE, null!, Link, TestEpubContentRef.Chapter1FileRef, NestedItems));
        }

        [Fact(DisplayName = "Constructing a EpubNavigationItemRef instance with null link parameter should succeed")]
        public void ConstructorWithNullLinkParameterTest()
        {
            EpubNavigationItemRef epubNavigationItemRef = new(TYPE, TITLE, null, TestEpubContentRef.Chapter1FileRef, NestedItems);
            Assert.Equal(TYPE, epubNavigationItemRef.Type);
            Assert.Equal(TITLE, epubNavigationItemRef.Title);
            EpubNavigationItemComparer.CompareNavigationItemLinks(null, epubNavigationItemRef.Link);
            EpubContentRefComparer.CompareEpubLocalContentFileRefs(TestEpubContentRef.Chapter1FileRef, epubNavigationItemRef.HtmlContentFileRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(NestedItems, epubNavigationItemRef.NestedItems);
        }

        [Fact(DisplayName = "Constructing a EpubNavigationItemRef instance with null htmlContentFileRef parameter should succeed")]
        public void ConstructorWithNullHtmlContentFileRefParameterTest()
        {
            EpubNavigationItemRef epubNavigationItemRef = new(TYPE, TITLE, Link, null, NestedItems);
            Assert.Equal(TYPE, epubNavigationItemRef.Type);
            Assert.Equal(TITLE, epubNavigationItemRef.Title);
            EpubNavigationItemComparer.CompareNavigationItemLinks(Link, epubNavigationItemRef.Link);
            EpubContentRefComparer.CompareEpubLocalContentFileRefs(null, epubNavigationItemRef.HtmlContentFileRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(NestedItems, epubNavigationItemRef.NestedItems);
        }

        [Fact(DisplayName = "Constructing a EpubNavigationItemRef instance with null nestedItems parameter should succeed")]
        public void ConstructorWithNullNestedItemsParameterTest()
        {
            EpubNavigationItemRef epubNavigationItemRef = new(TYPE, TITLE, Link, TestEpubContentRef.Chapter1FileRef, null);
            Assert.Equal(TYPE, epubNavigationItemRef.Type);
            Assert.Equal(TITLE, epubNavigationItemRef.Title);
            EpubNavigationItemComparer.CompareNavigationItemLinks(Link, epubNavigationItemRef.Link);
            EpubContentRefComparer.CompareEpubLocalContentFileRefs(TestEpubContentRef.Chapter1FileRef, epubNavigationItemRef.HtmlContentFileRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(new List<EpubNavigationItemRef>(), epubNavigationItemRef.NestedItems);
        }

        [Fact(DisplayName = "ToString method should produce a string with the type, the title, and the number of nested items")]
        public void ToStringTest()
        {
            EpubNavigationItemRef epubNavigationItemRef = new
            (
                type: TYPE,
                title: TITLE,
                nestedItems: NestedItems
            );
            Assert.Equal("Type: HEADER, Title: Chapter 1, NestedItems.Count: 1", epubNavigationItemRef.ToString());
        }
    }
}
