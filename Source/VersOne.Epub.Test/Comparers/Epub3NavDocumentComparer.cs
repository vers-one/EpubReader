using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Comparers
{
    internal static class Epub3NavDocumentComparer
    {
        public static void CompareEpub3NavDocuments(Epub3NavDocument? expected, Epub3NavDocument? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.NotNull(actual.Navs);
                CompareEpub3NavLists(expected.Navs, actual.Navs);
            }
        }

        public static void CompareEpub3NavLists(List<Epub3Nav> expected, List<Epub3Nav> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareEpub3Navs);
        }

        public static void CompareEpub3Navs(Epub3Nav expected, Epub3Nav actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.IsHidden, actual.IsHidden);
            Assert.Equal(expected.Head, actual.Head);
            CompareEpub3NavOls(expected.Ol, actual.Ol);
        }

        public static void CompareEpub3NavOls(Epub3NavOl? expected, Epub3NavOl? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.IsHidden, actual.IsHidden);
                CompareEpub3NavLiLists(expected.Lis, actual.Lis);
            }
        }

        public static void CompareEpub3NavLiLists(List<Epub3NavLi> expected, List<Epub3NavLi> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareEpub3NavLis);
        }

        public static void CompareEpub3NavLis(Epub3NavLi expected, Epub3NavLi actual)
        {
            Assert.NotNull(actual);
            CompareEpub3NavAnchors(expected.Anchor, actual.Anchor);
            CompareEpub3NavSpans(expected.Span, actual.Span);
            CompareEpub3NavOls(expected.ChildOl, actual.ChildOl);
        }

        public static void CompareEpub3NavAnchors(Epub3NavAnchor? expected, Epub3NavAnchor? actual)
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

        private static void CompareEpub3NavSpans(Epub3NavSpan? expected, Epub3NavSpan? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Text, actual.Text);
                Assert.Equal(expected.Title, actual.Title);
                Assert.Equal(expected.Alt, actual.Alt);
            }
        }
    }
}
