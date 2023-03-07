using System.Xml.Linq;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ops
{
    public class SmilHeadTests
    {
        private static SmilMetadata Metadata => new(new List<XElement>());

        [Fact(DisplayName = "Constructing a SmilHead instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            SmilHead smilHead = new(Metadata);
            SmilComparers.CompareSmilMetadatas(Metadata, smilHead.Metadata);
        }

        [Fact(DisplayName = "Constructing a SmilHead instance with null metadata parameter should succeed")]
        public void ConstructorWithNullMetadataTest()
        {
            SmilHead smilHead = new(null);
            Assert.Null(smilHead.Metadata);
        }
    }
}
