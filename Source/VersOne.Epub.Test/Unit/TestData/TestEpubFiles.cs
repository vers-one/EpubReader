using VersOne.Epub.Test.Unit.Mocks;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubFiles
    {
        public const string CONTAINER_FILE_CONTENT = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
                <rootfile media-type="application/oebps-package+xml" full-path="{OPF_FILE_PATH}" />
              </rootfiles>
            </container>
            """;

        public const string MINIMAL_OPF_FILE_CONTENT = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest>
                <item id="item-toc" href="{NAV_FILE_NAME}" media-type="{HTML_CONTENT_MIME_TYPE}" properties="nav" />
              </manifest>
              <spine />
            </package>
            """;

        public const string MINIMAL_EPUB2_OPF_FILE_CONTENT_WITHOUT_NAVIGATION = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="2.0">
              <metadata />
              <manifest />
              <spine />
            </package>
            """;

        public const string MINIMAL_NAV_FILE_CONTENT = $"""
            <html xmlns="http://www.w3.org/1999/xhtml">
              <body />
            </html>
            """;

        public const string FULL_OPF_FILE_CONTENT = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" xmlns:opf="http://www.idpf.org/2007/opf" xmlns:dc="http://purl.org/dc/elements/1.1/" version="3.0"
                     unique-identifier="book-uid">
              <metadata>
                <dc:title>{BOOK_TITLE}</dc:title>
                <dc:creator>{BOOK_AUTHOR}</dc:creator>
                <dc:description>{BOOK_DESCRIPTION}</dc:description>
                <dc:identifier id="book-uid">{BOOK_UID}</dc:identifier>
              </metadata>
              <manifest>
                <item id="item-1" href="{CHAPTER1_FILE_NAME}" media-type="{HTML_CONTENT_MIME_TYPE}" />
                <item id="item-2" href="{CHAPTER2_FILE_NAME}" media-type="{HTML_CONTENT_MIME_TYPE}" />
                <item id="item-3" href="{STYLES1_FILE_NAME}" media-type="{CSS_CONTENT_MIME_TYPE}" />
                <item id="item-4" href="{STYLES2_FILE_NAME}" media-type="{CSS_CONTENT_MIME_TYPE}" />
                <item id="item-5" href="{IMAGE1_FILE_NAME}" media-type="{IMAGE_CONTENT_MIME_TYPE}" />
                <item id="item-6" href="{IMAGE2_FILE_NAME}" media-type="{IMAGE_CONTENT_MIME_TYPE}" />
                <item id="item-7" href="{FONT1_FILE_NAME}" media-type="{FONT_CONTENT_MIME_TYPE}" />
                <item id="item-8" href="{FONT2_FILE_NAME}" media-type="{FONT_CONTENT_MIME_TYPE}" />
                <item id="item-9" href="{AUDIO_FILE_NAME}" media-type="{AUDIO_MPEG_CONTENT_MIME_TYPE}" />
                <item id="item-10" href="{REMOTE_HTML_CONTENT_FILE_HREF}" media-type="{HTML_CONTENT_MIME_TYPE}" />
                <item id="item-11" href="{REMOTE_CSS_CONTENT_FILE_HREF}" media-type="{CSS_CONTENT_MIME_TYPE}" />
                <item id="item-12" href="{REMOTE_IMAGE_CONTENT_FILE_HREF}" media-type="{IMAGE_CONTENT_MIME_TYPE}" />
                <item id="item-13" href="{REMOTE_FONT_CONTENT_FILE_HREF}" media-type="{FONT_CONTENT_MIME_TYPE}" />
                <item id="item-14" href="{REMOTE_XML_CONTENT_FILE_HREF}" media-type="{XML_CONTENT_MIME_TYPE}" />
                <item id="item-15" href="{REMOTE_AUDIO_CONTENT_FILE_HREF}" media-type="{AUDIO_MPEG_CONTENT_MIME_TYPE}" />
                <item id="item-toc" href="{NAV_FILE_NAME}" media-type="{HTML_CONTENT_MIME_TYPE}" properties="nav" />
                <item id="item-cover" href="{COVER_FILE_NAME}" media-type="{IMAGE_CONTENT_MIME_TYPE}" properties="cover-image" />
                <item id="ncx" href="{NCX_FILE_NAME}" media-type="{NCX_CONTENT_MIME_TYPE}" />
              </manifest>
              <spine toc="ncx">
                <itemref id="itemref-1" idref="item-1" />
                <itemref id="itemref-2" idref="item-2" />
              </spine>
            </package>
            """;

        public const string NCX_FILE_CONTENT = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <ncx xmlns="http://www.daisy.org/z3986/2005/ncx/" version="2005-1">
              <head>
                <meta name="dtb:uid" content="{BOOK_UID}" />
              </head>
              <docTitle>
                <text>{BOOK_TITLE}</text>
              </docTitle>
              <docAuthor>
                <text>{BOOK_AUTHOR}</text>
              </docAuthor>
              <navMap>
                <navPoint id="navpoint-1">
                  <navLabel>
                    <text>Chapter 1</text>
                  </navLabel>
                  <content src="{CHAPTER1_FILE_NAME}" />
                </navPoint>
                <navPoint id="navpoint-2">
                  <navLabel>
                    <text>Chapter 2</text>
                  </navLabel>
                  <content src="{CHAPTER2_FILE_NAME}" />
                </navPoint>
              </navMap>
            </ncx>
            """;

        public const string FULL_NAV_FILE_CONTENT = $"""
            <html xmlns="http://www.w3.org/1999/xhtml" xmlns:epub="http://www.idpf.org/2007/ops">
              <body>
                <nav epub:type="toc">
                  <ol>
                    <li>
                        <a href="{CHAPTER1_FILE_NAME}">Chapter 1</a>
                    </li>
                    <li>
                        <a href="{CHAPTER2_FILE_NAME}">Chapter 2</a>
                    </li>
                  </ol>
                </nav>
              </body>
            </html>
            """;

        public const string CHAPTER1_FILE_CONTENT = "<html><head><title>Chapter 1</title></head><body><h1>Chapter 1</h1></body></html>";

        public const string CHAPTER2_FILE_CONTENT = "<html><head><title>Chapter 2</title></head><body><h1>Chapter 2</h1></body></html>";

        public const string STYLES1_FILE_CONTENT = ".text{color:#010101}";

        public const string STYLES2_FILE_CONTENT = ".text{color:#020202}";

        public static readonly byte[] IMAGE1_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46, 0x01 };

        public static readonly byte[] IMAGE2_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46, 0x02 };

        public static readonly byte[] COVER_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46, 0xff };

        public static readonly byte[] FONT1_FILE_CONTENT = new byte[] { 0x00, 0x01, 0x00, 0x01 };

        public static readonly byte[] FONT2_FILE_CONTENT = new byte[] { 0x00, 0x01, 0x00, 0x02 };

        public static readonly byte[] AUDIO_FILE_CONTENT = new byte[] { 0x49, 0x44, 0x33, 0x03 };

        public const string REMOTE_HTML_FILE_CONTENT = "<html><head><title>Remote HTML file</title></head><body><h1>Remote HTML file content</h1></body></html>";

        public const string REMOTE_CSS_FILE_CONTENT = ".remote-text{color:#030303}";

        public static readonly byte[] REMOTE_IMAGE_FILE_CONTENT = new byte[] { 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46, 0x03 };

        public static readonly byte[] REMOTE_FONT_FILE_CONTENT = new byte[] { 0x00, 0x01, 0x00, 0x03 };

        public const string REMOTE_XML_FILE_CONTENT = "<test>Remote XML file</test>";

        public static readonly byte[] REMOTE_AUDIO_FILE_CONTENT = new byte[] { 0x49, 0x44, 0x33, 0x04 };

        public static TestZipFile CreateMinimalTestEpubFile()
        {
            TestZipFile result = new();
            result.AddEntry(CONTAINER_FILE_PATH, CONTAINER_FILE_CONTENT);
            result.AddEntry(OPF_FILE_PATH, MINIMAL_OPF_FILE_CONTENT);
            result.AddEntry(NAV_FILE_PATH, MINIMAL_NAV_FILE_CONTENT);
            return result;
        }

        public static TestZipFile CreateMinimalTestEpub2FileWithoutNavigation()
        {
            TestZipFile result = new();
            result.AddEntry(CONTAINER_FILE_PATH, CONTAINER_FILE_CONTENT);
            result.AddEntry(OPF_FILE_PATH, MINIMAL_EPUB2_OPF_FILE_CONTENT_WITHOUT_NAVIGATION);
            return result;
        }

        public static TestZipFile CreateFullTestEpubFile()
        {
            TestZipFile result = new();
            result.AddEntry(CONTAINER_FILE_PATH, CONTAINER_FILE_CONTENT);
            result.AddEntry(OPF_FILE_PATH, FULL_OPF_FILE_CONTENT);
            result.AddEntry(CHAPTER1_FILE_PATH, CHAPTER1_FILE_CONTENT);
            result.AddEntry(CHAPTER2_FILE_PATH, CHAPTER2_FILE_CONTENT);
            result.AddEntry(STYLES1_FILE_PATH, STYLES1_FILE_CONTENT);
            result.AddEntry(STYLES2_FILE_PATH, STYLES2_FILE_CONTENT);
            result.AddEntry(IMAGE1_FILE_PATH, IMAGE1_FILE_CONTENT);
            result.AddEntry(IMAGE2_FILE_PATH, IMAGE2_FILE_CONTENT);
            result.AddEntry(FONT1_FILE_PATH, FONT1_FILE_CONTENT);
            result.AddEntry(FONT2_FILE_PATH, FONT2_FILE_CONTENT);
            result.AddEntry(AUDIO_FILE_PATH, AUDIO_FILE_CONTENT);
            result.AddEntry(NAV_FILE_PATH, FULL_NAV_FILE_CONTENT);
            result.AddEntry(COVER_FILE_PATH, COVER_FILE_CONTENT);
            result.AddEntry(NCX_FILE_PATH, NCX_FILE_CONTENT);
            return result;
        }
    }
}
