#pragma once
#include "Timestamp.h"

namespace Platform::Timestamps
{
    class UniqueTimestampFactory
    {
    private: 
        std::uint64_t _lastTicks = 0;
    public: 
        Timestamp Create()
        {
            uint64_t utcTicks = CommonEraClock::now().time_since_epoch().count();
            _lastTicks = utcTicks > _lastTicks ? utcTicks : _lastTicks + 1;
            return Timestamp(_lastTicks);
        }
    };
}
