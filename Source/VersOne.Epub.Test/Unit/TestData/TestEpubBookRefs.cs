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
            EpubTextContentFileRef navFileRef = new(NAV_FILE_NAME, EpubContentLocation.LOCAL, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE, epubFile, CONTENT_DIRECTORY_PATH);
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
            EpubTextContentFileRef chapter1FileRef = CreateLocalTextContentFileRef(epubFile, CHAPTER1_FILE_NAME, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubTextContentFileRef chapter2FileRef = CreateLocalTextContentFileRef(epubFile, CHAPTER2_FILE_NAME, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubTextContentFileRef styles1FileRef = CreateLocalTextContentFileRef(epubFile, STYLES1_FILE_NAME, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
            EpubTextContentFileRef styles2FileRef = CreateLocalTextContentFileRef(epubFile, STYLES2_FILE_NAME, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
            EpubByteContentFileRef image1FileRef = CreateLocalByteContentFileRef(epubFile, IMAGE1_FILE_NAME, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubByteContentFileRef image2FileRef = CreateLocalByteContentFileRef(epubFile, IMAGE2_FILE_NAME, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubByteContentFileRef font1FileRef = CreateLocalByteContentFileRef(epubFile, FONT1_FILE_NAME, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
            EpubByteContentFileRef font2FileRef = CreateLocalByteContentFileRef(epubFile, FONT2_FILE_NAME, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
            EpubByteContentFileRef audioFileRef = CreateLocalByteContentFileRef(epubFile, AUDIO_FILE_NAME, OTHER_CONTENT_TYPE, AUDIO_MPEG_CONTENT_MIME_TYPE);
            EpubTextContentFileRef remoteTextContentItemRef = CreateRemoteTextContentFileRef(epubFile, REMOTE_TEXT_CONTENT_ITEM_HREF, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubByteContentFileRef remoteByteContentItemRef = CreateRemoteByteContentFileRef(epubFile, REMOTE_BYTE_CONTENT_ITEM_HREF, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubTextContentFileRef navFileRef = CreateLocalTextContentFileRef(epubFile, NAV_FILE_NAME, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubByteContentFileRef coverFileRef = CreateLocalByteContentFileRef(epubFile, COVER_FILE_NAME, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubTextContentFileRef ncxFileRef = CreateLocalTextContentFileRef(epubFile, NCX_FILE_NAME, NCX_CONTENT_TYPE, NCX_CONTENT_MIME_TYPE);
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

        private static EpubTextContentFileRef CreateLocalTextContentFileRef(TestZipFile testZipFile, string fileName, EpubContentType contentType, string contentMimeType)
        {
            return new(fileName, EpubContentLocation.LOCAL, contentType, contentMimeType, testZipFile, CONTENT_DIRECTORY_PATH);
        }

        private static EpubByteContentFileRef CreateLocalByteContentFileRef(TestZipFile testZipFile, string fileName, EpubContentType contentType, string contentMimeType)
        {
            return new(fileName, EpubContentLocation.LOCAL, contentType, contentMimeType, testZipFile, CONTENT_DIRECTORY_PATH);
        }

        private static EpubTextContentFileRef CreateRemoteTextContentFileRef(TestZipFile testZipFile, string href, EpubContentType contentType, string contentMimeType)
        {
            return new(href, EpubContentLocation.REMOTE, contentType, contentMimeType, testZipFile, CONTENT_DIRECTORY_PATH);
        }

        private static EpubByteContentFileRef CreateRemoteByteContentFileRef(TestZipFile testZipFile, string href, EpubContentType contentType, string contentMimeType)
        {
            return new(href, EpubContentLocation.REMOTE, contentType, contentMimeType, testZipFile, CONTENT_DIRECTORY_PATH);
        }
    }
}
