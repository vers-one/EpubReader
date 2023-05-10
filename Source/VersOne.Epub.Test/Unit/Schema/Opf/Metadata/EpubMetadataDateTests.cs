using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataDateTests
    {
        private const string DATE = "2021-12-31T23:59:59.123456Z";
        private const string ID = "date";
        private const string EVENT = "publication";

        [Fact(DisplayName = "Constructing a EpubMetadataDate instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataDate epubMetadataDate = new(DATE, ID, EVENT);
            Assert.Equal(DATE, epubMetadataDate.Date);
            Assert.Equal(ID, epubMetadataDate.Id);
            Assert.Equal(EVENT, epubMetadataDate.Event);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if date parameter is null")]
        public void ConstructorWithNullDateTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataDate(null!, ID, EVENT));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataDate instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataDate epubMetadataDate = new(DATE, null, EVENT);
            Assert.Equal(DATE, epubMetadataDate.Date);
            Assert.Null(epubMetadataDate.Id);
            Assert.Equal(EVENT, epubMetadataDate.Event);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataDate instance with null event parameter should succeed")]
        public void ConstructorWithNullEventTest()
        {
            EpubMetadataDate epubMetadataDate = new(DATE, ID, null);
            Assert.Equal(DATE, epubMetadataDate.Date);
            Assert.Equal(ID, epubMetadataDate.Id);
            Assert.Null(epubMetadataDate.Event);
        }
    }
}
