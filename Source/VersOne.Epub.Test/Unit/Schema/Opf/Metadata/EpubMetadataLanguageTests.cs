using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataLanguageTests
    {
        private const string LANGUAGE = "en";
        private const string ID = "language";

        [Fact(DisplayName = "Constructing a EpubMetadataLanguage instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataLanguage epubMetadataLanguage = new(LANGUAGE, ID);
            Assert.Equal(LANGUAGE, epubMetadataLanguage.Language);
            Assert.Equal(ID, epubMetadataLanguage.Id);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if language parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataLanguage(null!, ID));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataLanguage instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataLanguage epubMetadataLanguage = new(LANGUAGE, null);
            Assert.Equal(LANGUAGE, epubMetadataLanguage.Language);
            Assert.Null(epubMetadataLanguage.Id);
        }
    }
}
