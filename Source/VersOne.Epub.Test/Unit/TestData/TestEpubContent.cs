using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubContent
    {
        public static EpubContent CreateMinimalTestEpubContentWithNavigation()
        {
            return new EpubContent
            (
                cover: null,
                navigationHtmlFile: MinimalNavFile,
                html: new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>
                (
                    local: new Dictionary<string, EpubLocalTextContentFile>()
                    {
                        {
                            NAV_FILE_NAME,
                            MinimalNavFile
                        }
                    }
                ),
                css: new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>(),
                images: new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>(),
                fonts: new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>(),
                audio: new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>(),
                allFiles: new EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile>
                (
                    local: new Dictionary<string, EpubLocalContentFile>()
                    {
                        {
                            NAV_FILE_NAME,
                            MinimalNavFile
                        }
                    }
                )
            );
        }

        public static EpubContent CreateFullTestEpubContent(bool populateRemoteFilesContents)
        {
            return new
            (
                cover: CoverFile,
                navigationHtmlFile: FullNavFile,
                html: new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>
                (
                    local: new Dictionary<string, EpubLocalTextContentFile>()
                    {
                        {
                            CHAPTER1_FILE_NAME,
                            Chapter1File
                        },
                        {
                            CHAPTER2_FILE_NAME,
                            Chapter2File
                        },
                        {
                            NAV_FILE_NAME,
                            FullNavFile
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteTextContentFile>()
                    {
                        {
                            REMOTE_HTML_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteHtmlContentFile : RemoteHtmlContentFileWithNoContent
                        }
                    }
                ),
                css: new EpubContentCollection<EpubLocalTextContentFile, EpubRemoteTextContentFile>
                (
                    local: new Dictionary<string, EpubLocalTextContentFile>()
                    {
                        {
                            STYLES1_FILE_NAME,
                            Styles1File
                        },
                        {
                            STYLES2_FILE_NAME,
                            Styles2File
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteTextContentFile>()
                    {
                        {
                            REMOTE_CSS_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteCssContentFile : RemoteCssContentFileWithNoContent
                        }
                    }
                ),
                images: new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>
                (
                    local: new Dictionary<string, EpubLocalByteContentFile>()
                    {
                        {
                            IMAGE1_FILE_NAME,
                            Image1File
                        },
                        {
                            IMAGE2_FILE_NAME,
                            Image2File
                        },
                        {
                            COVER_FILE_NAME,
                            CoverFile
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteByteContentFile>()
                    {
                        {
                            REMOTE_IMAGE_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteImageContentFile : RemoteImageContentFileWithNoContent
                        }
                    }
                ),
                fonts: new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>
                (
                    local: new Dictionary<string, EpubLocalByteContentFile>()
                    {
                        {
                            FONT1_FILE_NAME,
                            Font1File
                        },
                        {
                            FONT2_FILE_NAME,
                            Font2File
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteByteContentFile>()
                    {
                        {
                            REMOTE_FONT_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteFontContentFile : RemoteFontContentFileWithNoContent
                        }
                    }
                ),
                audio: new EpubContentCollection<EpubLocalByteContentFile, EpubRemoteByteContentFile>
                (
                    local: new Dictionary<string, EpubLocalByteContentFile>()
                    {
                        {
                            AUDIO1_FILE_NAME,
                            Audio1File
                        },
                        {
                            AUDIO2_FILE_NAME,
                            Audio2File
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteByteContentFile>()
                    {
                        {
                            REMOTE_AUDIO_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteAudioContentFile : RemoteAudioContentFileWithNoContent
                        }
                    }
                ),
                allFiles: new EpubContentCollection<EpubLocalContentFile, EpubRemoteContentFile>
                (
                    local: new Dictionary<string, EpubLocalContentFile>()
                    {
                        {
                            CHAPTER1_FILE_NAME,
                            Chapter1File
                        },
                        {
                            CHAPTER2_FILE_NAME,
                            Chapter2File
                        },
                        {
                            STYLES1_FILE_NAME,
                            Styles1File
                        },
                        {
                            STYLES2_FILE_NAME,
                            Styles2File
                        },
                        {
                            IMAGE1_FILE_NAME,
                            Image1File
                        },
                        {
                            IMAGE2_FILE_NAME,
                            Image2File
                        },
                        {
                            FONT1_FILE_NAME,
                            Font1File
                        },
                        {
                            FONT2_FILE_NAME,
                            Font2File
                        },
                        {
                            AUDIO1_FILE_NAME,
                            Audio1File
                        },
                        {
                            AUDIO2_FILE_NAME,
                            Audio2File
                        },
                        {
                            VIDEO_FILE_NAME,
                            VideoFile
                        },
                        {
                            NAV_FILE_NAME,
                            FullNavFile
                        },
                        {
                            COVER_FILE_NAME,
                            CoverFile
                        },
                        {
                            NCX_FILE_NAME,
                            NcxFile
                        }
                    },
                    remote: new Dictionary<string, EpubRemoteContentFile>()
                    {
                        {
                            REMOTE_HTML_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteHtmlContentFile : RemoteHtmlContentFileWithNoContent
                        },
                        {
                            REMOTE_CSS_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteCssContentFile : RemoteCssContentFileWithNoContent
                        },
                        {
                            REMOTE_IMAGE_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteImageContentFile : RemoteImageContentFileWithNoContent
                        },
                        {
                            REMOTE_FONT_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteFontContentFile : RemoteFontContentFileWithNoContent
                        },
                        {
                            REMOTE_XML_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteXmlContentFile : RemoteXmlContentFileWithNoContent
                        },
                        {
                            REMOTE_AUDIO_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteAudioContentFile : RemoteAudioContentFileWithNoContent
                        },
                        {
                            REMOTE_VIDEO_CONTENT_FILE_HREF,
                            populateRemoteFilesContents ? RemoteVideoContentFile : RemoteVideoContentFileWithNoContent
                        }
                    }
                )
            );
        }

        public static EpubLocalTextContentFile Chapter1File =>
            new
            (
                key: CHAPTER1_FILE_NAME,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                filePath: CHAPTER1_FILE_PATH,
                content: TestEpubFiles.CHAPTER1_FILE_CONTENT
            );

        public static EpubLocalTextContentFile Chapter2File =>
            new
            (
                key: CHAPTER2_FILE_NAME,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                filePath: CHAPTER2_FILE_PATH,
                content: TestEpubFiles.CHAPTER2_FILE_CONTENT
            );

        public static EpubLocalTextContentFile Styles1File =>
            new
            (
                key: STYLES1_FILE_NAME,
                contentType: CSS_CONTENT_TYPE,
                contentMimeType: CSS_CONTENT_MIME_TYPE,
                filePath: STYLES1_FILE_PATH,
                content: TestEpubFiles.STYLES1_FILE_CONTENT
            );

        public static EpubLocalTextContentFile Styles2File =>
            new
            (
                key: STYLES2_FILE_NAME,
                contentType: CSS_CONTENT_TYPE,
                contentMimeType: CSS_CONTENT_MIME_TYPE,
                filePath: STYLES2_FILE_PATH,
                content: TestEpubFiles.STYLES2_FILE_CONTENT
            );

        public static EpubLocalByteContentFile Image1File =>
            new
            (
                key: IMAGE1_FILE_NAME,
                contentType: IMAGE_CONTENT_TYPE,
                contentMimeType: IMAGE_CONTENT_MIME_TYPE,
                filePath: IMAGE1_FILE_PATH,
                content: TestEpubFiles.IMAGE1_FILE_CONTENT
            );

        public static EpubLocalByteContentFile Image2File =>
            new
            (
                key: IMAGE2_FILE_NAME,
                contentType: IMAGE_CONTENT_TYPE,
                contentMimeType: IMAGE_CONTENT_MIME_TYPE,
                filePath: IMAGE2_FILE_PATH,
                content: TestEpubFiles.IMAGE2_FILE_CONTENT
            );

        public static EpubLocalByteContentFile Font1File =>
            new
            (
                key: FONT1_FILE_NAME,
                contentType: FONT_CONTENT_TYPE,
                contentMimeType: FONT_CONTENT_MIME_TYPE,
                filePath: FONT1_FILE_PATH,
                content: TestEpubFiles.FONT1_FILE_CONTENT
            );

        public static EpubLocalByteContentFile Font2File =>
            new
            (
                key: FONT2_FILE_NAME,
                contentType: FONT_CONTENT_TYPE,
                contentMimeType: FONT_CONTENT_MIME_TYPE,
                filePath: FONT2_FILE_PATH,
                content: TestEpubFiles.FONT2_FILE_CONTENT
            );

        public static EpubLocalByteContentFile Audio1File =>
            new
            (
                key: AUDIO1_FILE_NAME,
                contentType: AUDIO_CONTENT_TYPE,
                contentMimeType: AUDIO_MPEG_CONTENT_MIME_TYPE,
                filePath: AUDIO1_FILE_PATH,
                content: TestEpubFiles.AUDIO_FILE_CONTENT
            );

        public static EpubLocalByteContentFile Audio2File =>
            new
            (
                key: AUDIO2_FILE_NAME,
                contentType: AUDIO_CONTENT_TYPE,
                contentMimeType: AUDIO_MPEG_CONTENT_MIME_TYPE,
                filePath: AUDIO2_FILE_PATH,
                content: TestEpubFiles.AUDIO_FILE_CONTENT
            );

        public static EpubLocalByteContentFile VideoFile =>
            new
            (
                key: VIDEO_FILE_NAME,
                contentType: OTHER_CONTENT_TYPE,
                contentMimeType: VIDEO_MP4_CONTENT_MIME_TYPE,
                filePath: VIDEO_FILE_PATH,
                content: TestEpubFiles.VIDEO_FILE_CONTENT
            );

        public static EpubRemoteTextContentFile RemoteHtmlContentFile =>
            new
            (
                key: REMOTE_HTML_CONTENT_FILE_HREF,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                url: REMOTE_HTML_CONTENT_FILE_HREF,
                content: TestEpubFiles.REMOTE_HTML_FILE_CONTENT
            );

        public static EpubRemoteTextContentFile RemoteHtmlContentFileWithNoContent =>
            new
            (
                key: REMOTE_HTML_CONTENT_FILE_HREF,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                url: REMOTE_HTML_CONTENT_FILE_HREF,
                content: null
            );

        public static EpubRemoteTextContentFile RemoteCssContentFile =>
            new
            (
                key: REMOTE_CSS_CONTENT_FILE_HREF,
                contentType: CSS_CONTENT_TYPE,
                contentMimeType: CSS_CONTENT_MIME_TYPE,
                url: REMOTE_CSS_CONTENT_FILE_HREF,
                content: TestEpubFiles.REMOTE_CSS_FILE_CONTENT
            );

        public static EpubRemoteTextContentFile RemoteCssContentFileWithNoContent =>
            new
            (
                key: REMOTE_CSS_CONTENT_FILE_HREF,
                contentType: CSS_CONTENT_TYPE,
                contentMimeType: CSS_CONTENT_MIME_TYPE,
                url: REMOTE_CSS_CONTENT_FILE_HREF,
                content: null
            );

        public static EpubRemoteByteContentFile RemoteImageContentFile =>
            new
            (
                key: REMOTE_IMAGE_CONTENT_FILE_HREF,
                contentType: IMAGE_CONTENT_TYPE,
                contentMimeType: IMAGE_CONTENT_MIME_TYPE,
                url: REMOTE_IMAGE_CONTENT_FILE_HREF,
                content: TestEpubFiles.REMOTE_IMAGE_FILE_CONTENT
            );

        public static EpubRemoteByteContentFile RemoteImageContentFileWithNoContent =>
            new
            (
                key: REMOTE_IMAGE_CONTENT_FILE_HREF,
                contentType: IMAGE_CONTENT_TYPE,
                contentMimeType: IMAGE_CONTENT_MIME_TYPE,
                url: REMOTE_IMAGE_CONTENT_FILE_HREF,
                content: null
            );

        public static EpubRemoteByteContentFile RemoteFontContentFile =>
            new
            (
                key: REMOTE_FONT_CONTENT_FILE_HREF,
                contentType: FONT_CONTENT_TYPE,
                contentMimeType: FONT_CONTENT_MIME_TYPE,
                url: REMOTE_FONT_CONTENT_FILE_HREF,
                content: TestEpubFiles.REMOTE_FONT_FILE_CONTENT
            );

        public static EpubRemoteByteContentFile RemoteFontContentFileWithNoContent =>
            new
            (
                key: REMOTE_FONT_CONTENT_FILE_HREF,
                contentType: FONT_CONTENT_TYPE,
                contentMimeType: FONT_CONTENT_MIME_TYPE,
                url: REMOTE_FONT_CONTENT_FILE_HREF,
                content: null
            );

        public static EpubRemoteTextContentFile RemoteXmlContentFile =>
            new
            (
                key: REMOTE_XML_CONTENT_FILE_HREF,
                contentType: XML_CONTENT_TYPE,
                contentMimeType: XML_CONTENT_MIME_TYPE,
                url: REMOTE_XML_CONTENT_FILE_HREF,
                content: TestEpubFiles.REMOTE_XML_FILE_CONTENT
            );

        public static EpubRemoteTextContentFile RemoteXmlContentFileWithNoContent =>
            new
            (
                key: REMOTE_XML_CONTENT_FILE_HREF,
                contentType: XML_CONTENT_TYPE,
                contentMimeType: XML_CONTENT_MIME_TYPE,
                url: REMOTE_XML_CONTENT_FILE_HREF,
                content: null
            );

        public static EpubRemoteByteContentFile RemoteAudioContentFile =>
            new
            (
                key: REMOTE_AUDIO_CONTENT_FILE_HREF,
                contentType: AUDIO_CONTENT_TYPE,
                contentMimeType: AUDIO_MPEG_CONTENT_MIME_TYPE,
                url: REMOTE_AUDIO_CONTENT_FILE_HREF,
                content: TestEpubFiles.REMOTE_AUDIO_FILE_CONTENT
            );

        public static EpubRemoteByteContentFile RemoteAudioContentFileWithNoContent =>
            new
            (
                key: REMOTE_AUDIO_CONTENT_FILE_HREF,
                contentType: AUDIO_CONTENT_TYPE,
                contentMimeType: AUDIO_MPEG_CONTENT_MIME_TYPE,
                url: REMOTE_AUDIO_CONTENT_FILE_HREF,
                content: null
            );

        public static EpubRemoteByteContentFile RemoteVideoContentFile =>
            new
            (
                key: REMOTE_VIDEO_CONTENT_FILE_HREF,
                contentType: OTHER_CONTENT_TYPE,
                contentMimeType: VIDEO_MP4_CONTENT_MIME_TYPE,
                url: REMOTE_VIDEO_CONTENT_FILE_HREF,
                content: TestEpubFiles.REMOTE_VIDEO_FILE_CONTENT
            );

        public static EpubRemoteByteContentFile RemoteVideoContentFileWithNoContent =>
            new
            (
                key: REMOTE_VIDEO_CONTENT_FILE_HREF,
                contentType: OTHER_CONTENT_TYPE,
                contentMimeType: VIDEO_MP4_CONTENT_MIME_TYPE,
                url: REMOTE_VIDEO_CONTENT_FILE_HREF,
                content: null
            );

        public static EpubLocalTextContentFile MinimalNavFile =>
            new
            (
                key: NAV_FILE_NAME,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                filePath: NAV_FILE_PATH,
                content: TestEpubFiles.MINIMAL_NAV_FILE_CONTENT
            );

        public static EpubLocalTextContentFile FullNavFile =>
            new
            (
                key: NAV_FILE_NAME,
                contentType: HTML_CONTENT_TYPE,
                contentMimeType: HTML_CONTENT_MIME_TYPE,
                filePath: NAV_FILE_PATH,
                content: TestEpubFiles.FULL_NAV_FILE_CONTENT
            );

        public static EpubLocalByteContentFile CoverFile =>
            new
            (
                key: COVER_FILE_NAME,
                contentType: IMAGE_CONTENT_TYPE,
                contentMimeType: IMAGE_CONTENT_MIME_TYPE,
                filePath: COVER_FILE_PATH,
                content: TestEpubFiles.COVER_FILE_CONTENT
            );

        public static EpubLocalTextContentFile NcxFile =>
            new
            (
                key: NCX_FILE_NAME,
                contentType: NCX_CONTENT_TYPE,
                contentMimeType: NCX_CONTENT_MIME_TYPE,
                filePath: NCX_FILE_PATH,
                content: TestEpubFiles.NCX_FILE_CONTENT
            );
    }
}
