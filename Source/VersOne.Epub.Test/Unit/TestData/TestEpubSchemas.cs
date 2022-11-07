using VersOne.Epub.Schema;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubSchemas
    {
        public static EpubSchema CreateMinimalTestEpubSchema()
        {
            return new()
            {
                Package = new EpubPackage()
                {
                    EpubVersion = EpubVersion.EPUB_3,
                    Metadata = new EpubMetadata()
                    {
                        Titles = new List<string>(),
                        Creators = new List<EpubMetadataCreator>(),
                        Subjects = new List<string>(),
                        Description = null,
                        Publishers = new List<string>(),
                        Contributors = new List<EpubMetadataContributor>(),
                        Dates = new List<EpubMetadataDate>(),
                        Types = new List<string>(),
                        Formats = new List<string>(),
                        Identifiers = new List<EpubMetadataIdentifier>(),
                        Sources = new List<string>(),
                        Languages = new List<string>(),
                        Relations = new List<string>(),
                        Coverages = new List<string>(),
                        Rights = new List<string>(),
                        Links = new List<EpubMetadataLink>(),
                        MetaItems = new List<EpubMetadataMeta>()
                    },
                    Manifest = new EpubManifest()
                    {
                        Items = new List<EpubManifestItem>()
                        {
                            new EpubManifestItem()
                            {
                                Id = "item-toc",
                                Href = NAV_FILE_NAME,
                                MediaType = HTML_CONTENT_MIME_TYPE,
                                Properties = new List<EpubManifestProperty>()
                                {
                                    EpubManifestProperty.NAV
                                }
                            }
                        }
                    },
                    Spine = new EpubSpine()
                    {
                        Items = new List<EpubSpineItemRef>()
                    },
                    Guide = null
                },
                Epub2Ncx = null,
                Epub3NavDocument = new Epub3NavDocument()
                {
                    Navs = new List<Epub3Nav>()
                },
                ContentDirectoryPath = CONTENT_DIRECTORY_PATH
            };
        }

        public static EpubSchema CreateMinimalTestEpub2SchemaWithoutNavigation()
        {
            return new()
            {
                Package = new EpubPackage()
                {
                    EpubVersion = EpubVersion.EPUB_2,
                    Metadata = new EpubMetadata()
                    {
                        Titles = new List<string>(),
                        Creators = new List<EpubMetadataCreator>(),
                        Subjects = new List<string>(),
                        Description = null,
                        Publishers = new List<string>(),
                        Contributors = new List<EpubMetadataContributor>(),
                        Dates = new List<EpubMetadataDate>(),
                        Types = new List<string>(),
                        Formats = new List<string>(),
                        Identifiers = new List<EpubMetadataIdentifier>(),
                        Sources = new List<string>(),
                        Languages = new List<string>(),
                        Relations = new List<string>(),
                        Coverages = new List<string>(),
                        Rights = new List<string>(),
                        Links = new List<EpubMetadataLink>(),
                        MetaItems = new List<EpubMetadataMeta>()
                    },
                    Manifest = new EpubManifest()
                    {
                        Items = new List<EpubManifestItem>()
                    },
                    Spine = new EpubSpine()
                    {
                        Items = new List<EpubSpineItemRef>()
                    },
                    Guide = null
                },
                Epub2Ncx = null,
                Epub3NavDocument = null,
                ContentDirectoryPath = CONTENT_DIRECTORY_PATH
            };
        }

        public static EpubSchema CreateFullTestEpubSchema()
        {
            return new()
            {
                Package = new EpubPackage()
                {
                    EpubVersion = EpubVersion.EPUB_3,
                    Metadata = new EpubMetadata()
                    {
                        Titles = new List<string>()
                        {
                            BOOK_TITLE
                        },
                        Creators = new List<EpubMetadataCreator>()
                        {
                            new EpubMetadataCreator()
                            {
                                Creator = BOOK_AUTHOR
                            }
                        },
                        Subjects = new List<string>(),
                        Description = BOOK_DESCRIPTION,
                        Publishers = new List<string>(),
                        Contributors = new List<EpubMetadataContributor>(),
                        Dates = new List<EpubMetadataDate>(),
                        Types = new List<string>(),
                        Formats = new List<string>(),
                        Identifiers = new List<EpubMetadataIdentifier>(),
                        Sources = new List<string>(),
                        Languages = new List<string>(),
                        Relations = new List<string>(),
                        Coverages = new List<string>(),
                        Rights = new List<string>(),
                        Links = new List<EpubMetadataLink>(),
                        MetaItems = new List<EpubMetadataMeta>()
                    },
                    Manifest = new EpubManifest()
                    {
                        Items = new List<EpubManifestItem>()
                        {
                            new EpubManifestItem()
                            {
                                Id = "item-1",
                                Href = CHAPTER1_FILE_NAME,
                                MediaType = HTML_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-2",
                                Href = CHAPTER2_FILE_NAME,
                                MediaType = HTML_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-3",
                                Href = STYLES1_FILE_NAME,
                                MediaType = CSS_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-4",
                                Href = STYLES2_FILE_NAME,
                                MediaType = CSS_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-5",
                                Href = IMAGE1_FILE_NAME,
                                MediaType = IMAGE_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-6",
                                Href = IMAGE2_FILE_NAME,
                                MediaType = IMAGE_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-7",
                                Href = FONT1_FILE_NAME,
                                MediaType = FONT_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-8",
                                Href = FONT2_FILE_NAME,
                                MediaType = FONT_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-9",
                                Href = AUDIO_FILE_NAME,
                                MediaType = AUDIO_MPEG_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-10",
                                Href = REMOTE_HTML_CONTENT_FILE_HREF,
                                MediaType = HTML_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-11",
                                Href = REMOTE_CSS_CONTENT_FILE_HREF,
                                MediaType = CSS_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-12",
                                Href = REMOTE_IMAGE_CONTENT_FILE_HREF,
                                MediaType = IMAGE_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-13",
                                Href = REMOTE_FONT_CONTENT_FILE_HREF,
                                MediaType = FONT_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-14",
                                Href = REMOTE_XML_CONTENT_FILE_HREF,
                                MediaType = XML_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-15",
                                Href = REMOTE_AUDIO_CONTENT_FILE_HREF,
                                MediaType = AUDIO_MPEG_CONTENT_MIME_TYPE
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-toc",
                                Href = NAV_FILE_NAME,
                                MediaType = HTML_CONTENT_MIME_TYPE,
                                Properties = new List<EpubManifestProperty>()
                                {
                                    EpubManifestProperty.NAV
                                }
                            },
                            new EpubManifestItem()
                            {
                                Id = "item-cover",
                                Href = COVER_FILE_NAME,
                                MediaType = IMAGE_CONTENT_MIME_TYPE,
                                Properties = new List<EpubManifestProperty>()
                                {
                                    EpubManifestProperty.COVER_IMAGE
                                }
                            },
                            new EpubManifestItem()
                            {
                                Id = "ncx",
                                Href = NCX_FILE_NAME,
                                MediaType = NCX_CONTENT_MIME_TYPE
                            }
                        }
                    },
                    Spine = new EpubSpine()
                    {
                        Toc = "ncx",
                        Items = new List<EpubSpineItemRef>()
                        {
                            new EpubSpineItemRef()
                            {
                                Id = "itemref-1",
                                IdRef = "item-1",
                                IsLinear = true
                            },
                            new EpubSpineItemRef()
                            {
                                Id = "itemref-2",
                                IdRef = "item-2",
                                IsLinear = true
                            }
                        }
                    },
                    Guide = null
                },
                Epub2Ncx = new Epub2Ncx()
                {
                    Head = new Epub2NcxHead()
                    {
                        Items = new List<Epub2NcxHeadMeta>()
                        {
                            new Epub2NcxHeadMeta()
                            {
                                Name = "dtb:uid",
                                Content = BOOK_UID
                            }
                        }
                    },
                    DocTitle = BOOK_TITLE,
                    DocAuthors = new List<string>()
                    {
                        BOOK_AUTHOR
                    },
                    NavMap = new Epub2NcxNavigationMap()
                    {
                        Items = new List<Epub2NcxNavigationPoint>()
                        {
                            new Epub2NcxNavigationPoint()
                            {
                                Id = "navpoint-1",
                                NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Chapter 1"
                                    }
                                },
                                Content = new Epub2NcxContent()
                                {
                                    Source = CHAPTER1_FILE_NAME
                                },
                                ChildNavigationPoints = new List<Epub2NcxNavigationPoint>()
                            },
                            new Epub2NcxNavigationPoint()
                            {
                                Id = "navpoint-2",
                                NavigationLabels = new List<Epub2NcxNavigationLabel>()
                                {
                                    new Epub2NcxNavigationLabel()
                                    {
                                        Text = "Chapter 2"
                                    }
                                },
                                Content = new Epub2NcxContent()
                                {
                                    Source = CHAPTER2_FILE_NAME
                                },
                                ChildNavigationPoints = new List<Epub2NcxNavigationPoint>()
                            }
                        }
                    },
                    NavLists = new List<Epub2NcxNavigationList>()
                },
                Epub3NavDocument = new Epub3NavDocument()
                {
                    Navs = new List<Epub3Nav>()
                    {
                        new Epub3Nav()
                        {
                            Type = Epub3NavStructuralSemanticsProperty.TOC,
                            Ol = new Epub3NavOl()
                            {
                                Lis = new List<Epub3NavLi>()
                                {
                                    new Epub3NavLi()
                                    {
                                        Anchor = new Epub3NavAnchor()
                                        {
                                            Href = CHAPTER1_FILE_NAME,
                                            Text = "Chapter 1"
                                        }
                                    },
                                    new Epub3NavLi()
                                    {
                                        Anchor = new Epub3NavAnchor()
                                        {
                                            Href = CHAPTER2_FILE_NAME,
                                            Text = "Chapter 2"
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                ContentDirectoryPath = CONTENT_DIRECTORY_PATH
            };
        }
    }
}
