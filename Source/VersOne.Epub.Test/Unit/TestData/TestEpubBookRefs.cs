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
                    Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                    {
                        Local = new Dictionary<string, EpubLocalTextContentFileRef>(),
                        Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                    },
                    Css = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                    {
                        Local = new Dictionary<string, EpubLocalTextContentFileRef>(),
                        Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                    },
                    Images = new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
                    {
                        Local = new Dictionary<string, EpubLocalByteContentFileRef>(),
                        Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
                    },
                    Fonts = new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
                    {
                        Local = new Dictionary<string, EpubLocalByteContentFileRef>(),
                        Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
                    },
                    AllFiles = new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>()
                    {
                        Local = new Dictionary<string, EpubLocalContentFileRef>(),
                        Remote = new Dictionary<string, EpubRemoteContentFileRef>()
                    },
                    Cover = null
                }
            };
            EpubLocalTextContentFileRef navFileRef =
                new(new EpubContentFileRefMetadata(NAV_FILE_NAME, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE), NAV_FILE_PATH, new TestEpubContentLoader());
            result.Content.Html.Local[NAV_FILE_NAME] = navFileRef;
            result.Content.AllFiles.Local[NAV_FILE_NAME] = navFileRef;
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
            EpubLocalTextContentFileRef chapter1FileRef = CreateLocalTextContentFileRef(CHAPTER1_FILE_NAME, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubLocalTextContentFileRef chapter2FileRef = CreateLocalTextContentFileRef(CHAPTER2_FILE_NAME, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubLocalTextContentFileRef styles1FileRef = CreateLocalTextContentFileRef(STYLES1_FILE_NAME, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
            EpubLocalTextContentFileRef styles2FileRef = CreateLocalTextContentFileRef(STYLES2_FILE_NAME, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
            EpubLocalByteContentFileRef image1FileRef = CreateLocalByteContentFileRef(IMAGE1_FILE_NAME, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubLocalByteContentFileRef image2FileRef = CreateLocalByteContentFileRef(IMAGE2_FILE_NAME, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubLocalByteContentFileRef font1FileRef = CreateLocalByteContentFileRef(FONT1_FILE_NAME, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
            EpubLocalByteContentFileRef font2FileRef = CreateLocalByteContentFileRef(FONT2_FILE_NAME, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
            EpubLocalByteContentFileRef audioFileRef = CreateLocalByteContentFileRef(AUDIO_FILE_NAME, OTHER_CONTENT_TYPE, AUDIO_MPEG_CONTENT_MIME_TYPE);
            EpubRemoteTextContentFileRef remoteHtmlContentFileRef = CreateRemoteTextContentFileRef(REMOTE_HTML_CONTENT_FILE_HREF, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubRemoteTextContentFileRef remoteCssContentFileRef = CreateRemoteTextContentFileRef(REMOTE_CSS_CONTENT_FILE_HREF, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
            EpubRemoteByteContentFileRef remoteImageContentFileRef = CreateRemoteByteContentFileRef(REMOTE_IMAGE_CONTENT_FILE_HREF, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubRemoteByteContentFileRef remoteFontContentFileRef = CreateRemoteByteContentFileRef(REMOTE_FONT_CONTENT_FILE_HREF, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
            EpubRemoteTextContentFileRef remoteXmlContentFileRef = CreateRemoteTextContentFileRef(REMOTE_XML_CONTENT_FILE_HREF, XML_CONTENT_TYPE, XML_CONTENT_MIME_TYPE);
            EpubRemoteByteContentFileRef remoteAudioContentFileRef = CreateRemoteByteContentFileRef(REMOTE_AUDIO_CONTENT_FILE_HREF, OTHER_CONTENT_TYPE, AUDIO_MPEG_CONTENT_MIME_TYPE);
            EpubLocalTextContentFileRef navFileRef = CreateLocalTextContentFileRef(NAV_FILE_NAME, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
            EpubLocalByteContentFileRef coverFileRef = CreateLocalByteContentFileRef(COVER_FILE_NAME, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
            EpubLocalTextContentFileRef ncxFileRef = CreateLocalTextContentFileRef(NCX_FILE_NAME, NCX_CONTENT_TYPE, NCX_CONTENT_MIME_TYPE);
            result.Content = new EpubContentRef()
            {
                Html = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalTextContentFileRef>()
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
                            NAV_FILE_NAME,
                            navFileRef
                        }
                    },
                    Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            REMOTE_HTML_CONTENT_FILE_HREF,
                            remoteHtmlContentFileRef
                        }
                    }
                },
                Css = new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalTextContentFileRef>()
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
                    Remote = new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            REMOTE_CSS_CONTENT_FILE_HREF,
                            remoteCssContentFileRef
                        }
                    }
                },
                Images = new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalByteContentFileRef>()
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
                            COVER_FILE_NAME,
                            coverFileRef
                        }
                    },
                    Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            REMOTE_IMAGE_CONTENT_FILE_HREF,
                            remoteImageContentFileRef
                        }
                    }
                },
                Fonts = new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalByteContentFileRef>()
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
                    Remote = new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            REMOTE_FONT_CONTENT_FILE_HREF,
                            remoteFontContentFileRef
                        }
                    }
                },
                AllFiles = new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>()
                {
                    Local = new Dictionary<string, EpubLocalContentFileRef>()
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
                    Remote = new Dictionary<string, EpubRemoteContentFileRef>()
                    {
                        {
                            REMOTE_HTML_CONTENT_FILE_HREF,
                            remoteHtmlContentFileRef
                        },
                        {
                            REMOTE_CSS_CONTENT_FILE_HREF,
                            remoteCssContentFileRef
                        },
                        {
                            REMOTE_IMAGE_CONTENT_FILE_HREF,
                            remoteImageContentFileRef
                        },
                        {
                            REMOTE_FONT_CONTENT_FILE_HREF,
                            remoteFontContentFileRef
                        },
                        {
                            REMOTE_XML_CONTENT_FILE_HREF,
                            remoteXmlContentFileRef
                        },
                        {
                            REMOTE_AUDIO_CONTENT_FILE_HREF,
                            remoteAudioContentFileRef
                        }
                    }
                },
                NavigationHtmlFile = navFileRef,
                Cover = coverFileRef
            };
            return result;
        }

        private static EpubLocalTextContentFileRef CreateLocalTextContentFileRef(string fileName, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(fileName, contentType, contentMimeType), $"{CONTENT_DIRECTORY_PATH}/{fileName}", new TestEpubContentLoader());
        }

        private static EpubLocalByteContentFileRef CreateLocalByteContentFileRef(string fileName, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(fileName, contentType, contentMimeType), $"{CONTENT_DIRECTORY_PATH}/{fileName}", new TestEpubContentLoader());
        }

        private static EpubRemoteTextContentFileRef CreateRemoteTextContentFileRef(string href, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(href, contentType, contentMimeType), new TestEpubContentLoader());
        }

        private static EpubRemoteByteContentFileRef CreateRemoteByteContentFileRef(string href, EpubContentType contentType, string contentMimeType)
        {
            return new(new EpubContentFileRefMetadata(href, contentType, contentMimeType), new TestEpubContentLoader());
        }
    }
}
