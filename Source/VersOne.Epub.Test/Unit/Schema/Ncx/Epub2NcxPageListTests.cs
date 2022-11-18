using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxPageListTests
    {
        private static List<Epub2NcxPageTarget> Items =>
            new()
            {
                new Epub2NcxPageTarget
                (
                    id: "pagetarget",
                    value: "1",
                    type: Epub2NcxPageTargetType.FRONT,
                    @class: "pagetarget-class",
                    playOrder: "1",
                    navigationLabels: new List<Epub2NcxNavigationLabel>()
                    {
                        new Epub2NcxNavigationLabel
                        (
                            text: "1"
                        ),
                        new Epub2NcxNavigationLabel
                        (
                            text: "I"
                        )
                    },
                    content: new Epub2NcxContent
                    (
                        id: "content",
                        source: "chapter.html"
                    )
                )
            };

        [Fact(DisplayName = "Constructing a Epub2NcxPageList instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub2NcxPageList epub2NcxPageList = new(Items);
            Epub2NcxComparer.CompareEpub2NcxPageTargetLists(Items, epub2NcxPageList.Items);
        }

        [Fact(DisplayName = "Constructing a Epub2NcxPageList instance with null items parameter should succeed")]
        public void ConstructorWithNullItemsTest()
        {
            Epub2NcxPageList epub2NcxPageList = new(null);
            Epub2NcxComparer.CompareEpub2NcxPageTargetLists(new List<Epub2NcxPageTarget>(), epub2NcxPageList.Items);
        }
    }
}
