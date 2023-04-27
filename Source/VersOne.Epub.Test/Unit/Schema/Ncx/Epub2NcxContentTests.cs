using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Ncx
{
    public class Epub2NcxContentTests
    {
        private const string ID = "content";
        private const string SOURCE = "source.html";

        [Fact(DisplayName = "Constructing a Epub2NcxContent instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub2NcxContent epub2NcxContent = new(ID, SOURCE);
            Assert.Equal(ID, epub2NcxContent.Id);
            Assert.Equal(SOURCE, epub2NcxContent.Source);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if source parameter is null")]
        public void ConstructorWithNullSourceTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub2NcxContent(ID, null!));
        }

        [Fact(DisplayName = "Constructing a Epub2NcxContent instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            Epub2NcxContent epub2NcxContent = new(null, SOURCE);
            Assert.Null(epub2NcxContent.Id);
            Assert.Equal(SOURCE, epub2NcxContent.Source);
        }

        [Fact(DisplayName = "ToString method should produce a string with the value of the Source property")]
        public void ToStringTest()
        {
            Epub2NcxContent epub2NcxContent = new
            (
                source: "source.html"
            );
            Assert.Equal("Source: source.html", epub2NcxContent.ToString());
        }
    }
}
