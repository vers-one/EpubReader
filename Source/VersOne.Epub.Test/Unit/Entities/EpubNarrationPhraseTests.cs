using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.TestData;

namespace VersOne.Epub.Test.Unit.Entities
{
    public class EpubNarrationPhraseTests
    {
        private const string TEXT_CONTENT_ANCHOR = "paragraph1";

        private static EpubNarrationTimestamp AudioContentBegin => new(0, 24, 18, 123);
        private static EpubNarrationTimestamp AudioContentEnd => new(0, 24, 28, 671);

        [Fact(DisplayName = "Constructing a EpubNarrationPhrase instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubNarrationPhrase epubNarrationPhrase = new(TestEpubContent.Chapter1File, TEXT_CONTENT_ANCHOR, TestEpubContent.Audio1File, AudioContentBegin, AudioContentEnd);
            EpubContentComparer.CompareEpubLocalTextContentFiles(TestEpubContent.Chapter1File, epubNarrationPhrase.TextContentFile);
            Assert.Equal(TEXT_CONTENT_ANCHOR, epubNarrationPhrase.TextContentAnchor);
            EpubContentComparer.CompareEpubLocalByteContentFiles(TestEpubContent.Audio1File, epubNarrationPhrase.AudioContentFile as EpubLocalByteContentFile);
            EpubNarrationComparers.CompareEpubNarrationTimestamps(AudioContentBegin, epubNarrationPhrase.AudioContentBegin);
            EpubNarrationComparers.CompareEpubNarrationTimestamps(AudioContentEnd, epubNarrationPhrase.AudioContentEnd);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if textContentFile parameter is null")]
        public void ConstructorWithNullTextContentFileTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubNarrationPhrase(null!, TEXT_CONTENT_ANCHOR, TestEpubContent.Audio1File, AudioContentBegin, AudioContentEnd));
        }

        [Fact(DisplayName = "Constructing a EpubNarrationPhrase instance with null textContentAnchor parameter should succeed")]
        public void ConstructorWithNullTextContentAnchorParameterTest()
        {
            EpubNarrationPhrase epubNarrationPhrase = new(TestEpubContent.Chapter1File, null, TestEpubContent.Audio1File, AudioContentBegin, AudioContentEnd);
            EpubContentComparer.CompareEpubLocalTextContentFiles(TestEpubContent.Chapter1File, epubNarrationPhrase.TextContentFile);
            Assert.Null(epubNarrationPhrase.TextContentAnchor);
            EpubContentComparer.CompareEpubLocalByteContentFiles(TestEpubContent.Audio1File, epubNarrationPhrase.AudioContentFile as EpubLocalByteContentFile);
            EpubNarrationComparers.CompareEpubNarrationTimestamps(AudioContentBegin, epubNarrationPhrase.AudioContentBegin);
            EpubNarrationComparers.CompareEpubNarrationTimestamps(AudioContentEnd, epubNarrationPhrase.AudioContentEnd);
        }

        [Fact(DisplayName = "Constructing a EpubNarrationPhrase instance with null audioContentFile parameter should succeed")]
        public void ConstructorWithNullAudioContentFileParameterTest()
        {
            EpubNarrationPhrase epubNarrationPhrase = new(TestEpubContent.Chapter1File, TEXT_CONTENT_ANCHOR, null, AudioContentBegin, AudioContentEnd);
            EpubContentComparer.CompareEpubLocalTextContentFiles(TestEpubContent.Chapter1File, epubNarrationPhrase.TextContentFile);
            Assert.Equal(TEXT_CONTENT_ANCHOR, epubNarrationPhrase.TextContentAnchor);
            Assert.Null(epubNarrationPhrase.AudioContentFile);
            EpubNarrationComparers.CompareEpubNarrationTimestamps(AudioContentBegin, epubNarrationPhrase.AudioContentBegin);
            EpubNarrationComparers.CompareEpubNarrationTimestamps(AudioContentEnd, epubNarrationPhrase.AudioContentEnd);
        }

        [Fact(DisplayName = "Constructing a EpubNarrationPhrase instance with null audioContentBegin parameter should succeed")]
        public void ConstructorWithNullAudioContentBeginParameterTest()
        {
            EpubNarrationPhrase epubNarrationPhrase = new(TestEpubContent.Chapter1File, TEXT_CONTENT_ANCHOR, TestEpubContent.Audio1File, null, AudioContentEnd);
            EpubContentComparer.CompareEpubLocalTextContentFiles(TestEpubContent.Chapter1File, epubNarrationPhrase.TextContentFile);
            Assert.Equal(TEXT_CONTENT_ANCHOR, epubNarrationPhrase.TextContentAnchor);
            EpubContentComparer.CompareEpubLocalByteContentFiles(TestEpubContent.Audio1File, epubNarrationPhrase.AudioContentFile as EpubLocalByteContentFile);
            Assert.Null(epubNarrationPhrase.AudioContentBegin);
            EpubNarrationComparers.CompareEpubNarrationTimestamps(AudioContentEnd, epubNarrationPhrase.AudioContentEnd);
        }

        [Fact(DisplayName = "Constructing a EpubNarrationPhrase instance with null audioContentEnd parameter should succeed")]
        public void ConstructorWithNullAudioContentEndParameterTest()
        {
            EpubNarrationPhrase epubNarrationPhrase = new(TestEpubContent.Chapter1File, TEXT_CONTENT_ANCHOR, TestEpubContent.Audio1File, AudioContentBegin, null);
            EpubContentComparer.CompareEpubLocalTextContentFiles(TestEpubContent.Chapter1File, epubNarrationPhrase.TextContentFile);
            Assert.Equal(TEXT_CONTENT_ANCHOR, epubNarrationPhrase.TextContentAnchor);
            EpubContentComparer.CompareEpubLocalByteContentFiles(TestEpubContent.Audio1File, epubNarrationPhrase.AudioContentFile as EpubLocalByteContentFile);
            EpubNarrationComparers.CompareEpubNarrationTimestamps(AudioContentBegin, epubNarrationPhrase.AudioContentBegin);
            Assert.Null(epubNarrationPhrase.AudioContentEnd);
        }
    }
}
