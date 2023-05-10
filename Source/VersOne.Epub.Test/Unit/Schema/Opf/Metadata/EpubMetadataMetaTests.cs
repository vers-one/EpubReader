using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataMetaTests
    {
        private const string NAME = "cover";
        private const string CONTENT = "cover-image";
        private const string ID = "meta";
        private const string REFINES = "#creator";
        private const string PROPERTY = "identifier-type";
        private const string SCHEME = "onix:codelist5";

        [Fact(DisplayName = "Constructing a EpubMetadataMeta instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataMeta epubMetadataMeta = new(NAME, CONTENT, ID, REFINES, PROPERTY, SCHEME);
            Assert.Equal(NAME, epubMetadataMeta.Name);
            Assert.Equal(CONTENT, epubMetadataMeta.Content);
            Assert.Equal(ID, epubMetadataMeta.Id);
            Assert.Equal(REFINES, epubMetadataMeta.Refines);
            Assert.Equal(PROPERTY, epubMetadataMeta.Property);
            Assert.Equal(SCHEME, epubMetadataMeta.Scheme);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if content parameter is null")]
        public void ConstructorWithNullContentTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataMeta(NAME, null!, ID, REFINES, PROPERTY, SCHEME));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataMeta instance with null name parameter should succeed")]
        public void ConstructorWithNullNameTest()
        {
            EpubMetadataMeta epubMetadataMeta = new(null, CONTENT, ID, REFINES, PROPERTY, SCHEME);
            Assert.Null(epubMetadataMeta.Name);
            Assert.Equal(CONTENT, epubMetadataMeta.Content);
            Assert.Equal(ID, epubMetadataMeta.Id);
            Assert.Equal(REFINES, epubMetadataMeta.Refines);
            Assert.Equal(PROPERTY, epubMetadataMeta.Property);
            Assert.Equal(SCHEME, epubMetadataMeta.Scheme);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataMeta instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataMeta epubMetadataMeta = new(NAME, CONTENT, null, REFINES, PROPERTY, SCHEME);
            Assert.Equal(NAME, epubMetadataMeta.Name);
            Assert.Equal(CONTENT, epubMetadataMeta.Content);
            Assert.Null(epubMetadataMeta.Id);
            Assert.Equal(REFINES, epubMetadataMeta.Refines);
            Assert.Equal(PROPERTY, epubMetadataMeta.Property);
            Assert.Equal(SCHEME, epubMetadataMeta.Scheme);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataMeta instance with null refines parameter should succeed")]
        public void ConstructorWithNullRefinesTest()
        {
            EpubMetadataMeta epubMetadataMeta = new(NAME, CONTENT, ID, null, PROPERTY, SCHEME);
            Assert.Equal(NAME, epubMetadataMeta.Name);
            Assert.Equal(CONTENT, epubMetadataMeta.Content);
            Assert.Equal(ID, epubMetadataMeta.Id);
            Assert.Null(epubMetadataMeta.Refines);
            Assert.Equal(PROPERTY, epubMetadataMeta.Property);
            Assert.Equal(SCHEME, epubMetadataMeta.Scheme);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataMeta instance with null property parameter should succeed")]
        public void ConstructorWithNullPropertyTest()
        {
            EpubMetadataMeta epubMetadataMeta = new(NAME, CONTENT, ID, REFINES, null, SCHEME);
            Assert.Equal(NAME, epubMetadataMeta.Name);
            Assert.Equal(CONTENT, epubMetadataMeta.Content);
            Assert.Equal(ID, epubMetadataMeta.Id);
            Assert.Equal(REFINES, epubMetadataMeta.Refines);
            Assert.Null(epubMetadataMeta.Property);
            Assert.Equal(SCHEME, epubMetadataMeta.Scheme);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataMeta instance with null scheme parameter should succeed")]
        public void ConstructorWithNullSchemeTest()
        {
            EpubMetadataMeta epubMetadataMeta = new(NAME, CONTENT, ID, REFINES, PROPERTY, null);
            Assert.Equal(NAME, epubMetadataMeta.Name);
            Assert.Equal(CONTENT, epubMetadataMeta.Content);
            Assert.Equal(ID, epubMetadataMeta.Id);
            Assert.Equal(REFINES, epubMetadataMeta.Refines);
            Assert.Equal(PROPERTY, epubMetadataMeta.Property);
            Assert.Null(epubMetadataMeta.Scheme);
        }
    }
}
