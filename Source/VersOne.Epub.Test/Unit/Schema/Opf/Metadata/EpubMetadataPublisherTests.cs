using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataPublisherTests
    {
        private const string PUBLISHER = "Test Publisher";
        private const string ID = "publisher";
        private const EpubTextDirection TEXT_DIRECTION = EpubTextDirection.LEFT_TO_RIGHT;
        private const string LANGUAGE = "en";

        [Fact(DisplayName = "Constructing a EpubMetadataPublisher instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataPublisher epubMetadataPublisher = new(PUBLISHER, ID, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(PUBLISHER, epubMetadataPublisher.Publisher);
            Assert.Equal(ID, epubMetadataPublisher.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataPublisher.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataPublisher.Language);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if publisher parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataPublisher(null!, ID, TEXT_DIRECTION, LANGUAGE));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataPublisher instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataPublisher epubMetadataPublisher = new(PUBLISHER, null, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(PUBLISHER, epubMetadataPublisher.Publisher);
            Assert.Null(epubMetadataPublisher.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataPublisher.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataPublisher.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataPublisher instance with null textDirection parameter should succeed")]
        public void ConstructorWithNullTextDirectionTest()
        {
            EpubMetadataPublisher epubMetadataPublisher = new(PUBLISHER, ID, null, LANGUAGE);
            Assert.Equal(PUBLISHER, epubMetadataPublisher.Publisher);
            Assert.Equal(ID, epubMetadataPublisher.Id);
            Assert.Null(epubMetadataPublisher.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataPublisher.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataPublisher instance with null language parameter should succeed")]
        public void ConstructorWithNullLanguageTest()
        {
            EpubMetadataPublisher epubMetadataPublisher = new(PUBLISHER, ID, TEXT_DIRECTION, null);
            Assert.Equal(PUBLISHER, epubMetadataPublisher.Publisher);
            Assert.Equal(ID, epubMetadataPublisher.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataPublisher.TextDirection);
            Assert.Null(epubMetadataPublisher.Language);
        }
    }
}
