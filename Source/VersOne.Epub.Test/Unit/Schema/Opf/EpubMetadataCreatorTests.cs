using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubMetadataCreatorTests
    {
        private const string ID = "creator";
        private const string CREATOR = "John Doe";
        private const string FILE_AS = "Doe, John";
        private const string ROLE = "author";

        [Fact(DisplayName = "Constructing a EpubMetadataCreator instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataCreator epubMetadataCreator = new(ID, CREATOR, FILE_AS, ROLE);
            Assert.Equal(ID, epubMetadataCreator.Id);
            Assert.Equal(CREATOR, epubMetadataCreator.Creator);
            Assert.Equal(FILE_AS, epubMetadataCreator.FileAs);
            Assert.Equal(ROLE, epubMetadataCreator.Role);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if creator parameter is null")]
        public void ConstructorWithNullCreatorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataCreator(ID, null!, FILE_AS, ROLE));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataCreator instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataCreator epubMetadataCreator = new(null, CREATOR, FILE_AS, ROLE);
            Assert.Null(epubMetadataCreator.Id);
            Assert.Equal(CREATOR, epubMetadataCreator.Creator);
            Assert.Equal(FILE_AS, epubMetadataCreator.FileAs);
            Assert.Equal(ROLE, epubMetadataCreator.Role);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataCreator instance with null fileAs parameter should succeed")]
        public void ConstructorWithNullFileAsTest()
        {
            EpubMetadataCreator epubMetadataCreator = new(ID, CREATOR, null, ROLE);
            Assert.Equal(ID, epubMetadataCreator.Id);
            Assert.Equal(CREATOR, epubMetadataCreator.Creator);
            Assert.Null(epubMetadataCreator.FileAs);
            Assert.Equal(ROLE, epubMetadataCreator.Role);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataCreator instance with null role parameter should succeed")]
        public void ConstructorWithNullRoleTest()
        {
            EpubMetadataCreator epubMetadataCreator = new(ID, CREATOR, FILE_AS, null);
            Assert.Equal(ID, epubMetadataCreator.Id);
            Assert.Equal(CREATOR, epubMetadataCreator.Creator);
            Assert.Equal(FILE_AS, epubMetadataCreator.FileAs);
            Assert.Null(epubMetadataCreator.Role);
        }
    }
}
