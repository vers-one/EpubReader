using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.TestData;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.Entities
{
    public class EpubBookTests
    {
        private static List<string> AuthorList =>
            new()
            {
                BOOK_AUTHOR
            };

        private static List<EpubLocalTextContentFile> ReadingOrder => TestEpubReadingOrder.CreateFullTestEpubReadingOrder();

        private static List<EpubNavigationItem> Navigation => TestEpubNavigation.CreateFullTestEpubNavigation();

        private static EpubSchema Schema => TestEpubSchemas.CreateFullTestEpubSchema();

        private static EpubContent Content => TestEpubContent.CreateFullTestEpubContent(populateRemoteFilesContents: true);

        [Fact(DisplayName = "Constructing a EpubBook instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubBook epubBook = new(EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, TestEpubFiles.COVER_FILE_CONTENT, ReadingOrder, Navigation, Schema, Content);
            Assert.Equal(EPUB_FILE_PATH, epubBook.FilePath);
            Assert.Equal(BOOK_TITLE, epubBook.Title);
            Assert.Equal(BOOK_AUTHOR, epubBook.Author);
            Assert.Equal(AuthorList, epubBook.AuthorList);
            Assert.Equal(BOOK_DESCRIPTION, epubBook.Description);
            Assert.Equal(TestEpubFiles.COVER_FILE_CONTENT, epubBook.CoverImage);
            EpubContentComparer.CompareEpubLocalTextContentFileLists(ReadingOrder, epubBook.ReadingOrder);
            EpubNavigationItemComparer.CompareNavigationItemLists(Navigation, epubBook.Navigation);
            EpubSchemaComparer.CompareEpubSchemas(Schema, epubBook.Schema);
            EpubContentComparer.CompareEpubContents(Content, epubBook.Content);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if title parameter is null")]
        public void ConstructorWithNullTitleTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubBook(EPUB_FILE_PATH, null!, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, TestEpubFiles.COVER_FILE_CONTENT, ReadingOrder, Navigation, Schema, Content));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if author parameter is null")]
        public void ConstructorWithNullAuthorTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubBook(EPUB_FILE_PATH, BOOK_TITLE, null!, AuthorList, BOOK_DESCRIPTION, TestEpubFiles.COVER_FILE_CONTENT, ReadingOrder, Navigation, Schema, Content));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if schema parameter is null")]
        public void ConstructorWithNullSchemaTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubBook(EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, TestEpubFiles.COVER_FILE_CONTENT, ReadingOrder, Navigation, null!, Content));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if content parameter is null")]
        public void ConstructorWithNullContentTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubBook(EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, TestEpubFiles.COVER_FILE_CONTENT, ReadingOrder, Navigation, Schema, null!));
        }

        [Fact(DisplayName = "Constructing a EpubBook instance with null filePath parameter should succeed")]
        public void ConstructorWithNullFilePathTest()
        {
            EpubBook epubBook = new(null, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, TestEpubFiles.COVER_FILE_CONTENT, ReadingOrder, Navigation, Schema, Content);
            Assert.Null(epubBook.FilePath);
            Assert.Equal(BOOK_TITLE, epubBook.Title);
            Assert.Equal(BOOK_AUTHOR, epubBook.Author);
            Assert.Equal(AuthorList, epubBook.AuthorList);
            Assert.Equal(BOOK_DESCRIPTION, epubBook.Description);
            Assert.Equal(TestEpubFiles.COVER_FILE_CONTENT, epubBook.CoverImage);
            EpubContentComparer.CompareEpubLocalTextContentFileLists(ReadingOrder, epubBook.ReadingOrder);
            EpubNavigationItemComparer.CompareNavigationItemLists(Navigation, epubBook.Navigation);
            EpubSchemaComparer.CompareEpubSchemas(Schema, epubBook.Schema);
            EpubContentComparer.CompareEpubContents(Content, epubBook.Content);
        }

        [Fact(DisplayName = "Constructing a EpubBook instance with null authorList parameter should succeed")]
        public void ConstructorWithNullAuthorListTest()
        {
            EpubBook epubBook = new(EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, null, BOOK_DESCRIPTION, TestEpubFiles.COVER_FILE_CONTENT, ReadingOrder, Navigation, Schema, Content);
            Assert.Equal(EPUB_FILE_PATH, epubBook.FilePath);
            Assert.Equal(BOOK_TITLE, epubBook.Title);
            Assert.Equal(BOOK_AUTHOR, epubBook.Author);
            Assert.Equal(new List<string>(), epubBook.AuthorList);
            Assert.Equal(BOOK_DESCRIPTION, epubBook.Description);
            Assert.Equal(TestEpubFiles.COVER_FILE_CONTENT, epubBook.CoverImage);
            EpubContentComparer.CompareEpubLocalTextContentFileLists(ReadingOrder, epubBook.ReadingOrder);
            EpubNavigationItemComparer.CompareNavigationItemLists(Navigation, epubBook.Navigation);
            EpubSchemaComparer.CompareEpubSchemas(Schema, epubBook.Schema);
            EpubContentComparer.CompareEpubContents(Content, epubBook.Content);
        }

        [Fact(DisplayName = "Constructing a EpubBook instance with null description parameter should succeed")]
        public void ConstructorWithNullDescriptionTest()
        {
            EpubBook epubBook = new(EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, null, TestEpubFiles.COVER_FILE_CONTENT, ReadingOrder, Navigation, Schema, Content);
            Assert.Equal(EPUB_FILE_PATH, epubBook.FilePath);
            Assert.Equal(BOOK_TITLE, epubBook.Title);
            Assert.Equal(BOOK_AUTHOR, epubBook.Author);
            Assert.Equal(AuthorList, epubBook.AuthorList);
            Assert.Null(epubBook.Description);
            Assert.Equal(TestEpubFiles.COVER_FILE_CONTENT, epubBook.CoverImage);
            EpubContentComparer.CompareEpubLocalTextContentFileLists(ReadingOrder, epubBook.ReadingOrder);
            EpubNavigationItemComparer.CompareNavigationItemLists(Navigation, epubBook.Navigation);
            EpubSchemaComparer.CompareEpubSchemas(Schema, epubBook.Schema);
            EpubContentComparer.CompareEpubContents(Content, epubBook.Content);
        }

        [Fact(DisplayName = "Constructing a EpubBook instance with null coverImage parameter should succeed")]
        public void ConstructorWithNullCoverImageTest()
        {
            EpubBook epubBook = new(EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, null, ReadingOrder, Navigation, Schema, Content);
            Assert.Equal(EPUB_FILE_PATH, epubBook.FilePath);
            Assert.Equal(BOOK_TITLE, epubBook.Title);
            Assert.Equal(BOOK_AUTHOR, epubBook.Author);
            Assert.Equal(AuthorList, epubBook.AuthorList);
            Assert.Equal(BOOK_DESCRIPTION, epubBook.Description);
            Assert.Null(epubBook.CoverImage);
            EpubContentComparer.CompareEpubLocalTextContentFileLists(ReadingOrder, epubBook.ReadingOrder);
            EpubNavigationItemComparer.CompareNavigationItemLists(Navigation, epubBook.Navigation);
            EpubSchemaComparer.CompareEpubSchemas(Schema, epubBook.Schema);
            EpubContentComparer.CompareEpubContents(Content, epubBook.Content);
        }

        [Fact(DisplayName = "Constructing a EpubBook instance with null readingOrder parameter should succeed")]
        public void ConstructorWithNullReadingOrderTest()
        {
            EpubBook epubBook = new(EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, TestEpubFiles.COVER_FILE_CONTENT, null, Navigation, Schema, Content);
            Assert.Equal(EPUB_FILE_PATH, epubBook.FilePath);
            Assert.Equal(BOOK_TITLE, epubBook.Title);
            Assert.Equal(BOOK_AUTHOR, epubBook.Author);
            Assert.Equal(AuthorList, epubBook.AuthorList);
            Assert.Equal(BOOK_DESCRIPTION, epubBook.Description);
            Assert.Equal(TestEpubFiles.COVER_FILE_CONTENT, epubBook.CoverImage);
            EpubContentComparer.CompareEpubLocalTextContentFileLists(new List<EpubLocalTextContentFile>(), epubBook.ReadingOrder);
            EpubNavigationItemComparer.CompareNavigationItemLists(Navigation, epubBook.Navigation);
            EpubSchemaComparer.CompareEpubSchemas(Schema, epubBook.Schema);
            EpubContentComparer.CompareEpubContents(Content, epubBook.Content);
        }

        [Fact(DisplayName = "Constructing a EpubBook instance with null navigation parameter should succeed")]
        public void ConstructorWithNullNavigationTest()
        {
            EpubBook epubBook = new(EPUB_FILE_PATH, BOOK_TITLE, BOOK_AUTHOR, AuthorList, BOOK_DESCRIPTION, TestEpubFiles.COVER_FILE_CONTENT, ReadingOrder, null, Schema, Content);
            Assert.Equal(EPUB_FILE_PATH, epubBook.FilePath);
            Assert.Equal(BOOK_TITLE, epubBook.Title);
            Assert.Equal(BOOK_AUTHOR, epubBook.Author);
            Assert.Equal(AuthorList, epubBook.AuthorList);
            Assert.Equal(BOOK_DESCRIPTION, epubBook.Description);
            Assert.Equal(TestEpubFiles.COVER_FILE_CONTENT, epubBook.CoverImage);
            EpubContentComparer.CompareEpubLocalTextContentFileLists(ReadingOrder, epubBook.ReadingOrder);
            Assert.Null(epubBook.Navigation);
            EpubSchemaComparer.CompareEpubSchemas(Schema, epubBook.Schema);
            EpubContentComparer.CompareEpubContents(Content, epubBook.Content);
        }
    }
}
