namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubData
    {
        public const string EPUB_FILE_PATH = "test.epub";
        public const string CONTAINER_FILE_PATH = "META-INF/container.xml";
        public const string CONTENT_DIRECTORY_PATH = "Content";
        public const string OPF_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/content.opf";
        public const EpubContentType HTML_CONTENT_TYPE = EpubContentType.XHTML_1_1;
        public const string HTML_CONTENT_MIME_TYPE = "application/xhtml+xml";
        public const string CHAPTER1_FILE_NAME = "chapter1.html";
        public const string CHAPTER1_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{CHAPTER1_FILE_NAME}";
        public const string CHAPTER2_FILE_NAME = "chapter2.html";
        public const string CHAPTER2_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{CHAPTER2_FILE_NAME}";
        public const EpubContentType CSS_CONTENT_TYPE = EpubContentType.CSS;
        public const string CSS_CONTENT_MIME_TYPE = "text/css";
        public const string STYLES1_FILE_NAME = "styles1.css";
        public const string STYLES1_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{STYLES1_FILE_NAME}";
        public const string STYLES2_FILE_NAME = "styles2.css";
        public const string STYLES2_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{STYLES2_FILE_NAME}";
        public const EpubContentType IMAGE_CONTENT_TYPE = EpubContentType.IMAGE_JPEG;
        public const string IMAGE_CONTENT_MIME_TYPE = "image/jpeg";
        public const string IMAGE1_FILE_NAME = "image1.jpg";
        public const string IMAGE1_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{IMAGE1_FILE_NAME}";
        public const string IMAGE2_FILE_NAME = "image2.jpg";
        public const string IMAGE2_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{IMAGE2_FILE_NAME}";
        public const EpubContentType FONT_CONTENT_TYPE = EpubContentType.FONT_TRUETYPE;
        public const string FONT_CONTENT_MIME_TYPE = "font/truetype";
        public const string FONT1_FILE_NAME = "font1.ttf";
        public const string FONT1_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{FONT1_FILE_NAME}";
        public const string FONT2_FILE_NAME = "font2.ttf";
        public const string FONT2_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{FONT2_FILE_NAME}";
        public const string NAV_FILE_NAME = "toc.html";
        public const string NAV_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{NAV_FILE_NAME}";
        public const string COVER_FILE_NAME = "cover.jpg";
        public const string COVER_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{COVER_FILE_NAME}";
        public const EpubContentType NCX_CONTENT_TYPE = EpubContentType.DTBOOK_NCX;
        public const string NCX_CONTENT_MIME_TYPE = "application/x-dtbncx+xml";
        public const string NCX_FILE_NAME = "toc.ncx";
        public const string NCX_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{NCX_FILE_NAME}";
        public const EpubContentType OTHER_CONTENT_TYPE = EpubContentType.OTHER;
        public const string AUDIO_MPEG_CONTENT_MIME_TYPE = "audio/mpeg";
        public const string AUDIO_FILE_NAME = "audio.mp3";
        public const string AUDIO_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{AUDIO_FILE_NAME}";
        public const string REMOTE_HTML_CONTENT_FILE_HREF = "https://example.com/books/123/test.css";
        public const string REMOTE_CSS_CONTENT_FILE_HREF = "https://example.com/books/123/test.html";
        public const string REMOTE_IMAGE_CONTENT_FILE_HREF = "https://example.com/books/123/image.jpg";
        public const string REMOTE_FONT_CONTENT_FILE_HREF = "https://example.com/books/123/font.ttf";
        public const string REMOTE_XML_CONTENT_FILE_HREF = "https://example.com/books/123/book.xml";
        public const string REMOTE_AUDIO_CONTENT_FILE_HREF = "https://example.com/books/123/chapter1.mp3";
        public const EpubContentType XML_CONTENT_TYPE = EpubContentType.XML;
        public const string XML_CONTENT_MIME_TYPE = "application/xml";
        public const string BOOK_IDENTIFIER_ID = "book-uid";
        public const string BOOK_TITLE = "Test title";
        public const string BOOK_AUTHOR = "John Doe";
        public const string BOOK_DESCRIPTION = "Test description";
        public const string BOOK_UID = "9781234567890";
        public const string COLLECTION_ROLE = "http://example.org/roles/unit";
    }
}
