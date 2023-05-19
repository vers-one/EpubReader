namespace VersOne.Epub.Test.Unit.Entities
{
    public class EpubNavigationItemLinkTests
    {
        private const string CONTENT_FILE_URL_WITH_ANCHOR = "../content/chapter1.html#section1";
        private const string CONTENT_FILE_URL_WITHOUT_ANCHOR = "../content/chapter1.html";
        private const string REMOTE_FILE_URL = "https://example.com/books/123/chapter1.html";
        private const string BASE_DIRECTORY_PATH = "OPS/toc";
        private const string CONTENT_FILE_URL = "../content/chapter1.html";
        private const string CONTENT_FILE_PATH = "OPS/content/chapter1.html";
        private const string ANCHOR = "section1";

        [Fact(DisplayName = "Constructing a EpubNavigationItemLink instance with non-null contentFileUrl, contentFilePath, and anchor parameters should succeed")]
        public void ExplicitConstructorWithNonNullParametersTest()
        {
            EpubNavigationItemLink epubNavigationItemLink = new(CONTENT_FILE_URL_WITHOUT_ANCHOR, CONTENT_FILE_PATH, ANCHOR);
            Assert.Equal(CONTENT_FILE_URL, epubNavigationItemLink.ContentFileUrl);
            Assert.Equal(CONTENT_FILE_PATH, epubNavigationItemLink.ContentFilePath);
            Assert.Equal(ANCHOR, epubNavigationItemLink.Anchor);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contentFileUrl parameter is null")]
        public void ExplicitConstructorWithNullContentFileUrlWithoutAnchorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubNavigationItemLink(null!, CONTENT_FILE_PATH, ANCHOR));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentException if contentFileUrl parameter is a remote URL")]
        public void ExplicitConstructorWithRemoteContentFileUrlWithoutAnchorTest()
        {
            Assert.Throws<ArgumentException>(() => new EpubNavigationItemLink(REMOTE_FILE_URL, CONTENT_FILE_PATH, ANCHOR));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contentFilePath parameter is null")]
        public void ExplicitConstructorWithNullContentFilePathTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubNavigationItemLink(CONTENT_FILE_URL_WITHOUT_ANCHOR, null!, ANCHOR));
        }

        [Fact(DisplayName = "Constructing a EpubNavigationItemLink instance with null anchor parameter should succeed")]
        public void ExplicitConstructorWithNullAnchorTest()
        {
            EpubNavigationItemLink epubNavigationItemLink = new(CONTENT_FILE_URL_WITHOUT_ANCHOR, CONTENT_FILE_PATH, null);
            Assert.Equal(CONTENT_FILE_URL, epubNavigationItemLink.ContentFileUrl);
            Assert.Equal(CONTENT_FILE_PATH, epubNavigationItemLink.ContentFilePath);
            Assert.Null(epubNavigationItemLink.Anchor);
        }

        [Fact(DisplayName = "Constructing a EpubNavigationItemLink instance with non-null contentFileUrlWithAnchor and baseDirectoryPath parameters should succeed")]
        public void UrlConstructorWithNonNullParametersTest()
        {
            EpubNavigationItemLink epubNavigationItemLink = new(CONTENT_FILE_URL_WITH_ANCHOR, BASE_DIRECTORY_PATH);
            Assert.Equal(CONTENT_FILE_URL, epubNavigationItemLink.ContentFileUrl);
            Assert.Equal(CONTENT_FILE_PATH, epubNavigationItemLink.ContentFilePath);
            Assert.Equal(ANCHOR, epubNavigationItemLink.Anchor);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contentFileUrlWithAnchor parameter is null")]
        public void UrlConstructorWithNullContentFileUrlWithAnchorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubNavigationItemLink(null!, BASE_DIRECTORY_PATH));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentException if contentFileUrlWithAnchor parameter is a remote URL")]
        public void UrlConstructorWithRemoteContentFileUrlWithAnchorTest()
        {
            Assert.Throws<ArgumentException>(() => new EpubNavigationItemLink(REMOTE_FILE_URL, BASE_DIRECTORY_PATH));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if baseDirectoryPath parameter is null")]
        public void UrlConstructorWithNullBaseDirectoryPathTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubNavigationItemLink(CONTENT_FILE_URL_WITH_ANCHOR, null!));
        }
    }
}
