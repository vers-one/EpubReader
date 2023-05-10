using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Guide
{
    public class EpubGuideTests
    {
        private static List<EpubGuideReference> Items =>
            new()
            {
                new EpubGuideReference
                (
                    type: "toc",
                    title: "Contents",
                    href: "toc.html"
                )
            };

        [Fact(DisplayName = "Constructing a EpubGuide instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubGuide epubGuide = new(Items);
            EpubPackageComparer.CompareEpubGuideReferenceLists(Items, epubGuide.Items);
        }

        [Fact(DisplayName = "Constructing a EpubGuide instance with null items parameter should succeed")]
        public void ConstructorWithNullItemsTest()
        {
            EpubGuide epubGuide = new(null);
            EpubPackageComparer.CompareEpubGuideReferenceLists(new List<EpubGuideReference>(), epubGuide.Items);
        }
    }
}
