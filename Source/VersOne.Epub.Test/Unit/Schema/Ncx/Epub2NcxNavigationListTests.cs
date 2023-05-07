using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxNavigationListTests
    {
        private const string ID = "navlist";
        private const string CLASS = "navlist-class";

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

        private static List<Epub2NcxNavigationTarget> NavigationTargets =>
            new()
            {
                new Epub2NcxNavigationTarget
                (
                    id: "navtarget",
                    value: "Navigation Target",
                    @class: "navtarget-class",
                    playOrder: "1",
                    navigationLabels: new List<Epub2NcxNavigationLabel>()
                    {
                        new Epub2NcxNavigationLabel
                        (
                            text: "Navigation Label 1"
                        ),
                        new Epub2NcxNavigationLabel
                        (
                            text: "Navigation Label 2"
                        )
                    },
                    content: new Epub2NcxContent
                    (
                        source: "chapter.html#anchor"
                    )
                )
            };

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationList instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub2NcxNavigationList epub2NcxNavigationList = new(ID, CLASS, NavigationLabels, NavigationTargets);
            Assert.Equal(ID, epub2NcxNavigationList.Id);
            Assert.Equal(CLASS, epub2NcxNavigationList.Class);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationList.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxNavigationTargetLists(NavigationTargets, epub2NcxNavigationList.NavigationTargets);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationList instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            Epub2NcxNavigationList epub2NcxNavigationList = new(null, CLASS, NavigationLabels, NavigationTargets);
            Assert.Null(epub2NcxNavigationList.Id);
            Assert.Equal(CLASS, epub2NcxNavigationList.Class);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationList.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxNavigationTargetLists(NavigationTargets, epub2NcxNavigationList.NavigationTargets);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationList instance with null class parameter should succeed")]
        public void ConstructorWithNullClassTest()
        {
            Epub2NcxNavigationList epub2NcxNavigationList = new(ID, null, NavigationLabels, NavigationTargets);
            Assert.Equal(ID, epub2NcxNavigationList.Id);
            Assert.Null(epub2NcxNavigationList.Class);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationList.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxNavigationTargetLists(NavigationTargets, epub2NcxNavigationList.NavigationTargets);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationList instance with null navigationLabels parameter should succeed")]
        public void ConstructorWithNullNavigationLabelsTest()
        {
            Epub2NcxNavigationList epub2NcxNavigationList = new(ID, CLASS, null, NavigationTargets);
            Assert.Equal(ID, epub2NcxNavigationList.Id);
            Assert.Equal(CLASS, epub2NcxNavigationList.Class);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(new List<Epub2NcxNavigationLabel>(), epub2NcxNavigationList.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxNavigationTargetLists(NavigationTargets, epub2NcxNavigationList.NavigationTargets);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationList instance with null navigationTargets parameter should succeed")]
        public void ConstructorWithNullNavigationTargetsTest()
        {
            Epub2NcxNavigationList epub2NcxNavigationList = new(ID, CLASS, NavigationLabels, null);
            Assert.Equal(ID, epub2NcxNavigationList.Id);
            Assert.Equal(CLASS, epub2NcxNavigationList.Class);
            Epub2NcxComparer.CompareEpub2NcxNavigationLabelLists(NavigationLabels, epub2NcxNavigationList.NavigationLabels);
            Epub2NcxComparer.CompareEpub2NcxNavigationTargetLists(new List<Epub2NcxNavigationTarget>(), epub2NcxNavigationList.NavigationTargets);
        }
    }
}
