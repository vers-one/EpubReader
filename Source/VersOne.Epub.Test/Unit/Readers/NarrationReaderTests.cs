namespace VersOne.Epub.Test.Unit.Readers
{
    // Temporary test stubs for narration entities
    public class NarrationReaderTests
    {
        [Fact(DisplayName = "Temporary test stub for the EpubNarration class")]
        public void EpubNarrationTest()
        {
            EpubNarration epubNarration = new();
            Assert.NotNull(epubNarration.Phrases);
            _ = new EpubNarration(new List<EpubNarrationPhrase>());
        }

        [Fact(DisplayName = "Temporary test stub for the EpubNarrationPhrase class")]
        public void EpubNarrationPhraseTest()
        {
            EpubNarrationPhrase epubNarrationPhrase =
                new(new EpubLocalTextContentFile("key", EpubContentType.XHTML_1_1, "text/html", String.Empty, String.Empty), null, null, null, null);
            Assert.NotNull(epubNarrationPhrase.TextContentFile);
            Assert.Null(epubNarrationPhrase.TextContentAnchor);
            Assert.Null(epubNarrationPhrase.AudioContentFile);
            Assert.Null(epubNarrationPhrase.AudioContentBegin);
            Assert.Null(epubNarrationPhrase.AudioContentEnd);
            Assert.Throws<ArgumentNullException>(() => new EpubNarrationPhrase(null!, null, null, null, null));
        }
    }
}
