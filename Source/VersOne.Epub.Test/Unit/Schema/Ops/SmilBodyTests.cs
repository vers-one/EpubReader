using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ops
{
    public class SmilBodyTests
    {
        private const string ID = "id";
        private const string EPUB_TEXT_REF = "chapter.html";

        private static List<Epub3StructuralSemanticsProperty> EpubTypes => new();
        
        private static List<SmilSeq> Seqs => new();

        private static List<SmilPar> Pars => new();

        [Fact(DisplayName = "Constructing a SmilBody instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            SmilBody smilBody = new(ID, EpubTypes, EPUB_TEXT_REF, Seqs, Pars);
            Assert.Equal(ID, smilBody.Id);
            Assert.Equal(EpubTypes, smilBody.EpubTypes);
            Assert.Equal(EPUB_TEXT_REF, smilBody.EpubTextRef);
            SmilComparers.CompareSmilSeqLists(Seqs, smilBody.Seqs);
            SmilComparers.CompareSmilParLists(Pars, smilBody.Pars);
        }

        [Fact(DisplayName = "Constructing a SmilBody instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            SmilBody smilBody = new(null, EpubTypes, EPUB_TEXT_REF, Seqs, Pars);
            Assert.Null(smilBody.Id);
            Assert.Equal(EpubTypes, smilBody.EpubTypes);
            Assert.Equal(EPUB_TEXT_REF, smilBody.EpubTextRef);
            SmilComparers.CompareSmilSeqLists(Seqs, smilBody.Seqs);
            SmilComparers.CompareSmilParLists(Pars, smilBody.Pars);
        }

        [Fact(DisplayName = "Constructing a SmilBody instance with null epubTypes parameter should succeed")]
        public void ConstructorWithNullEpubTypesTest()
        {
            SmilBody smilBody = new(ID, null, EPUB_TEXT_REF, Seqs, Pars);
            Assert.Equal(ID, smilBody.Id);
            Assert.Null(smilBody.EpubTypes);
            Assert.Equal(EPUB_TEXT_REF, smilBody.EpubTextRef);
            SmilComparers.CompareSmilSeqLists(Seqs, smilBody.Seqs);
            SmilComparers.CompareSmilParLists(Pars, smilBody.Pars);
        }

        [Fact(DisplayName = "Constructing a SmilBody instance with null epubTextRef parameter should succeed")]
        public void ConstructorWithNullEpubTextRefTest()
        {
            SmilBody smilBody = new(ID, EpubTypes, null, Seqs, Pars);
            Assert.Equal(ID, smilBody.Id);
            Assert.Equal(EpubTypes, smilBody.EpubTypes);
            Assert.Null(smilBody.EpubTextRef);
            SmilComparers.CompareSmilSeqLists(Seqs, smilBody.Seqs);
            SmilComparers.CompareSmilParLists(Pars, smilBody.Pars);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if seqs parameter is null")]
        public void ConstructorWithNullSeqsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SmilBody(ID, EpubTypes, EPUB_TEXT_REF, null!, Pars));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if pars parameter is null")]
        public void ConstructorWithNullParsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SmilBody(ID, EpubTypes, EPUB_TEXT_REF, Seqs, null!));
        }
    }
}
