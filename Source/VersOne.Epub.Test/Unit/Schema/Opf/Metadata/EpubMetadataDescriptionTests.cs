using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataDescriptionTests
    {
        private const string DESCRIPTION = "Test Description";
        private const string ID = "description";
        private const EpubTextDirection TEXT_DIRECTION = EpubTextDirection.LEFT_TO_RIGHT;
        private const string LANGUAGE = "en";

        [Fact(DisplayName = "Constructing a EpubMetadataDescription instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataDescription epubMetadataDescription = new(DESCRIPTION, ID, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(DESCRIPTION, epubMetadataDescription.Description);
            Assert.Equal(ID, epubMetadataDescription.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataDescription.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataDescription.Language);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if description parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataDescription(null!, ID, TEXT_DIRECTION, LANGUAGE));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataDescription instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataDescription epubMetadataDescription = new(DESCRIPTION, null, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(DESCRIPTION, epubMetadataDescription.Description);
            Assert.Null(epubMetadataDescription.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataDescription.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataDescription.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataDescription instance with null textDirection parameter should succeed")]
        public void ConstructorWithNullTextDirectionTest()
        {
            EpubMetadataDescription epubMetadataDescription = new(DESCRIPTION, ID, null, LANGUAGE);
            Assert.Equal(DESCRIPTION, epubMetadataDescription.Description);
            Assert.Equal(ID, epubMetadataDescription.Id);
            Assert.Null(epubMetadataDescription.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataDescription.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataDescription instance with null language parameter should succeed")]
        public void ConstructorWithNullLanguageTest()
        {
            EpubMetadataDescription epubMetadataDescription = new(DESCRIPTION, ID, TEXT_DIRECTION, null);
            Assert.Equal(DESCRIPTION, epubMetadataDescription.Description);
            Assert.Equal(ID, epubMetadataDescription.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataDescription.TextDirection);
            Assert.Null(epubMetadataDescription.Language);
        }
    }
}
