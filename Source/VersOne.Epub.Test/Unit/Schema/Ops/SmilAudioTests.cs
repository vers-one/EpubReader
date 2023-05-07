using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Ops
{
    public class SmilAudioTests
    {
        private const string ID = "id";
        private const string SRC = "audio.mp3";
        private const string CLIP_BEGIN = "0s";
        private const string CLIP_END = "10s";

        [Fact(DisplayName = "Constructing a SmilAudio instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            SmilAudio smilAudio = new(ID, SRC, CLIP_BEGIN, CLIP_END);
            Assert.Equal(ID, smilAudio.Id);
            Assert.Equal(SRC, smilAudio.Src);
            Assert.Equal(CLIP_BEGIN, smilAudio.ClipBegin);
            Assert.Equal(CLIP_END, smilAudio.ClipEnd);
        }

        [Fact(DisplayName = "Constructing a SmilAudio instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            SmilAudio smilAudio = new(null, SRC, CLIP_BEGIN, CLIP_END);
            Assert.Null(smilAudio.Id);
            Assert.Equal(SRC, smilAudio.Src);
            Assert.Equal(CLIP_BEGIN, smilAudio.ClipBegin);
            Assert.Equal(CLIP_END, smilAudio.ClipEnd);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if src parameter is null")]
        public void ConstructorWithNullSrcTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SmilAudio(ID, null!, CLIP_BEGIN, CLIP_END));
        }

        [Fact(DisplayName = "Constructing a SmilAudio instance with null clipBegin parameter should succeed")]
        public void ConstructorWithNullClipBeginTest()
        {
            SmilAudio smilAudio = new(ID, SRC, null, CLIP_END);
            Assert.Equal(ID, smilAudio.Id);
            Assert.Equal(SRC, smilAudio.Src);
            Assert.Null(smilAudio.ClipBegin);
            Assert.Equal(CLIP_END, smilAudio.ClipEnd);
        }

        [Fact(DisplayName = "Constructing a SmilAudio instance with null clipEnd parameter should succeed")]
        public void ConstructorWithNullClipEndTest()
        {
            SmilAudio smilAudio = new(ID, SRC, CLIP_BEGIN, null);
            Assert.Equal(ID, smilAudio.Id);
            Assert.Equal(SRC, smilAudio.Src);
            Assert.Equal(CLIP_BEGIN, smilAudio.ClipBegin);
            Assert.Null(smilAudio.ClipEnd);
        }
    }
}
