using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;
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
                contentFileUrlWithAnchor: "../content/chapter1.html#section1",
                baseDirectoryPath: "OPS/toc"
            );

        private static List<EpubNavigationItemRef> NestedItems =>
            [
                new EpubNavigationItemRef
                (
                    type: EpubNavigationItemType.HEADER,
                    title: "Nested item"
                )
            ];

        [Fact(DisplayName = "Constructing a EpubNavigationItemRef instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            TestEpubContentRef testEpubContentRef = new(new TestZipFile());
            EpubLocalTextContentFileRef testNavigationContentFileRef = testEpubContentRef.Chapter1FileRef;
            EpubNavigationItemRef epubNavigationItemRef = new(TYPE, TITLE, Link, testNavigationContentFileRef, NestedItems);
            Assert.Equal(TYPE, epubNavigationItemRef.Type);
            Assert.Equal(TITLE, epubNavigationItemRef.Title);
            EpubNavigationItemComparer.CompareNavigationItemLinks(Link, epubNavigationItemRef.Link);
            EpubContentRefComparer.CompareEpubLocalContentFileRefs(testNavigationContentFileRef, epubNavigationItemRef.HtmlContentFileRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists(NestedItems, epubNavigationItemRef.NestedItems);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if title parameter is null")]
        public void ConstructorWithNullTitleTest()
        {
            TestEpubContentRef testEpubContentRef = new(new TestZipFile());
            EpubLocalTextContentFileRef testNavigationContentFileRef = testEpubContentRef.Chapter1FileRef;
            Assert.Throws<ArgumentNullException>(() => new EpubNavigationItemRef(TYPE, null!, Link, testNavigationContentFileRef, NestedItems));
        }

        [Fact(DisplayName = "Constructing a EpubNavigationItemRef instance with null link parameter should succeed")]
        public void ConstructorWithNullLinkParameterTest()
        {
            TestEpubContentRef testEpubContentRef = new(new TestZipFile());
            EpubLocalTextContentFileRef testNavigationContentFileRef = testEpubContentRef.Chapter1FileRef;
            EpubNavigationItemRef epubNavigationItemRef = new(TYPE, TITLE, null, testNavigationContentFileRef, NestedItems);
            Assert.Equal(TYPE, epubNavigationItemRef.Type);
            Assert.Equal(TITLE, epubNavigationItemRef.Title);
            EpubNavigationItemComparer.CompareNavigationItemLinks(null, epubNavigationItemRef.Link);
            EpubContentRefComparer.CompareEpubLocalContentFileRefs(testNavigationContentFileRef, epubNavigationItemRef.HtmlContentFileRef);
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
            TestEpubContentRef testEpubContentRef = new(new TestZipFile());
            EpubLocalTextContentFileRef testNavigationContentFileRef = testEpubContentRef.Chapter1FileRef;
            EpubNavigationItemRef epubNavigationItemRef = new(TYPE, TITLE, Link, testNavigationContentFileRef, null);
            Assert.Equal(TYPE, epubNavigationItemRef.Type);
            Assert.Equal(TITLE, epubNavigationItemRef.Title);
            EpubNavigationItemComparer.CompareNavigationItemLinks(Link, epubNavigationItemRef.Link);
            EpubContentRefComparer.CompareEpubLocalContentFileRefs(testNavigationContentFileRef, epubNavigationItemRef.HtmlContentFileRef);
            EpubNavigationItemRefComparer.CompareNavigationItemRefLists([], epubNavigationItemRef.NestedItems);
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
