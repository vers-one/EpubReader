using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Comparers
{
    internal static class EpubPackageComparer
    {
        public static void CompareEpubPackages(EpubPackage expected, EpubPackage actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.UniqueIdentifier, actual.UniqueIdentifier);
            Assert.Equal(expected.EpubVersion, actual.EpubVersion);
            EpubMetadataComparer.CompareEpubMetadatas(expected.Metadata, actual.Metadata);
            CompareEpubManifests(expected.Manifest, actual.Manifest);
            CompareEpubSpines(expected.Spine, actual.Spine);
            CompareEpubGuides(expected.Guide, actual.Guide);
            CompareEpubCollectionLists(expected.Collections, actual.Collections);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Prefix, actual.Prefix);
            Assert.Equal(expected.Language, actual.Language);
        }

        public static void CompareEpubManifests(EpubManifest expected, EpubManifest actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
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

        public static void CompareEpubCollectionLists(List<EpubCollection> expected, List<EpubCollection> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareEpubCollections);
        }

        public static void CompareEpubCollections(EpubCollection expected, EpubCollection actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Role, actual.Role);
            EpubMetadataComparer.CompareEpubMetadatas(expected.Metadata, actual.Metadata);
            CompareEpubCollectionLists(expected.NestedCollections, actual.NestedCollections);
            EpubMetadataComparer.CompareEpubMetadataLinkLists(expected.Links, actual.Links);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Language, actual.Language);
        }
    }
}
