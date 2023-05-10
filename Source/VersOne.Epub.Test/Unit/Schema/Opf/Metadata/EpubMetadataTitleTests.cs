using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataTitleTests
    {
        private const string TITLE = "Test Title";
        private const string ID = "title";
        private const EpubTextDirection TEXT_DIRECTION = EpubTextDirection.LEFT_TO_RIGHT;
        private const string LANGUAGE = "en";

        [Fact(DisplayName = "Constructing a EpubMetadataTitle instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataTitle epubMetadataTitle = new(TITLE, ID, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(TITLE, epubMetadataTitle.Title);
            Assert.Equal(ID, epubMetadataTitle.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataTitle.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataTitle.Language);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if title parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataTitle(null!, ID, TEXT_DIRECTION, LANGUAGE));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataTitle instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataTitle epubMetadataTitle = new(TITLE, null, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(TITLE, epubMetadataTitle.Title);
            Assert.Null(epubMetadataTitle.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataTitle.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataTitle.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataTitle instance with null textDirection parameter should succeed")]
        public void ConstructorWithNullTextDirectionTest()
        {
            EpubMetadataTitle epubMetadataTitle = new(TITLE, ID, null, LANGUAGE);
            Assert.Equal(TITLE, epubMetadataTitle.Title);
            Assert.Equal(ID, epubMetadataTitle.Id);
            Assert.Null(epubMetadataTitle.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataTitle.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataTitle instance with null language parameter should succeed")]
        public void ConstructorWithNullLanguageTest()
        {
            EpubMetadataTitle epubMetadataTitle = new(TITLE, ID, TEXT_DIRECTION, null);
            Assert.Equal(TITLE, epubMetadataTitle.Title);
            Assert.Equal(ID, epubMetadataTitle.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataTitle.TextDirection);
            Assert.Null(epubMetadataTitle.Language);
        }
    }
}
