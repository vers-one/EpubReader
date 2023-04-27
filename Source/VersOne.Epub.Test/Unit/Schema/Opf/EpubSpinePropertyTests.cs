using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubSpinePropertyTests
    {
        [Fact(DisplayName = "Converting a string to EpubSpineProperty list should succeed")]
        public void ParsePropertyListTest()
        {
            string stringValue = "page-spread-left page-spread-right test-unknown-property";
            List<EpubSpineProperty> expectedPropertyList = new()
            {
                EpubSpineProperty.PAGE_SPREAD_LEFT,
                EpubSpineProperty.PAGE_SPREAD_RIGHT,
                EpubSpineProperty.UNKNOWN
            };
            List<EpubSpineProperty> actualPropertyList = EpubSpinePropertyParser.ParsePropertyList(stringValue);
            Assert.Equal(expectedPropertyList, actualPropertyList);
        }

        [Fact(DisplayName = "Converting an empty string to EpubSpineProperty list should succeed")]
        public void ParsePropertyListFromEmptyStringTest()
        {
            string stringValue = "   ";
            List<EpubSpineProperty> expectedPropertyList = new();
            List<EpubSpineProperty> actualPropertyList = EpubSpinePropertyParser.ParsePropertyList(stringValue);
            Assert.Equal(expectedPropertyList, actualPropertyList);
        }
    }
}
