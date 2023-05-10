using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Schema.Ops.Narration
{
    public class SmilParTests
    {
        private const string ID = "id";

        private static List<Epub3StructuralSemanticsProperty> EpubTypes => new();

        private static SmilText Text =>
            new
            (
                id: null,
                src: "chapter.html"
            );

        private static SmilAudio Audio =>
            new
            (
                id: null,
                src: "audio.mp3",
                clipBegin: null,
                clipEnd: null
            );

        [Fact(DisplayName = "Constructing a SmilPar instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            SmilPar smilPar = new(ID, EpubTypes, Text, Audio);
            Assert.Equal(ID, smilPar.Id);
            Assert.Equal(EpubTypes, smilPar.EpubTypes);
            SmilComparers.CompareSmilTexts(Text, smilPar.Text);
            SmilComparers.CompareSmilAudios(Audio, smilPar.Audio);
        }

        [Fact(DisplayName = "Constructing a SmilPar instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            SmilPar smilPar = new(null, EpubTypes, Text, Audio);
            Assert.Null(smilPar.Id);
            Assert.Equal(EpubTypes, smilPar.EpubTypes);
            SmilComparers.CompareSmilTexts(Text, smilPar.Text);
            SmilComparers.CompareSmilAudios(Audio, smilPar.Audio);
        }

        [Fact(DisplayName = "Constructing a SmilPar instance with null epubTypes parameter should succeed")]
        public void ConstructorWithNullEpubTypesTest()
        {
            SmilPar smilPar = new(ID, null, Text, Audio);
            Assert.Equal(ID, smilPar.Id);
            Assert.Null(smilPar.EpubTypes);
            SmilComparers.CompareSmilTexts(Text, smilPar.Text);
            SmilComparers.CompareSmilAudios(Audio, smilPar.Audio);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if text parameter is null")]
        public void ConstructorWithNullTextTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SmilPar(ID, EpubTypes, null!, Audio));
        }

        [Fact(DisplayName = "Constructing a SmilPar instance with null audio parameter should succeed")]
        public void ConstructorWithNullAudioTest()
        {
            SmilPar smilPar = new(ID, EpubTypes, Text, null);
            Assert.Equal(ID, smilPar.Id);
            Assert.Equal(EpubTypes, smilPar.EpubTypes);
            SmilComparers.CompareSmilTexts(Text, smilPar.Text);
            Assert.Null(smilPar.Audio);
        }
    }
}
