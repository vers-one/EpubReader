namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubReadingOrder
    {
        public static List<EpubLocalTextContentFile> CreateFullTestEpubReadingOrder()
        {
            return new()
            {
                TestEpubContent.Chapter1File,
                TestEpubContent.Chapter2File
            };
        }
    }
}
