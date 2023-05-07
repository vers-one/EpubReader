using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Comparers
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

        public static void CompareEpubMetadatas(EpubMetadata expected, EpubMetadata actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Titles, actual.Titles);
            CollectionComparer.CompareCollections(expected.Creators, actual.Creators, CompareEpubMetadataCreators);
            Assert.Equal(expected.Subjects, actual.Subjects);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Publishers, actual.Publishers);
            CollectionComparer.CompareCollections(expected.Contributors, actual.Contributors, CompareEpubMetadataContributors);
            CollectionComparer.CompareCollections(expected.Dates, actual.Dates, CompareEpubMetadataDates);
            Assert.Equal(expected.Types, actual.Types);
            Assert.Equal(expected.Formats, actual.Formats);
            CollectionComparer.CompareCollections(expected.Identifiers, actual.Identifiers, CompareEpubMetadataIdentifiers);
            Assert.Equal(expected.Sources, actual.Sources);
            Assert.Equal(expected.Languages, actual.Languages);
            Assert.Equal(expected.Relations, actual.Relations);
            Assert.Equal(expected.Coverages, actual.Coverages);
            Assert.Equal(expected.Rights, actual.Rights);
            CollectionComparer.CompareCollections(expected.Links, actual.Links, CompareEpubMetadataLinks);
            CollectionComparer.CompareCollections(expected.MetaItems, actual.MetaItems, CompareEpubMetadataMetas);
        }

        public static void CompareEpubMetadataCreators(EpubMetadataCreator expected, EpubMetadataCreator actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Creator, actual.Creator);
            Assert.Equal(expected.FileAs, actual.FileAs);
            Assert.Equal(expected.Role, actual.Role);
        }

        public static void CompareEpubMetadataContributors(EpubMetadataContributor expected, EpubMetadataContributor actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Contributor, actual.Contributor);
            Assert.Equal(expected.FileAs, actual.FileAs);
            Assert.Equal(expected.Role, actual.Role);
        }

        public static void CompareEpubMetadataDates(EpubMetadataDate expected, EpubMetadataDate actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Date, actual.Date);
            Assert.Equal(expected.Event, actual.Event);
        }

        public static void CompareEpubMetadataIdentifiers(EpubMetadataIdentifier expected, EpubMetadataIdentifier actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Scheme, actual.Scheme);
            Assert.Equal(expected.Identifier, actual.Identifier);
        }

        public static void CompareEpubMetadataLinks(EpubMetadataLink expected, EpubMetadataLink actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Href, actual.Href);
            Assert.Equal(expected.MediaType, actual.MediaType);
            Assert.Equal(expected.Properties, actual.Properties);
            Assert.Equal(expected.Refines, actual.Refines);
            Assert.Equal(expected.Relationships, actual.Relationships);
        }

        public static void CompareEpubMetadataMetas(EpubMetadataMeta expected, EpubMetadataMeta actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Content, actual.Content);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Refines, actual.Refines);
            Assert.Equal(expected.Property, actual.Property);
            Assert.Equal(expected.Scheme, actual.Scheme);
        }

        public static void CompareEpubManifests(EpubManifest expected, EpubManifest actual)
        {
            Assert.NotNull(actual);
            CollectionComparer.CompareCollections(expected.Items, actual.Items, CompareEpubManifestItems);
        }

        public static void CompareEpubManifestItems(EpubManifestItem expected, EpubManifestItem actual)
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

        public static void CompareEpubSpines(EpubSpine expected, EpubSpine actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.PageProgressionDirection, actual.PageProgressionDirection);
            Assert.Equal(expected.Toc, actual.Toc);
            CollectionComparer.CompareCollections(expected.Items, actual.Items, CompareEpubSpineItemRefs);
        }

        public static void CompareEpubSpineItemRefs(EpubSpineItemRef expected, EpubSpineItemRef actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.IdRef, actual.IdRef);
            Assert.Equal(expected.IsLinear, actual.IsLinear);
            Assert.Equal(expected.Properties, actual.Properties);
        }

        public static void CompareEpubGuides(EpubGuide? expected, EpubGuide? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                CompareEpubGuideReferenceLists(expected.Items, actual.Items);
            }
        }

        public static void CompareEpubGuideReferenceLists(List<EpubGuideReference> expected, List<EpubGuideReference> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareEpubGuideReferences);
        }

        public static void CompareEpubGuideReferences(EpubGuideReference expected, EpubGuideReference actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Href, actual.Href);
        }
    }
}
