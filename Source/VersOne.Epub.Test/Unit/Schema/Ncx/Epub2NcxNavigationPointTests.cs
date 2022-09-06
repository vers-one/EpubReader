using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxNavigationPointTests
    {
        [Fact(DisplayName = "ToString method should produce a string with the ID and the content source")]
        public void ToStringTest()
        {
            Epub2NcxNavigationPoint epub2NcxNavigationPoint = new()
            {
                Id = "testId",
                Content = new Epub2NcxContent()
                {
                    Source = "source.html"
                }
            };
            Assert.Equal("Id: testId, Content.Source: source.html", epub2NcxNavigationPoint.ToString());
        }
    }
}
