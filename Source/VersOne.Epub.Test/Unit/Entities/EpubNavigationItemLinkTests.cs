namespace VersOne.Epub.Test.Unit.Entities
{
    public class EpubNavigationItemLinkTests
    {
        private const string CONTENT_FILE_URL = "../content/chapter1.html#section1";
        private const string BASE_DIRECTORY_PATH = "OPS/toc";
        private const string CONTENT_FILE_NAME = "../content/chapter1.html";
        private const string CONTENT_FILE_PATH = "OPS/content/chapter1.html";
        private const string ANCHOR = "section1";

        [Fact(DisplayName = "Constructing a EpubNavigationItemLink instance with non-null contentFileName, contentFilePath, and anchor parameters should succeed")]
        public void ExplicitConstructorWithNonNullParametersTest()
        {
            EpubNavigationItemLink epubNavigationItemLink = new(CONTENT_FILE_NAME, CONTENT_FILE_PATH, ANCHOR);
            Assert.Equal(CONTENT_FILE_NAME, epubNavigationItemLink.ContentFileName);
            Assert.Equal(CONTENT_FILE_PATH, epubNavigationItemLink.ContentFilePath);
            Assert.Equal(ANCHOR, epubNavigationItemLink.Anchor);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contentFileName parameter is null")]
        public void ExplicitConstructorWithNullContentFileNameTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubNavigationItemLink(null!, CONTENT_FILE_PATH, ANCHOR));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contentFilePath parameter is null")]
        public void ExplicitConstructorWithNullContentFilePathTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubNavigationItemLink(CONTENT_FILE_NAME, null!, ANCHOR));
        }

        [Fact(DisplayName = "Constructing a EpubNavigationItemLink instance with null anchor parameter should succeed")]
        public void ExplicitConstructorWithNullAnchorTest()
        {
            EpubNavigationItemLink epubNavigationItemLink = new(CONTENT_FILE_NAME, CONTENT_FILE_PATH, null);
            Assert.Equal(CONTENT_FILE_NAME, epubNavigationItemLink.ContentFileName);
            Assert.Equal(CONTENT_FILE_PATH, epubNavigationItemLink.ContentFilePath);
            Assert.Null(epubNavigationItemLink.Anchor);
        }

        [Fact(DisplayName = "Constructing a EpubNavigationItemLink instance with non-null contentFileUrl and baseDirectoryPath parameters should succeed")]
        public void UrlConstructorWithNonNullParametersTest()
        {
            EpubNavigationItemLink epubNavigationItemLink = new(CONTENT_FILE_URL, BASE_DIRECTORY_PATH);
            Assert.Equal(CONTENT_FILE_NAME, epubNavigationItemLink.ContentFileName);
            Assert.Equal(CONTENT_FILE_PATH, epubNavigationItemLink.ContentFilePath);
            Assert.Equal(ANCHOR, epubNavigationItemLink.Anchor);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contentFileUrl parameter is null")]
        public void UrlConstructorWithNullContentFileUrlTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubNavigationItemLink(null!, BASE_DIRECTORY_PATH));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if baseDirectoryPath parameter is null")]
        public void UrlConstructorWithNullBaseDirectoryPathTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubNavigationItemLink(CONTENT_FILE_URL, null!));
        }
    }
}
