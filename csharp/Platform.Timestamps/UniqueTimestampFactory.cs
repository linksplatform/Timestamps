using System;
using System.Runtime.CompilerServices;
using System.Threading;
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
        /// <para>Создаёт отметку времени соответствующую текущей дате и времени по UTC или следующую уникальную отметку времени.</para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Timestamp Create()
        {
            var utcTicks = (ulong)DateTime.UtcNow.Ticks;
            ulong lastTicks, newTicks;
            do
            {
                lastTicks = _lastTicks;
                newTicks = utcTicks > lastTicks ? utcTicks : lastTicks + 1;
            }
            while (Interlocked.CompareExchange(ref _lastTicks, newTicks, lastTicks) != lastTicks);
            
            return new Timestamp(newTicks);
        }
    }
}
