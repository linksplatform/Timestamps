using System;
using Platform.Interfaces;

namespace Platform.Timestamps
{
    /// <summary>
    /// <para>Represents a factory for creating unique timestamps.</para>
    /// <para>Представляет фабрику по созданию уникальных отметок времени.</para>
    /// </summary>
    public class UniqueTimestampFactory : IFactory<Timestamp>
    {
        private ulong _lastTicks;

        /// <summary>
        /// <para>Creates a timestamp corresponding to the current UTC date and time or next unique timestamp.</para>
        /// <para>Создаёт отмеку времени соответствующую текущей дате и времени по UTC или следующую уникальную отметку времени.</para>
        /// </summary>
        public Timestamp Create()
        {
            var utcTicks = (ulong)DateTime.UtcNow.Ticks;
            return utcTicks <= _lastTicks ? new Timestamp(_lastTicks++) : new Timestamp(utcTicks);
        }
    }
}
