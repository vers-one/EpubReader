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
    }
}
