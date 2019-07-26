using System;

namespace Platform.Timestamps
{
    /// <summary>
    /// Represents a timestamp.
    /// Представляет отметку времени.
    /// </summary>
    /// <remarks>
    /// To make this timestamp truly unique, it is recommended to use <see cref="UniqueTimestampFactory"/>.
    /// Чтобы эта метка была дейстительно уникальна рекомендуется использовать <see cref="UniqueTimestampFactory"/>.
    /// </remarks>
    public struct Timestamp : IEquatable<Timestamp>
    {
        public static readonly string DefaultFormat = "yyyy.MM.dd hh:mm:ss.fffffff";

        /// <summary>
        /// Gets or sets the number of ticks that represent the date and time in UTC.
        /// Возвращает или устанавливает количество тиков, которые представляют дату и время в UTC.
        /// </summary>
        public readonly ulong Ticks;

        public Timestamp(ulong ticks) => Ticks = ticks;

        public static implicit operator Timestamp(DateTime dateTime) => new Timestamp((ulong)dateTime.ToUniversalTime().Ticks);

        public static implicit operator DateTime(Timestamp timestamp) => new DateTime((long)timestamp.Ticks, DateTimeKind.Utc);

        public static implicit operator Timestamp(ulong ticks) => new Timestamp(ticks);

        public static implicit operator ulong(Timestamp timestamp) => timestamp.Ticks;

        public override string ToString() => ((DateTime)this).ToString(DefaultFormat);

        public bool Equals(Timestamp other) => Ticks == other.Ticks;
    }
}
