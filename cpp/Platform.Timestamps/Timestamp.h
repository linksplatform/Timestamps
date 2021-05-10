#pragma once

#include <string>
#include "CommonEraClock.h"

namespace Platform::Timestamps
{
    class Timestamp
    {
	public:
	    Timestamp(const uint64_t &ticks) : Ticks(ticks) { }
        Timestamp(const CommonEraClock &clock) : Ticks(CommonEraClock::now().time_since_epoch().count()) { }
        Timestamp() : Ticks(0) { }
        
        const inline static std::string DefaultFormat = "yyyy.MM.dd hh:mm:ss.fffffff";
        bool operator ==(const Timestamp &other) const { return Ticks == other.getTicks(); }
        bool operator !=(const Timestamp &other) const { return Ticks != other.getTicks(); }
		uint64_t getTicks() const { return this->Ticks; }
        // std::string ToString() { return ((DateTime)this).ToString(DefaultFormat); }
        // uint32_t GetHashCode() { return Ticks.GetHashCode(); }
	private:
        uint64_t Ticks;
    };

}
