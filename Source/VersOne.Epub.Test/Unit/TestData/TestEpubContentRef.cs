using VersOne.Epub.Test.Unit.Mocks;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubContentRef
    {
        public static EpubContentRef CreateMinimalTestEpubContentRefWithNavigation()
        {
            return new
            (
                cover: null,
                navigationHtmlFile: NavFileRef,
                html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalTextContentFileRef>()
                    {
                        {
                            NAV_FILE_NAME,
                            NavFileRef
                        }
                    }
                ),
                css: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(),
                images: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>(),
                fonts: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>(),
                audio: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>(),
                allFiles: new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalContentFileRef>()
                    {
                        {
                            NAV_FILE_NAME,
                            NavFileRef
                        }
                    }
                )
            );
        }

        public static EpubContentRef CreateFullTestEpubContentRef()
        {
            return new EpubContentRef
            (
                cover: CoverFileRef,
                navigationHtmlFile: NavFileRef,
                html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalTextContentFileRef>()
                    {
                        {
                            CHAPTER1_FILE_NAME,
                            Chapter1FileRef
                        },
                        {
                            CHAPTER2_FILE_NAME,
                            Chapter2FileRef
                        },
                        {
                            NAV_FILE_NAME,
                            NavFileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            REMOTE_HTML_CONTENT_FILE_HREF,
                            RemoteHtmlContentFileRef
                        }
                    }
                ),
                css: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalTextContentFileRef>()
                    {
                        {
                            STYLES1_FILE_NAME,
                            Styles1FileRef
                        },
                        {
                            STYLES2_FILE_NAME,
                            Styles2FileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteTextContentFileRef>()
                    {
                        {
                            REMOTE_CSS_CONTENT_FILE_HREF,
                            RemoteCssContentFileRef
                        }
                    }
                ),
                images: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalByteContentFileRef>()
                    {
                        {
                            IMAGE1_FILE_NAME,
                            Image1FileRef
                        },
                        {
                            IMAGE2_FILE_NAME,
                            Image2FileRef
                        },
                        {
                            COVER_FILE_NAME,
                            CoverFileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            REMOTE_IMAGE_CONTENT_FILE_HREF,
                            RemoteImageContentFileRef
                        }
                    }
                ),
                fonts: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalByteContentFileRef>()
                    {
                        {
                            FONT1_FILE_NAME,
                            Font1FileRef
                        },
                        {
                            FONT2_FILE_NAME,
                            Font2FileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            REMOTE_FONT_CONTENT_FILE_HREF,
                            RemoteFontContentFileRef
                        }
                    }
                ),
                audio: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalByteContentFileRef>()
                    {
                        {
                            AUDIO1_FILE_NAME,
                            Audio1FileRef
                        },
                        {
                            AUDIO2_FILE_NAME,
                            Audio2FileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteByteContentFileRef>()
                    {
                        {
                            REMOTE_AUDIO_CONTENT_FILE_HREF,
                            RemoteAudioContentFileRef
                        }
                    }
                ),
                allFiles: new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>
                (
                    local: new Dictionary<string, EpubLocalContentFileRef>()
                    {
                        {
                            CHAPTER1_FILE_NAME,
                            Chapter1FileRef
                        },
                        {
                            CHAPTER2_FILE_NAME,
                            Chapter2FileRef
                        },
                        {
                            STYLES1_FILE_NAME,
                            Styles1FileRef
                        },
                        {
                            STYLES2_FILE_NAME,
                            Styles2FileRef
                        },
                        {
                            IMAGE1_FILE_NAME,
                            Image1FileRef
                        },
                        {
                            IMAGE2_FILE_NAME,
                            Image2FileRef
                        },
                        {
                            FONT1_FILE_NAME,
                            Font1FileRef
                        },
                        {
                            FONT2_FILE_NAME,
                            Font2FileRef
                        },
                        {
                            AUDIO1_FILE_NAME,
                            Audio1FileRef
                        },
                        {
                            AUDIO2_FILE_NAME,
                            Audio2FileRef
                        },
                        {
                            VIDEO_FILE_NAME,
                            VideoFileRef
                        },
                        {
                            NAV_FILE_NAME,
                            NavFileRef
                        },
                        {
                            COVER_FILE_NAME,
                            CoverFileRef
                        },
                        {
                            NCX_FILE_NAME,
                            NcxFileRef
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteContentFileRef>()
                    {
                        {
                            REMOTE_HTML_CONTENT_FILE_HREF,
                            RemoteHtmlContentFileRef
                        },
                        {
                            REMOTE_CSS_CONTENT_FILE_HREF,
                            RemoteCssContentFileRef
                        },
                        {
                            REMOTE_IMAGE_CONTENT_FILE_HREF,
                            RemoteImageContentFileRef
                        },
                        {
                            REMOTE_FONT_CONTENT_FILE_HREF,
                            RemoteFontContentFileRef
                        },
                        {
                            REMOTE_XML_CONTENT_FILE_HREF,
                            RemoteXmlContentFileRef
                        },
                        {
                            REMOTE_AUDIO_CONTENT_FILE_HREF,
                            RemoteAudioContentFileRef
                        },
                        {
                            REMOTE_VIDEO_CONTENT_FILE_HREF,
                            RemoteVideoContentFileRef
                        }
                    }
                )
            );
        }

        public static EpubLocalTextContentFileRef Chapter1FileRef =>
            CreateLocalTextContentFileRef(CHAPTER1_FILE_NAME, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);

        public static EpubLocalTextContentFileRef Chapter2FileRef =>
            CreateLocalTextContentFileRef(CHAPTER2_FILE_NAME, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
        
        public static EpubLocalTextContentFileRef Styles1FileRef =>
            CreateLocalTextContentFileRef(STYLES1_FILE_NAME, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
        
        public static EpubLocalTextContentFileRef Styles2FileRef =>
            CreateLocalTextContentFileRef(STYLES2_FILE_NAME, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
        
        public static EpubLocalByteContentFileRef Image1FileRef =>
            CreateLocalByteContentFileRef(IMAGE1_FILE_NAME, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
        
        public static EpubLocalByteContentFileRef Image2FileRef =>
            CreateLocalByteContentFileRef(IMAGE2_FILE_NAME, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
        
        public static EpubLocalByteContentFileRef Font1FileRef =>
            CreateLocalByteContentFileRef(FONT1_FILE_NAME, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
        
        public static EpubLocalByteContentFileRef Font2FileRef =>
            CreateLocalByteContentFileRef(FONT2_FILE_NAME, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
        
        public static EpubLocalByteContentFileRef Audio1FileRef =>
            CreateLocalByteContentFileRef(AUDIO1_FILE_NAME, AUDIO_CONTENT_TYPE, AUDIO_MPEG_CONTENT_MIME_TYPE);

        public static EpubLocalByteContentFileRef Audio2FileRef =>
            CreateLocalByteContentFileRef(AUDIO2_FILE_NAME, AUDIO_CONTENT_TYPE, AUDIO_MPEG_CONTENT_MIME_TYPE);

        public static EpubLocalByteContentFileRef VideoFileRef =>
            CreateLocalByteContentFileRef(VIDEO_FILE_NAME, OTHER_CONTENT_TYPE, VIDEO_MP4_CONTENT_MIME_TYPE);

        public static EpubRemoteTextContentFileRef RemoteHtmlContentFileRef =>
            CreateRemoteTextContentFileRef(REMOTE_HTML_CONTENT_FILE_HREF, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
        
        public static EpubRemoteTextContentFileRef RemoteCssContentFileRef =>
            CreateRemoteTextContentFileRef(REMOTE_CSS_CONTENT_FILE_HREF, CSS_CONTENT_TYPE, CSS_CONTENT_MIME_TYPE);
        
        public static EpubRemoteByteContentFileRef RemoteImageContentFileRef =>
            CreateRemoteByteContentFileRef(REMOTE_IMAGE_CONTENT_FILE_HREF, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
        
        public static EpubRemoteByteContentFileRef RemoteFontContentFileRef =>
            CreateRemoteByteContentFileRef(REMOTE_FONT_CONTENT_FILE_HREF, FONT_CONTENT_TYPE, FONT_CONTENT_MIME_TYPE);
        
        public static EpubRemoteTextContentFileRef RemoteXmlContentFileRef =>
            CreateRemoteTextContentFileRef(REMOTE_XML_CONTENT_FILE_HREF, XML_CONTENT_TYPE, XML_CONTENT_MIME_TYPE);
        
        public static EpubRemoteByteContentFileRef RemoteAudioContentFileRef =>
            CreateRemoteByteContentFileRef(REMOTE_AUDIO_CONTENT_FILE_HREF, AUDIO_CONTENT_TYPE, AUDIO_MPEG_CONTENT_MIME_TYPE);

        public static EpubRemoteByteContentFileRef RemoteVideoContentFileRef =>
            CreateRemoteByteContentFileRef(REMOTE_VIDEO_CONTENT_FILE_HREF, OTHER_CONTENT_TYPE, VIDEO_MP4_CONTENT_MIME_TYPE);

        public static EpubLocalTextContentFileRef NavFileRef =>
            CreateLocalTextContentFileRef(NAV_FILE_NAME, HTML_CONTENT_TYPE, HTML_CONTENT_MIME_TYPE);
        
        public static EpubLocalByteContentFileRef CoverFileRef =>
            CreateLocalByteContentFileRef(COVER_FILE_NAME, IMAGE_CONTENT_TYPE, IMAGE_CONTENT_MIME_TYPE);
        
        public static EpubLocalTextContentFileRef NcxFileRef =>
            CreateLocalTextContentFileRef(NCX_FILE_NAME, NCX_CONTENT_TYPE, NCX_CONTENT_MIME_TYPE);

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
