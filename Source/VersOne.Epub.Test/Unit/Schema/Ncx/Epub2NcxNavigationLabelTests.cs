using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxNavigationLabelTests
    {
        private const string TEXT = "Test text";

        [Fact(DisplayName = "Constructing a Epub2NcxNavigationLabel instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub2NcxNavigationLabel epub2NcxNavigationLabel = new(TEXT);
            Assert.Equal(TEXT, epub2NcxNavigationLabel.Text);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if text parameter is null")]
        public void ConstructorWithNullTextTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub2NcxNavigationLabel(null!));
        }

        [Fact(DisplayName = "ToString method should produce a string with the value of the Text property")]
        public void ToStringTest()
        {
            Epub2NcxNavigationLabel epub2NcxNavigationLabel = new
            (
                text: TEXT
            );
            Assert.Equal(TEXT, epub2NcxNavigationLabel.ToString());
        }
    }
}
