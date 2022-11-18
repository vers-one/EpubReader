using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ops
{
    public class Epub3NavDocumentTests
    {
        private const string FILE_PATH = "Content/toc.html";

        private static List<Epub3Nav> Navs =>
            new()
            {
                new Epub3Nav
                (
                    type: Epub3NavStructuralSemanticsProperty.BODYMATTER,
                    isHidden: false,
                    head: "Head",
                    ol: new Epub3NavOl
                    (
                        isHidden: false,
                        lis: new List<Epub3NavLi>()
                        {
                            new Epub3NavLi
                            (
                                anchor: new Epub3NavAnchor
                                (
                                    href: "chapter.html#page",
                                    text: "1"
                                )
                            )
                        }
                    )
                )
            };

        [Fact(DisplayName = "Constructing a Epub3NavDocument instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub3NavDocument epub3NavDocument = new(FILE_PATH, Navs);
            Assert.Equal(FILE_PATH, epub3NavDocument.FilePath);
            Epub3NavDocumentComparer.CompareEpub3NavLists(Navs, epub3NavDocument.Navs);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if filePath parameter is null")]
        public void ConstructorWithNullFilePathTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub3NavDocument(null!, Navs));
        }

        [Fact(DisplayName = "Constructing a Epub3NavDocument instance with null navs parameter should succeed")]
        public void ConstructorWithNullNavsTest()
        {
            Epub3NavDocument epub3NavDocument = new(FILE_PATH, null);
            Assert.Equal(FILE_PATH, epub3NavDocument.FilePath);
            Epub3NavDocumentComparer.CompareEpub3NavLists(new List<Epub3Nav>(), epub3NavDocument.Navs);
        }
    }
}
