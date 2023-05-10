using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataCoverageTests
    {
        private const string COVERAGE = "1700-1850";
        private const string ID = "coverage";
        private const EpubTextDirection TEXT_DIRECTION = EpubTextDirection.LEFT_TO_RIGHT;
        private const string LANGUAGE = "en";

        [Fact(DisplayName = "Constructing a EpubMetadataCoverage instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataCoverage epubMetadataCoverage = new(COVERAGE, ID, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(COVERAGE, epubMetadataCoverage.Coverage);
            Assert.Equal(ID, epubMetadataCoverage.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataCoverage.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataCoverage.Language);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if coverage parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataCoverage(null!, ID, TEXT_DIRECTION, LANGUAGE));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataCoverage instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataCoverage epubMetadataCoverage = new(COVERAGE, null, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(COVERAGE, epubMetadataCoverage.Coverage);
            Assert.Null(epubMetadataCoverage.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataCoverage.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataCoverage.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataCoverage instance with null textDirection parameter should succeed")]
        public void ConstructorWithNullTextDirectionTest()
        {
            EpubMetadataCoverage epubMetadataCoverage = new(COVERAGE, ID, null, LANGUAGE);
            Assert.Equal(COVERAGE, epubMetadataCoverage.Coverage);
            Assert.Equal(ID, epubMetadataCoverage.Id);
            Assert.Null(epubMetadataCoverage.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataCoverage.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataCoverage instance with null language parameter should succeed")]
        public void ConstructorWithNullLanguageTest()
        {
            EpubMetadataCoverage epubMetadataCoverage = new(COVERAGE, ID, TEXT_DIRECTION, null);
            Assert.Equal(COVERAGE, epubMetadataCoverage.Coverage);
            Assert.Equal(ID, epubMetadataCoverage.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataCoverage.TextDirection);
            Assert.Null(epubMetadataCoverage.Language);
        }
    }
}
