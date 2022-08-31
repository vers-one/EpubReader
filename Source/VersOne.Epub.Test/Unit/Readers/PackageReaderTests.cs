using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.Mocks;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class PackageReaderTests
    {
        private const string CONTAINER_FILE_PATH = "META-INF/container.xml";
        private const string OPF_FILE_PATH = "content.opf";

        private const string CONTAINER_FILE = $"""
            <?xml version='1.0' encoding='utf-8'?>
            <container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
              <rootfiles>
                <rootfile media-type="application/oebps-package+xml" full-path="{OPF_FILE_PATH}"/>
              </rootfiles>
            </container>
            """;

        private const string MINIMAL_EPUB2_OPF_FILE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="2.0">
              <metadata />
              <manifest />
              <spine toc="ncx" />
            </package>
            """;

        private const string MINIMAL_EPUB3_OPF_FILE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.0">
              <metadata />
              <manifest />
              <spine />
            </package>
            """;

        private const string MINIMAL_EPUB3_1_OPF_FILE = $"""
            <?xml version='1.0' encoding='UTF-8'?>
            <package xmlns="http://www.idpf.org/2007/opf" version="3.1">
              <metadata />
              <manifest />
              <spine />
            </package>
            """;

        private static EpubMetadata EmptyMetadata =>
            new()
            {
                Titles = new List<string>(),
                Creators = new List<EpubMetadataCreator>(),
                Subjects = new List<string>(),
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
            };

        private static EpubPackage MinimalEpub2Package =>
            new()
            {
                EpubVersion = EpubVersion.EPUB_2,
                Metadata = EmptyMetadata,
                Manifest = new EpubManifest(),
                Spine = new EpubSpine()
                {
                    Toc = "ncx"
                }
            };

        private static EpubPackage MinimalEpub3Package =>
            new()
            {
                EpubVersion = EpubVersion.EPUB_3,
                Metadata = EmptyMetadata,
                Manifest = new EpubManifest(),
                Spine = new EpubSpine()
            };

        private static EpubPackage MinimalEpub31Package =>
            new()
            {
                EpubVersion = EpubVersion.EPUB_3_1,
                Metadata = EmptyMetadata,
                Manifest = new EpubManifest(),
                Spine = new EpubSpine()
            };

        public static IEnumerable<object[]> ReadMinimalPackageAsyncTestData
        {
            get
            {
                yield return new object[] { MINIMAL_EPUB2_OPF_FILE, MinimalEpub2Package };
                yield return new object[] { MINIMAL_EPUB3_OPF_FILE, MinimalEpub3Package };
                yield return new object[] { MINIMAL_EPUB3_1_OPF_FILE, MinimalEpub31Package };
            }
        }

        [Theory(DisplayName = "Reading a minimal OPF package should succeed")]
        [MemberData(nameof(ReadMinimalPackageAsyncTestData))]
        public async void ReadMinimalPackageAsyncTest(string opfFileContent, EpubPackage expectedEpubPackage)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(CONTAINER_FILE_PATH, CONTAINER_FILE);
            testZipFile.AddEntry(OPF_FILE_PATH, opfFileContent);
            EpubPackage actualEpubPackage = await PackageReader.ReadPackageAsync(testZipFile, OPF_FILE_PATH, new EpubReaderOptions());
            CompareEpubPackages(expectedEpubPackage, actualEpubPackage);
        }

        private void CompareEpubPackages(EpubPackage expected, EpubPackage actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.EpubVersion, actual.EpubVersion);
            CompareEpubMetadatas(expected.Metadata, actual.Metadata);
            CompareEpubManifests(expected.Manifest, actual.Manifest);
            CompareEpubSpines(expected.Spine, actual.Spine);
            CompareEpubGuides(expected.Guide, actual.Guide);
        }

        private void CompareEpubMetadatas(EpubMetadata expected, EpubMetadata actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Titles, actual.Titles);
            Assert.Equal(expected.Creators.Count, actual.Creators.Count);
            for (int i = 0; i < expected.Creators.Count; i++)
            {
                CompareEpubMetadataCreators(expected.Creators[i], actual.Creators[i]);
            }
            Assert.Equal(expected.Subjects, actual.Subjects);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Publishers, actual.Publishers);
            Assert.Equal(expected.Contributors.Count, actual.Contributors.Count);
            for (int i = 0; i < expected.Contributors.Count; i++)
            {
                CompareEpubMetadataContributors(expected.Contributors[i], actual.Contributors[i]);
            }
            Assert.Equal(expected.Dates.Count, actual.Dates.Count);
            for (int i = 0; i < expected.Dates.Count; i++)
            {
                CompareEpubMetadataDates(expected.Dates[i], actual.Dates[i]);
            }
            Assert.Equal(expected.Types, actual.Types);
            Assert.Equal(expected.Formats, actual.Formats);
            Assert.Equal(expected.Identifiers.Count, actual.Identifiers.Count);
            for (int i = 0; i < expected.Identifiers.Count; i++)
            {
                CompareEpubMetadataIdentifiers(expected.Identifiers[i], actual.Identifiers[i]);
            }
            Assert.Equal(expected.Sources, actual.Sources);
            Assert.Equal(expected.Languages, actual.Languages);
            Assert.Equal(expected.Relations, actual.Relations);
            Assert.Equal(expected.Coverages, actual.Coverages);
            Assert.Equal(expected.Rights, actual.Rights);
            Assert.Equal(expected.Links.Count, actual.Links.Count);
            for (int i = 0; i < expected.Links.Count; i++)
            {
                CompareEpubMetadataLinks(expected.Links[i], actual.Links[i]);
            }
            Assert.Equal(expected.MetaItems.Count, actual.MetaItems.Count);
            for (int i = 0; i < expected.Links.Count; i++)
            {
                CompareEpubMetadataMetas(expected.MetaItems[i], actual.MetaItems[i]);
            }
        }

        private void CompareEpubMetadataCreators(EpubMetadataCreator expected, EpubMetadataCreator actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Creator, actual.Creator);
            Assert.Equal(expected.FileAs, actual.FileAs);
            Assert.Equal(expected.Role, actual.Role);
        }

        private void CompareEpubMetadataContributors(EpubMetadataContributor expected, EpubMetadataContributor actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Contributor, actual.Contributor);
            Assert.Equal(expected.FileAs, actual.FileAs);
            Assert.Equal(expected.Role, actual.Role);
        }

        private void CompareEpubMetadataDates(EpubMetadataDate expected, EpubMetadataDate actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Date, actual.Date);
            Assert.Equal(expected.Event, actual.Event);
        }

        private void CompareEpubMetadataIdentifiers(EpubMetadataIdentifier expected, EpubMetadataIdentifier actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Scheme, actual.Scheme);
            Assert.Equal(expected.Identifier, actual.Identifier);
        }

        private void CompareEpubMetadataLinks(EpubMetadataLink expected, EpubMetadataLink actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Href, actual.Href);
            Assert.Equal(expected.MediaType, actual.MediaType);
            Assert.Equal(expected.Properties, actual.Properties);
            Assert.Equal(expected.Refines, actual.Refines);
            Assert.Equal(expected.Relationships, actual.Relationships);
        }

        private void CompareEpubMetadataMetas(EpubMetadataMeta expected, EpubMetadataMeta actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Content, actual.Content);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Refines, actual.Refines);
            Assert.Equal(expected.Property, actual.Property);
            Assert.Equal(expected.Scheme, actual.Scheme);
        }

        private void CompareEpubManifests(EpubManifest expected, EpubManifest actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                CompareEpubManifestItems(expected[i], actual[i]);
            }
        }

        private void CompareEpubManifestItems(EpubManifestItem expected, EpubManifestItem actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Href, actual.Href);
            Assert.Equal(expected.MediaType, actual.MediaType);
            Assert.Equal(expected.MediaOverlay, actual.MediaOverlay);
            Assert.Equal(expected.RequiredNamespace, actual.RequiredNamespace);
            Assert.Equal(expected.RequiredModules, actual.RequiredModules);
            Assert.Equal(expected.Fallback, actual.Fallback);
            Assert.Equal(expected.FallbackStyle, actual.FallbackStyle);
            Assert.Equal(expected.Properties, actual.Properties);
        }

        private void CompareEpubSpines(EpubSpine expected, EpubSpine actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.PageProgressionDirection, actual.PageProgressionDirection);
            Assert.Equal(expected.Toc, actual.Toc);
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                CompareEpubSpineItemRefs(expected[i], actual[i]);
            }
        }

        private void CompareEpubSpineItemRefs(EpubSpineItemRef expected, EpubSpineItemRef actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.IdRef, actual.IdRef);
            Assert.Equal(expected.IsLinear, actual.IsLinear);
            Assert.Equal(expected.Properties, actual.Properties);
        }

        private void CompareEpubGuides(EpubGuide expected, EpubGuide actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    CompareEpubGuideReferences(expected[i], actual[i]);
                }
            }
        }

        private void CompareEpubGuideReferences(EpubGuideReference expected, EpubGuideReference actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Href, actual.Href);
        }
    }
}
