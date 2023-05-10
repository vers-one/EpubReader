using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataRelationTests
    {
        private const string RELATION = "https://example.com/books/123/related.html";
        private const string ID = "relation";
        private const EpubTextDirection TEXT_DIRECTION = EpubTextDirection.LEFT_TO_RIGHT;
        private const string LANGUAGE = "en";

        [Fact(DisplayName = "Constructing a EpubMetadataRelation instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataRelation epubMetadataRelation = new(RELATION, ID, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(RELATION, epubMetadataRelation.Relation);
            Assert.Equal(ID, epubMetadataRelation.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataRelation.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataRelation.Language);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if relation parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataRelation(null!, ID, TEXT_DIRECTION, LANGUAGE));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataRelation instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataRelation epubMetadataRelation = new(RELATION, null, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(RELATION, epubMetadataRelation.Relation);
            Assert.Null(epubMetadataRelation.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataRelation.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataRelation.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataRelation instance with null textDirection parameter should succeed")]
        public void ConstructorWithNullTextDirectionTest()
        {
            EpubMetadataRelation epubMetadataRelation = new(RELATION, ID, null, LANGUAGE);
            Assert.Equal(RELATION, epubMetadataRelation.Relation);
            Assert.Equal(ID, epubMetadataRelation.Id);
            Assert.Null(epubMetadataRelation.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataRelation.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataRelation instance with null language parameter should succeed")]
        public void ConstructorWithNullLanguageTest()
        {
            EpubMetadataRelation epubMetadataRelation = new(RELATION, ID, TEXT_DIRECTION, null);
            Assert.Equal(RELATION, epubMetadataRelation.Relation);
            Assert.Equal(ID, epubMetadataRelation.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataRelation.TextDirection);
            Assert.Null(epubMetadataRelation.Language);
        }
    }
}
