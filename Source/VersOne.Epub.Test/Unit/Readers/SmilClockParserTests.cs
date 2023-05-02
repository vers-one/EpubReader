using VersOne.Epub.Readers;
using VersOne.Epub.Test.Comparers;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class SmilClockParserTests
    {
        [Fact(DisplayName = "Invalid SMIL clock values parsing test")]
        public void InvalidClockValuesTest()
        {
            TestInvalidValue(null!);
            TestInvalidValue("");
            TestInvalidValue(" ");
        }

        [Fact(DisplayName = "Valid SMIL full clock values parsing test")]
        public void ValidFullClockValuesTest()
        {
            TestValidValue("02:30:03", new EpubNarrationTimestamp(2, 30, 3, 0));
            TestValidValue("50:00:10.25", new EpubNarrationTimestamp(50, 0, 10, 250));
            TestValidValue("01:02:03.999", new EpubNarrationTimestamp(1, 2, 3, 999));
            TestValidValue("01:02:03.9991", new EpubNarrationTimestamp(1, 2, 3, 999));
            TestValidValue(" 1:02:03.4", new EpubNarrationTimestamp(1, 2, 3, 400));
            TestValidValue("1:02:03.4 ", new EpubNarrationTimestamp(1, 2, 3, 400));
            TestValidValue(" 1:02:03.4 ", new EpubNarrationTimestamp(1, 2, 3, 400));
        }

        [Fact(DisplayName = "Invalid SMIL full clock values parsing test")]
        public void InvalidFullClockValuesTest()
        {
            TestInvalidValue("1:02:03:04");
            TestInvalidValue("1:02:03:04.5");
            TestInvalidValue(":02:03");
            TestInvalidValue("1:02:");
            TestInvalidValue("a:02:03");
            TestInvalidValue("-1:02:03");
            TestInvalidValue("1:60:03");
            TestInvalidValue("1:a:03");
            TestInvalidValue("1:-1:03");
            TestInvalidValue("1:02:60");
            TestInvalidValue("1:02:a");
            TestInvalidValue("1:02:-1");
            TestInvalidValue("1:02:03.a");
            TestInvalidValue("1:02:03.-1");
            TestInvalidValue("1:02:03,1");
        }

        [Fact(DisplayName = "Valid SMIL partial clock values parsing test")]
        public void ValidPartialClockValuesTest()
        {
            TestValidValue("02:33", new EpubNarrationTimestamp(0, 2, 33, 0));
            TestValidValue("00:10.5", new EpubNarrationTimestamp(0, 0, 10, 500));
            TestValidValue("1:02.999", new EpubNarrationTimestamp(0, 1, 2, 999));
            TestValidValue("1:02.9991", new EpubNarrationTimestamp(0, 1, 2, 999));
            TestValidValue(" 1:02.3", new EpubNarrationTimestamp(0, 1, 2, 300));
            TestValidValue("1:02.3 ", new EpubNarrationTimestamp(0, 1, 2, 300));
            TestValidValue(" 1:02.3 ", new EpubNarrationTimestamp(0, 1, 2, 300));
        }

        [Fact(DisplayName = "Invalid SMIL partial clock values parsing test")]
        public void InvalidPartialClockValuesTest()
        {
            TestInvalidValue("1:");
            TestInvalidValue(":01");
            TestInvalidValue("60:02");
            TestInvalidValue("a:02");
            TestInvalidValue("-1:02");
            TestInvalidValue("1:60");
            TestInvalidValue("1:a");
            TestInvalidValue("1:-1");
            TestInvalidValue("1:02.a");
            TestInvalidValue("1:02.-1");
            TestInvalidValue("1:02,1");
        }

        [Fact(DisplayName = "Valid SMIL timecount values parsing test")]
        public void ValidTimecountValuesTest()
        {
            TestValidValue("3h", new EpubNarrationTimestamp(3, 0, 0, 0));
            TestValidValue("45min", new EpubNarrationTimestamp(0, 45, 0, 0));
            TestValidValue("30s", new EpubNarrationTimestamp(0, 0, 30, 0));
            TestValidValue("5ms", new EpubNarrationTimestamp(0, 0, 0, 5));
            TestValidValue("12", new EpubNarrationTimestamp(0, 0, 12, 0));
            TestValidValue("3.2h", new EpubNarrationTimestamp(3, 12, 0, 0));
            TestValidValue("45.5min", new EpubNarrationTimestamp(0, 45, 30, 0));
            TestValidValue("30.1s", new EpubNarrationTimestamp(0, 0, 30, 100));
            TestValidValue("12.467", new EpubNarrationTimestamp(0, 0, 12, 467));
            TestValidValue("1.999", new EpubNarrationTimestamp(0, 0, 1, 999));
            TestValidValue("1.9991", new EpubNarrationTimestamp(0, 0, 1, 999));
            TestValidValue("300h", new EpubNarrationTimestamp(300, 0, 0, 0));
            TestValidValue("3.3333333h", new EpubNarrationTimestamp(3, 19, 59, 999));
            TestValidValue("130.151234min", new EpubNarrationTimestamp(2, 10, 9, 74));
            TestValidValue("3725.1234s", new EpubNarrationTimestamp(1, 2, 5, 123));
            TestValidValue("11531567.8ms", new EpubNarrationTimestamp(3, 12, 11, 567));
            TestValidValue(" 10ms", new EpubNarrationTimestamp(0, 0, 0, 10));
            TestValidValue("10ms ", new EpubNarrationTimestamp(0, 0, 0, 10));
            TestValidValue(" 10ms ", new EpubNarrationTimestamp(0, 0, 0, 10));
        }

        [Fact(DisplayName = "Invalid SMIL timecount values parsing test")]
        public void InvalidTimecountValuesTest()
        {
            TestInvalidValue("1m");
            TestInvalidValue("1H");
            TestInvalidValue("1 h");
            TestInvalidValue("ah");
            TestInvalidValue("-1h");
            TestInvalidValue("-1.5h");
            TestInvalidValue($"{(decimal)Int32.MaxValue * 60 * 60 * 1000 + 1}ms");
        }

        private static void TestValidValue(string value, EpubNarrationTimestamp expectedResult)
        {
            Assert.True(SmilClockParser.TryParse(value, out EpubNarrationTimestamp actualResult));
            EpubNarrationComparers.CompareEpubNarrationTimestamps(expectedResult, actualResult);
        }

        private static void TestInvalidValue(string value)
        {
            Assert.False(SmilClockParser.TryParse(value, out _));
        }
    }
}
