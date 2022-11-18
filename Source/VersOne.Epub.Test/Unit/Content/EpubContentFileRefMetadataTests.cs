namespace VersOne.Epub.Test.Unit.Content
{
    public class EpubContentFileRefMetadataTests
    {
        private const string KEY = "test.html";
        private const EpubContentType CONTENT_TYPE = EpubContentType.XHTML_1_1;
        private const string CONTENT_MIME_TYPE = "application/xhtml+xml";

        [Fact(DisplayName = "Constructing a EpubContentFileRefMetadata instance with non-empty constructor parameters should succeed")]
        public void ConstructorWithNonEmptyParametersTest()
        {
            EpubContentFileRefMetadata epubContentFileRefMetadata = new(KEY, CONTENT_TYPE, CONTENT_MIME_TYPE);
            Assert.Equal(KEY, epubContentFileRefMetadata.Key);
            Assert.Equal(CONTENT_TYPE, epubContentFileRefMetadata.ContentType);
            Assert.Equal(CONTENT_MIME_TYPE, epubContentFileRefMetadata.ContentMimeType);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if key parameter is null")]
        public void ConstructorWithNullKeyTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubContentFileRefMetadata(null!, CONTENT_TYPE, CONTENT_MIME_TYPE));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentException if key parameter is empty")]
        public void ConstructorWithEmptyKeyTest()
        {
            Assert.Throws<ArgumentException>(() => new EpubContentFileRefMetadata(String.Empty, CONTENT_TYPE, CONTENT_MIME_TYPE));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contentMimeType parameter is null")]
        public void ConstructorWithNullContentMimeTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubContentFileRefMetadata(KEY, CONTENT_TYPE, null!));
        }
    }
}
