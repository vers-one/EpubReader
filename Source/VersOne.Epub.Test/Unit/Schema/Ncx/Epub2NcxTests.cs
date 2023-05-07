using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxTests
    {
        private const string FILE_PATH = "Content/toc.ncx";
        private const string DOC_TITLE = "Test title";

        private static Epub2NcxHead Head =>
            new
            (
                items: new List<Epub2NcxHeadMeta>
                {
                    new Epub2NcxHeadMeta
                    (
                        name: "dtb:uid",
                        content: "9781234567890"
                    )
                }
            );

        private static List<string> DocAuthors =>
            new()
            {
                "John Doe"
            };

        private static Epub2NcxNavigationMap NavMap =>
            new
            (
                items: new List<Epub2NcxNavigationPoint>()
                {
                    new Epub2NcxNavigationPoint
                    (
                        id: "navpoint",
                        navigationLabels: new List<Epub2NcxNavigationLabel>()
                        {
                            new Epub2NcxNavigationLabel
                            (
                                text: "Chapter"
                            )
                        },
                        content: new Epub2NcxContent
                        (
                            source: "chapter.html"
                        )
                    )
                }
            );

        private static Epub2NcxPageList PageList =>
            new
            (
                items: new List<Epub2NcxPageTarget>()
                {
                    new Epub2NcxPageTarget
                    (
                        type: Epub2NcxPageTargetType.NORMAL,
                        navigationLabels: new List<Epub2NcxNavigationLabel>()
                        {
                            new Epub2NcxNavigationLabel
                            (
                                text: "1"
                            )
                        },
                        content: new Epub2NcxContent
                        (
                            id: "content",
                            source: "chapter.html"
                        )
                    )
                }
            );

        private static List<Epub2NcxNavigationList> NavLists =>
            new()
            {
                new Epub2NcxNavigationList
                (
                    id: "navlist",
                    @class: "navlist-illustrations",
                    navigationLabels: new List<Epub2NcxNavigationLabel>()
                    {
                        new Epub2NcxNavigationLabel
                        (
                            text: "List of Illustrations"
                        )
                    },
                    navigationTargets: new List<Epub2NcxNavigationTarget>()
                    {
                        new Epub2NcxNavigationTarget
                        (
                            id: "navtarget",
                            value: "Illustration",
                            @class: "illustration",
                            playOrder: "1",
                            navigationLabels: new List<Epub2NcxNavigationLabel>()
                            {
                                new Epub2NcxNavigationLabel
                                (
                                    text: "Illustration"
                                )
                            },
                            content: new Epub2NcxContent
                            (
                                source: "chapter.html#illustration"
                            )
                        )
                    }
                )
            };

        [Fact(DisplayName = "Constructing a Epub2Ncx instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub2Ncx epub2Ncx = new(FILE_PATH, Head, DOC_TITLE, DocAuthors, NavMap, PageList, NavLists);
            Assert.Equal(FILE_PATH, epub2Ncx.FilePath);
            Epub2NcxComparer.CompareEpub2NcxHeads(Head, epub2Ncx.Head);
            Assert.Equal(DOC_TITLE, epub2Ncx.DocTitle);
            Assert.Equal(DocAuthors, epub2Ncx.DocAuthors);
            Epub2NcxComparer.CompareEpub2NcxNavigationMaps(NavMap, epub2Ncx.NavMap);
            Epub2NcxComparer.CompareEpub2NcxPageLists(PageList, epub2Ncx.PageList);
            Epub2NcxComparer.CompareEpub2NcxNavigationListLists(NavLists, epub2Ncx.NavLists);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if filePath parameter is null")]
        public void ConstructorWithNullFilePathTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub2Ncx(null!, Head, DOC_TITLE, DocAuthors, NavMap, PageList, NavLists));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if head parameter is null")]
        public void ConstructorWithNullHeadTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub2Ncx(FILE_PATH, null!, DOC_TITLE, DocAuthors, NavMap, PageList, NavLists));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if navMap parameter is null")]
        public void ConstructorWithNullNavMapTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub2Ncx(FILE_PATH, Head, DOC_TITLE, DocAuthors, null!, PageList, NavLists));
        }

        [Fact(DisplayName = "Constructing a Epub2Ncx instance with null docTitle parameter should succeed")]
        public void ConstructorWithNullDocTitleTest()
        {
            Epub2Ncx epub2Ncx = new(FILE_PATH, Head, null, DocAuthors, NavMap, PageList, NavLists);
            Assert.Equal(FILE_PATH, epub2Ncx.FilePath);
            Epub2NcxComparer.CompareEpub2NcxHeads(Head, epub2Ncx.Head);
            Assert.Null(epub2Ncx.DocTitle);
            Assert.Equal(DocAuthors, epub2Ncx.DocAuthors);
            Epub2NcxComparer.CompareEpub2NcxNavigationMaps(NavMap, epub2Ncx.NavMap);
            Epub2NcxComparer.CompareEpub2NcxPageLists(PageList, epub2Ncx.PageList);
            Epub2NcxComparer.CompareEpub2NcxNavigationListLists(NavLists, epub2Ncx.NavLists);
        }

        [Fact(DisplayName = "Constructing a Epub2Ncx instance with null docAuthors parameter should succeed")]
        public void ConstructorWithNullDocAuthorsTest()
        {
            Epub2Ncx epub2Ncx = new(FILE_PATH, Head, DOC_TITLE, null, NavMap, PageList, NavLists);
            Assert.Equal(FILE_PATH, epub2Ncx.FilePath);
            Epub2NcxComparer.CompareEpub2NcxHeads(Head, epub2Ncx.Head);
            Assert.Equal(DOC_TITLE, epub2Ncx.DocTitle);
            Assert.Equal(new List<string>(), epub2Ncx.DocAuthors);
            Epub2NcxComparer.CompareEpub2NcxNavigationMaps(NavMap, epub2Ncx.NavMap);
            Epub2NcxComparer.CompareEpub2NcxPageLists(PageList, epub2Ncx.PageList);
            Epub2NcxComparer.CompareEpub2NcxNavigationListLists(NavLists, epub2Ncx.NavLists);
        }

        [Fact(DisplayName = "Constructing a Epub2Ncx instance with null pageList parameter should succeed")]
        public void ConstructorWithNullPageListTest()
        {
            Epub2Ncx epub2Ncx = new(FILE_PATH, Head, DOC_TITLE, DocAuthors, NavMap, null, NavLists);
            Assert.Equal(FILE_PATH, epub2Ncx.FilePath);
            Epub2NcxComparer.CompareEpub2NcxHeads(Head, epub2Ncx.Head);
            Assert.Equal(DOC_TITLE, epub2Ncx.DocTitle);
            Assert.Equal(DocAuthors, epub2Ncx.DocAuthors);
            Epub2NcxComparer.CompareEpub2NcxNavigationMaps(NavMap, epub2Ncx.NavMap);
            Epub2NcxComparer.CompareEpub2NcxPageLists(null, epub2Ncx.PageList);
            Epub2NcxComparer.CompareEpub2NcxNavigationListLists(NavLists, epub2Ncx.NavLists);
        }

        [Fact(DisplayName = "Constructing a Epub2Ncx instance with null navLists parameter should succeed")]
        public void ConstructorWithNullNavListsTest()
        {
            Epub2Ncx epub2Ncx = new(FILE_PATH, Head, DOC_TITLE, DocAuthors, NavMap, PageList, null);
            Assert.Equal(FILE_PATH, epub2Ncx.FilePath);
            Epub2NcxComparer.CompareEpub2NcxHeads(Head, epub2Ncx.Head);
            Assert.Equal(DOC_TITLE, epub2Ncx.DocTitle);
            Assert.Equal(DocAuthors, epub2Ncx.DocAuthors);
            Epub2NcxComparer.CompareEpub2NcxNavigationMaps(NavMap, epub2Ncx.NavMap);
            Epub2NcxComparer.CompareEpub2NcxPageLists(PageList, epub2Ncx.PageList);
            Epub2NcxComparer.CompareEpub2NcxNavigationListLists(new List<Epub2NcxNavigationList>(), epub2Ncx.NavLists);
        }
    }
}
