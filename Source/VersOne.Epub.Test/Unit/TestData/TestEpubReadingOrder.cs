namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubReadingOrder
    {
        public static List<EpubLocalTextContentFile> CreateFullTestEpubReadingOrder()
        {
            return
            [
                TestEpubContent.Chapter1File,
                TestEpubContent.Chapter2File
            ];
        }
    }
}
