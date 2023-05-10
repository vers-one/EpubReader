using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Metadata
{
    public class EpubMetadataSubjectTests
    {
        private const string SUBJECT = "Test Subject";
        private const string ID = "subject";
        private const EpubTextDirection TEXT_DIRECTION = EpubTextDirection.LEFT_TO_RIGHT;
        private const string LANGUAGE = "en";

        [Fact(DisplayName = "Constructing a EpubMetadataSubject instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubMetadataSubject epubMetadataSubject = new(SUBJECT, ID, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(SUBJECT, epubMetadataSubject.Subject);
            Assert.Equal(ID, epubMetadataSubject.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataSubject.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataSubject.Language);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if subject parameter is null")]
        public void ConstructorWithNullContributorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubMetadataSubject(null!, ID, TEXT_DIRECTION, LANGUAGE));
        }

        [Fact(DisplayName = "Constructing a EpubMetadataSubject instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubMetadataSubject epubMetadataSubject = new(SUBJECT, null, TEXT_DIRECTION, LANGUAGE);
            Assert.Equal(SUBJECT, epubMetadataSubject.Subject);
            Assert.Null(epubMetadataSubject.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataSubject.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataSubject.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataSubject instance with null textDirection parameter should succeed")]
        public void ConstructorWithNullTextDirectionTest()
        {
            EpubMetadataSubject epubMetadataSubject = new(SUBJECT, ID, null, LANGUAGE);
            Assert.Equal(SUBJECT, epubMetadataSubject.Subject);
            Assert.Equal(ID, epubMetadataSubject.Id);
            Assert.Null(epubMetadataSubject.TextDirection);
            Assert.Equal(LANGUAGE, epubMetadataSubject.Language);
        }

        [Fact(DisplayName = "Constructing a EpubMetadataSubject instance with null language parameter should succeed")]
        public void ConstructorWithNullLanguageTest()
        {
            EpubMetadataSubject epubMetadataSubject = new(SUBJECT, ID, TEXT_DIRECTION, null);
            Assert.Equal(SUBJECT, epubMetadataSubject.Subject);
            Assert.Equal(ID, epubMetadataSubject.Id);
            Assert.Equal(TEXT_DIRECTION, epubMetadataSubject.TextDirection);
            Assert.Null(epubMetadataSubject.Language);
        }
    }
}
