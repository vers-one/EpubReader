using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Common
{
    public class EpubTextDirectionTests
    {
        [Theory(DisplayName = "Converting a string to EpubTextDirection should succeed")]
        [InlineData("ltr", EpubTextDirection.LEFT_TO_RIGHT)]
        [InlineData("rtl", EpubTextDirection.RIGHT_TO_LEFT)]
        [InlineData("auto", EpubTextDirection.AUTO)]
        [InlineData("test-unknown-property", EpubTextDirection.UNKNOWN)]
        [InlineData("", EpubTextDirection.UNKNOWN)]
        [InlineData(null, EpubTextDirection.UNKNOWN)]
        public void ParseTest(string stringValue, EpubTextDirection expectedTextDirection)
        {
            EpubTextDirection actualTextDirection = EpubTextDirectionParser.Parse(stringValue);
            Assert.Equal(expectedTextDirection, actualTextDirection);
        }
    }
}
