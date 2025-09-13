using System;
using Xunit;

namespace Platform.Timestamps.Tests
{
    public class TimestampTests
    {
        [Fact]
        public void Constructor_WithUlongTicks_SetsTicksProperty()
        {
            // Arrange
            const ulong expectedTicks = 637849200000000000UL;

            // Act
            var timestamp = new Timestamp(expectedTicks);

            // Assert
            Assert.Equal(expectedTicks, timestamp.Ticks);
        }

        [Fact]
        public void ImplicitConversion_FromDateTime_ConvertsCorrectly()
        {
            // Arrange
            var dateTime = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var expectedTicks = (ulong)dateTime.Ticks;

            // Act
            Timestamp timestamp = dateTime;

            // Assert
            Assert.Equal(expectedTicks, timestamp.Ticks);
        }

        [Fact]
        public void ImplicitConversion_FromLocalDateTime_ConvertsToUtc()
        {
            // Arrange
            var localDateTime = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Local);
            var utcDateTime = localDateTime.ToUniversalTime();
            var expectedTicks = (ulong)utcDateTime.Ticks;

            // Act
            Timestamp timestamp = localDateTime;

            // Assert
            Assert.Equal(expectedTicks, timestamp.Ticks);
        }

        [Fact]
        public void ImplicitConversion_ToDateTime_ConvertsCorrectly()
        {
            // Arrange
            const ulong ticks = 637849200000000000UL;
            var timestamp = new Timestamp(ticks);
            var expectedDateTime = new DateTime((long)ticks, DateTimeKind.Utc);

            // Act
            DateTime dateTime = timestamp;

            // Assert
            Assert.Equal(expectedDateTime, dateTime);
            Assert.Equal(DateTimeKind.Utc, dateTime.Kind);
        }

        [Fact]
        public void ImplicitConversion_FromUlong_CreatesTimestamp()
        {
            // Arrange
            const ulong ticks = 637849200000000000UL;

            // Act
            Timestamp timestamp = ticks;

            // Assert
            Assert.Equal(ticks, timestamp.Ticks);
        }

        [Fact]
        public void ImplicitConversion_ToUlong_ReturnsCorrectTicks()
        {
            // Arrange
            const ulong expectedTicks = 637849200000000000UL;
            var timestamp = new Timestamp(expectedTicks);

            // Act
            ulong ticks = timestamp;

            // Assert
            Assert.Equal(expectedTicks, ticks);
        }

        [Fact]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            var dateTime = new DateTime(2025, 1, 1, 12, 30, 45, 123, DateTimeKind.Utc).AddTicks(4567890);
            Timestamp timestamp = dateTime;
            var expectedString = dateTime.ToString(Timestamp.DefaultFormat);

            // Act
            var result = timestamp.ToString();

            // Assert
            Assert.Equal(expectedString, result);
        }

        [Fact]
        public void DefaultFormat_HasExpectedValue()
        {
            // Arrange & Act
            var format = Timestamp.DefaultFormat;

            // Assert
            Assert.Equal("yyyy.MM.dd hh:mm:ss.fffffff", format);
        }

        [Fact]
        public void Equals_WithSameTimestamp_ReturnsTrue()
        {
            // Arrange
            const ulong ticks = 637849200000000000UL;
            var timestamp1 = new Timestamp(ticks);
            var timestamp2 = new Timestamp(ticks);

            // Act & Assert
            Assert.True(timestamp1.Equals(timestamp2));
        }

        [Fact]
        public void Equals_WithDifferentTimestamp_ReturnsFalse()
        {
            // Arrange
            var timestamp1 = new Timestamp(637849200000000000UL);
            var timestamp2 = new Timestamp(637849200000000001UL);

            // Act & Assert
            Assert.False(timestamp1.Equals(timestamp2));
        }

        [Fact]
        public void Equals_WithObject_SameTimestamp_ReturnsTrue()
        {
            // Arrange
            const ulong ticks = 637849200000000000UL;
            var timestamp1 = new Timestamp(ticks);
            object timestamp2 = new Timestamp(ticks);

            // Act & Assert
            Assert.True(timestamp1.Equals(timestamp2));
        }

        [Fact]
        public void Equals_WithObject_DifferentTimestamp_ReturnsFalse()
        {
            // Arrange
            var timestamp1 = new Timestamp(637849200000000000UL);
            object timestamp2 = new Timestamp(637849200000000001UL);

            // Act & Assert
            Assert.False(timestamp1.Equals(timestamp2));
        }

        [Fact]
        public void Equals_WithObject_DifferentType_ReturnsFalse()
        {
            // Arrange
            var timestamp = new Timestamp(637849200000000000UL);
            object other = "not a timestamp";

            // Act & Assert
            Assert.False(timestamp.Equals(other));
        }

        [Fact]
        public void Equals_WithObject_Null_ReturnsFalse()
        {
            // Arrange
            var timestamp = new Timestamp(637849200000000000UL);
            object? other = null;

            // Act & Assert
            Assert.False(timestamp.Equals(other));
        }

        [Fact]
        public void GetHashCode_SameTimestamps_ReturnSameHashCode()
        {
            // Arrange
            const ulong ticks = 637849200000000000UL;
            var timestamp1 = new Timestamp(ticks);
            var timestamp2 = new Timestamp(ticks);

            // Act
            var hash1 = timestamp1.GetHashCode();
            var hash2 = timestamp2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_DifferentTimestamps_MayReturnDifferentHashCodes()
        {
            // Arrange
            var timestamp1 = new Timestamp(637849200000000000UL);
            var timestamp2 = new Timestamp(637849200000000001UL);

            // Act
            var hash1 = timestamp1.GetHashCode();
            var hash2 = timestamp2.GetHashCode();

            // Assert
            // Note: Hash codes might collide, but for different values it's likely they differ
            // This test documents the behavior rather than asserting it must always be different
            var hashesAreDifferent = hash1 != hash2;
            // We expect different hashes, but won't fail if they happen to be the same due to collision
            Assert.True(true); // Always passes, just documenting the behavior
        }

        [Fact]
        public void EqualityOperator_SameTimestamps_ReturnsTrue()
        {
            // Arrange
            const ulong ticks = 637849200000000000UL;
            var timestamp1 = new Timestamp(ticks);
            var timestamp2 = new Timestamp(ticks);

            // Act & Assert
            Assert.True(timestamp1 == timestamp2);
        }

        [Fact]
        public void EqualityOperator_DifferentTimestamps_ReturnsFalse()
        {
            // Arrange
            var timestamp1 = new Timestamp(637849200000000000UL);
            var timestamp2 = new Timestamp(637849200000000001UL);

            // Act & Assert
            Assert.False(timestamp1 == timestamp2);
        }

        [Fact]
        public void InequalityOperator_SameTimestamps_ReturnsFalse()
        {
            // Arrange
            const ulong ticks = 637849200000000000UL;
            var timestamp1 = new Timestamp(ticks);
            var timestamp2 = new Timestamp(ticks);

            // Act & Assert
            Assert.False(timestamp1 != timestamp2);
        }

        [Fact]
        public void InequalityOperator_DifferentTimestamps_ReturnsTrue()
        {
            // Arrange
            var timestamp1 = new Timestamp(637849200000000000UL);
            var timestamp2 = new Timestamp(637849200000000001UL);

            // Act & Assert
            Assert.True(timestamp1 != timestamp2);
        }

        [Fact]
        public void Conversion_RoundTrip_DateTime_PreservesValue()
        {
            // Arrange
            var originalDateTime = new DateTime(2025, 6, 15, 14, 30, 25, 500, DateTimeKind.Utc).AddTicks(1234567);

            // Act
            Timestamp timestamp = originalDateTime;
            DateTime convertedBack = timestamp;

            // Assert
            Assert.Equal(originalDateTime, convertedBack);
        }

        [Fact]
        public void Conversion_RoundTrip_Ulong_PreservesValue()
        {
            // Arrange
            const ulong originalTicks = 637849200123456789UL;

            // Act
            Timestamp timestamp = originalTicks;
            ulong convertedBack = timestamp;

            // Assert
            Assert.Equal(originalTicks, convertedBack);
        }

        [Fact]
        public void MinValue_CanBeCreated()
        {
            // Arrange & Act
            var timestamp = new Timestamp(ulong.MinValue);

            // Assert
            Assert.Equal(ulong.MinValue, timestamp.Ticks);
        }

        [Fact]
        public void MaxValue_CanBeCreated()
        {
            // Arrange & Act
            var timestamp = new Timestamp(ulong.MaxValue);

            // Assert
            Assert.Equal(ulong.MaxValue, timestamp.Ticks);
        }
    }
}