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

        [Fact(DisplayName = "Temporary test stub for the EpubNarrationTimestamp class")]
        public void EpubNarrationTimestampTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.Equal(1, epubNarrationTimestamp.Hour);
            Assert.Equal(2, epubNarrationTimestamp.Minute);
            Assert.Equal(3, epubNarrationTimestamp.Second);
            Assert.Equal(4, epubNarrationTimestamp.Millisecond);
            Assert.True(epubNarrationTimestamp == new EpubNarrationTimestamp(1, 2, 3, 4));
            Assert.True(epubNarrationTimestamp != new EpubNarrationTimestamp(1, 2, 3, 5));
            Assert.True(epubNarrationTimestamp > new EpubNarrationTimestamp(1, 2, 3, 3));
            Assert.True(epubNarrationTimestamp >= new EpubNarrationTimestamp(1, 2, 3, 3));
            Assert.True(epubNarrationTimestamp < new EpubNarrationTimestamp(1, 2, 3, 5));
            Assert.True(epubNarrationTimestamp <= new EpubNarrationTimestamp(1, 2, 3, 5));
            Assert.Equal(new TimeSpan(0, 0, 1), epubNarrationTimestamp - new EpubNarrationTimestamp(1, 2, 2, 4));
            Assert.Equal(1, epubNarrationTimestamp.CompareTo(null));
            Assert.Throws<ArgumentException>(() =>epubNarrationTimestamp.CompareTo(new object()));
            Assert.Equal(1, epubNarrationTimestamp.CompareTo((object)new EpubNarrationTimestamp(1, 2, 3, 3)));
            Assert.Equal(1, epubNarrationTimestamp.CompareTo(new EpubNarrationTimestamp(1, 2, 3, 3)));
            Assert.False(epubNarrationTimestamp.Equals(null));
            Assert.True(epubNarrationTimestamp.Equals((object)new EpubNarrationTimestamp(1, 2, 3, 4)));
            Assert.True(epubNarrationTimestamp.Equals(new EpubNarrationTimestamp(1, 2, 3, 4)));
            Assert.Equal(-1424665656, epubNarrationTimestamp.GetHashCode());
            Assert.Equal("1:02:03.004", epubNarrationTimestamp.ToString());
            Assert.Equal("1:02:03", new EpubNarrationTimestamp(1, 2, 3, 0).ToString());
            Assert.Equal(new TimeSpan(0, 1, 2, 3, 4), epubNarrationTimestamp.ToTimeSpan());
        }
    }
}
