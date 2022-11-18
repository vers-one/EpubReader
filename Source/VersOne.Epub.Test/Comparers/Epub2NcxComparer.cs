using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Comparers
{
    internal static class Epub2NcxComparer
    {
        public static void CompareEpub2Ncxes(Epub2Ncx? expected, Epub2Ncx? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                CompareEpub2NcxHeads(expected.Head, actual.Head);
                Assert.Equal(expected.DocTitle, actual.DocTitle);
                Assert.Equal(expected.DocAuthors, actual.DocAuthors);
                CompareEpub2NcxNavigationMaps(expected.NavMap, actual.NavMap);
                CompareEpub2NcxPageLists(expected.PageList, actual.PageList);
                CompareEpub2NcxNavigationListLists(expected.NavLists, actual.NavLists);
            }
        }

        public static void CompareEpub2NcxHeads(Epub2NcxHead expected, Epub2NcxHead actual)
        {
            Assert.NotNull(actual);
            CollectionComparer.CompareCollections(expected.Items, actual.Items, ComprareEpub2NcxHeadMetas);
        }

        public static void CompareEpub2NcxNavigationMaps(Epub2NcxNavigationMap expected, Epub2NcxNavigationMap actual)
        {
            Assert.NotNull(actual);
            CompareEpub2NcxNavigationPointLists(expected.Items, actual.Items);
        }

        public static void CompareEpub2NcxPageLists(Epub2NcxPageList? expected, Epub2NcxPageList? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                CompareEpub2NcxPageTargetLists(expected.Items, actual.Items);
            }
        }

        public static void CompareEpub2NcxNavigationTargetLists(List<Epub2NcxNavigationTarget> expected, List<Epub2NcxNavigationTarget> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareEpub2NcxNavigationTargets);
        }

        public static void CompareEpub2NcxNavigationTargets(Epub2NcxNavigationTarget expected, Epub2NcxNavigationTarget actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.Class, actual.Class);
            Assert.Equal(expected.PlayOrder, actual.PlayOrder);
            CompareEpub2NcxNavigationLabelLists(expected.NavigationLabels, actual.NavigationLabels);
            CompareEpub2NcxContents(expected.Content, actual.Content);
        }

        public static void CompareEpub2NcxPageTargetLists(List<Epub2NcxPageTarget> expected, List<Epub2NcxPageTarget> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareEpub2NcxPageTargets);
        }

        public static void CompareEpub2NcxPageTargets(Epub2NcxPageTarget expected, Epub2NcxPageTarget actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Class, actual.Class);
            Assert.Equal(expected.PlayOrder, actual.PlayOrder);
            CompareEpub2NcxNavigationLabelLists(expected.NavigationLabels, actual.NavigationLabels);
            CompareEpub2NcxContents(expected.Content, actual.Content);
        }

        public static void ComprareEpub2NcxHeadMetas(Epub2NcxHeadMeta expected, Epub2NcxHeadMeta actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Content, actual.Content);
            Assert.Equal(expected.Scheme, actual.Scheme);
        }

        public static void CompareEpub2NcxNavigationListLists(List<Epub2NcxNavigationList> expected, List<Epub2NcxNavigationList> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareEpub2NcxNavigationLists);
        }

        public static void CompareEpub2NcxNavigationLists(Epub2NcxNavigationList expected, Epub2NcxNavigationList actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Class, actual.Class);
            CompareEpub2NcxNavigationLabelLists(expected.NavigationLabels, actual.NavigationLabels);
            CompareEpub2NcxNavigationTargetLists(expected.NavigationTargets, actual.NavigationTargets);
        }

        public static void CompareEpub2NcxNavigationLabelLists(List<Epub2NcxNavigationLabel> expected, List<Epub2NcxNavigationLabel> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareEpub2NcxNavigationLabels);
        }

        public static void CompareEpub2NcxNavigationLabels(Epub2NcxNavigationLabel expected, Epub2NcxNavigationLabel actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Text, actual.Text);
        }

        public static void CompareEpub2NcxNavigationPointLists(List<Epub2NcxNavigationPoint> expected, List<Epub2NcxNavigationPoint> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareEpub2NcxNavigationPoints);
        }

        public static void CompareEpub2NcxNavigationPoints(Epub2NcxNavigationPoint expected, Epub2NcxNavigationPoint actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Class, actual.Class);
            Assert.Equal(expected.PlayOrder, actual.PlayOrder);
            CompareEpub2NcxNavigationLabelLists(expected.NavigationLabels, actual.NavigationLabels);
            CompareEpub2NcxContents(expected.Content, actual.Content);
            CompareEpub2NcxNavigationPointLists(expected.ChildNavigationPoints, actual.ChildNavigationPoints);
        }

        public static void CompareEpub2NcxContents(Epub2NcxContent? expected, Epub2NcxContent? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Source, actual.Source);
            }
        }
    }
}
