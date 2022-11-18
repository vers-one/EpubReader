using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxPageTargetTests
    {
        private const string ID = "pagetarget";
        private const string VALUE = "1";
        private const Epub2NcxPageTargetType TYPE = Epub2NcxPageTargetType.FRONT;
        private const string CLASS = "pagetarget-class";
        private const string PLAY_ORDER = "1";

        private static List<Epub2NcxNavigationLabel> NavigationLabels =>
            new()
            {
                new Epub2NcxNavigationLabel
                (
                    text: "1"
                ),
                new Epub2NcxNavigationLabel
                (
                    text: "I"
                )
            };

        private static Epub2NcxContent Content =>
            new
            (
                id: "content",
                source: "chapter.html"
            );

        [Fact(DisplayName = "Constructing a Epub2NcxPageTarget instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub2NcxPageTarget epub2NcxPageTarget = new(ID, VALUE, TYPE, CLASS, PLAY_ORDER, NavigationLabels, Content);
            Assert.Equal(ID, epub2NcxPageTarget.Id);
            Assert.Equal(VALUE, epub2NcxPageTarget.Value);
            Assert.Equal(TYPE, epub2NcxPageTarget.Type);
            Assert.Equal(CLASS, epub2NcxPageTarget.Class);
            Assert.Equal(PLAY_ORDER, epub2NcxPageTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxPageTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxPageTarget.Content);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxPageTarget instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            Epub2NcxPageTarget epub2NcxPageTarget = new(null, VALUE, TYPE, CLASS, PLAY_ORDER, NavigationLabels, Content);
            Assert.Null(epub2NcxPageTarget.Id);
            Assert.Equal(VALUE, epub2NcxPageTarget.Value);
            Assert.Equal(TYPE, epub2NcxPageTarget.Type);
            Assert.Equal(CLASS, epub2NcxPageTarget.Class);
            Assert.Equal(PLAY_ORDER, epub2NcxPageTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxPageTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxPageTarget.Content);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxPageTarget instance with null value parameter should succeed")]
        public void ConstructorWithNullValueTest()
        {
            Epub2NcxPageTarget epub2NcxPageTarget = new(ID, null, TYPE, CLASS, PLAY_ORDER, NavigationLabels, Content);
            Assert.Equal(ID, epub2NcxPageTarget.Id);
            Assert.Null(epub2NcxPageTarget.Value);
            Assert.Equal(TYPE, epub2NcxPageTarget.Type);
            Assert.Equal(CLASS, epub2NcxPageTarget.Class);
            Assert.Equal(PLAY_ORDER, epub2NcxPageTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxPageTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxPageTarget.Content);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxPageTarget instance with null class parameter should succeed")]
        public void ConstructorWithNullClassTest()
        {
            Epub2NcxPageTarget epub2NcxPageTarget = new(ID, VALUE, TYPE, null, PLAY_ORDER, NavigationLabels, Content);
            Assert.Equal(ID, epub2NcxPageTarget.Id);
            Assert.Equal(VALUE, epub2NcxPageTarget.Value);
            Assert.Equal(TYPE, epub2NcxPageTarget.Type);
            Assert.Null(epub2NcxPageTarget.Class);
            Assert.Equal(PLAY_ORDER, epub2NcxPageTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxPageTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxPageTarget.Content);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxPageTarget instance with null playOrder parameter should succeed")]
        public void ConstructorWithNullPlayOrderTest()
        {
            Epub2NcxPageTarget epub2NcxPageTarget = new(ID, VALUE, TYPE, CLASS, null, NavigationLabels, Content);
            Assert.Equal(ID, epub2NcxPageTarget.Id);
            Assert.Equal(VALUE, epub2NcxPageTarget.Value);
            Assert.Equal(TYPE, epub2NcxPageTarget.Type);
            Assert.Equal(CLASS, epub2NcxPageTarget.Class);
            Assert.Null(epub2NcxPageTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxPageTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxPageTarget.Content);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxPageTarget instance with null navigationLabels parameter should succeed")]
        public void ConstructorWithNullNavigationLabelsTest()
        {
            Epub2NcxPageTarget epub2NcxPageTarget = new(ID, VALUE, TYPE, CLASS, PLAY_ORDER, null, Content);
            Assert.Equal(ID, epub2NcxPageTarget.Id);
            Assert.Equal(VALUE, epub2NcxPageTarget.Value);
            Assert.Equal(TYPE, epub2NcxPageTarget.Type);
            Assert.Equal(CLASS, epub2NcxPageTarget.Class);
            Assert.Equal(PLAY_ORDER, epub2NcxPageTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(new List<Epub2NcxNavigationLabel>(), epub2NcxPageTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(Content, epub2NcxPageTarget.Content);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxPageTarget instance with null content parameter should succeed")]
        public void ConstructorWithNullContentTest()
        {
            Epub2NcxPageTarget epub2NcxPageTarget = new(ID, VALUE, TYPE, CLASS, PLAY_ORDER, NavigationLabels, null);
            Assert.Equal(ID, epub2NcxPageTarget.Id);
            Assert.Equal(VALUE, epub2NcxPageTarget.Value);
            Assert.Equal(TYPE, epub2NcxPageTarget.Type);
            Assert.Equal(CLASS, epub2NcxPageTarget.Class);
            Assert.Equal(PLAY_ORDER, epub2NcxPageTarget.PlayOrder);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxPageTarget.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxContents(null, epub2NcxPageTarget.Content);
        }
    }
}
