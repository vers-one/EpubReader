using VersOne.Epub.Schema;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubManifests
    {
        public static EpubManifest CreateMinimalTestEpubManifest()
        {
            return new
            (
                items: new List<EpubManifestItem>()
                {
                    new EpubManifestItem
                    (
                        id: "item-toc",
                        href: NAV_FILE_NAME,
                        mediaType: HTML_CONTENT_MIME_TYPE,
                        properties: new List<EpubManifestProperty>()
                        {
                            EpubManifestProperty.NAV
                        }
                    )
                }
            );
        }

        public static EpubManifest CreateFullTestEpubManifest()
        {
            return new
            (
                items: new List<EpubManifestItem>()
                {
                    new EpubManifestItem
                    (
                        id: "item-html-1",
                        href: CHAPTER1_FILE_NAME,
                        mediaType: HTML_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-html-2",
                        href: CHAPTER2_FILE_NAME,
                        mediaType: HTML_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-css-1",
                        href: STYLES1_FILE_NAME,
                        mediaType: CSS_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-css-2",
                        href: STYLES2_FILE_NAME,
                        mediaType: CSS_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-image-1",
                        href: IMAGE1_FILE_NAME,
                        mediaType: IMAGE_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-image-2",
                        href: IMAGE2_FILE_NAME,
                        mediaType: IMAGE_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-font-1",
                        href: FONT1_FILE_NAME,
                        mediaType: FONT_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-font-2",
                        href: FONT2_FILE_NAME,
                        mediaType: FONT_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-audio-1",
                        href: AUDIO1_FILE_NAME,
                        mediaType: AUDIO_MPEG_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-audio-2",
                        href: AUDIO2_FILE_NAME,
                        mediaType: AUDIO_MPEG_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-video",
                        href: VIDEO_FILE_NAME,
                        mediaType: VIDEO_MP4_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-remote-html",
                        href: REMOTE_HTML_CONTENT_FILE_HREF,
                        mediaType: HTML_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-remote-css",
                        href: REMOTE_CSS_CONTENT_FILE_HREF,
                        mediaType: CSS_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-remote-image",
                        href: REMOTE_IMAGE_CONTENT_FILE_HREF,
                        mediaType: IMAGE_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-remote-font",
                        href: REMOTE_FONT_CONTENT_FILE_HREF,
                        mediaType: FONT_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-remote-xml",
                        href: REMOTE_XML_CONTENT_FILE_HREF,
                        mediaType: XML_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-remote-audio",
                        href: REMOTE_AUDIO_CONTENT_FILE_HREF,
                        mediaType: AUDIO_MPEG_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-remote-video",
                        href: REMOTE_VIDEO_CONTENT_FILE_HREF,
                        mediaType: VIDEO_MP4_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-toc",
                        href: NAV_FILE_NAME,
                        mediaType: HTML_CONTENT_MIME_TYPE,
                        properties: new List<EpubManifestProperty>
                        {
                            EpubManifestProperty.NAV
                        }
                    ),
                    new EpubManifestItem
                    (
                        id: "item-cover",
                        href: COVER_FILE_NAME,
                        mediaType: IMAGE_CONTENT_MIME_TYPE,
                        properties: new List<EpubManifestProperty>
                        {
                            EpubManifestProperty.COVER_IMAGE
                        }
                    ),
                    new EpubManifestItem
                    (
                        id: "ncx",
                        href: NCX_FILE_NAME,
                        mediaType: NCX_CONTENT_MIME_TYPE
                    )
                }
            );
        }
    }
}
