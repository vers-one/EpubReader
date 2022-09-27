namespace VersOne.Epub.Test.Comparers
{
    internal static class CollectionComparer
    {
        public static void CompareCollections<T>(IList<T> expected, IList<T> actual, Action<T, T> elementComparer)
        {
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                elementComparer(expected[i], actual[i]);
            }
        }

        public static void CompareDictionaries<TKey, TValue>(IDictionary<TKey, TValue> expected, IDictionary<TKey, TValue> actual, Action<TValue, TValue> elementValueComprarer)
        {
            Assert.Equal(expected.Count, actual.Count);
            foreach (KeyValuePair<TKey, TValue> expectedKeyValuePair in expected)
            {
                TKey expectedKey = expectedKeyValuePair.Key;
                TValue expectedValue = expectedKeyValuePair.Value;
                if (actual.TryGetValue(expectedKey, out TValue actualValue))
                {
                    elementValueComprarer(expectedValue, actualValue);
                }
                else
                {
                    Assert.Fail($"Key {expectedKey} was expected but not found in the dictionary.");
                }
            }
        }
    }
}
