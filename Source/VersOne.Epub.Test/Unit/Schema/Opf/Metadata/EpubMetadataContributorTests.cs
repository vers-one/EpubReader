using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataContributorTests
    {
        private const string CONTRIBUTOR = "John Editor";
        private const string ID = "contributor";
        private const string FILE_AS = "Editor, John";
        private const string ROLE = "editor";
        private const EpubTextDirection TEXT_DIRECTION = EpubTextDirection.LEFT_TO_RIGHT;
        private const string LANGUAGE = "en";

        [Fact(DisplayName = "Constructing a EpubMetadataContributor instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataContributor epubMetadataContributor = new(CONTRIBUTOR, ID, FILE_AS, ROLE, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(CONTRIBUTOR, epubMetadataContributor.Contributor);
            Assert.Equal(ID, epubMetadataContributor.Id);
            Assert.Equal(FILE_AS, epubMetadataContributor.FileAs);
            Assert.Equal(ROLE, epubMetadataContributor.Role);
            Assert.Equal(TEXT_DIRECTION, epubMetadataContributor.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataContributor.Language);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contributor parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataContributor(null!, ID, FILE_AS, ROLE, TEXT_DIRECTION, LANGUAGE));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataContributor instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataContributor epubMetadataContributor = new(CONTRIBUTOR, null, FILE_AS, ROLE, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(CONTRIBUTOR, epubMetadataContributor.Contributor);
            Assert.Null(epubMetadataContributor.Id);
            Assert.Equal(FILE_AS, epubMetadataContributor.FileAs);
            Assert.Equal(ROLE, epubMetadataContributor.Role);
            Assert.Equal(TEXT_DIRECTION, epubMetadataContributor.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataContributor.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataContributor instance with null fileAs parameter should succeed")]
        public void ConstructorWithNullFileAsTest()
        {
            EpubMetadataContributor epubMetadataContributor = new(CONTRIBUTOR, ID, null, ROLE, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(CONTRIBUTOR, epubMetadataContributor.Contributor);
            Assert.Equal(ID, epubMetadataContributor.Id);
            Assert.Null(epubMetadataContributor.FileAs);
            Assert.Equal(ROLE, epubMetadataContributor.Role);
            Assert.Equal(TEXT_DIRECTION, epubMetadataContributor.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataContributor.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataContributor instance with null role parameter should succeed")]
        public void ConstructorWithNullRoleTest()
        {
            EpubMetadataContributor epubMetadataContributor = new(CONTRIBUTOR, ID, FILE_AS, null, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(CONTRIBUTOR, epubMetadataContributor.Contributor);
            Assert.Equal(ID, epubMetadataContributor.Id);
            Assert.Equal(FILE_AS, epubMetadataContributor.FileAs);
            Assert.Null(epubMetadataContributor.Role);
            Assert.Equal(TEXT_DIRECTION, epubMetadataContributor.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataContributor.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataContributor instance with null textDirection parameter should succeed")]
        public void ConstructorWithNullTextDirectionTest()
        {
            EpubMetadataContributor epubMetadataContributor = new(CONTRIBUTOR, ID, FILE_AS, ROLE, null, LANGUAGE);
            Assert.Equal(CONTRIBUTOR, epubMetadataContributor.Contributor);
            Assert.Equal(ID, epubMetadataContributor.Id);
            Assert.Equal(FILE_AS, epubMetadataContributor.FileAs);
            Assert.Equal(ROLE, epubMetadataContributor.Role);
            Assert.Null(epubMetadataContributor.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataContributor.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataContributor instance with null language parameter should succeed")]
        public void ConstructorWithNullLanguageTest()
        {
            EpubMetadataContributor epubMetadataContributor = new(CONTRIBUTOR, ID, FILE_AS, ROLE, TEXT_DIRECTION, null);
            Assert.Equal(CONTRIBUTOR, epubMetadataContributor.Contributor);
            Assert.Equal(ID, epubMetadataContributor.Id);
            Assert.Equal(FILE_AS, epubMetadataContributor.FileAs);
            Assert.Equal(ROLE, epubMetadataContributor.Role);
            Assert.Equal(TEXT_DIRECTION, epubMetadataContributor.TextDirection);
            Assert.Null(epubMetadataContributor.Language);
        }
    }
}
