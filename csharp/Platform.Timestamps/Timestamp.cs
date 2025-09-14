using System;
using System.Runtime.CompilerServices;

namespace Platform.Timestamps
{
    /// <summary>
    /// <para>Represents a timestamp.</para>
    /// <para>Представляет метку времени.</para>
    /// </summary>
    /// <remarks>
    /// <para>To make this timestamp truly unique, it is recommended to use <see cref="UniqueTimestampFactory"/>.</para>
    /// <para>Чтобы эта метка времени была действительно уникальна рекомендуется использовать <see cref="UniqueTimestampFactory"/>.</para>
    /// </remarks>
    public struct Timestamp : IEquatable<Timestamp>
    {
        /// <summary>
        /// <para>Returns a string containing the default DateTime format for Timestamp.</para>
        /// <para>Возвращает строку, содержащую формат даты и времени по умолчанию для метки времени.</para>
        /// </summary>
        public static readonly string DefaultFormat = "yyyy.MM.dd hh:mm:ss.fffffff";

        /// <summary>
        /// <para>Gets or sets the number of ticks that represent the date and time in UTC.</para>
        /// <para>Возвращает или устанавливает количество тиков, которые представляют дату и время в UTC.</para>
        /// </summary>
        public readonly ulong Ticks;

        /// <summary>
        /// <para>Creates a timestamp.</para>
        /// <para>Создаёт метку времени.</para>
        /// </summary>
        /// <param name="ticks"><para>A number representing the number of ticks.</para><para>Число представляющие количество тиков.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Timestamp(ulong ticks) => Ticks = ticks;

        /// <summary>
        /// <para>Defines an implicit conversion of a DateTime to a Timestamp.</para>
        /// <para>Определяет неявное преобразование DateTime в метку времени.</para>
        /// </summary>
        /// <param name="dateTime"><para>The DateTime struct.</para><para>Структура DateTime.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Timestamp(DateTime dateTime) => new Timestamp((ulong)dateTime.ToUniversalTime().Ticks);

        /// <summary>
        /// <para>Defines an implicit conversion of a Timestamp to a DateTime.</para>
        /// <para>Определяет неявное преобразование метки времени в DateTime.</para>
        /// </summary>
        /// <param name="timestamp"><para>The Timestamp.</para><para>Отметка времени.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator DateTime(Timestamp timestamp) => new DateTime((long)timestamp.Ticks, DateTimeKind.Utc);

        /// <summary>
        /// <para>Defines an implicit conversion of a 64-bit unsigned integer to a Timestamp.</para>
        /// <para>Определяет неявное преобразование 64-разрядного целого числа без знака в метку времени.</para>
        /// </summary>
        /// <param name="ticks"><para>The number of ticks represented as a 64-bit integer.</para><para>Количество тиков представленное в виде 64-разрядного целого числа.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Timestamp(ulong ticks) => new Timestamp(ticks);

        /// <summary>
        /// <para>Defines an implicit conversion of a Timestamp to a 64-bit unsigned integer.</para>
        /// <para>Определяет неявное преобразование метки времени в 64-разрядное целое число без знака.</para>
        /// </summary>
        /// <param name="timestamp"><para>The Timestamp.</para><para>Отметка времени.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator ulong(Timestamp timestamp) => timestamp.Ticks;

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ((DateTime)this).ToString(DefaultFormat);

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Timestamp other) => Ticks == other.Ticks;

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Timestamp timestamp ? Equals(timestamp) : false;

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => Ticks.GetHashCode();

        /// <summary>
        /// <para>Determines if the specified timestamp is equal to the current timestamp.</para>
        /// <para>Определяет, равна ли указанная метка времени текущей метке времени.</para>
        /// </summary>
        /// <param name="left"><para>The current timestamp.</para><para>Текущая метка времени.</para></param>
        /// <param name="right"><para>A timestamp to compare with this timestamp.</para><para>Метка времени для сравнения с этой меткой времени.</para></param>
        /// <returns><para>True if the current timestamp is equal to the other timestamp; otherwise, false.</para><para>True, если текущий метка времени равна другой метке времени; иначе false.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Timestamp left, Timestamp right) => left.Equals(right);

        /// <summary>
        /// <para>Determines if the specified timestamp is not equal to the current timestamp.</para>
        /// <para>Определяет, не равна ли указанная метка времени текущей метке времени.</para>
        /// </summary>
        /// <param name="left"><para>The current timestamp.</para><para>Текущая метка времени.</para></param>
        /// <param name="right"><para>A timestamp to compare with this timestamp.</para><para>Метка времени для сравнения с этой меткой времени.</para></param>
        /// <returns><para>True if the current timestamp is not equal to the other timestamp; otherwise, false.</para><para>True, если текущий метка времени не равна другой метке времени; иначе false.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Timestamp left, Timestamp right) => !(left == right);
    }
}
