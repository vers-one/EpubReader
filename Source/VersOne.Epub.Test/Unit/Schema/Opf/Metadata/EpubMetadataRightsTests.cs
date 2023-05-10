using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataRightsTests
    {
        private const string RIGHTS = "All rights reserved";
        private const string ID = "rights";
        private const EpubTextDirection TEXT_DIRECTION = EpubTextDirection.LEFT_TO_RIGHT;
        private const string LANGUAGE = "en";

        [Fact(DisplayName = "Constructing a EpubMetadataRights instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataRights epubMetadataRights = new(RIGHTS, ID, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(RIGHTS, epubMetadataRights.Rights);
            Assert.Equal(ID, epubMetadataRights.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataRights.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataRights.Language);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if rights parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataRights(null!, ID, TEXT_DIRECTION, LANGUAGE));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataRights instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataRights epubMetadataRights = new(RIGHTS, null, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(RIGHTS, epubMetadataRights.Rights);
            Assert.Null(epubMetadataRights.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataRights.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataRights.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataRights instance with null textDirection parameter should succeed")]
        public void ConstructorWithNullTextDirectionTest()
        {
            EpubMetadataRights epubMetadataRights = new(RIGHTS, ID, null, LANGUAGE);
            Assert.Equal(RIGHTS, epubMetadataRights.Rights);
            Assert.Equal(ID, epubMetadataRights.Id);
            Assert.Null(epubMetadataRights.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataRights.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataRights instance with null language parameter should succeed")]
        public void ConstructorWithNullLanguageTest()
        {
            EpubMetadataRights epubMetadataRights = new(RIGHTS, ID, TEXT_DIRECTION, null);
            Assert.Equal(RIGHTS, epubMetadataRights.Rights);
            Assert.Equal(ID, epubMetadataRights.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataRights.TextDirection);
            Assert.Null(epubMetadataRights.Language);
        }
    }
}
