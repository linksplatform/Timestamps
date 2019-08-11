using System;

namespace Platform.Timestamps
{
    /// <summary>
    /// Represents a timestamp.
    /// Представляет метку времени.
    /// </summary>
    /// <remarks>
    /// To make this timestamp truly unique, it is recommended to use <see cref="UniqueTimestampFactory"/>.
    /// Чтобы эта метка времени была дейстительно уникальна рекомендуется использовать <see cref="UniqueTimestampFactory"/>.
    /// </remarks>
    public struct Timestamp : IEquatable<Timestamp>
    {
        /// <summary>
        /// Returns a string containg the default DateTime format for Timestamp.
        /// Возвращает строку, содержащую формат даты и времени по умолчанию для метки времени.
        /// </summary>
        public static readonly string DefaultFormat = "yyyy.MM.dd hh:mm:ss.fffffff";

        /// <summary>
        /// Gets or sets the number of ticks that represent the date and time in UTC.
        /// Возвращает или устанавливает количество тиков, которые представляют дату и время в UTC.
        /// </summary>
        public readonly ulong Ticks;

        /// <summary>
        /// Creates a timestamp.
        /// Создаёт метку времени.
        /// </summary>
        /// <param name="ticks">A number representing the number of ticks. Число представляющие количество тиков.</param>
        public Timestamp(ulong ticks) => Ticks = ticks;

        /// <summary>
        /// Defines an implicit conversion of a DateTime to a Timestamp.
        /// Определяет неявное преобразование DateTime в метку времени.
        /// </summary>
        /// <param name="dateTime">The DateTime struct. Структура DateTime.</param>
        public static implicit operator Timestamp(DateTime dateTime) => new Timestamp((ulong)dateTime.ToUniversalTime().Ticks);

        /// <summary>
        /// Defines an implicit conversion of a Timestamp to a DateTime.
        /// Определяет неявное преобразование метки времени в DateTime.
        /// </summary>
        /// <param name="timestamp">The Timestamp. Отметка времени.</param>
        public static implicit operator DateTime(Timestamp timestamp) => new DateTime((long)timestamp.Ticks, DateTimeKind.Utc);

        /// <summary>
        /// Defines an implicit conversion of a 64-bit unsigned integer to a Timestamp.
        /// Определяет неявное преобразование 64-разрядного целого числа без знака в метку времени.
        /// </summary>
        /// <param name="ticks">The number of ticks represented as a 64-bit integer. Количество тиков представленное в виде 64-разрядного целого числа.</param>
        public static implicit operator Timestamp(ulong ticks) => new Timestamp(ticks);

        /// <summary>
        /// Defines an implicit conversion of a Timestamp to a 64-bit unsigned integer.
        /// Определяет неявное преобразование метки времени в 64-разрядное целое число без знака.
        /// </summary>
        /// <param name="timestamp">The Timestamp. Отметка времени.</param>
        public static implicit operator ulong(Timestamp timestamp) => timestamp.Ticks;

        /// <summary>
        /// Returns a string that represents the current Timestamp.
        /// Возвращает строку, которая представляет текущую метку времени.
        /// </summary>
        /// <returns>A string that represents the current Timestamp. Строка, представляющая текущую метку времени.</returns>
        public override string ToString() => ((DateTime)this).ToString(DefaultFormat);

        /// <summary>
        /// Определяет, равна ли текущая отметка времени другой отметке времени.
        /// Indicates whether the current Timestamp is equal to another Timestamp.
        /// </summary>
        /// <param name="other">Other Timestamp. Другая отметка времени.</param>
        /// <returns>True if the current Timestamp is equal to the other Timestamp; otherwise, false. Истину, если текущая отметка времени равна другой отметке времени; иначе ложь.</returns>
        public bool Equals(Timestamp other) => Ticks == other.Ticks;
    }
}
