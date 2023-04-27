using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxNavigationPointTests
    {
        private const string ID = "navpoint";
        private const string CLASS = "navpoint-class";
        private const string PLAY_ORDER = "1";

        private static List<Epub2NcxNavigationLabel> NavigationLabels =>
            new()
            {
                new Epub2NcxNavigationLabel
                (
                    text: "Navigation Label 1"
                ),
                new Epub2NcxNavigationLabel
                (
                    text: "Navigation Label 2"
                )
            };

        private static Epub2NcxContent Content =>
            new
            (
                id: "content",
                source: "chapter.html"
            );

        private static List<Epub2NcxNavigationPoint> ChildNavigationPoints =>
            new()
            {
                new Epub2NcxNavigationPoint
                (
                    id: "child-navpoint",
                    @class: "child-navpoint-class",
                    playOrder: "2",
                    navigationLabels: new List<Epub2NcxNavigationLabel>()
                    {
                        new Epub2NcxNavigationLabel
                        (
                            text: "Child Navigation Label"
                        )
                    },
                    content: new Epub2NcxContent
                    (
                        id: "child-content",
                        source: "chapter.html#section"
                    ),
                    childNavigationPoints: null
                )
            };

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationPoint instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub2NcxNavigationPoint epub2NcxNavigationPoint = new(ID, CLASS, PLAY_ORDER, NavigationLabels, Content, ChildNavigationPoints);
            Assert.Equal(ID, epub2NcxNavigationPoint.Id);
            Assert.Equal(CLASS, epub2NcxNavigationPoint.Class);
            Assert.Equal(PLAY_ORDER, epub2NcxNavigationPoint.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationPoint.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxNavigationPoint.Content);
            Epub2NcxComparer.CompareEpub2NcxNavigationPointLists(ChildNavigationPoints, epub2NcxNavigationPoint.ChildNavigationPoints);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if id parameter is null")]
        public void ConstructorWithNullIdTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub2NcxNavigationPoint(null!, CLASS, PLAY_ORDER, NavigationLabels, Content, ChildNavigationPoints));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if content parameter is null")]
        public void ConstructorWithNullContentTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub2NcxNavigationPoint(ID, CLASS, PLAY_ORDER, NavigationLabels, null!, ChildNavigationPoints));
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationList instance with null class parameter should succeed")]
        public void ConstructorWithNullClassTest()
        {
            Epub2NcxNavigationPoint epub2NcxNavigationPoint = new(ID, null, PLAY_ORDER, NavigationLabels, Content, ChildNavigationPoints);
            Assert.Equal(ID, epub2NcxNavigationPoint.Id);
            Assert.Null(epub2NcxNavigationPoint.Class);
            Assert.Equal(PLAY_ORDER, epub2NcxNavigationPoint.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationPoint.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxNavigationPoint.Content);
            Epub2NcxComparer.CompareEpub2NcxNavigationPointLists(ChildNavigationPoints, epub2NcxNavigationPoint.ChildNavigationPoints);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationList instance with null playOrder parameter should succeed")]
        public void ConstructorWithNullPlayOrderTest()
        {
            Epub2NcxNavigationPoint epub2NcxNavigationPoint = new(ID, CLASS, null, NavigationLabels, Content, ChildNavigationPoints);
            Assert.Equal(ID, epub2NcxNavigationPoint.Id);
            Assert.Equal(CLASS, epub2NcxNavigationPoint.Class);
            Assert.Null(epub2NcxNavigationPoint.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationPoint.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxNavigationPoint.Content);
            Epub2NcxComparer.CompareEpub2NcxNavigationPointLists(ChildNavigationPoints, epub2NcxNavigationPoint.ChildNavigationPoints);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationList instance with null navigationLabels parameter should succeed")]
        public void ConstructorWithNullNavigationLabelsTest()
        {
            Epub2NcxNavigationPoint epub2NcxNavigationPoint = new(ID, CLASS, PLAY_ORDER, null, Content, ChildNavigationPoints);
            Assert.Equal(ID, epub2NcxNavigationPoint.Id);
            Assert.Equal(CLASS, epub2NcxNavigationPoint.Class);
            Assert.Equal(PLAY_ORDER, epub2NcxNavigationPoint.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(new List<Epub2NcxNavigationLabel>(), epub2NcxNavigationPoint.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxNavigationPoint.Content);
            Epub2NcxComparer.CompareEpub2NcxNavigationPointLists(ChildNavigationPoints, epub2NcxNavigationPoint.ChildNavigationPoints);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationList instance with null childNavigationPoints parameter should succeed")]
        public void ConstructorWithNullChildNavigationPointsTest()
        {
            Epub2NcxNavigationPoint epub2NcxNavigationPoint = new(ID, CLASS, PLAY_ORDER, NavigationLabels, Content, null);
            Assert.Equal(ID, epub2NcxNavigationPoint.Id);
            Assert.Equal(CLASS, epub2NcxNavigationPoint.Class);
            Assert.Equal(PLAY_ORDER, epub2NcxNavigationPoint.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationPoint.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxNavigationPoint.Content);
            Epub2NcxComparer.CompareEpub2NcxNavigationPointLists(new List<Epub2NcxNavigationPoint>(), epub2NcxNavigationPoint.ChildNavigationPoints);
        }

        [Fact(DisplayName = "ToString method should produce a string with the ID and the content source")]
        public void ToStringTest()
        {
            Epub2NcxNavigationPoint epub2NcxNavigationPoint = new
            (
                id: "testId",
                navigationLabels: null,
                content: new Epub2NcxContent
                (
                    source: "source.html"
                )
            );
            Assert.Equal("Id: testId, Content.Source: source.html", epub2NcxNavigationPoint.ToString());
        }
    }
}
