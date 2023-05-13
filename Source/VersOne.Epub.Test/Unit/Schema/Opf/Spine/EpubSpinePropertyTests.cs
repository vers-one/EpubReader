using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Spine
{
    public class EpubSpinePropertyTests
    {
        [Fact(DisplayName = "Converting a string to EpubSpineProperty list should succeed")]
        public void ParsePropertyListTest()
        {
            string stringValue = "rendition:layout-pre-paginated rendition:layout-reflowable " +
                "rendition:orientation-auto rendition:orientation-landscape rendition:orientation-portrait " +
                "rendition:spread-auto rendition:spread-both rendition:spread-landscape rendition:spread-none " +
                "rendition:page-spread-center rendition:page-spread-left page-spread-left rendition:page-spread-right page-spread-right " +
                "rendition:flow-paginated rendition:flow-scrolled-continuous rendition:flow-scrolled-doc rendition:flow-auto " +
                "rendition:align-x-center " +
                "test-unknown-property";
            List<EpubSpineProperty> expectedPropertyList = new()
            {
                EpubSpineProperty.LAYOUT_PRE_PAGINATED,
                EpubSpineProperty.LAYOUT_REFLOWABLE,
                EpubSpineProperty.ORIENTATION_AUTO,
                EpubSpineProperty.ORIENTATION_LANDSCAPE,
                EpubSpineProperty.ORIENTATION_PORTRAIT,
                EpubSpineProperty.SPREAD_AUTO,
                EpubSpineProperty.SPREAD_BOTH,
                EpubSpineProperty.SPREAD_LANDSCAPE,
                EpubSpineProperty.SPREAD_NONE,
                EpubSpineProperty.PAGE_SPREAD_CENTER,
                EpubSpineProperty.PAGE_SPREAD_LEFT,
                EpubSpineProperty.PAGE_SPREAD_LEFT,
                EpubSpineProperty.PAGE_SPREAD_RIGHT,
                EpubSpineProperty.PAGE_SPREAD_RIGHT,
                EpubSpineProperty.FLOW_PAGINATED,
                EpubSpineProperty.FLOW_SCROLLED_CONTINUOUS,
                EpubSpineProperty.FLOW_SCROLLED_DOC,
                EpubSpineProperty.FLOW_AUTO,
                EpubSpineProperty.ALIGN_X_CENTER,
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
