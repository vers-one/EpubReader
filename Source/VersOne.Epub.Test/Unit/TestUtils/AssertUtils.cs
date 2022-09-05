namespace VersOne.Epub.Test.Unit.TestUtils
{
    internal static class AssertUtils
    {
        public static void CollectionsEqual<T>(IList<T> expected, IList<T> actual, Action<T, T> elementComparer)
        {
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                elementComparer(expected[i], actual[i]);
            }
        }
    }
}
