namespace VersOne.Epub.Test.Comparers
{
    internal static class EpubNarrationComparers
    {
        public static void CompareEpubNarrationTimestamps(EpubNarrationTimestamp? expected, EpubNarrationTimestamp? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected?.Hour, actual?.Hour);
                Assert.Equal(expected?.Minute, actual?.Minute);
                Assert.Equal(expected?.Second, actual?.Second);
                Assert.Equal(expected?.Millisecond, actual?.Millisecond);
            }
        }
    }
}
