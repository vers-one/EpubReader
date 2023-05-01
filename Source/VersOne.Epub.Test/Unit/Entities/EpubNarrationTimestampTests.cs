namespace VersOne.Epub.Test.Unit.Entities
{
    public class EpubNarrationTimestampTests
    {
        [Fact(DisplayName = "Constructing a EpubNarrationTimestamp instance should succeed")]
        public void ConstructorAndPropertiesTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.Equal(1, epubNarrationTimestamp.Hour);
            Assert.Equal(2, epubNarrationTimestamp.Minute);
            Assert.Equal(3, epubNarrationTimestamp.Second);
            Assert.Equal(4, epubNarrationTimestamp.Millisecond);
        }

        [Fact(DisplayName = "Constructing a EpubNarrationTimestamp instance with more than 24 hours should succeed")]
        public void MoreThan24HoursTimestampTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(25, 10, 20, 30);
            Assert.Equal(25, epubNarrationTimestamp.Hour);
            Assert.Equal(10, epubNarrationTimestamp.Minute);
            Assert.Equal(20, epubNarrationTimestamp.Second);
            Assert.Equal(30, epubNarrationTimestamp.Millisecond);
        }

        [Fact(DisplayName = "operator == test")]
        public void EqualsOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp == new EpubNarrationTimestamp(1, 2, 3, 4));
            Assert.False(epubNarrationTimestamp == new EpubNarrationTimestamp(1, 2, 3, 5));
        }

        [Fact(DisplayName = "operator != test")]
        public void NotEqualsOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp != new EpubNarrationTimestamp(1, 2, 3, 5));
            Assert.False(epubNarrationTimestamp != new EpubNarrationTimestamp(1, 2, 3, 4));
        }

        [Fact(DisplayName = "operator > test")]
        public void GreaterThanOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp > new EpubNarrationTimestamp(1, 2, 3, 3));
            Assert.False(epubNarrationTimestamp > new EpubNarrationTimestamp(1, 2, 3, 4));
        }

        [Fact(DisplayName = "operator >= test")]
        public void GreaterThanOrEqualsOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp >= new EpubNarrationTimestamp(1, 2, 3, 3));
            Assert.True(epubNarrationTimestamp >= new EpubNarrationTimestamp(1, 2, 3, 4));
            Assert.False(epubNarrationTimestamp >= new EpubNarrationTimestamp(1, 2, 3, 5));
        }

        [Fact(DisplayName = "operator < test")]
        public void LessThanOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp < new EpubNarrationTimestamp(1, 2, 3, 5));
            Assert.False(epubNarrationTimestamp < new EpubNarrationTimestamp(1, 2, 3, 4));
        }

        [Fact(DisplayName = "operator <= test")]
        public void LessThanOrEqualsOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.True(epubNarrationTimestamp <= new EpubNarrationTimestamp(1, 2, 3, 5));
            Assert.True(epubNarrationTimestamp <= new EpubNarrationTimestamp(1, 2, 3, 4));
            Assert.False(epubNarrationTimestamp <= new EpubNarrationTimestamp(1, 2, 3, 3));
        }

        [Fact(DisplayName = "operator - test")]
        public void SubtractionOperatorTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.Equal(new TimeSpan(0, 0, 1), epubNarrationTimestamp - new EpubNarrationTimestamp(1, 2, 2, 4));
        }

        [Fact(DisplayName = "CompareTo methods test")]
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

        [Fact(DisplayName = "Equals methods test")]
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

        [Fact(DisplayName = "GetHashCode method test")]
        public void GetHashCodeTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.Equal(new TimeSpan(0, 1, 2, 3, 4).GetHashCode(), epubNarrationTimestamp.GetHashCode());
        }

        [Fact(DisplayName = "ToString method test")]
        public void ToStringTest()
        {
            Assert.Equal("1:02:03.004", new EpubNarrationTimestamp(1, 2, 3, 4).ToString());
            Assert.Equal("1:02:03", new EpubNarrationTimestamp(1, 2, 3, 0).ToString());
            Assert.Equal("25:10:20.030", new EpubNarrationTimestamp(25, 10, 20, 30).ToString());
        }

        [Fact(DisplayName = "ToTimeSpan method test")]
        public void ToTimeSpanTest()
        {
            EpubNarrationTimestamp epubNarrationTimestamp = new(1, 2, 3, 4);
            Assert.Equal(new TimeSpan(0, 1, 2, 3, 4), epubNarrationTimestamp.ToTimeSpan());
        }
    }
}
