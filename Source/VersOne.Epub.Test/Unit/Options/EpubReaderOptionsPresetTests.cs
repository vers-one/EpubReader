using VersOne.Epub.Options;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Options
{
    public class EpubReaderOptionsPresetTests
    {
        private static EpubReaderOptions EpubReaderOptionsForStrictPreset =>
            new()
            {
                PackageReaderOptions = new()
                {
                    IgnoreMissingPackageFile = false,
                    IgnorePackageFileIsNotValidXmlError = false,
                    IgnoreMissingPackageNode = false,
                    FallbackEpubVersion = null,
                    IgnoreMissingMetadataNode = false,
                    IgnoreMissingManifestNode = false,
                    IgnoreMissingSpineNode = false,
                    IgnoreMissingToc = false,
                    SkipInvalidManifestItems = false,
                    SkipDuplicateManifestItemIds = false,
                    SkipDuplicateManifestHrefs = false,
                    SkipInvalidSpineItems = false,
                    SkipInvalidGuideReferences = false,
                    SkipInvalidCollections = false
                },
                BookCoverReaderOptions = new()
                {
                    Epub2MetadataIgnoreMissingContent = false,
                    Epub2MetadataIgnoreMissingManifestItem = false,
                    Epub2MetadataIgnoreMissingContentFile = false,
                    Epub3IgnoreMissingContentFile = false,
                    IgnoreRemoteContentFileError = false
                },
                ContainerFileReaderOptions = new()
                {
                    IgnoreMissingContainerFile = false,
                    IgnoreContainerFileIsNotValidXmlError = false,
                    IgnoreMissingPackageFilePathError = false
                },
                ContentDownloaderOptions = new()
                {
                    DownloadContent = false,
                    DownloaderUserAgent = null,
                    CustomContentDownloader = null
                },
                ContentReaderOptions = new()
                {
                    IgnoreMissingFileError = false,
                    IgnoreFileIsTooLargeError = false,
                    IgnoreRemoteEpub3NavigationFileError = false,
                    SkipItemsWithDuplicateHrefs = false,
                    SkipItemsWithDuplicateFilePaths = false,
                    SkipItemsWithDuplicateUrls = false
                },
                Epub2NcxReaderOptions = new()
                {
                    IgnoreMissingTocManifestItemError = false,
                    IgnoreMissingTocFileError = false,
                    IgnoreTocFileIsTooLargeError = false,
                    IgnoreTocFileIsNotValidXmlError = false,
                    IgnoreMissingNcxElementError = false,
                    IgnoreMissingHeadElementError = false,
                    IgnoreMissingDocTitleElementError = false,
                    IgnoreMissingNavMapElementError = false,
                    SkipInvalidMetaElements = false,
                    SkipNavigationPointsWithMissingIds = false,
                    AllowNavigationPointsWithoutLabels = false,
                    IgnoreMissingContentForNavigationPoints = false,
                    SkipInvalidNavigationLabels = false,
                    SkipInvalidNavigationContent = false,
                    ReplaceMissingPageTargetTypesWithUnknown = false,
                    AllowNavigationPageTargetsWithoutLabels = false,
                    AllowNavigationListsWithoutLabels = false,
                    SkipInvalidNavigationTargets = false,
                    AllowNavigationTargetsWithoutLabels = false
                },
                Epub3NavDocumentReaderOptions = new()
                {
                    IgnoreMissingNavManifestItemError = false,
                    IgnoreMissingNavFileError = false,
                    IgnoreNavFileIsTooLargeError = false,
                    IgnoreNavFileIsNotValidXmlError = false,
                    IgnoreMissingHtmlElementError = false,
                    IgnoreMissingBodyElementError = false,
                    SkipNavsWithMissingOlElements = false,
                    SkipInvalidLiElements = false
                },
                MetadataReaderOptions = new()
                {
                    SkipLinksWithoutHrefs = false,
                    IgnoreLinkWithoutRelError = false
                },
                NavigationReaderOptions = new()
                {
                    AllowEpub2NavigationItemsWithEmptyTitles = false,
                    SkipRemoteNavigationItems = false,
                    SkipNavigationItemsReferencingMissingContent = false
                },
                SmilReaderOptions = new()
                {
                    IgnoreMissingSmilFileError = false,
                    IgnoreSmilFileIsTooLargeError = false,
                    IgnoreSmilFileIsNotValidXmlError = false,
                    IgnoreMissingSmilElementError = false,
                    IgnoreMissingSmilVersionError = false,
                    IgnoreUnsupportedSmilVersionError = false,
                    IgnoreMissingBodyElementError = false,
                    IgnoreBodyMissingSeqOrParElementsError = false,
                    IgnoreSeqMissingSeqOrParElementsError = false,
                    SkipParsWithoutTextElements = false,
                    SkipTextsWithoutSrcAttributes = false,
                    SkipAudiosWithoutSrcAttributes = false
                },
                SpineReaderOptions = new()
                {
                    IgnoreMissingManifestItems = false,
                    SkipSpineItemsReferencingRemoteContent = false,
                    IgnoreMissingContentFiles = false
                },
                XmlReaderOptions = new()
                {
                    SkipXmlHeaders = false
                }
            };

        private static EpubReaderOptions EpubReaderOptionsForRelaxedPreset =>
            new()
            {
                PackageReaderOptions = new()
                {
                    IgnoreMissingPackageFile = false,
                    IgnorePackageFileIsNotValidXmlError = false,
                    IgnoreMissingPackageNode = false,
                    FallbackEpubVersion = EpubVersion.EPUB_2,
                    IgnoreMissingMetadataNode = false,
                    IgnoreMissingManifestNode = false,
                    IgnoreMissingSpineNode = false,
                    IgnoreMissingToc = true,
                    SkipInvalidManifestItems = false,
                    SkipDuplicateManifestItemIds = false,
                    SkipDuplicateManifestHrefs = false,
                    SkipInvalidSpineItems = false,
                    SkipInvalidGuideReferences = false,
                    SkipInvalidCollections = false
                },
                BookCoverReaderOptions = new()
                {
                    Epub2MetadataIgnoreMissingContent = false,
                    Epub2MetadataIgnoreMissingManifestItem = true,
                    Epub2MetadataIgnoreMissingContentFile = false,
                    Epub3IgnoreMissingContentFile = false,
                    IgnoreRemoteContentFileError = false
                },
                ContainerFileReaderOptions = new()
                {
                    IgnoreMissingContainerFile = false,
                    IgnoreContainerFileIsNotValidXmlError = false,
                    IgnoreMissingPackageFilePathError = false
                },
                ContentDownloaderOptions = new()
                {
                    DownloadContent = false,
                    DownloaderUserAgent = null,
                    CustomContentDownloader = null
                },
                ContentReaderOptions = new()
                {
                    IgnoreMissingFileError = false,
                    IgnoreFileIsTooLargeError = false,
                    IgnoreRemoteEpub3NavigationFileError = false,
                    SkipItemsWithDuplicateHrefs = false,
                    SkipItemsWithDuplicateFilePaths = false,
                    SkipItemsWithDuplicateUrls = false
                },
                Epub2NcxReaderOptions = new()
                {
                    IgnoreMissingTocManifestItemError = false,
                    IgnoreMissingTocFileError = false,
                    IgnoreTocFileIsTooLargeError = false,
                    IgnoreTocFileIsNotValidXmlError = false,
                    IgnoreMissingNcxElementError = false,
                    IgnoreMissingHeadElementError = false,
                    IgnoreMissingDocTitleElementError = false,
                    IgnoreMissingNavMapElementError = false,
                    SkipInvalidMetaElements = false,
                    SkipNavigationPointsWithMissingIds = true,
                    AllowNavigationPointsWithoutLabels = false,
                    IgnoreMissingContentForNavigationPoints = false,
                    SkipInvalidNavigationLabels = false,
                    SkipInvalidNavigationContent = false,
                    ReplaceMissingPageTargetTypesWithUnknown = false,
                    AllowNavigationPageTargetsWithoutLabels = false,
                    AllowNavigationListsWithoutLabels = false,
                    SkipInvalidNavigationTargets = false,
                    AllowNavigationTargetsWithoutLabels = false
                },
                Epub3NavDocumentReaderOptions = new()
                {
                    IgnoreMissingNavManifestItemError = false,
                    IgnoreMissingNavFileError = false,
                    IgnoreNavFileIsTooLargeError = false,
                    IgnoreNavFileIsNotValidXmlError = false,
                    IgnoreMissingHtmlElementError = false,
                    IgnoreMissingBodyElementError = false,
                    SkipNavsWithMissingOlElements = false,
                    SkipInvalidLiElements = false
                },
                MetadataReaderOptions = new()
                {
                    SkipLinksWithoutHrefs = false,
                    IgnoreLinkWithoutRelError = false
                },
                NavigationReaderOptions = new()
                {
                    AllowEpub2NavigationItemsWithEmptyTitles = false,
                    SkipRemoteNavigationItems = false,
                    SkipNavigationItemsReferencingMissingContent = true
                },
                SmilReaderOptions = new()
                {
                    IgnoreMissingSmilFileError = false,
                    IgnoreSmilFileIsTooLargeError = false,
                    IgnoreSmilFileIsNotValidXmlError = false,
                    IgnoreMissingSmilElementError = false,
                    IgnoreMissingSmilVersionError = false,
                    IgnoreUnsupportedSmilVersionError = false,
                    IgnoreMissingBodyElementError = false,
                    IgnoreBodyMissingSeqOrParElementsError = false,
                    IgnoreSeqMissingSeqOrParElementsError = false,
                    SkipParsWithoutTextElements = false,
                    SkipTextsWithoutSrcAttributes = false,
                    SkipAudiosWithoutSrcAttributes = false
                },
                SpineReaderOptions = new()
                {
                    IgnoreMissingManifestItems = false,
                    SkipSpineItemsReferencingRemoteContent = false,
                    IgnoreMissingContentFiles = false
                },
                XmlReaderOptions = new()
                {
                    SkipXmlHeaders = false
                }
            };

        private static EpubReaderOptions EpubReaderOptionsForIgnoreAllErrorsPreset =>
            new()
            {
                PackageReaderOptions = new()
                {
                    IgnoreMissingPackageFile = true,
                    IgnorePackageFileIsNotValidXmlError = true,
                    IgnoreMissingPackageNode = true,
                    FallbackEpubVersion = EpubVersion.EPUB_2,
                    IgnoreMissingMetadataNode = true,
                    IgnoreMissingManifestNode = true,
                    IgnoreMissingSpineNode = true,
                    IgnoreMissingToc = true,
                    SkipInvalidManifestItems = true,
                    SkipDuplicateManifestItemIds = true,
                    SkipDuplicateManifestHrefs = true,
                    SkipInvalidSpineItems = true,
                    SkipInvalidGuideReferences = true,
                    SkipInvalidCollections = true
                },
                BookCoverReaderOptions = new()
                {
                    Epub2MetadataIgnoreMissingContent = true,
                    Epub2MetadataIgnoreMissingManifestItem = true,
                    Epub2MetadataIgnoreMissingContentFile = true,
                    Epub3IgnoreMissingContentFile = true,
                    IgnoreRemoteContentFileError = true
                },
                ContainerFileReaderOptions = new()
                {
                    IgnoreMissingContainerFile = true,
                    IgnoreContainerFileIsNotValidXmlError = true,
                    IgnoreMissingPackageFilePathError = true
                },
                ContentDownloaderOptions = new()
                {
                    DownloadContent = false,
                    DownloaderUserAgent = null,
                    CustomContentDownloader = null
                },
                ContentReaderOptions = new()
                {
                    IgnoreMissingFileError = true,
                    IgnoreFileIsTooLargeError = true,
                    IgnoreRemoteEpub3NavigationFileError = true,
                    SkipItemsWithDuplicateHrefs = true,
                    SkipItemsWithDuplicateFilePaths = true,
                    SkipItemsWithDuplicateUrls = true
                },
                Epub2NcxReaderOptions = new()
                {
                    IgnoreMissingTocManifestItemError = true,
                    IgnoreMissingTocFileError = true,
                    IgnoreTocFileIsTooLargeError = true,
                    IgnoreTocFileIsNotValidXmlError = true,
                    IgnoreMissingNcxElementError = true,
                    IgnoreMissingHeadElementError = true,
                    IgnoreMissingDocTitleElementError = true,
                    IgnoreMissingNavMapElementError = true,
                    SkipInvalidMetaElements = true,
                    SkipNavigationPointsWithMissingIds = true,
                    AllowNavigationPointsWithoutLabels = true,
                    IgnoreMissingContentForNavigationPoints = true,
                    SkipInvalidNavigationLabels = true,
                    SkipInvalidNavigationContent = true,
                    ReplaceMissingPageTargetTypesWithUnknown = true,
                    AllowNavigationPageTargetsWithoutLabels = true,
                    AllowNavigationListsWithoutLabels = true,
                    SkipInvalidNavigationTargets = true,
                    AllowNavigationTargetsWithoutLabels = true
                },
                Epub3NavDocumentReaderOptions = new()
                {
                    IgnoreMissingNavManifestItemError = true,
                    IgnoreMissingNavFileError = true,
                    IgnoreNavFileIsTooLargeError = true,
                    IgnoreNavFileIsNotValidXmlError = true,
                    IgnoreMissingHtmlElementError = true,
                    IgnoreMissingBodyElementError = true,
                    SkipNavsWithMissingOlElements = true,
                    SkipInvalidLiElements = true
                },
                MetadataReaderOptions = new()
                {
                    SkipLinksWithoutHrefs = true,
                    IgnoreLinkWithoutRelError = true
                },
                NavigationReaderOptions = new()
                {
                    AllowEpub2NavigationItemsWithEmptyTitles = true,
                    SkipRemoteNavigationItems = true,
                    SkipNavigationItemsReferencingMissingContent = true
                },
                SmilReaderOptions = new()
                {
                    IgnoreMissingSmilFileError = true,
                    IgnoreSmilFileIsTooLargeError = true,
                    IgnoreSmilFileIsNotValidXmlError = true,
                    IgnoreMissingSmilElementError = true,
                    IgnoreMissingSmilVersionError = true,
                    IgnoreUnsupportedSmilVersionError = true,
                    IgnoreMissingBodyElementError = true,
                    IgnoreBodyMissingSeqOrParElementsError = true,
                    IgnoreSeqMissingSeqOrParElementsError = true,
                    SkipParsWithoutTextElements = true,
                    SkipTextsWithoutSrcAttributes = true,
                    SkipAudiosWithoutSrcAttributes = true
                },
                SpineReaderOptions = new()
                {
                    IgnoreMissingManifestItems = true,
                    SkipSpineItemsReferencingRemoteContent = true,
                    IgnoreMissingContentFiles = true
                },
                XmlReaderOptions = new()
                {
                    SkipXmlHeaders = true
                }
            };

        [Fact(DisplayName = "Constructing a EpubReaderOptions instance with a null preset parameter should be equivalent to creating it with the EpubReaderOptionsPreset.STRICT")]
        public void ConstructorWithNullPresetTest()
        {
            EpubReaderOptions actual = new(null);
            Assert.Equivalent(EpubReaderOptionsForStrictPreset, actual, strict: true);
        }

        [Fact(DisplayName = "Constructing a EpubReaderOptions instance with the EpubReaderOptionsPreset.STRICT parameter should set all options to their default values")]
        public void ConstructorWithStrictPresetTest()
        {
            EpubReaderOptions actual = new(EpubReaderOptionsPreset.STRICT);
            Assert.Equivalent(EpubReaderOptionsForStrictPreset, actual, strict: true);
        }

        [Fact(DisplayName = "Constructing a EpubReaderOptions instance with the EpubReaderOptionsPreset.RELAXED parameter should disable validation checks commonly failed by EPUB books")]
        public void ConstructorWithRelaxedPresetTest()
        {
            EpubReaderOptions actual = new(EpubReaderOptionsPreset.RELAXED);
            Assert.Equivalent(EpubReaderOptionsForRelaxedPreset, actual, strict: true);
        }

        [Fact(DisplayName = "Constructing a EpubReaderOptions instance with the EpubReaderOptionsPreset.IGNORE_ALL_ERRORS parameter should disable all validation checks")]
        public void ConstructorWithIgnoreAllErrorsPresetTest()
        {
            EpubReaderOptions actual = new(EpubReaderOptionsPreset.IGNORE_ALL_ERRORS);
            Assert.Equivalent(EpubReaderOptionsForIgnoreAllErrorsPreset, actual, strict: true);
        }
    }
}
