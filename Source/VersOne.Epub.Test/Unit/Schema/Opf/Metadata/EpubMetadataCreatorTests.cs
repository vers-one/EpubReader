using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataCreatorTests
    {
        private const string CREATOR = "John Doe";
        private const string ID = "creator";
        private const string FILE_AS = "Doe, John";
        private const string ROLE = "author";
        private const EpubTextDirection TEXT_DIRECTION = EpubTextDirection.LEFT_TO_RIGHT;
        private const string LANGUAGE = "en";

        [Fact(DisplayName = "Constructing a EpubMetadataCreator instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataCreator epubMetadataCreator = new(CREATOR, ID, FILE_AS, ROLE, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(CREATOR, epubMetadataCreator.Creator);
            Assert.Equal(ID, epubMetadataCreator.Id);
            Assert.Equal(FILE_AS, epubMetadataCreator.FileAs);
            Assert.Equal(ROLE, epubMetadataCreator.Role);
            Assert.Equal(TEXT_DIRECTION, epubMetadataCreator.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataCreator.Language);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if creator parameter is null")]
        public void ConstructorWithNullCreatorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataCreator(null!, ID, FILE_AS, ROLE, TEXT_DIRECTION, LANGUAGE));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataCreator instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataCreator epubMetadataCreator = new(CREATOR, null, FILE_AS, ROLE, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(CREATOR, epubMetadataCreator.Creator);
            Assert.Null(epubMetadataCreator.Id);
            Assert.Equal(FILE_AS, epubMetadataCreator.FileAs);
            Assert.Equal(ROLE, epubMetadataCreator.Role);
            Assert.Equal(TEXT_DIRECTION, epubMetadataCreator.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataCreator.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataCreator instance with null fileAs parameter should succeed")]
        public void ConstructorWithNullFileAsTest()
        {
            EpubMetadataCreator epubMetadataCreator = new(CREATOR, ID, null, ROLE, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(CREATOR, epubMetadataCreator.Creator);
            Assert.Equal(ID, epubMetadataCreator.Id);
            Assert.Null(epubMetadataCreator.FileAs);
            Assert.Equal(ROLE, epubMetadataCreator.Role);
            Assert.Equal(TEXT_DIRECTION, epubMetadataCreator.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataCreator.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataCreator instance with null role parameter should succeed")]
        public void ConstructorWithNullRoleTest()
        {
            EpubMetadataCreator epubMetadataCreator = new(CREATOR, ID, FILE_AS, null, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(CREATOR, epubMetadataCreator.Creator);
            Assert.Equal(ID, epubMetadataCreator.Id);
            Assert.Equal(FILE_AS, epubMetadataCreator.FileAs);
            Assert.Null(epubMetadataCreator.Role);
            Assert.Equal(TEXT_DIRECTION, epubMetadataCreator.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataCreator.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataCreator instance with null textDirection parameter should succeed")]
        public void ConstructorWithNullTextDirectionTest()
        {
            EpubMetadataCreator epubMetadataCreator = new(CREATOR, ID, FILE_AS, ROLE, null, LANGUAGE);
            Assert.Equal(CREATOR, epubMetadataCreator.Creator);
            Assert.Equal(ID, epubMetadataCreator.Id);
            Assert.Equal(FILE_AS, epubMetadataCreator.FileAs);
            Assert.Equal(ROLE, epubMetadataCreator.Role);
            Assert.Null(epubMetadataCreator.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataCreator.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataCreator instance with null language parameter should succeed")]
        public void ConstructorWithNullLanguageTest()
        {
            EpubMetadataCreator epubMetadataCreator = new(CREATOR, ID, FILE_AS, ROLE, TEXT_DIRECTION, null);
            Assert.Equal(CREATOR, epubMetadataCreator.Creator);
            Assert.Equal(ID, epubMetadataCreator.Id);
            Assert.Equal(FILE_AS, epubMetadataCreator.FileAs);
            Assert.Equal(ROLE, epubMetadataCreator.Role);
            Assert.Equal(TEXT_DIRECTION, epubMetadataCreator.TextDirection);
            Assert.Null(epubMetadataCreator.Language);
        }
    }
}
