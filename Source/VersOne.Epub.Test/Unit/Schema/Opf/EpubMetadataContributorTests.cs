using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubMetadataContributorTests
    {
        private const string ID = "contributor";
        private const string CONTRIBUTOR = "John Editor";
        private const string FILE_AS = "Editor, John";
        private const string ROLE = "editor";

        [Fact(DisplayName = "Constructing a EpubMetadataContributor instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataContributor epubMetadataContributor = new(ID, CONTRIBUTOR, FILE_AS, ROLE);
            Assert.Equal(ID, epubMetadataContributor.Id);
            Assert.Equal(CONTRIBUTOR, epubMetadataContributor.Contributor);
            Assert.Equal(FILE_AS, epubMetadataContributor.FileAs);
            Assert.Equal(ROLE, epubMetadataContributor.Role);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contributor parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataContributor(ID, null!, FILE_AS, ROLE));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataContributor instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataContributor epubMetadataContributor = new(null, CONTRIBUTOR, FILE_AS, ROLE);
            Assert.Null(epubMetadataContributor.Id);
            Assert.Equal(CONTRIBUTOR, epubMetadataContributor.Contributor);
            Assert.Equal(FILE_AS, epubMetadataContributor.FileAs);
            Assert.Equal(ROLE, epubMetadataContributor.Role);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataContributor instance with null fileAs parameter should succeed")]
        public void ConstructorWithNullFileAsTest()
        {
            EpubMetadataContributor epubMetadataContributor = new(ID, CONTRIBUTOR, null, ROLE);
            Assert.Equal(ID, epubMetadataContributor.Id);
            Assert.Equal(CONTRIBUTOR, epubMetadataContributor.Contributor);
            Assert.Null(epubMetadataContributor.FileAs);
            Assert.Equal(ROLE, epubMetadataContributor.Role);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataContributor instance with null role parameter should succeed")]
        public void ConstructorWithNullRoleTest()
        {
            EpubMetadataContributor epubMetadataContributor = new(ID, CONTRIBUTOR, FILE_AS, null);
            Assert.Equal(ID, epubMetadataContributor.Id);
            Assert.Equal(CONTRIBUTOR, epubMetadataContributor.Contributor);
            Assert.Equal(FILE_AS, epubMetadataContributor.FileAs);
            Assert.Null(epubMetadataContributor.Role);
        }
    }
}
