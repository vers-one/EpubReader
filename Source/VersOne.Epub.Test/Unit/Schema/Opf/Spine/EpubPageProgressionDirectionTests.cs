using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Spine
{
    public class EpubPageProgressionDirectionTests
    {
        [Theory(DisplayName = "Converting a string to EpubPageProgressionDirection should succeed")]
        [InlineData("default", EpubPageProgressionDirection.DEFAULT)]
        [InlineData("ltr", EpubPageProgressionDirection.LEFT_TO_RIGHT)]
        [InlineData("rtl", EpubPageProgressionDirection.RIGHT_TO_LEFT)]
        [InlineData("test-unknown-property", EpubPageProgressionDirection.UNKNOWN)]
        [InlineData("", EpubPageProgressionDirection.UNKNOWN)]
        [InlineData(null, EpubPageProgressionDirection.UNKNOWN)]
        public void ParseTest(string? stringValue, EpubPageProgressionDirection expectedPageProgressionDirection)
        {
            EpubPageProgressionDirection actualPageProgressionDirection = EpubPageProgressionDirectionParser.Parse(stringValue!);
            Assert.Equal(expectedPageProgressionDirection, actualPageProgressionDirection);
        }
    }
}
