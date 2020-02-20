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
    /// <para>Чтобы эта метка времени была дейстительно уникальна рекомендуется использовать <see cref="UniqueTimestampFactory"/>.</para>
    /// </remarks>
    public struct Timestamp : IEquatable<Timestamp>
    {
        /// <summary>
        /// <para>Returns a string containg the default DateTime format for Timestamp.</para>
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

        /// <summary>
        /// <para>Returns a string that represents the current Timestamp.</para>
        /// <para>Возвращает строку, которая представляет текущую метку времени.</para>
        /// </summary>
        /// <returns><para>A string that represents the current Timestamp.</para><para>Строка, представляющая текущую метку времени.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ((DateTime)this).ToString(DefaultFormat);

        /// <summary>
        /// <para>Определяет, равна ли текущая отметка времени другой отметке времени.</para>
        /// <para>Indicates whether the current Timestamp is equal to another Timestamp.</para>
        /// </summary>
        /// <param name="other"><para>Other Timestamp.</para><para>Другая отметка времени.</para></param>
        /// <returns><para>True if the current Timestamp is equal to the other Timestamp; otherwise, false.</para><para>Истину, если текущая отметка времени равна другой отметке времени; иначе ложь.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Timestamp other) => Ticks == other.Ticks;

        /// <summary>
        /// <para>Determines whether the specified object is equal to the current object.</para>
        /// <para>Определяет, равен ли указанный объект текущему объекту.</para>
        /// </summary>
        /// <param name="obj"><para>The object to compare with the current object.</para><para>Объект для сравнения с текущим объектом.</para></param>
        /// <returns><para>True if the specified object is equal to the current object; otherwise, false.</para><para>Истину, если указанный объект равен текущему объекту; иначе ложь.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Timestamp timestamp ? Equals(timestamp) : false;

        /// <summary>
        /// <para>Serves as the default hash function.</para>
        /// <para>Служит в качестве хэш-функции по умолчанию.</para>
        /// </summary>
        /// <returns><para>A hash code for the current object.</para><para>Хеш-код для текущего объекта.</para></returns>
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
