namespace VersOne.Epub.Test.Unit.Comparers
{
    internal static class EpubBookRefComparer
    {
        public static void CompareEpubBookRefs(EpubBookRef expected, EpubBookRef actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.FilePath, actual.FilePath);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Author, actual.Author);
            Assert.Equal(expected.AuthorList, actual.AuthorList);
            Assert.Equal(expected.Description, actual.Description);
            EpubSchemaComparer.CompareEpubSchemas(expected.Schema, actual.Schema);
            EpubContentRefComparer.CompareEpubContentRefs(expected.Content, actual.Content);
            Assert.Equal(expected.EpubFile, actual.EpubFile);
        }
    }
}
