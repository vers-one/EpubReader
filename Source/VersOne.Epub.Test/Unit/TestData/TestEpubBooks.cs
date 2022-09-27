using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubBooks
    {
        public static EpubBook CreateMinimalTestEpubBook(string epubFilePath)
        {
            EpubTextContentFile navFile = new()
            {
                FileName = NAV_FILE_NAME,
                FilePathInEpubArchive = NAV_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.MINIMAL_NAV_FILE_CONTENT
            };
            return new()
            {
                FilePath = epubFilePath,
                Title = String.Empty,
                Author = String.Empty,
                AuthorList = new List<string>(),
                Description = null,
                CoverImage = null,
                ReadingOrder = new List<EpubTextContentFile>(),
                Navigation = new List<EpubNavigationItem>(),
                Schema = TestEpubSchemas.CreateMinimalTestEpubSchema(),
                Content = new EpubContent()
                {
                    Html = new Dictionary<string, EpubTextContentFile>()
                    {
                        {
                            NAV_FILE_NAME,
                            navFile
                        }
                    },
                    Css = new Dictionary<string, EpubTextContentFile>(),
                    Images = new Dictionary<string, EpubByteContentFile>(),
                    Fonts = new Dictionary<string, EpubByteContentFile>(),
                    AllFiles = new Dictionary<string, EpubContentFile>()
                    {
                        {
                            NAV_FILE_NAME,
                            navFile
                        }
                    },
                    NavigationHtmlFile = navFile,
                    Cover = null
                }
            };
        }

        public static EpubBook CreateFullTestEpubBook(string epubFilePath)
        {
            EpubTextContentFile chapter1File = new()
            {
                FileName = CHAPTER1_FILE_NAME,
                FilePathInEpubArchive = CHAPTER1_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.CHAPTER1_FILE_CONTENT
            };
            EpubTextContentFile chapter2File = new()
            {
                FileName = CHAPTER2_FILE_NAME,
                FilePathInEpubArchive = CHAPTER2_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.CHAPTER2_FILE_CONTENT
            };
            EpubTextContentFile styles1File = new()
            {
                FileName = STYLES1_FILE_NAME,
                FilePathInEpubArchive = STYLES1_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = CSS_CONTENT_TYPE,
                ContentMimeType = CSS_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.STYLES1_FILE_CONTENT
            };
            EpubTextContentFile styles2File = new()
            {
                FileName = STYLES2_FILE_NAME,
                FilePathInEpubArchive = STYLES2_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = CSS_CONTENT_TYPE,
                ContentMimeType = CSS_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.STYLES2_FILE_CONTENT
            };
            EpubByteContentFile image1File = new()
            {
                FileName = IMAGE1_FILE_NAME,
                FilePathInEpubArchive = IMAGE1_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.IMAGE1_FILE_CONTENT
            };
            EpubByteContentFile image2File = new()
            {
                FileName = IMAGE2_FILE_NAME,
                FilePathInEpubArchive = IMAGE2_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.IMAGE2_FILE_CONTENT
            };
            EpubByteContentFile font1File = new()
            {
                FileName = FONT1_FILE_NAME,
                FilePathInEpubArchive = FONT1_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = FONT_CONTENT_TYPE,
                ContentMimeType = FONT_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.FONT1_FILE_CONTENT
            };
            EpubByteContentFile font2File = new()
            {
                FileName = FONT2_FILE_NAME,
                FilePathInEpubArchive = FONT2_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = FONT_CONTENT_TYPE,
                ContentMimeType = FONT_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.FONT2_FILE_CONTENT
            };
            EpubByteContentFile audioFile = new()
            {
                FileName = AUDIO_FILE_NAME,
                FilePathInEpubArchive = AUDIO_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = OTHER_CONTENT_TYPE,
                ContentMimeType = AUDIO_MPEG_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.AUDIO_FILE_CONTENT
            };
            EpubTextContentFile remoteTextContentItem = new()
            {
                FileName = null,
                FilePathInEpubArchive = null,
                Href = REMOTE_TEXT_CONTENT_ITEM_HREF,
                ContentLocation = EpubContentLocation.REMOTE,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                Content = null
            };
            EpubByteContentFile remoteByteContentItem = new()
            {
                FileName = null,
                FilePathInEpubArchive = null,
                Href = REMOTE_BYTE_CONTENT_ITEM_HREF,
                ContentLocation = EpubContentLocation.REMOTE,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                Content = null
            };
            EpubTextContentFile navFile = new()
            {
                FileName = NAV_FILE_NAME,
                FilePathInEpubArchive = NAV_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.FULL_NAV_FILE_CONTENT
            };
            EpubByteContentFile coverFile = new()
            {
                FileName = COVER_FILE_NAME,
                FilePathInEpubArchive = COVER_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.COVER_FILE_CONTENT
            };
            EpubTextContentFile ncxFile = new()
            {
                FileName = NCX_FILE_NAME,
                FilePathInEpubArchive = NCX_FILE_PATH,
                Href = null,
                ContentLocation = EpubContentLocation.LOCAL,
                ContentType = NCX_CONTENT_TYPE,
                ContentMimeType = NCX_CONTENT_MIME_TYPE,
                Content = TestEpubFiles.NCX_FILE_CONTENT
            };
            return new()
            {
                FilePath = epubFilePath,
                Title = BOOK_TITLE,
                Author = BOOK_AUTHOR,
                AuthorList = new List<string> { BOOK_AUTHOR },
                Description = BOOK_DESCRIPTION,
                CoverImage = TestEpubFiles.COVER_FILE_CONTENT,
                ReadingOrder = new List<EpubTextContentFile>()
                {
                    chapter1File,
                    chapter2File
                },
                Navigation = new List<EpubNavigationItem>()
                {
                    new EpubNavigationItem()
                    {
                        Type = EpubNavigationItemType.LINK,
                        Title = "Chapter 1",
                        Link = new EpubNavigationItemLink(CHAPTER1_FILE_NAME, CONTENT_DIRECTORY_PATH),
                        HtmlContentFile = chapter1File,
                        NestedItems = new List<EpubNavigationItem>()
                    },
                    new EpubNavigationItem()
                    {
                        Type = EpubNavigationItemType.LINK,
                        Title = "Chapter 2",
                        Link = new EpubNavigationItemLink(CHAPTER2_FILE_NAME, CONTENT_DIRECTORY_PATH),
                        HtmlContentFile = chapter2File,
                        NestedItems = new List<EpubNavigationItem>()
                    }
                },
                Schema = TestEpubSchemas.CreateFullTestEpubSchema(),
                Content = new EpubContent()
                {
                    Html = new Dictionary<string, EpubTextContentFile>()
                    {
                        {
                            CHAPTER1_FILE_NAME,
                            chapter1File
                        },
                        {
                            CHAPTER2_FILE_NAME,
                            chapter2File
                        },
                        {
                            REMOTE_TEXT_CONTENT_ITEM_HREF,
                            remoteTextContentItem
                        },
                        {
                            NAV_FILE_NAME,
                            navFile
                        }
                    },
                    Css = new Dictionary<string, EpubTextContentFile>()
                    {
                        {
                            STYLES1_FILE_NAME,
                            styles1File
                        },
                        {
                            STYLES2_FILE_NAME,
                            styles2File
                        }
                    },
                    Images = new Dictionary<string, EpubByteContentFile>()
                    {
                        {
                            IMAGE1_FILE_NAME,
                            image1File
                        },
                        {
                            IMAGE2_FILE_NAME,
                            image2File
                        },
                        {
                            REMOTE_BYTE_CONTENT_ITEM_HREF,
                            remoteByteContentItem
                        },
                        {
                            COVER_FILE_NAME,
                            coverFile
                        }
                    },
                    Fonts = new Dictionary<string, EpubByteContentFile>()
                    {
                        {
                            FONT1_FILE_NAME,
                            font1File
                        },
                        {
                            FONT2_FILE_NAME,
                            font2File
                        }
                    },
                    AllFiles = new Dictionary<string, EpubContentFile>()
                    {
                        {
                            CHAPTER1_FILE_NAME,
                            chapter1File
                        },
                        {
                            CHAPTER2_FILE_NAME,
                            chapter2File
                        },
                        {
                            STYLES1_FILE_NAME,
                            styles1File
                        },
                        {
                            STYLES2_FILE_NAME,
                            styles2File
                        },
                        {
                            IMAGE1_FILE_NAME,
                            image1File
                        },
                        {
                            IMAGE2_FILE_NAME,
                            image2File
                        },
                        {
                            FONT1_FILE_NAME,
                            font1File
                        },
                        {
                            FONT2_FILE_NAME,
                            font2File
                        },
                        {
                            AUDIO_FILE_NAME,
                            audioFile
                        },
                        {
                            REMOTE_TEXT_CONTENT_ITEM_HREF,
                            remoteTextContentItem
                        },
                        {
                            REMOTE_BYTE_CONTENT_ITEM_HREF,
                            remoteByteContentItem
                        },
                        {
                            NAV_FILE_NAME,
                            navFile
                        },
                        {
                            COVER_FILE_NAME,
                            coverFile
                        },
                        {
                            NCX_FILE_NAME,
                            ncxFile
                        }
                    },
                    NavigationHtmlFile = navFile,
                    Cover = coverFile
                }
            };
        }
    }
}
