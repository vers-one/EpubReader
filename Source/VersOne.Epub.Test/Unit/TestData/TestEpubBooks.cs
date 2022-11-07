using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubBooks
    {
        public static EpubBook CreateMinimalTestEpubBook(string epubFilePath)
        {
            EpubLocalTextContentFile navFile = new()
            {
                Key = NAV_FILE_NAME,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                FilePath = NAV_FILE_PATH,
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
                ReadingOrder = new List<EpubLocalTextContentFile>(),
                Navigation = new List<EpubNavigationItem>(),
                Schema = TestEpubSchemas.CreateMinimalTestEpubSchema(),
                Content = new EpubContent()
                {
                    Html = new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalTextContentFile>()
                        {
                            {
                                NAV_FILE_NAME,
                                navFile
                            }
                        },
                        Remote = new Dictionary<string, EpubRemoteTextContentFile>()
                    },
                    Css = new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalTextContentFile>(),
                        Remote = new Dictionary<string, EpubRemoteTextContentFile>()
                    },
                    Images = new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalByteContentFile>(),
                        Remote = new Dictionary<string, EpubRemoteByteContentFile>()
                    },
                    Fonts = new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalByteContentFile>(),
                        Remote = new Dictionary<string, EpubRemoteByteContentFile>()
                    },
                    AllFiles = new EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalContentFile>()
                        {
                            {
                                NAV_FILE_NAME,
                                navFile
                            }
                        },
                        Remote = new Dictionary<string, EpubRemoteContentFile>()
                    },
                    NavigationHtmlFile = navFile,
                    Cover = null
                }
            };
        }

        public static EpubBook CreateMinimalTestEpub2BookWithoutNavigation(string epubFilePath)
        {
            return new()
            {
                FilePath = epubFilePath,
                Title = String.Empty,
                Author = String.Empty,
                AuthorList = new List<string>(),
                Description = null,
                CoverImage = null,
                ReadingOrder = new List<EpubLocalTextContentFile>(),
                Navigation = null,
                Schema = TestEpubSchemas.CreateMinimalTestEpub2SchemaWithoutNavigation(),
                Content = new EpubContent()
                {
                    Html = new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalTextContentFile>(),
                        Remote = new Dictionary<string, EpubRemoteTextContentFile>()
                    },
                    Css = new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalTextContentFile>(),
                        Remote = new Dictionary<string, EpubRemoteTextContentFile>()
                    },
                    Images = new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalByteContentFile>(),
                        Remote = new Dictionary<string, EpubRemoteByteContentFile>()
                    },
                    Fonts = new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalByteContentFile>(),
                        Remote = new Dictionary<string, EpubRemoteByteContentFile>()
                    },
                    AllFiles = new EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalContentFile>(),
                        Remote = new Dictionary<string, EpubRemoteContentFile>()
                    },
                    NavigationHtmlFile = null,
                    Cover = null
                }
            };
        }

        public static EpubBook CreateFullTestEpubBook(string epubFilePath, bool populateRemoteFilesContents)
        {
            EpubLocalTextContentFile chapter1File = new()
            {
                Key = CHAPTER1_FILE_NAME,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                FilePath = CHAPTER1_FILE_PATH,
                Content = TestEpubFiles.CHAPTER1_FILE_CONTENT
            };
            EpubLocalTextContentFile chapter2File = new()
            {
                Key = CHAPTER2_FILE_NAME,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                FilePath = CHAPTER2_FILE_PATH,
                Content = TestEpubFiles.CHAPTER2_FILE_CONTENT
            };
            EpubLocalTextContentFile styles1File = new()
            {
                Key = STYLES1_FILE_NAME,
                ContentType = CSS_CONTENT_TYPE,
                ContentMimeType = CSS_CONTENT_MIME_TYPE,
                FilePath = STYLES1_FILE_PATH,
                Content = TestEpubFiles.STYLES1_FILE_CONTENT
            };
            EpubLocalTextContentFile styles2File = new()
            {
                Key = STYLES2_FILE_NAME,
                ContentType = CSS_CONTENT_TYPE,
                ContentMimeType = CSS_CONTENT_MIME_TYPE,
                FilePath = STYLES2_FILE_PATH,
                Content = TestEpubFiles.STYLES2_FILE_CONTENT
            };
            EpubLocalByteContentFile image1File = new()
            {
                Key = IMAGE1_FILE_NAME,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                FilePath = IMAGE1_FILE_PATH,
                Content = TestEpubFiles.IMAGE1_FILE_CONTENT
            };
            EpubLocalByteContentFile image2File = new()
            {
                Key = IMAGE2_FILE_NAME,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                FilePath = IMAGE2_FILE_PATH,
                Content = TestEpubFiles.IMAGE2_FILE_CONTENT
            };
            EpubLocalByteContentFile font1File = new()
            {
                Key = FONT1_FILE_NAME,
                ContentType = FONT_CONTENT_TYPE,
                ContentMimeType = FONT_CONTENT_MIME_TYPE,
                FilePath = FONT1_FILE_PATH,
                Content = TestEpubFiles.FONT1_FILE_CONTENT
            };
            EpubLocalByteContentFile font2File = new()
            {
                Key = FONT2_FILE_NAME,
                ContentType = FONT_CONTENT_TYPE,
                ContentMimeType = FONT_CONTENT_MIME_TYPE,
                FilePath = FONT2_FILE_PATH,
                Content = TestEpubFiles.FONT2_FILE_CONTENT
            };
            EpubLocalByteContentFile audioFile = new()
            {
                Key = AUDIO_FILE_NAME,
                ContentType = OTHER_CONTENT_TYPE,
                ContentMimeType = AUDIO_MPEG_CONTENT_MIME_TYPE,
                FilePath = AUDIO_FILE_PATH,
                Content = TestEpubFiles.AUDIO_FILE_CONTENT
            };
            EpubRemoteTextContentFile remoteHtmlContentFile = new()
            {
                Key = REMOTE_HTML_CONTENT_FILE_HREF,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                Url = REMOTE_HTML_CONTENT_FILE_HREF,
                Content = populateRemoteFilesContents ? TestEpubFiles.REMOTE_HTML_FILE_CONTENT : null
            };
            EpubRemoteTextContentFile remoteCssContentFile = new()
            {
                Key = REMOTE_CSS_CONTENT_FILE_HREF,
                ContentType = CSS_CONTENT_TYPE,
                ContentMimeType = CSS_CONTENT_MIME_TYPE,
                Url = REMOTE_CSS_CONTENT_FILE_HREF,
                Content = populateRemoteFilesContents ? TestEpubFiles.REMOTE_CSS_FILE_CONTENT : null
            };
            EpubRemoteByteContentFile remoteImageContentFile = new()
            {
                Key = REMOTE_IMAGE_CONTENT_FILE_HREF,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                Url = REMOTE_IMAGE_CONTENT_FILE_HREF,
                Content = populateRemoteFilesContents ? TestEpubFiles.REMOTE_IMAGE_FILE_CONTENT : null
            };
            EpubRemoteByteContentFile remoteFontContentFile = new()
            {
                Key = REMOTE_FONT_CONTENT_FILE_HREF,
                ContentType = FONT_CONTENT_TYPE,
                ContentMimeType = FONT_CONTENT_MIME_TYPE,
                Url = REMOTE_FONT_CONTENT_FILE_HREF,
                Content = populateRemoteFilesContents ? TestEpubFiles.REMOTE_FONT_FILE_CONTENT : null
            };
            EpubRemoteTextContentFile remoteXmlContentFile = new()
            {
                Key = REMOTE_XML_CONTENT_FILE_HREF,
                ContentType = XML_CONTENT_TYPE,
                ContentMimeType = XML_CONTENT_MIME_TYPE,
                Url = REMOTE_XML_CONTENT_FILE_HREF,
                Content = populateRemoteFilesContents ? TestEpubFiles.REMOTE_XML_FILE_CONTENT : null
            };
            EpubRemoteByteContentFile remoteAudioContentFile = new()
            {
                Key = REMOTE_AUDIO_CONTENT_FILE_HREF,
                ContentType = OTHER_CONTENT_TYPE,
                ContentMimeType = AUDIO_MPEG_CONTENT_MIME_TYPE,
                Url = REMOTE_AUDIO_CONTENT_FILE_HREF,
                Content = populateRemoteFilesContents ? TestEpubFiles.REMOTE_AUDIO_FILE_CONTENT : null
            };
            EpubLocalTextContentFile navFile = new()
            {
                Key = NAV_FILE_NAME,
                ContentType = HTML_CONTENT_TYPE,
                ContentMimeType = HTML_CONTENT_MIME_TYPE,
                FilePath = NAV_FILE_PATH,
                Content = TestEpubFiles.FULL_NAV_FILE_CONTENT
            };
            EpubLocalByteContentFile coverFile = new()
            {
                Key = COVER_FILE_NAME,
                ContentType = IMAGE_CONTENT_TYPE,
                ContentMimeType = IMAGE_CONTENT_MIME_TYPE,
                FilePath = COVER_FILE_PATH,
                Content = TestEpubFiles.COVER_FILE_CONTENT
            };
            EpubLocalTextContentFile ncxFile = new()
            {
                Key = NCX_FILE_NAME,
                ContentType = NCX_CONTENT_TYPE,
                ContentMimeType = NCX_CONTENT_MIME_TYPE,
                FilePath = NCX_FILE_PATH,
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
                ReadingOrder = new List<EpubLocalTextContentFile>()
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
                    Html = new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalTextContentFile>()
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
                                NAV_FILE_NAME,
                                navFile
                            }
                        },
                        Remote = new Dictionary<string, EpubRemoteTextContentFile>()
                        {
                            {
                                REMOTE_HTML_CONTENT_FILE_HREF,
                                remoteHtmlContentFile
                            }
                        }
                    },
                    Css = new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalTextContentFile>()
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
                        Remote = new Dictionary<string, EpubRemoteTextContentFile>()
                        {
                            {
                                REMOTE_CSS_CONTENT_FILE_HREF,
                                remoteCssContentFile
                            }
                        }
                    },
                    Images = new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalByteContentFile>()
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
                                COVER_FILE_NAME,
                                coverFile
                            }
                        },
                        Remote = new Dictionary<string, EpubRemoteByteContentFile>()
                        {
                            {
                                REMOTE_IMAGE_CONTENT_FILE_HREF,
                                remoteImageContentFile
                            }
                        }
                    },
                    Fonts = new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalByteContentFile>()
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
                        Remote = new Dictionary<string, EpubRemoteByteContentFile>()
                        {
                            {
                                REMOTE_FONT_CONTENT_FILE_HREF,
                                remoteFontContentFile
                            }
                        }
                    },
                    AllFiles = new EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile>()
                    {
                        Local = new Dictionary<string, EpubLocalContentFile>()
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
                        Remote = new Dictionary<string, EpubRemoteContentFile>()
                        {
                            {
                                REMOTE_HTML_CONTENT_FILE_HREF,
                                remoteHtmlContentFile
                            },
                            {
                                REMOTE_CSS_CONTENT_FILE_HREF,
                                remoteCssContentFile
                            },
                            {
                                REMOTE_IMAGE_CONTENT_FILE_HREF,
                                remoteImageContentFile
                            },
                            {
                                REMOTE_FONT_CONTENT_FILE_HREF,
                                remoteFontContentFile
                            },
                            {
                                REMOTE_XML_CONTENT_FILE_HREF,
                                remoteXmlContentFile
                            },
                            {
                                REMOTE_AUDIO_CONTENT_FILE_HREF,
                                remoteAudioContentFile
                            }
                        }
                    },
                    NavigationHtmlFile = navFile,
                    Cover = coverFile
                }
            };
        }
    }
}
