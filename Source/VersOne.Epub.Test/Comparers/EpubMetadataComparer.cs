using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Comparers
{
    internal static class EpubMetadataComparer
    {
        public static void CompareEpubMetadatas(EpubMetadata? expected, EpubMetadata? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                CollectionComparer.CompareCollections(expected.Titles, actual.Titles, CompareEpubMetadataTitles);
                CollectionComparer.CompareCollections(expected.Creators, actual.Creators, CompareEpubMetadataCreators);
                CollectionComparer.CompareCollections(expected.Subjects, actual.Subjects, CompareEpubMetadataSubjects);
                CollectionComparer.CompareCollections(expected.Descriptions, actual.Descriptions, CompareEpubMetadataDescriptions);
                CollectionComparer.CompareCollections(expected.Publishers, actual.Publishers, CompareEpubMetadataPublishers);
                CollectionComparer.CompareCollections(expected.Contributors, actual.Contributors, CompareEpubMetadataContributors);
                CollectionComparer.CompareCollections(expected.Dates, actual.Dates, CompareEpubMetadataDates);
                CollectionComparer.CompareCollections(expected.Types, actual.Types, CompareEpubMetadataTypes);
                CollectionComparer.CompareCollections(expected.Formats, actual.Formats, CompareEpubMetadataFormats);
                CollectionComparer.CompareCollections(expected.Identifiers, actual.Identifiers, CompareEpubMetadataIdentifiers);
                CollectionComparer.CompareCollections(expected.Sources, actual.Sources, CompareEpubMetadataSources);
                CollectionComparer.CompareCollections(expected.Languages, actual.Languages, CompareEpubMetadataLanguages);
                CollectionComparer.CompareCollections(expected.Relations, actual.Relations, CompareEpubMetadataRelations);
                CollectionComparer.CompareCollections(expected.Coverages, actual.Coverages, CompareEpubMetadataCoverages);
                CollectionComparer.CompareCollections(expected.Rights, actual.Rights, CompareEpubMetadataRights);
                CompareEpubMetadataLinkLists(expected.Links, actual.Links);
                CollectionComparer.CompareCollections(expected.MetaItems, actual.MetaItems, CompareEpubMetadataMetas);
            }
        }

        public static void CompareEpubMetadataTitles(EpubMetadataTitle expected, EpubMetadataTitle actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Language, actual.Language);
        }

        public static void CompareEpubMetadataCreators(EpubMetadataCreator expected, EpubMetadataCreator actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Creator, actual.Creator);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.FileAs, actual.FileAs);
            Assert.Equal(expected.Role, actual.Role);
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Language, actual.Language);
        }

        public static void CompareEpubMetadataSubjects(EpubMetadataSubject expected, EpubMetadataSubject actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Subject, actual.Subject);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Language, actual.Language);
        }

        public static void CompareEpubMetadataDescriptions(EpubMetadataDescription expected, EpubMetadataDescription actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Language, actual.Language);
        }

        public static void CompareEpubMetadataPublishers(EpubMetadataPublisher expected, EpubMetadataPublisher actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Publisher, actual.Publisher);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Language, actual.Language);
        }

        public static void CompareEpubMetadataContributors(EpubMetadataContributor expected, EpubMetadataContributor actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Contributor, actual.Contributor);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.FileAs, actual.FileAs);
            Assert.Equal(expected.Role, actual.Role);
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Language, actual.Language);
        }

        public static void CompareEpubMetadataDates(EpubMetadataDate expected, EpubMetadataDate actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Date, actual.Date);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Event, actual.Event);
        }

        public static void CompareEpubMetadataTypes(EpubMetadataType expected, EpubMetadataType actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Id, actual.Id);
        }

        public static void CompareEpubMetadataFormats(EpubMetadataFormat expected, EpubMetadataFormat actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Format, actual.Format);
            Assert.Equal(expected.Id, actual.Id);
        }

        public static void CompareEpubMetadataIdentifiers(EpubMetadataIdentifier expected, EpubMetadataIdentifier actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Identifier, actual.Identifier);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Scheme, actual.Scheme);
        }

        public static void CompareEpubMetadataSources(EpubMetadataSource expected, EpubMetadataSource actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Source, actual.Source);
            Assert.Equal(expected.Id, actual.Id);
        }

        public static void CompareEpubMetadataLanguages(EpubMetadataLanguage expected, EpubMetadataLanguage actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Language, actual.Language);
            Assert.Equal(expected.Id, actual.Id);
        }

        public static void CompareEpubMetadataRelations(EpubMetadataRelation expected, EpubMetadataRelation actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Relation, actual.Relation);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Language, actual.Language);
        }

        public static void CompareEpubMetadataCoverages(EpubMetadataCoverage expected, EpubMetadataCoverage actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Coverage, actual.Coverage);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Language, actual.Language);
        }

        public static void CompareEpubMetadataRights(EpubMetadataRights expected, EpubMetadataRights actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Rights, actual.Rights);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Language, actual.Language);
        }

        public static void CompareEpubMetadataLinkLists(List<EpubMetadataLink> expected, List<EpubMetadataLink> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareEpubMetadataLinks);
        }

        public static void CompareEpubMetadataLinks(EpubMetadataLink expected, EpubMetadataLink actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Href, actual.Href);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.MediaType, actual.MediaType);
            Assert.Equal(expected.Properties, actual.Properties);
            Assert.Equal(expected.Refines, actual.Refines);
            Assert.Equal(expected.Relationships, actual.Relationships);
            Assert.Equal(expected.HrefLanguage, actual.HrefLanguage);
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
            Assert.Equal(expected.TextDirection, actual.TextDirection);
            Assert.Equal(expected.Language, actual.Language);
        }
    }
}
