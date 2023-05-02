using System;
using System.Globalization;

namespace VersOne.Epub.Readers
{
    internal static class SmilClockParser
    {
        private const int MILLISECONDS_IN_SECOND = 1000;
        private const int MILLISECONDS_IN_MINUTE = 60 * MILLISECONDS_IN_SECOND;
        private const int MILLISECONDS_IN_HOUR = 60 * MILLISECONDS_IN_MINUTE;
        private const decimal MAX_TIMECOUNT = (decimal)Int32.MaxValue * MILLISECONDS_IN_HOUR;

        public static bool TryParse(string smilClock, out EpubNarrationTimestamp result)
        {
            if (string.IsNullOrWhiteSpace(smilClock))
            {
                result = default;
                return false;
            }
            smilClock = smilClock.Trim();
            if (smilClock.Contains(":"))
            {
                return TryParseClockValue(smilClock, out result);
            }
            else
            {
                return TryParseTimecountValue(smilClock, out result);
            }
        }

        private static bool TryParseClockValue(string clockValue, out EpubNarrationTimestamp result)
        {
            string[] parts = clockValue.Split(':');
            if (parts.Length > 3)
            {
                result = default;
                return false;
            }
            int hour;
            int currentPartIndex;
            if (parts.Length == 3)
            {
                if (!Int32.TryParse(parts[0], out hour) || hour < 0)
                {
                    result = default;
                    return false;
                }
                currentPartIndex = 1;
            }
            else
            {
                hour = 0;
                currentPartIndex = 0;
            }
            if (!Int32.TryParse(parts[currentPartIndex], out int minute) || minute < 0 || minute >= 60)
            {
                result = default;
                return false;
            }
            currentPartIndex++;
            if (!Decimal.TryParse(parts[currentPartIndex], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal secondWithFraction) ||
                secondWithFraction < 0 || secondWithFraction >= 60)
            {
                result = default;
                return false;
            }
            int second = (int)Math.Truncate(secondWithFraction);
            int millisecond = (int)Math.Truncate((secondWithFraction - second) * 1000);
            result = new EpubNarrationTimestamp(hour, minute, second, millisecond);
            return true;
        }

        private static bool TryParseTimecountValue(string timecountValue, out EpubNarrationTimestamp result)
        {
            bool hasMetric = false;
            int metricStartIndex = -1;
            for (int i = 0; i < timecountValue.Length; i++)
            {
                char c = timecountValue[i];
                if (!Char.IsDigit(c) && c != '.')
                {
                    hasMetric = true;
                    metricStartIndex = i;
                    break;
                }
            }
            string timecountWithFractionString = hasMetric ? timecountValue.Substring(0, metricStartIndex) : timecountValue;
            if (!Decimal.TryParse(timecountWithFractionString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal timecountWithFraction) ||
                timecountWithFraction > MAX_TIMECOUNT)
            {
                result = default;
                return false;
            }
            int metricMultiplier;
            if (hasMetric)
            {
                string metric = timecountValue.Substring(metricStartIndex);
                switch (metric)
                {
                    case "h":
                        metricMultiplier = MILLISECONDS_IN_HOUR;
                        break;
                    case "min":
                        metricMultiplier = MILLISECONDS_IN_MINUTE;
                        break;
                    case "s":
                        metricMultiplier = MILLISECONDS_IN_SECOND;
                        break;
                    case "ms":
                        metricMultiplier = 1;
                        break;
                    default:
                        result = default;
                        return false;
                }
            }
            else
            {
                metricMultiplier = MILLISECONDS_IN_SECOND;
            }
            decimal milliseconds = Math.Truncate(timecountWithFraction * metricMultiplier);
            result = ConvertMillisecondsToEpubNarrationTimestamp(milliseconds);
            return true;
        }

        private static EpubNarrationTimestamp ConvertMillisecondsToEpubNarrationTimestamp(decimal milliseconds)
        {
            int millisecond = (int)(milliseconds % MILLISECONDS_IN_SECOND);
            int second = (int)(Math.Truncate(milliseconds / MILLISECONDS_IN_SECOND) % 60);
            int minute = (int)(Math.Truncate(milliseconds / MILLISECONDS_IN_MINUTE) % 60);
            int hour = (int)Math.Truncate(milliseconds / MILLISECONDS_IN_HOUR);
            return new(hour, minute, second, millisecond);
        }
    }
}
