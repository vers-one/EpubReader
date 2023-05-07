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
                        id: "item-1",
                        href: CHAPTER1_FILE_NAME,
                        mediaType: HTML_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-2",
                        href: CHAPTER2_FILE_NAME,
                        mediaType: HTML_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-3",
                        href: STYLES1_FILE_NAME,
                        mediaType: CSS_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-4",
                        href: STYLES2_FILE_NAME,
                        mediaType: CSS_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-5",
                        href: IMAGE1_FILE_NAME,
                        mediaType: IMAGE_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-6",
                        href: IMAGE2_FILE_NAME,
                        mediaType: IMAGE_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-7",
                        href: FONT1_FILE_NAME,
                        mediaType: FONT_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-8",
                        href: FONT2_FILE_NAME,
                        mediaType: FONT_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-9",
                        href: AUDIO_FILE_NAME,
                        mediaType: AUDIO_MPEG_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-10",
                        href: REMOTE_HTML_CONTENT_FILE_HREF,
                        mediaType: HTML_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-11",
                        href: REMOTE_CSS_CONTENT_FILE_HREF,
                        mediaType: CSS_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-12",
                        href: REMOTE_IMAGE_CONTENT_FILE_HREF,
                        mediaType: IMAGE_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-13",
                        href: REMOTE_FONT_CONTENT_FILE_HREF,
                        mediaType: FONT_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-14",
                        href: REMOTE_XML_CONTENT_FILE_HREF,
                        mediaType: XML_CONTENT_MIME_TYPE
                    ),
                    new EpubManifestItem
                    (
                        id: "item-15",
                        href: REMOTE_AUDIO_CONTENT_FILE_HREF,
                        mediaType: AUDIO_MPEG_CONTENT_MIME_TYPE
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
