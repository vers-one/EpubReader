using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ops
{
    public class SmilSeqTests
    {
        private const string ID = "id";
        private const string EPUB_TEXT_REF = "chapter.html";

        private static List<Epub3StructuralSemanticsProperty> EpubTypes => new();

        private static List<SmilSeq> Seqs => new();

        private static List<SmilPar> Pars => new();

        [Fact(DisplayName = "Constructing a SmilSeq instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            SmilSeq smilSeq = new(ID, EpubTypes, EPUB_TEXT_REF, Seqs, Pars);
            Assert.Equal(ID, smilSeq.Id);
            Assert.Equal(EpubTypes, smilSeq.EpubTypes);
            Assert.Equal(EPUB_TEXT_REF, smilSeq.EpubTextRef);
            SmilComparers.CompareSmilSeqLists(Seqs, smilSeq.Seqs);
            SmilComparers.CompareSmilParLists(Pars, smilSeq.Pars);
        }

        [Fact(DisplayName = "Constructing a SmilSeq instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            SmilSeq smilSeq = new(null, EpubTypes, EPUB_TEXT_REF, Seqs, Pars);
            Assert.Null(smilSeq.Id);
            Assert.Equal(EpubTypes, smilSeq.EpubTypes);
            Assert.Equal(EPUB_TEXT_REF, smilSeq.EpubTextRef);
            SmilComparers.CompareSmilSeqLists(Seqs, smilSeq.Seqs);
            SmilComparers.CompareSmilParLists(Pars, smilSeq.Pars);
        }

        [Fact(DisplayName = "Constructing a SmilSeq instance with null epubTypes parameter should succeed")]
        public void ConstructorWithNullEpubTypesTest()
        {
            SmilSeq smilSeq = new(ID, null, EPUB_TEXT_REF, Seqs, Pars);
            Assert.Equal(ID, smilSeq.Id);
            Assert.Null(smilSeq.EpubTypes);
            Assert.Equal(EPUB_TEXT_REF, smilSeq.EpubTextRef);
            SmilComparers.CompareSmilSeqLists(Seqs, smilSeq.Seqs);
            SmilComparers.CompareSmilParLists(Pars, smilSeq.Pars);
        }

        [Fact(DisplayName = "Constructing a SmilSeq instance with null epubTextRef parameter should succeed")]
        public void ConstructorWithNullEpubTextRefTest()
        {
            SmilSeq smilSeq = new(ID, EpubTypes, null, Seqs, Pars);
            Assert.Equal(ID, smilSeq.Id);
            Assert.Equal(EpubTypes, smilSeq.EpubTypes);
            Assert.Null(smilSeq.EpubTextRef);
            SmilComparers.CompareSmilSeqLists(Seqs, smilSeq.Seqs);
            SmilComparers.CompareSmilParLists(Pars, smilSeq.Pars);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if seqs parameter is null")]
        public void ConstructorWithNullSeqsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SmilSeq(ID, EpubTypes, EPUB_TEXT_REF, null!, Pars));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if pars parameter is null")]
        public void ConstructorWithNullParsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SmilSeq(ID, EpubTypes, EPUB_TEXT_REF, Seqs, null!));
        }
    }
}
