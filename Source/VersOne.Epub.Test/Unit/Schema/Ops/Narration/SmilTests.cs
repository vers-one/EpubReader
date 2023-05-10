using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ops.Narration
{
    public class SmilTests
    {
        private const string ID = "id";
        private const SmilVersion VERSION = SmilVersion.SMIL_3;
        private const string EPUB_PREFIX = "test: http://example.com/test/spec/";

        private static SmilHead Head => new(null);

        private static SmilBody Body =>
            new
            (
                id: null,
                epubTypes: null,
                epubTextRef: null,
                seqs: new List<SmilSeq>(),
                pars: new List<SmilPar>()
            );

        [Fact(DisplayName = "Constructing a Smil instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            Smil smil = new(ID, VERSION, EPUB_PREFIX, Head, Body);
            Assert.Equal(ID, smil.Id);
            Assert.Equal(VERSION, smil.Version);
            Assert.Equal(EPUB_PREFIX, smil.EpubPrefix);
            SmilComparers.CompareSmilHeads(Head, smil.Head);
            SmilComparers.CompareSmilBodies(Body, smil.Body);
        }

        [Fact(DisplayName = "Constructing a Smil instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            Smil smil = new(null, VERSION, EPUB_PREFIX, Head, Body);
            Assert.Null(smil.Id);
            Assert.Equal(VERSION, smil.Version);
            Assert.Equal(EPUB_PREFIX, smil.EpubPrefix);
            SmilComparers.CompareSmilHeads(Head, smil.Head);
            SmilComparers.CompareSmilBodies(Body, smil.Body);
        }

        [Fact(DisplayName = "Constructing a Smil instance with null epubPrefix parameter should succeed")]
        public void ConstructorWithNullEpubPrefixTest()
        {
            Smil smil = new(ID, VERSION, null, Head, Body);
            Assert.Equal(ID, smil.Id);
            Assert.Equal(VERSION, smil.Version);
            Assert.Null(smil.EpubPrefix);
            SmilComparers.CompareSmilHeads(Head, smil.Head);
            SmilComparers.CompareSmilBodies(Body, smil.Body);
        }

        [Fact(DisplayName = "Constructing a Smil instance with null head parameter should succeed")]
        public void ConstructorWithNullHeadTest()
        {
            Smil smil = new(ID, VERSION, EPUB_PREFIX, null, Body);
            Assert.Equal(ID, smil.Id);
            Assert.Equal(VERSION, smil.Version);
            Assert.Equal(EPUB_PREFIX, smil.EpubPrefix);
            Assert.Null(smil.Head);
            SmilComparers.CompareSmilBodies(Body, smil.Body);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if body parameter is null")]
        public void ConstructorWithNullBodyTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Smil(ID, VERSION, EPUB_PREFIX, Head, null!));
        }
    }
}
