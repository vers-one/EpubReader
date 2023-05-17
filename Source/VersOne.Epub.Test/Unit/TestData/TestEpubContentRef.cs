using VersOne.Epub.Test.Unit.Mocks;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubContentRef
    {
        public static EpubContentRef CreateMinimalTestEpubContentRefWithNavigation()
        {
            List<EpubLocalTextContentFileRef> htmlLocal = new()
            {
                NavFileRef
            };
            List<EpubLocalContentFileRef> allFilesLocal = new()
            {
                NavFileRef
            };
            return new
            (
                cover: null,
                navigationHtmlFile: NavFileRef,
                html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(htmlLocal.AsReadOnly()),
                css: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(),
                images: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>(),
                fonts: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>(),
                audio: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>(),
                allFiles: new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>(allFilesLocal.AsReadOnly())
            );
        }

        public static EpubContentRef CreateFullTestEpubContentRef()
        {
            List<EpubLocalTextContentFileRef> htmlLocal = new()
            {
                Chapter1FileRef,
                Chapter2FileRef,
                NavFileRef
            };
            List<EpubRemoteTextContentFileRef> htmlRemote = new()
            {
                RemoteHtmlContentFileRef
            };
            List<EpubLocalTextContentFileRef> cssLocal = new()
            {
                Styles1FileRef,
                Styles2FileRef
            };
            List<EpubRemoteTextContentFileRef> cssRemote = new()
            {
                RemoteCssContentFileRef
            };
            List<EpubLocalByteContentFileRef> imagesLocal = new()
            {
                Image1FileRef,
                Image2FileRef,
                CoverFileRef
            };
            List<EpubRemoteByteContentFileRef> imagesRemote = new()
            {
                RemoteImageContentFileRef
            };
            List<EpubLocalByteContentFileRef> fontsLocal = new()
            {
                Font1FileRef,
                Font2FileRef
            };
            List<EpubRemoteByteContentFileRef> fontsRemote = new()
            {
                RemoteFontContentFileRef
            };
            List<EpubLocalByteContentFileRef> audioLocal = new()
            {
                Audio1FileRef,
                Audio2FileRef
            };
            List<EpubRemoteByteContentFileRef> audioRemote = new()
            {
                RemoteAudioContentFileRef
            };
            List<EpubLocalContentFileRef> allFilesLocal = new()
            {
                Chapter1FileRef,
                Chapter2FileRef,
                Styles1FileRef,
                Styles2FileRef,
                Image1FileRef,
                Image2FileRef,
                Font1FileRef,
                Font2FileRef,
                Audio1FileRef,
                Audio2FileRef,
                VideoFileRef,
                NavFileRef,
                CoverFileRef,
                NcxFileRef
            };
            List<EpubRemoteContentFileRef> allFilesRemote = new()
            {
                RemoteHtmlContentFileRef,
                RemoteCssContentFileRef,
                RemoteImageContentFileRef,
                RemoteFontContentFileRef,
                RemoteXmlContentFileRef,
                RemoteAudioContentFileRef,
                RemoteVideoContentFileRef
            };
            return new EpubContentRef
            (
                cover: CoverFileRef,
                navigationHtmlFile: NavFileRef,
                html: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(htmlLocal.AsReadOnly(), htmlRemote.AsReadOnly()),
                css: new EpubContentCollectionRef<EpubLocalTextContentFileRef, EpubRemoteTextContentFileRef>(cssLocal.AsReadOnly(), cssRemote.AsReadOnly()),
                images: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>(imagesLocal.AsReadOnly(), imagesRemote.AsReadOnly()),
                fonts: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>(fontsLocal.AsReadOnly(), fontsRemote.AsReadOnly()),
                audio: new EpubContentCollectionRef<EpubLocalByteContentFileRef, EpubRemoteByteContentFileRef>(audioLocal.AsReadOnly(), audioRemote.AsReadOnly()),
                allFiles: new EpubContentCollectionRef<EpubLocalContentFileRef, EpubRemoteContentFileRef>(allFilesLocal.AsReadOnly(), allFilesRemote.AsReadOnly())
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
