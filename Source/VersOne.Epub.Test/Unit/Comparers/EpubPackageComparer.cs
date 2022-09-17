using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.TestUtils;

namespace VersOne.Epub.Test.Unit.Comparers
{
    internal static class EpubPackageComparer
    {
        public static void CompareEpubPackages(EpubPackage expected, EpubPackage actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.EpubVersion, actual.EpubVersion);
            CompareEpubMetadatas(expected.Metadata, actual.Metadata);
            CompareEpubManifests(expected.Manifest, actual.Manifest);
            CompareEpubSpines(expected.Spine, actual.Spine);
            CompareEpubGuides(expected.Guide, actual.Guide);
        }

        private static void CompareEpubMetadatas(EpubMetadata expected, EpubMetadata actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Titles, actual.Titles);
            AssertUtils.CollectionsEqual(expected.Creators, actual.Creators, CompareEpubMetadataCreators);
            Assert.Equal(expected.Subjects, actual.Subjects);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Publishers, actual.Publishers);
            AssertUtils.CollectionsEqual(expected.Contributors, actual.Contributors, CompareEpubMetadataContributors);
            AssertUtils.CollectionsEqual(expected.Dates, actual.Dates, CompareEpubMetadataDates);
            Assert.Equal(expected.Types, actual.Types);
            Assert.Equal(expected.Formats, actual.Formats);
            AssertUtils.CollectionsEqual(expected.Identifiers, actual.Identifiers, CompareEpubMetadataIdentifiers);
            Assert.Equal(expected.Sources, actual.Sources);
            Assert.Equal(expected.Languages, actual.Languages);
            Assert.Equal(expected.Relations, actual.Relations);
            Assert.Equal(expected.Coverages, actual.Coverages);
            Assert.Equal(expected.Rights, actual.Rights);
            AssertUtils.CollectionsEqual(expected.Links, actual.Links, CompareEpubMetadataLinks);
            AssertUtils.CollectionsEqual(expected.MetaItems, actual.MetaItems, CompareEpubMetadataMetas);
        }

        private static void CompareEpubMetadataCreators(EpubMetadataCreator expected, EpubMetadataCreator actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Creator, actual.Creator);
            Assert.Equal(expected.FileAs, actual.FileAs);
            Assert.Equal(expected.Role, actual.Role);
        }

        private static void CompareEpubMetadataContributors(EpubMetadataContributor expected, EpubMetadataContributor actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Contributor, actual.Contributor);
            Assert.Equal(expected.FileAs, actual.FileAs);
            Assert.Equal(expected.Role, actual.Role);
        }

        private static void CompareEpubMetadataDates(EpubMetadataDate expected, EpubMetadataDate actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Date, actual.Date);
            Assert.Equal(expected.Event, actual.Event);
        }

        private static void CompareEpubMetadataIdentifiers(EpubMetadataIdentifier expected, EpubMetadataIdentifier actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Scheme, actual.Scheme);
            Assert.Equal(expected.Identifier, actual.Identifier);
        }

        private static void CompareEpubMetadataLinks(EpubMetadataLink expected, EpubMetadataLink actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Href, actual.Href);
            Assert.Equal(expected.MediaType, actual.MediaType);
            Assert.Equal(expected.Properties, actual.Properties);
            Assert.Equal(expected.Refines, actual.Refines);
            Assert.Equal(expected.Relationships, actual.Relationships);
        }

        private static void CompareEpubMetadataMetas(EpubMetadataMeta expected, EpubMetadataMeta actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Content, actual.Content);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Refines, actual.Refines);
            Assert.Equal(expected.Property, actual.Property);
            Assert.Equal(expected.Scheme, actual.Scheme);
        }

        private static void CompareEpubManifests(EpubManifest expected, EpubManifest actual)
        {
            Assert.NotNull(actual);
            AssertUtils.CollectionsEqual(expected.Items, actual.Items, CompareEpubManifestItems);
        }

        private static void CompareEpubManifestItems(EpubManifestItem expected, EpubManifestItem actual)
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

        private static void CompareEpubSpines(EpubSpine expected, EpubSpine actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.PageProgressionDirection, actual.PageProgressionDirection);
            Assert.Equal(expected.Toc, actual.Toc);
            AssertUtils.CollectionsEqual(expected.Items, actual.Items, CompareEpubSpineItemRefs);
        }

        private static void CompareEpubSpineItemRefs(EpubSpineItemRef expected, EpubSpineItemRef actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.IdRef, actual.IdRef);
            Assert.Equal(expected.IsLinear, actual.IsLinear);
            Assert.Equal(expected.Properties, actual.Properties);
        }

        private static void CompareEpubGuides(EpubGuide expected, EpubGuide actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                AssertUtils.CollectionsEqual(expected.Items, actual.Items, CompareEpubGuideReferences);
            }
        }

        private static void CompareEpubGuideReferences(EpubGuideReference expected, EpubGuideReference actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Href, actual.Href);
        }
    }
}
