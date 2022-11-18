using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.TestData;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.Entities
{
    public class EpubSchemaTests
    {
        private static EpubPackage Package => TestEpubPackages.CreateMinimalTestEpubPackage();

        private static Epub2Ncx Epub2Ncx => TestEpub2Ncxes.CreateFullTestEpub2Ncx();

        private static Epub3NavDocument Epub3NavDocument => TestEpub3NavDocuments.CreateFullTestEpub3NavDocument();

        [Fact(DisplayName = "Constructing a EpubSchema instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubSchema epubSchema = new(Package, Epub2Ncx, Epub3NavDocument, CONTENT_DIRECTORY_PATH);
            EpubPackageComparer.CompareEpubPackages(Package, epubSchema.Package);
            Epub2NcxComparer.CompareEpub2Ncxes(Epub2Ncx, epubSchema.Epub2Ncx);
            Epub3NavDocumentComparer.CompareEpub3NavDocuments(Epub3NavDocument, epubSchema.Epub3NavDocument);
            Assert.Equal(CONTENT_DIRECTORY_PATH, epubSchema.ContentDirectoryPath);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if package parameter is null")]
        public void ConstructorWithNullPackageTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubSchema(null!, Epub2Ncx, Epub3NavDocument, CONTENT_DIRECTORY_PATH));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if contentDirectoryPath parameter is null")]
        public void ConstructorWithNullContentDirectoryPathTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubSchema(Package, Epub2Ncx, Epub3NavDocument, null!));
        }

        [Fact(DisplayName = "Constructing a EpubSchema instance with null epub2Ncx parameter should succeed")]
        public void ConstructorWithNullEpub2NcxParameterTest()
        {
            EpubSchema epubSchema = new(Package, null, Epub3NavDocument, CONTENT_DIRECTORY_PATH);
            EpubPackageComparer.CompareEpubPackages(Package, epubSchema.Package);
            Epub2NcxComparer.CompareEpub2Ncxes(null, epubSchema.Epub2Ncx);
            Epub3NavDocumentComparer.CompareEpub3NavDocuments(Epub3NavDocument, epubSchema.Epub3NavDocument);
            Assert.Equal(CONTENT_DIRECTORY_PATH, epubSchema.ContentDirectoryPath);
        }

        [Fact(DisplayName = "Constructing a EpubSchema instance with null epub3NavDocument parameter should succeed")]
        public void ConstructorWithNullEpub3NavDocumentParameterTest()
        {
            EpubSchema epubSchema = new(Package, Epub2Ncx, null, CONTENT_DIRECTORY_PATH);
            EpubPackageComparer.CompareEpubPackages(Package, epubSchema.Package);
            Epub2NcxComparer.CompareEpub2Ncxes(Epub2Ncx, epubSchema.Epub2Ncx);
            Epub3NavDocumentComparer.CompareEpub3NavDocuments(null, epubSchema.Epub3NavDocument);
            Assert.Equal(CONTENT_DIRECTORY_PATH, epubSchema.ContentDirectoryPath);
        }
    }
}
