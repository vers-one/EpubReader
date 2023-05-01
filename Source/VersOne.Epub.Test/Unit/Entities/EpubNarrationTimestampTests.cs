namespace VersOne.Epub.Test.Unit.Entities
{
    public class EpubNarrationTimestampTests
    {
        [Fact]
        public void ConstructorAndPropertiesTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.Equal(1, epubNarrationTimestamp.Hour);
            Assert.Equal(2, epubNarrationTimestamp.Minute);
            Assert.Equal(3, epubNarrationTimestamp.Second);
            Assert.Equal(4, epubNarrationTimestamp.Millisecond);
        }

        [Fact]
        public void MoreThan24HoursTimestampTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(25, 10, 20, 30);
            Assert.Equal(25, epubNarrationTimestamp.Hour);
            Assert.Equal(10, epubNarrationTimestamp.Minute);
            Assert.Equal(20, epubNarrationTimestamp.Second);
            Assert.Equal(30, epubNarrationTimestamp.Millisecond);
        }

        [Fact]
        public void EqualsOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp == new EpubNarrationTimestamp(1, 2, 3, 4));
            Assert.False(epubNarrationTimestamp == new EpubNarrationTimestamp(1, 2, 3, 5));
        }

        [Fact]
        public void NotEqualsOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp != new EpubNarrationTimestamp(1, 2, 3, 5));
            Assert.False(epubNarrationTimestamp != new EpubNarrationTimestamp(1, 2, 3, 4));
        }

        [Fact]
        public void GreaterThanOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp > new EpubNarrationTimestamp(1, 2, 3, 3));
            Assert.False(epubNarrationTimestamp > new EpubNarrationTimestamp(1, 2, 3, 4));
        }

        [Fact]
        public void GreaterThanOrEqualsOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp >= new EpubNarrationTimestamp(1, 2, 3, 3));
            Assert.True(epubNarrationTimestamp >= new EpubNarrationTimestamp(1, 2, 3, 4));
            Assert.False(epubNarrationTimestamp >= new EpubNarrationTimestamp(1, 2, 3, 5));
        }

        [Fact]
        public void LessThanOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp < new EpubNarrationTimestamp(1, 2, 3, 5));
            Assert.False(epubNarrationTimestamp < new EpubNarrationTimestamp(1, 2, 3, 4));
        }

        [Fact]
        public void LessThanOrEqualsOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp <= new EpubNarrationTimestamp(1, 2, 3, 5));
            Assert.True(epubNarrationTimestamp <= new EpubNarrationTimestamp(1, 2, 3, 4));
            Assert.False(epubNarrationTimestamp <= new EpubNarrationTimestamp(1, 2, 3, 3));
        }

        [Fact]
        public void SubtractionOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.Equal(new TimeSpan(0, 0, 1), epubNarrationTimestamp - new EpubNarrationTimestamp(1, 2, 2, 4));
        }

        [Fact]
        public void CompareToTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.Equal(1, epubNarrationTimestamp.CompareTo(null));
            Assert.Throws<ArgumentException>(() => epubNarrationTimestamp.CompareTo(new object()));
            Assert.Equal(1, epubNarrationTimestamp.CompareTo((object)new EpubNarrationTimestamp(1, 2, 3, 3)));
            Assert.Equal(1, epubNarrationTimestamp.CompareTo(new EpubNarrationTimestamp(1, 2, 3, 3)));
            Assert.Equal(0, epubNarrationTimestamp.CompareTo((object)new EpubNarrationTimestamp(1, 2, 3, 4)));
            Assert.Equal(0, epubNarrationTimestamp.CompareTo(new EpubNarrationTimestamp(1, 2, 3, 4)));
            Assert.Equal(-1, epubNarrationTimestamp.CompareTo((object)new EpubNarrationTimestamp(1, 2, 3, 5)));
            Assert.Equal(-1, epubNarrationTimestamp.CompareTo(new EpubNarrationTimestamp(1, 2, 3, 5)));
        }

        [Fact]
        public void EqualsTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.False(epubNarrationTimestamp.Equals(null));
            Assert.False(epubNarrationTimestamp.Equals(new object()));
            Assert.True(epubNarrationTimestamp.Equals((object)new EpubNarrationTimestamp(1, 2, 3, 4)));
            Assert.True(epubNarrationTimestamp.Equals(new EpubNarrationTimestamp(1, 2, 3, 4)));
            Assert.False(epubNarrationTimestamp.Equals((object)new EpubNarrationTimestamp(1, 2, 3, 3)));
            Assert.False(epubNarrationTimestamp.Equals(new EpubNarrationTimestamp(1, 2, 3, 3)));
        }

        [Fact]
        public void GetHashCodeTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.Equal(new TimeSpan(0, 1, 2, 3, 4).GetHashCode(), epubNarrationTimestamp.GetHashCode());
        }

        [Fact]
        public void ToStringTest()
        {
            Assert.Equal("1:02:03.004", new EpubNarrationTimestamp(1, 2, 3, 4).ToString());
            Assert.Equal("1:02:03", new EpubNarrationTimestamp(1, 2, 3, 0).ToString());
            Assert.Equal("25:10:20.030", new EpubNarrationTimestamp(25, 10, 20, 30).ToString());
        }

        [Fact]
        public void ToTimeSpanTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.Equal(new TimeSpan(0, 1, 2, 3, 4), epubNarrationTimestamp.ToTimeSpan());
        }
    }
}
