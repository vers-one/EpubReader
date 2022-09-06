using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxNavigationLabelTests
    {
        [Fact(DisplayName = "ToString method should produce a string with the value of the Text property")]
        public void ToStringTest()
        {
            Epub2NcxNavigationLabel epub2NcxNavigationLabel = new()
            {
                Text = "Test text"
            };
            Assert.Equal("Test text", epub2NcxNavigationLabel.ToString());
        }
    }
}
