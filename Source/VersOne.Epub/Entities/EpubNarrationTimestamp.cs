using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents a timestamp of the beginning or the end of the narration audio clip.
    /// </summary>
    public readonly struct EpubNarrationTimestamp : IComparable, IComparable<EpubNarrationTimestamp>, IEquatable<EpubNarrationTimestamp>
    {
        private readonly TimeSpan timeSpan;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubNarrationTimestamp" /> structure.
        /// </summary>
        /// <param name="hour">The hours part of the timestamp.</param>
        /// <param name="minute">The minutes part of the timestamp.</param>
        /// <param name="second">The seconds part of the timestamp.</param>
        /// <param name="millisecond">The milliseconds part of the timestamp.</param>
        public EpubNarrationTimestamp(int hour, int minute, int second, int millisecond)
        {
            timeSpan = new TimeSpan(0, hour, minute, second, millisecond);
        }

        /// <summary>
        /// Gets the hours part of the timestamp.
        /// </summary>
        public int Hour => (timeSpan.Days * 24) + timeSpan.Hours;

        /// <summary>
        /// Gets the minutes part of the timestamp.
        /// </summary>
        public int Minute => timeSpan.Minutes;

        /// <summary>
        /// Gets the seconds part of the timestamp.
        /// </summary>
        public int Second => timeSpan.Seconds;

        /// <summary>
        /// Gets the milliseconds part of the timestamp.
        /// </summary>
        public int Millisecond => timeSpan.Milliseconds;

        /// <summary>
        /// Determines whether two specified instances of <see cref="EpubNarrationTimestamp" /> are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left" /> and <paramref name="right" /> represent the same timestamp; otherwise, false.</returns>
        public static bool operator ==(EpubNarrationTimestamp left, EpubNarrationTimestamp right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="EpubNarrationTimestamp" /> are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left" /> and <paramref name="right" /> do not represent the same timestamp; otherwise, false.</returns>
        public static bool operator !=(EpubNarrationTimestamp left, EpubNarrationTimestamp right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Determines whether one specified <see cref="EpubNarrationTimestamp" /> is later than another specified <see cref="EpubNarrationTimestamp" />.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left" /> is later than <paramref name="right" />; otherwise, false.</returns>
        public static bool operator >(EpubNarrationTimestamp left, EpubNarrationTimestamp right)
        {
            return left.timeSpan > right.timeSpan;
        }

        /// <summary>
        /// Determines whether one specified <see cref="EpubNarrationTimestamp" /> is the same or later than another specified <see cref="EpubNarrationTimestamp" />.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left" /> is the same or later than <paramref name="right" />; otherwise, false.</returns>
        public static bool operator >=(EpubNarrationTimestamp left, EpubNarrationTimestamp right)
        {
            return left.timeSpan >= right.timeSpan;
        }

        /// <summary>
        /// Determines whether one specified <see cref="EpubNarrationTimestamp" /> is earlier than another specified <see cref="EpubNarrationTimestamp" />.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left" /> is earlier than <paramref name="right" />; otherwise, false.</returns>
        public static bool operator <(EpubNarrationTimestamp left, EpubNarrationTimestamp right)
        {
            return left.timeSpan < right.timeSpan;
        }

        /// <summary>
        /// Determines whether one specified <see cref="EpubNarrationTimestamp" /> is the same or earlier than another specified <see cref="EpubNarrationTimestamp" />.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left" /> is the same or earlier than <paramref name="right" />; otherwise, false.</returns>
        public static bool operator <=(EpubNarrationTimestamp left, EpubNarrationTimestamp right)
        {
            return left.timeSpan <= right.timeSpan;
        }

        /// <summary>
        /// Subtracts a specified <see cref="EpubNarrationTimestamp" /> from another specified <see cref="EpubNarrationTimestamp" /> and returns a time interval.
        /// </summary>
        /// <param name="first">The timestamp value to subtract from (the minuend).</param>
        /// <param name="second">The timestamp value to subtract (the subtrahend).</param>
        /// <returns>The time interval between <paramref name="first" /> and <paramref name="second" />.</returns>
        public static TimeSpan operator -(EpubNarrationTimestamp first, EpubNarrationTimestamp second)
        {
            return first.timeSpan - second.timeSpan;
        }

        /// <summary>
        /// Compares the value of this instance to a specified object that contains a specified <see cref="EpubNarrationTimestamp" /> value,
        /// and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="EpubNarrationTimestamp" /> value.
        /// </summary>
        /// <param name="obj">A boxed object to compare, or null.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the <paramref name="obj" /> parameter.
        /// Less than zero if this instance is earlier than <paramref name="obj" />.
        /// Zero if this instance is the same as <paramref name="obj" />.
        /// Greater than zero if this instance is later than <paramref name="obj" />.
        /// </returns>
        /// <exception cref="ArgumentException">The type of the <paramref name="obj" /> parameter is not <see cref="EpubNarrationTimestamp" />.</exception>
        public int CompareTo(object? obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (obj is not EpubNarrationTimestamp epubNarrationTimestamp)
            {
                throw new ArgumentException($"The type of the \"{nameof(obj)}\" parameter must be {nameof(EpubNarrationTimestamp)}.", nameof(obj));
            }
            return CompareTo(epubNarrationTimestamp);
        }

        /// <summary>
        /// Compares the value of this instance to a specified <see cref="EpubNarrationTimestamp" /> value and returns an integer that indicates
        /// whether this instance is earlier than, the same as, or later than the specified <see cref="EpubNarrationTimestamp" /> value.
        /// </summary>
        /// <param name="other">The object to compare to the current instance.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the <paramref name="other" /> parameter.
        /// Less than zero if this instance is earlier than <paramref name="other" />.
        /// Zero if this instance is the same as <paramref name="other" />.
        /// Greater than zero if this instance is later than <paramref name="other" />.
        /// </returns>
        public int CompareTo(EpubNarrationTimestamp other)
        {
            return timeSpan.CompareTo(other.timeSpan);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj" /> is an instance of <see cref="EpubNarrationTimestamp" /> and equals the value of this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return obj is EpubNarrationTimestamp epubNarrationTimestamp && Equals(epubNarrationTimestamp);
        }

        /// <summary>
        /// Returns a value indicating whether the value of this instance is equal to the value of the specified <see cref="EpubNarrationTimestamp" /> instance.
        /// </summary>
        /// <param name="other">The object to compare to this instance.</param>
        /// <returns><c>true</c> if the <paramref name="other" /> parameter equals the value of this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(EpubNarrationTimestamp other)
        {
            return timeSpan.Equals(other.timeSpan);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return timeSpan.GetHashCode();
        }

        /// <summary>
        /// Converts the value of the current <see cref="EpubNarrationTimestamp" /> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the value of the current <see cref="EpubNarrationTimestamp" /> object in <c>h:mm:ss[.fff]</c> format (e.g. <c>1:23:45.678</c>).</returns>
        public override string ToString()
        {
            return $"{Hour}:{Minute:D2}:{Second:D2}{(Millisecond > 0 ? ("." + Millisecond.ToString("D3")) : String.Empty)}";
        }

        /// <summary>
        /// Converts the value of the current <see cref="EpubNarrationTimestamp" /> object to its equivalent <see cref="TimeSpan" /> representation.
        /// </summary>
        /// <returns>A <see cref="TimeSpan" /> representation of the value of the current <see cref="EpubNarrationTimestamp" /> object.</returns>
        public TimeSpan ToTimeSpan()
        {
            return timeSpan;
        }
    }
}
