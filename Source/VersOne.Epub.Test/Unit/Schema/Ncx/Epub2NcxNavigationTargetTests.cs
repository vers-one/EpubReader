using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxNavigationTargetTests
    {
        private const string ID = "navtarget";
        private const string CLASS = "navtarget-class";
        private const string VALUE = "Navigation Target";
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


        [Fact(DisplayName = "Constructing a Epub2NcxNavigationTarget instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub2NcxNavigationTarget epub2NcxNavigationTarget = new(ID, CLASS, VALUE, PLAY_ORDER, NavigationLabels, Content);
            Assert.Equal(ID, epub2NcxNavigationTarget.Id);
            Assert.Equal(CLASS, epub2NcxNavigationTarget.Class);
            Assert.Equal(VALUE, epub2NcxNavigationTarget.Value);
            Assert.Equal(PLAY_ORDER, epub2NcxNavigationTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxNavigationTarget.Content);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if id parameter is null")]
        public void ConstructorWithNullIdTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub2NcxNavigationTarget(null!, CLASS, VALUE, PLAY_ORDER, NavigationLabels, Content));
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationTarget instance with null class parameter should succeed")]
        public void ConstructorWithNullClassTest()
        {
            Epub2NcxNavigationTarget epub2NcxNavigationTarget = new(ID, null, VALUE, PLAY_ORDER, NavigationLabels, Content);
            Assert.Equal(ID, epub2NcxNavigationTarget.Id);
            Assert.Null(epub2NcxNavigationTarget.Class);
            Assert.Equal(VALUE, epub2NcxNavigationTarget.Value);
            Assert.Equal(PLAY_ORDER, epub2NcxNavigationTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxNavigationTarget.Content);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationTarget instance with null value parameter should succeed")]
        public void ConstructorWithNullValueTest()
        {
            Epub2NcxNavigationTarget epub2NcxNavigationTarget = new(ID, CLASS, null, PLAY_ORDER, NavigationLabels, Content);
            Assert.Equal(ID, epub2NcxNavigationTarget.Id);
            Assert.Equal(CLASS, epub2NcxNavigationTarget.Class);
            Assert.Null(epub2NcxNavigationTarget.Value);
            Assert.Equal(PLAY_ORDER, epub2NcxNavigationTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxNavigationTarget.Content);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationTarget instance with null playOrder parameter should succeed")]
        public void ConstructorWithNullPlayOrderTest()
        {
            Epub2NcxNavigationTarget epub2NcxNavigationTarget = new(ID, CLASS, VALUE, null, NavigationLabels, Content);
            Assert.Equal(ID, epub2NcxNavigationTarget.Id);
            Assert.Equal(CLASS, epub2NcxNavigationTarget.Class);
            Assert.Equal(VALUE, epub2NcxNavigationTarget.Value);
            Assert.Null(epub2NcxNavigationTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxNavigationTarget.Content);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationTarget instance with null navigationLabels parameter should succeed")]
        public void ConstructorWithNullNavigationLabelsTest()
        {
            Epub2NcxNavigationTarget epub2NcxNavigationTarget = new(ID, CLASS, VALUE, PLAY_ORDER, null, Content);
            Assert.Equal(ID, epub2NcxNavigationTarget.Id);
            Assert.Equal(CLASS, epub2NcxNavigationTarget.Class);
            Assert.Equal(VALUE, epub2NcxNavigationTarget.Value);
            Assert.Equal(PLAY_ORDER, epub2NcxNavigationTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(new List<Epub2NcxNavigationLabel>(), epub2NcxNavigationTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxNavigationTarget.Content);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationTarget instance with null content parameter should succeed")]
        public void ConstructorWithNullContentTest()
        {
            Epub2NcxNavigationTarget epub2NcxNavigationTarget = new(ID, CLASS, VALUE, PLAY_ORDER, NavigationLabels, null);
            Assert.Equal(ID, epub2NcxNavigationTarget.Id);
            Assert.Equal(CLASS, epub2NcxNavigationTarget.Class);
            Assert.Equal(VALUE, epub2NcxNavigationTarget.Value);
            Assert.Equal(PLAY_ORDER, epub2NcxNavigationTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(null, epub2NcxNavigationTarget.Content);
        }
    }
}
