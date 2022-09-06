using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubMetadataLinkPropertyTests
    {
        [Fact(DisplayName = "Converting a string to EpubMetadataLinkProperty list should succeed")]
        public void ParsePropertyListTest()
        {
            string stringValue = "onix xmp test-unknown-property";
            List<EpubMetadataLinkProperty> expectedPropertyList = new()
            {
                EpubMetadataLinkProperty.ONIX,
                EpubMetadataLinkProperty.XMP,
                EpubMetadataLinkProperty.UNKNOWN
            };
            List<EpubMetadataLinkProperty> actualPropertyList = EpubMetadataLinkPropertyParser.ParsePropertyList(stringValue);
            Assert.Equal(expectedPropertyList, actualPropertyList);
        }

        [Fact(DisplayName = "Converting an empty string to EpubMetadataLinkProperty list should succeed")]
        public void ParsePropertyListFromEmptyStringTest()
        {
            string stringValue = "   ";
            List<EpubMetadataLinkProperty> expectedPropertyList = new();
            List<EpubMetadataLinkProperty> actualPropertyList = EpubMetadataLinkPropertyParser.ParsePropertyList(stringValue);
            Assert.Equal(expectedPropertyList, actualPropertyList);
        }
    }
}
