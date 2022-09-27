namespace VersOne.Epub.Test.Comparers
{
    internal static class EpubSchemaComparer
    {
        public static void CompareEpubSchemas(EpubSchema expected, EpubSchema actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.ContentDirectoryPath, actual.ContentDirectoryPath);
            EpubPackageComparer.CompareEpubPackages(expected.Package, actual.Package);
            Epub2NcxComparer.CompareEpub2Ncxes(expected.Epub2Ncx, actual.Epub2Ncx);
            Epub3NavDocumentComparer.CompareEpub3NavDocuments(expected.Epub3NavDocument, actual.Epub3NavDocument);
        }
    }
}
