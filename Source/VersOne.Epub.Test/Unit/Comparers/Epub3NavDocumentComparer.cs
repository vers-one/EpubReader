using VersOne.Epub.Schema;
using VersOne.Epub.Test.Unit.TestUtils;

namespace VersOne.Epub.Test.Unit.Comparers
{
    internal static class Epub3NavDocumentComparer
    {
        public static void CompareEpub3NavDocuments(Epub3NavDocument expected, Epub3NavDocument actual)
        {
            Assert.NotNull(actual);
            Assert.NotNull(actual.Navs);
            AssertUtils.CollectionsEqual(expected.Navs, actual.Navs, CompareEpub3Navs);
        }

        private static void CompareEpub3Navs(Epub3Nav expected, Epub3Nav actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.IsHidden, actual.IsHidden);
            Assert.Equal(expected.Head, actual.Head);
            CompareEpub3NavOls(expected.Ol, actual.Ol);
        }

        private static void CompareEpub3NavOls(Epub3NavOl expected, Epub3NavOl actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.IsHidden, actual.IsHidden);
                AssertUtils.CollectionsEqual(expected.Lis, actual.Lis, CompareEpub3NavLis);
            }
        }

        private static void CompareEpub3NavLis(Epub3NavLi expected, Epub3NavLi actual)
        {
            Assert.NotNull(actual);
            CompareEpub3NavAnchors(expected.Anchor, actual.Anchor);
            CompareEpub3NavSpans(expected.Span, actual.Span);
            CompareEpub3NavOls(expected.ChildOl, actual.ChildOl);
        }

        private static void CompareEpub3NavAnchors(Epub3NavAnchor expected, Epub3NavAnchor actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Href, actual.Href);
                Assert.Equal(expected.Text, actual.Text);
                Assert.Equal(expected.Title, actual.Title);
                Assert.Equal(expected.Alt, actual.Alt);
                Assert.Equal(expected.Type, actual.Type);
            }
        }

        private static void CompareEpub3NavSpans(Epub3NavSpan expected, Epub3NavSpan actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.Equal(expected.Text, actual.Text);
                Assert.Equal(expected.Title, actual.Title);
                Assert.Equal(expected.Alt, actual.Alt);
            }
        }
    }
}
