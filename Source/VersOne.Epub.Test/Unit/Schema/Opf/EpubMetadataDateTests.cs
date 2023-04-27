using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubMetadataDateTests
    {
        private const string DATE = "2021-12-31T23:59:59.123456Z";
        private const string EVENT = "publication";

        [Fact(DisplayName = "Constructing a EpubMetadataDate instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataDate epubMetadataDate = new(DATE, EVENT);
            Assert.Equal(DATE, epubMetadataDate.Date);
            Assert.Equal(EVENT, epubMetadataDate.Event);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if date parameter is null")]
        public void ConstructorWithNullDateTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataDate(null!, EVENT));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataDate instance with null event parameter should succeed")]
        public void ConstructorWithNullEventTest()
        {
            EpubMetadataDate epubMetadataDate = new(DATE, null);
            Assert.Equal(DATE, epubMetadataDate.Date);
            Assert.Null(epubMetadataDate.Event);
        }
    }
}
