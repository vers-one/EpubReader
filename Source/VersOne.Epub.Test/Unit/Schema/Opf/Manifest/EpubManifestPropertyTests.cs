using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Manifest
{
    public class EpubManifestPropertyTests
    {
        [Fact(DisplayName = "Converting a string to EpubManifestProperty list should succeed")]
        public void ParsePropertyListTest()
        {
            string stringValue = "cover-image mathml nav remote-resources scripted svg test-unknown-property";
            List<EpubManifestProperty> expectedPropertyList = new()
            {
                EpubManifestProperty.COVER_IMAGE,
                EpubManifestProperty.MATHML,
                EpubManifestProperty.NAV,
                EpubManifestProperty.REMOTE_RESOURCES,
                EpubManifestProperty.SCRIPTED,
                EpubManifestProperty.SVG,
                EpubManifestProperty.UNKNOWN
            };
            List<EpubManifestProperty> actualPropertyList = EpubManifestPropertyParser.ParsePropertyList(stringValue);
            Assert.Equal(expectedPropertyList, actualPropertyList);
        }

        [Fact(DisplayName = "Converting an empty string to EpubManifestProperty list should succeed")]
        public void ParsePropertyListFromEmptyStringTest()
        {
            string stringValue = "   ";
            List<EpubManifestProperty> expectedPropertyList = new();
            List<EpubManifestProperty> actualPropertyList = EpubManifestPropertyParser.ParsePropertyList(stringValue);
            Assert.Equal(expectedPropertyList, actualPropertyList);
        }
    }
}
