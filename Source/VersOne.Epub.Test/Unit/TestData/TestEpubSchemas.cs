using VersOne.Epub.Schema;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestEpubSchemas
    {
        public static EpubSchema CreateMinimalTestEpubSchema()
        {
            return new
            (
                package: TestEpubPackages.CreateMinimalTestEpubPackage(),
                epub2Ncx: null,
                epub3NavDocument: new Epub3NavDocument
                (
                    filePath: NAV_FILE_PATH
                ),
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
        }

        public static EpubSchema CreateMinimalTestEpub2SchemaWithoutNavigation()
        {
            return new
            (
                package: TestEpubPackages.CreateMinimalTestEpub2PackageWithoutNavigation(),
                epub2Ncx: null,
                epub3NavDocument: null,
                mediaOverlays: null,
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
        }

        public static EpubSchema CreateFullTestEpubSchema()
        {
            return new
            (
                package: TestEpubPackages.CreateFullTestEpubPackage(),
                epub2Ncx: TestEpub2Ncxes.CreateFullTestEpub2Ncx(),
                epub3NavDocument: TestEpub3NavDocuments.CreateFullTestEpub3NavDocument(),
                mediaOverlays: new List<Smil>()
                {
                    TestSmils.CreateFullTestSmil()
                },
                contentDirectoryPath: CONTENT_DIRECTORY_PATH
            );
        }
    }
}
