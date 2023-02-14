using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ops
{
    public class Epub3NavTests
    {
        private const Epub3StructuralSemanticsProperty TYPE = Epub3StructuralSemanticsProperty.TOC;
        private const bool IS_HIDDEN = false;
        private const string HEAD = "Head";

        private static Epub3NavOl Ol =>
            new
            (
                isHidden: true,
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
            );

        [Fact(DisplayName = "Constructing a Epub3Nav instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Epub3Nav epub3Nav = new(TYPE, IS_HIDDEN, HEAD, Ol);
            Assert.Equal(TYPE, epub3Nav.Type);
            Assert.Equal(IS_HIDDEN, epub3Nav.IsHidden);
            Assert.Equal(HEAD, epub3Nav.Head);
            Epub3NavDocumentComparer.CompareEpub3NavOls(Ol, epub3Nav.Ol);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if ol parameter is null")]
        public void ConstructorWithNullOlTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Epub3Nav(TYPE, IS_HIDDEN, HEAD, null!));
        }

        [Fact(DisplayName = "Constructing a Epub3Nav instance with null head parameter should succeed")]
        public void ConstructorWithNullHeadTest()
        {
            Epub3Nav epub3Nav = new(TYPE, IS_HIDDEN, null, Ol);
            Assert.Equal(TYPE, epub3Nav.Type);
            Assert.Equal(IS_HIDDEN, epub3Nav.IsHidden);
            Assert.Null(epub3Nav.Head);
            Epub3NavDocumentComparer.CompareEpub3NavOls(Ol, epub3Nav.Ol);
        }
    }
}
