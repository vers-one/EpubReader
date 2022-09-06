using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.TestUtils;

namespace VersOne.Epub.Test.Unit.Comparers
{
    internal static class Epub2NcxComparer
    {
        public static void CompareEpub2Ncxes(Epub2Ncx expected, Epub2Ncx actual)
        {
            Assert.NotNull(actual);
            CompareEpub2NcxHeads(expected.Head, actual.Head);
            Assert.Equal(expected.DocTitle, actual.DocTitle);
            Assert.Equal(expected.DocAuthors, actual.DocAuthors);
            CompareEpub2NcxNavigationMaps(expected.NavMap, actual.NavMap);
            CompareEpub2NcxPageLists(expected.PageList, actual.PageList);
            AssertUtils.CollectionsEqual(expected.NavLists, actual.NavLists, CompareEpub2NcxNavigationLists);
        }

        private static void CompareEpub2NcxHeads(Epub2NcxHead expected, Epub2NcxHead actual)
        {
            Assert.NotNull(actual);
            AssertUtils.CollectionsEqual(expected, actual, ComprareEpub2NcxHeadMetas);
        }

        private static void ComprareEpub2NcxHeadMetas(Epub2NcxHeadMeta expected, Epub2NcxHeadMeta actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Content, actual.Content);
            Assert.Equal(expected.Scheme, actual.Scheme);
        }

        private static void CompareEpub2NcxNavigationMaps(Epub2NcxNavigationMap expected, Epub2NcxNavigationMap actual)
        {
            Assert.NotNull(actual);
            AssertUtils.CollectionsEqual(expected, actual, CompareEpub2NcxNavigationPoints);
        }

        private static void CompareEpub2NcxNavigationPoints(Epub2NcxNavigationPoint expected, Epub2NcxNavigationPoint actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Class, actual.Class);
            Assert.Equal(expected.PlayOrder, actual.PlayOrder);
            AssertUtils.CollectionsEqual(expected.NavigationLabels, actual.NavigationLabels, CompareEpub2NcxNavigationLabels);
            CompareEpub2NcxContents(expected.Content, actual.Content);
            AssertUtils.CollectionsEqual(expected.ChildNavigationPoints, actual.ChildNavigationPoints, CompareEpub2NcxNavigationPoints);
        }

        private static void CompareEpub2NcxNavigationLabels(Epub2NcxNavigationLabel expected, Epub2NcxNavigationLabel actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Text, actual.Text);
        }

        private static void CompareEpub2NcxContents(Epub2NcxContent expected, Epub2NcxContent actual)
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

        private static void CompareEpub2NcxPageLists(Epub2NcxPageList expected, Epub2NcxPageList actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                AssertUtils.CollectionsEqual(expected, actual, CompareEpub2NcxPageTargets);
            }
        }

        private static void CompareEpub2NcxPageTargets(Epub2NcxPageTarget expected, Epub2NcxPageTarget actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Class, actual.Class);
            Assert.Equal(expected.PlayOrder, actual.PlayOrder);
            AssertUtils.CollectionsEqual(expected.NavigationLabels, actual.NavigationLabels, CompareEpub2NcxNavigationLabels);
            CompareEpub2NcxContents(expected.Content, actual.Content);
        }

        private static void CompareEpub2NcxNavigationLists(Epub2NcxNavigationList expected, Epub2NcxNavigationList actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Class, actual.Class);
            AssertUtils.CollectionsEqual(expected.NavigationLabels, actual.NavigationLabels, CompareEpub2NcxNavigationLabels);
            AssertUtils.CollectionsEqual(expected.NavigationTargets, actual.NavigationTargets, CompareEpub2NcxNavigationTargets);
        }

        private static void CompareEpub2NcxNavigationTargets(Epub2NcxNavigationTarget expected, Epub2NcxNavigationTarget actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.Class, actual.Class);
            Assert.Equal(expected.PlayOrder, actual.PlayOrder);
            AssertUtils.CollectionsEqual(expected.NavigationLabels, actual.NavigationLabels, CompareEpub2NcxNavigationLabels);
            CompareEpub2NcxContents(expected.Content, actual.Content);
        }
    }
}
