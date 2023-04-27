using System.Xml.Linq;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ops
{
    public class SmilMetadataTests
    {
        private static List<XElement> Items = new();

        [Fact(DisplayName = "Constructing a SmilMetadata instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            SmilMetadata smilMetadata = new(Items);
            SmilComparers.CompareSmilMetadataItems(Items, smilMetadata.Items);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if items parameter is null")]
        public void ConstructorWithNullItemsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SmilMetadata(null!));
        }
    }
}
