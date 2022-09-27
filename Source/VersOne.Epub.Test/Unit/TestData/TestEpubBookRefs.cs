using VersOne.Epub.Test.Unit.Mocks;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubBookRefs
    {
        public static EpubBookRef CreateMinimalTestEpubBookRef(TestZipFile epubFile, string epubFilePath)
        {
            EpubBookRef result = new(epubFile)
            {
                FilePath = epubFilePath,
                Title = String.Empty,
                Author = String.Empty,
                AuthorList = new List<string>(),
                Description = null,
                Schema = TestEpubSchemas.CreateMinimalTestEpubSchema(),
                Content = new EpubContentRef()
                {
                    Html = new Dictionary<string, EpubTextContentFileRef>(),
                    Css = new Dictionary<string, EpubTextContentFileRef>(),
                    Images = new Dictionary<string, EpubByteContentFileRef>(),
                    Fonts = new Dictionary<string, EpubByteContentFileRef>(),
                    AllFiles = new Dictionary<string, EpubContentFileRef>(),
                    Cover = null
                }
            };
            EpubTextContentFileRef navFileRef = new(result, NAV_FILE_NAME, EpubContentLocation.LOCAL, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            result.Content.Html[NAV_FILE_NAME] = navFileRef;
            result.Content.AllFiles[NAV_FILE_NAME] = navFileRef;
            result.Content.NavigationHtmlFile = navFileRef;
            return result;
        }

        public static EpubBookRef CreateFullTestEpubBookRef(TestZipFile epubFile, string epubFilePath)
        {
            EpubBookRef result = new(epubFile)
            {
                FilePath = epubFilePath,
                Title = BOOK_TITLE,
                Author = BOOK_AUTHOR,
                AuthorList = new List<string> { BOOK_AUTHOR },
                Description = BOOK_DESCRIPTION,
                Schema = TestEpubSchemas.CreateFullTestEpubSchema()
            };
            EpubTextContentFileRef chapter1FileRef = new(result, CHAPTER1_FILE_NAME, EpubContentLocation.LOCAL, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubTextContentFileRef chapter2FileRef = new(result, CHAPTER2_FILE_NAME, EpubContentLocation.LOCAL, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubTextContentFileRef styles1FileRef = new(result, STYLES1_FILE_NAME, EpubContentLocation.LOCAL, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
            EpubTextContentFileRef styles2FileRef = new(result, STYLES2_FILE_NAME, EpubContentLocation.LOCAL, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
            EpubByteContentFileRef image1FileRef = new(result, IMAGE1_FILE_NAME, EpubContentLocation.LOCAL, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubByteContentFileRef image2FileRef = new(result, IMAGE2_FILE_NAME, EpubContentLocation.LOCAL, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubByteContentFileRef font1FileRef = new(result, FONT1_FILE_NAME, EpubContentLocation.LOCAL, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
            EpubByteContentFileRef font2FileRef = new(result, FONT2_FILE_NAME, EpubContentLocation.LOCAL, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
            EpubByteContentFileRef audioFileRef = new(result, AUDIO_FILE_NAME, EpubContentLocation.LOCAL, OTHER_CONTENT_TYPE, AUDIO_MPEG_CONTENT_MIME_TYPE);
            EpubTextContentFileRef remoteTextContentItemRef = new(result, REMOTE_TEXT_CONTENT_ITEM_HREF, EpubContentLocation.REMOTE, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubByteContentFileRef remoteByteContentItemRef = new(result, REMOTE_BYTE_CONTENT_ITEM_HREF, EpubContentLocation.REMOTE, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubTextContentFileRef navFileRef = new(result, NAV_FILE_NAME, EpubContentLocation.LOCAL, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubByteContentFileRef coverFileRef = new(result, COVER_FILE_NAME, EpubContentLocation.LOCAL, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubTextContentFileRef ncxFileRef = new(result, NCX_FILE_NAME, EpubContentLocation.LOCAL, NCX_CONTENT_TYPE, NCX_CONTENT_MIME_TYPE);
            result.Content = new EpubContentRef()
            {
                Html = new Dictionary<string, EpubTextContentFileRef>()
                {
                    {
                        CHAPTER1_FILE_NAME,
                        chapter1FileRef
                    },
                    {
                        CHAPTER2_FILE_NAME,
                        chapter2FileRef
                    },
                    {
                        REMOTE_TEXT_CONTENT_ITEM_HREF,
                        remoteTextContentItemRef
                    },
                    {
                        NAV_FILE_NAME,
                        navFileRef
                    }
                },
                Css = new Dictionary<string, EpubTextContentFileRef>()
                {
                    {
                        STYLES1_FILE_NAME,
                        styles1FileRef
                    },
                    {
                        STYLES2_FILE_NAME,
                        styles2FileRef
                    }
                },
                Images = new Dictionary<string, EpubByteContentFileRef>()
                {
                    {
                        IMAGE1_FILE_NAME,
                        image1FileRef
                    },
                    {
                        IMAGE2_FILE_NAME,
                        image2FileRef
                    },
                    {
                        REMOTE_BYTE_CONTENT_ITEM_HREF,
                        remoteByteContentItemRef
                    },
                    {
                        COVER_FILE_NAME,
                        coverFileRef
                    }
                },
                Fonts = new Dictionary<string, EpubByteContentFileRef>()
                {
                    {
                        FONT1_FILE_NAME,
                        font1FileRef
                    },
                    {
                        FONT2_FILE_NAME,
                        font2FileRef
                    }
                },
                AllFiles = new Dictionary<string, EpubContentFileRef>()
                {
                    {
                        CHAPTER1_FILE_NAME,
                        chapter1FileRef
                    },
                    {
                        CHAPTER2_FILE_NAME,
                        chapter2FileRef
                    },
                    {
                        STYLES1_FILE_NAME,
                        styles1FileRef
                    },
                    {
                        STYLES2_FILE_NAME,
                        styles2FileRef
                    },
                    {
                        IMAGE1_FILE_NAME,
                        image1FileRef
                    },
                    {
                        IMAGE2_FILE_NAME,
                        image2FileRef
                    },
                    {
                        FONT1_FILE_NAME,
                        font1FileRef
                    },
                    {
                        FONT2_FILE_NAME,
                        font2FileRef
                    },
                    {
                        AUDIO_FILE_NAME,
                        audioFileRef
                    },
                    {
                        REMOTE_TEXT_CONTENT_ITEM_HREF,
                        remoteTextContentItemRef
                    },
                    {
                        REMOTE_BYTE_CONTENT_ITEM_HREF,
                        remoteByteContentItemRef
                    },
                    {
                        NAV_FILE_NAME,
                        navFileRef
                    },
                    {
                        COVER_FILE_NAME,
                        coverFileRef
                    },
                    {
                        NCX_FILE_NAME,
                        ncxFileRef
                    }
                },
                NavigationHtmlFile = navFileRef,
                Cover = coverFileRef
            };
            return result;
        }
    }
}
