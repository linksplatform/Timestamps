using System;
using Platform.Interfaces;

namespace Platform.Timestamps
{
    /// <summary>
    /// Represents a factory for creating unique timestamps.
    /// Представляет фабрику по созданию уникальных отметок времени.
    /// </summary>
    public class UniqueTimestampFactory : IFactory<Timestamp>
    {
        private ulong _lastTicks;

        /// <summary>
        /// Creates a timestamp corresponding to the current UTC date and time or next unique timestamp
        /// Создаёт отмеку времени соответствующую текущей дате и времени по UTC или следующую уникальную отметку времени.
        /// </summary>
        public Timestamp Create()
        {
            var utcTicks = (ulong)DateTime.UtcNow.Ticks;
            return utcTicks <= _lastTicks ? new Timestamp(_lastTicks++) : new Timestamp(utcTicks);
        }
    }
}
