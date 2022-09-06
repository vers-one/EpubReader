using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxContentTests
    {
        [Fact(DisplayName = "ToString method should produce a string with the value of the Source property")]
        public void ToStringTest()
        {
            Epub2NcxContent epub2NcxContent = new()
            {
                Source = "source.html"
            };
            Assert.Equal("Source: source.html", epub2NcxContent.ToString());
        }
    }
}
