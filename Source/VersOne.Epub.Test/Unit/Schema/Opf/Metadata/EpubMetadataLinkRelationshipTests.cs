using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataLinkRelationshipTests
    {
        [Fact(DisplayName = "Converting a string to EpubMetadataLinkRelationship list should succeed")]
        public void ParsePropertyListTest()
        {
            string stringValue = "alternate marc21xml-record mods-record onix-record record voicing xml-signature xmp-record test-unknown-property";
            List<EpubMetadataLinkRelationship> expectedRelationshipList = new()
            {
                EpubMetadataLinkRelationship.ALTERNATE,
                EpubMetadataLinkRelationship.MARC21XML_RECORD,
                EpubMetadataLinkRelationship.MODS_RECORD,
                EpubMetadataLinkRelationship.ONIX_RECORD,
                EpubMetadataLinkRelationship.RECORD,
                EpubMetadataLinkRelationship.VOICING,
                EpubMetadataLinkRelationship.XML_SIGNATURE,
                EpubMetadataLinkRelationship.XMP_RECORD,
                EpubMetadataLinkRelationship.UNKNOWN
            };
            List<EpubMetadataLinkRelationship> actualRelationshipList = EpubMetadataLinkRelationshipParser.ParseRelationshipList(stringValue);
            Assert.Equal(expectedRelationshipList, actualRelationshipList);
        }

        [Fact(DisplayName = "Converting an empty string to EpubMetadataLinkRelationship list should succeed")]
        public void ParsePropertyListFromEmptyStringTest()
        {
            string stringValue = "   ";
            List<EpubMetadataLinkRelationship> expectedRelationshipList = new();
            List<EpubMetadataLinkRelationship> actualRelationshipList = EpubMetadataLinkRelationshipParser.ParseRelationshipList(stringValue);
            Assert.Equal(expectedRelationshipList, actualRelationshipList);
        }
    }
}
